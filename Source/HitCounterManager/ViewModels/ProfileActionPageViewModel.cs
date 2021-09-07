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

using HitCounterManager.Common;

namespace HitCounterManager.ViewModels
{
    public class ProfileActionPageViewModel : NotifyPropertyChangedImpl
    {
        public enum ProfileAction { Create, Rename, Copy, Delete }

        private ProfileAction _Action = ProfileAction.Create;
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

        private ProfileViewModel _Origin;
        public ProfileViewModel Origin
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

        private string _UserInput;
        public string UserInput
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
                case ProfileAction.Create: UserInput = null; IsUserInputEnabled = true; break;
                case ProfileAction.Rename: UserInput = _Origin.ProfileSelected.Name; IsUserInputEnabled = true; break;
                case ProfileAction.Copy:
                    {
                        string NewName = _Origin.ProfileSelected.Name;
                        do { NewName += " COPY"; } while (_Origin.IsProfileExisting(NewName)); // extend name till it becomes unique
                        UserInput = NewName;
                        IsUserInputEnabled = true;
                        break;
                    }
                case ProfileAction.Delete: UserInput = _Origin.ProfileSelected.Name; IsUserInputEnabled = false; break;
                default: UserInput = ""; IsUserInputEnabled = false; break;
            }
        }

        public bool Submit()
        {
            switch (Action)
            {
                case ProfileAction.Create: _Origin.ProfileNew.Execute(UserInput); break;
                case ProfileAction.Rename: _Origin.ProfileRename.Execute(UserInput); break;
                case ProfileAction.Copy: _Origin.ProfileCopy.Execute(UserInput); break;
                case ProfileAction.Delete: _Origin.ProfileDelete.Execute(null); break;
                default: break;
            }

            // TODO: Return false when action fails
            return true; // Success
        }
    }
}
