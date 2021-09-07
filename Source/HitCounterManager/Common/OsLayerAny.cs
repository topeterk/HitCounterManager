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

using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace HitCounterManager.Common.OsLayer
{
    #region OS Layer Interface

    public delegate void TimerProc(IntPtr hWnd, uint uMsg, IntPtr nIDEvent, uint dwTime);

    public interface IOsLayer
    {
        /// <summary>
        /// Name of this OS implementation
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Fires when a low level keyboard event designates a key state changes.
        /// Must be enabled/disabled via StartKeyboardLowLevelHook/StopKeyboardLowLevelHook.
        /// Keep execution time short as possible!
        /// </summary>
        event EventHandler LowLevelKeyboardEvent;

        /// <summary>
        /// Indicates if this OS is capable of global hotkeys
        /// </summary>
        bool GlobalHotKeySupport { get; }
        
        /// <summary>
        /// Checks if any keys are pressed right now.
        /// Key state is read at the moment of this call when the low level keybaord events are disabled.
        /// Gets the key state from internal cache when the low level keybaord events are enabled.
        /// </summary>
        /// <returns>null on error; otherwise a list of KeyCodes of all currently pressed keys</returns>
        List<int> GetKeysPressedAsync();

        /// <summary>
        /// Checks if a given key is pressed right now.
        /// Key state is read at the moment of this call when the low level keybaord events are disabled.
        /// Gets the key state from internal cache when the low level keybaord events are enabled.
        /// </summary>
        /// <param name="KeyCode">The key to check</param>
        /// <returns>true = pressed, false = released</returns>
        bool IsKeyPressedAsync(int KeyCode);

        /// <summary>
        /// Retrieves the name of a given key
        /// </summary>
        /// <param name="KeyCode">Key name to gather</param>
        /// <returns>Key name</returns>
        string GetKeyName(int KeyCode);

        /// <summary>
        /// Registers a hot key
        /// </summary>
        /// <param name="WindowHandle">Window handle that is receiving the hotkey message</param>
        /// <param name="HotKeyID">Custom ID for the hotkey</param>
        /// <param name="Modifiers">Key modifiers</param>
        /// <param name="KeyCode">Key to register</param>
        /// <returns>Success state</returns>
        bool SetHotKey(IntPtr WindowHandle, int HotKeyID, uint Modifiers, int KeyCode);

        /// <summary>
        /// Unregisters a hot key
        /// </summary>
        /// <param name="WindowHandle">Window handle that is receiving the hotkey message</param>
        /// <param name="HotKeyID">Custom ID for the hotkey</param>
        /// <returns>Success state</returns>
        bool KillHotKey(IntPtr WindowHandle, int HotKeyID);

        /// <summary>
        /// Start using the low level keyboard events.
        /// This enables the LowLevelKeyboardEvent on the same thread that is calling this function.
        /// The states for IsKeyPressedAsync will be taken from internal cache.
        /// </summary>
        /// <returns>Success state</returns>
        bool StartKeyboardLowLevelHook();

        /// <summary>
        /// Stops using the the low level keyboard events.
        /// This disables the LowLevelKeyboardEvent
        /// </summary>
        /// <returns>Success state</returns>
        bool StopKeyboardLowLevelHook();

        /// <summary>
        /// Creates a timer repeatedly calling the callback upon timeout.
        /// Depending on the callbacks return, the timer keeps running (true) or will be stopped (false)
        /// </summary>
        /// <param name="WindowHandle">Window handle that is receiving the timer message</param>
        /// <param name="TimerID">Custom ID for the timer</param>
        /// <param name="Interval">Timeout in milliseconds</param>
        /// <param name="Callback">Callback function pointer</param>
        /// <returns>Success state</returns>
        bool SetTimer(IntPtr WindowHandle, uint TimerID, TimeSpan Interval, Func<bool> Callback);

        /// <summary>
        /// Creates a timer repeatedly calling the callback upon timeout
        /// </summary>
        /// <param name="WindowHandle">Window handle that is receiving the timer message</param>
        /// <param name="TimerID">Custom ID for the timer</param>
        /// <param name="Timeout">Timeout in milliseconds</param>
        /// <param name="CallbackAsync">Callback function pointer</param>
        /// <returns>Timer identifier (non-zero on success)</returns>
        IntPtr SetTimer(IntPtr WindowHandle, int TimerID, uint Timeout, TimerProc CallbackAsync);

        /// <summary>
        /// Removes a timer
        /// </summary>
        /// <param name="WindowHandle">Window handle that the timer is registered at</param>
        /// <param name="TimerID">Custom ID for the timer</param>
        /// <returns>Success state</returns>
        bool KillTimer(IntPtr WindowHandle, int TimerID);

        /// <summary>
        /// Sends an hotkey message to the given window
        /// </summary>
        /// <param name="WindowHandle">Window handle that is receiving the hotkey message</param>
        /// <param name="wParam">Parameter 1 of message</param>
        /// <param name="lParam">Parameter 2 of message</param>
        /// <returns>Message result</returns>
        IntPtr SendHotKeyMessage(IntPtr WindowHandle, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Read the OS setting whether dark mode is enabled
        /// </summary>
        /// <returns>true = Dark mode; false = Light mode</returns>
        bool IsDarkModeActive();
    }
    #endregion

    #region Default OS Layer
    public class OsLayerDefault : IOsLayer
    {
        public string Name { get => "Any"; }
#pragma warning disable CS0067 // never used.
        public event EventHandler LowLevelKeyboardEvent;
//#pragma warning restore CS0067 // never used.
        public bool GlobalHotKeySupport { get => false; }
        public List<int> GetKeysPressedAsync() { return null; }
        public bool IsKeyPressedAsync(int KeyCode) { return false; }
        public string GetKeyName(int KeyCode) { return "?"; }
        public bool SetHotKey(IntPtr WindowHandle, int HotKeyID, uint Modifiers, int KeyCode) { return false; }
        public bool KillHotKey(IntPtr WindowHandle, int HotKeyID) { return false; }
        public bool StartKeyboardLowLevelHook() { return false; }
        public bool StopKeyboardLowLevelHook() { return false; }
        public bool SetTimer(IntPtr WindowHandle, uint TimerID, TimeSpan Interval, Func<bool> Callback) { Device.StartTimer(Interval, Callback); return true; }
        public IntPtr SetTimer(IntPtr WindowHandle, int TimerID, uint Timeout, TimerProc CallbackAsync) { return (IntPtr)0; }
        public bool KillTimer(IntPtr WindowHandle, int TimerID) { return false; }
        public IntPtr SendHotKeyMessage(IntPtr WindowHandle, IntPtr wParam, IntPtr lParam) { return (IntPtr)0; }
        public bool IsDarkModeActive() { return false; }
    }
    #endregion
}
