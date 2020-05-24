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
using System.Drawing;
using System.Windows.Forms;

namespace HitCounterManager
{
    static class Program
    {
        // Debug symbol that can be used in every scope:
        // Example: ((ProfilesControl)Program.main.Controls["profCtrl"]).ProfileTabControl.ProfileViewControls[2].ProfileInfo.ActiveSplit
        private static Form main;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            main = new Form1();
            Application.Run(main);
        }

        /// <summary>
        /// Checks if the upper corners of a given window location is visible on any available screen.
        /// This makes sure the taskbar is visible at this location and the user can move or interact with the window.
        /// </summary>
        /// <param name="Left">X-coordiate of the window's title bar location</param>
        /// <param name="Top">Y-coordiate of the window's title bar location</param>
        /// <param name="Width">Width of the window's title bar</param>
        /// <param name="Threshold">Additional margin.
        ///                         Ensures moving directly at the outer borders of the screens is still fine.
        ///                         Helps checking versus fullscreen or anchored windows.</param>
        /// <param name="RectSize">Minimum size of the windows's title bar that must be visible.</param>
        /// <returns>TRUE = Window's title bar is visible on at least one screen.
        ///          FALSE = Window is not visible on any screen.</returns>
        public static bool IsOnScreen(int Left, int Top, int Width, int Threshold = 10, int RectSize = 30)
        {
            System.Drawing.Rectangle rectLeft = new System.Drawing.Rectangle(Left + Threshold, Top + Threshold, RectSize, RectSize); // upper left corner
            System.Drawing.Rectangle rectRight = new System.Drawing.Rectangle(Left + Width - Threshold - RectSize, Top + Threshold, RectSize, RectSize); // upper right corner
            foreach (Screen screen in Screen.AllScreens)
            {
                // at least one of the edges must be present on any screen
                if (screen.WorkingArea.Contains(rectLeft)) return true;
                else if (screen.WorkingArea.Contains(rectRight)) return true;
            }
            return false;
        }

        #region Dark Mode
        private static Color Control_Light = Color.FromKnownColor(KnownColor.Control);
        private static Color Control_Dark = Color.FromArgb(255,40,40,40);
        private static Color Control_Dark_Button =  Color.FromKnownColor(KnownColor.ControlDark);

        private static Color ControlText_Light = Color.FromKnownColor(KnownColor.ControlText);
        private static Color ControlText_Dark = Color.FromKnownColor(KnownColor.HighlightText);
        private static Color ControlText_Light_GroupBox = Color.FromKnownColor(KnownColor.HighlightText);
        private static Color ControlText_Light_DataGridView_Mono = Color.FromKnownColor(KnownColor.HighlightText);

        private static Color AppWorkspace_Light = Color.FromKnownColor(KnownColor.AppWorkspace);
        private static Color AppWorkspace_Dark = Control_Dark;

        private static Color Window_Light = Color.FromKnownColor(KnownColor.Window);
        private static Color Window_Dark = Color.FromKnownColor(KnownColor.WindowText);

        private static bool _DarkMode = false;
        /// <summary>
        /// Get/Sets the dark mode state: true = Dark mode; false = Light mode.
        /// Update controls afterwards to apply effect visually!
        /// </summary>
        public static bool DarkMode
        {
            get { return _DarkMode; }
            set
            {
                _DarkMode = value;

                // Workaround: Mono loads themes and actual colors later,
                // so we load it on every dark mode change instead of only once during startup
                Control_Light = Color.FromKnownColor(KnownColor.Control);
                Control_Dark = Color.FromArgb(255,40,40,40);
                Control_Dark_Button =  Color.FromKnownColor(KnownColor.ControlDark);

                ControlText_Light = Color.FromKnownColor(KnownColor.ControlText);
                ControlText_Light_GroupBox = Color.FromKnownColor(KnownColor.HighlightText);
                ControlText_Light_DataGridView_Mono = Color.FromKnownColor(KnownColor.HighlightText);
                ControlText_Dark = Color.FromKnownColor(KnownColor.HighlightText);

                AppWorkspace_Light = Color.FromKnownColor(KnownColor.AppWorkspace);
                AppWorkspace_Dark = Control_Dark;

                Window_Light = Color.FromKnownColor(KnownColor.Window);
                Window_Dark = Color.FromKnownColor(KnownColor.WindowText);
            }
        }

