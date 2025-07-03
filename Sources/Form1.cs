﻿//MIT License

//Copyright (c) 2016-2025 Peter Kirmeier and Ezequiel Medina

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
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace HitCounterManager
{
    public partial class Form1 : Form
    {
        private const int WM_HOTKEY = 0x312;

        #region AutoSplitter
        private AutoSplitterCoreInterface InterfaceASC;
        #endregion

        #region ToolBar
        private ContextMenuStrip trayMenu;

        private void ShowMainForm()
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void CloseHCM()
        {
            notifyIconToolBar.Visible = false;
            Application.Exit();
        }

        #endregion

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
            ProfileChangedHandler(sender, e);

            #region AutoSplitter

            AutoSplitterCoreModule.AutoSplitterCoreTryLoadModule();

            if (!AutoSplitterCoreModule.AutoSplitterCoreLoaded)
            {
                int Offset = btnAutoSplitter.Left - btnSplit.Left;
                comboBoxGame.Hide();
                GameToSplitLabel.Hide();
                btnAutoSplitter.Hide();
                pnlHidePracticeModeCheckWhenWindowTooShort.Hide();
                PracticeModeCheck.Hide();

                btnHit.Width += Offset;
                btnSplit.Left += Offset;
                btnWayHit.Left += Offset;
                Offset = profCtrl.Top - GameToSplitLabel.Top;
                profCtrl.Top -= Offset;
                profCtrl.Height += Offset;
            }
            else
            {
                profCtrl.InterfaceASC = InterfaceASC = new(this);
                AutoSplitterCoreModule.AutoSplitterRegisterInterface(InterfaceASC);
                LoadAutoSplitterHotKeys();
            }

            #endregion

            #region ToolBar
            if (_settings.TrayIconEnable)
            {
                trayMenu = new ContextMenuStrip();

                // Split
                trayMenu.Items.Add("Next split", null, (s, e) => btnSplit_Click(null, null));
                trayMenu.Items.Add("Previous split", null, (s, e) => btnSplitPrev_Click(null, null));

                // Hit
                trayMenu.Items.Add(new ToolStripSeparator());
                trayMenu.Items.Add("Hit increase (way)", null, (s, e) => btnWayHit_Click(null, null));
                trayMenu.Items.Add("Hit decrease (way)", null, (s, e) => btnWayHitUndo_Click(null, null));
                trayMenu.Items.Add("Hit increase (boss)", null, (s, e) => btnHit_Click(null, null));
                trayMenu.Items.Add("Hit decrease (boss)", null, (s, e) => btnHitUndo_Click(null, null));

                // Timer / Run Reset
                trayMenu.Items.Add(new ToolStripSeparator());
                var runMenu = new ToolStripMenuItem("Run");
                runMenu.DropDownItems.Add("Timer start", null, (s, e) => StartStopTimer(true));
                runMenu.DropDownItems.Add("Timer stop", null, (s, e) => StartStopTimer(false));
                runMenu.DropDownItems.Add("Reset run", null, (s, e) => btnReset_Click(null, null));
                trayMenu.Items.Add(runMenu);

                // Open / Close
                trayMenu.Items.Add("Open", null, (s, e) => ShowMainForm());
                trayMenu.Items.Add(new ToolStripSeparator()); // Spacing to not close the application by accident
                trayMenu.Items.Add("Close", null, (s, e) => CloseHCM());

                // NotifyIcon
                notifyIconToolBar.ContextMenuStrip = trayMenu;
                notifyIconToolBar.DoubleClick += (s, e) => ShowMainForm();
                notifyIconToolBar.Visible = true;
            }
            #endregion

            this.UpdateDarkMode();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (_settings.TrayIconEnable && _settings.TrayIconMinimize && (WindowState == FormWindowState.Minimized))
            {
                Hide();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save this session?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SaveSettings();
                InterfaceASC?.SaveSettings();
            }
            else if (result == DialogResult.Cancel) e.Cancel = true;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                if (!SettingsDialogOpen)
                {
                    switch ((SC_Type)m.WParam)
                    {
                        case SC_Type.SC_Type_Reset: btnReset_Click(null, null); break;
                        case SC_Type.SC_Type_Hit: btnHit_Click(null, null); break;
                        case SC_Type.SC_Type_Split: btnSplit_Click(null, null); break;
                        case SC_Type.SC_Type_HitUndo: btnHitUndo_Click(null, null); break;
                        case SC_Type.SC_Type_SplitPrev: btnSplitPrev_Click(null, null); break;
                        case SC_Type.SC_Type_WayHit: btnWayHit_Click(null, null); break;
                        case SC_Type.SC_Type_WayHitUndo: btnWayHitUndo_Click(null, null); break;
                        case SC_Type.SC_Type_PB: btnPB_Click(null, null); break;
                        case SC_Type.SC_Type_TimerStart: StartStopTimer(true); break;
                        case SC_Type.SC_Type_TimerStop: StartStopTimer(false); break;
                        case SC_Type.SC_Type_HitBossPrev: HitIncreasePrev(); break;
                        case SC_Type.SC_Type_BossHitUndoPrev: HitDecreasePrev(); break;
                        case SC_Type.SC_Type_HitWayPrev: HitWayIncreasePrev(); break;
                        case SC_Type.SC_Type_WayHitUndoPrev: HitWayDecreasePrev(); break;
                        #region AutoSplitter
                        case SC_Type.SC_Type_Practice: if (AutoSplitterCoreModule.AutoSplitterCoreLoaded) { TogglePracticeMode(); } break;
                        #endregion
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

            foreach (Button button in new Button[] { btnUp, btnDown, btnInsertSplit, btnDeleteSplit, btnNew, btnRename, btnCopy, btnDelete })
            {
                button.Enabled = !IsReadOnly;
                button.BackColor = color;
            }

            profCtrl.ReadOnlyMode = ReadOnlyMode;
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

        private void btnSave_Click(object sender, EventArgs e) { SaveSettings(); InterfaceASC?.SaveSettings(); }
        private void btnWeb_Click(object sender, EventArgs e) { GitHubUpdate.WebOpenLandingPage(); }
        private void btnTeamHitless_Click(object sender, EventArgs e) { System.Diagnostics.Process.Start("https://discord.gg/4E7cSK7"); }
        private void btnTeamHitlessHispano_Click(object sender, EventArgs e) { System.Diagnostics.Process.Start("https://discord.gg/ntygnch"); }
        private void btnCheckVersion_Click(object sender, EventArgs e)
        {
            if (!GitHubUpdate.QueryAllReleases())
            {
                MessageBox.Show("An error occurred during update check!", "Check for update failed!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (GitHubUpdate.NewVersionDialog(this) == DialogResult.Yes) GitHubUpdate.WebOpenLatestRelease();
        }
        private void btnAbout_Click(object sender, EventArgs e) { new About().ShowDialog(this); }

        private void btnNew_Click(object sender, EventArgs e)
        {
            string NameNew = VisualBasic.Interaction.InputBox("Enter name of new profile", "New profile", profCtrl.SelectedProfile);
            if (NameNew.Length == 0) return;

            profCtrl.ProfileNew(NameNew);
        }
        private void btnRename_Click(object sender, EventArgs e) { profCtrl.ProfileRename(); }
        private void btnCopy_Click(object sender, EventArgs e) { profCtrl.ProfileCopy(); }
        private void btnDelete_Click(object sender, EventArgs e) { profCtrl.ProfileDelete(); }

        private void btnAttempts_Click(object sender, EventArgs e) { profCtrl.ProfileSetAttempts(); }
        private void btnUp_Click(object sender, EventArgs e) { profCtrl.ProfileSplitPermute(-1); }
        private void btnDown_Click(object sender, EventArgs e) { profCtrl.ProfileSplitPermute(+1); }
        private void BtnInsertSplit_Click(object sender, EventArgs e) { profCtrl.ProfileSplitInsert(); }
        private void btnDeleteSplit_Click(object sender, EventArgs e) { profCtrl.ProfileSplitDelete(); }

        private void BtnOnTop_Click(object sender, EventArgs e) { SetAlwaysOnTop(!this.TopMost); }
        private void BtnSplitLock_Click(object sender, EventArgs e) { SetReadOnlyMode(!profCtrl.ReadOnlyMode); }
        private void btnDarkMode_Click(object sender, EventArgs e) { Program.DarkMode = !Program.DarkMode; this.UpdateDarkMode(); }

        private void btnReset_Click(object sender, EventArgs e) { StartStopTimer(false); profCtrl.ProfileReset(); InterfaceASC?.SplitterReset(); }
        private void btnPB_Click(object sender, EventArgs e) { StartStopTimer(false); profCtrl.ProfilePB(); }
        private void btnPause_Click(object sender, EventArgs e) { StartStopTimer(!profCtrl.TimerRunning); }
        private void btnHit_Click(object sender, EventArgs e) { profCtrl.ProfileHit(+1); }
        private void btnHit_MouseDown(object sender, MouseEventArgs e) { if (e.Button == MouseButtons.Right) profCtrl.ProfileHit(-1); }
        private void btnHitUndo_Click(object sender, EventArgs e) { profCtrl.ProfileHit(-1); }
        private void btnWayHit_Click(object sender, EventArgs e) { profCtrl.ProfileWayHit(+1); }
        private void btnWayHit_MouseDown(object sender, MouseEventArgs e) { if (e.Button == MouseButtons.Right) profCtrl.ProfileWayHit(-1); }
        private void btnWayHitUndo_Click(object sender, EventArgs e) { profCtrl.ProfileWayHit(-1); }
        private void HitIncreasePrev() { profCtrl.ProfileSplitGo(-1); profCtrl.ProfileHit(+1); profCtrl.ProfileSplitGo(+1); } // TODO: Optimize out back and forth
        private void HitDecreasePrev() { profCtrl.ProfileSplitGo(-1); profCtrl.ProfileHit(-1); profCtrl.ProfileSplitGo(+1); } // TODO: Optimize out back and forth
        private void HitWayIncreasePrev() { profCtrl.ProfileSplitGo(-1); profCtrl.ProfileWayHit(+1); profCtrl.ProfileSplitGo(+1); } // TODO: Optimize out back and forth
        private void HitWayDecreasePrev() { profCtrl.ProfileSplitGo(-1); profCtrl.ProfileWayHit(-1); profCtrl.ProfileSplitGo(+1); } // TODO: Optimize out back and forth
        private void btnSplit_Click(object sender, EventArgs e) { profCtrl.ProfileSplitGo(+1); }
        private void btnSplit_MouseDown(object sender, MouseEventArgs e) { if (e.Button == MouseButtons.Right) profCtrl.ProfileSplitGo(-1); }
        private void btnSplitPrev_Click(object sender, EventArgs e) { profCtrl.ProfileSplitGo(-1); }

        private void ProfileChangedHandler(object sender, EventArgs e)
        {
            profCtrl.GetCalculatedSums(out int TotalSplits, out int TotalActiveSplit, out int TotalHits, out int TotalHitsWay, out int TotalPB, out long TotalTime, false);
            TotalTime /= 1000; // we only care about seconds

            lbl_progress.Text = "Progress:  " + TotalActiveSplit + " / " + TotalSplits + "  # " + profCtrl.CurrentAttempts.ToString("D3");
            lbl_time.Text = "Time: " + (TotalTime/60/60).ToString("D2") + " : " + ((TotalTime/60) % 60).ToString("D2") + " : " + (TotalTime % 60).ToString("D2");
            lbl_totals.Text = "Total: " + (TotalHits + TotalHitsWay) + " Hits   " + TotalPB + " PB";
            btnPause.Image = profCtrl.TimerRunning ? Sources.Resources.icons8_sleep_32 : Sources.Resources.icons8_time_32;
        }

        /// <summary>
        /// As also used by AutoSplitter, do not change prototype!
        /// </summary>
        public void StartStopTimer(bool Start)
        {
            timer1.Enabled = profCtrl.TimerRunning = Start;
            btnPause.Image = Start ? Sources.Resources.icons8_sleep_32 : Sources.Resources.icons8_time_32;
        }

        #endregion
        #region AutoSplitter
        //Internal AutoSplitter Without Livesplit program by Neimex23
        private void btnAutoSplitter_Click(object sender, EventArgs e)
        {
            InterfaceASC?.OpenSettings();
        }

        private void TogglePracticeMode()
        {
            PracticeModeCheck.Checked = !PracticeModeCheck.Checked;
            InterfaceASC?.SetPracticeMode(PracticeModeCheck.Checked);
        }

        private void PracticeModeCheck_CheckedChanged(object sender, EventArgs e)
        {
            InterfaceASC?.SetPracticeMode(PracticeModeCheck.Checked);
        }

        private void comboBoxGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            InterfaceASC?.SetActiveGameIndex(comboBoxGame.SelectedIndex);
        }
        #endregion

    }
}
