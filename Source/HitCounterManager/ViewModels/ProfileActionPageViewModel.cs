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

using Avalonia.Controls;
using HitCounterManager.Common;
using ReactiveUI;
using System.Windows.Input;

namespace HitCounterManager.ViewModels
{
    // TODO Avalonia: [sctypefix] Nested types not working with + sign within xaml, so it must be directly in namespace (ProfileAction)
    public enum ProfileAction { Invalid, Create, Rename, Copy, Delete }

    public class ProfileActionPageViewModel : NotifyPropertyChangedImpl
    {
        public Window? OwnerWindow { get; set; }

        public ProfileActionPageViewModel()
        {
            Submit = ReactiveCommand.Create(() => {

                try
                {
                    if (null == _Origin) throw new ProfileViewModel.ProfileActionException("Internal error");
                    switch (Action)
                    {
                        case ProfileAction.Create:
                            if (null == UserInput) throw new ProfileViewModel.ProfileActionException("Internal error");
                            _Origin.ProfileNew(UserInput);
                            break;
                        case ProfileAction.Rename:
                            if (null == UserInput) throw new ProfileViewModel.ProfileActionException("Internal error"); 
                            _Origin.ProfileRename(UserInput);
                            break;
                        case ProfileAction.Copy:
                            if (null == UserInput) throw new ProfileViewModel.ProfileActionException("Internal error"); 
                            _Origin.ProfileCopy(UserInput);
                            break;
                        case ProfileAction.Delete:
                            _Origin.ProfileDelete();
                            break;
                        default: throw new ProfileViewModel.ProfileActionException("Unknown action");
                    }
                }
                catch (ProfileViewModel.ProfileActionException ex)
                {
                    App.CurrentApp.DisplayAlert("Profile action failed!", ex.Message);
                    return; // Error
                }

                OwnerWindow?.Close(); // Success
            });
        }

        private ProfileAction _Action = ProfileAction.Invalid;
        public ProfileAction Action
        {
            get => _Action;
            set
            {
                if (_Action != value)
                {
                    _Action = value;
                    CallPropertyChanged(this, nameof(Action));
                }
            }
        }

        private ProfileViewModel? _Origin;
        public ProfileViewModel? Origin
        {
            get => _Origin;
            set
            {
                if (_Origin != value)
                {
                    _Origin = value;
                    CallPropertyChanged(this, nameof(Origin));
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
                    CallPropertyChanged(this, nameof(UserInput));
                }
            }
        }

        private bool _IsUserInputEnabled;
        public bool IsUserInputEnabled
        {
            get => _IsUserInputEnabled;
            private set => SetAndNotifyWhenChanged(this, ref _IsUserInputEnabled, value, nameof(IsUserInputEnabled));
        }

        public void OnAppearing()
        {
            switch (Action)
            {
                case ProfileAction.Create:
                    UserInput = null;
                    IsUserInputEnabled = true;
                    break;
                case ProfileAction.Rename:
                    if (null == _Origin) throw new ProfileViewModel.ProfileActionException("Internal error"); 
                    UserInput = _Origin.ProfileSelected.Name;
                    IsUserInputEnabled = true;
                    break;
                case ProfileAction.Copy:
                    {
                        if (null == _Origin) throw new ProfileViewModel.ProfileActionException("Internal error");
                        string NewName = _Origin.ProfileSelected.Name;
                        do { NewName += " COPY"; } while (_Origin.IsProfileExisting(NewName)); // extend name till it becomes unique
                        UserInput = NewName;
                        IsUserInputEnabled = true;
                        break;
                    }
                case ProfileAction.Delete:
                    if (null == _Origin) throw new ProfileViewModel.ProfileActionException("Internal error"); 
                    UserInput = _Origin.ProfileSelected.Name;
                    IsUserInputEnabled = false;
                    break;
                default:
                    UserInput = "";
                    IsUserInputEnabled = false;
                    break;
            }
        }

        public ICommand Submit { get; }
    }
}
