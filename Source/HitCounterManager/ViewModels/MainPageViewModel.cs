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

using System.Windows.Input;
using Xamarin.Forms;
using HitCounterManager.Common;
using System;

namespace HitCounterManager.ViewModels
{
    // Xamarin Examples: https://docs.microsoft.com/en-us/samples/browse/?products=xamarin&term=Xamarin.Forms&terms=Xamarin.Forms

    // Multiple Templates in TreeView: https://www.wpf-tutorial.com/treeview-control/treeview-data-binding-multiple-templates/

    // BoxView: https://docs.microsoft.com/de-de/xamarin/xamarin-forms/user-interface/boxview

    class MainPageViewModel : NotifyPropertyChangedImpl
    {
        public MainPageViewModel()
        {
            ToggleAlwaysOnTop = new Command(() => {
                App app = App.CurrentApp;
                app.Settings.AlwaysOnTop = app.PlatformLayer.ApplicationWindowTopMost = !app.PlatformLayer.ApplicationWindowTopMost;
                CallPropertyChanged(this, nameof(AlwaysOnTop));
            });
        }

        public bool AlwaysOnTop { get => App.CurrentApp.PlatformLayer.ApplicationWindowTopMost; }

        public ICommand SaveToDisk { get; } = new Command(() => App.CurrentApp.SaveSettings());
        public ICommand OpenWebsiteHome { get; } = new Command(() => GitHubUpdate.WebOpenLandingPage());
#pragma warning disable CS0618 // Ignore deprecated (but without replacement, sure it is Launcher.OpenAsync in Xamarin.Essentials but requires additianal references
        public ICommand OpenWebsiteTeamHitless { get; } = new Command(() => Device.OpenUri(new Uri("https://discord.gg/4E7cSK7")));
#pragma warning restore CS0618
        public ICommand CheckUpdatesOnline { get; } = new Command(() => App.CurrentApp.CheckAndShowUpdates());

        public ICommand ToggleAlwaysOnTop { get; }
        public ICommand ToggleDarkMode { get; } = new Command(() => App.Current.UserAppTheme = (App.Current.UserAppTheme == OSAppTheme.Dark ? OSAppTheme.Light : OSAppTheme.Dark));
    }
}
