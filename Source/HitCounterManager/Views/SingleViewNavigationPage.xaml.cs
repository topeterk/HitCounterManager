// SPDX-FileCopyrightText: © 2023-2025 Peter Kirmeier
// SPDX-License-Identifier: MIT

using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;

namespace HitCounterManager.Views
{
    public partial class SingleViewNavigationPage : Panel
    {
        public readonly MainPage InnerPage = new();

        public Stack<IPageBasePure> NavStack = new();

        public SingleViewNavigationPage()
        {
            AvaloniaXamlLoader.Load(this);
            NavStack.Push(InnerPage);
            Children.Add(InnerPage);
            InnerPage.GetViewModelPure().OnAppearing();
        }

        public void PushPage(IPageBasePure page)
        {
            NavStack.Push(page);
            Children[0] = page.GetControlPure();
            page.GetViewModelPure().OnAppearing();
        }

        public void PopPage()
        {
            IPageBasePure page = NavStack.Pop();
            page.GetViewModelPure().OnDisappearing();
            Children[0] = NavStack.Peek().GetControlPure();
        }
    }
}
