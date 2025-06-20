// SPDX-FileCopyrightText: © 2022 Peter Kirmeier
// SPDX-License-Identifier: MIT

using System.Windows.Input;
using ReactiveUI;

namespace HitCounterManager.ViewModels
{
    public class AskSaveBeforeClosePageViewModel : ViewModelWindowBase
    {
        public AskSaveBeforeClosePageViewModel()
        {
            Submit = ReactiveCommand.Create((bool input) => {
                PressedYes = input;
                OwnerWindow?.Close();
            });
        }

        /// <summary>
        /// Result of user action.
        /// null = Cancel or Closed
        /// true = Yes
        /// false = No
        /// </summary>
        public bool? PressedYes;

        public ICommand Submit { get; }
    }
}
