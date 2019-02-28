//MIT License

//Copyright(c) 2016-2019 Peter Kirmeier

//Permission Is hereby granted, free Of charge, to any person obtaining a copy
//of this software And associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, And/Or sell
//copies of the Software, And to permit persons to whom the Software Is
//furnished to do so, subject to the following conditions:

//The above copyright notice And this permission notice shall be included In all
//copies Or substantial portions of the Software.

//THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or
//IMPLIED, INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE And NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS Or COPYRIGHT HOLDERS BE LIABLE For ANY CLAIM, DAMAGES Or OTHER
//LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or OTHERWISE, ARISING FROM,
//OUT OF Or IN CONNECTION WITH THE SOFTWARE Or THE USE Or OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using System.IO;

namespace HitCounterManager
{
    /// <summary>
    /// Reads a file, patches it and writes to a new file
    /// </summary>
    public class OutModule
    {
        private string _FilePathIn;
        private string template = "";
        private IProfileInfo pi;

        public bool DataUpdatePending = false;
        public string FilePathOut = null;
        public bool ShowAttemptsCounter = true;
        public bool ShowHeadline = true;
        public bool ShowSessionProgress = true;
        public int ShowSplitsCountFinished = 999;
        public int ShowSplitsCountUpcoming = 999;
        public bool StyleUseHighContrast = false;
        public bool StyleUseCustom = false;
        public string StyleCssUrl = "";
        public string StyleFontUrl = "";
        public string StyleFontName = "";
        public int StyleDesiredWidth = 0;

        /// <summary>
        /// Bind object to a data grid
        /// </summary>
        /// <param name="dgv">object to set binding</param>
        public OutModule(IProfileInfo ProfileInfo)
        {
            pi = ProfileInfo;
        }

        /// <summary>
        /// Read a file into buffer
        /// </summary>
        public string FilePathIn
        {
            get { return _FilePathIn; }
            set
            {
                _FilePathIn = value;

                if (File.Exists(_FilePathIn))
                {
                    StreamReader sr = new StreamReader(_FilePathIn);
                    template = sr.ReadToEnd();
                    sr.Close();
                }
            }
        }

        /// <summary>
        /// Escapes special HTML characters
        /// </summary>
        /// <param name="Str">String with special characters</param>
        /// <returns>String with HTML encoded special character</returns>
        public string SimpleHtmlEscape(string Str)
        {
            if (null != Str)
            {
                Str = Str.ToString().Replace("&", "&amp;").Replace(" ", "&nbsp;");
                // Keep for compatibility to support designs up to version 1.15 that were not using Unicode:
                Str = Str.Replace("ä", "&auml;").Replace("ö", "&ouml;").Replace("ü", "&uuml;");
                Str = Str.Replace("Ä", "&Auml;").Replace("Ö", "&Ouml;").Replace("Ü", "&Uuml;");
            }
            return Str;
        }

        /// <summary>
        /// Writes a JSON statement to assign a simple value
        /// </summary>
        private void WriteJsonSimpleValue(StreamWriter File, string Name, bool Bool)
        {
            File.WriteLine("\"" + Name + "\": " + (Bool ? "true" : "false") + ",");
        }
        /// <summary>
        /// Writes a JSON statement to assign a simple value
        /// </summary>
        private void WriteJsonSimpleValue(StreamWriter File, string Name, int Integer)
        {
            File.WriteLine("\"" + Name + "\": " + Integer.ToString() + ",");
        }
        /// <summary>
        /// Writes a JSON statement to assign a simple value
        /// </summary>
        private void WriteJsonSimpleValue(StreamWriter File, string Name, string String)
        {
            File.WriteLine("\"" + Name + "\": " + (null != String ? "\"" + String + "\"" : "undefined") + ",");
        }

