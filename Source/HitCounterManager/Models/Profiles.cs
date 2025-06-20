// SPDX-FileCopyrightText: © 2016-2025 Peter Kirmeier
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;

namespace HitCounterManager.Models
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
        public bool SubSplit = false;
        public long Duration = 0;
        public long DurationPB = 0;
        public long DurationGold = 0;

        public ProfileRow ShallowCopy() => (ProfileRow)MemberwiseClone();
        public ProfileRow DeepCopy() => ShallowCopy();
    }

    /// <summary>
    /// Single profile (used for a whole datagridview data collection)
    /// </summary>
    [Serializable]
    public class Profile
    {
        public string Name = "Unnamed";
        public int Attempts = 0;
        public int ActiveSplit = 0;
        public int BestProgress = 0;
        public List<ProfileRow> Rows = [];

        public Profile ShallowCopy() => (Profile)MemberwiseClone();
        public Profile DeepCopy()
        {
            Profile copy = ShallowCopy();
            copy.Rows = new List<ProfileRow>(Rows.Count);
            foreach (ProfileRow row in Rows) copy.Rows.Add(row.DeepCopy());
            return copy;
        }
    }

    /// <summary>
    /// Manages multiple profiles
    /// </summary>
    [Serializable]
    public class Profiles
    {
        public List<Profile> ProfileList { get; } = []; // used by XML serialization!
    }
}
