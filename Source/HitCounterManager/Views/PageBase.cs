//MIT License

//Copyright (c) 2023-2023 Peter Kirmeier

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
            if (Application.Current?.ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                SingleViewNavigationPage RootPage = (SingleViewNavigationPage)singleViewPlatform.MainView!;
                RootPage.PushPage(this);
            }
            else throw new PlatformNotSupportedException("Opening a page is not allowed");

            await Task.CompletedTask; // to pretend being an async method
        }

        public virtual void OnClosing(object? sender, CancelEventArgs e) { }

        public void CloseHandler(object sender, RoutedEventArgs args)
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Pass command to parent window
                if (ViewModel.OwnerWindow is not null) ViewModel.OwnerWindow.Close();
            }
            else if (Application.Current?.ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                // Return to previous page
                SingleViewNavigationPage RootPage = (SingleViewNavigationPage)singleViewPlatform.MainView!;
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
