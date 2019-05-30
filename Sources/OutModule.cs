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
using System.IO;

namespace HitCounterManager
{
    /// <summary>
    /// Reads a file, patches it and writes to a new file
    /// </summary>
    public class OutModule
    {
        public enum OM_Purpose {
            OM_Purpose_SplitCounter = 0,
            OM_Purpose_DeathCounter = 1,
            OM_Purpose_Checklist = 2,
            OM_Purpose_MAX = 3
        };
        public enum OM_Severity {
            OM_Severity_AnyHitsCritical = 0,
            OM_Severity_BossHitCritical = 1,
            OM_Severity_ComparePB = 2,
            OM_Severity_MAX = 3
        };

        private string template = "";
        private string _FilePathIn;
        public string FilePathOut = null;

        public bool ShowAttemptsCounter = true;
        public bool ShowHeadline = true;
        public bool ShowSessionProgress = true;
        public int ShowSplitsCountFinished = 999;
        public int ShowSplitsCountUpcoming = 999;
        public bool ShowHitsCombined = true;
        public bool ShowNumbers = true;
        public bool ShowPB = true;
        public OM_Purpose Purpose = OM_Purpose.OM_Purpose_SplitCounter;
        public OM_Severity Severity = OM_Severity.OM_Severity_AnyHitsCritical;

        public bool StyleUseHighContrast = false;
        public bool StyleUseHighContrastNames = false;
        public bool StyleUseCustom = false;
        public string StyleCssUrl = "";
        public string StyleFontUrl = "";
        public string StyleFontName = "";
        public int StyleDesiredWidth = 0;

        private IProfileInfo pi;
        public bool DataUpdatePending = false;

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
                    int iSplitCount = pi.GetSplitCount();
                    int iSplitFirst;
                    int iSplitLast;

                    sr.WriteLine("{");

                    sr.WriteLine("\"list\": [");
                    for (int r = 0; r < iSplitCount; r++)
                    {
                        if (r != 0) sr.WriteLine(","); // separator
                        sr.Write("[\"" + SimpleHtmlEscape(pi.GetSplitTitle(r)) + "\", " + (pi.GetSplitHits(r) + pi.GetSplitWayHits(r)) + ", " + pi.GetSplitPB(r) + ", " + pi.GetSplitWayHits(r) + "]");
                    }
                    sr.WriteLine(""); // no trailing separator
                    sr.WriteLine("],");

                    WriteJsonSimpleValue(sr, "session_progress", pi.GetSessionProgress());

                    // Calculation to show same amount of splits independent from active split:
                    // Example: ShowSplitsCountFinished = 3 , ShowSplitsCountUpcoming = 2 , iSplitCount = 7 (0-6)
                    //  A:  B:  C:  D:  E:  F:  G:
                    // <0>  0   0   0
                    //  1  <1>  1   1   1   1   1
                    //  2   2  <2>  2   2   2   2
                    //  3   3   3  <3>  3   3   3
                    //  4   4   4   4  <4>  4   4
                    //  5   5   5   5   5  <5>  5
                    //                  6   6  <6>

                    if (active < ShowSplitsCountFinished) // A-C: less previous, more upcoming
                    {
                        iSplitFirst = 0;
                        iSplitLast = ShowSplitsCountUpcoming + ShowSplitsCountFinished;
                    }
                    else if (iSplitCount - active > ShowSplitsCountUpcoming) // D-E: previous and upcoming as it is
                    {
                        iSplitFirst = active - ShowSplitsCountFinished;
                        iSplitLast = active + ShowSplitsCountUpcoming;
                    }
                    else // F-G: more previous, less upcoming
                    {
                        iSplitFirst = iSplitCount - 1 - ShowSplitsCountUpcoming - ShowSplitsCountFinished;
                        iSplitLast = iSplitCount - 1;
                    }
                    WriteJsonSimpleValue(sr, "split_active", active);
                    WriteJsonSimpleValue(sr, "split_first", (iSplitFirst < 0 ? 0 : iSplitFirst));
                    WriteJsonSimpleValue(sr, "split_last", (iSplitCount <= iSplitLast ? iSplitCount-1 : iSplitLast));

                    WriteJsonSimpleValue(sr, "attempts", pi.GetAttemptsCount());
                    WriteJsonSimpleValue(sr, "show_attempts", ShowAttemptsCounter);
                    WriteJsonSimpleValue(sr, "show_headline", ShowHeadline);
                    WriteJsonSimpleValue(sr, "show_session_progress", ShowSessionProgress);
                    WriteJsonSimpleValue(sr, "show_hitscombined", ShowHitsCombined);
                    WriteJsonSimpleValue(sr, "show_numbers", ShowNumbers);
                    WriteJsonSimpleValue(sr, "show_pb", ShowPB);
                    WriteJsonSimpleValue(sr, "purpose", (int)Purpose);
                    WriteJsonSimpleValue(sr, "severity", (int)Severity);

                    WriteJsonSimpleValue(sr, "font_name", (StyleUseCustom ? StyleFontName : null));
                    WriteJsonSimpleValue(sr, "font_url", (StyleUseCustom ? StyleFontUrl : ""));
                    WriteJsonSimpleValue(sr, "css_url", (StyleUseCustom ? StyleCssUrl : "stylesheet.css"));
                    WriteJsonSimpleValue(sr, "high_contrast", StyleUseHighContrast);
                    WriteJsonSimpleValue(sr, "high_contrast_names", StyleUseHighContrastNames);
                    WriteJsonSimpleValue(sr, "width", StyleDesiredWidth);

                    sr.WriteLine("}");

                    IsWritingJson = true;
                }
                else if (line.Contains("HITCOUNTER_LIST_START")) // Kept for old designs before version 1.10
                {
                    int active = pi.GetActiveSplit();

                    for (int r = 0; r < pi.GetSplitCount(); r++)
                    {
                        sr.Write("[\"" + SimpleHtmlEscape(pi.GetSplitTitle(r)) + "\", " + pi.GetSplitHits(r) + pi.GetSplitWayHits(r) + ", " + pi.GetSplitPB(r) + ", " + (r == active ? "1" : "0") + "]");
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
