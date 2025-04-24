//MIT License

//Copyright (c) 2021-2025 Peter Kirmeier

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

using HitCounterManager.Common;

namespace HitCounterManager.Models
{
    public  class ProfileRowModel(ProfileRow origin, ProfileModel parent) : NotifyPropertyChangedImpl
    {
        private readonly ProfileRow _origin = origin;
        private readonly ProfileModel _parent = parent;

        public string Title
        {
            get => _origin.Title;
            set => SetAndNotifyWhenChanged(ref _origin.Title, value);
        }
        public int Hits
        {
            get => _origin.Hits;
            set { if (SetAndNotifyWhenNaturalNumberChanged(ref _origin.Hits, value)) CallPropertyChanged(nameof(Diff)); }
        }
        public int WayHits
        {
            get => _origin.WayHits;
            set { if (SetAndNotifyWhenNaturalNumberChanged(ref _origin.WayHits, value)) CallPropertyChanged(nameof(Diff)); }
        }
        public int Diff => _origin.Hits + _origin.WayHits - _origin.PB;

        public int PB
        {
            get => _origin.PB;
            set { if (SetAndNotifyWhenNaturalNumberChanged(ref _origin.PB, value)) CallPropertyChanged(nameof(Diff)); }
        }

        public bool SubSplit
        {
            get => _origin.SubSplit;
            set => SetAndNotifyWhenChanged(ref _origin.SubSplit, value);
        }
        public bool SP => this == _parent.SessionProgressModel;
        public void SPChanged() => CallPropertyChanged(nameof(SP));
        public bool BP => this == _parent.BestProgressModel;
        public void BPChanged() => CallPropertyChanged(nameof(BP));
        public long Duration
        {
            get => _origin.Duration;
            set => _origin.Duration = value; // We don't notify about updates here as this would fire very very often while the timer is running!
        }
        public long DurationPB
        {
            get => _origin.DurationPB;
            set => SetAndNotifyWhenChanged(ref _origin.DurationPB, value);
        }
        public long DurationGold
        {
            get => _origin.DurationGold;
            set => SetAndNotifyWhenChanged(ref _origin.DurationGold, value);
        }
        public bool Active => this == _parent.ActiveSplitModel;
        public void ActiveChanged() => CallPropertyChanged(nameof(Active));
    }
}
