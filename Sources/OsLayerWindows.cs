//MIT License

//Copyright (c) 2018-2024 Peter Kirmeier

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

#if OS_WINDOWS
using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;

namespace HitCounterManager
{
    /*[SupportedOSPlatform("Windows")]*/
    public static class OsLayer
    {
        /// <summary>
        /// Name of this OS implementation
        /// </summary>
        public const string Name = "Win";

        /// <summary>
        /// Fires when a low level keyboard event designates a key state changes.
        /// Must be enabled/disabled via StartKeyboardLowLevelHook/StopKeyboardLowLevelHook.
        /// Keep execution time short as possible!
        /// </summary>
        public static event EventHandler LowLevelKeyboardEvent;

        public delegate void TimerProc(IntPtr hWnd, uint uMsg, IntPtr nIDEvent, uint dwTime);

        #region Windows API

        // Datatypes: https://docs.microsoft.com/en-us/windows/win32/winprog/windows-data-types

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
        [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-unhookwindowshookex
        /// </summary>
        /// <param name="hhk">HHOOK = HANDLE = PVOID = void*</param>
        /// <returns>BOOL = int</returns>
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
        [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-getmodulehandlew
        /// </summary>
        /// <param name="lpModuleName">LPCWSTR = CONST WCHAR * = const wchar_t *</param>
        /// <returns>HMODULE = HINSTANCE = HANDLE = PVOID = void*</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr GetModuleHandleW(string lpModuleName);


        [DllImport("User32.dll")]
        private static extern IntPtr SetTimer(IntPtr hWnd, IntPtr nIDEvent, uint uElapse, TimerProc lpTimerFunc);

        [DllImport("User32.dll")]
        private static extern bool KillTimer(IntPtr hWnd, IntPtr nIDEvent);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendmessagew
        /// </summary>
        /// <param name="hWnd">HWND = HANDLE = PVOID = void*</param>
        /// <param name="Msg">UINT = unsigned int</param>
        /// <param name="wParam">WPARAM = UINT_PTR = __int64 or long</param>
        /// <param name="lParam">LPARAM = LONG_PTR = __int64 or long</param>
        /// <returns>LRESULT = LONG_PTR = __int64 or long</returns>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr SendMessageW(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        #endregion

        /// <summary>
        /// Creates a timer repeatedly calling the callback upon timeout
        /// </summary>
        /// <param name="WindowHandle">Window handle that is receiving the timer message</param>
        /// <param name="TimerID">Custom ID for the timer</param>
        /// <param name="Timeout">Timeout in milliseconds</param>
        /// <param name="CallbackAsync">Callback function pointer</param>
        /// <returns>Timer identifier (non-zero on success)</returns>
        public static IntPtr SetTimer(IntPtr WindowHandle, int TimerID, uint Timeout, TimerProc CallbackAsync)
        {
            return SetTimer(WindowHandle, (IntPtr)TimerID, Timeout, CallbackAsync);
        }

        /// <summary>
        /// Removes a timer
        /// </summary>
        /// <param name="WindowHandle">Window handle that the timer is registered at</param>
        /// <param name="TimerID">Custom ID for the timer</param>
        /// <returns>Success state</returns>
        public static bool KillTimer(IntPtr WindowHandle, int TimerID)
        {
            return KillTimer(WindowHandle, (IntPtr)TimerID);
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
            return SendMessageW(WindowHandle, WM_HOTKEY, wParam, lParam);
        }

        #endregion

        #region Hot Key

        /// <summary>
        /// Indicates if this OS is capable of global hotkeys
        /// </summary>
        public const bool GlobalHotKeySupport = true;

        private const int WM_HOTKEY = 0x0312;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;
        private const int HC_ACTION = 0;
        private const int KF_EXTENDED = 0x0100;
        /*private const int KF_ALTDOWN = 0x2000;*/
        private const int KF_UP = 0x8000;
        /*private const int LLKHF_ALTDOWN = (KF_ALTDOWN >> 8);*/
        private const int LLKHF_UP = (KF_UP >> 8);
        private const int KEY_PRESSED_NOW = 0x8000;
        private const uint MAPVK_VK_TO_VSC_EX = 4;

        private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static readonly HookProc _HookProc = new(HookCallback); // prevent garbage collector freeing up the callback
        private static IntPtr _HookId = IntPtr.Zero;
        private static readonly bool[] KeyStates = new bool[256];

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-mapvirtualkeyw
        /// </summary>
        /// <param name="uCode">UINT = unsigned int</param>
        /// <param name="uMapType">UINT = unsigned int</param>
        /// <returns>UINT = unsigned int</returns>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        private static extern uint MapVirtualKeyW(uint uCode, uint uMapType);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeynametextw
        /// </summary>
        /// <param name="lParam">LONG = long</param>
        /// <param name="lpBuffer">LPWSTR = CONST WCHAR * = const wchar_t *</param>
        /// <param name="nSize">int</param>
        /// <returns>int</returns>
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
        [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        private static extern int RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-unregisterhotkey
        /// </summary>
        /// <param name="hWnd">HWND = HANDLE = PVOID = void*</param>
        /// <param name="id">int </param>
        /// <returns>BOOL = int</returns>
        [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        private static extern int UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getasynckeystate
        /// </summary>
        /// <param name="vKey">int</param>
        /// <returns>SHORT = short</returns>
        [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        private static extern short GetAsyncKeyState(VirtualKeyStates vKey);

        /// <summary>
        /// Checks if a given key is pressed right now.
        /// Key state is read at the moment of this call when the low level keybaord events are disabled.
        /// Gets the key state from internal cache when the low level keybaord events are enabled.
        /// </summary>
        /// <param name="KeyCode">The key to check</param>
        /// <returns>true = pressed, false = released</returns>
        public static bool IsKeyPressedAsync(VirtualKeyStates KeyCode)
        {
            if ((_HookId != IntPtr.Zero) && (KeyStates.Length < (int)KeyCode))
            {
                // use the values from the low level keyboard hook instead
                return KeyStates[(int)KeyCode];
            }

            return (0 != (GetAsyncKeyState(KeyCode) & KEY_PRESSED_NOW));
        }

        // Known extended scan codes for extended keys.
        // See: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input#scan-codes
        internal const uint SC_PRINTSCREEN = 0xE037;
        internal const uint SC_PAUSE = 0xE11D;
        internal const uint SC_PAUSE__LEGACY = 0x0045;

        /// <summary>
        /// Retrieves the name of a given virtual-key code.
        /// </summary>
        /// <param name="VirtualKeyCode">Virtual-key code</param>
        /// <returns>Key name</returns>
        public static string GetKeyName(VirtualKeyStates VirtualKeyCode)
        {
            // GetKeyNameTextW does not return proper names on some virtual-key codes.
            // Therefore just directly translate them
            switch (VirtualKeyCode)
            {
                case VirtualKeyStates.VK_MULTIPLY:
                    return "*";
                case VirtualKeyStates.VK_DIVIDE:
                    return "/";
                default:
                    break;
            }

            // Convert virtual-key code into extended scan code
            uint extendedScanCode = MapVirtualKeyW((uint)VirtualKeyCode, MAPVK_VK_TO_VSC_EX);
            uint scanCode = (0xFF & extendedScanCode); // Scan code is in lower byte
            if (((extendedScanCode >> 8) & 0xFE) == 0xE0)
            {
                switch (extendedScanCode)
                {
                    case SC_PAUSE:
                        scanCode = SC_PAUSE__LEGACY;
                        break;
                    default:
                        scanCode |= KF_EXTENDED; // Set extended key flag when it is an extended scan code (0xe0 or 0xe1 in high byte)
                        break;
                }
            }
            else
            {
                // It seems that MAPVK_VK_TO_VSC_EX is not handling extended scan codes properly,
                // we try fix this by ourself but applying the extended bit for known extended scan codes.
                switch (VirtualKeyCode)
                {
                    case VirtualKeyStates.VK_PAUSE:
                        scanCode = SC_PAUSE__LEGACY;
                        break;
                    case VirtualKeyStates.VK_PRIOR: // Page Up
                    case VirtualKeyStates.VK_NEXT: // Page down
                    case VirtualKeyStates.VK_END:
                    case VirtualKeyStates.VK_HOME:
                    case VirtualKeyStates.VK_LEFT:
                    case VirtualKeyStates.VK_UP:
                    case VirtualKeyStates.VK_RIGHT:
                    case VirtualKeyStates.VK_DOWN:
                    case VirtualKeyStates.VK_INSERT:
                    case VirtualKeyStates.VK_DELETE:
                    case VirtualKeyStates.VK_NUMLOCK:
                        scanCode |= KF_EXTENDED;
                        break;
                    case VirtualKeyStates.VK_SNAPSHOT: // Print Screen
                        scanCode = KF_EXTENDED | (SC_PRINTSCREEN & 0xFF);
                        break;
                }
            }

            // Retrieve the name
            int lParam = (int)scanCode << 16;
            string lpKeyNameString = new('\0', 256);
            if (0 != GetKeyNameTextW(lParam, lpKeyNameString, lpKeyNameString.Length))
            {
                return lpKeyNameString.Trim('\0');
            }

            return "(?)";
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
            return 0 != RegisterHotKey(WindowHandle, HotKeyID, Modifiers, (uint)KeyCode);
        }

        /// <summary>
        /// Unregisters a hot key
        /// </summary>
        /// <param name="WindowHandle">Window handle that is receiving the hotkey message</param>
        /// <param name="HotKeyID">Custom ID for the hotkey</param>
        /// <returns>Success state</returns>
        public static bool KillHotKey(IntPtr WindowHandle, int HotKeyID)
        {
            return 0 != UnregisterHotKey(WindowHandle, HotKeyID);
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
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

                            bool isPressed = (flags & LLKHF_UP) == 0; //transition state: 0 = pressed, 1 = released
                            if (KeyStates[KeyCode] != isPressed) // drop this event, we already know this!
                            {
                                KeyStates[KeyCode] = isPressed;
                                LowLevelKeyboardEvent?.Invoke(null, null); // Fire event // Fire event
                            }
                        }
                        break;
                    default: break;
                }
            }
            return CallNextHookEx(_HookId, nCode, wParam, lParam);
        }

        /// <summary>
        /// Starts using the low level keyboard events.
        /// This enables the LowLevelKeyboardEvent on the same thread that is calling this function.
        /// The states for IsKeyPressedAsync will be taken from internal cache.
        /// </summary>
        /// <returns>Success state</returns>
        public static bool StartKeyboardLowLevelHook()
        {
            if (_HookId != IntPtr.Zero) return false; // Only one instance supported

            // Reset the keystates, as their correct states are collected on the way
            for (int i = 0; i < KeyStates.Length; i++) KeyStates[i] = false;

            _HookId = SetWindowsHookEx(WH_KEYBOARD_LL, _HookProc, GetModuleHandleW(System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName), 0);
            return _HookId != IntPtr.Zero;
        }

        /// <summary>
        /// Stops using the the low level keyboard events.
        /// This disables the LowLevelKeyboardEvent
        /// </summary>
        /// <returns>Success state</returns>
        public static bool StopKeyboardLowLevelHook()
        {
            return _HookId == IntPtr.Zero || (0 != UnhookWindowsHookEx(_HookId));
        }

        #endregion

        #region Windows Runtime Assembly

        /// <summary>
        /// Read the OS setting whether dark mode is enabled
        /// </summary>
        /// <returns>true = Dark mode; false = Light mode</returns>
        public static bool IsDarkModeActive()
        {
            try
            {
                // Check: AppsUseLightTheme (REG_DWORD)
                // 0 = Dark mode, 1 = Light mode
                return Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", "AppsUseLightTheme", 1).ToString() == "0";
            }
            catch
            {
                // Catch exceptions when key is not working on this system
                return false;
            }
        }

        #endregion

        /// <summary>
        /// MonoDevelop workaround placeholder
        /// </summary>
        public static void EnableBreakpoint() { /* nothing to do - it just works like a charm */ }
    }
}
#endif
