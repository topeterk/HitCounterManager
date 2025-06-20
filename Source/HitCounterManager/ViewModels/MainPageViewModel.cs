// SPDX-FileCopyrightText: © 2021-2024 Peter Kirmeier
// SPDX-License-Identifier: MIT

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
