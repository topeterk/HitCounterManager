// SPDX-FileCopyrightText: © 2023 Peter Kirmeier
// SPDX-License-Identifier: MIT

using Avalonia.Markup.Xaml;
using HitCounterManager.ViewModels;

namespace HitCounterManager.Views
{
    public partial class ProfileActionWindow : WindowBase<ProfileActionPageViewModel>
    {
        public ProfileActionWindow()
        {
            AvaloniaXamlLoader.Load(this);
            InitializeComponent();
        }
    }
}
