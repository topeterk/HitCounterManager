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
using System.ComponentModel;
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

        private bool SettingsDialogOpen = false;

        private readonly int gpSuccession_Height;

        #region Form

        public Form1()
        {
            InitializeComponent();
            tabControl1.InitializeProfileTabControl();

            gpSuccession_Height = gpSuccession.Height; // remember expanded size from designer settings

            om = new OutModule(tabControl1.SelectedProfileInfo);
            sc = new Shortcuts(Handle);

            tabControl1.ProfileChanged += UpdateProgressAndTotals;
            tabControl1.ProfileViewControlSelected += TabControl1_ProfileTabSelected;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += (sender2, certificate, chain, errors) => { return true; };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = Text + " - v" + Application.ProductVersion + " " + OsLayer.Name;
            btnHit.Select();
            tabControl1.SelectedProfileInfo.ProfileUpdateBegin();
            LoadSettings();
            ShowSuccessionMenu(false); // start collapsed
            tabControl1.SelectedProfileInfo.ProfileUpdateEnd(); // Write very first output once after application start (fires ProfileChanged with UpdateProgressAndTotals())
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

        private void UpdateProgressAndTotals(object sender, EventArgs e)
        {
            int TotalSplits, TotalActiveSplit, TotalHits, TotalHitsWay, TotalPB;
            tabControl1.GetCalculatedSums(out TotalSplits, out TotalActiveSplit, out TotalHits, out TotalHitsWay, out TotalPB);
            lbl_progress.Text = "Progress:  " + TotalActiveSplit + " / " + TotalSplits + "  # " + tabControl1.CurrentAttempts.ToString("D3");
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

        private void SuccessionChanged(object sender, EventArgs e)
        {
            om.ShowSuccession = cbShowPredecessor.Checked;
            om.SuccessionTitle = txtPredecessorTitle.Text;
            om.Update();
        }

        private void TabControl1_ProfileTabSelected(object sender, TabControlCancelEventArgs e)
        {
            // Switch Output module to interact with selected tab
            om = new OutModule(tabControl1.SelectedProfileInfo);
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

        private void btnNew_Click(object sender, EventArgs e) { tabControl1.AddProfile(); }
        private void btnRename_Click(object sender, EventArgs e) { tabControl1.SelectedProfileRename(); }
        private void btnCopy_Click(object sender, EventArgs e) { tabControl1.SelectedProfileCopy(); }
        private void btnDelete_Click(object sender, EventArgs e) { tabControl1.SelectedProfileDelete(); }

        private void btnAttempts_Click(object sender, EventArgs e)
        {
            string amount_string = InputBox("Enter amount to be set!", "Set new run number (amount of attempts)", tabControl1.CurrentAttempts.ToString());
            int amount_value;
            if (!int.TryParse(amount_string, out amount_value))
            {
                if (amount_string.Equals("")) return; // Unfortunately this is the Cancel button
                MessageBox.Show("Only numbers are allowed!");
                return;
            }
            tabControl1.CurrentAttempts = amount_value;
        }
        private void btnUp_Click(object sender, EventArgs e) { tabControl1.SelectedProfileInfo.PermuteSplit(tabControl1.SelectedProfileInfo.ActiveSplit, -1); }
        private void btnDown_Click(object sender, EventArgs e) { tabControl1.SelectedProfileInfo.PermuteSplit(tabControl1.SelectedProfileInfo.ActiveSplit, +1); }
        private void BtnInsertSplit_Click(object sender, EventArgs e) { tabControl1.SelectedProfileInfo.InsertSplit(); }

        private void BtnOnTop_Click(object sender, EventArgs e) { SetAlwaysOnTop(!this.TopMost); }

        private void btnReset_Click(object sender, EventArgs e) { tabControl1.SelectedProfilesReset(); }
        private void btnPB_Click(object sender, EventArgs e) { tabControl1.SelectedProfilesPB(); }
        private void btnHit_Click(object sender, EventArgs e) { tabControl1.SelectedProfileInfo.Hit(+1); }
        private void btnHitUndo_Click(object sender, EventArgs e) { tabControl1.SelectedProfileInfo.Hit(-1); }
        private void btnWayHit_Click(object sender, EventArgs e) { tabControl1.SelectedProfileInfo.WayHit(+1); }
        private void btnWayHitUndo_Click(object sender, EventArgs e) { tabControl1.SelectedProfileInfo.WayHit(-1); }
        private void btnSplit_Click(object sender, EventArgs e) { tabControl1.SelectedProfileInfo.MoveSplits(+1); }
        private void btnSplitPrev_Click(object sender, EventArgs e) { tabControl1.SelectedProfileInfo.MoveSplits(-1); }
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

        #endregion
    }

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
        private int SuccessionAttempts = 0;

        [Browsable(false)] // Hide from designer
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Hide from designer generator
        public int CurrentAttempts
        {
            get
            {
                return (1 < ProfileViewControls.Length ? SuccessionAttempts : SelectedProfileInfo.AttemptsCount); // Succession active?
            }
            set
            {
                if (1 < ProfileViewControls.Length) // Succession active?
                {
                    SuccessionAttempts = value;
                    PVC_ProfileChangedHandler(this, null); // Notify about change as there is no profile which will do this for us
                }
                else
                    SelectedProfileInfo.AttemptsCount = value;
            }
        }


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
                foreach(TabPage page in TabPages)
                {
                    if (page.Text.Equals("+") || page.Text.Equals("-")) continue; // Skip "New"/"Delete" tab
                    pvcs[i++] = (ProfileViewControl)page.Controls["pvc"];
                }
                return pvcs;
            }
        }

        public void InitializeProfileTabControl()
        {
            SelectedProfileViewControl = ProfileViewControls[0]; // the only one created by designer
            SelectedProfileInfo = SelectedProfileViewControl.ProfileInfo;
            SelectedProfileInfo.ProfileChanged += PVC_ProfileChangedHandler;
            SelectedProfileViewControl.SelectedProfileChanged += PVC_SelectedProfileChangedHandler;
            Selecting += TabSelectingHandler;
        }
        public void LoadProfileTabControl(Profiles profiles) { profs = profiles; }

        private void PVC_SelectedProfileChangedHandler(object sender, ProfileViewControl.SelectedProfileChangedCauseType cause)
        {
            if (cause != ProfileViewControl.SelectedProfileChangedCauseType.Delete)
            {
                profs.SaveProfile(); // save currently selected profile
            }
            profs.LoadProfile(((ProfileViewControl)sender).SelectedProfile);
        }

        public event EventHandler<EventArgs> ProfileChanged;
        public void PVC_ProfileChangedHandler(object sender, EventArgs e)
        {
            if (null != ProfileChanged) ProfileChanged(sender, e); // Fire event
        }

        public event EventHandler<TabControlCancelEventArgs> ProfileViewControlSelected;
        private void TabSelectingHandler(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage.Text.Equals("-")) // Switch to tab?
            {
                e.Cancel = true; // "Delete" tab cannot be selected
                return;
            }
            
            profs.SaveProfile(); // save current tab's profile

            if (e.TabPage.Text.Equals("+")) // Create new tab?
            {
                if (ProfileViewControls.Length == 1) // Warning message only on the first tab creation
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
            profs.SetProfileInfo(SelectedProfileInfo);
            profs.LoadProfile(SelectedProfileInfo.ProfileName);

            if (null != ProfileViewControlSelected) ProfileViewControlSelected(SelectedProfileViewControl, e); // Fire event
        }

        public void GetCalculatedSums(out int TotalSplits, out int TotalActiveSplit, out int TotalHits, out int TotalHitsWay, out int TotalPB)
        {
            bool ActiveProfileFound = false;

            TotalSplits = TotalActiveSplit = TotalHits = TotalHitsWay = TotalPB = 0;

            foreach(ProfileViewControl pvc_tab in ProfileViewControls)
            {
                IProfileInfo pi_tab = pvc_tab.ProfileInfo;
                int Splits = pi_tab.SplitCount;

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

        public void AddProfile()
        {
            string Name = Form1.InputBox("Enter name of new profile", "New profile", SelectedProfileViewControl.SelectedProfile);
            if (Name.Length == 0) return;

            if (SelectedProfileViewControl.HasProfile(Name))
            {
                MessageBox.Show("A profile with this name already exists!", "Profile already exists", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            profs.SaveProfile(); // save previous selected profile

            // Apply on all tabs
            foreach(ProfileViewControl pvc_tab in ProfileViewControls)
            {
                pvc_tab.CreateNewProfile(Name, (pvc_tab == SelectedProfileViewControl)); // Select only for the current tab
            }
        }

        public void SelectedProfileCopy()
        {
            profs.SaveProfile(); // save previous selected profile
            SelectedProfileViewControl.CopySelectedProfile(); // Apply on foreground tab

            // Apply on all tabs
            foreach(ProfileViewControl pvc_tab in ProfileViewControls)
            {
                if (pvc_tab == SelectedProfileViewControl) continue; // Skip current tab
                pvc_tab.CreateNewProfile(SelectedProfileViewControl.SelectedProfile, false);
            }
        }

        public void SelectedProfileRename()
        {
            string NameOld = SelectedProfileViewControl.SelectedProfile;
            if (null == NameOld) return;

            string NameNew = Form1.InputBox("Enter new name for profile \"" + NameOld + "\"!", "Rename profile", NameOld);
            if (NameNew.Length == 0) return;

            if (SelectedProfileViewControl.HasProfile(NameNew))
            {
                MessageBox.Show("A profile with this name already exists!", "Profile already exists", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            profs.RenameProfile(NameOld, NameNew);

            // Apply on all tabs
            foreach(ProfileViewControl pvc_tab in ProfileViewControls)
            {
                pvc_tab.RenameProfile(NameOld, NameNew);
            }
        }

        public void SelectedProfileDelete()
        {
            string Name = SelectedProfileViewControl.SelectedProfile;
            if (DialogResult.OK == MessageBox.Show("Do you really want to delete profile \"" + Name + "\"?", "Deleting profile", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                profs.DeleteProfile(Name);
                SelectedProfileViewControl.DeleteSelectedProfile(); // Apply on foreground tab

                // Apply on all background tabs
                foreach(ProfileViewControl pvc_tab in ProfileViewControls)
                {
                    if (pvc_tab == SelectedProfileViewControl) continue; // Skip current tab
                    pvc_tab.DeleteProfile(Name);
                }

                // profile was changed by deletion, so try loading the newly selected profile
                profs.LoadProfile(SelectedProfileViewControl.SelectedProfile);
            }
        }

        public void SelectedProfilesReset()
        {
            // Apply on all tabs
            foreach (ProfileViewControl pvc_tab in ProfileViewControls)
            {
                IProfileInfo pi_tab = pvc_tab.ProfileInfo;
                profs.SetProfileInfo(pi_tab);
                pi_tab.ResetRun();
                profs.SaveProfile(); // save tab's profile
            }
        }

        public void SelectedProfilesPB()
        {
            // Apply on all tabs
            foreach(ProfileViewControl pvc_tab in ProfileViewControls)
            {
                IProfileInfo pi_tab = pvc_tab.ProfileInfo;
                profs.SetProfileInfo(pi_tab);
                pi_tab.setPB();
                profs.SaveProfile(); // save tab's profile
            }

            profs.SetProfileInfo(SelectedProfileInfo); // Restore current profile selection
        }

        #endregion
    }
}
