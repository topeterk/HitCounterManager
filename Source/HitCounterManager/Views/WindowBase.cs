//MIT License

//Copyright (c) 2021-2024 Peter Kirmeier

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
        private PageBase<TViewModel> InnerPage = null!;
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

            Opened += (object? sender, EventArgs e) => ViewModel.OnAppearing();
            Closed += (object? sender, EventArgs e) => ViewModel.OnDisappearing();

            Closing += InnerPage.OnClosing;

#if DEBUG
            this.AttachDevTools();
#endif
        }

        public async Task OpenAsDialog(Window? owner)
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                if (owner is not null) await ShowDialog(owner);
            }
            else throw new PlatformNotSupportedException("Opening a window is not allowed");
        }
    }
}
