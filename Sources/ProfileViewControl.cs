//MIT License

//Copyright (c) 2019-2019 Peter Kirmeier

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HitCounterManager
{
    public partial class ProfileViewControl : UserControl
    {
        public ProfileViewControl()
        {
            InitializeComponent();
        }

        public string SelectedProfile
        {
            get { return (string)ComboBox1.SelectedItem; }
            set { ComboBox1.SelectedItem = value; }
        }

        public event EventHandler<EventArgs> SelectedProfileChanged;
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != SelectedProfileChanged) SelectedProfileChanged(this, new EventArgs());
        }

        public void SetProfileList(string[] ProfileNames, string SelectProfile)
        {
            ComboBox1.Items.AddRange(ProfileNames);
            if (ComboBox1.Items.Count == 0)
            {
                SelectProfile = "Unnamed";
                ComboBox1.Items.Add(SelectProfile);
            }
            this.SelectedProfile = SelectProfile;
        }

    }

    public class ProfileDataGridView : DataGridView, IProfileInfo
    {
        #region DataGridView event handlers
        
        private bool ValueChangedSema = false;

        public ProfileDataGridView()
        {
            this.CellValidating += new DataGridViewCellValidatingEventHandler(this.CellValidatingHandler);
            this.CellValueChanged += new DataGridViewCellEventHandler(this.CellValueChangedHandler);
            this.CellMouseUp += new DataGridViewCellMouseEventHandler(this.CellMouseUpEventHandler);
            this.KeyUp += new KeyEventHandler(this.KeyUpEventHandler);
            this.SelectionChanged += new EventHandler(this.SelectionChangedHandler);
        }

        private void CellValidatingHandler(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (Rows[e.RowIndex].Cells[e.ColumnIndex].GetType().Name == "DataGridViewCheckBoxCell") return;

            if (e.ColumnIndex != Rows[0].Cells["cTitle"].ColumnIndex)
            {
                // We expect integers only here, so either it is of type int or can be converted
                if (!(e.FormattedValue is int))
                {
                    int i;
                    if (!int.TryParse((string)e.FormattedValue, out i))
                    {
                        e.Cancel = true;
                        MessageBox.Show("Must be numeric!");
                        return;
                    }
                }
                Rows[e.RowIndex].Cells[e.ColumnIndex].ValueType = typeof(int); // Force int otherwise it is most likely treated as string
            }
        }

        private void CellValueChangedHandler(object sender, DataGridViewCellEventArgs e)
        {
            if (ValueChangedSema) return;

            if (0 <= e.RowIndex && 0 <= e.ColumnIndex)
            {
                ProfileUpdateBegin();
                SetSplitDiff(e.RowIndex, GetSplitHits(e.RowIndex) + GetSplitWayHits(e.RowIndex) - GetSplitPB(e.RowIndex));

                // When the session progress selection has changed, make sure no other selection is active at the same time
                if (e.ColumnIndex == Rows[0].Cells["cSP"].ColumnIndex)
                {
                    ValueChangedSema = true;
                    for (int r = 0; r <= RowCount - 2; r++) Rows[r].Cells["cSP"].Value = false;
                    SetSessionProgress(e.RowIndex);
                    ValueChangedSema = false;
                }
                ProfileUpdateEnd();
            }
        }

        private void CellMouseUpEventHandler(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Workaround to fire CellValueChanged on a Checkbox change via mouse (left click) by switching cell focus
            if ((e.RowIndex < 0) || (e.ColumnIndex < 0)) return;

            if (Rows[e.RowIndex].Cells[e.ColumnIndex].GetType().Name == "DataGridViewCheckBoxCell")
            {
                // Care with changing the following sequence as during lots of testing
                // this is the first and only combination that works in Windows and Mono..
                ProfileUpdateBegin();
                EndEdit(); // will fire CellValueChanged
                ClearSelection();
                Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                Rows[e.RowIndex].Selected = true;
                ProfileUpdateEnd();
            }
        }

        private void KeyUpEventHandler(object sender, KeyEventArgs e)
        {
            // Workaround to fire CellValueChanged on a Checkbox change via keyboard (space) by switching cell focus
            if (e.KeyCode == Keys.Space)
            {
                foreach (DataGridViewCell cell in SelectedCells)
                {
                    if (cell.GetType().Name == "DataGridViewCheckBoxCell")
                    {
                        e.Handled = true;
                        if (cell.RowIndex >= SplitCount) return; // avoid creating a split from the "new line" row

                        // Care with changing the following sequence as during lots of testing
                        // this is the first and only combination that works in Windows and Mono..
                        DataGridViewCheckBoxCell SelectedCell = (DataGridViewCheckBoxCell)cell;
                        ProfileUpdateBegin();
                        SelectedCell.Value = !(SelectedCell.Value == null ? /*not set yet, so it's not checked*/ false : (bool)SelectedCell.Value); // will fire CellValueChanged
                        EndEdit();
                        ClearSelection();
                        Rows[SelectedCell.RowIndex].Cells[SelectedCell.ColumnIndex].Selected = true;
                        ProfileUpdateEnd();
                    }
                }
            }
        }

        private void SelectionChangedHandler(object sender, EventArgs e)
        {
            if (0 < SelectedCells.Count)
            {
                // Row could have been deleted ending up at the same index, so we should definitely treat it as usual update
                ProfileUpdateBegin();
                ActiveSplit = SelectedCells[0].RowIndex;
                ProfileUpdateEnd();
            }
        }

        #endregion

        #region IProfileInfo implementation

        private string _ProfileName = null;
        private int _AttemptsCounter = 0;
        private int LastActiveSplit = -1;
        private uint DataUpdatePending = 0;
        
        [Browsable(false)] // Hide from designer
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Hide from designer generator
        public string ProfileName
        {
            get { return _ProfileName; }
            set
            {
                if (_ProfileName != value)
                {
                    ProfileUpdateBegin();
                    _ProfileName = value;
                    ProfileUpdateEnd();
                }
            }
        }
        
        [Browsable(false)] // Hide from designer
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Hide from designer generator
        public int AttemptsCount
        {
            get { return _AttemptsCounter; }
            set
            {
                if (_AttemptsCounter != value)
                {
                    ProfileUpdateBegin();
                    _AttemptsCounter = value;
                    ProfileUpdateEnd();
                }
            }
        }
        
        [Browsable(false)] // Hide from designer
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Hide from designer generator
        public int SplitCount { get { return RowCount - 1; } } // Remove the "new line"
        
        [Browsable(false)] // Hide from designer
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Hide from designer generator
        public int ActiveSplit
        {
            get
            {
                if (0 == SelectedCells.Count) ActiveSplit = 0;
                return SelectedCells[0].RowIndex;
            }
            set
            {
                if ((LastActiveSplit != value) || (0 == SelectedCells.Count))
                {
                    LastActiveSplit = value;

                    ProfileUpdateBegin();
                    ClearSelection();
                    Rows[value].Selected = true;
                    ProfileUpdateEnd();
                }
            }
        }

        public void ClearSplits() { ProfileUpdateBegin(); Rows.Clear(); ProfileUpdateEnd(); }
        public void AddSplit(string Title, int Hits, int WayHits, int PB) { ProfileUpdateBegin(); Rows.Add(new object[] { Title, Hits, WayHits, Hits + WayHits - PB, PB, false }); ProfileUpdateEnd(); }
        public void InsertSplit()
        {
            int idx = ActiveSplit;

            ProfileUpdateBegin();
            Rows.Insert(idx, 1);
            // Select new row's title cell that user can directly start typing name of new split
            CurrentCell = Rows[idx].Cells["cTitle"];
            Rows[idx].Selected = true;
            Focus();
            ProfileUpdateEnd();
        }

        public void ResetRun()
        {
            ProfileUpdateBegin();
            AttemptsCount++; // Increase attempts
            for (int r = 0; r < SplitCount; r++) { SetSplitHits(r, 0); SetSplitWayHits(r, 0); }
            ActiveSplit = 0;
            ProfileUpdateEnd();
        }
        public void setPB()
        {
            int Splits = SplitCount;
            if (0 == Splits) return;

            ProfileUpdateBegin();
            for (int r = 0; r < Splits; r++) SetSplitPB(r, GetSplitHits(r) + GetSplitWayHits(r));
            ActiveSplit = Splits;
            SetSessionProgress(Splits-1);
            ProfileUpdateEnd();
        }
        public void Hit(int Amount)
        {
            int active = ActiveSplit;
            int hits = GetSplitHits(active) + Amount;
            if (hits < 0) hits = 0;

            ProfileUpdateBegin();
            SetSplitHits(active, hits);
            Rows[active].Selected = true; // row is already selected already but we make sure the whole row gets visually selected if user has selected a cell only
            ProfileUpdateEnd();
        }
        public void WayHit(int Amount)
        {
            int active = ActiveSplit;
            int hits = GetSplitWayHits(active) + Amount;
            if (hits < 0) hits = 0;

            ProfileUpdateBegin();
            SetSplitWayHits(active, hits);
            Rows[active].Selected = true; // row is already selected already but we make sure the whole row gets visually selected if user has selected a cell only
            ProfileUpdateEnd();
        }
        public void MoveSplits(int Amount)
        {
            int split = ActiveSplit + Amount;
            if ((0 <= split) && (split <= SplitCount))
            {
                ProfileUpdateBegin();
                ActiveSplit = split;
                if (0 < Amount) SetSessionProgress(split);
                ProfileUpdateEnd();
            }
        }
        public void PermuteSplit(int Index, int Offset)
        {
            int IndexDst = Index + Offset;
            if ((0 <= Index) && (Index < SplitCount) &&
                (0 <= IndexDst) && (IndexDst < SplitCount)) // Is permutation in range?
            {
                ProfileUpdateBegin();
                for (int i = 0; i <= Columns.Count - 1; i++)
                {
                    object cell = Rows[Index].Cells[i].Value;
                    Rows[Index].Cells[i].Value = Rows[IndexDst].Cells[i].Value;
                    Rows[IndexDst].Cells[i].Value = cell;
                }

                ActiveSplit = IndexDst;
                ProfileUpdateEnd();
            }
        }

        public int GetSessionProgress()
        {
            for (int Index = 0; Index < SplitCount; Index++)
            {
                if (GetCellValueOfType<bool>(Rows[Index].Cells["cSP"], false)) return Index;
            }
            return 0;
        }

        private T GetCellValueOfType<T>(DataGridViewCell Cell, T Default) {  try { return (null == Cell.Value ? Default : (T)Cell.Value); } catch { return Default; } }
        public string GetSplitTitle(int Index) { return GetCellValueOfType<string>(Rows[Index].Cells["cTitle"], ""); }
        public int GetSplitHits(int Index) { return GetCellValueOfType<int>(Rows[Index].Cells["cHits"], 0); }
        public int GetSplitWayHits(int Index) { return GetCellValueOfType<int>(Rows[Index].Cells["cWayHits"], 0); }
        public int GetSplitDiff(int Index) { return GetCellValueOfType<int>(Rows[Index].Cells["cDiff"], 0); }
        public int GetSplitPB(int Index) { return GetCellValueOfType<int>(Rows[Index].Cells["cPB"], 0); }

        public void SetSessionProgress(int Index, bool AllowReset = false)
        {
            if (SplitCount <= Index) return;

            if ((GetSessionProgress() <= Index) || AllowReset)
            {
                if (!GetCellValueOfType<bool>(Rows[Index].Cells["cSP"], false))
                {
                    if (null != Rows[Index].Cells["cSP"].Value)
                    {
                        ProfileUpdateBegin();
                        Rows[Index].Cells["cSP"].Value = true;
                        ProfileUpdateEnd();
                    }
                }
            }
        }

        public void SetSplitTitle(int Index, string Title)
        {
            if (GetSplitTitle(Index) != Title)
            {
                ProfileUpdateBegin();
                Rows[Index].Cells["cTitle"].Value = Title;
                ProfileUpdateEnd();
            }
        }
        public void SetSplitHits(int Index, int Hits)
        {
            if (GetSplitHits(Index) != Hits)
            {
                ProfileUpdateBegin();
                Rows[Index].Cells["cHits"].Value = Hits;
                ProfileUpdateEnd();
            }
        }
        public void SetSplitWayHits(int Index, int WayHits)
        {
            if (GetSplitWayHits(Index) != WayHits)
            {
                ProfileUpdateBegin();
                Rows[Index].Cells["cWayHits"].Value = WayHits;
                ProfileUpdateEnd();
            }
        }
        public void SetSplitDiff(int Index, int Diff)
        {
            if (GetSplitDiff(Index) != Diff)
            {
                ProfileUpdateBegin();
                Rows[Index].Cells["cDiff"].Value = Diff;
                ProfileUpdateEnd();
            }
        }
        public void SetSplitPB(int Index, int PBHits)
        {
            if (GetSplitPB(Index) != PBHits)
            {
                ProfileUpdateBegin();
                Rows[Index].Cells["cPB"].Value = PBHits;
                ProfileUpdateEnd();
            }
        }

        public void ProfileUpdateBegin() { DataUpdatePending++; }
        public void ProfileUpdateEnd()
        {
            if (0 < DataUpdatePending)  // check for safety - you never know
                DataUpdatePending--;

            if (0 == DataUpdatePending)
            {
                if (null != ProfileChanged) ProfileChanged(this, new EventArgs());
            }
        }
        
        public event EventHandler<EventArgs> ProfileChanged;

        #endregion
    }
}
