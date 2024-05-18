//MIT License

//Copyright(c) 2016-2024 Peter Kirmeier

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

namespace HitCounterManager.Models
{
    // Shrinked support wrapper for .Net Framework's System.Windows.Forms.KeyEventArgs
    public class KeyEventArgs
    {
        public KeyEventArgs(Keys keyData) { KeyData = keyData; }

        /// <summary>
        /// Representing the key code for the key that was pressed, combined with modifier flags
        /// </summary>
        public Keys KeyData;
        /// <summary>
        /// Representing the key code for the key that was pressed, without any modifier flags
        /// </summary>
        public Keys KeyCode { get => Keys.KeyCode & KeyData; }
        /// <summary>
        /// Gets a value indicating whether the ALT key was pressed
        /// </summary>
        public bool Alt { get => 0 != (Keys.Alt & KeyData); }
        /// <summary>
        /// Gets a value indicating whether the CTRL key was pressed
        /// </summary>
        public bool Control { get => 0 != (Keys.Control & KeyData); }
        /// <summary>
        /// Gets a value indicating whether the SHIFT key was pressed
        /// </summary>
        public bool Shift { get => 0 != (Keys.Shift & KeyData); }
        /// <summary>
        /// Integer representation of KeyCode
        /// </summary>
        public int KeyValue { get => (int)KeyCode; }
    }

    /// <summary>
    /// Shrinked support wrapper for .Net Framework's System.Windows.Forms.Keys
    /// </summary>
    public enum Keys {
        /// <summary>
        /// The bitmask to extract modifiers from a key value
        /// </summary>
        Modifiers = -65536,
        /// <summary>
        /// No key
        /// </summary>
        None = 0,
        /// <summary>
        /// The BACKSPACE key
        /// </summary>
        Back = 8,
        /// <summary>
        /// The SHIFT key
        /// </summary>
        ShiftKey = 16,
        /// <summary>
        /// The CTRL key
        /// </summary>
        ControlKey = 17,
        /// <summary>
        /// The ALT key
        /// </summary>
        Menu = 18,
        /// <summary>
        /// The left SHIFT key
        /// </summary>
        LShiftKey = 160,
        /// <summary>
        /// The right SHIFT key
        /// </summary>
        RShiftKey = 161,
        /// <summary>
        /// The left CTRL key
        /// </summary>
        LControlKey = 162,
        /// <summary>
        /// The right CTRL key
        /// </summary>
        RControlKey = 163,
        /// <summary>
        /// The left ALT key
        /// </summary>
        LMenu = 164,
        /// <summary>
        /// The right ALT key
        /// </summary>
        RMenu = 165,
        /// <summary>
        /// The CLEAR key
        /// </summary>
        OemClear = 254,
        /// <summary>
        /// The bitmask to extract a key code from a key value
        /// </summary>
        KeyCode = 65535,
        /// <summary>
        /// The SHIFT modifier key
        /// </summary>
        Shift = 65536,
        /// <summary>
        /// The CTRL modifier key
        /// </summary>
        Control = 131072,
        /// <summary>
        /// The ALT modifier key
        /// </summary>
        Alt = 262144
    }

    /// <summary>
    /// Holds data about a hotkey
    /// </summary>
    public class ShortcutsKey
    {
        private bool _used; // indicates if shortcut is registered at windows
        private bool _down; // indicates if shortcut is currently pressed
        public bool valid;  // indicates if a valid key/modifier pair was ever set
        public KeyEventArgs key;
        public bool Used
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
            key = new KeyEventArgs(Keys.None);
        }

