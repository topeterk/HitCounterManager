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
using System.Windows.Input;
using ReactiveUI;
using Avalonia.Controls.Notifications;
using HitCounterManager.Common;
using HitCounterManager.Controls;
using HitCounterManager.Views;

namespace HitCounterManager.ViewModels
{
    public class MainPageViewModel : ViewModelWindowBase
    {
        internal ProfileView? ProfileView;

        public MainPageViewModel()
        {
            OpenPageSettings = ReactiveCommand.CreateFromTask(async () => { await this.CreatePageOrWindow<SettingsPageViewModel, SettingsPage, SettingsWindow>().OpenAsDialog(OwnerWindow); });
            OpenPageUpdate = ReactiveCommand.CreateFromTask(async () => { await this.CreatePageOrWindow<UpdatePageViewModel, UpdatePage, UpdateWindow>().OpenAsDialog(OwnerWindow); });
            OpenPageAbout = ReactiveCommand.CreateFromTask(async () => { await this.CreatePageOrWindow<ViewModelWindowBase, AboutPage, AboutWindow>().OpenAsDialog(OwnerWindow); } );

            OpenPageSetAttempts = ReactiveCommand.CreateFromTask(async () => {
                if (null == ProfileView) return;

                IPageBase<ProfileAttemptsPageViewModel> page = this.CreatePageOrWindow<ProfileAttemptsPageViewModel, ProfileAttemptsPage, ProfileAttemptsWindow>();
                ProfileAttemptsPageViewModel viewModel = page.GetViewModel();
                viewModel.Origin = (ProfileViewViewModel?)ProfileView?.DataContext!;
                viewModel.UserInput = viewModel.Origin!.ProfileSelected.Attempts;
                await page.OpenAsDialog(OwnerWindow);
            });
            OpenPageProfileAction = ReactiveCommand.CreateFromTask(async (ProfileAction type) => {
                if (App.CurrentApp.Settings.ReadOnlyMode || (null == ProfileView)) return;

                IPageBase<ProfileActionPageViewModel> page = this.CreatePageOrWindow<ProfileActionPageViewModel, ProfileActionPage, ProfileActionWindow>();
                ProfileActionPageViewModel viewModel = page.GetViewModel();
                viewModel.Origin = (ProfileViewViewModel?)ProfileView?.DataContext!;
                viewModel.Action = type;
                await page.OpenAsDialog(OwnerWindow);
            });
            OpenPageAskSaveBeforeClose = ReactiveCommand.CreateFromTask(async () => {
                IPageBase<AskSaveBeforeClosePageViewModel> page = this.CreatePageOrWindow<AskSaveBeforeClosePageViewModel, AskSaveBeforeClosePage, AskSaveBeforeCloseWindow>();
                AskSaveBeforeClosePageViewModel viewModel = page.GetViewModel();
                await page.OpenAsDialog(OwnerWindow);
                if (null == viewModel.PressedYes) return; // Cancel pressed or dialog closed

                DoAskSaveBeforeClose = false; // Do not ask any more, we are about to shutdown
                if (true == viewModel.PressedYes) App.CurrentApp.SaveSettings();
                OwnerWindow?.Close();
            });

            CheckUpdatesOnline = ReactiveCommand.Create(() => App.CheckAndShowUpdates(this));

            ToggleAlwaysOnTop = ReactiveCommand.Create(() => {
                if (OwnerWindow != null)
                {
                    OwnerWindow.Topmost = App.CurrentApp.Settings.AlwaysOnTop = !App.CurrentApp.Settings.AlwaysOnTop;
                }
                CallPropertyChanged(nameof(AlwaysOnTop));
            });
            ToggleDarkMode = ReactiveCommand.Create(() => {
                App app = App.CurrentApp;
                app.ApplyTheme(!app.Settings.DarkMode); // Toggle
            });
        }

        public ICommand OpenPageSettings { get; }
        public ICommand OpenPageUpdate { get; }
        public ICommand OpenPageAbout { get; }

        public ICommand OpenPageSetAttempts { get; }
        public ICommand OpenPageProfileAction { get; }
        public ICommand OpenPageAskSaveBeforeClose { get; }
        public bool DoAskSaveBeforeClose { get; private set; } = true;

        public ICommand SaveToDisk { get; } = ReactiveCommand.Create(() => {
            App.CurrentApp.SaveSettings();
            App.CurrentApp.DisplayAlert("Saving complete!", "Written to \"" + Statics.ApplicationName + "Save.xml\"", NotificationType.Success);
        });
        public ICommand OpenWebsiteHome { get; } = ReactiveCommand.Create(() => GitHubUpdate.WebOpenLandingPage());
        public ICommand OpenWebsiteTeamHitless { get; } = ReactiveCommand.Create(() => Extensions.OpenWithBrowser(new Uri("https://discord.gg/4E7cSK7")));
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
