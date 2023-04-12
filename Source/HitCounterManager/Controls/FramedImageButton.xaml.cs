//MIT License

//Copyright (c) 2021-2023 Peter Kirmeier

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

using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace HitCounterManager.Controls
{
    public partial class FramedImageButton : UserControl
    {
        // Info about StyledProperty, see: https://docs.avaloniaui.net/docs/authoring-controls/defining-properties
        public static readonly StyledProperty<ICommand> OnClickCommandProperty = AvaloniaProperty.Register<FramedImageButton, ICommand>(nameof(OnClickCommand));
        public static readonly StyledProperty<ICommand> OnRightClickCommandProperty = AvaloniaProperty.Register<FramedImageButton, ICommand>(nameof(OnRightClickCommandProperty));
        public static readonly StyledProperty<Bitmap> ImgSrcProperty = AvaloniaProperty.Register<FramedImageButton, Bitmap>(nameof(ImgSrc));
        public static readonly StyledProperty<IBrush> MainColorProperty = AvaloniaProperty.Register<FramedImageButton, IBrush>(nameof(MainColor));

        public FramedImageButton()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public ICommand OnClickCommand
        {
            get => GetValue(OnClickCommandProperty);
            set => SetValue(OnClickCommandProperty, value);
        }

        public ICommand OnRightClickCommand
        {
            get => GetValue(OnRightClickCommandProperty);
            set => SetValue(OnRightClickCommandProperty, value);
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

        void ButtonPointerPressedHandler(object? sender, PointerPressedEventArgs e)
        {
            if (PointerUpdateKind.RightButtonPressed == e.GetCurrentPoint(this).Properties.PointerUpdateKind)
            {
                if (OnRightClickCommand?.CanExecute(null) ?? false)
                {
                    OnRightClickCommand.Execute(null);
                }
            }
        }
    }
}
