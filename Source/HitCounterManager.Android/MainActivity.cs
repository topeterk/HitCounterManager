// SPDX-FileCopyrightText: © 2023-2026 Peter Kirmeier
// SPDX-License-Identifier: MIT

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using AndroidX.Activity;
using Avalonia;
using Avalonia.Android;
using Avalonia.Controls.ApplicationLifetimes;
using HitCounterManager.Common;
using HitCounterManager.Views;
using System.IO;

namespace HitCounterManager.Android;

[Activity(
    Label = "HitCounterManager.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity
{
    private readonly BackPressedCallback _currentBackPressedCallback;

    public MainActivity()
    {
        _currentBackPressedCallback = new BackPressedCallback(this);
        OnBackPressedDispatcher.AddCallback(this, _currentBackPressedCallback);
    }

    protected override void OnStop()
    {
        // Save when application becomes invisible
        App.CurrentApp.SaveSettings();
        base.OnStop();
    }

    protected override void OnDestroy()
    {
        App.CurrentApp.AppExitHandler(this, new());
        base.OnDestroy();
    }
}

internal class BackPressedCallback(AvaloniaActivity activity) : OnBackPressedCallback(true)
{
    public override void HandleOnBackPressed()
    {
        if (App.CurrentApp.ApplicationLifetime is IActivityApplicationLifetime activityLifetime)
        {
            // As we do not spawn any new pages, we just use the factory method to get access to our single root page
            SingleViewNavigationPage RootPage = (SingleViewNavigationPage)activityLifetime.MainViewFactory!();
            if (1 < RootPage.NavStack.Count)
            {
                RootPage.PopPage();
                return;
            }

#if EXAMPLE_FORCED_SHUTDOWN
            // Idea of stopping the process by default but this will popup the message box
            // every time one is pressing the hit back button. Therefore it is better to keep
            // the application running in the background.
            // We will save the current state during OnStop().
            RootPage.MainPage.OnClosing(this, new CancelEventArgs());
            App.CurrentApp.AppExitHandler(this, null);
            Finish();
#endif
        }

        // run default system handler..
        this.Enabled = false; // Do not run our handler again indefinitely
        activity.OnBackPressedDispatcher?.OnBackPressed();
        this.Enabled = true; // Restore handler
    }
}

[Application]
public class AndroidApp : AvaloniaAndroidApplication<App>
{
    protected AndroidApp(nint javaReference, JniHandleOwnership transfer)
        : base(javaReference, transfer)
    {
    }

    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        // Browse files on VS emulator:
        //   Extra > Android > Pompt
        //     adb devices -l
        //     adb -s emulator-5554 shell
        //     su 0
        // https://developer.android.com/training/data-storage/app-specific
        // https://developer.android.com/guide/topics/resources/providing-resources
        var documentsDir = GetExternalFilesDir(Environment.DirectoryDocuments);
        if (documentsDir is not null && documentsDir.Exists())
        {
            string storageDir = documentsDir.AbsolutePath;
            Statics.ApplicationStorageDir = storageDir;

            // Copy default template to storage directory when not already existing
            string templateFilepath = new SettingsRoot().Inputfile;
            if (!Path.IsPathRooted(templateFilepath))
            {
                templateFilepath = Path.Combine(storageDir, templateFilepath);
            }
            if (!File.Exists(templateFilepath))
            {
                try
                {
                    Stream? templateResource = Resources?.OpenRawResource(Android.Resource.Raw.hitcounter);
                    if (templateResource is not null)
                    {
                        FileStream? fileStream = File.Create(templateFilepath);
                        if (fileStream is not null)
                        {
                            templateResource.CopyTo(fileStream);
                            fileStream.Close();
                        }
                    }
                }
                catch { }
            }
        }

        return base.CustomizeAppBuilder(builder)
            .WithInterFont();
    }
}
