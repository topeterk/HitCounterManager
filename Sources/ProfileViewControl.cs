//MIT License

//Copyright (c) 2019-2022 Peter Kirmeier

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
        private readonly IProfileInfo pi;
        [Browsable(false)] // Hide from designer
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Hide from designer generator
        public IProfileInfo ProfileInfo { get { return pi; } }

        public enum SelectedProfileChangedCauseType { Select, Init, Create, Rename, Delete };

        private SelectedProfileChangedCauseType SelectedProfileChangedCause = SelectedProfileChangedCauseType.Select;

        public ProfileViewControl()
        {
            InitializeComponent();
            pi = DataGridView1; // for better encapsulation
        }

        [Browsable(false)] // Hide from designer
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Hide from designer generator
        public bool ReadOnly
        {
            get { return DataGridView1.ReadOnly; }
            set { ComboBox1.SelectedItem = value; }
        }

        [Browsable(false)] // Hide from designer
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Hide from designer generator
        public string SelectedProfile
        {
            get { return (string)ComboBox1.SelectedItem; }
            set { ComboBox1.SelectedItem = value; }
        }

        public event EventHandler<SelectedProfileChangedCauseType> SelectedProfileChanged;
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null == SelectedProfileChanged) return;

            if (SelectedProfileChangedCause != SelectedProfileChangedCauseType.Rename)
            {
                DataGridView1.Visible = (0 < ComboBox1.Items.Count); // "Enable" via visibility to make it fully useable
                SelectedProfileChanged(this, SelectedProfileChangedCause); // Fire event
            }
            else pi.ProfileName = SelectedProfile; // Updating name at profile info is enough on renaming
        }

        public void SetProfileList(string[] ProfileNames, string SelectProfile)
        {
            SelectedProfileChangedCauseType SelectedProfileChangedCausePrev = SelectedProfileChangedCause;
            SelectedProfileChangedCause = SelectedProfileChangedCauseType.Init;

            // Workaround: Mono's scaling calculation results in wrong size
            //  - placement off for the PVC that already existed (due to designer initialization)
            //  - the new PVC copies are ok that were not selected during init
            //  - the new PVC copies are too small when tab was selected during init
            // Solution: Overwrite own calculated size again
            DataGridView1.Width = Width;
            DataGridView1.Height = Height - DataGridView1.Top;

            ComboBox1.Items.AddRange(ProfileNames);
            if (ComboBox1.Items.Count == 0)
            {
                SelectProfile = null;
                pi.ClearSplits();
            }
            if (SelectProfile != null) this.SelectedProfile = SelectProfile;

            SelectedProfileChangedCause = SelectedProfileChangedCausePrev;
        }

        public void CreateNewProfile(string NameNew, bool Select, bool AsCopy)
        {
            SelectedProfileChangedCauseType SelectedProfileChangedCausePrev = SelectedProfileChangedCause;
            SelectedProfileChangedCause = SelectedProfileChangedCauseType.Create;

            // Add new profile to the list..
            ComboBox1.Items.Add(NameNew);
            if (Select)
            {
                // Select and reset new profile..
                pi.ProfileName = NameNew;
                if (!AsCopy)
                {
                    pi.ClearSplits(); // Clear profile when it shall not be copied
                    pi.AttemptsCount = 0; // Reset Attempts
                }
                SelectedProfile = NameNew;
            }

            SelectedProfileChangedCause = SelectedProfileChangedCausePrev;
        }

        public void RenameProfile(string NameOld, string NameNew)
        {
            SelectedProfileChangedCauseType SelectedProfileChangedCausePrev = SelectedProfileChangedCause;
            SelectedProfileChangedCause = SelectedProfileChangedCauseType.Rename;

            ComboBox1.Items[ComboBox1.Items.IndexOf(NameOld)] = NameNew;

            SelectedProfileChangedCause = SelectedProfileChangedCausePrev;
        }

        public void DeleteSelectedProfile()
        {
            SelectedProfileChangedCauseType SelectedProfileChangedCausePrev = SelectedProfileChangedCause;
            SelectedProfileChangedCause = SelectedProfileChangedCauseType.Delete;

            int idx = ComboBox1.SelectedIndex;
            ComboBox1.Items.RemoveAt(idx);

            if (ComboBox1.Items.Count == 0)
            {
                ComboBox1.SelectedItem = null;
                pi.ClearSplits();
            }
            else ComboBox1.SelectedIndex = (ComboBox1.Items.Count >= idx ? ComboBox1.Items.Count - 1 : idx);

            SelectedProfileChangedCause = SelectedProfileChangedCausePrev;
        }

        public void DeleteProfile(string Name)
        {
            SelectedProfileChangedCauseType SelectedProfileChangedCausePrev = SelectedProfileChangedCause;
            SelectedProfileChangedCause = SelectedProfileChangedCauseType.Delete;

            int idx = ComboBox1.Items.IndexOf(Name);
            if (idx == ComboBox1.SelectedIndex)
            {
                ComboBox1.SelectedItem = null;
                pi.ClearSplits();
            }
            ComboBox1.Items.RemoveAt(idx);

            SelectedProfileChangedCause = SelectedProfileChangedCausePrev;
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
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return; // for safety
            DataGridViewCell cell = Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (!cell.Visible) return; // this avoids useless updates for profiles that are loaded in a non visible tab
            if (cell.GetType().Name == "DataGridViewCheckBoxCell") return;

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
                cell.ValueType = typeof(int); // Force int otherwise it is most likely treated as string
            }
        }

        private void CellValueChangedHandler(object sender, DataGridViewCellEventArgs e)
        {
            if (ValueChangedSema) return;
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return; // for safety
            if (!Rows[e.RowIndex].Cells[e.ColumnIndex].Visible) return; // this avoids useless updates for profiles that are loaded in a non visible tab

            if (e.ColumnIndex == Rows[0].Cells["cSP"].ColumnIndex)
            {
                // When the session progress selection has changed, make sure no other selection is active at the same time
                ProfileUpdateBegin();
                ValueChangedSema = true;
                for (int r = 0; r <= RowCount - 2; r++) Rows[r].Cells["cSP"].Value = false;
                SetSessionProgress(e.RowIndex);
                ValueChangedSema = false;
                ProfileUpdateEnd();
            }
            else if ((e.ColumnIndex == Rows[0].Cells["cHits"].ColumnIndex) ||
                     (e.ColumnIndex == Rows[0].Cells["cWayHits"].ColumnIndex) ||
                     (e.ColumnIndex == Rows[0].Cells["cPB"].ColumnIndex) )
            {
                SetSplitDiff(e.RowIndex, GetSplitHits(e.RowIndex) + GetSplitWayHits(e.RowIndex) - GetSplitPB(e.RowIndex));
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
            if (0 < DataUpdatePending) return; // this was no call from user, we can ignore it

            if (0 < SelectedCells.Count)
            {
                // Row could have been deleted ending up at the same index, so we should definitely treat it as usual update
                ProfileUpdateBegin();
                CurrentCell = SelectedCells[0]; // must be set that Mono on Linux sets focus simultaneously with selection
                ActiveSplit = SelectedCells[0].RowIndex;
                ProfileUpdateEnd();
            }
        }

        #endregion

        #region IProfileInfo implementation

        private class HiddenRowData {
            public long Duration = 0;
            public long DurationPB = 0;
            public long DurationGold = 0;

            public HiddenRowData() { }
            public HiddenRowData(long Duration, long DurationPB, long DurationGold)
            {
                this.Duration = Duration;
                this.DurationPB = DurationPB;
                this.DurationGold = DurationGold;
            }
        }

        private string _ProfileName = null;
        private int _AttemptsCounter = 0;
        private int LastActiveSplit = -1;
        private uint DataUpdatePending = 0;
        private bool RunCompleted = false;

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
                    if (value == Rows.Count - 1) RunCompleted = true;
                    ProfileUpdateEnd();
                }
            }
        }

        public void ClearSplits() { ProfileUpdateBegin(); Rows.Clear(); ProfileUpdateEnd(); }
        public void AddSplit(string Title, int Hits, int WayHits, int PB, long Duration, long DurationPB, long DurationGold)
        {
            ProfileUpdateBegin();
            Rows[Rows.Add(new object[] { Title, Hits, WayHits, Hits + WayHits - PB, PB, false })].Tag = new HiddenRowData(Duration, DurationPB, DurationGold);
            ProfileUpdateEnd();
        }
        public void InsertSplit()
        {
            int active = ActiveSplit;

            ProfileUpdateBegin();
            Rows.Insert(active, 1);
            Rows[active].Tag = new HiddenRowData();
            // Select new row's title cell that user can directly start typing name of new split
            CurrentCell = Rows[active].Cells["cTitle"];
            Rows[active].Selected = true;
            Focus();
            ProfileUpdateEnd();
        }
        public void DeleteSplit()
        {
            if (SplitCount < 1) return; // do not delete the "new line"

            int active = ActiveSplit;
            int Index = active + 1;

            ProfileUpdateBegin();
            if (SplitCount == 1)
            {
                // select "new line" and delete the last remaining row
                Rows[Index].Selected = true;
            }
            else
            {
                // select next or previouse row (and merge durations) and then delete the row
                if (SplitCount <= Index) Index = SplitCount - 2; // in case last row is deleted, choose previous one
                SetSplitDuration(Index, GetSplitDuration(Index) + GetSplitDuration(active));
                Rows[Index].Selected = true;
            }
            Rows.RemoveAt(active);
            Focus();
            ProfileUpdateEnd();
        }

        public void ResetRun()
        {
            long Duration;

            ProfileUpdateBegin();
            AttemptsCount++; // Increase attempts
            for (int r = 0; r < SplitCount; r++)
            {
                if (r != ActiveSplit) // current split is not finised yet
                {
                    Duration = GetSplitDuration(r);
                    if ((0 < Duration) && (Duration < GetSplitDurationGold(r))) SetSplitDurationGold(r, Duration);
                }
                SetSplitHits(r, 0);
                SetSplitWayHits(r, 0);
                SetSplitDuration(r, 0);
            }
            ActiveSplit = 0;
            ProfileUpdateEnd();
        }
        public void setPB()
        {
            int Splits = SplitCount;
            long Duration;

            if (0 == Splits) return;

            ProfileUpdateBegin();
            for (int r = 0; r < Splits; r++)
            {
                Duration = GetSplitDuration(r);
                if ((0 < Duration) && (Duration < GetSplitDurationGold(r))) SetSplitDurationGold(r, Duration);
                SetSplitPB(r, GetSplitHits(r) + GetSplitWayHits(r));
                SetSplitDurationPB(r, Duration);
            }
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
            Rows[active].Selected = true; // row is selected already but we make sure the whole row gets visually selected in case user has selected a cell only
            ProfileUpdateEnd();
        }
        public void WayHit(int Amount)
        {
            int active = ActiveSplit;
            int hits = GetSplitWayHits(active) + Amount;
            if (hits < 0) hits = 0;

            ProfileUpdateBegin();
            SetSplitWayHits(active, hits);
            Rows[active].Selected = true; // row is selected already but we make sure the whole row gets visually selected in case user has selected a cell only
            ProfileUpdateEnd();
        }
        public void GoSplits(int Amount)
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
                
                HiddenRowData data = (HiddenRowData)Rows[Index].Tag;
                Rows[Index].Tag = Rows[IndexDst].Tag;
                Rows[IndexDst].Tag = data;

                ActiveSplit = IndexDst;
                ProfileUpdateEnd();
            }
        }
        public void AddDuration(long Duration)
        {
            int active = ActiveSplit;
            long duration = GetSplitDuration(active) + Duration;
            if (duration < 0) duration = 0;

            // We don't mark profile as updated here as this would generate output very very often!
            SetSplitDuration(active, duration);
        }

        public int GetSessionProgress()
        {
            for (int Index = 0; Index < SplitCount; Index++)
            {
                if (GetCellValueOfType<bool>(Rows[Index].Cells["cSP"], false)) return Index;
            }
            return 0;
        }

        private HiddenRowData GetRowTagData(int Index) { if (null == Rows[Index].Tag) Rows[Index].Tag = new HiddenRowData(); return (HiddenRowData)Rows[Index].Tag; }
        private T GetCellValueOfType<T>(DataGridViewCell Cell, T Default) {  try { return (null == Cell.Value ? Default : (T)Cell.Value); } catch { return Default; } }
        public string GetSplitTitle(int Index) { return GetCellValueOfType<string>(Rows[Index].Cells["cTitle"], ""); }
        public int GetSplitHits(int Index) { return GetCellValueOfType<int>(Rows[Index].Cells["cHits"], 0); }
        public int GetSplitWayHits(int Index) { return GetCellValueOfType<int>(Rows[Index].Cells["cWayHits"], 0); }
        public int GetSplitDiff(int Index) { return GetCellValueOfType<int>(Rows[Index].Cells["cDiff"], 0); }
        public int GetSplitPB(int Index) { return GetCellValueOfType<int>(Rows[Index].Cells["cPB"], 0); }
        public long GetSplitDuration(int Index) { return GetRowTagData(Index).Duration; }
        public long GetSplitDurationPB(int Index) { return GetRowTagData(Index).DurationPB; }
        public long GetSplitDurationGold(int Index) { return GetRowTagData(Index).DurationGold; }

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
        public void SetSplitDuration(int Index, long Duration)
        {
            // We don't mark profile as updated here as this would generate output very very often!
            GetRowTagData(Index).Duration = Duration;
        }
        public void SetSplitDurationPB(int Index, long Duration)
        {
            ProfileUpdateBegin();
            GetRowTagData(Index).DurationPB = Duration;
            ProfileUpdateEnd();
        }
        public void SetSplitDurationGold(int Index, long Duration)
        {
            ProfileUpdateBegin();
            GetRowTagData(Index).DurationGold = Duration;
            ProfileUpdateEnd();
        }

        public void ProfileUpdateBegin() { DataUpdatePending++; }
        public void ProfileUpdateEnd()
        {
            if (0 < DataUpdatePending)  // check for safety - you never know
                DataUpdatePending--;

            if (0 == DataUpdatePending)
            {
                if (null != ProfileChanged)
                {
                    ProfileChangedEventArgs args = new ProfileChangedEventArgs();
                    args.RunCompleted = RunCompleted;
                    RunCompleted = false;
                    ProfileChanged(this, args);
                }
            }
        }
        
        public event EventHandler<ProfileChangedEventArgs> ProfileChanged;

        #endregion
    }
}
