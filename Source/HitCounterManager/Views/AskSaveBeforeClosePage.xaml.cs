// SPDX-FileCopyrightText: © 2021-2022 Peter Kirmeier
// SPDX-License-Identifier: MIT

using Avalonia.Markup.Xaml;
using HitCounterManager.ViewModels;

namespace HitCounterManager.Views
{
    public partial class AskSaveBeforeClosePage : PageBase<AskSaveBeforeClosePageViewModel>
    {
        public AskSaveBeforeClosePage()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

