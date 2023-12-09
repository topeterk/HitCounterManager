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

#if SHOW_COMPLER_VERSION // enable and hover over #error to see C# compiler version and the used language version
#error version
#endif

namespace HitCounterManager
{
    public enum TimerIDs : uint { Shortcuts, ShortcutsCapture, GameTime, GameTimeGui }

    public partial class App : Application
    {
        private IntPtr NativeWindowHandle = IntPtr.Zero;
        public ProfileViewViewModel? profileViewViewModel { get; private set; }
        public readonly OutModule om;
        public readonly Shortcuts sc = new Shortcuts();
        public SettingsRoot Settings { get; internal set; }
        public bool SettingsDialogOpen = false;
        private bool IsCleanStart = true;

        private static readonly IntPtr SubclassID = new IntPtr(0x48434D); // ASCII = "HCM"
        private bool SubclassprocInstalled = false;
        private NativeApi.Subclassproc? _Subclassproc = null;
        private static NativeApi.HookProc? _HookProc = null;

        private readonly Dictionary<TimerIDs, DispatcherTimer> ApplicationTimers = new Dictionary<TimerIDs, DispatcherTimer>();
        private IDisposable? UpdateCheckTimer;
        public WindowNotificationManager? NotificationManager;

        public App()
        {
            LoadSettings();
            om = new OutModule(Settings);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
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

        private struct PostponedAlert
        {
            public string Title;
            public string Message;
            public NotificationType Type;

            public PostponedAlert(string Title, string Message, NotificationType Type)
            {
                this.Title = Title;
                this.Message = Message;
                this.Type = Type;
            }
        }
        private List<PostponedAlert>? PostponedAlerts = new List<PostponedAlert>();

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

        public void CheckAndShowUpdates(MainPageViewModel vm)
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

        [SupportedOSPlatform("windows")]
        private IntPtr SubclassprocFunc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, IntPtr uIdSubclass, IntPtr dwRefData) // like WndProc
        {
            App app = CurrentApp;

            if (uMsg == NativeApi.WM_HOTKEY)
            {
                if (!app.SettingsDialogOpen)
                {
                    // Console.WriteLine("WinProc: " + ((Shortcuts.SC_Type)wParam).ToString() +"!");
                    switch ((Shortcuts.SC_Type)wParam)
                    {
                        case Shortcuts.SC_Type.SC_Type_Reset: app.profileViewViewModel?.ProfileReset.Execute(null); break;
                        case Shortcuts.SC_Type.SC_Type_Hit: app.profileViewViewModel?.HitIncrease.Execute(null); break;
                        case Shortcuts.SC_Type.SC_Type_Split: app.profileViewViewModel?.SplitSelectNext.Execute(null); break;
                        case Shortcuts.SC_Type.SC_Type_HitUndo: app.profileViewViewModel?.HitDecrease.Execute(null); break;
                        case Shortcuts.SC_Type.SC_Type_SplitPrev: app.profileViewViewModel?.SplitSelectPrev.Execute(null); break;
                        case Shortcuts.SC_Type.SC_Type_WayHit: app.profileViewViewModel?.HitWayIncrease.Execute(null); break;
                        case Shortcuts.SC_Type.SC_Type_WayHitUndo: app.profileViewViewModel?.HitWayDecrease.Execute(null); break;
                        case Shortcuts.SC_Type.SC_Type_PB: app.profileViewViewModel?.ProfilePB.Execute(null); break;
                        case Shortcuts.SC_Type.SC_Type_TimerStart: if (null != app.profileViewViewModel) app.profileViewViewModel.TimerRunning = true; break;
                        case Shortcuts.SC_Type.SC_Type_TimerStop: if (null != app.profileViewViewModel) app.profileViewViewModel.TimerRunning = false; break;
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
            Dictionary<TimerIDs, DispatcherTimer> timers = ApplicationTimers;

            if (timers.ContainsKey(ID))
            {
                timers[ID].Stop();
                timers.Remove(ID);
            }

            DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.MaxValue) { Interval = TimeSpan.FromMilliseconds(Interval) };
            timer.Tick += (s, e) => { if (!Callback()) timer.Stop(); };
            timers.Add(ID, timer);
            timer.Start();

            return true;
        }

        /// <summary>
        /// Stops and removes the timer
        /// </summary>
        /// <param name="ID">The timer ID</param>
        public void StopApplicationTimer(TimerIDs ID)
        {
            Dictionary<TimerIDs, DispatcherTimer> timers = ApplicationTimers;

            if (timers.ContainsKey(ID))
            {
                timers[ID].Stop();
                timers.Remove(ID);
            }
        }

        public override void OnFrameworkInitializationCompleted()
        {
            MainPage? mainPage = null;
            RequestedThemeVariant = Settings.DarkMode ? ThemeVariant.Dark : ThemeVariant.Light;

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                MainWindow main = new MainWindow();
                mainPage = main.InnerPage;
                profileViewViewModel = (ProfileViewViewModel?)mainPage.ProfileView?.DataContext;
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
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    SubclassprocInstalled = 0 != NativeApi.SetWindowSubclass(NativeWindowHandle, _Subclassproc!, SubclassID, IntPtr.Zero);
                }
                bool Success = LoadAllHotKeySettings();
                if (!Success) DisplayAlert("Error setting up hot keys!", "Not all enabled hot keys could be registered successfully!");
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                //MainPage main = new MainPage();
                SingleViewNavigationPage main = new SingleViewNavigationPage();
                mainPage = main.InnerPage;
                singleViewPlatform.MainView = main;
                profileViewViewModel = (ProfileViewViewModel?)mainPage.ProfileView?.DataContext;
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
        private bool IsDarkModeActive() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? NativeApi.IsDarkModeActiveWin32() : false;

        private bool IsTitleBarOnScreen(Screens screens, int Left, int Top, int Width, int Threshold = 10, int RectSize = 30)
        {
            PixelRect rectLeft = new PixelRect(Left + Threshold, Top + Threshold, RectSize, RectSize); // upper left corner
            PixelRect rectRight = new PixelRect(Left + Width - Threshold - RectSize, Top + Threshold, RectSize, RectSize); // upper right corner
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

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
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
            if (Resources.ContainsKey(BrushName) && Resources.ContainsKey(ColorName))
                (Resources[BrushName] as SolidColorBrush)!.Color = (Color)Resources[ColorName]!;
#if DEBUG
            else
                throw new Exception("The brush \"" + BrushName + "\" cannot be updated with the color \"" + ColorName + "\"!");
#endif
        }

        /// <summary>
        /// Indicates if this OS is capable of global hotkeys
        /// </summary>
        public bool GlobalHotKeySupport => RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? true : false;

        /// <summary>
        /// Retrieves the name of a given key
        /// </summary>
        /// <param name="KeyCode">Key name to gather</param>
        /// <returns>Key name</returns>
        public string GetKeyName(int KeyCode)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                string lpKeyNameString = new string('\0', 256);

                uint lParam = NativeApi.MapVirtualKeyW((uint)KeyCode, NativeApi.MAPVK_VK_TO_VSC) << 16;
                if (0 != NativeApi.GetKeyNameTextW((int)lParam, lpKeyNameString, lpKeyNameString.Length))
                    return lpKeyNameString.Trim('\0');
            }
            return "?";
        }