        /// <summary>
        /// Updates a Control an its children to apply current dark mode settings
        /// </summary>
        /// <param name="ctrl">Root Control like a Form</param>
        public static void UpdateDarkMode(this Control ctrl)
        {
            // Workaround: Mono has several different "KnownColors", so there a two or more "Controls"
            // Therefore, instead of checkin if the color matches a specific one, we have to check the name.
            try
            {
                if (DarkMode)
                {
                    // Background..

                    if (ctrl is Button)
                    {
                        if (ctrl.BackColor.Name == Control_Light.Name) ctrl.BackColor = Control_Dark_Button;
                    }
                    else if (ctrl is DataGridView)
                    {
                        DataGridView dgv = (DataGridView)ctrl;
                        DataGridViewCellStyle dcs = dgv.DefaultCellStyle;
                        if (dgv.BackgroundColor.Name == AppWorkspace_Light.Name) dgv.BackgroundColor = AppWorkspace_Dark;
                        if (dcs.BackColor.Name == Window_Light.Name) dcs.BackColor = Window_Dark;
                        if (OsLayer.Name != "Win") // required for Mono
                        {
                            if (dcs.ForeColor.Name == ControlText_Light.Name) dcs.ForeColor = ControlText_Dark;
                            if (dcs.SelectionForeColor.Name == ControlText_Light.Name) dcs.SelectionForeColor = ControlText_Dark;
                        }
                        if (dgv.RowHeadersDefaultCellStyle.BackColor.Name == Control_Light.Name) dgv.RowHeadersDefaultCellStyle.BackColor = Control_Dark;
                        if (dgv.ColumnHeadersDefaultCellStyle.BackColor.Name == Control_Light.Name) dgv.ColumnHeadersDefaultCellStyle.BackColor = Control_Dark;
                        if (dgv.ColumnHeadersDefaultCellStyle.ForeColor.Name == Window_Dark.Name) dgv.ColumnHeadersDefaultCellStyle.ForeColor = ControlText_Dark;
                    }
                    else if (ctrl is TextBox)
                    {
                        TextBox tb = (TextBox)ctrl;
                        if (tb.ReadOnly)
                        {
                            if (ctrl.BackColor.Name == Window_Light.Name) ctrl.BackColor = Window_Dark;
                        }
                        else
                        {
                            if (ctrl.BackColor.Name == Control_Light.Name) ctrl.BackColor = Control_Dark;
                        }
                    }
                    else
                    {
                        if (ctrl.BackColor.Name == Control_Light.Name) ctrl.BackColor = Control_Dark;
                    }

                    // Foreground..

                    // Workaround: The GroupBox text seems using highlight colors, so we force it back to "normal" colors
                    if (ctrl is GroupBox) { if (ctrl.ForeColor.Name == ControlText_Light_GroupBox.Name) ctrl.ForeColor = ControlText_Dark; }

                    if (!(ctrl is Button))
                    {
                        if (ctrl.ForeColor.Name == ControlText_Light.Name) ctrl.ForeColor = ControlText_Dark;
                    }
                }
                else
                {
                    // Background..

                    if (ctrl is Button)
                    {
                        if (ctrl.BackColor.Name == Control_Dark_Button.Name) ctrl.BackColor = Control_Light;
                    }
                    else if (ctrl is DataGridView)
                    {
                        DataGridView dgv = (DataGridView)ctrl;
                        DataGridViewCellStyle dcs = dgv.DefaultCellStyle;
                        if (dgv.BackgroundColor.Name == AppWorkspace_Dark.Name) dgv.BackgroundColor = AppWorkspace_Light;
                        if (dcs.BackColor.Name == Window_Dark.Name) dcs.BackColor = Window_Light;
                        if (OsLayer.Name != "Win") // required for Mono
                        {
                            if (dcs.ForeColor.Name == ControlText_Dark.Name) dcs.ForeColor = ControlText_Light;
                            if (dcs.SelectionForeColor.Name == ControlText_Dark.Name) dcs.SelectionForeColor = ControlText_Light;
                        }
                        if (dgv.RowHeadersDefaultCellStyle.BackColor.Name == Control_Dark.Name) dgv.RowHeadersDefaultCellStyle.BackColor = Control_Light;
                        if (dgv.ColumnHeadersDefaultCellStyle.BackColor.Name == Control_Dark.Name) dgv.ColumnHeadersDefaultCellStyle.BackColor = Control_Light;
                        if (dgv.ColumnHeadersDefaultCellStyle.ForeColor.Name == ControlText_Dark.Name) dgv.ColumnHeadersDefaultCellStyle.ForeColor = Window_Dark;
                    }
                    else if (ctrl is TextBox)
                    {
                        TextBox tb = (TextBox)ctrl;
                        if (tb.ReadOnly)
                        {
                            if (ctrl.BackColor.Name == Window_Dark.Name) ctrl.BackColor = Window_Light;
                        }
                        else
                        {
                            if (ctrl.BackColor.Name == Control_Dark.Name) ctrl.BackColor = Control_Light;
                        }
                    }
                    else
                    {
                        if (ctrl.BackColor.Name == Control_Dark.Name) ctrl.BackColor = Control_Light;
                    }

                    // Foreground..

                    if (!(ctrl is Button))
                    {
                        if (ctrl.ForeColor.Name == ControlText_Dark.Name) ctrl.ForeColor = ControlText_Light;
                    }
                }

                // Update all childs..
                foreach (Control item in ctrl.Controls) item.UpdateDarkMode();
            }
            catch { }
        }
        #endregion
    }

    #region InputBox (replaceable with Microsoft.​Visual​Basic.Interaction.Input​Box)
    namespace VisualBasic
    {
        public static class Interaction
        {
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
        }
    }
    #endregion
}
