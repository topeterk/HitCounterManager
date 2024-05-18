//MIT License

//Copyright (c) 2023-2024 Peter Kirmeier

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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Microsoft.Win32;

namespace HitCounterManager
{
    internal static class NativeApi
    {
        internal delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        #region Windows API

        // Datatypes: https://docs.microsoft.com/en-us/windows/win32/winprog/windows-data-types

        #region Window Subclass

        internal const int WM_HOTKEY = 0x0312;

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
        internal delegate IntPtr Subclassproc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, IntPtr uIdSubclass, IntPtr dwRefData);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/commctrl/nf-commctrl-setwindowsubclass
        /// </summary>
        /// <param name="hWnd">HWND = HANDLE = PVOID = void*</param>
        /// <param name="pfnSubclass">SUBCLASSPROC</param>
        /// <param name="uIdSubclass">UINT_PTR = __int64 or long</param>
        /// <param name="dwRefData">DWORD_PTR = ULONG_PTR = __int64 or long</param>
        /// <returns>BOOL = int</returns>
        [SupportedOSPlatform("Windows")]
        [DllImport("Comctl32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        internal static extern int SetWindowSubclass(IntPtr hWnd, Subclassproc pfnSubclass, IntPtr uIdSubclass, IntPtr dwRefData);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/commctrl/nf-commctrl-removewindowsubclass
        /// </summary>
        /// <param name="hWnd">HWND = HANDLE = PVOID = void*</param>
        /// <param name="pfnSubclass">SUBCLASSPROC</param>
        /// <param name="uIdSubclass">UINT_PTR = __int64 or long</param>
        /// <returns>BOOL = int</returns>
        [SupportedOSPlatform("Windows")]
        [DllImport("Comctl32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        internal static extern int RemoveWindowSubclass(IntPtr hWnd, Subclassproc pfnSubclass, IntPtr uIdSubclass);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/commctrl/nf-commctrl-defsubclassproc
        /// </summary>
        /// <param name="hWnd">HWND = HANDLE = PVOID = void*</param>
        /// <param name="uMsg">UINT = unsigned int</param>
        /// <param name="wParam">WPARAM = UINT_PTR = __int64 or long</param>
        /// <param name="lParam">LPARAM = LONG_PTR = __int64 or long</param>
        /// <returns>LRESULT = LONG_PTR = __int64 or long</returns>
        [SupportedOSPlatform("Windows")]
        [DllImport("Comctl32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        internal static extern IntPtr DefSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendmessagew
        /// </summary>
        /// <param name="hWnd">HWND = HANDLE = PVOID = void*</param>
        /// <param name="Msg">UINT = unsigned int</param>
        /// <param name="wParam">WPARAM = UINT_PTR = __int64 or long</param>
        /// <param name="lParam">LPARAM = LONG_PTR = __int64 or long</param>
        /// <returns>LRESULT = LONG_PTR = __int64 or long</returns>
        [SupportedOSPlatform("Windows")]
        [DllImport("User32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        internal static extern IntPtr SendMessageW(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        #endregion

        #region Window Message Hook

        internal const int WH_KEYBOARD_LL = 13;

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexw
        /// </summary>
        /// <param name="idHook">int</param>
        /// <param name="lpfn">HOOKPROC</param>
        /// <param name="hMod">HINSTANCE = HANDLE = PVOID = void*</param>
        /// <param name="dwThreadId">DWORD = unsigned long</param>
        /// <returns>HHOOK = HANDLE = PVOID = void*</returns>
        [SupportedOSPlatform("Windows")]
        [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        internal static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-unhookwindowshookex
        /// </summary>
        /// <param name="hhk">HHOOK = HANDLE = PVOID = void*</param>
        /// <returns>BOOL = int</returns>
        [SupportedOSPlatform("Windows")]
        [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        internal static extern int UnhookWindowsHookEx(IntPtr hhk);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-callnexthookex
        /// </summary>
        /// <param name="hhk">HHOOK = HANDLE = PVOID = void*</param>
        /// <param name="nCode">int</param>
        /// <param name="wParam">WPARAM = UINT_PTR = __int64 or long</param>
        /// <param name="lParam">LPARAM = LONG_PTR = __int64 or long</param>
        /// <returns>LRESULT = LONG_PTR = __int64 or long</returns>
        [SupportedOSPlatform("Windows")]
        [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-getmodulehandlew
        /// </summary>
        /// <param name="lpModuleName">LPCWSTR = CONST WCHAR * = const wchar_t *</param>
        /// <returns>HMODULE = HINSTANCE = HANDLE = PVOID = void*</returns>
        [SupportedOSPlatform("Windows")]
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        internal static extern IntPtr GetModuleHandleW(string lpModuleName);

        #endregion

        #region Hot Key

        internal const nint WM_KEYDOWN = 0x0100;
        internal const nint WM_KEYUP = 0x0101;
        internal const nint WM_SYSKEYDOWN = 0x0104;
        internal const nint WM_SYSKEYUP = 0x0105;
        internal const int HC_ACTION = 0;
        internal const int KF_EXTENDED = 0x0100;
        internal const int KF_ALTDOWN = 0x2000;
        internal const int KF_UP = 0x8000;
        internal const int LLKHF_ALTDOWN = (KF_ALTDOWN >> 8);
        internal const int LLKHF_UP = (KF_UP >> 8);
        internal const int KEY_PRESSED_NOW = 0x8000;
        internal const uint MAPVK_VK_TO_VSC_EX = 4;

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-mapvirtualkeyw
        /// </summary>
        /// <param name="uCode">UINT = unsigned int</param>
        /// <param name="uMapType">UINT = unsigned int</param>
        /// <returns>UINT = unsigned int</returns>
        [SupportedOSPlatform("Windows")]
        [DllImport("User32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        internal static extern uint MapVirtualKeyW(uint uCode, uint uMapType);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeynametextw
        /// </summary>
        /// <param name="lParam">LONG = long</param>
        /// <param name="lpBuffer">LPWSTR = CONST WCHAR * = const wchar_t *</param>
        /// <param name="nSize">int</param>
        /// <returns>int</returns>
        [SupportedOSPlatform("Windows")]
        [DllImport("User32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        internal static extern int GetKeyNameTextW(int lParam, string lpBuffer, int nSize);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerhotkey
        /// </summary>
        /// <param name="hWnd">HWND = HANDLE = PVOID = void*</param>
        /// <param name="id">int</param>
        /// <param name="fsModifiers">UINT = unsigned int</param>
        /// <param name="vk">UINT = unsigned int</param>
        /// <returns>BOOL = int</returns>
        [SupportedOSPlatform("Windows")]
        [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        internal static extern int RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-unregisterhotkey
        /// </summary>
        /// <param name="hWnd">HWND = HANDLE = PVOID = void*</param>
        /// <param name="id">int </param>
        /// <returns>BOOL = int</returns>
        [SupportedOSPlatform("Windows")]
        [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        internal static extern int UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeyboardstate
        /// </summary>
        /// <param name="lpKeyState">PBYTE = BYTE * = unsigned char *</param>
        /// <returns>BOOL = int</returns>
        [SupportedOSPlatform("Windows")]
        [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        internal static extern int GetKeyboardState(byte[] lpKeyState);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getasynckeystate
        /// </summary>
        /// <param name="vKey">int</param>
        /// <returns>SHORT = short</returns>
        [SupportedOSPlatform("Windows")]
        [DllImport("User32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        internal static extern short GetAsyncKeyState(int vKey);

        // Known virtual-key codes that are extended keys.
        // See: https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
        internal const int VK_PAUSE = 0x13;
        internal const int VK_PRIOR = 0x21; // Page Up
        internal const int VK_NEXT = 0x22; // Page down
        internal const int VK_END = 0x23;
        internal const int VK_HOME = 0x24;
        internal const int VK_LEFT = 0x25;
        internal const int VK_UP = 0x26;
        internal const int VK_RIGHT = 0x27;
        internal const int VK_DOWN = 0x28;
        internal const int VK_SNAPSHOT = 0x2C; // Print Screen
        internal const int VK_INSERT = 0x2D;
        internal const int VK_DELETE = 0x2E;
        internal const int VK_NUMLOCK = 0x90;
        internal const int VK_MULTIPLY = 0x6A;
        internal const int VK_DIVIDE = 0x6F;

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
        [SupportedOSPlatform("Windows")]
        public static string GetKeyName(int VirtualKeyCode)
        {
            if (OperatingSystem.IsWindows())
            {
                // GetKeyNameText does not return proper names on some virtual-key codes.
                // Therefore just directly translate them
                switch (VirtualKeyCode)
                {
                    case VK_MULTIPLY:
                        return "*";
                    case VK_DIVIDE:
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
                        case VK_PAUSE:
                            scanCode = SC_PAUSE__LEGACY;
                            break;
                        case VK_PRIOR: // Page Up
                        case VK_NEXT: // Page down
                        case VK_END:
                        case VK_HOME:
                        case VK_LEFT:
                        case VK_UP:
                        case VK_RIGHT:
                        case VK_DOWN:
                        case VK_INSERT:
                        case VK_DELETE:
                        case VK_NUMLOCK:
                            scanCode |= KF_EXTENDED;
                            break;
                        case VK_SNAPSHOT: // Print Screen
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
            }
            return "(?)";
        }

        #endregion

        #endregion

        #region Windows Runtime Assembly

        [MethodImpl(MethodImplOptions.NoInlining)] // https://stackoverflow.com/questions/21914692/when-exactly-are-assemblies-loaded
        [SupportedOSPlatform("Windows")]
        internal static bool IsDarkModeActiveWin32()
        {
            try
            {
                // Check: AppsUseLightTheme (REG_DWORD)
                // 0 = Dark mode, 1 = Light mode
                object? value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", "AppsUseLightTheme", 1);
                return null != value && value?.ToString() == "0";
            }
            catch
            {
                // Catch exceptions when key is not working on this system
                return false;
            }
        }

        #endregion
    }
}
