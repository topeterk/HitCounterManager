//MIT License

//Copyright (c) 2016-2022 Peter Kirmeier

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
        public List<ProfileRow> Rows = new List<ProfileRow>();

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
        private List<Profile> _Profiles = new List<Profile>();
        public List<Profile> ProfileList { get { return _Profiles; } } // used by XML serialization!
    }
}
