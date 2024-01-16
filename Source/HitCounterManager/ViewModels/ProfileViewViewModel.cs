//MIT License

//Copyright (c) 2021-2024 Peter Kirmeier

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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Windows.Input;
using ReactiveUI;
using Avalonia.Controls;
using Avalonia.Threading;
using HitCounterManager.Common;
using HitCounterManager.Models;

namespace HitCounterManager.ViewModels
{
    public class ProfileViewViewModel : ViewModelBase
    {
        private SettingsRoot Settings = App.CurrentApp.Settings;
        public ComboBox? ProfileSelector = null;

        public ProfileViewViewModel()
        {
            monotonic_timer.Start();

            ToggleShowInfo = ReactiveCommand.Create<string>((string name) => { ShowInfo[name].Value = !ShowInfo[name].Value; });

            ProfileList = new ObservableCollection<ProfileModel>();
            foreach (Profile prof in Settings.Profiles.ProfileList)
            {
                ProfileModel profileModel = new ProfileModel(prof);
                profileModel.ProfileDataChanged += OutputDataChangedHandler;
                ProfileList.Add(profileModel);
            }
            ProfileList.CollectionChanged += CollectionChangedHandler;

            string Name = Settings.ProfileSelected;
            foreach (ProfileModel prof in ProfileList)
            {
                if (prof.Name == Name)
                {
                    ProfileSelected = prof;
                    break;
                }
            }
            // when no matching profile is found, we start with the first one
            if (null == ProfileSelected) ProfileSelected = ProfileList[0];

            CmdRemoveSplit = ReactiveCommand.Create<ProfileRowModel>((ProfileRowModel item) =>
            {
                // sad there is no try-lock, so we use the "precise equivalent" of lock(){}
                // from: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/lock-statement
                bool GotLock = false;
                try
                {
                    Monitor.Enter(TimerUpdateLock, ref GotLock);

                    // It has to merge Duration values of the deleted row, so run run it within timer lock
                    _ProfileSelected.DeleteRow(item);
                }
                finally
                {
                    if (GotLock) Monitor.Exit(TimerUpdateLock);
                }
            });
            CmdSetActiveSplit = ReactiveCommand.Create<ProfileRowModel>((ProfileRowModel item) =>
            {
                if (item.Active) return;
                _ProfileSelected.ActiveSplit = _ProfileRowList.IndexOf(item);
            });
            CmdSetSessionProgress = ReactiveCommand.Create<ProfileRowModel>((ProfileRowModel item) =>
            {
                if (item.SP) return;
                _ProfileSelected.SessionProgress = _ProfileRowList.IndexOf(item);
            });
            CmdSetBestProgress = ReactiveCommand.Create<ProfileRowModel>((ProfileRowModel item) =>
            {
                if (item.BP) return;
                _ProfileSelected.BestProgress = _ProfileRowList.IndexOf(item);
            });

            ProfileReset = ReactiveCommand.Create(() =>
            {
                TimerRunning = false;

                _ProfileSelected.Attempts++; // Increase attempts
                foreach (ProfileRowModel row in _ProfileSelected.Rows)
                {
                    if (!row.Active) // Check and update gold time when split is not finished
                    {
                        if ((0 < row.Duration) && (row.Duration < row.DurationGold)) row.DurationGold = row.Duration;
                    }
                    row.Hits = 0;
                    row.WayHits = 0;
                    row.Duration = 0;
                }
                _ProfileSelected.ActiveSplit = 0;
            });
            ProfilePB = ReactiveCommand.Create(() =>
            {
                TimerRunning = false;

                foreach (ProfileRowModel row in _ProfileSelected.Rows)
                {
                    row.PB = row.Hits + row.WayHits;
                    if ((0 < row.Duration) && (row.Duration < row.DurationGold)) row.DurationGold = row.Duration;
                    row.DurationPB = row.Duration;
                }
                _ProfileSelected.ActiveSplit = _ProfileSelected.Rows.Count - 1;
                _ProfileSelected.SessionProgress = _ProfileSelected.Rows.Count - 1;
            });
            ProfileSetAttempts = ReactiveCommand.Create<int>((int NewAttempts) => _ProfileSelected.Attempts = NewAttempts);

            ToggleTimerPause = ReactiveCommand.Create(() => TimerRunning = !TimerRunning);
            ToggleReadOnlyMode = ReactiveCommand.Create(() => IsReadOnly = !IsReadOnly);

            ProfileSplitMoveUp = ReactiveCommand.Create(() => {
                if (App.CurrentApp.Settings.ReadOnlyMode) return;
                _ProfileSelected.PermuteActiveSplit(-1);
            });
            ProfileSplitMoveDown = ReactiveCommand.Create(() => {
                if (App.CurrentApp.Settings.ReadOnlyMode) return;
                _ProfileSelected.PermuteActiveSplit(+1);
            });
            ProfileSplitInsert = ReactiveCommand.Create(() => {
                if (App.CurrentApp.Settings.ReadOnlyMode) return;
                _ProfileSelected.InsertNewRow();
            });

            HitIncrease = ReactiveCommand.Create(() => _ProfileSelected.Rows[_ProfileSelected.ActiveSplit].Hits++);
            HitDecrease = ReactiveCommand.Create(() => _ProfileSelected.Rows[_ProfileSelected.ActiveSplit].Hits--);
            HitWayIncrease = ReactiveCommand.Create(() => _ProfileSelected.Rows[_ProfileSelected.ActiveSplit].WayHits++);
            HitWayDecrease = ReactiveCommand.Create(() => _ProfileSelected.Rows[_ProfileSelected.ActiveSplit].WayHits--);
            SplitSelectNext = ReactiveCommand.Create(() => GoSplits(+1));
            SplitSelectPrev = ReactiveCommand.Create(() => GoSplits(-1));

            OutputUpdateTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(300), DispatcherPriority.Background, OutputUpdateTimerTick);
            OutputUpdateTimer.Start();
        }

