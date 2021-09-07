//MIT License

//Copyright (c) 2021-2021 Peter Kirmeier

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

using System.Drawing;
using System.Windows.Forms;
using Application = System.Windows.Application;
using FormsApplication = System.Windows.Forms.Application;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

#if SHOW_COMPLER_VERSION // enable and hover over #error to see C# compiler version and the used language version
#error version
#endif

namespace HitCounterManager.WPF
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            FormsApplication.EnableVisualStyles();
            FormsApplication.SetCompatibleTextRenderingDefault(false);
        }
    }

    // TODO ? Replace with: https://docs.microsoft.com/de-de/xamarin/xamarin-forms/user-interface/pop-ups
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
                inputBox.ClientSize = new Size(500, 80);
                inputBox.Text = Title;

                Label label = new Label();
                label.Size = new Size(inputBox.ClientSize.Width - 10, 23);
                label.Location = new Point(5, 5);
                label.Text = Prompt;
                inputBox.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Size = new Size(inputBox.ClientSize.Width - 10, 23);
                textBox.Location = new Point(5, label.Location.Y + label.Size.Height + 10);
                textBox.Text = DefaultResponse;
                inputBox.Controls.Add(textBox);

                Button okButton = new Button();
                okButton.DialogResult = DialogResult.OK;
                okButton.Name = "okButton";
                okButton.Size = new Size(75, 23);
                okButton.Text = "&OK";
                okButton.Location = new Point(inputBox.ClientSize.Width - 80 - 80, textBox.Location.Y + textBox.Size.Height + 10);
                inputBox.Controls.Add(okButton);

                Button cancelButton = new Button();
                cancelButton.DialogResult = DialogResult.Cancel;
                cancelButton.Name = "cancelButton";
                cancelButton.Size = okButton.Size;
                cancelButton.Text = "&Cancel";
                cancelButton.Location = new Point(inputBox.ClientSize.Width - 80, okButton.Location.Y);
                inputBox.Controls.Add(cancelButton);

                inputBox.ClientSize = new Size(inputBox.ClientSize.Width, cancelButton.Location.Y + cancelButton.Size.Height + 5);
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
