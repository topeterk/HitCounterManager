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
using Xamarin.Forms.Platform.GTK;
using HitCounterManager.Common.OsLayer;

#if SHOW_COMPLER_VERSION // enable and hover over #error to see C# compiler version and the used language version
#error version
#endif

namespace HitCounterManager.GTK
{
    class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Gtk.Application.Init();
            Forms.Init();

            FormsWindow window = new FormsWindow();
            App app = new App(new OsLayerDefault(), new PlatformLayer.PlatformLayer(window));

            window.LoadApplication(app);
            window.SetApplicationTitle(HitCounterManager.Common.Statics.ApplicationTitle + " GTK");
            window.Show();

#if DEBUG_GTK_BUG_NAVIGATION_BAR // https://github.com/xamarin/Xamarin.Forms/issues/7492
            Gtk.Widget child = window.Child;
            Gdk.Window wind = child.GdkWindow;
            printloc(0, wind);
#endif

            Gtk.Application.Run();
        }

#if DEBUG_GTK_BUG_NAVIGATION_BAR // https://github.com/xamarin/Xamarin.Forms/issues/7492
        static void printloc(int i, Gdk.Window w)
        {
            int left, right, width, height;
            foreach( Gdk.Window cc in w.Children)
            {
                cc.GetPosition(out left, out right);
                cc.GetSize(out width, out height);
                for (int k = 0; k < i; k++) Console.Write(" "); 
                Console.WriteLine("> " + left.ToString() + " " + right.ToString() + " , " + width.ToString() + " " + height.ToString());
                printloc(i + 1, cc);
            }
        }
#endif
    }
}
