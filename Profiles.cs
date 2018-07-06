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
using System.Collections.Generic;
using System.Windows.Forms;

namespace HitCounterManager
{
    /// <summary>
    /// A row as part of a profile (equals a row at datagridview)
    /// </summary>
    [Serializable]
    public class ProfileRow
    {
        public string Title;
        public int Hits;
        public int Diff;
        public int PB;

        /// <summary>
        /// Creates an empty entry
        /// </summary>
        public ProfileRow()
        {
            Title = "";
            Hits = 0;
            Diff = 0;
            PB = 0;
        }

        /// <summary>
        /// Creates an entry based on comma separated string
        /// </summary>
        public ProfileRow(string Line)
        {
            int i;
            string val;
            int col = 0;

            Title = "";
            Hits = 0;
            Diff = 0;
            PB = 0;

            do {
                if (!Line.Contains(";")) break; // no more data in this lne
                i = Line.IndexOf(";");
                val = Line.Substring(0, i);
                switch (col)
                {
                    case 0: Title = val; break;
                    case 1: Int32.TryParse(val, out Hits); break;
                    case 2: Int32.TryParse(val, out Diff); break;
                    case 3: Int32.TryParse(val, out PB); break;
                }
                col++;
                Line = Line.Substring(i + 1);
            } while (true);
        }
    }

    /// <summary>
    /// Single profile (equals a whole datagridview data collection)
    /// </summary>
    [Serializable]
    public class Profile
    {
        public string Name;
        public int Attempts;
        public List<ProfileRow> Rows = new List<ProfileRow>();

        /// <summary>
        /// Add a new profile row
        /// </summary>
        public void AddNewRow(string Line)
        {
            Rows.Add(new ProfileRow(Line));
        }
    }

    /// <summary>
    /// Manages multiple profiles
    /// </summary>
    [Serializable]
    public class Profiles
    {
        private List<Profile> _Profiles = new List<Profile>();
        public List<Profile> ProfileList { get { return _Profiles; } } // used by XML serialization!

        /// <summary>
        /// Checks if a profile with given name exists and gets its instance
        /// </summary>
        private bool _FindProfile(string Name, out Profile profile)
        {
            foreach (Profile prof in _Profiles)
            {
                if (prof.Name == Name)
                {
                    profile = prof;
                    return true;
                }
            }
            profile = null;
            return false;
        }

        /// <summary>
        /// Builds a newline, pipe and comma separated string for all internally cached profiles
        /// </summary>
        public string GetProfilesString()
        {
            string ProfileStr = "";
            foreach (Profile prof in _Profiles)
            {
                ProfileStr += prof.Name + "|" + Environment.NewLine;
                foreach (ProfileRow row in prof.Rows)
                {
                    ProfileStr += row.Title + ";" + row.Hits + ";" + row.Diff + ";" + row.PB + ";" + Environment.NewLine;
                }
            }
            return ProfileStr;
        }

        /// <summary>
        /// Returns a list of all available internally cached profile names
        /// </summary>
        public object[] GetProfileList()
        {
            List<string> ret = new List<string>();
            foreach (Profile prof in _Profiles) ret.Add(prof.Name);
            return ret.ToArray();
        }

        /// <summary>
        /// Updates a datagrid based on a specific internally cached profile
        /// </summary>
        public void LoadProfileInto(string Name, ref DataGridView dgv, ref int Attempts)
        {
            Attempts = 0;
            dgv.Rows.Clear();
            foreach (Profile prof in _Profiles)
            {
                if (prof.Name == Name)
                {
                    Attempts = prof.Attempts;
                    foreach (ProfileRow row in prof.Rows)
                    {
                        object[] cells = { row.Title, row.Hits, row.Diff, row.PB, false };
                        dgv.Rows.Add(cells);
                    }
                    break;
                }
            }
            dgv.Rows[0].Cells["cSP"].Value = true;
        }

        /// <summary>
        /// Updates internal profile cache of a specific profile by reading data from datagrid
        /// </summary>
        public void SaveProfileFrom(string Name, DataGridView dgv, int Attempts, bool AllowCreation = false)
        {
            Profile prof_save;
            DataGridViewCellCollection cells;
            ProfileRow ProfileRow;

            // look for existing one and create if not exists
            if (!_FindProfile(Name, out prof_save))
            {
                if (!AllowCreation) return;

                prof_save = new Profile();
                prof_save.Name = Name;
                _Profiles.Add(prof_save);
            }

            prof_save.Rows.Clear();

            // collecting data, nom nom nom
            prof_save.Attempts = Attempts;
            for (int r = 0; r <= dgv.RowCount - 2; r++)
            {
                ProfileRow = new ProfileRow();
                cells = dgv.Rows[r].Cells;
                ProfileRow.Title = (string)cells["cTitle"].Value;
                ProfileRow.Hits = (int)cells["cHits"].Value;
                ProfileRow.Diff = (int)cells["cDiff"].Value;
                ProfileRow.PB = (int)cells["cPB"].Value;
                prof_save.Rows.Add(ProfileRow);
            }
        }

        /// <summary>
        /// Removes a specific profile from internal cache
        /// </summary>
        public void DeleteProfile(string Name)
        {
            Profile prof;
            if (_FindProfile(Name, out prof)) _Profiles.Remove(prof); 
        }

        /// <summary>
        /// Renames a specific profile in internal cache
        /// </summary>
        public void RenameProfile(string NameOld, string NameNew)
        {
            Profile prof;
            if (_FindProfile(NameOld, out prof)) prof.Name = NameNew;
        }
    }
}
