//MIT License

//Copyright (c) 2023-2025 Peter Kirmeier

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
            App.CurrentApp.MainPageAppearing(this, new ());
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
