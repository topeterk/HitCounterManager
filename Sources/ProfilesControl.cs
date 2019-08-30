﻿//MIT License

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
    public partial class ProfilesControl : UserControl
    {
        private readonly int gpSuccession_Height;
        private int SuccessionAttempts = 0;
        private Profiles profs;

        public ProfilesControl()
        {
            InitializeComponent();

            gpSuccession_Height = gpSuccession.Height; // remember expanded size from designer settings
            ShowSuccessionMenu(false); // start collapsed

            ptc.InitializeProfileTabControl();
        }

        [Browsable(false)] // Hide from designer
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Hide from designer generator
        public ProfileTabControl ProfileTabControl { get { return ptc; } }

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

        #endregion
        #region Profile related
        
        public string SelectedProfile { get { return ptc.SelectedProfileViewControl.SelectedProfile; } }

        [Browsable(false)] // Hide from designer
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Hide from designer generator
        public int CurrentAttempts
        {
            get { return (ptc.SuccessionActive ? SuccessionAttempts : ptc.SelectedProfileInfo.AttemptsCount); }
            set
            {
                if (ptc.SuccessionActive)
                {
                    SuccessionAttempts = value;
                    ProfileChangedHandler(this, null); // Notify about change as there is no profile which will do this for us
                }
                else
                    ptc.SelectedProfileInfo.AttemptsCount = value;
            }
        }
 
        public void InitializeProfilesControl(Profiles profiles, string ProfileSelected, string SuccessionTitle, bool ShowSuccession)
        {
            profs = profiles;

            ptc.SelectedProfileViewControl.SetProfileList(profs.GetProfileList(), ProfileSelected);
            ptc.SelectedProfileInfo.SetSessionProgress(0, true);

            if (null != SuccessionTitle) txtPredecessorTitle.Text = SuccessionTitle;
            cbShowPredecessor.Checked = ShowSuccession;
        }

        public event EventHandler<EventArgs> ProfileChanged;
        public void ProfileChangedHandler(object sender, EventArgs e)
        {
            if (null != ProfileChanged) ProfileChanged(sender, e); // Fire event
        }

        private void SelectedProfileChanged(object sender, ProfileViewControl.SelectedProfileChangedCauseType cause)
        {
            ProfileViewControl pvc_sender = (ProfileViewControl)sender;
            if (cause != ProfileViewControl.SelectedProfileChangedCauseType.Delete)
            {
                profs.SaveProfile(pvc_sender.ProfileInfo); // save currently selected profile
            }
            profs.LoadProfile(pvc_sender.SelectedProfile, pvc_sender.ProfileInfo);
        }
  
        private void ProfileTabSelect(object sender, ProfileTabControl.ProfileTabSelectAction action)
        {
            ProfileViewControl pvc_sender = (ProfileViewControl)sender;
            switch (action)
            {
                case ProfileTabControl.ProfileTabSelectAction.Selecting:
                    profs.SaveProfile(ptc.SelectedProfileInfo); // save current tab's profile
                    break;
                case ProfileTabControl.ProfileTabSelectAction.Created:
                    pvc_sender.SetProfileList(profs.GetProfileList(), null);
                    break;
                case ProfileTabControl.ProfileTabSelectAction.Selected:
                    profs.LoadProfile(pvc_sender.ProfileInfo.ProfileName, pvc_sender.ProfileInfo);
                    break;
                default: break;
            }
        }

        public void ProfileNew()
        {
            string Name = VisualBasic.Interaction.InputBox("Enter name of new profile", "New profile", SelectedProfile);
            if (Name.Length == 0) return;

            if (ptc.SelectedProfileViewControl.HasProfile(Name)) // TODO: Check at profs instead?
            {
                MessageBox.Show("A profile with this name already exists!", "Profile already exists", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            profs.SaveProfile(ptc.SelectedProfileViewControl.ProfileInfo); // save previous selected profile

            // Apply on all tabs
            foreach (ProfileViewControl pvc_tab in ptc.ProfileViewControls)
            {
                pvc_tab.CreateNewProfile(Name, (pvc_tab == ptc.SelectedProfileViewControl)); // Select only for the current tab
            }
        }
        public void ProfileRename()
        {
            string NameOld = SelectedProfile;
            if (null == NameOld) return;

            string NameNew = VisualBasic.Interaction.InputBox("Enter new name for profile \"" + NameOld + "\"!", "Rename profile", NameOld);
            if (NameNew.Length == 0) return;

            if (ptc.SelectedProfileViewControl.HasProfile(NameNew)) // TODO: Check at profs instead?
            {
                MessageBox.Show("A profile with this name already exists!", "Profile already exists", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            profs.RenameProfile(NameOld, NameNew);

            // Apply on all tabs
            foreach (ProfileViewControl pvc_tab in ptc.ProfileViewControls)
            {
                pvc_tab.RenameProfile(NameOld, NameNew);
            }
        }
        public void ProfileCopy()
        {
            profs.SaveProfile(ptc.SelectedProfileViewControl.ProfileInfo); // save previous selected profile

            string NameNew = ptc.SelectedProfileViewControl.CopySelectedProfile(); // Apply on foreground tab

            // Apply on all tabs
            foreach (ProfileViewControl pvc_tab in ptc.ProfileViewControls)
            {
                if (pvc_tab == ptc.SelectedProfileViewControl) continue; // Skip current tab
                pvc_tab.CreateNewProfile(NameNew, false);
            }
        }
        public void ProfileDelete()
        {
            string Name = SelectedProfile;
            if (DialogResult.OK == MessageBox.Show("Do you really want to delete profile \"" + Name + "\"?", "Deleting profile", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                profs.DeleteProfile(Name);

                // Apply on all tabs
                foreach (ProfileViewControl pvc_tab in ptc.ProfileViewControls)
                {
                    if (pvc_tab == ptc.SelectedProfileViewControl)
                        ptc.SelectedProfileViewControl.DeleteSelectedProfile(); // Apply on foreground tab: Remove profile and select next one (if any)
                    else
                        pvc_tab.DeleteProfile(Name); // background tab: Remove profile and if was selected, unselect
                }

                // profile was changed by deletion, so we load the newly selected profile
                profs.LoadProfile(SelectedProfile, ptc.SelectedProfileInfo);
            }
        }

        public void ProfileSplitPermute(int Amount) { ptc.SelectedProfileInfo.PermuteSplit(ptc.SelectedProfileInfo.ActiveSplit, Amount); }
        public void ProfileSplitInsert() { ptc.SelectedProfileInfo.InsertSplit(); }

        public void ProfileReset()
        { 
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
            // Apply on all tabs
            foreach (ProfileViewControl pvc_tab in ptc.ProfileViewControls)
            {
                IProfileInfo pi_tab = pvc_tab.ProfileInfo;
                pi_tab.setPB();
                profs.SaveProfile(pi_tab); // save tab's profile
            }
        }
        public void ProfileHit(int Amount) { ptc.SelectedProfileInfo.Hit(Amount); }
        public void ProfileWayHit(int Amount) { ptc.SelectedProfileInfo.WayHit(Amount); }
        public void ProfileSplitGo(int Amount) { ptc.SelectedProfileInfo.GoSplits(Amount); }

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
            CurrentAttempts = amount_value;
        }
        
        public void GetCalculatedSums(out int TotalSplits, out int TotalActiveSplit, out int TotalHits, out int TotalHitsWay, out int TotalPB, bool PastOnly)
        {
            bool ActiveProfileFound = false;

            TotalSplits = TotalActiveSplit = TotalHits = TotalHitsWay = TotalPB = 0;

            foreach (ProfileViewControl pvc_tab in ptc.ProfileViewControls)
            {
                IProfileInfo pi_tab = pvc_tab.ProfileInfo;
                int Splits = pi_tab.SplitCount;

                if ((pi_tab == ptc.SelectedProfileInfo) && PastOnly) // When the past should be calculated only, stop when active profile tab found
                    break;

                TotalSplits += Splits;
                for (int i = 0; i < Splits; i++)
                {
                    TotalHits += pi_tab.GetSplitHits(i);
                    TotalHitsWay += pi_tab.GetSplitWayHits(i);
                    TotalPB += pi_tab.GetSplitPB(i);
                }

                if (!ActiveProfileFound)
                {
                    if (pi_tab == ptc.SelectedProfileInfo) // Active profile tab found
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
