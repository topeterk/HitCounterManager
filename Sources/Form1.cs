//MIT License

//Copyright (c) 2016-2020 Peter Kirmeier

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

        private bool SettingsDialogOpen = false;
        private bool ReadOnlyMode = false;

        #region Form

        public Form1()
        {
            InitializeComponent();

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
            UpdateProgressAndTotals(sender, e);
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
        #region Functions

        private void SetAlwaysOnTop(bool AlwaysOnTop)
        {
            if (this.TopMost = AlwaysOnTop)
                btnOnTop.BackColor = Color.LimeGreen;
            else
                btnOnTop.BackColor = Color.FromKnownColor(Program.DarkMode ? KnownColor.ControlDark : KnownColor.Control);
        }

        private void SetReadOnlyMode(bool IsReadOnly)
        {
            Color color;
            if (ReadOnlyMode = IsReadOnly)
            {
                btnLock.BackColor = Color.Salmon;
                color = Color.IndianRed;
            }
            else
            {
                color = Color.FromKnownColor(Program.DarkMode ? KnownColor.ControlDark : KnownColor.Control);
                btnLock.BackColor = color;
            }

            foreach (Button button in new Button[] { btnUp, btnDown, btnInsertSplit, btnNew, btnRename, btnCopy, btnDelete })
            {
                button.Enabled = !IsReadOnly;
                button.BackColor = color;
            }

            profCtrl.ReadOnlyMode = ReadOnlyMode;
        }

        /// <summary>
        /// Creates a window to show changelog of new available versions
        /// </summary>
        /// <param name="Prompt">Prompt text</param>
        /// <param name="Title">Title</param>
        /// <param name="Text">Changelog</param>
        public void NewVersionDialog(string LatestVersionTitle, string Changelog)
        {
            const int ClientPad = 15;
            Form frm = new Form();
            
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.FormBorderStyle = FormBorderStyle.FixedDialog;
            frm.Icon = Icon;
            frm.ShowInTaskbar = false;
            frm.FormBorderStyle = FormBorderStyle.Sizable;
            frm.MaximizeBox = true;
            frm.MinimizeBox = false;
            frm.ClientSize = new Size(600, 400);
            frm.MinimumSize = frm.ClientSize;
            frm.Text = "New version available!";

            Label label = new Label();
            label.Size = new Size(frm.ClientSize.Width - ClientPad, 20);
            label.Location = new Point(ClientPad, ClientPad);
            label.Text = "Latest available version:      " + LatestVersionTitle;
            frm.Controls.Add(label);

            Button okButton = new Button();
            okButton.DialogResult = DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Location = new Point(frm.ClientSize.Width - okButton.Size.Width - ClientPad, frm.ClientSize.Height - okButton.Size.Height - ClientPad);
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.Text = "&OK";
            frm.Controls.Add(okButton);

            TextBox textBox = new TextBox();
            textBox.Location = new Point(ClientPad, label.Location.Y + label.Size.Height + 5);
            textBox.Size = new Size(frm.ClientSize.Width - ClientPad*2, okButton.Location.Y - ClientPad - textBox.Location.Y);
            textBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox.Multiline = true;
            textBox.Text = Changelog.Replace("\n", Environment.NewLine);
            textBox.ReadOnly = true;
            textBox.ScrollBars = ScrollBars.Both;
            textBox.BackColor = Color.FromKnownColor(KnownColor.Window);
            frm.Controls.Add(textBox);

            frm.AcceptButton = okButton;
            frm.ShowDialog(this);
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
        private void btnTeamHitless_Click(object sender, EventArgs e) { System.Diagnostics.Process.Start("https://discord.gg/4E7cSK7"); }
        private void btnCheckVersion_Click(object sender, EventArgs e)
        {
            try
            {
                WebClient client = new WebClient();
                client.Encoding = System.Text.Encoding.UTF8;

                // https://developer.github.com/v3/#user-agent-required
                client.Headers.Add("User-Agent", "HitCounterManager/" + Application.ProductVersion);
                // https://developer.github.com/v3/media/#request-specific-version
                client.Headers.Add("Accept", "application/vnd.github.v3.text+json");
                // https://developer.github.com/v3/repos/releases/#get-a-single-release
                string response = client.DownloadString("http://api.github.com/repos/topeterk/HitCounterManager/releases");

                List<Dictionary<string, object>> json = response.FromJson<List<Dictionary<string, object>>>();
                Dictionary<string, object> release = json[0]; // latest

                if (release["tag_name"].ToString() == Application.ProductVersion.ToString())
                {
                    MessageBox.Show("You are using the latest version!", "Up to date!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string changelog = "";
                    int i;
                    for (i = 0; i < json.Count; i++)
                    {
                        release = json[i];
                        if (release["tag_name"].ToString() == Application.ProductVersion.ToString()) break;

                        changelog += "----------------------------------------------------------------------------------------------------------------------------------------------------------------"
                            + Environment.NewLine
                            + release["name"].ToString()
                            + Environment.NewLine + Environment.NewLine
                            + release["body_text"].ToString().Replace("\n\n", Environment.NewLine)
                            + Environment.NewLine + Environment.NewLine;
                    }

                    changelog = "New version available!" + Environment.NewLine + Environment.NewLine
                            + "There are " + i + " new version available:" + Environment.NewLine
                            + "Please visit the github project website (WWW button on main window)." + Environment.NewLine
                            + "Then look at the \"releases\" to download the new version." + Environment.NewLine
                            + Environment.NewLine + changelog;

                    NewVersionDialog(json[0]["name"].ToString(), changelog);
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
        private void BtnSplitLock_Click(object sender, EventArgs e) { SetReadOnlyMode(!profCtrl.ReadOnlyMode); }
        private void btnDarkMode_Click(object sender, EventArgs e) { Program.DarkMode = !Program.DarkMode; this.UpdateDarkMode(); }

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
        }

        #endregion
    }
}
