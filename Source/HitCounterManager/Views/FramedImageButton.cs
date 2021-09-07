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

using Xamarin.Forms;

namespace HitCounterManager.Views
{
    // example: https://dev.to/codeprototype/learn-xamarin-forms-control-template-by-making-a-button-with-built-in-loading-indicator-in-4-steps-3lkm

    public class FramedImageButton : ContentView
    {
        public static readonly BindableProperty OnClickCommandProperty = BindableProperty.Create(nameof(OnClickCommand), typeof(Command), typeof(FramedImageButton), null);
        public static readonly BindableProperty ImgSrcProperty = BindableProperty.Create(nameof(ImgSrc), typeof(ImageSource), typeof(FramedImageButton), null);
        public static readonly BindableProperty MainColorProperty = BindableProperty.Create(nameof(MainColor), typeof(Color), typeof(FramedImageButton), null);

        public Command OnClickCommand
        {
            get => (Command)GetValue(OnClickCommandProperty);
            set => SetValue(OnClickCommandProperty, value);
        }

        public ImageSource ImgSrc
        {
            get => (ImageSource)GetValue(ImgSrcProperty);
            set => SetValue(ImgSrcProperty, value);
        }
        
        public Color MainColor
        {
            get => (Color)GetValue(MainColorProperty);
            set => SetValue(MainColorProperty, value);
        }
    }
}
