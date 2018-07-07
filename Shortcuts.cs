//MIT License

//Copyright(c) 2016-2018 Peter Kirmeier

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
    /// <summary>
    /// Holds data about a hotkey
    /// </summary>
    public class ShortcutsKey
    {
        private const int KEY_PRESSED_NOW = 0x8000;

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        private bool _used; // tells if shortcut is registered at windows
        private bool _down; // tells if shortcut is currently pressed
        public bool valid; // tell if a valid key/modifier pair was ever set
        public KeyEventArgs key;
        public bool used
        {
            get { return _used; }
            set
            {
                CheckPressedState(false, false, false); // flush current state
                _used = value;
            }
        }
 
        /// <summary>
        /// Creates a key shortcut object
        /// </summary>
        public ShortcutsKey()
        {
            _used = false;
            _down = false;
            valid = false;
            key = new KeyEventArgs(0);
        }

        /// <summary>
        /// Performs a shallow copy of a shortcut key
        /// </summary>
        public ShortcutsKey ShallowCopy()
        {
            return (ShortcutsKey)this.MemberwiseClone();
        }

        /// <summary>
        /// Checks current key stat and returns if all registered keys are pressed at the moment
        /// </summary>
        private bool CheckPressedState(bool ShiftState, bool ControlState, bool AltState)
        {
            if (0 == (GetAsyncKeyState(key.KeyCode) & KEY_PRESSED_NOW)) return false;
            if (key.Shift && !ShiftState) return false;
            if (key.Control && !ControlState) return false;
            if (key.Alt && !AltState) return false;
            return true;
        }

        /// <summary>
        /// Checks if all registered keys are just pressed the first time
        /// </summary>
        public bool WasPressed(bool ShiftState, bool ControlState, bool AltState)
        {
            if (CheckPressedState(ShiftState, ControlState, AltState)) // Is key down right now?
            {
                if (!_down) // Did key go down the first time?
                {
                    _down = true; // Make sure key gets released before going down again
                    return true; // Okay, key was just pressed!
                }
            }
            else _down = false; // Key is up again, so re-arm 'first time' trigger 

            return false; // not pressed or still pressed
        }
    }

    /// <summary>
    /// Manages all hot keys aka shortcuts
    /// </summary>
    public class Shortcuts
    {
        private const int MOD_ALT = 0x0001;
        private const int MOD_CONTROL = 0x0002;
        private const int MOD_SHIFT = 0x0004;
        private const int WM_HOTKEY = 0x312;
        private const int VK_SHIFT = 0x10;
        private const int VK_CONTROL = 0x11;
        private const int VK_MENU = 0x12;
        private const int KEY_PRESSED_NOW = 0x8000;

        [DllImport("User32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, int vk);

        [DllImport("User32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private delegate void TimerProc(IntPtr hWnd, uint uMsg, IntPtr nIDEvent, uint dwTime);
        private TimerProc TimerProcKeepAliveReference; // prevent garbage collector freeing up the callback without any reason

        [DllImport("User32.dll")]
        private static extern IntPtr SetTimer(IntPtr hWnd, IntPtr nIDEvent, uint uElapse, TimerProc lpTimerFunc);

        [DllImport("User32.dll")]
        private static extern bool KillTimer(IntPtr hWnd, IntPtr nIDEvent);

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(System.Int32 vKey);

        [DllImport("User32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public enum SC_Type {
            SC_Type_Reset = 0,
            SC_Type_Hit = 1,
            SC_Type_Split = 2,
            SC_Type_MAX = 3
        };

        public enum SC_HotKeyMethod {
            SC_HotKeyMethod_Sync = 0, // = RegisterHotKey + UnregisterHotKey
            SC_HotKeyMethod_Async = 1 // = SetTimer + KillTimer + GetAsyncKeyState + SendMessage
        };

        private IntPtr hwnd;
        private ShortcutsKey[] sc_list = new ShortcutsKey[(int)SC_Type.SC_Type_MAX];
        private SC_HotKeyMethod method;

        public SC_HotKeyMethod NextStart_Method;

        /// <summary>
        /// Build empty hot key list, save window handle.
        /// Call Initialize() to start using hot keys
        /// </summary>
        public Shortcuts(IntPtr WindowHandle)
        {
            hwnd = WindowHandle;
            for (int i = 0; i < (int)SC_Type.SC_Type_MAX; i++) sc_list[i] = new ShortcutsKey();
        }

        /// <summary>
        /// Destroy object (and free timer)
        /// </summary>
        ~Shortcuts()
        {
            if (method == SC_HotKeyMethod.SC_HotKeyMethod_Async) KillTimer(hwnd, (IntPtr)0);
        }

        /// <summary>
        /// Initializes oject by setting the method (and configure timer)
        /// </summary>
        public void Initialize(SC_HotKeyMethod HotKeyMethod)
        {
            NextStart_Method = method = HotKeyMethod;

            for (int i = 0; i < (int)SC_Type.SC_Type_MAX; i++) sc_list[i] = new ShortcutsKey();

            if (method == SC_HotKeyMethod.SC_HotKeyMethod_Async)
            {
                TimerProcKeepAliveReference = new TimerProc(timer_event); // stupid garbage collection fixup
                SetTimer(hwnd, (IntPtr)0, 20, TimerProcKeepAliveReference);
            }
        }

        /// <summary>
        /// Registers and unregisters a hotkey
        /// </summary>
        private void HotKeyRegister(SC_Type Id, ShortcutsKey key, bool Enable)
        {
            uint modifier = 0;

            if (key.key.Shift) modifier += MOD_SHIFT;
            if (key.key.Control) modifier += MOD_CONTROL;
            if (key.key.Alt) modifier += MOD_ALT;

            if (method == SC_HotKeyMethod.SC_HotKeyMethod_Sync)
            {
                if (Enable)
                {
                    if (RegisterHotKey(hwnd, (int)Id, modifier, (int)key.key.KeyCode))
                    {
                        key.used = true;
                        key.valid = true;
                    }
                    else
                    {
                        // anything went wrong while registering, clear key..
                        key.used = false;
                        key.valid = false;
                    }
                }
                else
                {
                    UnregisterHotKey(hwnd, (int)Id);
                    key.used = false;
                }
            }
            else if (method == SC_HotKeyMethod.SC_HotKeyMethod_Async)
            {
                if (Enable)
                {
                    if (RegisterHotKey(hwnd, (int)Id, modifier, (int)key.key.KeyCode))
                    {
                        UnregisterHotKey(hwnd, (int)Id); // don't use this method, we just used registration to check if keycode is valid and works
                        key.used = true;
                        key.valid = true;
                    }
                    else
                    {
                        // anything went wrong while registering, clear key..
                        key.used = false;
                        key.valid = false;
                    }
                }
                else key.used = false;
            }
        }

        /// <summary>
        /// Configures and disables a shortcut for a key
        /// </summary>
        public void Key_PreSet(SC_Type Id, ShortcutsKey key)
        {
            Key_Set(Id, key.ShallowCopy()); // configures and validate key (enable once)
            Key_SetState(Id, false); // disable afterwards
        }

        /// <summary>
        /// Configures and enables a shortcut for a key
        /// </summary>
        public void Key_Set(SC_Type Id, ShortcutsKey key)
        {
            if (Key_Get(Id).used) HotKeyRegister(Id, key, false);

            HotKeyRegister(Id, key, true);
            sc_list.SetValue(key.ShallowCopy(), (int)Id);
        }

        /// <summary>
        /// Enables or Disables a shortcut
        /// </summary>
        public void Key_SetState(SC_Type Id, bool IsEnabled)
        {
            ShortcutsKey key = Key_Get(Id);

            if (!key.valid) return;

            if (!key.used && IsEnabled) HotKeyRegister(Id, key, true);
            else if (key.used && !IsEnabled) HotKeyRegister(Id, key, false);

            sc_list.SetValue(key, (int)Id);
        }

        /// <summary>
        /// Get the bound key of a shortcut
        /// </summary>
        public ShortcutsKey Key_Get(SC_Type Id)
        {
            ShortcutsKey key = (ShortcutsKey)sc_list.GetValue((int)Id);
            return key.ShallowCopy();
        }

        /// <summary>
        /// Timer message handler to check for async hot keys
        /// </summary>
        private void timer_event(IntPtr hwnd, uint uMsg, IntPtr nIDEvent, uint dwTime)
        {
            bool k_shift;
            bool k_control;
            bool k_alt;

            k_shift = (0 != (GetAsyncKeyState(VK_SHIFT) & KEY_PRESSED_NOW));
            k_control = (0 != (GetAsyncKeyState(VK_CONTROL) & KEY_PRESSED_NOW));
            k_alt = (0 != (GetAsyncKeyState(VK_MENU) & KEY_PRESSED_NOW));

            for (int i = 0; i < (int)SC_Type.SC_Type_MAX; i++)
            {
                if (sc_list[i].used)
                {
                    if (sc_list[i].WasPressed(k_shift, k_control, k_alt)) SendMessage(hwnd, WM_HOTKEY, (IntPtr)i, (IntPtr)0);
                }
            }
        }
    }
}
