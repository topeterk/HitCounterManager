//MIT License

//Copyright (c) 2021-2022 Peter Kirmeier

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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Microsoft.Win32;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Notifications;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using Avalonia.Threading;
using HitCounterManager.Common;
using HitCounterManager.Models;
using HitCounterManager.Views;
using HitCounterManager.ViewModels;
using Avalonia.Controls.Templates;

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
        public readonly OutModule om = new OutModule();
        public readonly Shortcuts sc = new Shortcuts();
        public SettingsRoot Settings { get; internal set; }
        public bool SettingsDialogOpen = false;
        private bool IsCleanStart = true;

        private static readonly IntPtr SubclassID = new IntPtr(0x48434D); // ASCII = "HCM"
        private bool SubclassprocInstalled = false;
        private Subclassproc? _Subclassproc = null;
        private static HookProc? _HookProc = null;

        private readonly Dictionary<TimerIDs, DispatcherTimer> ApplicationTimers = new Dictionary<TimerIDs, DispatcherTimer>();
        private IDisposable? UpdateCheckTimer;
        private WindowNotificationManager? NotificationManager;

        public App()
        {
            LoadSettings();
            // TODO: Check if we can create OutModule after Settings were loaded?
            om.Settings = Settings!;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _Subclassproc = new Subclassproc(SubclassprocFunc); // store delegate to prevent garbage collector from freeing it
                _HookProc ??= new HookProc(HookCallback); // store delegate to prevent garbage collector from freeing it
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
        /// <param name="Cancel"></param>
        /// <param name="Type"></param>
        public void DisplayAlert(string Title, string Message, NotificationType Type = NotificationType.Error)
        {
            if (null == PostponedAlerts)
                NotificationManager?.Show(new Notification(Title, Message, Type, TimeSpan.FromSeconds(10)));
            else
                PostponedAlerts.Add(new PostponedAlert(Title, Message, Type));
        }

        private void MainPageAppearing(object? sender, EventArgs e)
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

#region Windows API

        // Datatypes: https://docs.microsoft.com/en-us/windows/win32/winprog/windows-data-types

#region Window Subclass

        private const int WM_HOTKEY = 0x0312;

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/commctrl/nc-commctrl-subclassproc
        /// </summary>
        /// <param name="hWnd">HWND = HANDLE = PVOID = void*</param>
        /// <param name="uMsg">UINT = unsigned int</param>
        /// <param name="wParam">WPARAM = UINT_PTR = __int64 or long</param>
        /// <param name="lParam">LPARAM = LONG_PTR = __int64 or long</param>
        /// <param name="uIdSubclass">UINT_PTR = __int64 or long</param>
        /// <param name="dwRefData">DWORD_PTR = ULONG_PTR = __int64 or long</param>
        /// <returns>LRESULT = LONG_PTR = __int64 or long</returns>
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        private delegate IntPtr Subclassproc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, IntPtr uIdSubclass, IntPtr dwRefData);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/commctrl/nf-commctrl-setwindowsubclass
        /// </summary>
        /// <param name="hWnd">HWND = HANDLE = PVOID = void*</param>
        /// <param name="pfnSubclass">SUBCLASSPROC</param>
        /// <param name="uIdSubclass">UINT_PTR = __int64 or long</param>
        /// <param name="dwRefData">DWORD_PTR = ULONG_PTR = __int64 or long</param>
        /// <returns>BOOL = int</returns>
        [SupportedOSPlatform("windows")]
        [DllImport("Comctl32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        private static extern int SetWindowSubclass(IntPtr hWnd, Subclassproc pfnSubclass, IntPtr uIdSubclass, IntPtr dwRefData);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/commctrl/nf-commctrl-removewindowsubclass
        /// </summary>
        /// <param name="hWnd">HWND = HANDLE = PVOID = void*</param>
        /// <param name="pfnSubclass">SUBCLASSPROC</param>
        /// <param name="uIdSubclass">UINT_PTR = __int64 or long</param>
        /// <returns>BOOL = int</returns>
        [SupportedOSPlatform("windows")]
        [DllImport("Comctl32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        private static extern int RemoveWindowSubclass(IntPtr hWnd, Subclassproc pfnSubclass, IntPtr uIdSubclass);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/commctrl/nf-commctrl-defsubclassproc
        /// </summary>
        /// <param name="hWnd">HWND = HANDLE = PVOID = void*</param>
        /// <param name="uMsg">UINT = unsigned int</param>
        /// <param name="wParam">WPARAM = UINT_PTR = __int64 or long</param>
        /// <param name="lParam">LPARAM = LONG_PTR = __int64 or long</param>
        /// <returns>LRESULT = LONG_PTR = __int64 or long</returns>
        [SupportedOSPlatform("windows")]
        [DllImport("Comctl32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr DefSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendmessagew
        /// </summary>
        /// <param name="hWnd">HWND = HANDLE = PVOID = void*</param>
        /// <param name="Msg">UINT = unsigned int</param>
        /// <param name="wParam">WPARAM = UINT_PTR = __int64 or long</param>
        /// <param name="lParam">LPARAM = LONG_PTR = __int64 or long</param>
        /// <returns>LRESULT = LONG_PTR = __int64 or long</returns>
        [SupportedOSPlatform("windows")]
        [DllImport("User32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr SendMessageW(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

#endregion

#region Window Message Hook

        private const int WH_KEYBOARD_LL = 13;

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexw
        /// </summary>
        /// <param name="idHook">int</param>
        /// <param name="lpfn">HOOKPROC</param>
        /// <param name="hMod">HINSTANCE = HANDLE = PVOID = void*</param>
        /// <param name="dwThreadId">DWORD = unsigned long</param>
        /// <returns>HHOOK = HANDLE = PVOID = void*</returns>
        [SupportedOSPlatform("windows")]
        [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-unhookwindowshookex
        /// </summary>
        /// <param name="hhk">HHOOK = HANDLE = PVOID = void*</param>
        /// <returns>BOOL = int</returns>
        [SupportedOSPlatform("windows")]
        [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        private static extern int UnhookWindowsHookEx(IntPtr hhk);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-callnexthookex
        /// </summary>
        /// <param name="hhk">HHOOK = HANDLE = PVOID = void*</param>
        /// <param name="nCode">int</param>
        /// <param name="wParam">WPARAM = UINT_PTR = __int64 or long</param>
        /// <param name="lParam">LPARAM = LONG_PTR = __int64 or long</param>
        /// <returns>LRESULT = LONG_PTR = __int64 or long</returns>
        [SupportedOSPlatform("windows")]
        [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-getmodulehandlew
        /// </summary>
        /// <param name="lpModuleName">LPCWSTR = CONST WCHAR * = const wchar_t *</param>
        /// <returns>HMODULE = HINSTANCE = HANDLE = PVOID = void*</returns>
        [SupportedOSPlatform("windows")]
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr GetModuleHandleW(string lpModuleName);

#endregion

#region Hot Key

        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;
        private const int HC_ACTION = 0;
        private const int KF_ALTDOWN = 0x2000;
        private const int KF_UP = 0x8000;
        private const int LLKHF_ALTDOWN = (KF_ALTDOWN >> 8);
        private const int LLKHF_UP = (KF_UP >> 8);
        private const int KEY_PRESSED_NOW = 0x8000;
        private const uint MAPVK_VK_TO_VSC = 0;

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-mapvirtualkeyw
        /// </summary>
        /// <param name="uCode">UINT = unsigned int</param>
        /// <param name="uMapType">UINT = unsigned int</param>
        /// <returns>UINT = unsigned int</returns>
        [SupportedOSPlatform("windows")]
        [DllImport("User32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        private static extern uint MapVirtualKeyW(uint uCode, uint uMapType);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeynametextw
        /// </summary>
        /// <param name="lParam">LONG = long</param>
        /// <param name="lpBuffer">LPWSTR = CONST WCHAR * = const wchar_t *</param>
        /// <param name="nSize">int</param>
        /// <returns>int</returns>
        [SupportedOSPlatform("windows")]
        [DllImport("User32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        private static extern int GetKeyNameTextW(int lParam, string lpBuffer, int nSize);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerhotkey
        /// </summary>
        /// <param name="hWnd">HWND = HANDLE = PVOID = void*</param>
        /// <param name="id">int</param>
        /// <param name="fsModifiers">UINT = unsigned int</param>
        /// <param name="vk">UINT = unsigned int</param>
        /// <returns>BOOL = int</returns>
        [SupportedOSPlatform("windows")]
        [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        private static extern int RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-unregisterhotkey
        /// </summary>
        /// <param name="hWnd">HWND = HANDLE = PVOID = void*</param>
        /// <param name="id">int </param>
        /// <returns>BOOL = int</returns>
        [SupportedOSPlatform("windows")]
        [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        private static extern int UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeyboardstate
        /// </summary>
        /// <param name="lpKeyState">PBYTE = BYTE * = unsigned char *</param>
        /// <returns>BOOL = int</returns>
        [SupportedOSPlatform("windows")]
        [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        static extern int GetKeyboardState(byte[] lpKeyState);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getasynckeystate
        /// </summary>
        /// <param name="vKey">int</param>
        /// <returns>SHORT = short</returns>
        [SupportedOSPlatform("windows")]
        [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        private static extern short GetAsyncKeyState(int vKey);

#endregion

#endregion

#region Windows Runtime Assembly

        [MethodImpl(MethodImplOptions.NoInlining)] // https://stackoverflow.com/questions/21914692/when-exactly-are-assemblies-loaded
        [SupportedOSPlatform("windows")]
        private bool IsDarkModeActiveWin32()
        {
            try
            {
                // Check: AppsUseLightTheme (REG_DWORD)
                // 0 = Dark mode, 1 = Light mode
                object? value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", "AppsUseLightTheme", 1);
                return null == value ? false : value.ToString() == "0";
            }
            catch
            {
                // Catch exceptions when key is not working on this system
                return false;
            }
        }

#endregion

        [SupportedOSPlatform("windows")]
        private IntPtr SubclassprocFunc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, IntPtr uIdSubclass, IntPtr dwRefData) // like WndProc
        {
            const int WM_HOTKEY = 0x312;
            App app = CurrentApp;

            if (uMsg == WM_HOTKEY)
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

            return DefSubclassProc(hWnd, uMsg, wParam, lParam);
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
            foreach (IStyle style in Styles)
            {
                if (style is FluentTheme theme)
                    theme.Mode = Settings.DarkMode ? FluentThemeMode.Dark : FluentThemeMode.Light;
            }

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                MainPage main = new MainPage();
                profileViewViewModel = (ProfileViewViewModel)main.ProfileView.DataContext!;
                main.Opened += MainPageAppearing;

                desktop.MainWindow = main;
                NotificationManager = new WindowNotificationManager(desktop.MainWindow) { Position = NotificationPosition.TopRight, MaxItems = 1 };
                NativeWindowHandle = desktop.MainWindow.PlatformImpl.Handle.Handle;
                if (!IsCleanStart && IsTitleBarOnScreen(desktop.MainWindow.Screens, Settings.MainPosX, Settings.MainPosY, Settings.MainWidth))
                {
                    // Set window position when application is not started the first time and window would not end up outside of all screens
                    desktop.MainWindow.Position = new PixelPoint(Settings.MainPosX, Settings.MainPosY);
                }
                desktop.Exit += AppExitHandler;

                // Register the hot key handler and hot keys
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    SubclassprocInstalled = 0 != SetWindowSubclass(NativeWindowHandle, _Subclassproc!, SubclassID, IntPtr.Zero);
                }
                bool Success = LoadAllHotKeySettings();
                if (!Success) DisplayAlert("Error setting up hot keys!", "Not all enabled hot keys could be registered successfully!");

                // Check for updates..
                if (Settings.CheckUpdatesOnStartup)
                {
                    // Run check later in the background, so offline startup can proceed faster
                    UpdateCheckTimer = DispatcherTimer.RunOnce(
                        () => Dispatcher.UIThread.Post(() => CheckAndShowUpdates((MainPageViewModel)main.DataContext!)),
                        TimeSpan.FromSeconds(4),
                        DispatcherPriority.ApplicationIdle);
                }
            }

            base.OnFrameworkInitializationCompleted();
        }

        /// <summary>
        /// Read the OS setting whether dark mode is enabled
        /// </summary>
        /// <returns>true = Dark mode; false = Light mode</returns>
        private bool IsDarkModeActive() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? IsDarkModeActiveWin32() : false;

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

        private void AppExitHandler(object? sender, ControlledApplicationLifetimeExitEventArgs e)
        {
            foreach ((TimerIDs _, DispatcherTimer timer) in ApplicationTimers) timer.Stop();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    // store false then successful as then it is no longer installed
                    if (SubclassprocInstalled)
                    {
                        SubclassprocInstalled = 0 == RemoveWindowSubclass(NativeWindowHandle, _Subclassproc!, SubclassID);
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

            foreach (IStyle style in Styles)
            {
                if (style is FluentTheme theme)
                    theme.Mode = DarkMode ? FluentThemeMode.Dark : FluentThemeMode.Light;
            }
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

                uint lParam = MapVirtualKeyW((uint)KeyCode, MAPVK_VK_TO_VSC) << 16;
                if (0 != GetKeyNameTextW((int)lParam, lpKeyNameString, lpKeyNameString.Length))
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
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? (0 != RegisterHotKey(WindowHandle, HotKeyID, Modifiers, (uint)KeyCode)) : false;
        }

        /// <summary>
        /// Unregisters a hot key
        /// </summary>
        /// <param name="WindowHandle">Window handle that is receiving the hotkey message</param>
        /// <param name="HotKeyID">Custom ID for the hotkey</param>
        /// <returns>Success state</returns>
        public bool KillHotKey(IntPtr WindowHandle, int HotKeyID)
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? (0 != UnregisterHotKey(WindowHandle, HotKeyID)) : false;
        }

        private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);
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
                    if (0 == GetKeyboardState(buffer)) return null;
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

                return 0 != (GetAsyncKeyState(KeyCode) & KEY_PRESSED_NOW);
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
                _HookId = SetWindowsHookEx(WH_KEYBOARD_LL, _HookProc!, GetModuleHandleW(module.ModuleName), 0);
                return _HookId == IntPtr.Zero ? false : true;
            }

            return false;
        }

        [SupportedOSPlatform("windows")]
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (HC_ACTION == nCode)
            {
                switch ((int)wParam)
                {
                    case WM_KEYDOWN:
                    case WM_KEYUP:
                    case WM_SYSKEYDOWN:
                    case WM_SYSKEYUP:
                        {
                            int KeyCode = Marshal.ReadInt32(lParam); // KBDLLHOOKSTRUCT.vkCode
                            int flags = Marshal.ReadInt32(lParam, 8); // KBDLLHOOKSTRUCT.flags

                            if (KeyStates.Length <= KeyCode) break; // Invalid value

                            bool isPressed = ((flags & LLKHF_UP) == 0 ? true : false); //transition state: 0 = pressed, 1 = released
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
            return CallNextHookEx(_HookId, nCode, wParam, lParam);
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
                return _HookId != IntPtr.Zero ? (0 != UnhookWindowsHookEx(_HookId)) : true;
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
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? SendMessageW(WindowHandle, WM_HOTKEY, wParam, lParam) : IntPtr.Zero;
        }
    }

    public class ViewLocator : IDataTemplate
    {
        public IControl Build(object data)
        {
            var name = data.GetType().FullName!.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type)!;
            }

            return new TextBlock { Text = "Not Found: " + name };
        }

        public bool Match(object data)
        {
            return data is ViewModelBase;
        }
    }
}
