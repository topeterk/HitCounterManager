//MIT License

//Copyright (c) 2020-2020 Peter Kirmeier

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
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using TinyJson;

namespace HitCounterManager
{
    public static class GitHubUpdate
    {
        private static List<Dictionary<string, object>> Releases = null;

        /// <summary>
        /// Opens the default project website on GitHub
        /// </summary>
        public static void WebOpenLandingPage() { System.Diagnostics.Process.Start("https://github.com/topeterk/HitCounterManager"); }

        /// <summary>
        /// Opens the website on GitHub of the latest release version
        /// </summary>
        public static void WebOpenLatestRelease() { System.Diagnostics.Process.Start("https://github.com/topeterk/HitCounterManager/releases/latest"); }

        /// <summary>
        /// Updates the information about all available releases
        /// </summary>
        /// <returns>Success state</returns>
        public static bool QueryAllReleases()
        {
            bool result = false;

            try
            {
                WebClient client = new WebClient();
                client.Encoding = System.Text.Encoding.UTF8;

                // https://developer.github.com/v3/#user-agent-required
                client.Headers.Add("User-Agent", "HitCounterManager/" + Application.ProductVersion.ToString());
                // https://developer.github.com/v3/media/#request-specific-version
                client.Headers.Add("Accept", "application/vnd.github.v3.text+json");
                // https://developer.github.com/v3/repos/releases/#get-a-single-release
                string response = client.DownloadString("http://api.github.com/repos/topeterk/HitCounterManager/releases");

                Releases = response.FromJson<List<Dictionary<string, object>>>();
                result = true;
            }
            catch (Exception) { };

            return result;
        }

        /// <summary>
        /// Returns the latest release version name
        /// </summary>
        /// <returns>Version name</returns>
        public static string GetLatestVersionName()
        {
            string result = "Unknown";

            if (Releases == null) return result;

            try
            {
                result = Releases[0]["tag_name"].ToString(); // 0 = latest
            }
            catch (Exception) { };

            return result;
        }
        /// <summary>
        /// Returns the change log from current version up to the latest release version
        /// </summary>
        /// <returns>Change log summary or error text</returns>
        public static string GetChangelog()
        {
            string result;
            string errorstr = "An error occurred during update check!" + Environment.NewLine + Environment.NewLine;

            if (Releases == null) return errorstr + "Could not receive changelog!";

            if (GetLatestVersionName() == Application.ProductVersion.ToString())
            {
                result = "Up to date!";
            }
            else
            {
                try
                {
                    Dictionary<string, object> release;
                    int i;

                    result = "";

                    for (i = 0; i < Releases.Count; i++)
                    {
                        release = Releases[i];
                        if (release["tag_name"].ToString() == Application.ProductVersion.ToString()) break; // stop on current version

                        result += "----------------------------------------------------------------------------------------------------------------------------------------------------------------"
                            + Environment.NewLine
                            + release["name"].ToString()
                            + Environment.NewLine + Environment.NewLine
                            + release["body_text"].ToString().Replace("\n\n", Environment.NewLine)
                            + Environment.NewLine + Environment.NewLine;
                    }

                    result = i.ToString() + " new version" + (i == 1 ? "" : "s") + " available:" + Environment.NewLine + Environment.NewLine + result;

                    result = result.Replace("\n", Environment.NewLine);
                }
                catch (Exception ex)
                {
                    result = errorstr + ex.Message.ToString();
                }
            }

            return result;
        }

        /// <summary>
        /// Creates a window to show changelog of new available versions
        /// </summary>
        /// <param name="LatestVersionTitle">Name of latest release</param>
        /// <param name="Changelog">Pathnotes</param>
        /// <returns>OK = OK, Yes = Website, else = Cancel</returns>
        public static DialogResult NewVersionDialog(Form ParentWindow)
        {
            const int ClientPad = 15;
            Form frm = new Form();
            
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.FormBorderStyle = FormBorderStyle.FixedDialog;
            frm.Icon = ParentWindow.Icon;
            frm.ShowInTaskbar = false;
            frm.FormBorderStyle = FormBorderStyle.Sizable;
            frm.MaximizeBox = true;
            frm.MinimizeBox = false;
            frm.ClientSize = new Size(600, 400);
            frm.MinimumSize = frm.ClientSize;
            frm.Text = "New version available!";

            Label label = new Label();
            label.Size = new Size(frm.ClientSize.Width - ClientPad, 20);
            label.Location = new Point(ClientPad, ClientPad);
            label.Text = "Latest available version:      " + GetLatestVersionName();
            frm.Controls.Add(label);

            Button okButton = new Button();
            okButton.DialogResult = DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Location = new Point(frm.ClientSize.Width - okButton.Size.Width - ClientPad, frm.ClientSize.Height - okButton.Size.Height - ClientPad);
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.Text = "&OK";
            frm.Controls.Add(okButton);

            Button wwwButton = new Button();
            wwwButton.DialogResult = DialogResult.Yes;
            wwwButton.Name = "wwwButton";
            wwwButton.Location = new Point(frm.ClientSize.Width - wwwButton.Size.Width- ClientPad - okButton.Size.Width - ClientPad, frm.ClientSize.Height - wwwButton.Size.Height - ClientPad);
            wwwButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            wwwButton.Text = "&Go to download page";
            wwwButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            wwwButton.AutoSize = true;
            frm.Controls.Add(wwwButton);

            TextBox textBox = new TextBox();
            textBox.Location = new Point(ClientPad, label.Location.Y + label.Size.Height + 5);
            textBox.Size = new Size(frm.ClientSize.Width - ClientPad*2, okButton.Location.Y - ClientPad - textBox.Location.Y);
            textBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox.Multiline = true;
            textBox.Text = GetChangelog();
            textBox.ReadOnly = true;
            textBox.ScrollBars = ScrollBars.Both;
            textBox.BackColor = Color.FromKnownColor(KnownColor.Window);
            textBox.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            frm.Controls.Add(textBox);

            frm.AcceptButton = okButton;
            frm.UpdateDarkMode();
            return frm.ShowDialog(ParentWindow);
        }
    }
}
