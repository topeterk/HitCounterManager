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

using System;
using Xamarin.Forms;
using HitCounterManager.ViewModels;

namespace HitCounterManager.Views
{
    public partial class MainPage : ContentPage
    {
        public int MinWidth { get; set; }
        public int MinHeight { get; set; }

        public ProfileView ProfileView => profileView;

        public MainPage()
        {
            InitializeComponent();
        }

        private void NavigateToSettings(object sender, EventArgs e) => Navigation.PushModalAsync(new SettingsPage(), false);
        private void NavigateToAbout(object sender, EventArgs e) => Navigation.PushModalAsync(new AboutPage(), false);
        private void NavigateFromAbout(object sender, EventArgs e) => Navigation.PopModalAsync();

        private void NavigateToProfileAction(object sender, EventArgs e) // TODO: Check other "Tapped" calls (e.g. of Settings with hotkey enum) that parameters are used instead of binding context?
        {
            if (App.CurrentApp.Settings.ReadOnlyMode) return;

            ProfileActionPageViewModel.ProfileAction type;
            if (Enum.TryParse<ProfileActionPageViewModel.ProfileAction>((string)((TappedEventArgs)e).Parameter, out type))
            {
                Navigation.PushModalAsync(new ProfileActionPage(type, (ProfileViewModel)profileView.BindingContext), false);
            }
        }

        private void NavigateToSetAttempts(object sender, EventArgs e) // TODO: Check other "Tapped" calls (e.g. of Settings with hotkey enum) that parameters are used instead of binding context?
        {
            Navigation.PushModalAsync(new ProfileAttemptsPage((ProfileViewModel)profileView.BindingContext), false);
        }
    }
}
