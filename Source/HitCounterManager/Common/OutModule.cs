//MIT License

//Copyright (c) 2016-2025 Peter Kirmeier

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
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace HitCounterManager.Models
{
    /// <summary>
    /// Reads a file, patches it and writes to a new file
    /// </summary>
    public class OutModule(SettingsRoot settings)
    {
        #region Settings

        public enum OM_Purpose {
            OM_Purpose_SplitCounter = 0,
            OM_Purpose_DeathCounter = 1,
            OM_Purpose_Checklist = 2,
            OM_Purpose_NoDeath = 3,
            OM_Purpose_ResetCounter = 4,
            OM_Purpose_MAX = 5
        };
        public enum OM_Severity {
            OM_Severity_AnyHitsCritical = 0,
            OM_Severity_BossHitCritical = 1,
            OM_Severity_ComparePB = 2,
            OM_Severity_MAX = 3
        };

        public OM_Purpose Purpose
        {
            get { return Settings.Purpose < (int)OM_Purpose.OM_Purpose_MAX ? (OM_Purpose)Settings.Purpose : OM_Purpose.OM_Purpose_SplitCounter; }
            set { Settings.Purpose = (int)value; }
        }
        public OM_Severity Severity
        {
            get { return Settings.Severity < (int)OM_Severity.OM_Severity_MAX ? (OM_Severity)Settings.Severity : OM_Severity.OM_Severity_AnyHitsCritical; }
            set { Settings.Severity = (int)value; }
        }

        private string? template = null;
        private SettingsRoot Settings { get; init; } = settings;

        #endregion

        /// <summary>
        /// Refreshes the file handles.
        /// Call when Settings.Inputfile changes!
        /// </summary>
        [MemberNotNullWhen(true, nameof(template))]
        public bool ReloadTemplate()
        {
            // Reload input file handle when possible
            if (!File.Exists(Settings.Inputfile))
                return false;

            StreamReader sr = new (Settings.Inputfile);
            template = sr.ReadToEnd();
            sr.Close();
            return true;
        }

        #region JSON helpers

        /// <summary>
        /// Escapes special JSON characters
        /// </summary>
        /// <param name="Str">String with special characters</param>
        /// <returns>String with JSON encoded special characters</returns>
        static public string? SimpleJsonEscape(string? Str)
        {
            if (!string.IsNullOrEmpty(Str))
            {
                Str = Str.Replace("\\", "\\\\").Replace("\"", "\\\"");
            }
            return Str;
        }

        /// <summary>
        /// Writes a JSON statement to assign a simple value
        /// </summary>
        static private void WriteJsonSimpleValue(StreamWriter File, string Name, bool Bool)
        {
            File.WriteLine("\"" + Name + "\": " + (Bool ? "true" : "false") + ",");
        }
        /// <summary>
        /// Writes a JSON statement to assign a simple value
        /// </summary>
        static private void WriteJsonSimpleValue(StreamWriter File, string Name, long Integer)
        {
            File.WriteLine("\"" + Name + "\": " + Integer.ToString() + ",");
        }
        /// <summary>
        /// Writes a JSON statement to assign a simple value
        /// </summary>
        static private void WriteJsonSimpleValue(StreamWriter File, string Name, string? String)
        {
            File.WriteLine("\"" + Name + "\": " + (null != String ? "\"" + SimpleJsonEscape(String) + "\"" : "undefined") + ",");
        }

        #endregion

        /// <summary>
        /// Use buffer to create outputfile while patching some data
        /// </summary>
        public void Update(ProfileModel pi, bool TimerRunning)
        {
            //Console.Beep(); // For debugging to check whenever output is beeing generated :)

            if (null == Settings.OutputFile) return;
            if (null == template) // no valid template read yet?
            {
                if (!ReloadTemplate()) return; // try to read template again. on error, avoid writing empty output file
            }

            StreamWriter sr;
            bool IsWritingList = false; // Kept for old designs before version 1.10
            bool IsWritingJson = false;
            try
            {
                sr = new StreamWriter(Settings.OutputFile, false, System.Text.Encoding.Unicode); // UTF16LE
            }
            catch { return; }
            sr.NewLine = Environment.NewLine;

            foreach (string line in template.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries))
            {
                if (IsWritingJson)
                {
                    if (line.Contains("HITCOUNTER_JSON_END")) IsWritingJson = false;
                }
                else if (line.Contains("HITCOUNTER_JSON_START")) // Format data according to RFC 4627 (JSON)
                {
                    int active = pi.ActiveSplit;
                    int iSplitCount = pi.Rows.Count;
                    int iSplitFirst;
                    int iSplitLast;
                    int RunIndex = 1;
                    int RunIndexActive;

                    sr.WriteLine("{");

                    WriteJsonSimpleValue(sr, "profile_name", pi.Name);

                    sr.WriteLine("\"list\": [");

                    // ----- Splits of the current run:
                    RunIndexActive = RunIndex;
                    for (int r = 0; r < iSplitCount; r++)
                    {
                        // Dump all actually visible splits of the current run
                        if (0 < r) sr.WriteLine(","); // separator
                        sr.Write("[\"" + SimpleJsonEscape(pi.Rows[r].Title) + "\", "
                            + (pi.Rows[r].Hits + pi.Rows[r].WayHits) + ", " + pi.Rows[r].PB + ", " + pi.Rows[r].WayHits + ", "
                            + RunIndex + ", " + pi.Rows[r].Duration + ", " + pi.Rows[r].DurationPB + ", " + pi.Rows[r].DurationGold + "]");
                    }

                    // ----- Splits of the upcoming runs:
                    RunIndex++;
                    sr.WriteLine(""); // no trailing separator
                    sr.WriteLine("],");

                    WriteJsonSimpleValue(sr, "session_progress", pi.SessionProgress);
                    WriteJsonSimpleValue(sr, "best_progress", pi.BestProgress);

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
                    if (active < Settings.ShowSplitsCountFinished) // A-C: less previous, more upcoming
                    {
                        iSplitFirst = 0;
                        iSplitLast = Settings.ShowSplitsCountUpcoming + Settings.ShowSplitsCountFinished;
                    }
                    else if (iSplitCount - active > Settings.ShowSplitsCountUpcoming) // D-E: previous and upcoming as it is
                    {
                        iSplitFirst = active - Settings.ShowSplitsCountFinished;
                        iSplitLast = active + Settings.ShowSplitsCountUpcoming;
                    }
                    else // F-G: more previous, less upcoming
                    {
                        iSplitFirst = iSplitCount - 1 - Settings.ShowSplitsCountUpcoming - Settings.ShowSplitsCountFinished;
                        iSplitLast = iSplitCount - 1;
                    }

                    // safety limiters
                    if (iSplitFirst < 0) iSplitFirst = 0;
                    if (iSplitCount <= iSplitLast) iSplitLast =  iSplitCount-1;

                    WriteJsonSimpleValue(sr, "run_active", RunIndexActive);
                    WriteJsonSimpleValue(sr, "split_active", active);
                    WriteJsonSimpleValue(sr, "split_first", iSplitFirst);
                    WriteJsonSimpleValue(sr, "split_last", iSplitLast);

                    WriteJsonSimpleValue(sr, "attempts", pi.Attempts);
                    WriteJsonSimpleValue(sr, "show_attempts", Settings.ShowAttemptsCounter);
                    WriteJsonSimpleValue(sr, "show_headline", Settings.ShowHeadline);
                    WriteJsonSimpleValue(sr, "show_footer", Settings.ShowFooter);
                    WriteJsonSimpleValue(sr, "show_session_progress", Settings.ShowSessionProgress);
                    WriteJsonSimpleValue(sr, "show_progress_bar", Settings.ShowProgressBar);
                    WriteJsonSimpleValue(sr, "show_hits", Settings.ShowHits);
                    WriteJsonSimpleValue(sr, "show_hitscombined", Settings.ShowHitsCombined);
                    WriteJsonSimpleValue(sr, "show_numbers", Settings.ShowNumbers);
                    WriteJsonSimpleValue(sr, "show_pb", Settings.ShowPB);
                    WriteJsonSimpleValue(sr, "show_pb_totals", Settings.ShowPBTotals);
                    WriteJsonSimpleValue(sr, "show_diff", Settings.ShowDiff);
                    WriteJsonSimpleValue(sr, "show_time", Settings.ShowTimeCurrent);
                    WriteJsonSimpleValue(sr, "show_time_diff", Settings.ShowTimeDiff);
                    WriteJsonSimpleValue(sr, "show_time_pb", Settings.ShowTimePB);
                    WriteJsonSimpleValue(sr, "show_time_footer", Settings.ShowTimeFooter);
                    WriteJsonSimpleValue(sr, "show_duration", Settings.ShowDurationCurrent);
                    WriteJsonSimpleValue(sr, "show_duration_diff", Settings.ShowDurationDiff);
                    WriteJsonSimpleValue(sr, "show_duration_pb", Settings.ShowDurationPB);
                    WriteJsonSimpleValue(sr, "show_duration_gold", Settings.ShowDurationGold);
                    WriteJsonSimpleValue(sr, "purpose", (int)Purpose);
                    WriteJsonSimpleValue(sr, "severity", (int)Severity);

                    WriteJsonSimpleValue(sr, "font_name", (Settings.StyleUseCustom ? Settings.StyleFontName : null));
                    WriteJsonSimpleValue(sr, "font_url", (Settings.StyleUseCustom ? Settings.StyleFontUrl : ""));
                    WriteJsonSimpleValue(sr, "css_url", (Settings.StyleUseCustom ? Settings.StyleCssUrl : "stylesheet.css"));
                    WriteJsonSimpleValue(sr, "high_contrast", Settings.StyleUseHighContrast);
                    WriteJsonSimpleValue(sr, "high_contrast_names", Settings.StyleUseHighContrastNames);
                    WriteJsonSimpleValue(sr, "use_roman", Settings.StyleUseRoman);
                    WriteJsonSimpleValue(sr, "highlight_active_split", Settings.StyleHightlightCurrentSplit);
                    WriteJsonSimpleValue(sr, "progress_bar_colored", Settings.StyleProgressBarColored);
                    WriteJsonSimpleValue(sr, "height", Settings.StyleDesiredHeight);
                    WriteJsonSimpleValue(sr, "width", Settings.StyleDesiredWidth);
                    if (!string.IsNullOrEmpty(Settings.StyleTableAlignment)) WriteJsonSimpleValue(sr, "tblalign", Settings.StyleTableAlignment);
                    WriteJsonSimpleValue(sr, "subPB", Settings.StyleSubscriptPB);
                    
                    WriteJsonSimpleValue(sr, "update_time", (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
                    WriteJsonSimpleValue(sr, "timer_paused", !TimerRunning);

                    sr.WriteLine("}");

                    IsWritingJson = true;
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
