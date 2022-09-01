//MIT License

//Copyright (c) 2016-2022 Peter Kirmeier and Ezequiel Medina

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
using System.IO;
using System.Reflection;

namespace HitCounterManager
{
    public partial class Form1 : Form
    {
        private const int WM_HOTKEY = 0x312;

        public readonly Shortcuts sc;

        private bool SettingsDialogOpen = false;
        private bool ReadOnlyMode = false;

        private System.Windows.Forms.Timer _update_timer = new System.Windows.Forms.Timer() { Interval = 1000 };
        private bool _DllAttached { get; set; }
        private string DllPath = String.Empty;
        private Assembly assembly = null;
        private object obj = null;
        Type type = null;
        MethodInfo LoadAutoSplitterSettings = null;
        MethodInfo SaveAutoSplitterSettings = null;
        MethodInfo GetSplitterEnable = null;
        MethodInfo EnableSplitting = null;
        MethodInfo ReturnCurrentIGT = null;
        MethodInfo ResetSplitterFlags = null;
        MethodInfo SetGameIGT = null;
        MethodInfo AutoSplitterForm = null;
        MethodInfo CheckAutoTimerFlag = null;
        MethodInfo CheckGameTimerFlag = null;
        MethodInfo GetIgtSplitterTimer = null;
        MethodInfo CheckSplitterRunStarted = null;
        MethodInfo SetSplitterRunStarted = null;
        MethodInfo SetPointers = null;


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

            DllPath = @Path.GetFullPath("AutoSplitterCore.dll");
            if (!File.Exists(DllPath))
            {
                _DllAttached = false;
                comboBoxGame.Enabled = false;
            }
            else
            {
               _DllAttached = true;
                assembly = Assembly.LoadFile(DllPath);
                type = assembly.GetType("AutoSplitterCore.AutoSplitterMainModule");
                obj = Activator.CreateInstance(type);
                LoadAutoSplitterSettings = type.GetMethod("LoadAutoSplitterSettingsM");
                SaveAutoSplitterSettings = type.GetMethod("SaveAutoSplitterSettingsM");
                GetSplitterEnable = type.GetMethod("GetSplitterEnable");
                EnableSplitting = type.GetMethod("EnableSplitting");
                ReturnCurrentIGT = type.GetMethod("ReturnCurrentIGTM");
                ResetSplitterFlags = type.GetMethod("ResetSplitterFlags");
                SetGameIGT = type.GetMethod("SetGameIGT");
                AutoSplitterForm = type.GetMethod("AutoSplitterForm");
                CheckAutoTimerFlag = type.GetMethod("CheckAutoTimerFlag");
                CheckGameTimerFlag = type.GetMethod("CheckGameTimerFlag");
                GetIgtSplitterTimer = type.GetMethod("GetIgtSplitterTimer");
                CheckSplitterRunStarted = type.GetMethod("CheckSplitterRunStarted");
                SetSplitterRunStarted = type.GetMethod("SetSplitterRunStarted");
                SetPointers = type.GetMethod("SetPointers");


                SetPointers.Invoke(obj, null);
                LoadAutoSplitterSettings.Invoke(obj, new object [] { profCtrl });
                var index = GetSplitterEnable.Invoke(obj,null);
                switch (index)
                {
                    case 1: comboBoxGame.SelectedIndex = 1; break;
                    case 2: comboBoxGame.SelectedIndex = 2; break;
                    case 3: comboBoxGame.SelectedIndex = 3; break;
                    case 4: comboBoxGame.SelectedIndex = 4; break;
                    case 5: comboBoxGame.SelectedIndex = 5; break;
                    case 6: comboBoxGame.SelectedIndex = 6; break;
                    case 7: comboBoxGame.SelectedIndex = 7; break;
                    case 8: comboBoxGame.SelectedIndex = 8; break;
                    case 9: comboBoxGame.SelectedIndex = 9; break;
                    case 0:
                    default: comboBoxGame.SelectedIndex = 0; break;
                }

                profCtrl.SetIGTSource(ReturnCurrentIGT,obj);
                _update_timer.Tick += (senderT, args) => CheckAutoTimers();
                _update_timer.Enabled = true;
            }         
            this.UpdateDarkMode();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save this session?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SaveSettings();
                if (_DllAttached) { SaveAutoSplitterSettings.Invoke(obj, null); }
            }
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
                        case Shortcuts.SC_Type.SC_Type_TimerStart: StartStopTimer(true); break;
                        case Shortcuts.SC_Type.SC_Type_TimerStop: StartStopTimer(false); break;
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