        /// <summary>
        /// Registers a hot key
        /// </summary>
        /// <param name="WindowHandle">Window handle that is receiving the hotkey message</param>
        /// <param name="HotKeyID">Custom ID for the hotkey</param>
        /// <param name="Modifiers">Key modifiers</param>
        /// <param name="KeyCode">Key to register</param>
        /// <returns>Success state</returns>
        public bool SetHotKey(IntPtr WindowHandle, int HotKeyID, uint Modifiers, int KeyCode)
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? (0 != NativeApi.RegisterHotKey(WindowHandle, HotKeyID, Modifiers, (uint)KeyCode)) : false;
        }

        /// <summary>
        /// Unregisters a hot key
        /// </summary>
        /// <param name="WindowHandle">Window handle that is receiving the hotkey message</param>
        /// <param name="HotKeyID">Custom ID for the hotkey</param>
        /// <returns>Success state</returns>
        public bool KillHotKey(IntPtr WindowHandle, int HotKeyID)
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? (0 != NativeApi.UnregisterHotKey(WindowHandle, HotKeyID)) : false;
        }

        private static IntPtr _HookId = IntPtr.Zero;
        private static bool[] KeyStates = new bool[256];

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
        public List<int>? GetKeysPressedAsync()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                List<int> result = new List<int>();

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
        public bool IsKeyPressedAsync(int KeyCode)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if ((_HookId != IntPtr.Zero) && (KeyStates.Length < KeyCode))
                {
                    // use the values from the low level keyboard hook instead
                    return KeyStates[KeyCode];
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
        public bool StartKeyboardLowLevelHook()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (_HookId != IntPtr.Zero) return false; // Only one instance supported

                // Reset the keystates, as their correct states are collected on the way
                for (int i = 0; i < KeyStates.Length; i++) KeyStates[i] = false;

                ProcessModule? module = Process.GetCurrentProcess().MainModule;
                if (null == module || null == module.ModuleName) return false;
                _HookId = NativeApi.SetWindowsHookEx(NativeApi.WH_KEYBOARD_LL, _HookProc!, NativeApi.GetModuleHandleW(module.ModuleName), 0);
                return _HookId == IntPtr.Zero ? false : true;
            }

            return false;
        }

        [SupportedOSPlatform("windows")]
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (NativeApi.HC_ACTION == nCode)
            {
                switch ((int)wParam)
                {
                    case NativeApi.WM_KEYDOWN:
                    case NativeApi.WM_KEYUP:
                    case NativeApi.WM_SYSKEYDOWN:
                    case NativeApi.WM_SYSKEYUP:
                        {
                            int KeyCode = Marshal.ReadInt32(lParam); // KBDLLHOOKSTRUCT.vkCode
                            int flags = Marshal.ReadInt32(lParam, 8); // KBDLLHOOKSTRUCT.flags

                            if (KeyStates.Length <= KeyCode) break; // Invalid value

                            bool isPressed = ((flags & NativeApi.LLKHF_UP) == 0 ? true : false); //transition state: 0 = pressed, 1 = released
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
        public bool StopKeyboardLowLevelHook()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return _HookId != IntPtr.Zero ? (0 != NativeApi.UnhookWindowsHookEx(_HookId)) : true;
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
        public IntPtr SendHotKeyMessage(IntPtr WindowHandle, IntPtr wParam, IntPtr lParam)
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? NativeApi.SendMessageW(WindowHandle, NativeApi.WM_HOTKEY, wParam, lParam) : IntPtr.Zero;
        }
    }
}
