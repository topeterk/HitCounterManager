// SPDX-FileCopyrightText: © 2023-2026 Peter Kirmeier
// SPDX-License-Identifier: MIT

using Avalonia.Markup.Xaml;
using HitCounterManager.ViewModels;

namespace HitCounterManager.Views
{
    public partial class AskSaveBeforeCloseWindow : WindowBase<AskSaveBeforeClosePageViewModel>
    {
        public AskSaveBeforeCloseWindow()
        {
            AvaloniaXamlLoader.Load(this);
            base.InitializeComponent();
        }
    }
}