        private void btnSave_Click(object sender, EventArgs e) { SaveSettings(); if (_DllAttached) { SaveAutoSplitterSettings.Invoke(obj, null); }; }
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

        private void btnNew_Click(object sender, EventArgs e) { profCtrl.ProfileNew(); }
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

        private void btnReset_Click(object sender, EventArgs e) 
        { StartStopTimer(false); 
          profCtrl.ProfileReset();
          if (_DllAttached) { ResetSplitterFlags.Invoke(obj, null); }
        }
        private void btnPB_Click(object sender, EventArgs e) { StartStopTimer(false); profCtrl.ProfilePB(); }
        private void btnPause_Click(object sender, EventArgs e) { StartStopTimer(!profCtrl.TimerRunning); }
        private void btnHit_Click(object sender, EventArgs e) { profCtrl.ProfileHit(+1); }
        private void btnHit_MouseDown(object sender, MouseEventArgs e) { if (e.Button == MouseButtons.Right) profCtrl.ProfileHit(-1); }
        private void btnHitUndo_Click(object sender, EventArgs e) { profCtrl.ProfileHit(-1); }
        private void btnWayHit_Click(object sender, EventArgs e) { profCtrl.ProfileWayHit(+1); }
        private void btnWayHit_MouseDown(object sender, MouseEventArgs e) { if (e.Button == MouseButtons.Right) profCtrl.ProfileWayHit(-1); }
        private void btnWayHitUndo_Click(object sender, EventArgs e) { profCtrl.ProfileWayHit(-1); }
        private void btnSplit_Click(object sender, EventArgs e) { profCtrl.ProfileSplitGo(+1); }
        private void btnSplit_MouseDown(object sender, MouseEventArgs e) { if (e.Button == MouseButtons.Right) profCtrl.ProfileSplitGo(-1); }
        private void btnSplitPrev_Click(object sender, EventArgs e) { profCtrl.ProfileSplitGo(-1); }

