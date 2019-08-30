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
using System.Collections.Generic;

namespace HitCounterManager
{
    /// <summary>
    /// A row as part of a profile (used for a row at datagridview)
    /// </summary>
    [Serializable]
    public class ProfileRow
    {
        public string Title = "";
        public int Hits = 0;
        public int WayHits = 0;
        public int PB = 0;
    }

    /// <summary>
    /// Single profile (used for a whole datagridview data collection)
    /// </summary>
    [Serializable]
    public class Profile
    {
        public string Name;
        public int Attempts;
        public int ActiveSplit;
        public List<ProfileRow> Rows = new List<ProfileRow>();
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
        /// Returns a list of all available internally cached profile names
        /// </summary>
        public string[] GetProfileList()
        {
            List<string> ret = new List<string>();
            foreach (Profile prof in _Profiles) ret.Add(prof.Name);
            return ret.ToArray();
        }

        /// <summary>
        /// Checks if a profile with given name exists
        /// </summary>
        public bool HasProfile(string Name)
        {
            Profile prof;
            return _FindProfile(Name, out prof);
        }

        /// <summary>
        /// Updates profile info based on a specific internally cached profile.
        /// </summary>
        /// <param name="Name">Name of profile that gets loaded</param>
        /// <param name="pi_dst">Target ProfileInfo interface</param>
        public void LoadProfile(string Name, IProfileInfo pi_dst)
        {
            if (null == pi_dst) return; // just for safety should never happen

            Profile prof;
            pi_dst.ProfileUpdateBegin();
            pi_dst.ProfileName = Name;
            pi_dst.AttemptsCount = 0;
            pi_dst.ClearSplits();
            if (_FindProfile(Name, out prof))
            {
                pi_dst.AttemptsCount = prof.Attempts;
                foreach (ProfileRow row in prof.Rows)
                {
                    pi_dst.AddSplit(row.Title, row.Hits, row.WayHits, row.PB);
                }
                pi_dst.ActiveSplit = prof.ActiveSplit;
            }
            pi_dst.SetSessionProgress(0);
            pi_dst.ProfileUpdateEnd();
        }

