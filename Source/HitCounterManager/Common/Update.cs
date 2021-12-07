//MIT License

//Copyright (c) 2020-2021 Peter Kirmeier

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
using System.Net;
using System.Reflection;
using TinyJson;
using Xamarin.Forms;

namespace HitCounterManager.Common
{
    public static class GitHubUpdate
    {
        private static List<Dictionary<string, object>> Releases = null;

        /// <summary>
        /// Opens the default project website on GitHub
        /// </summary>
        public static void WebOpenLandingPage()
        {
#pragma warning disable CS0618 // Ignore deprecated (but without replacement, sure it is Launcher.OpenAsync in Xamarin.Essentials but requires additianal references
            Device.OpenUri(new Uri("https://github.com/topeterk/HitCounterManager"));
#pragma warning restore CS0618
        }

        /// <summary>
        /// Opens the website on GitHub of the latest release version
        /// </summary>
        public static void WebOpenLatestRelease()
        {
#pragma warning disable CS0618 // Ignore deprecated (but without replacement, sure it is Launcher.OpenAsync in Xamarin.Essentials but requires additianal references
            Device.OpenUri(new Uri("https://github.com/topeterk/HitCounterManager/releases/latest"));
#pragma warning restore CS0618
        }

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
                client.Headers.Add("User-Agent", "HitCounterManager/" + Statics.ApplicationVersionString);
                // https://developer.github.com/v3/media/#request-specific-version
                client.Headers.Add("Accept", "application/vnd.github.v3.text+json");
                // https://developer.github.com/v3/repos/releases/#get-a-single-release
                string response = client.DownloadString("http://api.github.com/repos/topeterk/HitCounterManager/releases");

                Releases = response.FromJson<List<Dictionary<string, object>>>();

                // Only keep newer releases of own major version
                int i = Releases.Count;
                string MajorVersionString = Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + ".";
                while (0 < i--)
                {
                    string tag_name = Releases[i]["tag_name"].ToString();
                    if (!tag_name.StartsWith(MajorVersionString)) Releases.RemoveAt(i);
                }

                result = true;
            }
            catch (Exception) { };

            return result;
        }

        /// <summary>
        /// Returns the latest release version name
        /// </summary>
        public static string LatestVersionName
        {
            get
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
        }

        /// <summary>
        /// Returns the change log from current version up to the latest release version
        /// </summary>
        public static string Changelog
        {
            get
            {
                string result;
                string errorstr = "An error occurred during update check!" + Environment.NewLine + Environment.NewLine;

                if (Releases == null) return errorstr + "Could not receive changelog!";

                if (LatestVersionName == Statics.ApplicationVersionString)
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
                            if (release["tag_name"].ToString() == Statics.ApplicationVersionString) break; // stop on current version

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
        }
    }
}
