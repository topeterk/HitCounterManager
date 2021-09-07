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

// TODO Cleanup whole file

using System;
using System.Threading;
using Xamarin.Forms;
using HitCounterManager.ViewModels;
using static HitCounterManager.Views.InputBoxView;

namespace HitCounterManager.Views
{
    public partial class MainPage : ContentPage
    {

        // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/messaging-center

        public int MinWidth { get; set; }
        public int MinHeight { get; set; }

        public ProfileView ProfileView => profileView;

        public MainPage()
        {
            InitializeComponent();
#if MAYBE_IOS_COLLECTION_VIEW_WORKAROUND
            HACK_iOS_Collectionview_Fix();
#endif

            //ControlTemplate = template1; // Control Templates 101 https://www.youtube.com/watch?v=oah-Q1kPOyI
            //aboutPage.OnBack += NavigateFromAbout;
        }

#if MAYBE_IOS_COLLECTION_VIEW_WORKAROUND
        // https://www.gitmemory.com/issue/xamarin/Xamarin.Forms/10378/636506480
        void HACK_iOS_Collectionview_Fix() {
            //iOS has a problem with the collection view that this apparently causes it to render as needed
            var tempBindingContext = BindingContext;
            Device.StartTimer(TimeSpan.FromMilliseconds(100), () => {
                BindingContext = null;
                Device.StartTimer(TimeSpan.FromMilliseconds(100), () => {
                    BindingContext = tempBindingContext;
                    Device.StartTimer(TimeSpan.FromMilliseconds(100), () => {
                        BindingContext = null;
                        Device.StartTimer(TimeSpan.FromMilliseconds(100), () => {
                            BindingContext = tempBindingContext;
                            return false; });
                        return false; });
                    return false; });
                return false; });
        }
#endif

#if TEST
        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e == null) return;
            if (sender == null) return;
            if (((Editor)sender).BindingContext == null) return;

            if (e.NewTextValue == "U")
            {
                //string xxxx = await DisplayPromptAsync("TEST", "NACHRICHT", "OK");

                // https://docs.microsoft.com/de-de/xamarin/xamarin-forms/user-interface/pop-ups

                //await Device.InvokeOnMainThreadAsync(() => Application.Current.MainPage.DisplayAlert("TEST", "NACHRICHT", "ABBRUCH"));
                //string xx = await Device.InvokeOnMainThreadAsync(() => Application.Current.MainPage.DisplayPromptAsync("TEST", "NACHRICHT"));
                //xx += "A;";
                //Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(async () =>
                /*await Device.InvokeOnMainThreadAsync(async () =>
                {
                    string xx = await Application.Current.MainPage.DisplayPromptAsync("TEST", "NACHRICHT", "OK");
                    xx += "A;";
                });*/
                // https://www.c-sharpcorner.com/article/xamarin-forms-custom-popup/
            }
            if (e.NewTextValue == "F")
            {
                // https://docs.microsoft.com/de-de/xamarin/xamarin-forms/app-fundamentals/navigation/modal
                //this.Navigation.PushModalAsync(new Views.InputBoxView());
            }
            if (e.NewTextValue == "z")
            {
            // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/platform/device
                Device.InvokeOnMainThreadAsync (() =>
                {
                    DisplayPromptAsync("TEST", "NACHRICHT", "OK");
                });
            }
            if (e.NewTextValue == "w")
            {
                SynchronizationContext.Current.Send(_ =>
                {
                    DisplayPromptAsync("AAAA", "BBBB", "OK");
                }, null);
            }
        }
#endif

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
