// SPDX-FileCopyrightText: © 2021-2025 Peter Kirmeier
// SPDX-License-Identifier: MIT

using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using HitCounterManager.Controls;
using HitCounterManager.ViewModels;

namespace HitCounterManager.Views
{
    public partial class MainPage : PageBase<MainPageViewModel>
    {
        public ProfileView? ProfileView => this.FindControl<ProfileView>("profileView");

        public MainPage()
        {
            AvaloniaXamlLoader.Load(this);

            ViewModel.ProfileView = ProfileView;

            // Workaround: Avalonia's DataTriggerBehavior seems to be created but not executed during InitializeComponent()
            //             However, we rely on this trigger to update the UI accordingly.
            //             Therefore we notify about the propery change that the UI will update the value.
            //             But when the actual value does not change the trigger will not be executed,
            //             so, we return null up to here that we ensure there is a data change during notify.
            ViewModel.AlwaysOnTopDataTriggerWorkaroundCalled = true;
            ViewModel.CallPropertyChanged(nameof(ViewModel.AlwaysOnTop));
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            TopLevel? toplevel = TopLevel.GetTopLevel(this);
            App.CurrentApp.TopLevel = toplevel;
            App.CurrentApp.NotificationManager = new WindowNotificationManager(toplevel) { Position = NotificationPosition.TopRight, MaxItems = 1 };
        }

        public override void OnClosing(object? sender, CancelEventArgs e)
        {
            if (ViewModel.DoAskSaveBeforeClose)
            {
                // Not sure if we need to dispatch it but better safe than sorry
                Dispatcher.UIThread.Post(() => ViewModel.OpenPageAskSaveBeforeClose.Execute(null));
                e.Cancel = true;
            }
        }
    }
}
