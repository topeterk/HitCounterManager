// SPDX-FileCopyrightText: © 2021-2025 Peter Kirmeier
// SPDX-License-Identifier: MIT

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
