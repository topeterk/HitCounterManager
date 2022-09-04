//MIT License

//Copyright (c) 2021-2021 Peter Kirmeier

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

using System.Windows.Input;
using ReactiveUI;
using Avalonia.Controls;
using HitCounterManager.Common;

namespace HitCounterManager.ViewModels
{
    public class ProfileAttemptsPageViewModel : ViewModelWindowBase
    {
        public ProfileAttemptsPageViewModel()
        {
            Submit = ReactiveCommand.Create(() => {
                int val;
                if (!Extensions.TryParseMinMaxNumber(_UserInput, out val, 0, int.MaxValue)) return; // Error

                _Origin?.ProfileSetAttempts.Execute(val);

                OwnerWindow?.Close(); // Success
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

        private string? _UserInput;
        public string? UserInput
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
