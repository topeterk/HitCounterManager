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
using System.Windows.Forms;
using System.Windows.Interop;
using Xamarin.Forms;
using HitCounterManager.Common.PlatformLayer;


namespace HitCounterManager.WPF.PlatformLayer
{
    public class PlatformLayer : IPlatformLayer
    {
        private readonly MainWindow Window;

        public PlatformLayer(MainWindow window) { Window = window; }

        public string Name { get => "WPF"; }
        public Point ApplicationWindowPosition
        {
            get => new Point(Window.Left, Window.Top);
            set
            {
                Window.Left = value.X;
                Window.Top = value.Y;
            }
        }
        public Size ApplicationWindowSize
        {
            get => new Size(Window.Width, Window.Height);
            set
            {
                Window.Width = value.Width;
                Window.Height = value.Height;
            }
        }
        public Size ApplicationWindowMinSize
        {
            get => new Size(Window.MinWidth, Window.MinHeight);
            set
            {
                Window.MinWidth = value.Width;
                Window.MinHeight = value.Height;
            }
        }

        public IntPtr ApplicationWindowHandle { get => new WindowInteropHelper(Window).Handle; }
        public bool IsTitleBarOnScreen(int Left, int Top, int Width, int Threshold, int RectSize)
        {
            System.Drawing.Rectangle rectLeft = new System.Drawing.Rectangle(Left + Threshold, Top + Threshold, RectSize, RectSize); // upper left corner
            System.Drawing.Rectangle rectRight = new System.Drawing.Rectangle(Left + Width - Threshold - RectSize, Top + Threshold, RectSize, RectSize); // upper right corner
            foreach (Screen screen in Screen.AllScreens)
            {
                // at least one of the edges must be present on any screen
                if (screen.WorkingArea.Contains(rectLeft)) return true;
                else if (screen.WorkingArea.Contains(rectRight)) return true;
            }
            return false;
        }

        public bool ApplicationWindowTopMost { get => Window.Topmost; set => Window.Topmost = value; }

        public HookHandle HookInstall(HookProc HookProc, IntPtr WindowHandle)
        {
            HwndSourceHook hook = new HwndSourceHook(HookProc);
            HwndSource.FromHwnd(WindowHandle).AddHook(hook);
            return new HookHandle(hook, WindowHandle);
        }
        public void HookUninstall(HookHandle HookHandle)
        {
            // Most likey window is already destroyed, so remove it just when window is still alive
            HwndSource.FromHwnd(HookHandle.WindowHandle)?.RemoveHook((HwndSourceHook)HookHandle.HookProcObj);
        }
    }
}
