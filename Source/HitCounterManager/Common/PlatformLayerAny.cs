//MIT License

//Copyright (c) 2021-2021 Peter Kirmeier

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
using Xamarin.Forms;

namespace HitCounterManager.Common.PlatformLayer
{
    #region Platform Layer Interface

    #region Types

    public delegate IntPtr HookProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled);

    /// <summary>
    /// Hook handle for the platform layer implementation
    /// </summary>
    public class HookHandle
    {
        /// <summary>
        /// Creates a handle that can hold any object and a window handle
        /// </summary>
        /// <param name="hookProcObj">Internal object of the hook</param>
        /// <param name="windowHandle">The assigned window handle</param>
        public HookHandle(object hookProcObj, IntPtr windowHandle)
        {
            HookProcObj = hookProcObj;
            WindowHandle = windowHandle;
        }

        /// <summary>
        /// Returns the internal object of this hook handle
        /// </summary>
        public object HookProcObj { get; internal set; }

        /// <summary>
        /// Returns the window handle that is assiged to this hook
        /// </summary>
        public IntPtr WindowHandle { get; internal set; }
    }
    
    #endregion
    public interface IPlatformLayer
    {
        /// <summary>
        /// Name of this Platform implementation
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets the location of the application window
        /// </summary>
        Point ApplicationWindowPosition { get; set; }

        /// <summary>
        /// Gets or sets the size of the application window
        /// </summary>
        Size ApplicationWindowSize { get; set; }

        /// <summary>
        /// Gets or sets the minimum size of the application window
        /// </summary>
        Size ApplicationWindowMinSize { get; set; }

        /// <summary>
        /// Gets the window handle of the application window
        /// </summary>
        IntPtr ApplicationWindowHandle { get; }

        /// <summary>
        /// Gets or sets the application window as topmost
        /// </summary>
        bool ApplicationWindowTopMost { get; set; }

        /// <summary>
        /// Checks if the upper corners of a given window location is visible on any available screen.
        /// This makes sure the taskbar is visible at this location and the user can move or interact with the window.
        /// </summary>
        /// <param name="Left">X-coordiate of the window's title bar location</param>
        /// <param name="Top">Y-coordiate of the window's title bar location</param>
        /// <param name="Width">Width of the window's title bar</param>
        /// <param name="Threshold">Additional margin.
        ///                         Ensures moving directly at the outer borders of the screens is still fine.
        ///                         Helps checking versus fullscreen or anchored windows.</param>
        /// <param name="RectSize">Minimum size of the windows's title bar that must be visible.</param>
        /// <returns>TRUE = Window's title bar is visible on at least one screen.
        ///          FALSE = Window is not visible on any screen.</returns>
        bool IsTitleBarOnScreen(int Left, int Top, int Width, int Threshold = 10, int RectSize = 30);

        /// <summary>
        /// Installing a hook for window messages for a specific window
        /// </summary>
        /// <param name="HookProc">Function that is called for each message</param>
        /// <param name="WindowHandle">Handle of the specific window</param>
        /// <returns>Handle for this hook or null on error</returns>
        HookHandle HookInstall(HookProc HookProc, IntPtr WindowHandle);

        /// <summary>
        /// Uninstalls a hook that was installed with HookInstall
        /// </summary>
        /// <param name="HookHandle">Handle that was returned by HookInstall</param>
        void HookUninstall(HookHandle HookHandle);

        /// <summary>
        /// Indicates if this platform is capable of changing the app theme on the fly
        /// </summary>
        bool AppThemeBindingSupport { get; }
    }

    #endregion

    #region Default Platform Layer
    public class PlatformLayerDefault : IPlatformLayer
    {
        public string Name => "PCL";
        public Point ApplicationWindowPosition { get; set; }
        public Size ApplicationWindowSize { get; set;}
        public Size ApplicationWindowMinSize { get; set; }
        public IntPtr ApplicationWindowHandle => IntPtr.Zero;
        public bool IsTitleBarOnScreen(int Left, int Top, int Width, int Threshold, int RectSize) { return true; }
        public HookHandle HookInstall(HookProc HookProc, IntPtr WindowHandle) { return null; }
        public void HookUninstall(HookHandle HookHandle) { return; }
        public bool ApplicationWindowTopMost { get; set; }
        public bool AppThemeBindingSupport => true;
    }
    #endregion
}