        ~ProfileViewViewModel()
        {
            OutputUpdateTimer?.Stop();
        }

        public class ShowInfoBool : NotifyPropertyChangedImpl
        {
            private bool _Value = false;
            public bool Value
            {
                get => _Value;
                set => SetAndNotifyWhenChanged(ref _Value, value);
            }
        }

        private Dictionary<string, ShowInfoBool> _ShowInfo = new Dictionary<string, ShowInfoBool>(){
            {"MainColumnHeaders", new ShowInfoBool()},
        };
        public Dictionary<string, ShowInfoBool> ShowInfo { get => _ShowInfo; }

        public ICommand ToggleShowInfo { get; }

        public void OutputDataChangedHandler(object? sender, PropertyChangedEventArgs e)
        {
            UpdateDuration();
            CallPropertyChanged(nameof(StatsProgress));
            CallPropertyChanged(nameof(StatsTime));
            CallPropertyChanged(nameof(StatsTotalHits));
            OutputDataQueueUpdate();
        }
        private void CollectionChangedHandler(object? sender, NotifyCollectionChangedEventArgs e) => OutputDataChangedHandler(sender, new PropertyChangedEventArgs(nameof(ProfileList)));

        public ICommand CmdRemoveSplit { get; }
        public ICommand CmdSetActiveSplit { get; }
        public ICommand CmdSetSessionProgress { get; }
        public ICommand CmdSetBestProgress { get; }

        private ObservableCollection<ProfileRowModel> _ProfileRowList => _ProfileSelected.Rows;
        public ObservableCollection<ProfileModel> ProfileList { get; private set; }

        public bool IsProfileExisting(string Name)
        {
            foreach (ProfileModel profileModel in ProfileList)
            {
                if (profileModel.Name == Name) return true;
            }
            return false;
        }

        private ProfileModel _ProfileSelected;
        [MemberNotNull(nameof(_ProfileSelected))]
        public ProfileModel ProfileSelected
        {
#pragma warning disable CS8774
            get => _ProfileSelected!;
            set
            {
                if (null != value && _ProfileSelected != value)
                {
                    if (ProfileList.Contains(value))
                    {
                        UpdateDuration();

                        Monitor.Enter(TimerUpdateLock);
                        _ProfileSelected = value;
                        Monitor.Exit(TimerUpdateLock);
                        Settings.ProfileSelected = _ProfileSelected.Name;

                        CallPropertyChanged();
                        OutputDataChangedHandler(this, new PropertyChangedEventArgs(nameof(ProfileSelected)));
                    }
                }
            }
#pragma warning restore CS8774
        }

        public bool IsReadOnly
        {
            get => Settings.ReadOnlyMode;
            set
            {
                Settings.ReadOnlyMode = value;
                CallPropertyChanged();
            }
        }

        public class ProfileActionException : Exception
        {
            public ProfileActionException(string message) : base(message) { }
        }

