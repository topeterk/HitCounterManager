// SPDX-FileCopyrightText: © 2021-2024 Peter Kirmeier
// SPDX-License-Identifier: MIT

using System.Windows.Input;
using ReactiveUI;

namespace HitCounterManager.ViewModels
{
    public enum ProfileAction { Invalid, Create, Rename, Copy, Delete }

    public class ProfileActionPageViewModel : ViewModelWindowBase
    {
        public ProfileActionPageViewModel()
        {
            Submit = ReactiveCommand.Create(() => {

                try
                {
                    if (null == _Origin) throw new ProfileViewViewModel.ProfileActionException("Internal error");
                    switch (Action)
                    {
                        case ProfileAction.Create:
                            if (null == UserInput) throw new ProfileViewViewModel.ProfileActionException("Internal error");
                            _Origin.ProfileNew(UserInput);
                            break;
                        case ProfileAction.Rename:
                            if (null == UserInput) throw new ProfileViewViewModel.ProfileActionException("Internal error"); 
                            _Origin.ProfileRename(UserInput);
                            break;
                        case ProfileAction.Copy:
                            if (null == UserInput) throw new ProfileViewViewModel.ProfileActionException("Internal error"); 
                            _Origin.ProfileCopy(UserInput);
                            break;
                        case ProfileAction.Delete:
                            _Origin.ProfileDelete();
                            break;
                        default: throw new ProfileViewViewModel.ProfileActionException("Unknown action");
                    }
                }
                catch (ProfileViewViewModel.ProfileActionException ex)
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
                    CallPropertyChanged();
                }
            }
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

        private bool _IsUserInputEnabled;
        public bool IsUserInputEnabled
        {
            get => _IsUserInputEnabled;
            private set => SetAndNotifyWhenChanged(ref _IsUserInputEnabled, value);
        }

        public sealed override void OnAppearing()
        {
            switch (Action)
            {
                case ProfileAction.Create:
                    UserInput = null;
                    IsUserInputEnabled = true;
                    break;
                case ProfileAction.Rename:
                    if (null == _Origin) throw new ProfileViewViewModel.ProfileActionException("Internal error"); 
                    UserInput = _Origin.ProfileSelected.Name;
                    IsUserInputEnabled = true;
                    break;
                case ProfileAction.Copy:
                    {
                        if (null == _Origin) throw new ProfileViewViewModel.ProfileActionException("Internal error");
                        string NewName = _Origin.ProfileSelected.Name;
                        do { NewName += " COPY"; } while (_Origin.IsProfileExisting(NewName)); // extend name till it becomes unique
                        UserInput = NewName;
                        IsUserInputEnabled = true;
                        break;
                    }
                case ProfileAction.Delete:
                    if (null == _Origin) throw new ProfileViewViewModel.ProfileActionException("Internal error"); 
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
