// SPDX-FileCopyrightText: © 2021-2025 Peter Kirmeier
// SPDX-License-Identifier: MIT

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
