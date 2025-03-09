//MIT License

//Copyright (c) 2023-2025 Peter Kirmeier

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

using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;
//using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
//using HitCounterManager.Views;

namespace HitCounterManager.Android;

[Activity(
    Label = "HitCounterManager.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
            .WithInterFont()
            .UseReactiveUI();
    }

/*
    public override void OnBackPressed()
    {
        if (App.CurrentApp.ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            // Return to previous page
            SingleViewNavigationPage RootPage = (SingleViewNavigationPage)singleViewPlatform.MainView!;
            if (1 < RootPage.NavStack.Count)
            {
                RootPage.PopPage();
                return;
            }

            // Idea of stopping the process by defualt but this will popup the message box
            // every time one is about the hit back button. Therefore it is better to keep
            // the application running in backgroung.
            // We will save the current state during OnStop().
#if EXAMPLE_FORCED_SHUTDOWN
            RootPage.MainPage.OnClosing(this, new CancelEventArgs());
            App.CurrentApp.AppExitHandler(this, null);
            Finish();
#endif
        }

        base.OnBackPressed();
    }

    protected override void OnStop()
    {
        App.CurrentApp.SaveSettings();
        base.OnStop();
    }

    protected override void OnDestroy()
    {
        App.CurrentApp.AppExitHandler(this, new());
        base.OnDestroy();
    }
*/
}
