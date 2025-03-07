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
using Avalonia.Controls.Notifications;
using Avalonia.Threading;
using HitCounterManager.Common;
using HitCounterManager.Models;

namespace HitCounterManager.ViewModels
{
    public class ProfileViewViewModel : ViewModelBase
    {
        #region AutoSplitter
        private readonly AutoSplitterCoreInterface? InterfaceASC = null;
        #endregion
        private readonly SettingsRoot Settings = App.CurrentApp.Settings;
        public ComboBox? ProfileSelector = null;

        public ProfileViewViewModel()
        {
            monotonic_timer.Start();

            ToggleShowInfo = ReactiveCommand.Create<string>((string name) => { ShowInfo[name].Value = !ShowInfo[name].Value; });

            ProfileList = [];
            foreach (Profile prof in Settings.Profiles.ProfileList)
            {
                ProfileModel profileModel = new (prof);
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
                _ProfileSelected.ActiveSplit = _ProfileSelected.Rows.IndexOf(item);
            });
            CmdSetSessionProgress = ReactiveCommand.Create<ProfileRowModel>((ProfileRowModel item) =>
            {
                if (item.SP) return;
                _ProfileSelected.SessionProgress = _ProfileSelected.Rows.IndexOf(item);
            });
            CmdSetBestProgress = ReactiveCommand.Create<ProfileRowModel>((ProfileRowModel item) =>
            {
                if (item.BP) return;
                _ProfileSelected.BestProgress = _ProfileSelected.Rows.IndexOf(item);
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

                InterfaceASC?.SplitterReset();
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

            HitIncrease = ReactiveCommand.Create(() => HitSumUp(+1, false));
            HitDecrease = ReactiveCommand.Create(() => HitSumUp(-1, false));
            HitWayIncrease = ReactiveCommand.Create(() => HitSumUp(+1, true));
            HitWayDecrease = ReactiveCommand.Create(() => HitSumUp(-1, true));
            HitIncreasePrev = ReactiveCommand.Create(() => HitSumUpPrev(+1, false));
            HitDecreasePrev = ReactiveCommand.Create(() => HitSumUpPrev(-1, false));
            HitWayIncreasePrev = ReactiveCommand.Create(() => HitSumUpPrev(+1, true));
            HitWayDecreasePrev = ReactiveCommand.Create(() => HitSumUpPrev(-1, true));
            SplitSelectNext = ReactiveCommand.Create(() => GoSplits(+1));
            SplitSelectPrev = ReactiveCommand.Create(() => GoSplits(-1));

            OutputUpdateTimer = new (TimeSpan.FromMilliseconds(300), DispatcherPriority.Background, OutputUpdateTimerTick);
            OutputUpdateTimer.Start();

            SaveToDisk = ReactiveCommand.Create(() => {
                App.CurrentApp.SaveSettings();
                InterfaceASC?.SaveSettings();
                App.CurrentApp.DisplayAlert("Saving complete!", "Written to \"" + Statics.ApplicationName + "Save.xml\"", NotificationType.Success);
            });

            #region AutoSplitter

            if (AutoSplitterCoreModule.AutoSplitterCoreLoaded)
            {
                InterfaceASC = new(this);
                AutoSplitterOpenConfig = ReactiveCommand.Create(InterfaceASC.OpenSettings);
                AutoSplitterGameList = InterfaceASC.GameList;
                AutoSplitterCoreModule.AutoSplitterRegisterInterface(InterfaceASC);
            }

            #endregion
        }

        ~ProfileViewViewModel()
        {
            OutputUpdateTimer.Stop();
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

        public Dictionary<string, ShowInfoBool> ShowInfo { get; } = new () {
                {"MainColumnHeaders", new ShowInfoBool()},
            };

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

        public class ProfileActionException(string message) : Exception(message)
        {
        }

        public void ProfileNew(string NewName)
        {
            if (App.CurrentApp.Settings.ReadOnlyMode) throw new ProfileActionException("Profile marked as read-only.");

            if (null == NewName) throw new ProfileActionException("The name is invalid.");
            if (NewName.Length == 0) throw new ProfileActionException("Empty name is not allowed.");
            if (IsProfileExisting(NewName)) throw new ProfileActionException("Profile already exists.");

            TimerRunning = false;

            // Create profile
            Profile profile = new()
            {
                Name = NewName
            };
            ProfileModel profileModel = new (profile);
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
            ProfileModel profileModel = new(profile);
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
        public ICommand HitIncreasePrev { get; }
        public ICommand HitDecreasePrev { get; }
        public ICommand HitWayIncreasePrev { get; }
        public ICommand HitWayDecreasePrev { get; }
        public ICommand SplitSelectNext { get; }
        public ICommand SplitSelectPrev { get; }

        public void GoSplits(int Amount)
        {
            int split = _ProfileSelected.ActiveSplit + Amount;
            if ((0 <= split) && (split < _ProfileSelected.Rows.Count))
            {
                if (0 < Amount) // going forward?
                {
                    if (_ProfileSelected.Rows[_ProfileSelected.ActiveSplit].SP) CmdSetSessionProgress.Execute(_ProfileSelected.Rows[split]);
                    if (_ProfileSelected.ActiveSplit == _ProfileSelected.BestProgress)
                    {
                        bool IsFlawless = true;
                        for (int i = 0; i < split; i++)
                        {
                            if ((0 != _ProfileSelected.Rows[i].Hits) || (0 != _ProfileSelected.Rows[i].WayHits))
                            {
                                IsFlawless = false;
                                break;
                            }
                        }
                        if (IsFlawless) CmdSetBestProgress.Execute(_ProfileSelected.Rows[split]);
                    }
                }
                _ProfileSelected.ActiveSplit = split;
            }
            else if (split <= _ProfileSelected.Rows.Count)
            {
                // Stop timer when run completes (when last split is finished)
                TimerRunning = false;
            }
        }

        public void HitSumUp(int Amount, bool IsWayHit)
        {
            ProfileRowModel row = _ProfileSelected.Rows[_ProfileSelected.ActiveSplit];
            if (IsWayHit) row.WayHits += Amount;
            else row.Hits += Amount;
        }

        public void HitSumUpPrev(int Amount, bool IsWayHit)
        {
            ProfileRowModel row = _ProfileSelected.Rows[Math.Max(_ProfileSelected.ActiveSplit - 1, 0)];
            if (IsWayHit) row.WayHits += Amount;
            else row.Hits += Amount;
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

        private readonly DispatcherTimer OutputUpdateTimer;
        private readonly object OutputUpdateLock = new ();
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
                OutputUpdateTimer.Stop();
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

        private readonly Stopwatch monotonic_timer = new ();

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
                    App.CurrentApp.StartApplicationTimer(TimerIDs.GameTime, 10, () => UpdateDuration());
                    App.CurrentApp.StartApplicationTimer(TimerIDs.GameTimeGui, 300, () => { CallPropertyChanged(nameof(StatsTime)); return _TimerRunning; });
                }
                else
                {
                    // STOP THE COUNT!
                    UpdateDuration(true); // AutoSplitterCore might not always update the time, so we have to force update
                    _TimerRunning = false;
                }
                CallPropertyChanged();
                OutputDataChangedHandler(this, new PropertyChangedEventArgs(nameof(TimerRunning)));
            }
        }

        private readonly object TimerUpdateLock = new ();
        private int AutoSplitterCoreUpdateCounter;
        public bool UpdateDuration(bool ForceUpdate = false)
        {
            // Early cancellation point
            if (!_TimerRunning) return false;

            // sad there is no try-lock, so we use the "precise equivalent" of lock(){}
            // from: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/lock-statement
            bool GotLock = false;
            try
            {
                Monitor.Enter(TimerUpdateLock, ref GotLock);

                #region AutoSplitter
                if (InterfaceASC?.GetCurrentInGameTime(out long CurrentTotalTime) ?? false && CurrentTotalTime > 0)
                {
                    // Calculate the current split's duration by removing durations of all previous splits from total time
                    long duration = CurrentTotalTime;
                    for (var previousSplitIndex = 0; previousSplitIndex < _ProfileSelected.ActiveSplit; previousSplitIndex++)
                        duration -= _ProfileSelected.Rows[previousSplitIndex].Duration;

                    // We don't always mark profile as updated here as this would generate output very often!
                    // When not forced, only update output when given and stored time is significantly out of sync
                    if (ForceUpdate
                        || (AutoSplitterCoreUpdateCounter++ % 30 == 0)
                        || (Math.Abs(duration - _ProfileSelected.Rows[_ProfileSelected.ActiveSplit].Duration) >= 1000)) // = 1 sec
                    {
                        _ProfileSelected.Rows[_ProfileSelected.ActiveSplit].Duration = Math.Max(duration, 0);
                    }
                }
                else
                #endregion
                {
                    long elapsed_time = monotonic_timer.ElapsedMilliseconds;
                    _ProfileSelected.Rows[_ProfileSelected.ActiveSplit].Duration += elapsed_time - last_elapsed_time;
                    last_elapsed_time = elapsed_time;
                }
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

        public ICommand? SaveToDisk { get; init; } = null;

        #region AutoSplitter

        public ICommand? AutoSplitterOpenConfig { get; init; } = null;

        public ObservableCollection<string>? AutoSplitterGameList { get; init; }

        private int _AutoSplitterGameSelectedIndex = 0;
        public int AutoSplitterGameSelectedIndex
        {
            get => _AutoSplitterGameSelectedIndex;
            set
            {
                if (_AutoSplitterGameSelectedIndex != value)
                {
                    _AutoSplitterGameSelectedIndex = value;
                    InterfaceASC?.SetActiveGameIndex(value);
                    CallPropertyChanged();
                }
            }
        }

        private bool _AutoSplitterPracticeModeChecked = false;
        public bool AutoSplitterPracticeModeChecked
        {
            get => _AutoSplitterPracticeModeChecked;
            set
            {
                if (_AutoSplitterPracticeModeChecked != value)
                {
                    _AutoSplitterPracticeModeChecked = value;
                    InterfaceASC?.SetPracticeMode(value);
                    CallPropertyChanged();
                }
            }
        }

        #endregion
    }
}
