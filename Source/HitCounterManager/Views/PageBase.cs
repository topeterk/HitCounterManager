// SPDX-FileCopyrightText: © 2023-2025 Peter Kirmeier
// SPDX-License-Identifier: MIT

using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using HitCounterManager.ViewModels;

namespace HitCounterManager.Views
{
    public class PageBase<TViewModel> : UserControl, IPageBasePure, IPageBase<TViewModel> where TViewModel : ViewModelWindowBase
    {
        public TViewModel ViewModel => (TViewModel)DataContext!;
        public TViewModel GetViewModel() => ViewModel;
        public Control GetControlPure() => this;
        public ViewModelWindowBase GetViewModelPure() => ViewModel;

        public  async Task OpenAsDialog(Window? owner)
        {
            if (Application.Current?.ApplicationLifetime is ISingleViewApplicationLifetime singleViewLifetime)
            {
                SingleViewNavigationPage RootPage = (SingleViewNavigationPage)singleViewLifetime.MainView!;
                RootPage.PushPage(this);
            }
            else throw new PlatformNotSupportedException("Opening a page is not allowed");

            await Task.CompletedTask; // to pretend being an async method
        }

        public virtual void OnClosing(object? sender, CancelEventArgs e) { }

        public void CloseHandler(object sender, RoutedEventArgs args)
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
            {
                // Pass command to parent window
                ViewModel.OwnerWindow?.Close();
            }
            else if (Application.Current?.ApplicationLifetime is ISingleViewApplicationLifetime singleViewLifetime)
            {
                // Return to previous page
                SingleViewNavigationPage RootPage = (SingleViewNavigationPage)singleViewLifetime.MainView!;
                RootPage.PopPage();
            }
        }

        /// <summary>
        /// Fired before a window is closed.
        /// </summary>
        public event EventHandler<WindowClosingEventArgs>? Closing
        {
            add { if (ViewModel.OwnerWindow is not null) ViewModel.OwnerWindow.Closing += value; }
            remove { if (ViewModel.OwnerWindow is not null) ViewModel.OwnerWindow.Closing -= value; }
        }
    }

    public interface IPageBasePure
    {
        public Control GetControlPure();
        public ViewModelWindowBase GetViewModelPure();
    }

    public interface IPageBase<TViewModel> where TViewModel : ViewModelWindowBase
    {
        public Task OpenAsDialog(Window? owner);
        public TViewModel GetViewModel();
    }

    public static class PageBaseExtensions
    {
        public static IPageBase<TViewModel> CreatePageOrWindow<TViewModel, TPage, TWindow>(this ViewModelWindowBase parent)
            where TViewModel : ViewModelWindowBase
            where TPage : PageBase<TViewModel>, new()
            where TWindow : WindowBase<TViewModel>, new()
        {
            if (parent.OwnerWindow is null)
            {
                // SingleViewApp
                return new TPage();
            }
            else
            {
                // Desktop
                return new TWindow();
            }
        }
    }
}
