//MIT License

//Copyright (c) 2016-2019 Peter Kirmeier

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
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using TinyJson;

namespace HitCounterManager
{
    public partial class Form1 : Form
    {
        private const int WM_HOTKEY = 0x312;

        public Shortcuts sc;
        public OutModule om;

        private Profiles profs = new Profiles();
        private bool SettingsDialogOpen = false;
        private IProfileInfo pi;
        private bool DataGridView1_ValueChangedSema = false;
        private bool gpSuccession_ValueChangedSema = false;

        #region Form

        public Form1()
        {
            // The designer sometimes orders the control creation in an order
            // that would call event handlers already during initialization.
            // But not all variables are available yet, so we prevent access to them.
            DataGridView1_ValueChangedSema = true;
            gpSuccession_ValueChangedSema = true;
            InitializeComponent();
            gpSuccession_ValueChangedSema = false;
            DataGridView1_ValueChangedSema = false;

            pi = DataGridView1; // for better capsulation
            om = new OutModule(pi);
            sc = new Shortcuts(Handle);

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += (sender2, certificate, chain, errors) => { return true; };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = Text + " - v" + Application.ProductVersion + " " + OsLayer.Name;
            btnHit.Select();
            LoadSettings();
            UpdateProgressAndTotals();
            om.Update(true); // Write very first output once after application start
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save this session?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) SaveSettings();
            else if (result == DialogResult.Cancel) e.Cancel = true;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            const int Pad_Controls = 6;
            const int Pad_Frame = 15;

            int Frame_Width = ClientRectangle.Width;
            int Frame_Height = ClientRectangle.Height;

            // fill
            ComboBox1.Left = Pad_Frame;
            ComboBox1.Width = Frame_Width - Pad_Frame * 2;
            gpSuccession.Left = Pad_Frame;
            gpSuccession.Width = ComboBox1.Width;
            gpSuccession.Top = Frame_Height - gpSuccession.Height - Pad_Frame;
            DataGridView1.Left = Pad_Frame;
            DataGridView1.Width = ComboBox1.Width;
            DataGridView1.Height = gpSuccession.Top - DataGridView1.Top - Pad_Controls;

            // right aligned
            btnSuccessionProceed.Left = Frame_Width - btnSuccessionProceed.Width - Pad_Frame;
            btnSplit.Left = btnSuccessionProceed.Left - Pad_Controls - btnSplit.Width;
            btnWayHit.Left = btnSplit.Left - Pad_Controls - btnWayHit.Width;
            lbl_totals.Width = Frame_Width - lbl_totals.Left - Pad_Frame;

            // left aligned
            btnHit.Width = btnWayHit.Left - Pad_Controls - btnHit.Left;

