// SPDX-FileCopyrightText: © 2021-2026 Peter Kirmeier
// SPDX-License-Identifier: MIT

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using HitCounterManager.ViewModels;

namespace HitCounterManager.Views
{
    public class WindowBase<TViewModel> : Window, IPageBase<TViewModel> where TViewModel : ViewModelWindowBase
    {
        public PageBase<TViewModel> InnerPage = null!;
        public TViewModel ViewModel => InnerPage.ViewModel;
        public TViewModel GetViewModel() => InnerPage.ViewModel;

        /// <summary>
        /// InitializeComponent must be called by the derived class.
        /// </summary>
        [MemberNotNull(nameof(InnerPage), nameof(ViewModel))]
        protected void InitializeComponent()
        {
            InnerPage = this.FindControl<PageBase<TViewModel>>("InnerPage")!;

            ViewModel!.OwnerWindow = this;

            Topmost = App.CurrentApp.Settings.AlwaysOnTop;

            Opened += (sender, e) => ViewModel.OnAppearing();
            Closed += (sender, e) => ViewModel.OnDisappearing();

            Closing += InnerPage.OnClosing;
        }

        public async Task OpenAsDialog(Window? owner)
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime /* desktop */)
            {
                if (owner is not null) await ShowDialog(owner);
            }
            else throw new PlatformNotSupportedException("Opening a window is not allowed");
        }
    }
}