        private void btnSplitter_Click(object sender, EventArgs e)
        {
            if (_DllAttached)
            {
                AutoSplitterForm.Invoke(obj, new[] { (object)Program.DarkMode });
            }
            else
            {
                MessageBox.Show("Download and Install AutoSplitter Extencion", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
                

        private void ProfileChangedHandler(object sender, EventArgs e)
        {
            int TotalSplits, TotalActiveSplit, TotalHits, TotalHitsWay, TotalPB;
            long TotalTime;
            profCtrl.GetCalculatedSums(out TotalSplits, out TotalActiveSplit, out TotalHits, out TotalHitsWay, out TotalPB, out TotalTime, false);
            TotalTime /= 1000; // we only care about seconds

            lbl_progress.Text = "Progress:  " + TotalActiveSplit + " / " + TotalSplits + "  # " + profCtrl.CurrentAttempts.ToString("D3");
            lbl_time.Text = "Time: " + (TotalTime / 60 / 60).ToString("D2") + " : " + ((TotalTime / 60) % 60).ToString("D2") + " : " + (TotalTime % 60).ToString("D2");
            lbl_totals.Text = "Total: " + (TotalHits + TotalHitsWay) + " Hits   " + TotalPB + " PB";
            btnPause.Image = profCtrl.TimerRunning ? Sources.Resources.icons8_sleep_32 : Sources.Resources.icons8_time_32;
        }

        private void StartStopTimer(bool Start)
        {
            timer1.Enabled = profCtrl.TimerRunning = Start;
            btnPause.Image = Start ? Sources.Resources.icons8_sleep_32 : Sources.Resources.icons8_time_32;
        }

        private int gameActive = 0;
        private int _lastGameActive = 0;
        private long? _lastInGameTime;

        private void CheckAutoTimers()
        {
            bool anyGameTime = false;
            object g = 0;
            switch (gameActive)
            {
                case 1: //Sekiro
                    g = 1;
                    if ((bool)CheckAutoTimerFlag.Invoke(obj, new[] { g }))
                    {
                        if ((bool)CheckGameTimerFlag.Invoke(obj, new[] { g }))
                        {
                            if (!(bool)CheckSplitterRunStarted.Invoke(obj, new[] { g }) && (int)GetIgtSplitterTimer.Invoke(obj, new[] { g }) > 0)
                            {
                                StartStopTimer(true);
                                SetSplitterRunStarted.Invoke(obj, new object[] { g, true });
                            }
                            else
                            if ((bool)CheckSplitterRunStarted.Invoke(obj, new[] { g }) && (int)GetIgtSplitterTimer.Invoke(obj, new[] { g }) == 0)
                            {
                                StartStopTimer(false);
                                SetSplitterRunStarted.Invoke(obj, new object[] { g, false });
                            }
                        }
                        else
                        {
                            anyGameTime = true;
                        }
                    }
                    break;
                case 2: //DS1
                    g = 2;
                    if ((bool)CheckAutoTimerFlag.Invoke(obj, new[] { g }))
                    {
                        if ((bool)CheckGameTimerFlag.Invoke(obj, new[] { g }))
                        {
                            if (!(bool)CheckSplitterRunStarted.Invoke(obj, new[] { g }) && (int)GetIgtSplitterTimer.Invoke(obj, new[] { g }) > 0)
                            {
                                StartStopTimer(true);
                                SetSplitterRunStarted.Invoke(obj, new object[] { g, true });
                            }
                            else
                            if ((bool)CheckSplitterRunStarted.Invoke(obj, new[] { g }) && (int)GetIgtSplitterTimer.Invoke(obj, new[] { g }) == 0)
                            {
                                StartStopTimer(false);
                                SetSplitterRunStarted.Invoke(obj, new object[] { g, false });
                            }
                        }
                        else
                        {
                            anyGameTime = true;
                        }
                    }
                    break;
                case 4: //Ds3
                    g = 4;
                    if ((bool)CheckAutoTimerFlag.Invoke(obj, new[] { g }))
                    {
                        if ((bool)CheckGameTimerFlag.Invoke(obj, new[] { g }))
                        {
                            if (!(bool)CheckSplitterRunStarted.Invoke(obj, new[] { g }) && (int)GetIgtSplitterTimer.Invoke(obj, new[] { g }) > 0)
                            {
                                StartStopTimer(true);
                                SetSplitterRunStarted.Invoke(obj, new object[] { g, true });
                            }
                            else
                            if ((bool)CheckSplitterRunStarted.Invoke(obj, new[] { g }) && (int)GetIgtSplitterTimer.Invoke(obj, new[] { g }) == 0)
                            {
                                StartStopTimer(false);
                                SetSplitterRunStarted.Invoke(obj, new object[] { g, false });
                            }
                        }
                        else
                        {
                            anyGameTime = true;
                        }
                    }
                    break;
                case 5: //Elden
                    g = 5;
                    if ((bool)CheckAutoTimerFlag.Invoke(obj, new[] { g }))
                    {
                        if ((bool)CheckGameTimerFlag.Invoke(obj, new[] { g }))
                        {
                            if (!(bool)CheckSplitterRunStarted.Invoke(obj, new[] { g }) && (int)GetIgtSplitterTimer.Invoke(obj, new[] { g }) > 0)
                            {
                                StartStopTimer(true);
                                SetSplitterRunStarted.Invoke(obj, new object[] { g, true });
                            }
                            else
                            if ((bool)CheckSplitterRunStarted.Invoke(obj, new[] { g }) && (int)GetIgtSplitterTimer.Invoke(obj, new[] { g }) == 0)
                            {
                                StartStopTimer(false);
                                SetSplitterRunStarted.Invoke(obj, new object[] { g, false });
                            }
                        }
                        else
                        {
                            anyGameTime = true;
                        }
                    }
                    break;
                case 7: //Celeste
                    g = 7;
                    if ((bool)CheckAutoTimerFlag.Invoke(obj, new[] { g }))
                    {
                        if ((bool)CheckGameTimerFlag.Invoke(obj, new[] { g }))
                        {
                            if (!(bool)CheckSplitterRunStarted.Invoke(obj, new[] { g }) && (int)GetIgtSplitterTimer.Invoke(obj, new[] { g }) > 0)
                            {
                                StartStopTimer(true);
                                SetSplitterRunStarted.Invoke(obj, new object[] { g, true });
                            }
                            else
                            if ((bool)CheckSplitterRunStarted.Invoke(obj, new[] { g }) && (int)GetIgtSplitterTimer.Invoke(obj, new[] { g }) == 0)
                            {
                                StartStopTimer(false);
                                SetSplitterRunStarted.Invoke(obj, new object[] { g, false });
                            }
                        }
                        else
                        {
                            anyGameTime = true;
                        }
                    }
                    break;
                case 8: //Cuphead
                    g = 8;
                    if ((bool)CheckAutoTimerFlag.Invoke(obj, new[] { g }))
                    {
                        if ((bool)CheckGameTimerFlag.Invoke(obj, new[] { g }))
                        {
                            if (!(bool)CheckSplitterRunStarted.Invoke(obj, new[] { g }) && (int)GetIgtSplitterTimer.Invoke(obj, new[] { g }) > 0)
                            {
                                StartStopTimer(true);
                                SetSplitterRunStarted.Invoke(obj, new object[] { g, true });
                            }
                            else
                            if ((bool)CheckSplitterRunStarted.Invoke(obj, new[] { g }) && (int)GetIgtSplitterTimer.Invoke(obj, new[] { g }) == 0)
                            {
                                StartStopTimer(false);
                                SetSplitterRunStarted.Invoke(obj, new object[] { g, false });
                            }
                        }
                        else
                        {
                            anyGameTime = true;
                        }
                    }
                    break;

                case 3: //DS2
                    g = 3;
                    if ((bool)CheckAutoTimerFlag.Invoke(obj, new[] { g }))
                    {
                        if ((bool)CheckSplitterRunStarted.Invoke(obj, new[] { g }))
                        {
                            StartStopTimer(true);
                        }
                        else
                        {
                            if (!(bool)CheckSplitterRunStarted.Invoke(obj, new[] { g }))
                            {
                                StartStopTimer(false);
                            }
                        }
                    }
                    break;
                case 6: //Hollow
                    g = 6;
                    if ((bool)CheckAutoTimerFlag.Invoke(obj, new[] { g }))
                    {
                        if ((bool)CheckSplitterRunStarted.Invoke(obj, new[] { g }))
                        {
                            StartStopTimer(true);
                        }
                        else
                        {
                            if (!(bool)CheckSplitterRunStarted.Invoke(obj, new[] { g }))
                            {
                                StartStopTimer(false);
                            }
                        }
                    }
                    break;


                case 0:
                case 9:
                default: break;
            }

            if (_lastGameActive != gameActive)
            {
                SetGameIGT.Invoke(obj, new object[] { gameActive });
                _lastGameActive = gameActive;
            }

            if (anyGameTime)
            {
                var inGameTime = (int)ReturnCurrentIGT.Invoke(obj, null);
                if (inGameTime > 0 && !profCtrl.TimerRunning)
                    StartStopTimer(true);
                else if (inGameTime == 0 && profCtrl.TimerRunning)
                    StartStopTimer(false);
                else if (inGameTime > 0 && _lastInGameTime == inGameTime && profCtrl.TimerRunning)
                    StartStopTimer(false);
                else if (inGameTime > 0 && _lastInGameTime != inGameTime && !profCtrl.TimerRunning)
                    StartStopTimer(true);
                if (inGameTime > 0)
                    _lastInGameTime = inGameTime;
            }

        }

        private void comboBoxGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Disable all games
            EnableSplitting.Invoke(obj, new object[] { 0 });
            gameActive = 0;

            //Ask Selected index
            if (comboBoxGame.SelectedIndex == 1)
            {
                EnableSplitting.Invoke(obj, new object[] { 1 });
                gameActive = 1;
            }
            if (comboBoxGame.SelectedIndex == 2)
            {
                EnableSplitting.Invoke(obj, new object[] { 2 });
                gameActive = 2;
            }
            if (comboBoxGame.SelectedIndex == 3)
            {
                EnableSplitting.Invoke(obj, new object[] { 3 });
                gameActive = 3;
            }
            if (comboBoxGame.SelectedIndex == 4)
            {
                EnableSplitting.Invoke(obj, new object[] { 4 });
                gameActive = 4;
            }
            if (comboBoxGame.SelectedIndex == 5)
            {
                EnableSplitting.Invoke(obj, new object[] { 5 });
                gameActive = 5;
            }
            if (comboBoxGame.SelectedIndex == 6)
            {
                EnableSplitting.Invoke(obj, new object[] { 6 });
                gameActive = 6;
            }        
            if(comboBoxGame.SelectedIndex == 7)
            {
                EnableSplitting.Invoke(obj, new object[] { 7 });
                gameActive = 7;
            }
            if (comboBoxGame.SelectedIndex == 8)
            {
                EnableSplitting.Invoke(obj, new object[] { 8 });
                gameActive = 8;
            }
            if (comboBoxGame.SelectedIndex == 9)
            {
                EnableSplitting.Invoke(obj, new object[] { 9 });
                gameActive = 9;
            }            
        }

        #endregion
    }
}
