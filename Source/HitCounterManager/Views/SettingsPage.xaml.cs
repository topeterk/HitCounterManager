﻿//MIT License

//Copyright (c) 2021-2025 Peter Kirmeier

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

using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using HitCounterManager.ViewModels;

namespace HitCounterManager.Views
{
    public partial class SettingsPage : PageBase<SettingsPageViewModel>
    {
        public SettingsPage()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void SubSplitVisibilityCheckedChanged(object sender, RoutedEventArgs args)
        {
            if (sender is RadioButton radioButton && radioButton.Content is string content)
            {
                if (radioButton.IsChecked.HasValue && radioButton.IsChecked.Value)
                {
                    ViewModel.SetSubSplitVisibility(content);
                }
            }
        }

        public void TableAlignmentGroupCheckedChanged(object sender, RoutedEventArgs args)
        {
            if (sender is RadioButton radioButton && radioButton.Content is string content)
            {
                if (radioButton.IsChecked.HasValue && radioButton.IsChecked.Value)
                {
                    ViewModel.SetStyleTableAlignment(content);
                }
            }
        }
    }
}
