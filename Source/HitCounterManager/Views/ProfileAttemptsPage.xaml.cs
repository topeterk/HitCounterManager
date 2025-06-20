// SPDX-FileCopyrightText: © 2021-2023 Peter Kirmeier
// SPDX-License-Identifier: MIT

using Avalonia.Markup.Xaml;
using HitCounterManager.ViewModels;

namespace HitCounterManager.Views
{
    public partial class ProfileAttemptsPage : PageBase<ProfileAttemptsPageViewModel>
    {
        public ProfileAttemptsPage()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
