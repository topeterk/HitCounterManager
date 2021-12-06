//MIT License

//Copyright (c) 2021-2021 Peter Kirmeier

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
    public  class ProfileRowModel : NotifyPropertyChangedImpl
    {
        private readonly ProfileRow _origin;
        private readonly ProfileModel _parent;

        public ProfileRowModel(ProfileRow origin, ProfileModel parent)
        {
            _origin = origin;
            _parent = parent;
        }

        public string Title
        {
            get => _origin.Title;
            set => SetAndNotifyWhenChanged(this, ref _origin.Title, value, nameof(Title));
        }
        public int Hits
        {
            get => _origin.Hits;
            set { if (SetAndNotifyWhenNaturalNumberChanged(this, ref _origin.Hits, value, nameof(Hits))) CallPropertyChanged(this, nameof(Diff)); }
        }
        public int WayHits
        {
            get => _origin.WayHits;
            set { if (SetAndNotifyWhenNaturalNumberChanged(this, ref _origin.WayHits, value, nameof(WayHits))) CallPropertyChanged(this, nameof(Diff)); }
        }
        public int Diff => _origin.Hits + _origin.WayHits - _origin.PB;

        public int PB
        {
            get => _origin.PB;
            set { if (SetAndNotifyWhenNaturalNumberChanged(this, ref _origin.PB, value, nameof(PB))) CallPropertyChanged(this, nameof(Diff)); }
        }
        private bool _SP = false;
        public bool SP
        {
            get => _SP;
            set => SetAndNotifyWhenChanged(this, ref _SP, value, nameof(SP));
        }
        public long Duration
        {
            get => _origin.Duration;
            set => _origin.Duration = value; // We don't implicitly mark it as updated here as this would generate output very very often! // TODO: Maybe filter later (respondable UI!)?
        }
        public long DurationPB
        {
            get => _origin.DurationPB;
            set => SetAndNotifyWhenChanged(this, ref _origin.DurationPB, value, nameof(DurationPB));
        }
        public long DurationGold
        {
            get => _origin.DurationGold;
            set => SetAndNotifyWhenChanged(this, ref _origin.DurationGold, value, nameof(DurationGold));
        }
        public bool Active => this == _parent.ActiveSplitModel;
        public void ActiveChanged() => CallPropertyChanged(this, nameof(Active));
    }
}
