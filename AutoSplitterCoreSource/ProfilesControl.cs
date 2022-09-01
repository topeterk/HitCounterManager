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
    public partial class ProfilesControl : UserControl
    {
        private readonly int gpSuccession_Height;
        private Profiles profs;
        private Succession succession;
        public readonly OutModule om;
        private bool Ready = false;

        public ProfilesControl()
        {
            InitializeComponent();

            // Workaround: Should be done by designer but...
            //   - event is not showing up in the event list
            //   - when added by hand: changing anything in the designer a non-compileable method gets created (unspecified Tuple):
            //        public void ProfileTabPermuting(object sender, Tuple e)
            // Solution here: add event handler after the designer's code manually
            ptc.ProfileTabPermuting += ProfileTabPermuting;

            om = new OutModule(this);

            gpSuccession_Height = gpSuccession.Height; // remember expanded size from designer settings
            ShowSuccessionMenu(false); // start collapsed

            ptc.InitializeProfileTabControl();
        }

        [Browsable(false)] // Hide from designer
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Hide from designer generator
        public ProfileTabControl ProfileTabControl { get { return ptc; } }

        #region Timer related
        
        private DateTime last_update_time = DateTime.UtcNow;

        private bool _TimerRunning = false;
        [Browsable(false)] // Hide from designer
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Hide from designer generator
        public bool TimerRunning
        {
            get { return _TimerRunning; }
            set
            {
                if (value != _TimerRunning)
                {
                    last_update_time = DateTime.UtcNow;
                    timer1.Enabled = _TimerRunning = value;
                    UpdateDuration();
                    om.Update();
                }
            }
        }

        [Browsable(false)] // Hide from designer
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Hide from designer generator
        public void UpdateDurationIfTimerIsRunning()
        {
            if (_TimerRunning)
            {
                UpdateDuration();
            }
        }

        private int _updateCounter;
        private void UpdateDuration()
        {
            var inGameTime = IGTSource?.ReturnCurrentIGT() ?? -1;
            if (inGameTime != -1) // we have a valid time
            {
                if (inGameTime > 0) // only use the time if we're in the game
                {
                    SelectedProfileInfo.SetDuration(inGameTime, !TimerRunning || _updateCounter++%30==0);
                }
            }
            else 
            {
                DateTime utc_now = DateTime.UtcNow;
                SelectedProfileInfo.AddDuration((long)(utc_now - last_update_time).TotalMilliseconds);
                last_update_time = utc_now;
            }
        }

        private void timer1_Tick(object sender, EventArgs e) { UpdateDurationIfTimerIsRunning(); }

        #endregion

        #region Succession related

        private void btnSuccessionVisibility_Click(object sender, EventArgs e) { ShowSuccessionMenu();  }

        /// <summary>
        /// Collapses or expands succession menu
        /// </summary>
        /// <param name="expand">TRUE = Expand, FALSE = Collapse, NULL = Toggle</param>
        public void ShowSuccessionMenu(Nullable<bool> expand = null)
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
            ptc.Height -= diff;
            gpSuccession.Top -= diff;
        }

        private void AddTabToolStripMenuItem_Click(object sender, EventArgs e) { UpdateDurationIfTimerIsRunning(); ptc.ProfileTabCreateAndSelect(); }
        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e) { UpdateDurationIfTimerIsRunning(); ptc.ProfileTabRemove(ptc.SelectedTab); }

        private void Menu_ptc_Opening(object sender, CancelEventArgs e)
        {
            foreach (ToolStripItem item in (sender as ContextMenuStrip).Items)
            {
                if (item.Text.Contains("Remove"))
                {
                    item.Text = "Remove active tab: " + ptc.SelectedTab.Text;
                }
            }
        }

        #endregion

        #region Profile related

        private ProfileViewControl SelectedProfileViewControl { get { return ptc.SelectedProfileViewControl; } }
        public IProfileInfo SelectedProfileInfo { get { return SelectedProfileViewControl.ProfileInfo; } }
        public string SelectedProfile { get { return SelectedProfileViewControl.SelectedProfile; } }

        [Browsable(false)] // Hide from designer
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Hide from designer generator
        public int CurrentAttempts
        {
            get { return (ptc.SuccessionActive ? succession.Attempts : SelectedProfileInfo.AttemptsCount); }
            set
            {
                if (ptc.SuccessionActive)
                {
                    succession.Attempts = value;
                    ProfileChangedHandler(this, null); // Notify about change as there is no profile which will do this for us
                }
                else
                    SelectedProfileInfo.AttemptsCount = value;
            }
        }

        [Browsable(false)] // Hide from designer
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Hide from designer generator
        public bool ReadOnlyMode
        {
            get { return ptc.ReadOnlyMode; }
            set { ptc.ReadOnlyMode = value; }
        }
        
        public IGTModule IGTSource { get; set; }

        public void InitializeProfilesControl(Profiles profiles, Succession Succession)
        {
            profs = profiles;
            succession = Succession;

            // As the designer is used, we have at least one tab already existing, so only further tabs must be created
            for (int i = 1; i < succession.SuccessionList.Count; i++)
                ptc.ProfileTabCreateAndSelect();

            // Load profile lists into all tabs and select previous selected profiles
            for (int i = 0; i < succession.SuccessionList.Count; i++)
                ptc.ProfileViewControls[i].SetProfileList(profs.GetProfileList(), succession.SuccessionList[i].ProfileSelected);

            // Initialize succession settings
            if (null != succession.HistorySplitTitle) txtPredecessorTitle.Text = succession.HistorySplitTitle;
            cbShowPredecessor.Checked = succession.HistorySplitVisible;

            // Select the last user selected tab
            ptc.SelectTab(succession.ActiveIndex);
            ptc.InitDone = true;

            Ready = true;
        }

        public event EventHandler<EventArgs> ProfileChanged;
        public void ProfileChangedHandler(object sender, EventArgs e)
        {
            if (!Ready) return;

            if (e is ProfileChangedEventArgs)
            {
                ProfileChangedEventArgs eventArgs = (ProfileChangedEventArgs)e;
                if (eventArgs.RunCompleted && _TimerRunning)
                {
                    timer1.Enabled = _TimerRunning = false;
                    UpdateDuration(); // we want to update once at the end of the run (although the timer's stopped already)
                }
            }

            UpdateDurationIfTimerIsRunning();

            succession.HistorySplitVisible = cbShowPredecessor.Checked;
            succession.HistorySplitTitle = txtPredecessorTitle.Text;

            if (null != ProfileChanged) ProfileChanged(sender, e); // Fire event

            om.Update();
        }

        private void SelectedProfileChanged(object sender, ProfileViewControl.SelectedProfileChangedCauseType cause)
        {
            UpdateDurationIfTimerIsRunning();

            ProfileViewControl pvc_sender = (ProfileViewControl)sender;
            if (cause != ProfileViewControl.SelectedProfileChangedCauseType.Delete)
            {
                profs.SaveProfile(pvc_sender.ProfileInfo); // save currently selected profile
            }
            profs.LoadProfile(pvc_sender.SelectedProfile, pvc_sender.ProfileInfo);
            succession.SuccessionList[ptc.IndexOf(pvc_sender)].ProfileSelected = pvc_sender.SelectedProfile;
        }
  
        public void ProfileTabPermuting(object sender, Tuple<int, int> indices)
        {
            SuccessionEntry tmp = succession.SuccessionList[indices.Item1];
            succession.SuccessionList[indices.Item1] = succession.SuccessionList[indices.Item2];
            succession.SuccessionList[indices.Item2] = tmp;
        }

        private void ProfileTabSelect(object sender, ProfileTabControl.ProfileTabSelectAction action)
        {
            if (!Ready) return;

            ProfileViewControl pvc_sender = (ProfileViewControl)sender;
            switch (action)
            {
                case ProfileTabControl.ProfileTabSelectAction.Selecting:
                    UpdateDurationIfTimerIsRunning();
                    profs.SaveProfile(SelectedProfileInfo); // save current tab's profile
                    break;
                case ProfileTabControl.ProfileTabSelectAction.Created:
                    pvc_sender.SetProfileList(profs.GetProfileList(), null);
                    succession.SuccessionList.Add(new SuccessionEntry()); // Adds new succession entry without profile selection
                    succession.ActiveIndex = ptc.IndexOf(pvc_sender);
                    break;
                case ProfileTabControl.ProfileTabSelectAction.Selected:
                    profs.LoadProfile(pvc_sender.ProfileInfo.ProfileName, pvc_sender.ProfileInfo);
                    succession.ActiveIndex = ptc.IndexOf(pvc_sender);
                    break;
                case ProfileTabControl.ProfileTabSelectAction.Deleting:
                    UpdateDurationIfTimerIsRunning();
                    succession.SuccessionList.RemoveAt(ptc.IndexOf(pvc_sender));
                    break;
                default: break;
            }
        }

        public void ProfileNew()
        {
            string NameNew = VisualBasic.Interaction.InputBox("Enter name of new profile", "New profile", SelectedProfile);
            if (NameNew.Length == 0) return;

            if (profs.HasProfile(NameNew))
            {
                MessageBox.Show("A profile with this name already exists!", "Profile already exists", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            UpdateDurationIfTimerIsRunning();
            profs.SaveProfile(SelectedProfileViewControl.ProfileInfo); // save previous selected profile

            // Apply on all tabs
            foreach (ProfileViewControl pvc_tab in ptc.ProfileViewControls)
            {
                pvc_tab.CreateNewProfile(NameNew, (pvc_tab == SelectedProfileViewControl), false); // Select only for the current tab
            }
        }
        public void ProfileRename()
        {
            string NameOld = SelectedProfile;
            if (null == NameOld) return;

            string NameNew = VisualBasic.Interaction.InputBox("Enter new name for profile \"" + NameOld + "\"!", "Rename profile", NameOld);
            if (NameNew.Length == 0) return;

            if (profs.HasProfile(NameNew))
            {
                MessageBox.Show("A profile with this name already exists!", "Profile already exists", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            UpdateDurationIfTimerIsRunning();
            profs.RenameProfile(NameOld, NameNew);

            // Apply on all tabs
            foreach (ProfileViewControl pvc_tab in ptc.ProfileViewControls)
            {
                pvc_tab.RenameProfile(NameOld, NameNew);
            }
        }
        public void ProfileCopy()
        {
            UpdateDurationIfTimerIsRunning();
            profs.SaveProfile(SelectedProfileViewControl.ProfileInfo); // save previous selected profile

            string NameNew = SelectedProfile;
            do { NameNew += " COPY"; } while (profs.HasProfile(NameNew)); // extend name till it becomes unique

            // Apply on all tabs
            foreach (ProfileViewControl pvc_tab in ptc.ProfileViewControls)
            {
                pvc_tab.CreateNewProfile(NameNew, (pvc_tab == SelectedProfileViewControl), true); // Select only for the current tab
            }
        }
        public void ProfileDelete()
        {
            string NameDel = SelectedProfile;
            if (DialogResult.OK == MessageBox.Show("Do you really want to delete profile \"" + NameDel + "\"?", "Deleting profile", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                UpdateDurationIfTimerIsRunning();
                profs.DeleteProfile(NameDel);

                // Apply on all tabs
                foreach (ProfileViewControl pvc_tab in ptc.ProfileViewControls)
                {
                    if (pvc_tab == SelectedProfileViewControl)
                        SelectedProfileViewControl.DeleteSelectedProfile(); // Apply on foreground tab: Remove profile and select next one (if any)
                    else
                        pvc_tab.DeleteProfile(NameDel); // background tab: Remove profile and if was selected, unselect
                }

                // profile was changed by deletion, so we load the newly selected profile
                profs.LoadProfile(SelectedProfile, SelectedProfileInfo);
            }
        }

        public void ProfileSplitPermute(int Amount) { UpdateDurationIfTimerIsRunning(); SelectedProfileInfo.PermuteSplit(SelectedProfileInfo.ActiveSplit, Amount); }
        public void ProfileSplitInsert() { UpdateDurationIfTimerIsRunning(); SelectedProfileInfo.InsertSplit(); }
        public void ProfileSplitDelete() { UpdateDurationIfTimerIsRunning(); SelectedProfileInfo.DeleteSplit(); }

        public void ProfileReset()
        {
            UpdateDurationIfTimerIsRunning();

            // Apply on all tabs
            foreach (ProfileViewControl pvc_tab in ptc.ProfileViewControls)
            {
                IProfileInfo pi_tab = pvc_tab.ProfileInfo;
                pi_tab.ResetRun();
                profs.SaveProfile(pi_tab); // save tab's profile
            }
        }
        public void ProfilePB()
        {
            UpdateDurationIfTimerIsRunning();

            // Apply on all tabs
            foreach (ProfileViewControl pvc_tab in ptc.ProfileViewControls)
            {
                IProfileInfo pi_tab = pvc_tab.ProfileInfo;
                pi_tab.setPB();
                profs.SaveProfile(pi_tab); // save tab's profile
            }
        }
        public void ProfileHit(int Amount) { UpdateDurationIfTimerIsRunning(); SelectedProfileInfo.Hit(Amount); }
        public void ProfileWayHit(int Amount) { UpdateDurationIfTimerIsRunning(); SelectedProfileInfo.WayHit(Amount); }
        public void ProfileSplitGo(int Amount) { UpdateDurationIfTimerIsRunning(); SelectedProfileInfo.GoSplits(Amount); }

        public void ProfileSetAttempts()
        {
            string amount_string = VisualBasic.Interaction.InputBox("Enter amount to be set!", "Set new run number (amount of attempts)", CurrentAttempts.ToString());
            int amount_value;
            if (!int.TryParse(amount_string, out amount_value))
            {
                if (amount_string.Equals("")) return; // Unfortunately this is the Cancel button
                MessageBox.Show("Only numbers are allowed!");
                return;
            }

            UpdateDurationIfTimerIsRunning();
            CurrentAttempts = amount_value;
        }
        
        public void GetCalculatedSums(out int TotalSplits, out int TotalActiveSplit, out int TotalHits, out int TotalHitsWay, out int TotalPB, out long TotalTime, bool PastOnly)
        {
            bool ActiveProfileFound = false;

            TotalSplits = TotalActiveSplit = TotalHits = TotalHitsWay = TotalPB = 0;
            TotalTime = 0;

            foreach (ProfileViewControl pvc_tab in ptc.ProfileViewControls)
            {
                IProfileInfo pi_tab = pvc_tab.ProfileInfo;
                int Splits = pi_tab.SplitCount;

                if ((pi_tab == SelectedProfileInfo) && PastOnly) // When the past should be calculated only, stop when active profile tab found
                    break;

                TotalSplits += Splits;
                for (int i = 0; i < Splits; i++)
                {
                    TotalHits += pi_tab.GetSplitHits(i);
                    TotalHitsWay += pi_tab.GetSplitWayHits(i);
                    TotalPB += pi_tab.GetSplitPB(i);
                    TotalTime += pi_tab.GetSplitDuration(i);
                }

                if (!ActiveProfileFound)
                {
                    if (pi_tab == SelectedProfileInfo) // Active profile tab found
                    {
                        TotalActiveSplit += pi_tab.ActiveSplit;
                        ActiveProfileFound = true;
                    }
                    else TotalActiveSplit += Splits; // Add all splits of preceeding profiles
                }
            }
        }

        #endregion
    }
}
