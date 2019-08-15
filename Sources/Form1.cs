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
        private bool gpSuccession_ValueChangedSema = false;

        private readonly int gpSuccession_Height;

        #region Form

        public Form1()
        {
            // The designer sometimes orders the control creation in an order
            // that would call event handlers already during initialization.
            // But not all variables are available yet, so we prevent access to them.
            gpSuccession_ValueChangedSema = true;
            InitializeComponent();
            gpSuccession_ValueChangedSema = false;

            gpSuccession_Height = gpSuccession.Height; // remember expanded size from designer settings

            pi = profileViewControl1.DataGridView1; // for better capsulation
            om = new OutModule(pi);
            sc = new Shortcuts(Handle);

            pi.ProfileChanged += DataGridView1_ProfileChanged;
            profileViewControl1.SelectedProfileChanged += profileViewControl1_SelectedProfileChanged;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += (sender2, certificate, chain, errors) => { return true; };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = Text + " - v" + Application.ProductVersion + " " + OsLayer.Name;
            btnHit.Select();
            pi.ProfileUpdateBegin();
            LoadSettings();
            ShowSuccessionMenu(false); // start collapsed
            pi.ProfileUpdateEnd(); // Write very first output once after application start (fires ProfileChanged with UpdateProgressAndTotals())
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
            gpSuccession.Top = Frame_Height - gpSuccession.Height - Pad_Frame;
            tabControl1.Height = gpSuccession.Top - tabControl1.Top - Pad_Controls;
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

        public bool IsOnScreen(int Left, int Top, int Width, int Height)
        {
            const int threshold = 10; // we add this to ensure moving at the outer borders of the screens is still fine
            const int rectSize = 30; // enough of the title bar that must be visible
            Rectangle rectLeft  = new Rectangle( Left      +threshold,          Top+threshold, rectSize, rectSize); // upper left corner
            Rectangle rectRight = new Rectangle( Left+Width-threshold-rectSize, Top+threshold, rectSize, rectSize); // upper right corner
            foreach( Screen screen in Screen.AllScreens )
            {
                // at least one of the edges must be present on any screen
                if( screen.WorkingArea.Contains( rectLeft ) ) return true;
                else if( screen.WorkingArea.Contains( rectRight ) ) return true;
            }
            return false;
        }

        private void GetCalculatedSums(ref int TotalHits, ref int TotalHitsWay, ref int TotalPB)
        {
            int Splits = pi.SplitCount;

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

        private void DataGridView1_ProfileChanged(object sender, EventArgs e)
        {
            UpdateProgressAndTotals();
        }

        private void UpdateProgressAndTotals()
        {
            int TotalHits = 0;
            int TotalHitsWay = 0;
            int TotalPB = 0;
            int Splits = pi.SplitCount;

            if (Splits < 0) // Check for valid entries
                lbl_progress.Text = "Progress:  ?? / ??  # " + pi.AttemptsCount.ToString("D3");
            else
            {
                GetCalculatedSums(ref TotalHits, ref TotalHitsWay, ref TotalPB);
                lbl_progress.Text = "Progress:  " + pi.ActiveSplit + " / " + Splits + "  # " + pi.AttemptsCount.ToString("D3");
            }

            lbl_totals.Text = "Total: " + (TotalHits + TotalHitsWay) + " Hits   " + TotalPB + " PB";

            om.Update();
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

        private void btnSave_Click(object sender, EventArgs e) { SaveSettings(); }

        private void btnWeb_Click(object sender, EventArgs e) { System.Diagnostics.Process.Start("https://github.com/topeterk/HitCounterManager"); }

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

        private void btnAbout_Click(object sender, EventArgs e) { new About().ShowDialog(this); }

        private void btnNew_Click(object sender, EventArgs e)
        {
            string name = InputBox("Enter name of new profile", "New profile", profileViewControl1.SelectedProfile);
            if (name.Length == 0) return;

            if (profileViewControl1.ComboBox1.Items.Contains(name))
            {
                if (DialogResult.OK != MessageBox.Show("A profile with this name already exists. Do you want to create as copy from the currently selected?", "Profile already exists", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                    return;

                btnCopy_Click(sender, e);
                return;
            }

            profs.SaveProfile(); // save previous selected profile

            // create, select and save new profile..
            profileViewControl1.ComboBox1.Items.Add(name);
            profileViewControl1.SelectedProfile = name;
            pi.ProfileName = name;
            profs.SaveProfile(); // save new empty profile
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (profileViewControl1.ComboBox1.Items.Count == 0) return;

            string name = InputBox("Enter new name for profile \"" + profileViewControl1.SelectedProfile + "\"!", "Rename profile", profileViewControl1.SelectedProfile);
            if (name.Length == 0) return;

            if (profileViewControl1.ComboBox1.Items.Contains(name))
            {
                MessageBox.Show("A profile with this name already exists!", "Profile already exists");
                return;
            }

            profs.RenameProfile(profileViewControl1.SelectedProfile, name);
            profileViewControl1.ComboBox1.Items[profileViewControl1.ComboBox1.SelectedIndex] = name;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            string name = profileViewControl1.SelectedProfile;

            do { name += " COPY"; } while (profileViewControl1.ComboBox1.Items.Contains(name)); // extend name till it becomes unique

            profs.SaveProfile(); // save previous selected profile

            // create, select and save new profile..
            profileViewControl1.ComboBox1.Items.Add(name);
            pi.ProfileName = name;
            profs.SaveProfile(); // copy current data to new profile
            profileViewControl1.SelectedProfile = name;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("Do you really want to delete profile \"" + profileViewControl1.SelectedProfile + "\"?", "Deleting profile", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                profs.DeleteProfile(profileViewControl1.SelectedProfile);
                profileViewControl1.DeleteSelectedProfile();
                profs.LoadProfile(profileViewControl1.SelectedProfile);
            }
        }

        private void btnAttempts_Click(object sender, EventArgs e)
        {
            string amount_string = InputBox("Enter amount to be set!", "Set new run number (amount of attempts)", pi.AttemptsCount.ToString());
            int amount_value;
            if (!int.TryParse(amount_string, out amount_value))
            {
                if (amount_string.Equals("")) return; // Unfortunately this is the Cancel button
                MessageBox.Show("Only numbers are allowed!");
                return;
            }
            pi.AttemptsCount = amount_value;
        }

        private void btnUp_Click(object sender, EventArgs e) { pi.PermuteSplit(pi.ActiveSplit, -1); }
        private void btnDown_Click(object sender, EventArgs e) { pi.PermuteSplit(pi.ActiveSplit, +1); }

        private void BtnInsertSplit_Click(object sender, EventArgs e) { pi.InsertSplit(); }

        private void BtnOnTop_Click(object sender, EventArgs e) { SetAlwaysOnTop(!this.TopMost); }

        private void btnReset_Click(object sender, EventArgs e)
        {
            bool SuccessionReset = true;
            if ((null != sender) && (cbShowPredecessor.Checked)) // avoid message box when not called from GUI (e.g. called by hot key)
            {
                DialogResult result = MessageBox.Show("Reset the currently running succession?\nYes: reset current profile and succession\nNo: reset current profile only\nCancel: abort reset", "Succession", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.No) SuccessionReset = false;
                else if (result == DialogResult.Cancel) return;
            }

            pi.ProfileUpdateBegin();
            pi.ResetRun();
            if (SuccessionReset)
            {
                SetSuccession(0, 0, 0, null, false);
                SuccessionChanged(null, null);
            }
            pi.ProfileUpdateEnd();
        }

        private void btnPB_Click(object sender, EventArgs e) { pi.setPB(); }

        private void btnHit_Click(object sender, EventArgs e) { pi.Hit(+1); }
        private void btnHitUndo_Click(object sender, EventArgs e) { pi.Hit(-1); }

        private void btnWayHit_Click(object sender, EventArgs e) { pi.WayHit(+1); }
        private void btnWayHitUndo_Click(object sender, EventArgs e) { pi.WayHit(-1); }

        private void btnSplit_Click(object sender, EventArgs e) { pi.MoveSplits(+1); }
        private void btnSplitPrev_Click(object sender, EventArgs e) { pi.MoveSplits(-1); }

        private void profileViewControl1_SelectedProfileChanged(object sender, ProfileViewControl.SelectedProfileChangedCauseType cause)
        {
            if ((cause != ProfileViewControl.SelectedProfileChangedCauseType.Delete) ||
                (cause != ProfileViewControl.SelectedProfileChangedCauseType.Init))
            {
                profs.SaveProfile(); // save currently selected profile
            }
            profs.LoadProfile(((ProfileViewControl)sender).SelectedProfile);
        }

        private void BtnSuccessionProceed_Click(object sender, EventArgs e)
        {
            int TotalHits = 0;
            int TotalHitsWay = 0;
            int TotalPB = 0;
            GetCalculatedSums(ref TotalHits, ref TotalHitsWay, ref TotalPB);
            SetSuccession(TotalHits, TotalHitsWay, TotalPB);
            ShowSuccessionMenu(true);
            SuccessionChanged(null, null);

            MessageBox.Show("The progress of this profile was saved.\nYou can select your next profile now!", "Succession", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    
        private void btnSuccessionVisibility_Click(object sender, EventArgs e) { ShowSuccessionMenu();  }

        /// <summary>
        /// Collapses or expands succession menu
        /// </summary>
        /// <param name="expand">TRUE = Expand, FALSE = Collapse, NULL = Toggle</param>
        private void ShowSuccessionMenu(Nullable<bool> expand = null)
        {
            int diff = 0;

            if (!expand.HasValue) expand = gpSuccession.Height != gpSuccession_Height; // Toggle

            if (expand.Value) // Expand..
            {
                diff = gpSuccession_Height - gpSuccession.Height;
                gpSuccession.Height = gpSuccession_Height;
                btnSuccessionVisibility.BackgroundImage = Sources.Resources.icons8_double_up_20;
            }
            else // Collapse..
            {
                diff = btnSuccessionVisibility.Height - gpSuccession.Height;
                gpSuccession.Height = btnSuccessionVisibility.Height;
                btnSuccessionVisibility.BackgroundImage = Sources.Resources.icons8_double_down_20;
            }
            Height += diff;
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
            }
        }

        #endregion

        private TabPage TabPageDragDrop = null;

        private void TabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Start of DragDrop
                TabPage hover_Tab = HoverTab();
                if (hover_Tab == null ) return;
                if (hover_Tab.Text.Equals("+")) return;
                TabPageDragDrop = hover_Tab;
            }
        }

        private void TabControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (null == TabPageDragDrop) return; // No DragDrop currently active

            if (e.Button != MouseButtons.Left)
            {
                // DragDrop stopped
                TabPageDragDrop = null;
            }
            else
            {
                // During DragDrop
                TabPage hover_Tab = HoverTab();
                if (hover_Tab == null) return;
                if (hover_Tab.Text.Equals("+") || (hover_Tab == TabPageDragDrop)) return;

                // Switch tabs but retain numbering
                int Index1 = tabControl1.TabPages.IndexOf(TabPageDragDrop);
                int Index2 = tabControl1.TabPages.IndexOf(hover_Tab);
                tabControl1.TabPages[Index1] = hover_Tab;
                tabControl1.TabPages[Index2] = TabPageDragDrop;
                hover_Tab.Text = (Index1+1).ToString();
                TabPageDragDrop.Text = (Index2+1).ToString();
                tabControl1.SelectedTab = TabPageDragDrop;
            }
        }

        private TabPage HoverTab()
        {
            for (int index = 0; index <= tabControl1.TabCount - 1; index++)
            {
                if (tabControl1.GetTabRect(index).Contains(tabControl1.PointToClient(Cursor.Position)))
                    return tabControl1.TabPages[index];
            }
            return null;
        }
    }
}
