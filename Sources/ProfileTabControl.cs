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
        #region TabControl DragAndDrop

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
                        if (3 < TabPages.Count)
                        {
                            ProfileTabSelectedHandler((ProfileViewControl)TabPageDragDrop.Controls["pvc"], ProfileTabSelectAction.Deleting);
                            TabPages.Remove(TabPageDragDrop);

                            for (int i = TabPages.Count - 3; 0 <= i; i--)
                            {
                                TabPages[i].Text = (i + 1).ToString(); // Update tab names
                            }
                        }
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
                ProfileTabPermutingHandler(this, new Tuple<int, int>(Index1, Index2));
                TabPages[Index1] = hover_Tab;
                TabPages[Index2] = TabPageDragDrop;
                hover_Tab.Text = (Index1+1).ToString();
                TabPageDragDrop.Text = (Index2+1).ToString();
                SelectedTab = TabPageDragDrop;
            }
        }

        #endregion

        #region Profile related implementation

        public void InitializeProfileTabControl()
        {
            SelectedProfileViewControl = ProfileViewControls[0]; // the only one created by designer
            SelectedProfileViewControl.ProfileInfo.ProfileChanged += PVC_ProfileChangedHandler;
            SelectedProfileViewControl.SelectedProfileChanged += PVC_SelectedProfileChangedHandler;
            Selecting += TabSelectingHandler;
        }

        public ProfileViewControl[] ProfileViewControls
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

        public bool SuccessionActive { get { return (1 < ProfileViewControls.Length); } }

        [Browsable(false)] // Hide from designer
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Hide from designer generator
        public ProfileViewControl SelectedProfileViewControl { get; private set; }

        /// <summary>
        /// Fetches the tab index of the given object
        /// </summary>
        /// <param name="pvc">Object to search for</param>
        /// <returns>-1 on failure, otherwise tab index</returns>
        public int IndexOf(ProfileViewControl pvc)
        {
            for (int i = 0; i < TabPages.Count; i++)
            {
                if ((ProfileViewControl)TabPages[i].Controls["pvc"] == pvc)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Creates a new ProfileViewControl in a new tab
        /// </summary>
        /// <returns>Created instance</returns>
        public ProfileViewControl ProfileTabCreate()
        {
            for (int i = TabPages.Count - 1; 0 <= i; i--)
            {
                if (TabPages[i].Text.Equals("+")) // Search for "New" tab
                {
                    Control template = TabPages[i-1].Controls["pvc"];

                    // Reuse the "New" tab for the actual new page and create an new "New" tab (skips tab selection)
                    TabPages[i].Text = (i + 1).ToString();

                    // Fill controls of the tab
                    ProfileViewControl pvc_new = new ProfileViewControl();
                    pvc_new.Anchor = template.Anchor;
                    pvc_new.Location = template.Location;
                    pvc_new.Name = "pvc";
                    pvc_new.Size = template.Size;
                    pvc_new.TabIndex = 0;
                    pvc_new.ProfileInfo.ProfileChanged += PVC_ProfileChangedHandler;
                    pvc_new.SelectedProfileChanged += PVC_SelectedProfileChangedHandler;
                    TabPages[i].Controls.Add(pvc_new);
                    TabPages.Insert(i + 1, "+");

                    // TODO: when achieved to run, creating a new ProfileViewControl in a new tab sets wrong size for calculating acnchor points
                    // https://stackoverflow.com/questions/29939232/anchor-failed-with-tab-page
                    // https://social.msdn.microsoft.com/Forums/en-US/6fa5e92a-8ab1-41cb-a348-23a8f3bad48c/problem-with-anchor-in-a-inherited-user-control?forum=netfxcompact
                    // Visual Studio Designer bug?

                    ProfileTabSelectedHandler(pvc_new, ProfileTabSelectAction.Created);
                    return pvc_new;
                }
            }

            return null;
        }

        public void TabSelectingHandler(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage.Text.Equals("-")) // Switch to tab?
            {
                e.Cancel = true; // "Delete" tab cannot be selected
                return;
            }

            ProfileTabSelectedHandler(null, ProfileTabSelectAction.Selecting);

            if (e.TabPage.Text.Equals("+")) // Create new tab?
            {
                if (!SuccessionActive) // Show initial warning message only when succession is not already active
                {
                    DialogResult result = MessageBox.Show(
                        "Opening further tabs combine multiple profiles into one run. " +
                        "Best known as a trilogy run for Dark Souls 1 to 3.\n\n" +
                        "There is a separate attempts counter. All profiles' attempts counters are paused.\n\n" +
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

                SelectedProfileViewControl = ProfileTabCreate(); // Create and redirect interaction to selected tab
            }
            else SelectedProfileViewControl = (ProfileViewControl)e.TabPage.Controls["pvc"]; // redirect interaction to selected tab

            ProfileTabSelectedHandler(SelectedProfileViewControl, ProfileTabSelectAction.Selected);
        }

        public enum ProfileTabSelectAction { Selecting, Created, Selected, Deleting };
        public event EventHandler<ProfileTabSelectAction> ProfileTabSelect;
        public void ProfileTabSelectedHandler(ProfileViewControl sender, ProfileTabSelectAction action)
        {
            if (null != ProfileTabSelect) ProfileTabSelect(sender, action); // Fire event
        }

        /// <summary>
        /// When two tabs will change their places.
        /// </summary>
        public event EventHandler<Tuple<int, int>> ProfileTabPermuting;
        public void ProfileTabPermutingHandler(object sender, Tuple<int, int> indices)
        {
            if (null != ProfileTabPermuting) ProfileTabPermuting(sender, indices); // Fire event
        }

        public event EventHandler<EventArgs> ProfileChanged;
        public void PVC_ProfileChangedHandler(object sender, EventArgs e)
        {
            if (null != ProfileChanged) ProfileChanged(sender, e); // Fire event
        }

        public event EventHandler<ProfileViewControl.SelectedProfileChangedCauseType> SelectedProfileChanged;
        public void PVC_SelectedProfileChangedHandler(object sender, ProfileViewControl.SelectedProfileChangedCauseType cause)
        {
            if (null != SelectedProfileChanged) SelectedProfileChanged(sender, cause); // Fire event
        }

        #endregion
    }
}
