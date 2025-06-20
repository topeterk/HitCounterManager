// SPDX-FileCopyrightText: © 2022 Peter Kirmeier
// SPDX-License-Identifier: MIT

using Avalonia.Controls;
using HitCounterManager.Common;

namespace HitCounterManager.ViewModels
{
    public class ViewModelBase : NotifyPropertyChangedImpl
    {
    }

    public class ViewModelWindowBase : ViewModelBase
    {
        public Window? OwnerWindow { get; set; }

        public virtual void OnAppearing() { }
        public virtual void OnDisappearing() { }
    }
}
