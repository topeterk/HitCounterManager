//MIT License

//Copyright (c) 2021-2025 Peter Kirmeier

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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Notifications;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Styling;
using Avalonia.Threading;
using HitCounterManager.Common;
using HitCounterManager.Models;
using HitCounterManager.Views;
using HitCounterManager.ViewModels;

#if SHOW_COMPILER_VERSION // enable and hover over #error to see C# compiler version and the used language version
#error version
#endif

namespace HitCounterManager
{
    public enum TimerIDs : uint { Shortcuts, ShortcutsCapture, GameTime, GameTimeGui }

    public partial class App : Application
    {
        private IntPtr NativeWindowHandle = IntPtr.Zero;
        public ProfileViewViewModel? ProfileViewViewModel { get; private set; }
        public readonly OutModule om;
        public readonly Shortcuts sc = new ();
        public SettingsRoot Settings { get; internal set; }
        public bool SettingsDialogOpen = false;
        private bool IsCleanStart = true;

        private static readonly IntPtr SubclassID = new (0x48434D); // ASCII = "HCM"
        private bool SubclassprocInstalled = false;
        private readonly NativeApi.Subclassproc? _Subclassproc = null;
        private static NativeApi.HookProc? _HookProc = null;

        private readonly Dictionary<TimerIDs, DispatcherTimer> ApplicationTimers = [];
        private IDisposable? UpdateCheckTimer;
        public WindowNotificationManager? NotificationManager;

        public App()
        {
            LoadSettings();
            om = new OutModule(Settings);
            if (!om.ReloadTemplate())
                DisplayAlert("Error loading template!", "The file " + (string.IsNullOrEmpty(Settings.Inputfile) ? "<Inputfile not set>" : "\"" + Settings.Inputfile + "\"") + " not found!");

            if (OperatingSystem.IsWindows())
            {
                _Subclassproc = new NativeApi.Subclassproc(SubclassprocFunc); // store delegate to prevent garbage collector from freeing it
                _HookProc ??= new NativeApi.HookProc(HookCallback); // store delegate to prevent garbage collector from freeing it
            }
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            ApplyTheme(Settings.DarkMode); // Initially update values according to selected mode
        }

        private struct PostponedAlert(string Title, string Message, NotificationType Type)
        {
            public string Title = Title;
            public string Message = Message;
            public NotificationType Type = Type;
        }
        private List<PostponedAlert>? PostponedAlerts = [];

        /// <summary>
        /// Page.DisplayAlert but when no appeared yet, it will be postponed until MainPage has appeared
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Message"></param>
        /// <param name="Type"></param>
        public void DisplayAlert(string Title, string Message, NotificationType Type = NotificationType.Error)
        {
            if (null == PostponedAlerts)
                NotificationManager?.Show(new Notification(Title, Message, Type, TimeSpan.FromSeconds(10)));
            else
                PostponedAlerts.Add(new PostponedAlert(Title, Message, Type));
        }

        internal void MainPageAppearing(object? sender, EventArgs e)
        {
            if (null != PostponedAlerts)
            {
                List<PostponedAlert>? alerts = PostponedAlerts;
                PostponedAlerts = null;
                foreach (PostponedAlert Alert in alerts) DisplayAlert(Alert.Title, Alert.Message, Alert.Type);
            }
        }

        public static void CheckAndShowUpdates(MainPageViewModel vm)
        {
            // Get latest data
            if (GitHubUpdate.QueryAllReleases())
            {
                // When we are not the latest version, switch to the update page
                if (GitHubUpdate.LatestVersionName != Statics.ApplicationVersionString)
                {
                    vm.OpenPageUpdate.Execute(null);
                }
            }
        }

