// SPDX-FileCopyrightText: © 2021-2026 Peter Kirmeier
// SPDX-License-Identifier: MIT

using System.Windows.Input;
using HitCounterManager.Common;

namespace HitCounterManager.ViewModels
{
    public class ProfileAttemptsPageViewModel : ViewModelWindowBase
    {
        public ProfileAttemptsPageViewModel()
        {
            Submit = RelayCommand.Create(() => {
                _Origin?.ProfileSetAttempts.Execute(_UserInput);
                OwnerWindow?.Close();
            });
        }

        private ProfileViewViewModel? _Origin;
        public ProfileViewViewModel? Origin
        {
            get => _Origin;
            set
            {
                if (_Origin != value)
                {
                    _Origin = value;
                    RaisePropertyChanged();
                }
            }
        }

        private int _UserInput = 0;
        public int UserInput
        {
            get => _UserInput;
            set
            {
                if (_UserInput != value)
                {
                    _UserInput = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ICommand Submit { get; }
    }
}