            // succession group
            txtPredecessorTitle.Width = gpSuccession.Width - txtPredecessorTitle.Left - Pad_Frame;
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
                        case Shortcuts.SC_Type.SC_Type_HitUndo: btnHitUndo_Click(null, null); break;
                        case Shortcuts.SC_Type.SC_Type_SplitPrev: btnSplitPrev_Click(null, null); break;
                        case Shortcuts.SC_Type.SC_Type_WayHit: btnWayHit_Click(null, null); break;
                        case Shortcuts.SC_Type.SC_Type_WayHitUndo: btnWayHitUndo_Click(null, null); break;
                        case Shortcuts.SC_Type.SC_Type_PB: btnPB_Click(null, null); break;
                    }
                }
            }

            base.WndProc(ref m);
        }

        #endregion
        #region InputBox (replaceable with Microsoft.​Visual​Basic.Interaction.Input​Box)

        /// <summary>
        /// Creates an InputBox
        /// </summary>
        /// <returns>Value of UserInput</returns>
        /// <param name="Prompt">Prompt text</param>
        /// <param name="Title">Title</param>
        /// <param name="DefaultResponse">Initial user input value</param>
        public static string InputBox(string Prompt, string Title = "", string DefaultResponse = "")
        {
            Form inputBox = new Form();

            inputBox.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBox.ShowIcon = false;
            inputBox.ShowInTaskbar = false;
            inputBox.MaximizeBox = false;
            inputBox.MinimizeBox = false;
            inputBox.ClientSize = new System.Drawing.Size(500, 80);
            inputBox.Text = Title;

            Label label = new Label();
            label.Size = new System.Drawing.Size(inputBox.ClientSize.Width - 10, 23);
            label.Location = new System.Drawing.Point(5, 5);
            label.Text = Prompt;
            inputBox.Controls.Add(label);

            TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(inputBox.ClientSize.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, label.Location.Y + label.Size.Height + 10);
            textBox.Text = DefaultResponse;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(inputBox.ClientSize.Width - 80 - 80, textBox.Location.Y + textBox.Size.Height + 10);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = okButton.Size;
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(inputBox.ClientSize.Width - 80, okButton.Location.Y);
            inputBox.Controls.Add(cancelButton);

            inputBox.ClientSize = new System.Drawing.Size(inputBox.ClientSize.Width, cancelButton.Location.Y + cancelButton.Size.Height + 5);
            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            if (DialogResult.OK != inputBox.ShowDialog())
                return "";

            return textBox.Text;
        }

        #endregion
        #region Functions

        private void GetCalculatedSums(ref int TotalHits, ref int TotalHitsWay, ref int TotalPB)
        {
            int Splits = pi.GetSplitCount();

            TotalHits = TotalHitsWay = TotalPB = 0;
            for (int i = 0; i < Splits; i++)
            {
                TotalHits += pi.GetSplitHits(i);
                TotalHitsWay += pi.GetSplitWayHits(i);
                TotalPB += pi.GetSplitPB(i);
            }

            TotalHits += (int)numHits.Value;
            TotalHitsWay += (int)numHitsWay.Value;
            TotalPB += (int)numPB.Value;
        }

        private void UpdateProgressAndTotals()
        {
            int TotalHits = 0;
            int TotalHitsWay = 0;
            int TotalPB = 0;
            int Splits = pi.GetSplitCount();

            if (Splits < 0) // Check for valid entries
                lbl_progress.Text = "Progress:  ?? / ??  # " + pi.GetAttemptsCount().ToString("D3");
            else
            {
                GetCalculatedSums(ref TotalHits, ref TotalHitsWay, ref TotalPB);
                lbl_progress.Text = "Progress:  " + pi.GetActiveSplit() + " / " + Splits + "  # " + pi.GetAttemptsCount().ToString("D3");
            }

            lbl_totals.Text = "Total: " + (TotalHits + TotalHitsWay) + " Hits   " + TotalPB + " PB";

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

        private void SetAlwaysOnTop(bool AlwaysOnTop)
        {
            if (this.TopMost = AlwaysOnTop)
                btnOnTop.BackColor = Color.LimeGreen;
            else
                btnOnTop.BackColor = Color.FromKnownColor(KnownColor.Control);
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
            profs.SaveProfile(pi);
            SaveSettings();
        }

        private void btnWeb_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/topeterk/HitCounterManager");
        }

        private void btnCheckVersion_Click(object sender, EventArgs e)
        {
            try
            {
                WebClient client = new WebClient();

                // https://developer.github.com/v3/#user-agent-required
                client.Headers.Add("User-Agent", "HitCounterManager/" + Application.ProductVersion);
                // https://developer.github.com/v3/media/#request-specific-version
                client.Headers.Add("Accept", "application/vnd.github.v3.text+json");
                // https://developer.github.com/v3/repos/releases/#get-a-single-release
                string response = client.DownloadString("http://api.github.com/repos/topeterk/HitCounterManager/releases/latest");

                Dictionary<string, object> json = response.FromJson<Dictionary<string, object>>();
                if (json["tag_name"].ToString() == Application.ProductVersion.ToString())
                {
                    MessageBox.Show("You are using the latest version!", "Up to date!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Latest available version:\n\n   " + json["name"].ToString() + "\n\n" +
                        "Please visit the github project website (WWW button).\n" +
                        "Then look at the releases to download the new version.", "New version available!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred during update check!\n\n" + ex.Message.ToString(), "Check for updated failed!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            Form form = new About();
            form.ShowDialog(this);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            string name = InputBox("Enter name of new profile", "New profile", (string)ComboBox1.SelectedItem);
            if (name.Length == 0 || IsInvalidConfigString(name)) return;

            if (ComboBox1.Items.Contains(name))
            {
                if (DialogResult.OK != MessageBox.Show("A profile with this name already exists. Do you want to create as copy from the currently selected?", "Profile already exists", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                    return;

                btnCopy_Click(sender, e);
                return;
            }

            profs.SaveProfile(pi); // save previous selected profile

            // create, select and save new profile..
            ComboBox1.Items.Add(name);
            ComboBox1.SelectedItem = name;
            pi.SetProfileName(name);
            profs.SaveProfile(pi, true); // save new empty profile
            UpdateProgressAndTotals();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (ComboBox1.Items.Count == 0) return;

            string name = InputBox("Enter new name for profile \"" + (string)ComboBox1.SelectedItem + "\"!", "Rename profile", (string)ComboBox1.SelectedItem);
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

            profs.SaveProfile(pi); // save previous selected profile

            // create, select and save new profile..
            ComboBox1.Items.Add(name);
            pi.SetProfileName(name);
            profs.SaveProfile(pi, true); // copy current data to new profile
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
                
                profs.LoadProfile((string)ComboBox1.SelectedItem, pi);
            }
        }

        private void btnAttempts_Click(object sender, EventArgs e)
        {
            string amount_string = InputBox("Enter amount to be set!", "Set new run number (amount of attempts)", pi.GetAttemptsCount().ToString());
            int amount_value;
            if (!int.TryParse(amount_string, out amount_value))
            {
                if (amount_string.Equals("")) return; // Unfortunately this is the Cancel button
                MessageBox.Show("Only numbers are allowed!");
                return;
            }
            pi.SetAttemptsCount(amount_value);
            profs.SaveProfile(pi);
            UpdateProgressAndTotals();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int idx_old = pi.GetActiveSplit();
            int idx_new = idx_old - 1;

            if (0 <= idx_new && (idx_old < pi.GetSplitCount())) // Do not move when UP is not possible
            {
                om.DataUpdatePending = true;
                for (int i = 0; i <= DataGridView1.Columns.Count - 1; i++)
                {
                    object cell = DataGridView1.Rows[idx_old].Cells[i].Value;
                    DataGridView1.Rows[idx_old].Cells[i].Value = DataGridView1.Rows[idx_new].Cells[i].Value;
                    DataGridView1.Rows[idx_new].Cells[i].Value = cell;
                }
                om.DataUpdatePending = false;

                pi.SetActiveSplit(idx_new);
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int idx_old = pi.GetActiveSplit();
            int idx_new = idx_old + 1;

            if (idx_new < pi.GetSplitCount()) // Do not move when DOWN is not possible
            {
                om.DataUpdatePending = true;
                for (int i = 0; i <= DataGridView1.Columns.Count - 1; i++)
                {
                    object cell = DataGridView1.Rows[idx_old].Cells[i].Value;
                    DataGridView1.Rows[idx_old].Cells[i].Value = DataGridView1.Rows[idx_new].Cells[i].Value;
                    DataGridView1.Rows[idx_new].Cells[i].Value = cell;
                }
                om.DataUpdatePending = false;

                pi.SetActiveSplit(idx_new);
            }
        }

        private void BtnOnTop_Click(object sender, EventArgs e)
        {
            SetAlwaysOnTop(!this.TopMost);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            bool SuccessionReset = true;
            if ((null != sender) && (cbShowPredecessor.Checked)) // avoid message box when not called from GUI (e.g. called by hot key)
            {
                DialogResult result = MessageBox.Show("Reset the currently running succession?\nYes: reset current profile and succession\nNo: reset current profile only\nCancel: abort reset", "Succession", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.No) SuccessionReset = false;
                else if (result == DialogResult.Cancel) return;
            }

            om.DataUpdatePending = true;
            pi.SetAttemptsCount(pi.GetAttemptsCount() + 1); // Increase attempts
            for (int r = 0; r < pi.GetSplitCount(); r++) { pi.SetSplitHits(r, 0); pi.SetSplitWayHits(r, 0); }
            pi.SetActiveSplit(0);
            om.DataUpdatePending = false;

            if (SuccessionReset)
            {
                gpSuccession_ValueChangedSema = true;
                numHits.Value = 0;
                numHitsWay.Value = 0;
                numPB.Value = 0;
                cbShowPredecessor.Checked = false;
                gpSuccession_ValueChangedSema = false;
                SuccessionChanged(sender, e);
            }

            UpdateProgressAndTotals();
        }

        private void btnPB_Click(object sender, EventArgs e)
        {
            int Splits = pi.GetSplitCount();
            if (0 == Splits) return;

            om.DataUpdatePending = true;
            for (int r = 0; r < Splits; r++) pi.SetSplitPB(r, pi.GetSplitHits(r) + pi.GetSplitWayHits(r));
            pi.SetActiveSplit(Splits);
            pi.SetSessionProgress(Splits-1);
            om.DataUpdatePending = false;
            UpdateProgressAndTotals();
        }

        private void btnHit_Click(object sender, EventArgs e)
        {
            int active = pi.GetActiveSplit();
            pi.SetSplitHits(active, pi.GetSplitHits(active) + 1);
            pi.SetActiveSplit(active); // row is already selected already but we make sure the whole row gets visually selected if user has selected a cell only
            UpdateProgressAndTotals();
        }

        private void btnHitUndo_Click(object sender, EventArgs e)
        {
            int active = pi.GetActiveSplit();
            int hits = pi.GetSplitHits(active);
            if (0 < hits)
            {
                pi.SetSplitHits(active, hits - 1);
                pi.SetActiveSplit(active); // row is already selected already but we make sure the whole row gets visually selected if user has selected a cell only
                UpdateProgressAndTotals();
            }
        }
        private void btnWayHit_Click(object sender, EventArgs e)
        {
            int active = pi.GetActiveSplit();
            pi.SetSplitWayHits(active, pi.GetSplitWayHits(active) + 1);
            pi.SetActiveSplit(active); // row is already selected already but we make sure the whole row gets visually selected if user has selected a cell only
            UpdateProgressAndTotals();
        }

        private void btnWayHitUndo_Click(object sender, EventArgs e)
        {
            int active = pi.GetActiveSplit();
            int hits = pi.GetSplitWayHits(active);
            if (0 < hits)
            {
                pi.SetSplitWayHits(active, hits - 1);
                pi.SetActiveSplit(active); // row is already selected already but we make sure the whole row gets visually selected if user has selected a cell only
                UpdateProgressAndTotals();
            }
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            int next_index = pi.GetActiveSplit() + 1;
            if (next_index <= pi.GetSplitCount())
            {
                om.DataUpdatePending = true;
                pi.SetActiveSplit(next_index);
                if (next_index < pi.GetSplitCount()) pi.SetSessionProgress(next_index);
                om.DataUpdatePending = false;
                UpdateProgressAndTotals();
            }
        }

        private void btnSplitPrev_Click(object sender, EventArgs e)
        {
            int next_index = pi.GetActiveSplit() - 1;
            if (0 <= next_index)
            {
                om.DataUpdatePending = true;
                pi.SetActiveSplit(next_index);
                // we do not update session progress here as we don't know if it was already set on a previous run
                om.DataUpdatePending = false;
                UpdateProgressAndTotals();
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != pi.GetProfileName())
            {
                profs.SaveProfile(pi);
            }

            om.DataUpdatePending = true;
            profs.LoadProfile((string)ComboBox1.SelectedItem, pi);
            pi.SetProfileName((string)ComboBox1.SelectedItem);
            om.DataUpdatePending = false;
            UpdateProgressAndTotals();
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (0 < DataGridView1.SelectedCells.Count)
            {
                pi.SetActiveSplit(DataGridView1.SelectedCells[0].RowIndex);
                om.Update(true); // Row could have been deleted ending up at the same index, so we should definitely update
                UpdateProgressAndTotals();
            }
        }

        private void DataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType().Name == "DataGridViewCheckBoxCell") return;

            if (e.ColumnIndex == DataGridView1.Rows[0].Cells["cTitle"].ColumnIndex)
            {
                if (IsInvalidConfigString((string)e.FormattedValue))
                {
                    e.Cancel = true;
                }
                else
                {
                    // Make sure we mark any title changes as "modified"
                    pi.SetSplitTitle(e.RowIndex, (string)e.FormattedValue);
                }
            }
            else
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
                DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ValueType = typeof(int); // Force int otherwise it is most likely treated as string
            }
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridView1_ValueChangedSema) return;

            if (0 <= e.RowIndex && 0 <= e.ColumnIndex)
            {
                pi.SetSplitDiff(e.RowIndex, pi.GetSplitHits(e.RowIndex) + pi.GetSplitWayHits(e.RowIndex) - pi.GetSplitPB(e.RowIndex));

                // When the session progress selection has changed, make sure no other selection is active at the same time
                if (e.ColumnIndex == DataGridView1.Rows[0].Cells["cSP"].ColumnIndex)
                {
                    int idx = e.RowIndex;
                    if (idx < pi.GetSplitCount())
                    {
                        DataGridView1_ValueChangedSema = true;
                        for (int r = 0; r <= DataGridView1.RowCount - 2; r++) DataGridView1.Rows[r].Cells["cSP"].Value = false;
                        pi.SetSessionProgress(idx);
                        DataGridView1_ValueChangedSema = false;
                    }
                }
            }

            if (!om.DataUpdatePending) profs.SaveProfile(pi, true);
            UpdateProgressAndTotals();
        }

        private void DataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Workaround to fire CellValueChanged on a Checkbox change via mouse (left click) by switching cell focus
            if ((e.RowIndex < 0) || (e.ColumnIndex < 0)) return;

            if (DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType().Name == "DataGridViewCheckBoxCell")
            {
                // Care with changing the following sequence as during lots of testing
                // this is the first and only combination that works in Windows and Mono..
                om.DataUpdatePending = true;
                DataGridView1.EndEdit();
                DataGridView1.ClearSelection();
                om.DataUpdatePending = false;
                DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                DataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }

        private void DataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            // Workaround to fire CellValueChanged on a Checkbox change via keyboard (space) by switching cell focus
            if (e.KeyCode == Keys.Space)
            {
                foreach (DataGridViewCell cell in DataGridView1.SelectedCells)
                {
                    if (cell.GetType().Name == "DataGridViewCheckBoxCell")
                    {
                        DataGridViewCheckBoxCell SelectedCell = (DataGridViewCheckBoxCell)cell;
                        e.Handled = true;

                        // Care with changing the following sequence as during lots of testing
                        // this is the first and only combination that works in Windows and Mono..
                        om.DataUpdatePending = true;
                        SelectedCell.Value = !(SelectedCell.Value == null ? /*not set yet, so it's not checked*/ false : (bool)SelectedCell.Value);
                        om.DataUpdatePending = false;
                        DataGridView1.EndEdit();
                        DataGridView1.ClearSelection();
                        DataGridView1.Rows[SelectedCell.RowIndex].Cells[SelectedCell.ColumnIndex].Selected = true;
                    }
                }
            }
        }
        
        private void BtnSuccessionProceed_Click(object sender, EventArgs e)
        {
            int TotalHits = 0;
            int TotalHitsWay = 0;
            int TotalPB = 0;
            GetCalculatedSums(ref TotalHits, ref TotalHitsWay, ref TotalPB);
            SetSuccession(TotalHits, TotalHitsWay, TotalPB);
            SuccessionChanged(sender, e);

            MessageBox.Show("The progress of this profile was saved.\nYou can select your next profile now!", "Succession", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetSuccession(int TotalHits, int TotalHitsWay, int TotalPB, string Title = null, bool ShowPredecessor = true)
        {
            gpSuccession_ValueChangedSema = true;
            if (null != Title) txtPredecessorTitle.Text = Title;
            numHits.Value = TotalHits;
            numHitsWay.Value = TotalHitsWay;
            numPB.Value = TotalPB;
            cbShowPredecessor.Checked = ShowPredecessor;
            gpSuccession_ValueChangedSema = false;
        }

        private void SuccessionChanged(object sender, EventArgs e)
        {
            if (gpSuccession_ValueChangedSema) return;

            om.ShowSuccession = cbShowPredecessor.Checked;
            om.SuccessionTitle = txtPredecessorTitle.Text;
            om.SuccessionHits = (int)numHits.Value;
            om.SuccessionHitsWay = (int)numHitsWay.Value;
            om.SuccessionHitsPB = (int)numPB.Value;

            if (null != sender) // update on a GUI handler only
            {
                UpdateProgressAndTotals();
                om.Update(true);
            }
        }

        #endregion
    }

    public class ProfileDataGridView : DataGridView, IProfileInfo
    {
        private string _ProfileName = null;
        private int _AttemptsCounter = 0;
        private bool ModifiedFlag = false;
        private int LastActiveSplit = -1;

        public string GetProfileName() { return _ProfileName; }
        public void SetProfileName(string Name)
        {
            if (_ProfileName != Name)
            {
                ModifiedFlag = true;
                _ProfileName = Name;
            }
        }

        public int GetSplitCount() { return RowCount - 1; } // Remove the "new line"
        public int GetActiveSplit()
        {
            if (0 == SelectedCells.Count) SetActiveSplit(0);
            return SelectedCells[0].RowIndex;
        }
        public void SetActiveSplit(int Index)
        {
            if ((LastActiveSplit != Index) || (0 == SelectedCells.Count))
            {
                LastActiveSplit = Index;
                ModifiedFlag = true;

                ClearSelection();
                Rows[Index].Selected = true;
            }
        }

        public void ClearSplits() { Rows.Clear(); }
        public void AddSplit(string Title, int Hits, int WayHits, int PB) { ModifiedFlag = true; Rows.Add(new object[] { Title, Hits, WayHits, Hits + WayHits - PB, PB, false }); }

        public int GetAttemptsCount() { return _AttemptsCounter; }
        public void SetAttemptsCount(int Attempts)
        {
            if (_AttemptsCounter != Attempts)
            {
                ModifiedFlag = true;
                _AttemptsCounter = Attempts;
            }
        }

        public int GetSessionProgress()
        {
            for (int Index = 0; Index < GetSplitCount(); Index++)
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
            if ((GetSessionProgress() <= Index) || AllowReset)
            {
                if (!GetCellValueOfType<bool>(Rows[Index].Cells["cSP"], false))
                {
                    ModifiedFlag = true;
                    if (null != Rows[Index].Cells["cSP"].Value)
                        Rows[Index].Cells["cSP"].Value = true;
                }
            }
        }

        public void SetSplitTitle(int Index, string Title)
        {
            if (GetSplitTitle(Index) != Title)
            {
                ModifiedFlag = true;
                Rows[Index].Cells["cTitle"].Value = Title;
            }
        }
        public void SetSplitHits(int Index, int Hits)
        {
            if (GetSplitHits(Index) != Hits)
            {
                ModifiedFlag = true;
                Rows[Index].Cells["cHits"].Value = Hits;
            }
        }
        public void SetSplitWayHits(int Index, int WayHits)
        {
            if (GetSplitWayHits(Index) != WayHits)
            {
                ModifiedFlag = true;
                Rows[Index].Cells["cWayHits"].Value = WayHits;
            }
        }
        public void SetSplitDiff(int Index, int Diff)
        {
            if (GetSplitDiff(Index) != Diff)
            {
                ModifiedFlag = true;
                Rows[Index].Cells["cDiff"].Value = Diff;
            }
        }
        public void SetSplitPB(int Index, int PBHits)
        {
            if (GetSplitPB(Index) != PBHits)
            {
                ModifiedFlag = true;
                Rows[Index].Cells["cPB"].Value = PBHits;
            }
        }

        public bool HasChanged(bool Reset = false)
        {
            bool WasModified = ModifiedFlag;
            if (Reset) ModifiedFlag = false;
            return WasModified;
        }
    }
}