        /// <summary>
        /// Returns the name of the key and its modifiers
        /// </summary>
        /// <returns>Description</returns>
        public string GetDescriptionString()
        {
            if (key.KeyCode == Keys.None) return "None";

            string Description = "";
            if (!valid) Description += "ERROR:   ";
            if (key.Alt) Description += "ALT  +  ";
            if (key.Control) Description += "CTRL  +  ";
            if (key.Shift) Description += "SHIFT  +  ";

            if (OperatingSystem.IsWindows())
            {
                return Description + NativeApi.GetKeyName(key.KeyValue);
            }
            else
            {
                return Description + "(?)";
            }
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
            if (!App.IsKeyPressedAsync(key.KeyValue)) return false;
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

    public enum SC_Type
    {
        SC_Type_Reset = 0,
        SC_Type_Hit = 1,
        SC_Type_Split = 2,
        // Since version 1.15:
        SC_Type_HitUndo = 3,
        SC_Type_SplitPrev = 4,
        // Since version 1.17:
        SC_Type_WayHit = 5,
        SC_Type_WayHitUndo = 6,
        SC_Type_PB = 7,
        // Since version 1.20:
        SC_Type_TimerStart = 8,
        SC_Type_TimerStop = 9,
        SC_Type_MAX = 10
    };

    /// <summary>
    /// Manages all hot keys aka shortcuts
    /// </summary>
    public class Shortcuts
    {
        private const int MOD_ALT = 0x0001;
        private const int MOD_CONTROL = 0x0002;
        private const int MOD_SHIFT = 0x0004;
        private const int VK_SHIFT = 0x10;
        private const int VK_CONTROL = 0x11;
        private const int VK_MENU = 0x12;
        private const int VK_LSHIFT = 0xA0;
        private const int VK_RSHIFT = 0xA1;
        private const int VK_LCONTROL = 0xA2;
        private const int VK_RCONTROL = 0xA3;
        private const int VK_LMENU = 0xA4;
        private const int VK_RMENU = 0xA5;

        public enum SC_HotKeyMethod {
            SC_HotKeyMethod_Sync = 0, // = RegisterHotKey + UnregisterHotKey
            SC_HotKeyMethod_Async = 1, // = SetTimer + KillTimer + GetAsyncKeyState + SendMessage
            SC_HotKeyMethod_LLKb = 2 // = SetWindowsHookEx (low level keyboard hook) + UnhookWindowsHookEx + SendMessage
        };

        private IntPtr hwnd;
        private readonly ShortcutsKey[] sc_list = new ShortcutsKey[(int)SC_Type.SC_Type_MAX];
        private SC_HotKeyMethod method;

        public SC_HotKeyMethod NextStart_Method;

        /// <summary>
        /// Build empty hot key list, save window handle.
        /// Call Initialize() to start using hot keys
        /// </summary>
        public Shortcuts()
        {
            for (int i = 0; i < (int)SC_Type.SC_Type_MAX; i++) sc_list[i] = new ShortcutsKey();
        }

        /// <summary>
        /// Destroy object (and free timer)
        /// </summary>
        ~Shortcuts()
        {
            if (method == SC_HotKeyMethod.SC_HotKeyMethod_Async) App.CurrentApp.StopApplicationTimer(TimerIDs.Shortcuts);
            else if (method == SC_HotKeyMethod.SC_HotKeyMethod_LLKb)
            {
                App.StopKeyboardLowLevelHook();
                App.CurrentApp.LowLevelKeyboardEvent -= LowLevelKeyboardEventHandler;
            }
        }

        /// <summary>
        /// Initializes object by setting the method (and configure timer)
        /// </summary>
        public void Initialize(SC_HotKeyMethod HotKeyMethod, IntPtr WindowHandle)
        {
            hwnd = WindowHandle;
            NextStart_Method = method = HotKeyMethod;

            for (int i = 0; i < (int)SC_Type.SC_Type_MAX; i++) sc_list[i] = new ShortcutsKey();

            if (method == SC_HotKeyMethod.SC_HotKeyMethod_Async)
            {
                App.CurrentApp.StartApplicationTimer(TimerIDs.Shortcuts, 2, () => { LowLevelKeyboardEventHandler(); return true; } );
            }
            else if (method == SC_HotKeyMethod.SC_HotKeyMethod_LLKb)
            {
                App.CurrentApp.LowLevelKeyboardEvent += LowLevelKeyboardEventHandler;
                App.StartKeyboardLowLevelHook();
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
                    if (App.SetHotKey(hwnd, (int)Id, modifier, key.key.KeyValue))
                    {
                        key.Used = true;
                        key.valid = true;
                    }
                    else
                    {
                        // anything went wrong while registering, clear key..
                        key.Used = false;
                        key.valid = false;
                    }
                }
                else
                {
                    App.KillHotKey(hwnd, (int)Id);
                    key.Used = false;
                }
            }
            else if ((method == SC_HotKeyMethod.SC_HotKeyMethod_Async) || (method == SC_HotKeyMethod.SC_HotKeyMethod_LLKb))
            {
                if (Enable)
                {
                    if (App.SetHotKey(hwnd, (int)Id, modifier, key.key.KeyValue))
                    {
                        App.KillHotKey(hwnd, (int)Id); // don't use this method, we just used registration to check if keycode is valid and works
                        key.Used = true;
                        key.valid = true;
                    }
                    else
                    {
                        // anything went wrong while registering, clear key..
                        key.Used = false;
                        key.valid = false;
                    }
                }
                else key.Used = false;
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
            if (Key_Get(Id).Used) HotKeyRegister(Id, key, false);

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

            if (!key.Used && IsEnabled) HotKeyRegister(Id, key, true);
            else if (key.Used && !IsEnabled) HotKeyRegister(Id, key, false);

            sc_list.SetValue(key, (int)Id);
        }

        /// <summary>
        /// Get the bound key of a shortcut
        /// </summary>
        public ShortcutsKey Key_Get(SC_Type Id)
        {
            ShortcutsKey key = (ShortcutsKey)sc_list.GetValue((int)Id)!;
            return key.ShallowCopy();
        }

        /// <summary>
        /// Low Level Keyboard message handler to check for hot keys
        /// </summary>
        private void LowLevelKeyboardEventHandler()
        {
            bool k_shift;
            bool k_control;
            bool k_alt;

            k_shift = App.IsKeyPressedAsync(VK_SHIFT) || App.IsKeyPressedAsync(VK_LSHIFT) || App.IsKeyPressedAsync(VK_RSHIFT);
            k_control = App.IsKeyPressedAsync(VK_CONTROL) || App.IsKeyPressedAsync(VK_LCONTROL) || App.IsKeyPressedAsync(VK_RCONTROL);
            k_alt = App.IsKeyPressedAsync(VK_MENU) || App.IsKeyPressedAsync(VK_LMENU) || App.IsKeyPressedAsync(VK_RMENU);

            for (int i = 0; i < (int)SC_Type.SC_Type_MAX; i++)
            {
                if (sc_list[i].Used)
                {
                    if (sc_list[i].WasPressed(k_shift, k_control, k_alt)) App.SendHotKeyMessage(hwnd, (IntPtr)i, (IntPtr)0);
                }
            }
        }
    }
}