        [SupportedOSPlatform("Windows")]
        private IntPtr SubclassprocFunc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, IntPtr uIdSubclass, IntPtr dwRefData) // like WndProc
        {
            App app = CurrentApp;

            if (uMsg == NativeApi.WM_HOTKEY)
            {
                if (!app.SettingsDialogOpen)
                {
                    // Console.WriteLine("WinProc: " + ((SC_Type)wParam).ToString() +"!");
                    switch ((SC_Type)wParam)
                    {
                        case SC_Type.SC_Type_Reset: app.ProfileViewViewModel?.ProfileReset.Execute(null); break;
                        case SC_Type.SC_Type_Hit: app.ProfileViewViewModel?.HitIncrease.Execute(null); break;
                        case SC_Type.SC_Type_Split: app.ProfileViewViewModel?.SplitSelectNext.Execute(null); break;
                        case SC_Type.SC_Type_HitUndo: app.ProfileViewViewModel?.HitDecrease.Execute(null); break;
                        case SC_Type.SC_Type_SplitPrev: app.ProfileViewViewModel?.SplitSelectPrev.Execute(null); break;
                        case SC_Type.SC_Type_WayHit: app.ProfileViewViewModel?.HitWayIncrease.Execute(null); break;
                        case SC_Type.SC_Type_WayHitUndo: app.ProfileViewViewModel?.HitWayDecrease.Execute(null); break;
                        case SC_Type.SC_Type_PB: app.ProfileViewViewModel?.ProfilePB.Execute(null); break;
                        case SC_Type.SC_Type_TimerStart: if (app.ProfileViewViewModel is not null) app.ProfileViewViewModel.TimerRunning = true; break;
                        case SC_Type.SC_Type_TimerStop: if (app.ProfileViewViewModel is not null) app.ProfileViewViewModel.TimerRunning = false; break;
                        case SC_Type.SC_Type_HitBossPrev: app.ProfileViewViewModel?.HitIncreasePrev.Execute(null); break;
                        case SC_Type.SC_Type_BossHitUndoPrev: app.ProfileViewViewModel?.HitDecreasePrev.Execute(null); break;
                        case SC_Type.SC_Type_HitWayPrev: app.ProfileViewViewModel?.HitWayIncreasePrev.Execute(null); break;
                        case SC_Type.SC_Type_WayHitUndoPrev: app.ProfileViewViewModel?.HitWayDecreasePrev.Execute(null); break;
                        #region AutoSplitter
                        case SC_Type.SC_Type_Practice: if (app.ProfileViewViewModel is not null) app.ProfileViewViewModel.AutoSplitterPracticeModeChecked = !app.ProfileViewViewModel.AutoSplitterPracticeModeChecked; break;
                        #endregion
                        default: break;
                    }
                }
                // else Console.WriteLine("WinProc: Settings open!");
            }

            return NativeApi.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        /// <summary>
        /// App instance of the running application
        /// </summary>
        public static App CurrentApp { get => (App)Current!; }

        /// <summary>
        /// Creates a timer repeatedly calling the callback upon timeout.
        /// Depending on the callbacks return, the timer keeps running (true) or will be stopped (false)
        /// </summary>
        /// <param name="ID">The timer ID (one ID cannot be started multiple times)</param>
        /// <param name="Interval">Timeout in milliseconds</param>
        /// <param name="Callback">Callback function pointer</param>
        /// <returns>Success state</returns>
        public bool StartApplicationTimer(TimerIDs ID, double Interval, Func<bool> Callback)
        {
            StopApplicationTimer(ID);

            DispatcherTimer timer = new (DispatcherPriority.MaxValue) { Interval = TimeSpan.FromMilliseconds(Interval) };
            timer.Tick += (s, e) => { if (!Callback()) timer.Stop(); };
            ApplicationTimers.Add(ID, timer);
            timer.Start();

            return true;
        }

        /// <summary>
        /// Stops and removes the timer
        /// </summary>
        /// <param name="ID">The timer ID</param>
        public void StopApplicationTimer(TimerIDs ID)
        {
            if (ApplicationTimers.Remove(ID, out var oldTimer))
            {
                oldTimer.Stop();
            }
        }

        public override void OnFrameworkInitializationCompleted()
        {
            MainPage? mainPage = null;
            RequestedThemeVariant = Settings.DarkMode ? ThemeVariant.Dark : ThemeVariant.Light;

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                #region AutoSplitter

                AutoSplitterCoreModule.AutoSplitterCoreTryLoadModule();

                #endregion

                MainWindow main = new ();
                mainPage = main.InnerPage;
                ProfileViewViewModel = (ProfileViewViewModel?)mainPage.ProfileView?.DataContext;
                main.Opened += MainPageAppearing;

                desktop.MainWindow = main;
                NativeWindowHandle = desktop.MainWindow.TryGetPlatformHandle()?.Handle ?? default;
                if (!IsCleanStart && IsTitleBarOnScreen(desktop.MainWindow.Screens, Settings.MainPosX, Settings.MainPosY, Settings.MainWidth))
                {
                    // Set window position when application is not started the first time and window would not end up outside of all screens
                    desktop.MainWindow.Position = new PixelPoint(Settings.MainPosX, Settings.MainPosY);
                }
                desktop.Exit += AppExitHandler;

                // Register the hot key handler and hot keys
                if (OperatingSystem.IsWindows())
                {
                    SubclassprocInstalled = 0 != NativeApi.SetWindowSubclass(NativeWindowHandle, _Subclassproc!, SubclassID, IntPtr.Zero);
                }
                bool Success = LoadAllHotKeySettings();
                if (!Success) DisplayAlert("Error setting up hot keys!", "Not all enabled hot keys could be registered successfully!");
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                //MainPage main = new MainPage();
                SingleViewNavigationPage main = new ();
                mainPage = main.InnerPage;
                singleViewPlatform.MainView = main;
                ProfileViewViewModel = (ProfileViewViewModel?)mainPage.ProfileView?.DataContext;
            }

            // Check for updates..
            if (Settings.CheckUpdatesOnStartup && mainPage is not null)
            {
                // Run check later in the background, so offline startup can proceed faster
                UpdateCheckTimer = DispatcherTimer.RunOnce(
                    () => Dispatcher.UIThread.Post(() => CheckAndShowUpdates(mainPage.ViewModel)),
                    TimeSpan.FromSeconds(4),
                    DispatcherPriority.ApplicationIdle);
            }

            base.OnFrameworkInitializationCompleted();
        }

