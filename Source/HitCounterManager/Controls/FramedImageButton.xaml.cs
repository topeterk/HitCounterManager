// SPDX-FileCopyrightText: © 2021-2025 Peter Kirmeier
// SPDX-License-Identifier: MIT

using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace HitCounterManager.Controls
{
    public partial class FramedImageButton : UserControl
    {
        // Info about StyledProperty, see: https://docs.avaloniaui.net/docs/authoring-controls/defining-properties
        public static readonly StyledProperty<ICommand> OnClickCommandProperty = AvaloniaProperty.Register<FramedImageButton, ICommand>(nameof(OnClickCommand));
        public static readonly StyledProperty<ICommand> OnRightClickCommandProperty = AvaloniaProperty.Register<FramedImageButton, ICommand>(nameof(OnRightClickCommandProperty));
        public static readonly StyledProperty<IImage> ImgSrcProperty = AvaloniaProperty.Register<FramedImageButton, IImage>(nameof(ImgSrc));
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

        public IImage ImgSrc
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
