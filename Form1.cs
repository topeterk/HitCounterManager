//MIT License

//Copyright(c) 2016-2018 Peter Kirmeier

//Permission Is hereby granted, free Of charge, to any person obtaining a copy
//of this software And associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, And/Or sell
//copies of the Software, And to permit persons to whom the Software Is
//furnished to do so, subject to the following conditions:

//The above copyright notice And this permission notice shall be included In all
//copies Or substantial portions of the Software.

//THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or
//IMPLIED, INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE And NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS Or COPYRIGHT HOLDERS BE LIABLE For ANY CLAIM, DAMAGES Or OTHER
//LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or OTHERWISE, ARISING FROM,
//OUT OF Or IN CONNECTION WITH THE SOFTWARE Or THE USE Or OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace HitCounterManager
{
    public partial class Form1 : Form
    {
        private const int WM_HOTKEY = 0x312;

        public Shortcuts sc;
        public OutModule om;

        private Profiles profs = new Profiles();
        private int _AttemptsCounter = 0;
        private string ComboBox1PrevSelectedItem = null;
        private bool SettingsDialogOpen = false;
        private IProfileInfo pi;

        // Sync AttemptsCounter with output module
        private int AttemptsCounter
        {
            get { return _AttemptsCounter; }
            set
            {
                _AttemptsCounter = value;
                om.AttemptsCount = value;
            }
        }

        #region Form

        public Form1()
        {
            InitializeComponent();
            pi = DataGridView1; // for better capsulation
            om = new OutModule(pi);
            sc = new Shortcuts(Handle);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = Text + " - v" + Application.ProductVersion;
            LoadSettings();
            UpdateProgressAndTotals();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            const int Pad = 13;

            // fill
            ComboBox1.Width = Width - Pad * 2 - 15;
            DataGridView1.Left = Pad;
            DataGridView1.Width = ComboBox1.Width;
            DataGridView1.Height = Height - DataGridView1.Top - Pad - btnSettings.Height - 15;

            // right aligned
            btnSplit.Left = Width - Pad - 15 - btnSplit.Width;
            lbl_totals.Width = Width - Pad - 15 - lbl_totals.Left;

            // left aligned
            btnHit.Width = btnSplit.Left - Pad / 2 - btnHit.Left;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                if (!SettingsDialogOpen)
                {
                    switch ((Shortcuts.SC_Type)m.WParam)
                    {
                        case Shortcuts.SC_Type.SC_Type_Reset: btnReset_Click(null, null); break;
                        case Shortcuts.SC_Type.SC_Type_Hit: btnHit_Click(null, null); break;
                        case Shortcuts.SC_Type.SC_Type_Split: btnSplit_Click(null, null); break;
                    }
                }
            }

            base.WndProc(ref m);
        }

        #endregion
        #region Functions

        private void UpdateProgressAndTotals()
        {
            int TotalHits = 0;
            int TotalPB = 0;
            int Splits = pi.GetSplitCount();

            if (Splits < 0) // Check for valid entries
                lbl_progress.Text = "Progress:  ?? / ??  # " + AttemptsCounter.ToString("D3");
            else
            {
                for (int r = 0; r < Splits; r++)
                {
                    TotalHits = TotalHits + pi.GetSplitHits(r);
                    TotalPB = TotalPB + pi.GetSplitPB(r);
                }

                lbl_progress.Text = "Progress:  " + pi.GetActiveSplit() + " / " + Splits + "  # " + AttemptsCounter.ToString("D3");
            }

            lbl_totals.Text = "Total: " + TotalHits + " Hits   " + TotalPB + " PB";

            om.Update();
        }

        private bool IsInvalidConfigString(string str)
        {
            bool isInvalid = false;
            string errormessage = "";

            foreach (string s in new string[] { ";", "|", "<", ">" })
            {
                if (str.Contains(s))
                {
                    errormessage += "Not allowed to use \";\"!" + Environment.NewLine;
                    isInvalid = true;
                }
            }

            if (isInvalid) MessageBox.Show(errormessage);
            return isInvalid;
        }

        #endregion
        #region UI

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Form form = new Settings();
            SettingsDialogOpen = true;
            form.ShowDialog(this);
            SettingsDialogOpen = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            profs.SaveProfileFrom((string)ComboBox1.SelectedItem, pi, AttemptsCounter);
            SaveSettings();
        }

        private void btnWeb_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/topeterk/HitCounterManager");
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            Form form = new About();
            form.ShowDialog(this);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            string name = Interaction.InputBox("Enter name of new profile", "New profile", (string)ComboBox1.SelectedItem);
            if (name.Length == 0 || IsInvalidConfigString(name)) return;

            if (ComboBox1.Items.Contains(name))
            {
                if (DialogResult.OK != MessageBox.Show("A profile with this name already exists. Do you want to create as copy from the currently selected?", "Profile already exists", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                    return;

                btnCopy_Click(sender, e);
                return;
            }

            profs.SaveProfileFrom((string)ComboBox1.SelectedItem, pi, AttemptsCounter); // save previous selected profile

            // create, select and save new profile..
            ComboBox1.Items.Add(name);
            ComboBox1.SelectedItem = name;
            profs.SaveProfileFrom(name, pi, AttemptsCounter, true); // save new empty profile
            UpdateProgressAndTotals();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (ComboBox1.Items.Count == 0) return;

            string name = Interaction.InputBox("Enter new name for profile \"" + (string)ComboBox1.SelectedItem + "\"!", "Rename profile", (string)ComboBox1.SelectedItem);
            if (name.Length == 0 || IsInvalidConfigString(name)) return;

            if (ComboBox1.Items.Contains(name))
            {
                MessageBox.Show("A profile with this name already exists!", "Profile already exists");
                return;
            }

            profs.RenameProfile((string)ComboBox1.SelectedItem, name);
            ComboBox1.Items[ComboBox1.SelectedIndex] = name;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            string name = (string)ComboBox1.SelectedItem;

            do { name += " COPY"; } while (ComboBox1.Items.Contains(name)); // extend name till it becomes unique

            profs.SaveProfileFrom((string)ComboBox1.SelectedItem, pi, AttemptsCounter); // save previous selected profile

            // create, select and save new profile..
            ComboBox1.Items.Add(name);
            profs.SaveProfileFrom(name, pi, AttemptsCounter, true); // copy current data to new profile
            ComboBox1.SelectedItem = name;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ComboBox1.Items.Count == 0) return;

            if (DialogResult.OK == MessageBox.Show("Do you really want to delete profile \"" + (string)ComboBox1.SelectedItem + "\"?", "Deleting profile", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                int idx = ComboBox1.SelectedIndex;

                profs.DeleteProfile((string)ComboBox1.SelectedItem);
                ComboBox1.Items.RemoveAt(idx);

                if (ComboBox1.Items.Count == 0)
                {
                    ComboBox1.Items.Add("Unnamed");
                    ComboBox1.SelectedIndex = 0;
                }
                else if (ComboBox1.Items.Count >= idx)
                    ComboBox1.SelectedIndex = ComboBox1.Items.Count - 1;
                else
                    ComboBox1.SelectedIndex = idx;

                int CsharpWorkaroundForBadPropertyImplementation = AttemptsCounter; // getter/setter cannot be passed as reference
                profs.LoadProfileInto((string)ComboBox1.SelectedItem, pi, ref CsharpWorkaroundForBadPropertyImplementation);
                AttemptsCounter = CsharpWorkaroundForBadPropertyImplementation;
            }
        }

        private void btnAttempts_Click(object sender, EventArgs e)
        {
            string amount_string = Interaction.InputBox("Enter amount to be set!", "Set amount of attempts", AttemptsCounter.ToString());
            int amount_value;
            if (!int.TryParse(amount_string, out amount_value))
            {
                if (amount_string.Equals("")) return; // Unfortunately this is the Cancel button
                MessageBox.Show("Only numbers are allowed!");
                return;
            }
            AttemptsCounter = amount_value;
            profs.SaveProfileFrom((string)ComboBox1PrevSelectedItem, pi, AttemptsCounter);
            UpdateProgressAndTotals();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int idx_old = pi.GetActiveSplit();
            int idx_new = idx_old - 1;

            if (0 <= idx_new && (idx_old < pi.GetSplitCount())) // Do not move when UP is not possible
            {
                for (int i = 0; i <= DataGridView1.Columns.Count - 1; i++)
                {
                    object cell = DataGridView1.Rows[idx_old].Cells[i].Value;
                    DataGridView1.Rows[idx_old].Cells[i].Value = DataGridView1.Rows[idx_new].Cells[i].Value;
                    DataGridView1.Rows[idx_new].Cells[i].Value = cell;
                }

                pi.SetActiveSplit(idx_new);
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int idx_old = pi.GetActiveSplit();
            int idx_new = idx_old + 1;

            if (idx_new < pi.GetSplitCount()) // Do not move when DOWN is not possible
            {
                for (int i = 0; i <= DataGridView1.Columns.Count - 1; i++)
                {
                    object cell = DataGridView1.Rows[idx_old].Cells[i].Value;
                    DataGridView1.Rows[idx_old].Cells[i].Value = DataGridView1.Rows[idx_new].Cells[i].Value;
                    DataGridView1.Rows[idx_new].Cells[i].Value = cell;
                }

                pi.SetActiveSplit(idx_new);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            AttemptsCounter++; // Increase attempts

            for (int r = 0; r < pi.GetSplitCount(); r++) pi.SetSplitHits(r, 0);

            pi.SetActiveSplit(0);
            UpdateProgressAndTotals();
        }

        private void btnPB_Click(object sender, EventArgs e)
        {
            int Splits = pi.GetSplitCount();
            if (0 == Splits) return;

            for (int r = 0; r < Splits; r++) pi.SetSplitPB(r, pi.GetSplitHits(r));

            pi.SetActiveSplit(Splits);
            pi.SetSessionProgress(Splits-1);
            UpdateProgressAndTotals();
        }

        private void btnHit_Click(object sender, EventArgs e)
        {
            int active = pi.GetActiveSplit();
            pi.SetSplitHits(active, pi.GetSplitHits(active) + 1);
            pi.SetActiveSplit(active); // row is already selected already but we make sure the whole row gets visually selected if user has selected a cell only
            UpdateProgressAndTotals();
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            int next_index = pi.GetActiveSplit() + 1;
            int session_progress = next_index;

            if (next_index <= pi.GetSplitCount())
            {
                pi.SetActiveSplit(next_index);
                if (next_index < pi.GetSplitCount()) pi.SetSessionProgress(next_index);
            }
            UpdateProgressAndTotals();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != ComboBox1PrevSelectedItem)
            {
                profs.SaveProfileFrom(ComboBox1PrevSelectedItem, pi, AttemptsCounter);
            }

            int CsharpWorkaroundForBadPropertyImplementation = AttemptsCounter; // getter/setter cannot be passed as reference
            profs.LoadProfileInto((string)ComboBox1.SelectedItem, pi, ref CsharpWorkaroundForBadPropertyImplementation);
            AttemptsCounter = CsharpWorkaroundForBadPropertyImplementation;

            ComboBox1PrevSelectedItem = (string)ComboBox1.SelectedItem;
            UpdateProgressAndTotals();
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (0 < DataGridView1.SelectedCells.Count) UpdateProgressAndTotals();
        }

        private void DataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType().Name == "DataGridViewCheckBoxCell") return;

            if (IsInvalidConfigString((string)e.FormattedValue))
            {
                e.Cancel = true;
                return;
            }

            if (e.ColumnIndex != DataGridView1.Rows[0].Cells["cTitle"].ColumnIndex)
            {
                int i;
                if (!int.TryParse((string)e.FormattedValue, out i))
                {
                    e.Cancel = true;
                    MessageBox.Show("Must be numeric!");
                }
                else DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ValueType = typeof(int); // Force int otherwise it is most likely treated as string
            }
        }

        private bool SemaValueChange = false;
        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (SemaValueChange) return;

            if (0 <= e.RowIndex && 0 <= e.ColumnIndex)
            {
                pi.SetSplitDiff(e.RowIndex, pi.GetSplitHits(e.RowIndex) - pi.GetSplitPB(e.RowIndex));

                if (e.ColumnIndex == DataGridView1.Rows[0].Cells["cSP"].ColumnIndex)
                {
                    int idx = e.RowIndex;
                    if (idx < pi.GetSplitCount())
                    {
                        SemaValueChange = true;
                        for (int r = 0; r <= DataGridView1.RowCount - 2; r++) DataGridView1.Rows[r].Cells["cSP"].Value = false;
                        pi.SetSessionProgress(idx);
                        SemaValueChange = false;
                    }
                }
            }

            profs.SaveProfileFrom((string)ComboBox1.SelectedItem, DataGridView1, AttemptsCounter, true);
            UpdateProgressAndTotals();
        }

        private void DataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Workaround to fire CellValueChanged on a Checkbox change via mouse (left click) by switching cell focus
            if ((e.RowIndex < 0) || (e.ColumnIndex < 0)) return;

            if (DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType().Name == "DataGridViewCheckBoxCell")
            {
                DataGridView1.Rows[DataGridView1.RowCount - 1].Cells[DataGridView1.ColumnCount - 1].Selected = true;
                DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
            }
        }

        private void DataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            // Workaround to fire CellValueChanged on a Checkbox change via keyboard (space) by switching cell focus
            if (e.KeyCode == Keys.Space)
            {
                if (DataGridView1.SelectedCells[0].GetType().Name == "DataGridViewCheckBoxCell")
                {
                    DataGridViewCheckBoxCell SelectedCell = (DataGridViewCheckBoxCell)DataGridView1.SelectedCells[0];
                    e.Handled = true;

                    SelectedCell.Value = !(bool)SelectedCell.Value;
                    DataGridView1.Rows[DataGridView1.RowCount - 1].Cells[DataGridView1.ColumnCount - 1].Selected = true;
                    DataGridView1.Rows[SelectedCell.RowIndex].Cells[SelectedCell.ColumnIndex].Selected = true;
                }
            }
        }

        #endregion
    }

    public class ProfileDataGridView : DataGridView, IProfileInfo
    {
        public int GetSplitCount() { return RowCount - 1; } // Remove the "new line"
        public int GetActiveSplit()
        {
            if (0 == SelectedCells.Count) SetActiveSplit(0);
            return SelectedCells[0].RowIndex;
        }
        public void SetActiveSplit(int Index) { ClearSelection(); Rows[Index].Selected = true; }

        public void ClearSplits() { Rows.Clear(); }
        public void AddSplit(string Title, int Hits, int PB) { Rows.Add(new object[] { Title, Hits, Hits - PB, PB, false }); }

        public int GetSessionProgress()
        {
            for (int Index = 0; Index < GetSplitCount(); Index++)
            {
                if ((bool)(Rows[Index].Cells["cSP"].Value)) return Index;
            }
            return 0;
        }

        public string GetSplitTitle(int Index) { return (null == Rows[Index].Cells["cTitle"].Value ? "" : (string)Rows[Index].Cells["cTitle"].Value); }
        public int GetSplitHits(int Index) { return (null == Rows[Index].Cells["cHits"].Value ? 0 : (int)Rows[Index].Cells["cHits"].Value); }
        public int GetSplitDiff(int Index) { return (null == Rows[Index].Cells["cDiff"].Value ? 0 : (int)Rows[Index].Cells["cDiff"].Value); }
        public int GetSplitPB(int Index) { return (null == Rows[Index].Cells["cPB"].Value ? 0 : (int)Rows[Index].Cells["cPB"].Value); }

        public void SetSessionProgress(int Index, bool AllowReset = false)
        {
            if ((GetSessionProgress() <= Index) || AllowReset) Rows[Index].Cells["cSP"].Value = true;
        }

        public void SetSplitTitle(int Index, string Title) { Rows[Index].Cells["cTitle"].Value = Title; }
        public void SetSplitHits(int Index, int Hits) { Rows[Index].Cells["cHits"].Value = Hits; }
        public void SetSplitDiff(int Index, int Diff) { Rows[Index].Cells["cDiff"].Value = Diff; }
        public void SetSplitPB(int Index, int PBHits) { Rows[Index].Cells["cPB"].Value = PBHits; }
    }
}
