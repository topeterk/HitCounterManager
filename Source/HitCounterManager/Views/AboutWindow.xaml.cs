// SPDX-FileCopyrightText: © 2023-2026 Peter Kirmeier
// SPDX-License-Identifier: MIT

using Avalonia.Markup.Xaml;
using HitCounterManager.ViewModels;

namespace HitCounterManager.Views
{
    public partial class AboutWindow : WindowBase<ViewModelWindowBase>
    {
        public AboutWindow()
        {
            AvaloniaXamlLoader.Load(this);
            base.InitializeComponent();
        }
    }
}
