// SPDX-FileCopyrightText: © 2021-2025 Peter Kirmeier
// SPDX-License-Identifier: MIT

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using HitCounterManager.Common;

namespace HitCounterManager.Models
{
    public  class ProfileModel : NotifyPropertyChangedImpl
    {
        public event PropertyChangedEventHandler? ProfileDataChanged;

        private readonly Profile _origin;

        public ProfileModel(Profile origin)
        {
            _origin = origin;
            Rows = [];
            for (int i = 0; i < _origin.Rows.Count; i++)
            {
                ProfileRowModel rowModel = new(_origin.Rows[i], this);
                rowModel.PropertyChanged += PropertyChangedHandler;
                Rows.Add(rowModel);
            }
            PropertyChanged += PropertyChangedHandler;
            Rows.CollectionChanged += CollectionChangedHandler;
        }

        public string Name
        {
            get { return _origin.Name; }
            set { SetAndNotifyWhenChanged(ref _origin.Name, value); }
        }
        public int Attempts
        {
            get { return _origin.Attempts; }
            set { SetAndNotifyWhenChanged(ref _origin.Attempts, value); }
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
                CallPropertyChanged();
            }
        }

        public ProfileRowModel? ActiveSplitModel => _origin.ActiveSplit < Rows.Count ? Rows[_origin.ActiveSplit] : null;

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
                CallPropertyChanged();
            }
        }
        public ProfileRowModel? BestProgressModel => _origin.BestProgress < Rows.Count ? Rows[_origin.BestProgress] : null;

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
                CallPropertyChanged();
            }
        }
        public ProfileRowModel? SessionProgressModel => _SessionProgress < Rows.Count ? Rows[_SessionProgress] : null;

        public void PermuteActiveSplit(int Offset)
        {
            int Index = ActiveSplit;
            int IndexDst = Index + Offset;
            if ((0 <= Index) && (Index < _origin.Rows.Count) &&
                (0 <= IndexDst) && (IndexDst < _origin.Rows.Count)) // Is permutation in range?
            {
                // Swap Data and Model
                (_origin.Rows[IndexDst], _origin.Rows[Index]) = (_origin.Rows[Index], _origin.Rows[IndexDst]);
                Rows.Move(Index, IndexDst);
                ActiveSplit = IndexDst;
            }
        }

        public void InsertNewRow()
        {
            ProfileRow row = new();
            ProfileRowModel rowModel = new(row, this);
            rowModel.PropertyChanged += PropertyChangedHandler;

            int newIndex = (0 == _origin.Rows.Count ? 0 :  ActiveSplit + 1);
            _origin.Rows.Insert(newIndex, row);
            Rows.Insert(newIndex, rowModel);
            ActiveSplit = newIndex;
        }

        public void AppendNewRow(string? NewName)
        {
            ProfileRow row = new();
            ProfileRowModel rowModel = new(row, this);
            rowModel.PropertyChanged += PropertyChangedHandler;

            int rowIndex = Rows.Count;
            _origin.Rows.Insert(rowIndex, row);
            Rows.Insert(rowIndex, rowModel);
            if (!string.IsNullOrEmpty(NewName)) Rows[rowIndex].Title = NewName;
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

        private void PropertyChangedHandler(object? sender, PropertyChangedEventArgs e) => ProfileDataChanged?.Invoke(sender, e);

        private void CollectionChangedHandler(object? sender, NotifyCollectionChangedEventArgs e) => PropertyChangedHandler(sender, new PropertyChangedEventArgs(nameof(Rows)));

        public Profile DeepCopyOrigin() => _origin.DeepCopy();
    }
}
