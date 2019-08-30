//MIT License

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
    public class ProfileTabControl : TabControl
    {
        #region TabControl event handlers (mainly DragAndDrop)

        private TabPage TabPageDragDrop = null;

        public ProfileTabControl()
        {
            MouseDown += new MouseEventHandler(MouseDownHandler);
            MouseUp += new MouseEventHandler(MouseUpHandler);
            MouseMove += new MouseEventHandler(MouseMoveHandler);
        }

        private TabPage GetTabUnderMouse()
        {
            for (int index = 0; index <= TabCount - 1; index++)
            {
                if (GetTabRect(index).Contains(PointToClient(Cursor.Position)))
                    return TabPages[index];
            }
            return null;
        }

        private void MouseDownHandler(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Start of DragDrop
                TabPage hover_Tab = GetTabUnderMouse();
                if (hover_Tab == null ) return;
                if (hover_Tab.Text.Equals("+") || hover_Tab.Text.Equals("-")) return; // Keep "New"/"Delete" tab at the end
                TabPageDragDrop = hover_Tab;
            }
        }
 
        private void MouseUpHandler(object sender, MouseEventArgs e)
        {
            if (null == TabPageDragDrop) return; // No DragDrop currently active

            if (e.Button == MouseButtons.Left)
            {
                // DragDrop stopped
                TabPage hover_Tab = GetTabUnderMouse();
                if ((hover_Tab != null) && (hover_Tab != TabPageDragDrop)) // Dragged onto nothing or itself?
                {
                    if (hover_Tab.Text.Equals("-"))// Dragged on "Delete" tab?
                    {
                        // Remove tab but we still need one regular, the "New" and "Delete tabs.
                        if (3 < TabPages.Count) TabPages.Remove(TabPageDragDrop);
                    }
                }
                TabPageDragDrop = null;
            }
        }

        private void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            if (null == TabPageDragDrop) return; // No DragDrop currently active

            if (e.Button != MouseButtons.Left)
            {
                // Should be coverd by MouseUp, just for safety
                // DragDrop stopped
                TabPageDragDrop = null;
            }
            else
            {
                // During DragDrop
                TabPage hover_Tab = GetTabUnderMouse();
                if ((hover_Tab == null) || (hover_Tab == TabPageDragDrop)) return; // Dragged onto nothing or itself?
                if (hover_Tab.Text.Equals("+") || hover_Tab.Text.Equals("-")) return; // Keep "New"/"Delete" tab at the end

                // Switch tabs but retain numbering
                int Index1 = TabPages.IndexOf(TabPageDragDrop);
                int Index2 = TabPages.IndexOf(hover_Tab);
                TabPages[Index1] = hover_Tab;
                TabPages[Index2] = TabPageDragDrop;
                hover_Tab.Text = (Index1+1).ToString();
                TabPageDragDrop.Text = (Index2+1).ToString();
                SelectedTab = TabPageDragDrop;
            }
        }

        #endregion
        
        #region Profile related implementation
        
        private Profiles profs;

        [Browsable(false)] // Hide from designer
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Hide from designer generator
        public IProfileInfo SelectedProfileInfo { get; private set; }

        [Browsable(false)] // Hide from designer
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Hide from designer generator
        public ProfileViewControl SelectedProfileViewControl { get; private set; }

        private ProfileViewControl[] ProfileViewControls
        {
            get
            {
                ProfileViewControl[] pvcs = new ProfileViewControl[TabCount-2];
                int i = 0;
                foreach (TabPage page in TabPages)
                {
                    if (page.Text.Equals("+") || page.Text.Equals("-")) continue; // Skip "New"/"Delete" tab
                    pvcs[i++] = (ProfileViewControl)page.Controls["pvc"];
                }
                return pvcs;
            }
        }
        
        [Browsable(false)] // Hide from designer
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Hide from designer generator
        public bool SuccessionActive { get { return (1 < ProfileViewControls.Length); } }

        public void InitializeProfileTabControl()
        {
            SelectedProfileViewControl = ProfileViewControls[0]; // the only one created by designer
            SelectedProfileInfo = SelectedProfileViewControl.ProfileInfo;
            SelectedProfileInfo.ProfileChanged += PVC_ProfileChangedHandler;
            SelectedProfileViewControl.SelectedProfileChanged += PVC_SelectedProfileChangedHandler;
            Selecting += TabSelectingHandler;
        }

        public void LoadProfilesIntoTabControl(Profiles profiles, string ProfileSelected)
        {
            profs = profiles;
            SelectedProfileViewControl.SetProfileList(profs.GetProfileList(), ProfileSelected);
            SelectedProfileInfo.SetSessionProgress(0, true);
        }

        private void PVC_SelectedProfileChangedHandler(object sender, ProfileViewControl.SelectedProfileChangedCauseType cause)
        {
            ProfileViewControl pvc_sender = (ProfileViewControl)sender;
            if (cause != ProfileViewControl.SelectedProfileChangedCauseType.Delete)
            {
                profs.SaveProfile(pvc_sender.ProfileInfo); // save currently selected profile
            }
            profs.LoadProfile(pvc_sender.SelectedProfile, pvc_sender.ProfileInfo);
        }

        public event EventHandler<EventArgs> ProfileChanged;
        public void PVC_ProfileChangedHandler(object sender, EventArgs e)
        {
            if (null != ProfileChanged) ProfileChanged(sender, e); // Fire event
        }

        private void TabSelectingHandler(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage.Text.Equals("-")) // Switch to tab?
            {
                e.Cancel = true; // "Delete" tab cannot be selected
                return;
            }

            profs.SaveProfile(SelectedProfileInfo); // save current tab's profile

            if (e.TabPage.Text.Equals("+")) // Create new tab?
            {
                if (!SuccessionActive) // Show initial warning message only when succession is not already active
                {
                    DialogResult result = MessageBox.Show(
                        "Opening further tabs combine multiple profiles into one run. " +
                        "Best known as a trilogy run for Dark Souls 1 to 3.\n\n" +
                        "There is a separate attempts counter. All profiles' attempts counters are paused.\n\n" + // TODO separate attempts counter
                        "Please BE AWARE the Reset and PB buttons/hotkeys will apply to ALL open profiles! " +
                        "For example, pressing reset will reset all the selected profiles of all available tabs!\n\n" +
                        "OK = I understand, continue using tabs\n" +
                        "Cancel = Ups, I better stick with one tab only",
                        "Succession", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (result != DialogResult.OK)
                    {
                        e.Cancel = true; // Action aborted
                        return;
                    }
                }

                Control template = TabPages[0].Controls["pvc"];
                TabPage page = e.TabPage;

                // Reuse the "New" tab for the actual new page and create an new "New" tab
                page.Text = (e.TabPageIndex + 1).ToString();
                TabPages.Insert(e.TabPageIndex + 1, "+");

                // Fill controls of the tab
                ProfileViewControl pvc_new = new ProfileViewControl();
                pvc_new.Anchor = template.Anchor;
                pvc_new.Location = template.Location;
                pvc_new.Name = "pvc";
                pvc_new.Size = template.Size;
                pvc_new.TabIndex = 0;
                pvc_new.ProfileInfo.ProfileChanged += PVC_ProfileChangedHandler;
                pvc_new.SelectedProfileChanged += PVC_SelectedProfileChangedHandler;
                page.Controls.Add(pvc_new);

                pvc_new.SetProfileList(profs.GetProfileList(), null);
            }

            // Switch interaction to selected tab
            SelectedProfileViewControl = (ProfileViewControl)e.TabPage.Controls["pvc"];
            SelectedProfileInfo = SelectedProfileViewControl.ProfileInfo;
            profs.LoadProfile(SelectedProfileInfo.ProfileName, SelectedProfileInfo);
        }

        public void GetCalculatedSums(out int TotalSplits, out int TotalActiveSplit, out int TotalHits, out int TotalHitsWay, out int TotalPB, bool PastOnly)
        {
            bool ActiveProfileFound = false;

            TotalSplits = TotalActiveSplit = TotalHits = TotalHitsWay = TotalPB = 0;

            foreach (ProfileViewControl pvc_tab in ProfileViewControls)
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

        public void AddAndSelectProfile(string Name)
        {
            // Apply on all tabs
            foreach (ProfileViewControl pvc_tab in ProfileViewControls)
            {
                pvc_tab.CreateNewProfile(Name, (pvc_tab == SelectedProfileViewControl)); // Select only for the current tab
            }
        }

        public void SelectedProfileCopy()
        {
            string Name = SelectedProfileViewControl.CopySelectedProfile(); // Apply on foreground tab

            // Apply on all tabs
            foreach (ProfileViewControl pvc_tab in ProfileViewControls)
            {
                if (pvc_tab == SelectedProfileViewControl) continue; // Skip current tab
                pvc_tab.CreateNewProfile(Name, false);
            }
        }

        public void SelectedProfileRename(string NameOld, string NameNew)
        {
            // Apply on all tabs
            foreach (ProfileViewControl pvc_tab in ProfileViewControls)
            {
                pvc_tab.RenameProfile(NameOld, NameNew);
            }
        }

        public void SelectedProfileDelete()
        {
            string Name = SelectedProfileViewControl.SelectedProfile;

            // Apply on all tabs
            foreach (ProfileViewControl pvc_tab in ProfileViewControls)
            {
                if (pvc_tab == SelectedProfileViewControl)
                    SelectedProfileViewControl.DeleteSelectedProfile(); // Apply on foreground tab: Remove profile and select next one (if any)
                else
                    pvc_tab.DeleteProfile(Name); // background tab: Remove profile and if was selected, unselect
            }
        }

        public void SelectedProfilesReset()
        {
            // Apply on all tabs
            foreach (ProfileViewControl pvc_tab in ProfileViewControls)
            {
                IProfileInfo pi_tab = pvc_tab.ProfileInfo;
                pi_tab.ResetRun();
                profs.SaveProfile(pi_tab); // save tab's profile
            }
        }

        public void SelectedProfilesPB()
        {
            // Apply on all tabs
            foreach (ProfileViewControl pvc_tab in ProfileViewControls)
            {
                IProfileInfo pi_tab = pvc_tab.ProfileInfo;
                pi_tab.setPB();
                profs.SaveProfile(pi_tab); // save tab's profile
            }
        }

        #endregion
    }
}
