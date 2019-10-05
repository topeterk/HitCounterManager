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
        private bool DragDropEnabled = true;

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
            if (!DragDropEnabled) return;

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
                        ProfileTabRemove(TabPageDragDrop);
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

                // How to swtich tabs...
                // - Just overwrite the references to the pages at the specific indicies:
                //   On Windows working solution but will remove entries using Mono.
                //       TabPages[Index1] = hover_Tab;
                //       TabPages[Index2] = TabPageDragDrop;
                // - Modifying index of one to move it around:
                //   On Mono working solution, but on Windows it seems it does not update order in TabPages
                //       Controls.SetChildIndex(hover_Tab, Index1);
                // - Be pragmatic and remove one tab and insert it at the new position again: works..
                TabPages.RemoveAt(Index1);
                TabPages.Insert(Index2, TabPageDragDrop);

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
        public bool ReadOnlyMode
        {
            get { return !DragDropEnabled; }
            set
            {
                DragDropEnabled = !value;

                foreach (ProfileViewControl pvc_tab in ProfileViewControls)
                    pvc_tab.ProfileInfo.ReadOnly = value;
            }
        }

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
        /// Creates a new ProfileViewControl in a new tab.
        /// For user controlled tab creation use ProfileTabCreateAndSelect instead!
        /// </summary>
        /// <returns>Created instance</returns>
        public ProfileViewControl ProfileTabCreate()
        {
            // Workaround: TabPage size calculation incorrect...
            //   - first tab when created programmatically during Form init uses wrong TabPage size
            //   - one can see then halting with debugger at next code line that tab sizes are different
            //   - selected tab has correct size, info from: https://stackoverflow.com/questions/56242122/c-sharp-winforms-tabpage-size-and-clientsize-wrong
            //   (same issue:) https://stackoverflow.com/questions/29939232/anchor-failed-with-tab-page
            //   (same issue:) https://social.msdn.microsoft.com/Forums/en-US/6fa5e92a-8ab1-41cb-a348-23a8f3bad48c/problem-with-anchor-in-a-inherited-user-control?forum=netfxcompact
            // Solution here: force Size of all tabs to be equal when creating a new one
            //   (from: https://stackoverflow.com/questions/56242122/c-sharp-winforms-tabpage-size-and-clientsize-wrong )
            System.Drawing.Size same = SelectedTab.Size;
            for (int i = TabPages.Count - 1; 0 <= i; i--)
                TabPages[i].Size = same;

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

                    // Workaround: Controls contained in a TabPage are not created until the tab page is shown,
                    //             and any data bindings in these controls are not activated until the tab page is shown.
                    //             (see: https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.tabpage?redirectedfrom=MSDN&view=netframework-4.8 )
                    // Solution: Temporarily add the control to an already visible tab in order to properly create the control
                    TabPages[0].Controls.Add(pvc_new);
                    TabPages[0].Controls.Remove(pvc_new);

                    TabPages[i].Controls.Add(pvc_new);
                    TabPages.Insert(i + 1, "+");

                    ProfileTabSelectedHandler(pvc_new, ProfileTabSelectAction.Created);
                    return pvc_new;
                }
            }

            return null;
        }

        /// <summary>
        /// Creates and selects a new ProfileViewControl in a new tab
        /// </summary>
        public void ProfileTabCreateAndSelect()
        {
            for (int i = TabPages.Count - 1; 0 <= i; i--)
            {
                if (TabPages[i].Text.Equals("+")) // Search for "New" tab
                {
                    SelectTab(i); // We use the selection handler for creation
                    return;
                }
            }
        }

        /// <summary>
        /// Removes a tab
        /// </summary>
        /// <param name="Target">The tab that will be removed</param>
        public void ProfileTabRemove(TabPage Target)
        {
            if (ReadOnlyMode)
            {
                System.Media.SystemSounds.Beep.Play();
                return; // control not editable
            }

            // Remove tab but we still need to keep last regular one, the "New" and "Delete tabs.
            if (TabPages.Count <= 3) return;

            // Mono workaround: Selection required otherwise empty tab will be shown after deletion
            // Solution: Select last available tab (as it gets shifted due to deletion
            //           This way we force TabControl to re-select tab upon deletion
            SelectedIndex = TabPages.Count-3;

            int index = TabPages.IndexOf(Target);
            ProfileTabSelectedHandler((ProfileViewControl)Target.Controls["pvc"], ProfileTabSelectAction.Deleting);
            TabPages.RemoveAt(index);

            for (int i = TabPages.Count - 3; 0 <= i; i--)
            {
                TabPages[i].Text = (i + 1).ToString(); // Update tab names
            }

            // Mono workaround: Selection required otherwise empty tab will be shown after deletion
            // Solution: Due to new selection the tab will be loaded properly again and no empty page is shown
            SelectedIndex = (index == 0 ? 0 : index-1); 
        }

        public void TabSelectingHandler(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage.Text.Equals("-")) // Switch to tab?
            {
                e.Cancel = true; // "Delete" tab cannot be selected
                return;
            }

            if (e.TabPage.Text.Equals("+")) // Create new tab?
            {
                if (ReadOnlyMode)
                {
                    System.Media.SystemSounds.Beep.Play();
                    e.Cancel = true; // control not editable
                    return;
                }

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

                ProfileTabSelectedHandler(null, ProfileTabSelectAction.Selecting);
                SelectedProfileViewControl = ProfileTabCreate(); // Create and redirect interaction to selected tab
            }
            else
            {
                ProfileTabSelectedHandler(null, ProfileTabSelectAction.Selecting);
                SelectedProfileViewControl = (ProfileViewControl)e.TabPage.Controls["pvc"]; // redirect interaction to selected tab
            }

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
