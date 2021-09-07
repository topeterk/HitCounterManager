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
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;
using HitCounterManager.Common.PlatformLayer;

namespace HitCounterManager.GTK.PlatformLayer
{
    public class PlatformLayer : IPlatformLayer
    {
        private readonly FormsWindow Window; // https://developer.gnome.org/pygtk/stable/class-gtkwindow.html#method-gtkwindow--resize

        public PlatformLayer(FormsWindow window) { Window = window; }

        public string Name { get => "GTK"; }

        public Point ApplicationWindowPosition
        {
            get
            {
                int left, top;
                Window.GetPosition(out left, out top);
                return new Point(left, top);
            }
            set => Window.Move((int)value.X, (int)value.Y);
        }
        public Size ApplicationWindowSize
        {
            get
            {
                int width, height;
                Window.GetSize(out width, out height);
                return new Size(width, height);
            }
            set => Window.Resize((int)value.Width, (int)value.Height);
        }
        public Size ApplicationWindowMinSize
        {
            get
            {
                int width, height;
                Window.GetSizeRequest(out width, out height);
                return new Size(width, height);
            }
            set => Window.SetSizeRequest((int)value.Width, (int)value.Height);
        }

        public IntPtr ApplicationWindowHandle { get => Window.Handle; }
        private bool _ApplicationWindowTopMost = false;
        public bool ApplicationWindowTopMost { get => _ApplicationWindowTopMost; set => Window.KeepAbove = _ApplicationWindowTopMost = value; }

        public bool IsTitleBarOnScreen(int Left, int Top, int Width, int Threshold, int RectSize)
        {
            Gdk.Rectangle rectLeft = new Gdk.Rectangle(Left + Threshold, Top + Threshold, RectSize, RectSize); // upper left corner
            Gdk.Rectangle rectRight = new Gdk.Rectangle(Left + Width - Threshold - RectSize, Top + Threshold, RectSize, RectSize); // upper right corner
            Gdk.Screen screen = Gdk.Screen.Default;
            for (int i = 0; i < screen.NMonitors; i++)
            {
                Gdk.Rectangle monitor = screen.GetMonitorGeometry(i);
                // at least one of the edges must be present on any screen
                if (monitor.Contains(rectLeft)) return true;
                else if (monitor.Contains(rectRight)) return true;
            }
            return false;
        }
        public HookHandle HookInstall(HookProc HookProc, IntPtr WindowHandle) { return null; }
        public void HookUninstall(HookHandle HookHandle) { return; }
    }
}
