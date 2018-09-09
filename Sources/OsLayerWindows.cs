//MIT License

//Copyright(c) 2018-2018 Peter Kirmeier

//Permission Is hereby granted, free Of charge, to any person obtaining a copy
//of this software And associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, And/Or sell
//copies of the Software, And to permit persons to whom the Software Is
//furnished to do so, subject to the following conditions:

//The above copyright notice And this permission notice shall be included In all
//copies Or substantial portions of the Software.

//THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or
//IMPLIED, INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE And NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS Or COPYRIGHT HOLDERS BE LIABLE For ANY CLAIM, DAMAGES Or OTHER
//LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or OTHERWISE, ARISING FROM,
//OUT OF Or IN CONNECTION WITH THE SOFTWARE Or THE USE Or OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HitCounterManager
{
    public static class OsLayer
    {
        #region Windows API Imports

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("User32.dll")]
        private static extern int MapVirtualKey(int uCode, int uMapType);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetKeyNameTextW(long lParam, string lpBuffer, int nSize);

        [DllImport("User32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, int vk);

        [DllImport("User32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public delegate void TimerProc(IntPtr hWnd, uint uMsg, IntPtr nIDEvent, uint dwTime);

        [DllImport("User32.dll")]
        private static extern IntPtr SetTimer(IntPtr hWnd, IntPtr nIDEvent, uint uElapse, TimerProc lpTimerFunc);

        [DllImport("User32.dll")]
        private static extern bool KillTimer(IntPtr hWnd, IntPtr nIDEvent);

        [DllImport("User32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        #endregion

        private const int MAPVK_VK_TO_VSC = 0;
        private const int KEY_PRESSED_NOW = 0x8000;
        private const int WM_HOTKEY = 0x312;

        /// <summary>
        /// Checks if a given key is pressed right now
        /// </summary>
        /// <param name="KeyCode">The key to check</param>
        /// <returns>true = pressed, false = released</returns>
        public static bool IsKeyPressedAsync(Keys KeyCode)
        {
            return (0 != (GetAsyncKeyState(KeyCode) & KEY_PRESSED_NOW));
        }
        /// <summary>
        /// Retrieves the name of a given key
        /// </summary>
        /// <param name="KeyCode">Key name to gather</param>
        /// <returns>Key name</returns>
        public static string GetKeyName(Keys KeyCode)
        {
            string lpKeyNameString = new string('\0', 256);
            long lParam = MapVirtualKey((int)KeyCode, MAPVK_VK_TO_VSC) << 16;
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
        public static bool SetHotKey(IntPtr WindowHandle, int HotKeyID, uint Modifiers, Keys KeyCode)
        {
            return RegisterHotKey(WindowHandle, HotKeyID, Modifiers, (int)KeyCode);
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
    }
}