        public void ProfileNew(string NewName)
        {
            if (App.CurrentApp.Settings.ReadOnlyMode) throw new ProfileActionException("Profile marked as read-only.");

            if (null == NewName) throw new ProfileActionException("The name is invalid.");
            if (NewName.Length == 0) throw new ProfileActionException("Empty name is not allowed.");
            if (IsProfileExisting(NewName)) throw new ProfileActionException("Profile already exists.");

            TimerRunning = false;

            // Create profile
            Profile profile = new Profile();
            profile.Name = NewName;
            ProfileModel profileModel = new ProfileModel(profile);
            profileModel.InsertNewRow();
            profileModel.ProfileDataChanged += OutputDataChangedHandler;

            // Add and select profile
            Settings.Profiles.ProfileList.Add(profile);
            Settings.Profiles.ProfileList.Sort((a, b) => a.Name.CompareTo(b.Name)); // Sort by name
            int profileIndex = Settings.Profiles.ProfileList.IndexOf(profile);
            ProfileList.Insert(profileIndex, profileModel);
            ProfileSelected = profileModel;
        }
        public void ProfileRename(string NewName)
        {
            if (App.CurrentApp.Settings.ReadOnlyMode) throw new ProfileActionException("Profile marked as read-only.");

            if (null == NewName) throw new ProfileActionException("The name is invalid.");
            if (NewName.Length == 0) throw new ProfileActionException("Empty name is not allowed.");
            if (IsProfileExisting(NewName)) throw new ProfileActionException("Profile already exists.");

            _ProfileSelected.Name = NewName;
        }
        public void ProfileCopy(string NewName)
        {
            if (App.CurrentApp.Settings.ReadOnlyMode) throw new ProfileActionException("Profile marked as read-only.");

            if (null == NewName) throw new ProfileActionException("The name is invalid.");
            if (NewName.Length == 0) throw new ProfileActionException("Empty name is not allowed.");
            if (IsProfileExisting(NewName)) throw new ProfileActionException("Profile already exists.");

            UpdateDuration();

            // Create profile
            Profile profile = ProfileSelected.DeepCopyOrigin();
            profile.Name = NewName;
            ProfileModel profileModel = new ProfileModel(profile);
            profileModel.ProfileDataChanged += OutputDataChangedHandler;

            // Add and select profile
            Settings.Profiles.ProfileList.Add(profile);
            Settings.Profiles.ProfileList.Sort((a, b) => a.Name.CompareTo(b.Name)); // Sort by name
            int profileIndex = Settings.Profiles.ProfileList.IndexOf(profile);
            ProfileList.Insert(profileIndex, profileModel);
            ProfileSelected = profileModel;
        }
        public void ProfileDelete()
        {
            if (App.CurrentApp.Settings.ReadOnlyMode) throw new ProfileActionException("Profile marked as read-only.");

            if (ProfileList.Count <= 1) throw new ProfileActionException("Cannot delete last profile.");

            TimerRunning = false;

            int profileIndex = ProfileList.IndexOf(_ProfileSelected);

            // Change to next/previous profile before removing
            int Index = profileIndex + 1;
            if (ProfileList.Count <= Index) Index = ProfileList.Count - 2; // in case last profile is deleted, choose previous one
            ProfileSelected = ProfileList[Index];

            // Removing
            Settings.Profiles.ProfileList.RemoveAt(profileIndex);
            ProfileList.RemoveAt(profileIndex);

            OutputDataChangedHandler(this, new PropertyChangedEventArgs(nameof(ProfileSelected)));
        }

        public ICommand ProfileReset { get; }
        public ICommand ProfilePB { get; }
        public ICommand ProfileSetAttempts { get; }
        public ICommand ToggleTimerPause { get; }
        public ICommand ToggleReadOnlyMode { get; }

        public ICommand ProfileSplitMoveUp { get; }
        public ICommand ProfileSplitMoveDown { get; }
        public ICommand ProfileSplitInsert { get; }

        public ICommand HitIncrease { get; }
        public ICommand HitDecrease { get; }
        public ICommand HitWayIncrease { get; }
        public ICommand HitWayDecrease { get; }
        public ICommand SplitSelectNext { get; }
        public ICommand SplitSelectPrev { get; }

        private void GoSplits(int Amount)
        {
            int split = _ProfileSelected.ActiveSplit + Amount;
            if ((0 <= split) && (split < _ProfileRowList.Count))
            {
                if (0 < Amount) // going forward?
                {
                    if (_ProfileRowList[_ProfileSelected.ActiveSplit].SP) CmdSetSessionProgress.Execute(_ProfileRowList[split]);
                    if (_ProfileSelected.ActiveSplit == _ProfileSelected.BestProgress)
                    {
                        bool IsFlawless = true;
                        for (int i = 0; i < split; i++)
                        {
                            if ((0 != _ProfileRowList[i].Hits) || (0 != _ProfileRowList[i].WayHits))
                            {
                                IsFlawless = false;
                                break;
                            }
                        }
                        if (IsFlawless) CmdSetBestProgress.Execute(_ProfileRowList[split]);
                    }
                }
                _ProfileSelected.ActiveSplit = split;
            }
            else if (split <= _ProfileRowList.Count)
            {
                // Stop timer when run completes (when last split is finished)
                TimerRunning = false;
            }
        }

