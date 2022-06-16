//MIT License

//Copyright (c) 2021-2022 Peter Kirmeier

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

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using HitCounterManager.Common;

namespace HitCounterManager.Models
{
    public  class ProfileModel : NotifyPropertyChangedImpl
    {
        public event PropertyChangedEventHandler ProfileDataChanged;

        private readonly Profile _origin;

        public ProfileModel(Profile origin)
        {
            _origin = origin;
            Rows = new ObservableCollection<ProfileRowModel>();
            for (int i = 0; i < _origin.Rows.Count; i++)
            {
                ProfileRowModel rowModel = new ProfileRowModel(_origin.Rows[i], this);
                rowModel.PropertyChanged += PropertyChangedHandler;
                Rows.Add(rowModel);
            }
            PropertyChanged += PropertyChangedHandler;
            Rows.CollectionChanged += CollectionChangedHandler;
        }

        public string Name
        {
            get { return _origin.Name; }
            set { SetAndNotifyWhenChanged(this, ref _origin.Name, value, nameof(Name)); }
        }
        public int Attempts
        {
            get { return _origin.Attempts; }
            set { SetAndNotifyWhenChanged(this, ref _origin.Attempts, value, nameof(Attempts)); }
        }
        public int ActiveSplit
        {
            get { return _origin.ActiveSplit; }
            set
            {
                if (value == _origin.ActiveSplit) return; // Nothing to do when nothing has changed

                int prevValue = _origin.ActiveSplit;
                _origin.ActiveSplit = value;
                if (prevValue < Rows.Count) Rows[prevValue].ActiveChanged();
                if (value < Rows.Count) Rows[value].ActiveChanged();
                CallPropertyChanged(this, nameof(ActiveSplit));
            }
        }

        public ProfileRowModel ActiveSplitModel => (_origin.ActiveSplit < Rows.Count ? Rows[_origin.ActiveSplit] : null);

        public int BestProgress
        {
            get { return _origin.BestProgress; }
            set
            {
                if (value == _origin.BestProgress) return; // Nothing to do when nothing has changed

                int prevValue = _origin.BestProgress;
                _origin.BestProgress = value;
                if (prevValue < Rows.Count) Rows[prevValue].BPChanged();
                if (value < Rows.Count) Rows[value].BPChanged();
                CallPropertyChanged(this, nameof(BestProgress));
            }
        }
        public ProfileRowModel BestProgressModel => (_origin.BestProgress < Rows.Count ? Rows[_origin.BestProgress] : null);

        public ObservableCollection<ProfileRowModel> Rows { get; private set; }
        private int _SessionProgress = 0;
        public int SessionProgress
        {
            get { return _SessionProgress; }
            set
            {
                if (value == _SessionProgress) return; // Nothing to do when nothing has changed

                int prevValue = _SessionProgress;
                _SessionProgress = value;
                if (prevValue < Rows.Count) Rows[prevValue].SPChanged();
                if (value < Rows.Count) Rows[value].SPChanged();
                CallPropertyChanged(this, nameof(SessionProgress));
            }
        }
        public ProfileRowModel SessionProgressModel => (_SessionProgress < Rows.Count ? Rows[_SessionProgress] : null);

        public void PermuteActiveSplit(int Offset)
        {
            int Index = ActiveSplit;
            int IndexDst = Index + Offset;
            if ((0 <= Index) && (Index < _origin.Rows.Count) &&
                (0 <= IndexDst) && (IndexDst < _origin.Rows.Count)) // Is permutation in range?
            {
                // Swap Data and Model
                ProfileRow tmp = _origin.Rows[Index];
                _origin.Rows[Index] = _origin.Rows[IndexDst];
                _origin.Rows[IndexDst] = tmp;
                Rows.Move(Index, IndexDst);
                ActiveSplit = IndexDst;
            }
        }

        public void InsertNewRow()
        {
            ProfileRow row = new ProfileRow();
            ProfileRowModel rowModel = new ProfileRowModel(row, this);
            rowModel.PropertyChanged += PropertyChangedHandler;

            _origin.Rows.Insert(ActiveSplit, row);
            Rows.Insert(ActiveSplit, new ProfileRowModel(row, this));

            Rows[ActiveSplit].ActiveChanged();
            Rows[ActiveSplit+1].ActiveChanged();
        }

        public void DeleteRow(ProfileRowModel row)
        {
            int rowIndex;
            int nextIndex;

            if (Rows.Count <= 1) return; // do not delete the only last remaining row

            rowIndex = Rows.IndexOf(row);
            nextIndex = rowIndex + 1;
            if (Rows.Count <= nextIndex) nextIndex = Rows.Count - 2; // in case last row is deleted, choose previous one

            // Merge duration with next row
            Rows[nextIndex].Duration += Rows[rowIndex].Duration;

            // Move active and session progress flags to next index
            if (row.Active) ActiveSplit = nextIndex;
            if (row.SP) SessionProgress = nextIndex;

            _origin.Rows.RemoveAt(rowIndex);
            Rows.RemoveAt(rowIndex);
        }

        private void PropertyChangedHandler(object sender, PropertyChangedEventArgs e) => ProfileDataChanged?.Invoke(sender, e);

        private void CollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e) => PropertyChangedHandler(sender, new PropertyChangedEventArgs(nameof(Rows)));

        public Profile DeepCopyOrigin() => _origin.DeepCopy();
    }
}