        /// <summary>
        /// Read the OS setting whether dark mode is enabled
        /// </summary>
        /// <returns>true = Dark mode; false = Light mode</returns>
        private static bool IsDarkModeActive() => OperatingSystem.IsWindows() && NativeApi.IsDarkModeActiveWin32();

        private static bool IsTitleBarOnScreen(Screens screens, int Left, int Top, int Width, int Threshold = 10, int RectSize = 30)
        {
            PixelRect rectLeft = new (Left + Threshold, Top + Threshold, RectSize, RectSize); // upper left corner
            PixelRect rectRight = new (Left + Width - Threshold - RectSize, Top + Threshold, RectSize, RectSize); // upper right corner
            foreach (Screen screen in screens.All)
            {
                // at least one of the edges must be present on any screen
                if (screen.WorkingArea.Contains(rectLeft)) return true;
                else if (screen.WorkingArea.Contains(rectRight)) return true;
            }
            return false;
        }

        public void AppExitHandler(object? sender, EventArgs e)
        {
            foreach ((TimerIDs _, DispatcherTimer timer) in ApplicationTimers) timer.Stop();

            UpdateCheckTimer?.Dispose();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                if (OperatingSystem.IsWindows())
                {
                    // store false then successful as then it is no longer installed
                    if (SubclassprocInstalled)
                    {
                        SubclassprocInstalled = 0 == NativeApi.RemoveWindowSubclass(NativeWindowHandle, _Subclassproc!, SubclassID);
                    }
                }
            }
        }

        public void ApplyTheme(bool DarkMode)
        {
            Settings.DarkMode = DarkMode;

            string ModeName = DarkMode ? "Dark" : "Light";

            UpdateBrushColor("ImageButtonBackgroundBrush", "ImageButtonBackgroundColor" + ModeName);
            UpdateBrushColor("DialogWindowBackgroundBrush", "DialogWindowBackgroundColor" + ModeName);
            UpdateBrushColor("MainWindowBackgroundBrush", "MainWindowBackgroundColor" + ModeName);
            UpdateBrushColor("TextBoxBackgroundBrush", "TextBoxBackgroundColor" + ModeName);

            CurrentApp.RequestedThemeVariant = DarkMode ? ThemeVariant.Dark : ThemeVariant.Light;
        }

        private void UpdateBrushColor(string BrushName, string ColorName)
        {
            if (Resources.TryGetValue(BrushName, out var resourceBrush) && Resources.TryGetValue(ColorName, out var resourceColor))
                (resourceBrush as SolidColorBrush)!.Color = (Color)resourceColor!;
#if DEBUG
            else
                throw new Exception("The brush \"" + BrushName + "\" cannot be updated with the color \"" + ColorName + "\"!");
#endif
        }

        /// <summary>
        /// Registers a hot key
        /// </summary>
        /// <param name="WindowHandle">Window handle that is receiving the hotkey message</param>
        /// <param name="HotKeyID">Custom ID for the hotkey</param>
        /// <param name="Modifiers">Key modifiers</param>
        /// <param name="KeyCode">Key to register</param>
        /// <returns>Success state</returns>
        public static bool SetHotKey(IntPtr WindowHandle, int HotKeyID, uint Modifiers, VirtualKeyStates KeyCode)
        {
            return OperatingSystem.IsWindows() && (0 != NativeApi.RegisterHotKey(WindowHandle, HotKeyID, Modifiers, (uint)KeyCode));
        }

        /// <summary>
        /// Unregisters a hot key
        /// </summary>
        /// <param name="WindowHandle">Window handle that is receiving the hotkey message</param>
        /// <param name="HotKeyID">Custom ID for the hotkey</param>
        /// <returns>Success state</returns>
        public static bool KillHotKey(IntPtr WindowHandle, int HotKeyID)
        {
            return OperatingSystem.IsWindows() && (0 != NativeApi.UnregisterHotKey(WindowHandle, HotKeyID));
        }

        private static IntPtr _HookId = IntPtr.Zero;
        private static readonly bool[] KeyStates = new bool[256];

        /// <summary>
        /// Fires when a low level keyboard event designates a key state changes.
        /// Must be enabled/disabled via StartKeyboardLowLevelHook/StopKeyboardLowLevelHook.
        /// Keep execution time short as possible!
        /// </summary>
        public event Action? LowLevelKeyboardEvent;

        /// <summary>
        /// Checks if any keys are pressed right now.
        /// Key state is read at the moment of this call when the low level keybaord events are disabled.
        /// Gets the key state from internal cache when the low level keybaord events are enabled.
        /// </summary>
        /// <returns>null on error; otherwise a list of KeyCodes of all currently pressed keys</returns>
        public static List<int>? GetKeysPressedAsync()
        {
            if (OperatingSystem.IsWindows())
            {
                List<int> result = [];

                if (_HookId != IntPtr.Zero)
                {
                    // use the values from the low level keyboard hook instead
                    for (int KeyCode = 0; KeyCode < KeyStates.Length; KeyCode++)
                        if (KeyStates[KeyCode]) result.Add(KeyCode);
                }
                else
                {
                    byte[] buffer = new byte[256];
                    if (0 == NativeApi.GetKeyboardState(buffer)) return null;
                    for (int KeyCode = 0; KeyCode < buffer.Length; KeyCode++)
                        if (0 != (buffer[KeyCode] & 0x80 /*key is down flag*/)) result.Add(KeyCode);
                }

                return result;
            }

            return null;
        }

        /// <summary>
        /// Checks if a given key is pressed right now.
        /// Key state is read at the moment of this call when the low level keybaord events are disabled.
        /// Gets the key state from internal cache when the low level keybaord events are enabled.
        /// </summary>
        /// <param name="KeyCode">The key to check</param>
        /// <returns>true = pressed, false = released</returns>
        public static bool IsKeyPressedAsync(VirtualKeyStates KeyCode)
        {
            if (OperatingSystem.IsWindows())
            {
                if ((_HookId != IntPtr.Zero) && (KeyStates.Length < (int)KeyCode))
                {
                    // use the values from the low level keyboard hook instead
                    return KeyStates[(int)KeyCode];
                }

                return 0 != (NativeApi.GetAsyncKeyState(KeyCode) & NativeApi.KEY_PRESSED_NOW);
            }

            return false;
        }

        /// <summary>
        /// Start using the low level keyboard events.
        /// This enables the LowLevelKeyboardEvent on the same thread that is calling this function.
        /// The states for IsKeyPressedAsync will be taken from internal cache.
        /// </summary>
        /// <returns>Success state</returns>
        public static bool StartKeyboardLowLevelHook()
        {
            if (OperatingSystem.IsWindows())
            {
                if (_HookId != IntPtr.Zero) return false; // Only one instance supported

                // Reset the keystates, as their correct states are collected on the way
                for (int i = 0; i < KeyStates.Length; i++) KeyStates[i] = false;

                ProcessModule? module = Process.GetCurrentProcess().MainModule;
                if (null == module || null == module.ModuleName) return false;
                _HookId = NativeApi.SetWindowsHookEx(NativeApi.WH_KEYBOARD_LL, _HookProc!, NativeApi.GetModuleHandleW(module.ModuleName), 0);
                return _HookId != IntPtr.Zero;
            }

            return false;
        }

        [SupportedOSPlatform("Windows")]
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (NativeApi.HC_ACTION == nCode)
            {
                switch (wParam)
                {
                    case NativeApi.WM_KEYDOWN:
                    case NativeApi.WM_KEYUP:
                    case NativeApi.WM_SYSKEYDOWN:
                    case NativeApi.WM_SYSKEYUP:
                        {
                            int KeyCode = Marshal.ReadInt32(lParam); // KBDLLHOOKSTRUCT.vkCode
                            int flags = Marshal.ReadInt32(lParam, 8); // KBDLLHOOKSTRUCT.flags

                            if (KeyStates.Length <= KeyCode) break; // Invalid value

                            bool isPressed = (flags & NativeApi.LLKHF_UP) == 0; //transition state: 0 = pressed, 1 = released
                            if (KeyStates[KeyCode] != isPressed) // drop this event, we already know this!
                            {
                                KeyStates[KeyCode] = isPressed;
                                LowLevelKeyboardEvent?.Invoke(); // Fire event
                            }
                        }
                        break;
                    default: break;
                }
            }
            return NativeApi.CallNextHookEx(_HookId, nCode, wParam, lParam);
        }

        /// <summary>
        /// Stops using the the low level keyboard events.
        /// This disables the LowLevelKeyboardEvent
        /// </summary>
        /// <returns>Success state</returns>
        public static bool StopKeyboardLowLevelHook()
        {
            if (OperatingSystem.IsWindows())
            {
                return _HookId == IntPtr.Zero || (0 != NativeApi.UnhookWindowsHookEx(_HookId));
            }
            return false;
        }

        /// <summary>
        /// Sends an hotkey message to the given window
        /// </summary>
        /// <param name="WindowHandle">Window handle that is receiving the hotkey message</param>
        /// <param name="wParam">Parameter 1 of message</param>
        /// <param name="lParam">Parameter 2 of message</param>
        /// <returns>Message result</returns>
        public static IntPtr SendHotKeyMessage(IntPtr WindowHandle, IntPtr wParam, IntPtr lParam)
        {
            return OperatingSystem.IsWindows() ? NativeApi.SendMessageW(WindowHandle, NativeApi.WM_HOTKEY, wParam, lParam) : IntPtr.Zero;
        }
    }
}
