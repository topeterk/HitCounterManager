//MIT License

//Copyright (c) 2018-2021 Peter Kirmeier

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

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HitCounterManager.Common.OsLayer;

namespace HitCounterManager.WPF.OsLayer
{
    public class OsLayer : IOsLayer
    {

        #region Windows API Imports
        [DllImport("user32.dll")]
        static extern bool GetKeyboardState(byte [] lpKeyState);

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        [DllImport("User32.dll")]
        private static extern int MapVirtualKey(int uCode, int uMapType);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetKeyNameTextW(int lParam, string lpBuffer, int nSize);

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

        private struct TimerHandle {
            public IntPtr WindowHandle;
            public IntPtr TimerID;
            public Func<bool> Callback;
            public bool IsRunning;
        };

        private static List<TimerHandle> TimersList = new List<TimerHandle>();
        private static TimerProc _TimerProc = null;

        public OsLayer()
        {
            if (null == _TimerProc) _TimerProc = new TimerProc(TimerCallbackWrapper); // prevent garbage collector freeing up the callback
            if (null == _HookProc) _HookProc = new HookProc(HookCallback); // prevent garbage collector freeing up the callback
        }

        public string Name { get => "Win"; }

        public event EventHandler LowLevelKeyboardEvent;

        public bool GlobalHotKeySupport { get => true; }

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
        private static HookProc _HookProc = null;
        private static IntPtr _HookId = IntPtr.Zero;
        private static bool[] KeyStates = new bool[256];
    
        public List<int> GetKeysPressedAsync()
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
                if (!GetKeyboardState(buffer)) return null;
                for (int KeyCode = 0; KeyCode < buffer.Length; KeyCode++)
                    if (0 != (buffer[KeyCode] & 0x80 /*key is down flag*/)) result.Add(KeyCode);
            }

            return result;
        }

        public bool IsKeyPressedAsync(int KeyCode)
        {
            if ((_HookId != IntPtr.Zero) && (KeyStates.Length < KeyCode))
            {
                // use the values from the low level keyboard hook instead
                return KeyStates[KeyCode];
            }

            return (0 != (GetAsyncKeyState(KeyCode) & KEY_PRESSED_NOW));
        }

        public string GetKeyName(int KeyCode)
        {
            string lpKeyNameString = new string('\0', 256);
            
            int lParam = MapVirtualKey(KeyCode, MAPVK_VK_TO_VSC) << 16;
            if (0 == GetKeyNameTextW(lParam, lpKeyNameString, lpKeyNameString.Length))
                lpKeyNameString = "?";
            return lpKeyNameString;
        }

        public bool SetHotKey(IntPtr WindowHandle, int HotKeyID, uint Modifiers, int KeyCode)
        {
            return RegisterHotKey(WindowHandle, HotKeyID, Modifiers, KeyCode);
        }

        public bool KillHotKey(IntPtr WindowHandle, int HotKeyID)
        {
            return UnregisterHotKey(WindowHandle, HotKeyID);
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
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

        public bool StartKeyboardLowLevelHook()
        {
            if (_HookId != IntPtr.Zero) return false; // Only one instance supported

            // Reset the keystates, as their correct states are collected on the way
            for (int i = 0; i < KeyStates.Length; i++) KeyStates[i] = false;

            _HookId = SetWindowsHookEx(WH_KEYBOARD_LL, _HookProc, GetModuleHandle(System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName), 0);
            return (_HookId == IntPtr.Zero ? false : true);
        }

        public bool StopKeyboardLowLevelHook()
        {
            return _HookId != IntPtr.Zero ? UnhookWindowsHookEx(_HookId) : true;
        }

        /// <summary>
        /// Finds the responsible timer handle and execute its callback.
        /// When the callback returns false, the timer will be stopped.
        /// Note: No real error checking if timer handle is not existing any more or timer cannot be killed.
        /// </summary>
        private void TimerCallbackWrapper(IntPtr hwnd, uint uMsg, IntPtr nIDEvent, uint dwTime)
        {
            // Find the respective timer and handle it
            for (int i = 0; i < TimersList.Count; i++)
            {
                TimerHandle handle = TimersList[i];

                if ((handle.WindowHandle == hwnd) && (handle.TimerID == nIDEvent))
                {
                    if (!handle.Callback()) // On false, stop the timer
                    {
                        KillTimer(handle.WindowHandle, handle.TimerID);
                        TimersList.RemoveAt(i);
                    }
                    return;
                }
            }
        }

        public bool SetTimer(IntPtr WindowHandle, uint TimerID, TimeSpan Interval, Func<bool> Callback)
        {
            // Is existing as we only allow to create new times for now
            foreach (TimerHandle handle in TimersList)
            {
                if ((handle.WindowHandle == WindowHandle) && (handle.TimerID == (IntPtr)TimerID)) return false;
            }

            // Prepare timer
            TimerHandle timer = new TimerHandle();
            timer.WindowHandle = WindowHandle;
            timer.TimerID = (IntPtr)TimerID;
            timer.Callback = Callback;
            timer.IsRunning = true;
            TimersList.Add(timer);

            // Start timer
            if (IntPtr.Zero == SetTimer(timer.WindowHandle, timer.TimerID, (uint)Interval.TotalMilliseconds, _TimerProc))
            {
                TimersList.Remove(timer);
                return false;
            }
            return true;
        }

        public IntPtr SetTimer(IntPtr WindowHandle, int TimerID, uint Timeout, TimerProc CallbackAsync)
        {
            return SetTimer(WindowHandle, (IntPtr)TimerID, Timeout, CallbackAsync);
        }

        public bool KillTimer(IntPtr WindowHandle, int TimerID)
        {
            return KillTimer(WindowHandle, (IntPtr)TimerID);
        }

        public IntPtr SendHotKeyMessage(IntPtr WindowHandle, IntPtr wParam, IntPtr lParam)
        {
            return SendMessage(WindowHandle, WM_HOTKEY, wParam, lParam);
        }

        public bool IsDarkModeActive()
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
    }
}
