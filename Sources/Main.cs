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
using System.Windows.Forms;

namespace HitCounterManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
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
        public static bool IsOnScreen(in int Left, in int Top, in int Width, in int Threshold = 10, in int RectSize = 30)
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
