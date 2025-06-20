// SPDX-FileCopyrightText: © 2021-2025 Peter Kirmeier
// SPDX-License-Identifier: MIT

using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using HitCounterManager.ViewModels;

namespace HitCounterManager.Controls
{
    public partial class ProfileView : UserControl
    {
        public ProfileView()
        {
            AvaloniaXamlLoader.Load(this);

            ((ProfileViewViewModel)DataContext!).ProfileSelector = this.FindControl<ComboBox>("profileSelector");
        }
    }
}
