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

using System;
using System.Windows.Input;
using ReactiveUI;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using HitCounterManager.Common;
using HitCounterManager.Controls;
using HitCounterManager.Views;

namespace HitCounterManager.ViewModels
{
    public class MainPageViewModel : NotifyPropertyChangedImpl
    {
        public Window? OwnerWindow { get; set; }
        private ProfileView? ProfileView => OwnerWindow?.FindControl<ProfileView>("profileView");

        public MainPageViewModel()
        {
            NavigateToSettings = ReactiveCommand.CreateFromTask(async () => { if (null != OwnerWindow) await new SettingsPage().ShowDialog(OwnerWindow); });
            NavigateToUpdate = ReactiveCommand.CreateFromTask(async () => { if (null != OwnerWindow) await new UpdatePage().ShowDialog(OwnerWindow); });
            NavigateToAbout = ReactiveCommand.CreateFromTask(async () => { if (null != OwnerWindow) await new AboutPage().ShowDialog(OwnerWindow); } );

            NavigateToSetAttempts = ReactiveCommand.CreateFromTask(async () => {
                if ((null == OwnerWindow) || (null == ProfileView)) return;

                await new ProfileAttemptsPage((ProfileViewModel?)ProfileView?.DataContext!).ShowDialog(OwnerWindow);
            });
            NavigateToProfileAction = ReactiveCommand.CreateFromTask(async (ProfileAction type) => {
                if (App.CurrentApp.Settings.ReadOnlyMode || (null == OwnerWindow) || (null == ProfileView)) return;

                await new ProfileActionPage(type, (ProfileViewModel?)ProfileView?.DataContext!).ShowDialog(OwnerWindow);
            });

            CheckUpdatesOnline = ReactiveCommand.Create(() => App.CurrentApp.CheckAndShowUpdates(this));

            ToggleAlwaysOnTop = ReactiveCommand.Create(() => {
                OwnerWindow?.PlatformImpl.SetTopmost(App.CurrentApp.Settings.AlwaysOnTop = !App.CurrentApp.Settings.AlwaysOnTop);
                CallPropertyChanged(this, nameof(AlwaysOnTop));
            });
            ToggleDarkMode = ReactiveCommand.Create(() => {
                App app = App.CurrentApp;
                app.ApplyTheme(!app.Settings.DarkMode); // Toggle
            });
        }

        public ICommand NavigateToSettings { get; }
        public ICommand NavigateToUpdate { get; }
        public ICommand NavigateToAbout { get; }

        public ICommand NavigateToSetAttempts { get; }

        public ICommand NavigateToProfileAction { get; }


        public ICommand SaveToDisk { get; } = ReactiveCommand.Create(() => {
            App.CurrentApp.SaveSettings();
            App.CurrentApp.DisplayAlert("Saving complete!", "Written to \"" + Statics.ApplicationName + "Save.xml\"", NotificationType.Success);
        });
        public ICommand OpenWebsiteHome { get; } = ReactiveCommand.Create(() => GitHubUpdate.WebOpenLandingPage());
        public ICommand OpenWebsiteTeamHitless { get; } = ReactiveCommand.Create(() => Device.OpenUri(new Uri("https://discord.gg/4E7cSK7")));
        public ICommand CheckUpdatesOnline { get; }

        public bool AlwaysOnTopDataTriggerWorkaroundCalled = false;
        public bool? AlwaysOnTop {
            get
            {
                if (!AlwaysOnTopDataTriggerWorkaroundCalled) return null;
                return App.CurrentApp.Settings.AlwaysOnTop;
            }
        }
        public ICommand ToggleAlwaysOnTop { get; }
        public ICommand ToggleDarkMode { get; }
    }
}
