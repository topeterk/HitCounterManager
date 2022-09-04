//MIT License

//Copyright (c) 2021-2022 Peter Kirmeier

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
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using HitCounterManager.ViewModels;

namespace HitCounterManager.Views
{
    public partial class ProfileAttemptsPage : Window
    {
        ProfileAttemptsPageViewModel vm;
        public ProfileAttemptsPage() => throw new NotImplementedException(); // This ctor is only needed for Avalonia XAML interpreter
        public ProfileAttemptsPage(ProfileViewModel Origin)
        {
            InitializeComponent();
            PlatformImpl.SetTopmost(App.CurrentApp.Settings.AlwaysOnTop);

            vm = (ProfileAttemptsPageViewModel)DataContext!;
            vm.OwnerWindow = this;
            vm.Origin = Origin;
            vm.UserInput = Origin.ProfileSelected.Attempts.ToString();

#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
