//MIT License

//Copyright (c) 2018-2020 Peter Kirmeier

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

        #region Windows API Imports

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        [DllImport("User32.dll")]
        private static extern int MapVirtualKey(int uCode, int uMapType);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetKeyNameTextW(long lParam, string lpBuffer, int nSize);

        [DllImport("User32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, int vk);

        [DllImport("User32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);


        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);


        [DllImport("User32.dll")]
        private static extern IntPtr SetTimer(IntPtr hWnd, IntPtr nIDEvent, uint uElapse, TimerProc lpTimerFunc);

        [DllImport("User32.dll")]
        private static extern bool KillTimer(IntPtr hWnd, IntPtr nIDEvent);

        [DllImport("User32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        #endregion

        /// <summary>
        /// Indicates if this OS is capable of global hotkeys
        /// </summary>
        public const bool GlobalHotKeySupport = true;

        private const int MAPVK_VK_TO_VSC = 0;
        private const int KEY_PRESSED_NOW = 0x8000;
        private const int WM_HOTKEY = 0x0312;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;
        private const int WH_KEYBOARD_LL = 13;
        private const int HC_ACTION = 0;
        private const int KF_ALTDOWN = 0x2000;
        private const int KF_UP = 0x8000;
        private const int LLKHF_ALTDOWN = (KF_ALTDOWN >> 8);
        private const int LLKHF_UP = (KF_UP >> 8);

        private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static readonly HookProc _HookProc = new HookProc(HookCallback); // prevent garbage collector freeing up the callback
        private static IntPtr _HookId = IntPtr.Zero;
        private static bool[] KeyStates = new bool[256];

        /// <summary>
        /// Checks if a given key is pressed right now.
        /// Key state is read at the moment of this call when the low level keybaord events are disabled.
        /// Gets the key state from internal cache when the low level keybaord events are enabled.
        /// </summary>
        /// <param name="KeyCode">The key to check</param>
        /// <returns>true = pressed, false = released</returns>
        public static bool IsKeyPressedAsync(int KeyCode)
        {
            if ((_HookId != IntPtr.Zero) && (KeyStates.Length < KeyCode))
            {
                // use the values from the low level keyboard hook instead
                return KeyStates[KeyCode];
            }

            return (0 != (GetAsyncKeyState(KeyCode) & KEY_PRESSED_NOW));
        }
        /// <summary>
        /// Retrieves the name of a given key
        /// </summary>
        /// <param name="KeyCode">Key name to gather</param>
        /// <returns>Key name</returns>
        public static string GetKeyName(int KeyCode)
        {
            string lpKeyNameString = new string('\0', 256);
            long lParam = MapVirtualKey(KeyCode, MAPVK_VK_TO_VSC) << 16;
            if (0 == GetKeyNameTextW(lParam, lpKeyNameString, lpKeyNameString.Length))
                lpKeyNameString = "?";
            return lpKeyNameString;
        }

        /// <summary>
        /// Registers a hot key
        /// </summary>
        /// <param name="WindowHandle">Window handle that is receiving the hotkey message</param>
        /// <param name="HotKeyID">Custom ID for the hotkey</param>
        /// <param name="Modifiers">Key modifiers</param>
        /// <param name="KeyCode">Key to register</param>
        /// <returns>Success state</returns>
        public static bool SetHotKey(IntPtr WindowHandle, int HotKeyID, uint Modifiers, int KeyCode)
        {
            return RegisterHotKey(WindowHandle, HotKeyID, Modifiers, KeyCode);
        }

        /// <summary>
        /// Unregisters a hot key
        /// </summary>
        /// <param name="WindowHandle">Window handle that is receiving the hotkey message</param>
        /// <param name="HotKeyID">Custom ID for the hotkey</param>
        /// <returns>Success state</returns>
        public static bool KillHotKey(IntPtr WindowHandle, int HotKeyID)
        {
            return UnregisterHotKey(WindowHandle, HotKeyID);
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (HC_ACTION == nCode)
            {
                switch((int)wParam)
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
                                if (null != LowLevelKeyboardEvent) LowLevelKeyboardEvent(null, null); // Fire event
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

            _HookId = SetWindowsHookEx(WH_KEYBOARD_LL, _HookProc, GetModuleHandle(System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName), 0);
            return (_HookId == IntPtr.Zero ? false : true);
        }

        /// <summary>
        /// Stops using the the low level keyboard events.
        /// This disables the LowLevelKeyboardEvent
        /// </summary>
        /// <returns>Success state</returns>
        public static bool StopKeyboardLowLevelHook()
        {
            return _HookId != IntPtr.Zero ? UnhookWindowsHookEx(_HookId) : true;
        }

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
            return SendMessage(WindowHandle, WM_HOTKEY, wParam, lParam);
        }

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

        /// <summary>
        /// MonoDevelop workaround placeholder
        /// </summary>
        public static void EnableBreakpoint() { /* nothing to do - it just works like a charm */ }
    }
}
#endif
