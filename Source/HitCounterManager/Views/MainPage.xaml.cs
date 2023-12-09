//MIT License

//Copyright (c) 2021-2023 Peter Kirmeier

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
            App.CurrentApp.NotificationManager = new WindowNotificationManager(TopLevel.GetTopLevel(this)) { Position = NotificationPosition.TopRight, MaxItems = 1 };
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
