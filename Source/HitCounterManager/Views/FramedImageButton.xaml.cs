//MIT License

//Copyright (c) 2021-2022 Peter Kirmeier

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

using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System.Windows.Input;

namespace HitCounterManager.Views
{
    public partial class FramedImageButton : UserControl
    {
        // TODO Avalonia Reference ok? https://docs.avaloniaui.net/docs/authoring-controls/defining-properties
        // TODO Avalonia Reference ok? https://docs.avaloniaui.net/docs/data-binding/creating-and-binding-attached-properties
        public static readonly StyledProperty<ICommand> OnClickCommandProperty = AvaloniaProperty.Register<FramedImageButton, ICommand>(nameof(OnClickCommand));
        public static readonly StyledProperty<Bitmap> ImgSrcProperty = AvaloniaProperty.Register<FramedImageButton, Bitmap>(nameof(ImgSrc));
        public static readonly StyledProperty<IBrush> MainColorProperty = AvaloniaProperty.Register<FramedImageButton, IBrush>(nameof(MainColor));

        public FramedImageButton()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public ICommand OnClickCommand
        {
            get => GetValue(OnClickCommandProperty);
            set => SetValue(OnClickCommandProperty, value);
        }

        public Bitmap ImgSrc
        {
            get => GetValue(ImgSrcProperty);
            set => SetValue(ImgSrcProperty, value);
        }

        public IBrush MainColor
        {
            get => GetValue(MainColorProperty);
            set => SetValue(MainColorProperty, value);
        }
    }
}
