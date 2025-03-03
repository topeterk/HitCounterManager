//MIT License

//Copyright(c) 2016-2025 Peter Kirmeier

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
    /// <summary>
    /// Extended subset of Windows Virtual-key Codes.
    /// See: https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
    /// (Non-)extended values for virtual-key codes match with .Net Framework's System.Windows.Forms.Keys.
    /// (Non-)extended values and names for virtual-key codes match with Avalonia.Win32.Interop.UnmanagedMethods.VirtualKeyStates.
    /// </summary>
    public enum VirtualKeyStates : int
    {
        #region Custom definitions

        /// <summary>
        /// No key
        /// </summary>
        None = 0,

        // Masks.

        /// <summary>
        /// The bitmask to extract a key code from a key value
        /// </summary>
        KeyCode = 0xFFFF,

        // Modifiers

        /// <summary>
        /// The SHIFT modifier key
        /// </summary>
        Shift = 0x10000,
        /// <summary>
        /// The CTRL modifier key
        /// </summary>
        Control = 0x20000,
        /// <summary>
        /// The ALT modifier key
        /// </summary>
        Alt = 0x40000,

        #endregion

        #region Non-extended keys

        /// <summary>
        /// The BACKSPACE key
        /// </summary>
        VK_BACK = 0x08,
        /// <summary>
        /// The SHIFT key
        /// </summary>
        VK_SHIFT = 0x10,
        /// <summary>
        /// The CTRL key
        /// </summary>
        VK_CONTROL = 0x11,
        /// <summary>
        /// The ALT key
        /// </summary>
        VK_MENU = 0x12,
        /// <summary>
        /// The F1 key
        /// </summary>
        VK_F1 = 0x70,
        /// <summary>
        /// The F2 key
        /// </summary>
        VK_F2 = 0x71,
        /// <summary>
        /// The F3 key
        /// </summary>
        VK_F3 = 0x72,
        /// <summary>
        /// The F4 key
        /// </summary>
        VK_F4 = 0x73,
        /// <summary>
        /// The F5 key
        /// </summary>
        VK_F5 = 0x74,
        /// <summary>
        /// The F6 key
        /// </summary>
        VK_F6 = 0x75,
        /// <summary>
        /// The F7 key
        /// </summary>
        VK_F7 = 0x76,
        /// <summary>
        /// The F8 key
        /// </summary>
        VK_F8 = 0x77,
        /// <summary>
        /// The F9 key
        /// </summary>
        VK_F9 = 0x78,
        /// <summary>
        /// The F10 key
        /// </summary>
        VK_F10 = 0x79,
        /// <summary>
        /// The F11 key
        /// </summary>
        VK_F11 = 0x7A,
        /// <summary>
        /// The F12 key
        /// </summary>
        VK_F12 = 0x7B,
        /// <summary>
        /// The left SHIFT key
        /// </summary>
        VK_LSHIFT = 0xA0,
        /// <summary>
        /// The right SHIFT key
        /// </summary>
        VK_RSHIFT = 0xA1,
        /// <summary>
        /// The left CTRL key
        /// </summary>
        VK_LCONTROL = 0xA2,
        /// <summary>
        /// The right CTRL key
        /// </summary>
        VK_RCONTROL = 0xA3,
        /// <summary>
        /// The left ALT key
        /// </summary>
        VK_LMENU = 0xA4,
        /// <summary>
        /// The right ALT key
        /// </summary>
        VK_RMENU = 0xA5,
        /// <summary>
        /// The CLEAR key
        /// </summary>
        VK_OEM_CLEAR = 0xFE,

        #endregion

        #region Extended keys

        /// <summary>
        /// The PAUSE key
        /// </summary>
        VK_PAUSE = 0x13,
        /// <summary>
        /// The PAGE UP key
        /// </summary>
        VK_PRIOR = 0x21,
        /// <summary>
        /// The PAGE DOWN key
        /// </summary>
        VK_NEXT = 0x22,
        /// <summary>
        /// The END key
        /// </summary>
        VK_END = 0x23,
        /// <summary>
        /// The HOME key
        /// </summary>
        VK_HOME = 0x24,
        /// <summary>
        /// The LEFT key
        /// </summary>
        VK_LEFT = 0x25,
        /// <summary>
        /// The UP key
        /// </summary>
        VK_UP = 0x26,
        /// <summary>
        /// The RIGHT key
        /// </summary>
        VK_RIGHT = 0x27,
        /// <summary>
        /// The DOWN key
        /// </summary>
        VK_DOWN = 0x28,
        /// <summary>
        /// The PRINT SCREEN key
        /// </summary>
        VK_SNAPSHOT = 0x2C,
        /// <summary>
        /// The INSERT key
        /// </summary>
        VK_INSERT = 0x2D,
        /// <summary>
        /// The DELETE key
        /// </summary>
        VK_DELETE = 0x2E,
        /// <summary>
        /// The NUMLOCK key
        /// </summary>
        VK_NUMLOCK = 0x90,
        /// <summary>
        /// The MULTIPLY key
        /// </summary>
        VK_MULTIPLY = 0x6A,
        /// <summary>
        /// The Add (Num) key
        /// </summary>
        VK_ADD = 0x6B,
        /// <summary>
        /// The Subtract (Num) key
        /// </summary>
        VK_SUBTRACT = 0x6D,
        /// <summary>
        /// The DIVIDE key
        /// </summary>
        VK_DIVIDE = 0x6F,

        #endregion
    }

    /// <summary>
    /// Holds data about a hotkey
    /// </summary>
    public class ShortcutsKey(VirtualKeyStates keyData)
    {
        private bool _used = false; // indicates if shortcut is registered at windows
        private bool _down = false; // indicates if shortcut is currently pressed
        public bool valid = false;  // indicates if a valid key/modifier pair was ever set
        public bool Used
        {
            get { return _used; }
            set
            {
                CheckPressedState(false, false, false); // flush current state
                _used = value;
            }
        }

        public ShortcutsKey() : this(VirtualKeyStates.None) { }

        /// <summary>
        /// Representing the key code for the key that was pressed, combined with modifier flags
        /// </summary>
        public readonly VirtualKeyStates KeyData = keyData;

        /// <summary>
        /// Representing the key code for the key that was pressed, without any modifier flags
        /// </summary>
        public VirtualKeyStates KeyCode => VirtualKeyStates.KeyCode & KeyData;

        /// <summary>
        /// Gets a value indicating whether the ALT key was pressed
        /// </summary>
        public bool Alt => 0 != (VirtualKeyStates.Alt & KeyData);
        /// <summary>
        /// Gets a value indicating whether the CTRL key was pressed
        /// </summary>
        public bool Control => 0 != (VirtualKeyStates.Control & KeyData);
        /// <summary>
        /// Gets a value indicating whether the SHIFT key was pressed
        /// </summary>
        public bool Shift => 0 != (VirtualKeyStates.Shift & KeyData);

        /// <summary>
        /// Returns the name of the key and its modifiers
        /// </summary>
        /// <returns>Description</returns>
        public string GetDescriptionString()
        {
            if (KeyCode == VirtualKeyStates.None) return "None";

            string Description = "";
            if (!valid) Description += "ERROR:   ";
            if (Alt) Description += "ALT  +  ";
            if (Control) Description += "CTRL  +  ";
            if (Shift) Description += "SHIFT  +  ";

            if (OperatingSystem.IsWindows())
            {
                return Description + NativeApi.GetKeyName(KeyCode);
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
            if (!App.IsKeyPressedAsync(KeyCode)) return false;
            if (Shift && !ShiftState) return false;
            if (Control && !ControlState) return false;
            if (Alt && !AltState) return false;
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
        #region AutoSplitter
        SC_Type_Practice = 10,
        #endregion
        SC_Type_HitBossPrev = 11,
        SC_Type_HitWayPrev = 12,
        SC_Type_BossHitUndoPrev = 13,
        SC_Type_WayHitUndoPrev = 14,
        SC_Type_MAX = 15,
    };

    /// <summary>
    /// Manages all hot keys aka shortcuts
    /// </summary>
    public class Shortcuts
    {
        private const int MOD_ALT = 0x0001;
        private const int MOD_CONTROL = 0x0002;
        private const int MOD_SHIFT = 0x0004;

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
            for (int i = 0; i < (int)SC_Type.SC_Type_MAX; i++) sc_list[i] = new ();
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

            for (int i = 0; i < (int)SC_Type.SC_Type_MAX; i++) sc_list[i] = new ();

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

            if (key.Shift) modifier += MOD_SHIFT;
            if (key.Control) modifier += MOD_CONTROL;
            if (key.Alt) modifier += MOD_ALT;

            if (method == SC_HotKeyMethod.SC_HotKeyMethod_Sync)
            {
                if (Enable)
                {
                    if (App.SetHotKey(hwnd, (int)Id, modifier, key.KeyCode))
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
                    if (App.SetHotKey(hwnd, (int)Id, modifier, key.KeyCode))
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
        public ShortcutsKey Key_Get(SC_Type Id) => sc_list[(int)Id].ShallowCopy();

        /// <summary>
        /// Low Level Keyboard message handler to check for hot keys
        /// </summary>
        private void LowLevelKeyboardEventHandler()
        {
            bool k_shift;
            bool k_control;
            bool k_alt;

            k_shift = App.IsKeyPressedAsync(VirtualKeyStates.VK_SHIFT) || App.IsKeyPressedAsync(VirtualKeyStates.VK_LSHIFT) || App.IsKeyPressedAsync(VirtualKeyStates.VK_RSHIFT);
            k_control = App.IsKeyPressedAsync(VirtualKeyStates.VK_CONTROL) || App.IsKeyPressedAsync(VirtualKeyStates.VK_LCONTROL) || App.IsKeyPressedAsync(VirtualKeyStates.VK_RCONTROL);
            k_alt = App.IsKeyPressedAsync(VirtualKeyStates.VK_MENU) || App.IsKeyPressedAsync(VirtualKeyStates.VK_LMENU) || App.IsKeyPressedAsync(VirtualKeyStates.VK_RMENU);

            for (int i = 0; i < (int)SC_Type.SC_Type_MAX; i++)
            {
                if (sc_list[i].Used)
                {
                    if (sc_list[i].WasPressed(k_shift, k_control, k_alt)) App.SendHotKeyMessage(hwnd, i, IntPtr.Zero);
                }
            }
        }
    }
}
