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

        public readonly Shortcuts sc;
        public readonly OutModule om;

        private bool SettingsDialogOpen = false;

        #region Form

        public Form1()
        {
            InitializeComponent();

            om = new OutModule(profCtrl.ProfileTabControl);
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
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save this session?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) SaveSettings();
            else if (result == DialogResult.Cancel) e.Cancel = true;
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

        private void btnNew_Click(object sender, EventArgs e) { profCtrl.ProfileNew(); }
        private void btnRename_Click(object sender, EventArgs e) { profCtrl.ProfileRename(); }
        private void btnCopy_Click(object sender, EventArgs e) { profCtrl.ProfileCopy(); }
        private void btnDelete_Click(object sender, EventArgs e) { profCtrl.ProfileDelete(); }

        private void btnAttempts_Click(object sender, EventArgs e) { profCtrl.ProfileSetAttempts(); }
        private void btnUp_Click(object sender, EventArgs e) { profCtrl.ProfileSplitPermute(-1); }
        private void btnDown_Click(object sender, EventArgs e) { profCtrl.ProfileSplitPermute(+1); }
        private void BtnInsertSplit_Click(object sender, EventArgs e) { profCtrl.ProfileSplitInsert(); }

        private void BtnOnTop_Click(object sender, EventArgs e) { SetAlwaysOnTop(!this.TopMost); }

        private void btnReset_Click(object sender, EventArgs e) { profCtrl.ProfileReset(); }
        private void btnPB_Click(object sender, EventArgs e) { profCtrl.ProfilePB(); }
        private void btnHit_Click(object sender, EventArgs e) { profCtrl.ProfileHit(+1); }
        private void btnHitUndo_Click(object sender, EventArgs e) { profCtrl.ProfileHit(-1); }
        private void btnWayHit_Click(object sender, EventArgs e) { profCtrl.ProfileWayHit(+1); }
        private void btnWayHitUndo_Click(object sender, EventArgs e) { profCtrl.ProfileWayHit(-1); }
        private void btnSplit_Click(object sender, EventArgs e) { profCtrl.ProfileSplitGo(+1); }
        private void btnSplitPrev_Click(object sender, EventArgs e) { profCtrl.ProfileSplitGo(-1); }

        private void UpdateProgressAndTotals(object sender, EventArgs e)
        {
            int TotalSplits, TotalActiveSplit, TotalHits, TotalHitsWay, TotalPB;
            profCtrl.GetCalculatedSums(out TotalSplits, out TotalActiveSplit, out TotalHits, out TotalHitsWay, out TotalPB, false);
            lbl_progress.Text = "Progress:  " + TotalActiveSplit + " / " + TotalSplits + "  # " + profCtrl.CurrentAttempts.ToString("D3");
            lbl_totals.Text = "Total: " + (TotalHits + TotalHitsWay) + " Hits   " + TotalPB + " PB";
            
            // TODO: Move into ProfilesControl
            om.ShowSuccession = profCtrl.cbShowPredecessor.Checked;
            om.SuccessionTitle = profCtrl.txtPredecessorTitle.Text;

            om.Update();
        }

        #endregion
    }
}
