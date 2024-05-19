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

#if OS_ANY
using System;

namespace HitCounterManager
{
    public static class OsLayer
    {
        /// <summary>
        /// Name of this OS implementation
        /// </summary>
        public const string Name = "Any";
        
#pragma warning disable CS0067 // never used.
        /// <summary>
        /// Fires when a low level keyboard event designates a key state changes.
        /// Must be enabled/disabled via StartKeyboardLowLevelHook/StopKeyboardLowLevelHook.
        /// Keep execution time short as possible!
        /// </summary>
        public static event EventHandler LowLevelKeyboardEvent;
#pragma warning restore CS0067 // never used.

        public delegate void TimerProc(IntPtr hWnd, uint uMsg, IntPtr nIDEvent, uint dwTime);

        /// <summary>
        /// Indicates if this OS is capable of global hotkeys
        /// </summary>
        public const bool GlobalHotKeySupport = false;

        /// <summary>
        /// Checks if a given key is pressed right now.
        /// Key state is read at the moment of this call when the low level keybaord events are disabled.
        /// Gets the key state from internal cache when the low level keybaord events are enabled.
        /// </summary>
        /// <param name="KeyCode">The key to check</param>
        /// <returns>true = pressed, false = released</returns>
        public static bool IsKeyPressedAsync(VirtualKeyStates KeyCode) { return false; }

        /// <summary>
        /// Retrieves the name of a given key
        /// </summary>
        /// <param name="KeyCode">Key name to gather</param>
        /// <returns>Key name</returns>
        public static string GetKeyName(VirtualKeyStates KeyCode) { return "(?)"; }

        /// <summary>
        /// Registers a hot key
        /// </summary>
        /// <param name="WindowHandle">Window handle that is receiving the hotkey message</param>
        /// <param name="HotKeyID">Custom ID for the hotkey</param>
        /// <param name="Modifiers">Key modifiers</param>
        /// <param name="KeyCode">Key to register</param>
        /// <returns>Success state</returns>
        public static bool SetHotKey(IntPtr WindowHandle, int HotKeyID, uint Modifiers, VirtualKeyStates KeyCode) { return false; }

        /// <summary>
        /// Unregisters a hot key
        /// </summary>
        /// <param name="WindowHandle">Window handle that is receiving the hotkey message</param>
        /// <param name="HotKeyID">Custom ID for the hotkey</param>
        /// <returns>Success state</returns>
        public static bool KillHotKey(IntPtr WindowHandle, int HotKeyID) { return false; }

        /// <summary>
        /// Start using the low level keyboard events.
        /// This enables the LowLevelKeyboardEvent on the same thread that is calling this function.
        /// The states for IsKeyPressedAsync will be taken from internal cache.
        /// </summary>
        /// <returns>Success state</returns>
        public static bool StartKeyboardLowLevelHook() { return false; }

        /// <summary>
        /// Stops using the the low level keyboard events.
        /// This disables the LowLevelKeyboardEvent
        /// </summary>
        /// <returns>Success state</returns>
        public static bool StopKeyboardLowLevelHook() { return false; }

        /// <summary>
        /// Creates a timer repeatedly calling the callback upon timeout
        /// </summary>
        /// <param name="WindowHandle">Window handle that is receiving the timer message</param>
        /// <param name="TimerID">Custom ID for the timer</param>
        /// <param name="Timeout">Timeout in milliseconds</param>
        /// <param name="CallbackAsync">Callback function pointer</param>
        /// <returns>Timer identifier (non-zero on success)</returns>
        public static IntPtr SetTimer(IntPtr WindowHandle, int TimerID, uint Timeout, TimerProc CallbackAsync) { return (IntPtr)0; }

        /// <summary>
        /// Removes a timer
        /// </summary>
        /// <param name="WindowHandle">Window handle that the timer is registered at</param>
        /// <param name="TimerID">Custom ID for the timer</param>
        /// <returns>Success state</returns>
        public static bool KillTimer(IntPtr WindowHandle, int TimerID) { return false; }

        /// <summary>
        /// Sends an hotkey message to the given window
        /// </summary>
        /// <param name="WindowHandle">Window handle that is receiving the hotkey message</param>
        /// <param name="wParam">Parameter 1 of message</param>
        /// <param name="lParam">Parameter 2 of message</param>
        /// <returns>Message result</returns>
        public static IntPtr SendHotKeyMessage(IntPtr WindowHandle, IntPtr wParam, IntPtr lParam) { return (IntPtr)0; }

        /// <summary>
        /// Read the OS setting whether dark mode is enabled
        /// </summary>
        /// <returns>true = Dark mode; false = Light mode</returns>
        public static bool IsDarkModeActive() { return false; }

        /// <summary>
        /// If you know how to use breakpoints in MonoDevelop within
        /// Windows Forms' event handlers that will not lead to a freeze
        /// of the whole operating system's UI when the debugging application has focus:
        /// Please let me know as using this workaround is pretty annoying!
        /// 
        /// To make this work, we have to ensure that we removed focus from the application
        /// we are currently debugging. When the timer runs down we have to switch focus
        /// to another application (most likely) MonoDevelop and we will be fine.
        /// </summary>
        public static void EnableBreakpoint()
        {
            System.Diagnostics.Debug.WriteLine("Hitting breakpoint, switch to debugger!");
            System.Diagnostics.Debug.Write("In");
            // Option 1:
            for (int i = 3; 0 < i; i--)
            {
                System.Diagnostics.Debug.Write(" .. " + i);
                System.Threading.Thread.Sleep(1000);
            }
            System.Diagnostics.Debug.WriteLine(".. HIT!");
        } // set breakpoint here
    }
}
#endif
