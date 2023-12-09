using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;
using Avalonia.ReactiveUI;

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
}

/*using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Avalonia;
using Avalonia.Android;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using HitCounterManager.Views;
using System.ComponentModel;

namespace HitCounterManager.Android
{
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
    }
}
*/