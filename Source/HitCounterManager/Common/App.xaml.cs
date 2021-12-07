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
using System.Collections.Generic;
using Xamarin.Forms;
using HitCounterManager.Common.OsLayer;
using HitCounterManager.Common.PlatformLayer;
using HitCounterManager.Models;
using HitCounterManager.Views;
using HitCounterManager.ViewModels;
using HitCounterManager.Common;

#if SHOW_COMPLER_VERSION // enable and hover over #error to see C# compiler version and the used language version
#error version
#endif

namespace HitCounterManager
{
    public enum TimerIDs : uint { Shortcuts, ShortcutsCapture, GameTime, GameTimeGui }

    public partial class App : Application
    {
        private bool StartupDone = false;
        public readonly IOsLayer OsLayer;
        public readonly IPlatformLayer PlatformLayer;
        private HookHandle WindowHookHandle;
        public readonly ProfileViewModel profileViewModel;
        public readonly ProfileView profileView;
        public readonly OutModule om;
        public readonly Shortcuts sc;
        public SettingsRoot Settings { get; internal set; }
        public bool SettingsDialogOpen;

        public App(IOsLayer osLayer, IPlatformLayer platformLayer)
        {
            OsLayer = osLayer;
            PlatformLayer = platformLayer;
            SettingsDialogOpen = false;

            InitializeComponent();

            sc = new Shortcuts();
            om = new OutModule();
            LoadSettings();
            om.Settings = Settings;

            MainPage main = new MainPage();
            profileView = main.ProfileView;
            profileViewModel = (ProfileViewModel)main.ProfileView.BindingContext;
            PlatformLayer.ApplicationWindowMinSize = new Size(main.MinWidth, main.MinHeight);

            // GTK Issue: Navigationbar is always visible when root page is a NavigationPage
            // Known Bug: https://github.com/xamarin/Xamarin.Forms/issues/7492
            // Workaround: Do not use NavigationBar on GTK platform (Push/Pop works even without it)
            if (Device.RuntimePlatform == "GTK")
                MainPage = main;
            else
                MainPage = new NavigationPage(main);

            MainPage.Appearing += MainPage_Appearing;
            PageAppearing += App_PageAppearing;

            ModalPopped += App_ModalPopped;
        }

        private struct PostponedAlert
        {
            public string Title;
            public string Message;
            public string Cancel;

            public PostponedAlert(string Title, string Message, string Cancel)
            {
                this.Title = Title;
                this.Message = Message;
                this.Cancel = Cancel;
            }
        }
        private List<PostponedAlert> PostponedAlerts = new List<PostponedAlert>();

        /// <summary>
        /// Page.DisplayAlert but when no appeared yet, it will be postponed until MainPage has appeared
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Message"></param>
        /// <param name="Cancel"></param>
        public void DisplayAlert(string Title, string Message, string Cancel)
        {
            if (StartupDone)
                MainPage.DisplayAlert(Title, Message, Cancel);
            else
                PostponedAlerts.Add(new PostponedAlert(Title, Message, Cancel));
        }

        private void App_PageAppearing(object sender, Page e)
        {
            if (!StartupDone)
            {
                StartupDone = true;
                foreach(var Alert in PostponedAlerts) MainPage.DisplayAlert(Alert.Title, Alert.Message, Alert.Cancel);
                PostponedAlerts.Clear();
                PostponedAlerts = null;
            }
        }

        private void App_ModalPopped(object sender, ModalPoppedEventArgs e)
        {
            // Xamarin Issue: Framework seems to keep pages alive all the time in the background, it favors creating and destroing the same stuff all over again,
            //                instead of maintain a page and simply reusing it to spare memory and processor load.
            // See: https://social.msdn.microsoft.com/Forums/en-US/f3a2b48c-96ce-4e03-9f7b-1f57ff3f81b2/viewmodels-not-disposing-on-navigationpop?forum=xamarinforms
            //      https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/app-lifecycle
            // Workaround: Manually remove binding on disposal when modal popps in order to release all bindings immediately.
            //             It will ensure binding removal before before garbage collector disposal,
            //             otherwise any notification about a changed value might notify a [already disposed] no longer existing control
            (e.Modal.BindingContext as IDisposable)?.Dispose();
            e.Modal.BindingContext = null;
        }