        /// <summary>
        /// Updates internally cached profile with data from profile info
        /// </summary>
        /// <param name="pi_src">Source ProfileInfo interface</param>
        public void SaveProfile(IProfileInfo pi_src)
        {
            if (null == pi_src) return; // just for safety should never happen
            if (null == pi_src.ProfileName) return;

            Profile prof;

            // look for existing one and create if not exists
            if (!_FindProfile(pi_src.ProfileName, out prof))
            {
                prof = new Profile();
                prof.Name = pi_src.ProfileName;
                _Profiles.Add(prof);
            }

            prof.Rows.Clear();

            // collecting data, nom nom nom
            prof.Attempts = pi_src.AttemptsCount;
            prof.ActiveSplit = pi_src.ActiveSplit;
            for (int r = 0; r < pi_src.SplitCount; r++)
            {
                ProfileRow ProfileRow = new ProfileRow();
                ProfileRow.Title = pi_src.GetSplitTitle(r);
                ProfileRow.Hits = pi_src.GetSplitHits(r);
                ProfileRow.WayHits = pi_src.GetSplitWayHits(r);
                ProfileRow.PB = pi_src.GetSplitPB(r);
                prof.Rows.Add(ProfileRow);
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

    /// <summary>
    /// Interface to maintain the currently loaded profile data
    /// </summary>
    public interface IProfileInfo
    {
        /// <summary>
        /// Name of the currently loaded profile
        /// </summary>
        string ProfileName { get; set; }

        /// <summary>
        /// Amount of attempts
        /// </summary>
        int AttemptsCount { get; set; }

        /// <summary>
        /// Amount of splits
        /// </summary>
        int SplitCount { get; }

        /// <summary>
        /// Index of currently active split
        /// </summary>
        int ActiveSplit { get; set; }

        /// <summary>
        /// Removes all splits
        /// </summary>
        void ClearSplits();
        /// <summary>
        /// Add split
        /// </summary>
        /// <param name="Title">Title</param>
        /// <param name="Hits">Amount of hits at nosses (or all)</param>
        /// <param name="WaysHits">Amount of hits on the way</param>
        /// <param name="PB">Amount of personal best hits</param>
        void AddSplit(string Title, int Hits, int WayHits, int PB);
        /// <summary>
        /// Insert a new split before the current one
        /// </summary>
        void InsertSplit();

        /// <summary>
        /// Clear all hits, increase attempts counter and select first split
        /// </summary>
        void ResetRun();
        /// <summary>
        /// Mark the run as PB and jump to the run's end
        /// </summary>
        void setPB();
        /// <summary>
        /// Modifies the hit count of the current split
        /// </summary>
        /// <param name="Amount">Amount of hits that will be added/subtracted</param>
        void Hit(int Amount);
        /// <summary>
        /// Modifies the way hit count of the current split
        /// </summary>
        /// <param name="Amount">Amount of hits that will be added/subtracted</param>
        void WayHit(int Amount);
        /// <summary>
        /// Modifies the currently selected split
        /// </summary>
        /// <param name="Amount">Amount of splits that will be moved forwards/backwards</param>
        void GoSplits(int Amount);
        /// <summary>
        /// Interchange of two data rows
        /// </summary>
        /// <param name="Index">Source row</param>
        /// <param name="Offset">Offset to row that shall be permuted</param>
        void PermuteSplit(int Index, int Offset);

        /// <summary>
        /// Gets the split index of the session progress
        /// </summary>
        /// <returns>Session progress</returns>
        int GetSessionProgress();
        /// <summary>
        /// Sets the split index of the session progress
        /// Can only be higher unless AllowReset is set
        /// </summary>
        /// <param name="Index">Index of the new session progress</param>
        /// <param name="AllowReset">True allows to set lower values</param>
        void SetSessionProgress(int Index, bool AllowReset = false);

        /// <summary>
        /// Gets the title of a split
        /// </summary>
        /// <param name="Index">Index</param>
        /// <returns>Title</returns>
        string GetSplitTitle(int Index);
        /// <summary>
        /// Gets the hit counts (boss) of a split
        /// </summary>
        /// <param name="Index">Index</param>
        /// <returns>Amount of hits</returns>
        int GetSplitHits(int Index);
        /// <summary>
        /// Gets the hit counts (way) of a split
        /// </summary>
        /// <param name="Index">Index</param>
        /// <returns>Amount of hits</returns>
        int GetSplitWayHits(int Index);
        /// <summary>
        /// Gets the hit difference of a split
        /// </summary>
        /// <param name="Index">Index</param>
        /// <returns>Hit count difference</returns>
        int GetSplitDiff(int Index);
        /// <summary>
        /// Gets the person best hit counts of a split
        /// </summary>
        /// <param name="Index">Index</param>
        /// <returns>Amount of personal best hits</returns>
        int GetSplitPB(int Index);

        /// <summary>
        /// Sets the title of a split
        /// </summary>
        /// <param name="Index">Index</param>
        /// <param name="Title">Title</param>
        void SetSplitTitle(int Index, string Title);
        /// <summary>
        /// Sets the hit counts of a split
        /// </summary>
        /// <param name="Index">Index</param>
        /// <param name="Hits">Amount of hits at bosses (or all)</param>
        void SetSplitHits(int Index, int Hits);
        /// <summary>
        /// Sets the hit counts of a split
        /// </summary>
        /// <param name="Index">Index</param>
        /// <param name="WayHits">Amount of hits on the way</param>
        void SetSplitWayHits(int Index, int WayHits);
        /// <summary>
        /// Sets the hit difference of a split
        /// </summary>
        /// <param name="Index">Index</param>
        /// <param name="Diff">Hit count difference</param>
        void SetSplitDiff(int Index, int Diff);
        /// <summary>
        /// Sets the person best hit counts of a split
        /// </summary>
        /// <param name="Index">Index</param>
        /// <param name="PBHits">Amount of personal best hits</param>
        void SetSplitPB(int Index, int PBHits);

        /// <summary>
        /// Marks that an update will be performed
        /// Rises ProfileChanged event after update has completed
        /// </summary>
        void ProfileUpdateBegin();
        /// <summary>
        /// Marks that the current update has ended.
        /// Rises ProfileChanged on completion
        /// </summary>
        void ProfileUpdateEnd();

        /// <summary>
        /// Event that fires when any kind of data changed
        /// </summary>
        event EventHandler<EventArgs> ProfileChanged;
    }
}
