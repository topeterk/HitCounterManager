// SPDX-FileCopyrightText: © 2021 Peter Kirmeier
// SPDX-License-Identifier: MIT

using System.Windows.Input;
using ReactiveUI;

namespace HitCounterManager.ViewModels
{
    public class ProfileAttemptsPageViewModel : ViewModelWindowBase
    {
        public ProfileAttemptsPageViewModel()
        {
            Submit = ReactiveCommand.Create(() => {
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
                    CallPropertyChanged();
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
                    CallPropertyChanged();
                }
            }
        }

        public ICommand Submit { get; }
    }
}