        /// <summary>
        /// By definition when any Page is about to be presented:
        /// GTK: Before Window is actually shown
        /// WPF: When Window is already shown
        /// </summary>
        private void MainPage_Appearing(object sender, EventArgs e)
        {
            // Check for updates..
            if (Settings.CheckUpdatesOnStartup)
            {
                // Run check later in the background, so offline startup can proceed faster
                Device.StartTimer(TimeSpan.FromSeconds(2), () =>
                {
                    Device.BeginInvokeOnMainThread(() => CheckAndShowUpdates());
                    return false; // only once
                });
            }

            // just in case, do this only once
            if (null == WindowHookHandle)
            {
                // Register the hook function and hot keys
                WindowHookHandle = PlatformLayer.HookInstall(WndProc, PlatformLayer.ApplicationWindowHandle);
                bool Success = LoadAllHotKeySettings();
                if (!Success) DisplayAlert("Error setting up hot keys!", "Not all enabled hot keys could be registered successfully!", "OK");
            }
        }
        
        public void CheckAndShowUpdates()
        {
            // Get latest data
            if (GitHubUpdate.QueryAllReleases())
            {
                // When we are not the latest version, switch to the update page
                if (GitHubUpdate.LatestVersionName != Statics.ApplicationVersionString)
                {
                    MainPage.Navigation.PushModalAsync(new UpdatePage(), false);
                }
            }
        }

        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x312;
            App app = CurrentApp;

            if (msg == WM_HOTKEY)
            {
                if (!app.SettingsDialogOpen)
                {
                    // Console.WriteLine("WinProc: " + ((Shortcuts.SC_Type)wParam).ToString() +"!");
                    switch ((Shortcuts.SC_Type)wParam)
                    {
                        case Shortcuts.SC_Type.SC_Type_Reset: app.profileViewModel.ProfileReset.Execute(null); break;
                        case Shortcuts.SC_Type.SC_Type_Hit: app.profileViewModel.HitIncrease.Execute(null); break;
                        case Shortcuts.SC_Type.SC_Type_Split: app.profileViewModel.SplitSelectNext.Execute(null); break;
                        case Shortcuts.SC_Type.SC_Type_HitUndo: app.profileViewModel.HitDecrease.Execute(null); break;
                        case Shortcuts.SC_Type.SC_Type_SplitPrev: app.profileViewModel.SplitSelectPrev.Execute(null); break;
                        case Shortcuts.SC_Type.SC_Type_WayHit: app.profileViewModel.HitWayIncrease.Execute(null); break;
                        case Shortcuts.SC_Type.SC_Type_WayHitUndo: app.profileViewModel.HitWayDecrease.Execute(null); break;
                        case Shortcuts.SC_Type.SC_Type_PB: app.profileViewModel.ProfilePB.Execute(null); break;
                        case Shortcuts.SC_Type.SC_Type_TimerStart: app.profileViewModel.TimerRunning = true; break;
                        case Shortcuts.SC_Type.SC_Type_TimerStop: app.profileViewModel.TimerRunning = false; break;
                        default: break;
                    }
                }
                // else Console.WriteLine("WinProc: Settings open!");
            }

            return IntPtr.Zero;
        }

        ~App()
        {
            PlatformLayer.HookUninstall(WindowHookHandle);
            SaveSettings();
        }

        /// <summary>
        /// By definition ????:
        /// GTK: After MainPage_Appearing
        /// WPF: Before MainPage_Appearing
        /// </summary>
        protected override void OnStart() { }

        /// <summary>
        /// By definition when Minimized or Closed, but after testing:
        /// GTK: Minimize
        /// WPF: Close
        /// </summary>
        protected override void OnSleep() { }

        protected override void OnResume() { }

        // System.Environment.Exit(0)
        // ?? https://stackoverflow.com/questions/29257929/how-to-terminate-a-xamarin-application

        /// <summary>
        /// App instance of the running application
        /// </summary>
        public static App CurrentApp { get => (App)Current; }

        /// <summary>
        /// Creates a timer repeatedly calling the callback upon timeout.
        /// Depending on the callbacks return, the timer keeps running (true) or will be stopped (false)
        /// </summary>
        /// <param name="ID">The timer ID (one ID cannot be started multiple times)</param>
        /// <param name="Interval">Timeout in milliseconds</param>
        /// <param name="Callback">Callback function pointer</param>
        /// <returns>Success state</returns>
        public static bool StartApplicationTimer(TimerIDs ID, double Interval, Func<bool> Callback)
        {
            App app = CurrentApp;
            return app.OsLayer.SetTimer(app.PlatformLayer.ApplicationWindowHandle, (uint)ID, TimeSpan.FromMilliseconds(Interval), Callback);
        }
    }
}
