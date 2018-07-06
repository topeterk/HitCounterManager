//MIT License

//Copyright(c) 2016-2018 Peter Kirmeier

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
using System.Windows.Forms;

namespace HitCounterManager
{
    /// <summary>
    /// Reads a file, patches it and writes to a new file
    /// </summary>
    public class OutModule
    {
        private string _FilePathIn;
        private string template = "";
        private DataGridView dgv;

        public string FilePathOut = null;
        public int AttemptsCount = 0;
        public bool ShowAttemptsCounter = true;
        public bool ShowHeadline = true;
        public bool ShowSessionProgress = true;
        public int ShowSplitsCountFinished = 999;
        public int ShowSplitsCountUpcoming = 999;
        public bool StyleUseHighContrast = false;
        public bool StyleUseCustom = false;
        public string StyleCssUrl = "";
        public string StyleFontUrl = "";
        public int StyleDesiredWidth = 0;

        /// <summary>
        /// Bind object to a data grid
        /// </summary>
        /// <param name="dgv">object to set binding</param>
        public OutModule(DataGridView DataGridView)
        {
            dgv = DataGridView;
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
            if (null == Str)
            {
                Str = Str.ToString().Replace("&", "&amp;").Replace(" ", "&nbsp;");
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
            File.WriteLine("\"" + Name + "\": \"" + String + "\",");
        }

        /// <summary>
        /// Use buffer to create outputfile while patching some data
        /// </summary>
        public void Update()
        {
            StreamWriter sr;
            bool IsWritingList = false; // Kept for old designs before version 1.10
            bool IsWritingJson = false;

            if (null == FilePathOut) return;

            try
            {
                if (File.Exists(FilePathOut)) File.Create(FilePathOut).Close();
                sr = new StreamWriter(FilePathOut);
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
                    DataGridViewCellCollection cells;
                    string title;
                    int diff;
                    int hits;
                    int PB;
                    int active = 0;
                    int session_progress = 0;
                    int iTemp;

                    sr.WriteLine("{");

                    sr.WriteLine("\"list\": [");
                    for (int r = 0; r <= dgv.RowCount - 2; r++)
                    {
                        cells = dgv.Rows[r].Cells;
                        title = SimpleHtmlEscape((string)cells["cTitle"].Value);
                        hits = (int)cells["cHits"].Value;
                        diff = (int)cells["cDiff"].Value;
                        PB = (int)cells["cPB"].Value;
                        if ((bool)(cells["cSP"].Value)) session_progress = r;
                        if (r == dgv.SelectedCells[0].RowIndex) active = r;
                        if (r != 0) sr.WriteLine(","); // separator
                        sr.Write("[\"" + title + "\", " + hits + ", " + PB + "]");
                    }
                    sr.WriteLine(""); // no trailing separator
                    sr.WriteLine("],");

                    WriteJsonSimpleValue(sr, "session_progress", session_progress);

                    WriteJsonSimpleValue(sr, "split_active", active);
                    iTemp = active - ShowSplitsCountFinished;
                    WriteJsonSimpleValue(sr, "split_first", (iTemp < 0 ? 0 : iTemp));
                    iTemp = active + ShowSplitsCountUpcoming;
                    WriteJsonSimpleValue(sr, "split_last", (999 < iTemp ? 999 : iTemp));

                    WriteJsonSimpleValue(sr, "attempts", AttemptsCount);
                    WriteJsonSimpleValue(sr, "show_attempts", ShowAttemptsCounter);
                    WriteJsonSimpleValue(sr, "show_headline", ShowHeadline);
                    WriteJsonSimpleValue(sr, "show_session_progress", ShowSessionProgress);
                    
                    WriteJsonSimpleValue(sr, "font_url", (StyleUseCustom ? StyleFontUrl : ""));
                    WriteJsonSimpleValue(sr, "css_url", (StyleUseCustom ? StyleCssUrl : "stylesheet.css"));
                    WriteJsonSimpleValue(sr, "high_contrast", StyleUseHighContrast);
                    WriteJsonSimpleValue(sr, "width", StyleDesiredWidth);

                    sr.WriteLine("}");

                    IsWritingJson = true;
                }
                else if (line.Contains("HITCOUNTER_LIST_START")) // Kept for old designs before version 1.10
                {
                    DataGridViewCellCollection cells;
                    string title;
                    int diff;
                    int hits;
                    int PB;
                    int active;

                    for (int r = 0; r <= dgv.RowCount - 2; r++)
                    {
                        cells = dgv.Rows[r].Cells;
                        title = SimpleHtmlEscape((string)cells["cTitle"].Value);
                        hits = (int)cells["cHits"].Value;
                        diff = (int)cells["cDiff"].Value;
                        PB = (int)cells["cPB"].Value;
                        if (r == dgv.SelectedCells[0].RowIndex) active = r; else active = 0;
                        sr.Write("[\"" + title + "\", " + hits + ", " + PB + ", " + active + "]");
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
