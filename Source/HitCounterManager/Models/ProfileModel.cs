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
                ProfileRowModel rowModel = new ProfileRowModel(_origin.Rows[i]);
                rowModel.Active = ActiveSplit == i;
                rowModel.PropertyChanged += PropertyChangedHandler;
                Rows.Add(rowModel);
            }
            if (1 <= Rows.Count) Rows[0].SP = true; // TODO: Use SessionProgress member?
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
            set { SetAndNotifyWhenChanged(this, ref _origin.ActiveSplit, value, nameof(ActiveSplit)); } // TODO: Update Rows?
        }
        public ObservableCollection<ProfileRowModel> Rows { get; private set; }
        private int _SessionProgress = 0;
        public int SessionProgress
        {
            get { return _SessionProgress; }
            set { _SessionProgress = value; CallPropertyChanged(this, nameof(SessionProgress)); } // TODO: Update Rows?
        }

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
                //CallPropertyChanged(this, nameof(Rows)); // TODO: Check if required?
            }
        }

        public void InsertNewRow()
        {
            ProfileRow row = new ProfileRow();
            ProfileRowModel rowModel = new ProfileRowModel(row);
            rowModel.PropertyChanged += PropertyChangedHandler;

            _origin.Rows.Insert(ActiveSplit, row);
            Rows.Insert(ActiveSplit, new ProfileRowModel(row));
        }

        public void DeleteRow(ProfileRowModel row)
        {
            int rowIndex;

            if (Rows.Count <= 1) return; // do not delete the only last remaining row

            rowIndex = Rows.IndexOf(row);

            if (row.Active)
            {
                int Index = rowIndex + 1;
                if (Rows.Count <= Index) Index = Rows.Count - 2; // in case last row is deleted, choose previous one
                ActiveSplit = Index;
                Rows[Index].Active = true;
            }
            if (row.SP)
            {
                int Index = rowIndex + 1;
                if (Rows.Count <= Index) Index = Rows.Count - 2; // in case last row is deleted, choose previous one
                SessionProgress = Index;
                Rows[Index].SP = true;
            }

            _origin.Rows.RemoveAt(rowIndex);
            Rows.RemoveAt(rowIndex);
        }

        private void PropertyChangedHandler(object sender, PropertyChangedEventArgs e) => ProfileDataChanged?.Invoke(sender, e);

        private void CollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e) => PropertyChangedHandler(sender, new PropertyChangedEventArgs(nameof(Rows)));

        public Profile DeepCopyOrigin()
        {
            return _origin.DeepCopy(); // TODO: What happens with events on copy?
        }

#if TODO // Don't know if we need this event any more?
        private bool RunCompleted = false;
        
        public event EventHandler<ProfileChangedEventArgs> ProfileChanged;

        public int ActiveSplit
        {
            set
            {
                if ((LastActiveSplit != value) || (0 == SelectedCells.Count))
                {
                    if (value == Rows.Count - 1) RunCompleted = true;

                    if (null != ProfileChanged)
                    {
                        ProfileChangedEventArgs args = new ProfileChangedEventArgs();
                        args.RunCompleted = RunCompleted;
                        RunCompleted = false;
                        ProfileChanged(this, args);
                    }
                }
            }
        }
#endif
    }
}