        /// <summary>
        /// Use buffer to create outputfile while patching some data
        /// </summary>
        /// <param name="Force">true = Force producing output, false = Output only on pending changes</param>
        public void Update(bool Force = false)
        {
            if (DataUpdatePending) return; // Data is incomplete, wait till data is no longer dirty

            if (!Force && !pi.HasChanged(true)) return; // Prevent writing the same idendical output multiple times
  
            //Console.Beep(); // For debugging to check whenever output is beeing generated :)

            StreamWriter sr;
            bool IsWritingList = false; // Kept for old designs before version 1.10
            bool IsWritingJson = false;

            if (null == FilePathOut) return;

            try
            {
                sr = new StreamWriter(FilePathOut, false, System.Text.Encoding.Unicode); // UTF16LE
            }
            catch { return; }

            sr.NewLine = Environment.NewLine;

            foreach (string line in template.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (IsWritingJson)
                {
                    if (line.Contains("HITCOUNTER_JSON_END")) IsWritingJson = false;
                }
                else if (IsWritingJson)
                {
                    if (line.Contains("HITCOUNTER_LIST_END")) IsWritingList = false;
                }
                else if (line.Contains("HITCOUNTER_JSON_START")) // Format data according to RFC 4627 (JSON)
                {
                    int active = pi.GetActiveSplit();
                    int iTemp;

                    sr.WriteLine("{");

                    sr.WriteLine("\"list\": [");
                    for (int r = 0; r < pi.GetSplitCount(); r++)
                    {
                        if (r != 0) sr.WriteLine(","); // separator
                        sr.Write("[\"" + SimpleHtmlEscape(pi.GetSplitTitle(r)) + "\", " + pi.GetSplitHits(r) + ", " + pi.GetSplitPB(r) + "]");
                    }
                    sr.WriteLine(""); // no trailing separator
                    sr.WriteLine("],");

                    WriteJsonSimpleValue(sr, "session_progress", pi.GetSessionProgress());

                    WriteJsonSimpleValue(sr, "split_active", active);
                    iTemp = active - ShowSplitsCountFinished;
                    WriteJsonSimpleValue(sr, "split_first", (iTemp < 0 ? 0 : iTemp));
                    iTemp = active + ShowSplitsCountUpcoming;
                    WriteJsonSimpleValue(sr, "split_last", (999 < iTemp ? 999 : iTemp));

                    WriteJsonSimpleValue(sr, "attempts", pi.GetAttemptsCount());
                    WriteJsonSimpleValue(sr, "show_attempts", ShowAttemptsCounter);
                    WriteJsonSimpleValue(sr, "show_headline", ShowHeadline);
                    WriteJsonSimpleValue(sr, "show_session_progress", ShowSessionProgress);

                    WriteJsonSimpleValue(sr, "font_name", (StyleUseCustom ? StyleFontName : null));
                    WriteJsonSimpleValue(sr, "font_url", (StyleUseCustom ? StyleFontUrl : ""));
                    WriteJsonSimpleValue(sr, "css_url", (StyleUseCustom ? StyleCssUrl : "stylesheet.css"));
                    WriteJsonSimpleValue(sr, "high_contrast", StyleUseHighContrast);
                    WriteJsonSimpleValue(sr, "width", StyleDesiredWidth);

                    sr.WriteLine("}");

                    IsWritingJson = true;
                }
                else if (line.Contains("HITCOUNTER_LIST_START")) // Kept for old designs before version 1.10
                {
                    int active = pi.GetActiveSplit();

                    for (int r = 0; r < pi.GetSplitCount(); r++)
                    {
                        sr.Write("[\"" + SimpleHtmlEscape(pi.GetSplitTitle(r)) + "\", " + pi.GetSplitHits(r) + ", " + pi.GetSplitPB(r) + ", " + (r == active ? "1" : "0") + "]");
                    }

                    IsWritingList = true;
                }
                else if ((!IsWritingList) && (!IsWritingJson))
                {
                    sr.WriteLine(line.Replace(Environment.NewLine, ""));
                }
            }

            sr.Close();
        }
    }
}