        public string StatsProgress => "Progress:  " + _ProfileSelected.ActiveSplit.ToString() + " / " + _ProfileSelected.Rows.Count.ToString() + "  # " + _ProfileSelected.Attempts.ToString("D3");
        public string StatsTime
        {
            get
            {
                long TotalTime = 0;

                foreach (ProfileRowModel row in _ProfileSelected.Rows)
                {
                    TotalTime += row.Duration;
                }
                TotalTime /= 1000; // we only care about seconds

                return "Time: " + (TotalTime/60/60).ToString("D2") + " : " + ((TotalTime/60) % 60).ToString("D2") + " : " + (TotalTime % 60).ToString("D2");
            }
        }
        public string StatsTotalHits
        {
            get
            {
                int TotalHits = 0;
                int TotalPB = 0;

                foreach (ProfileRowModel row in _ProfileSelected.Rows)
                {
                    TotalHits += row.Hits + row.WayHits;
                    TotalPB += row.PB;
                }

                return "Total: " + TotalHits.ToString() + " Hits   " + TotalPB.ToString() + " PB";
            }
        }

        #region Output Update Queue

        private DispatcherTimer? OutputUpdateTimer;
        private readonly object OutputUpdateLock = new object();
        private bool IsOutputUpdatePending = false;
        private bool IsOutputUpdateStable = true;

        private void OutputUpdateTimerTick(object? sender, EventArgs e)
        {
            lock (OutputUpdateLock)
            {
                // Continue waiting as long as data is not stable
                IsOutputUpdatePending = !IsOutputUpdateStable;
                IsOutputUpdateStable = true;
            }

            // When update is no longer pending, write output and stop timer
            if (!IsOutputUpdatePending)
            {
                Dispatcher.UIThread.Post(() => App.CurrentApp.om.Update(_ProfileSelected, TimerRunning));
                OutputUpdateTimer!.Stop();
            }
        }

        /// <summary>
        /// When data has changed, call this function to write the output.
        /// The output will be delayed and only written when values are no longer changing.
        /// </summary>
        private void OutputDataQueueUpdate()
        {
            if (null == OutputUpdateTimer) return;

            lock (OutputUpdateLock)
            {
                IsOutputUpdateStable = false;

                if (!IsOutputUpdatePending)
                {
                    IsOutputUpdatePending = true;
                    OutputUpdateTimer.Stop();
                    OutputUpdateTimer.Start();
                }
            }
        }

        #endregion

        #region Game Timer

        private Stopwatch monotonic_timer = new ();

        private long last_elapsed_time = 0;

        private bool _TimerRunning = false;
        public bool TimerRunning
        {
            get { return _TimerRunning; }
            set
            {
                if (value == _TimerRunning) return;

                if (!_TimerRunning)
                {
                    // Starting the timer
                    last_elapsed_time = monotonic_timer.ElapsedMilliseconds;
                    _TimerRunning = true;
                    App.CurrentApp.StartApplicationTimer(TimerIDs.GameTime, 10, UpdateDuration);
                    App.CurrentApp.StartApplicationTimer(TimerIDs.GameTimeGui, 300, () => { CallPropertyChanged(nameof(StatsTime)); return _TimerRunning; });
                }
                else
                {
                    // STOP THE COUNT!
                    UpdateDuration();
                    _TimerRunning = false;
                }
                CallPropertyChanged();
                OutputDataChangedHandler(this, new PropertyChangedEventArgs(nameof(TimerRunning)));
            }
        }

        private readonly object TimerUpdateLock = new object();
        public bool UpdateDuration()
        {
            // Early cancellation point
            if (!_TimerRunning) return false;

            // sad there is no try-lock, so we use the "precise equivalent" of lock(){}
            // from: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/lock-statement
            bool GotLock = false;
            try
            {
                Monitor.Enter(TimerUpdateLock, ref GotLock);

                long elapsed_time = monotonic_timer.ElapsedMilliseconds;
                _ProfileSelected.Rows[_ProfileSelected.ActiveSplit].Duration += elapsed_time - last_elapsed_time;
                last_elapsed_time = elapsed_time;
            }
            catch
            {
                // May happen during deletion when GUI updates are postponed and/or excetued out of order.
                // Try to fix it by selecting last split (when we are out of range the last split should be fine)
                _ProfileSelected.ActiveSplit = _ProfileSelected.Rows.Count - 1;
            }
            finally
            {
                if (GotLock) Monitor.Exit(TimerUpdateLock);
            }

            return _TimerRunning;
        }

        #endregion
    }
}
