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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using HitCounterManager.Common;
using HitCounterManager.Models;

namespace HitCounterManager.ViewModels
{
    public class SettingsViewModel : NotifyPropertyChangedImpl, IDisposable
    {
        public SettingsRoot Settings => App.CurrentApp.Settings;
        private Shortcuts sc = App.CurrentApp.sc;
        private OutModule om = App.CurrentApp.om;
        private bool AppearedOnce = false;
        private Shortcuts.SC_Type CapturingId = Shortcuts.SC_Type.SC_Type_MAX;

        public SettingsViewModel()
        {
            _StyleFontName = Settings.StyleFontName;
            _StyleFontUrl = Settings.StyleFontUrl;
            _StyleCssUrl = Settings.StyleCssUrl;

            ToggleShowInfo = new Command<string>((string name) => { ShowInfo[name].Value = !ShowInfo[name].Value; });

            Capture = new Command<Shortcuts.SC_Type>((type) =>
            {
                Shortcuts.SC_Type CapturingIdPrev = CapturingId;

                if ((Shortcuts.SC_Type.SC_Type_MAX == type) || (CapturingId == type))
                {
                    CapturingId = Shortcuts.SC_Type.SC_Type_MAX; // Stop capturing and implicitly stop timer
                    RecordActionChanged(CapturingIdPrev);
                }
                else if (Shortcuts.SC_Type.SC_Type_MAX != CapturingId)
                {
                    CapturingId = type; // Switch to another key
                    RecordActionChanged(CapturingIdPrev);
                    RecordActionChanged(CapturingId);
                }
                else
                {
                    // Start capturing
                    CapturingId = type;
                    App.StartApplicationTimer(TimerIDs.ShortcutsCapture, 20, CaptureKeysTick);
                    RecordActionChanged(CapturingId);
                }
            });

            ApplyCssAndFont = new Command(() =>
            {
                Settings.StyleFontName = _StyleFontName;
                Settings.StyleFontUrl = _StyleFontUrl;
                Settings.StyleCssUrl = _StyleCssUrl;

                // Implicitly enable custom settings
                SetAndNotifyWhenChanged(this, ref Settings.StyleUseCustom, true, nameof(StyleUseCustom));

                CallPropertyChanged(this, nameof(ApplyCssAndFont));
            });

            PropertyChanged += SettingDataChangedHandler;
        }

        ~SettingsViewModel() => Dispose(false);

        private bool _Disposed = false;
        public void Dispose() { Dispose(true); GC.SuppressFinalize(this); } // Allow to free memory early (https://coding.abel.nu/2011/12/implementing-idisposable/)
        protected virtual void Dispose(bool disposing)
        {
            if (!_Disposed) // only once
            {
                /*if (disposing) // called manually (true) or from destructor (false)
                {
                }*/

                Capture.Execute(Shortcuts.SC_Type.SC_Type_MAX); // Stop any running capture
                App.CurrentApp.SettingsDialogOpen = false; // Re-Enable hotkeys (for main application)

                _Disposed = true;
            }
        }

        // Move this into a event handler of Settings itself, so we can run Update depending on the whole Settings from anywhere
        private void SettingDataChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            // Do not propagate local variables
            if (e.PropertyName.Equals(nameof(StyleFontName)) ||
                e.PropertyName.Equals(nameof(StyleFontUrl)) ||
                e.PropertyName.Equals(nameof(StyleCssUrl))) return;

            App.CurrentApp.profileViewModel.OutputDataChangedHandler(sender, e);
        }

        public class ShowInfoBool : NotifyPropertyChangedImpl
        {
            private bool _Value = false;
            public bool Value
            {
                get => _Value;
                set => SetAndNotifyWhenChanged(this, ref _Value, value, nameof(Value));
            }
        }

        private Dictionary<string, ShowInfoBool> _ShowInfo = new Dictionary<string, ShowInfoBool>(){
            {"RadioHotKeyMethod_Sync", new ShowInfoBool()},
            {"RadioHotKeyMethod_Async", new ShowInfoBool()},
            {"RadioHotKeyMethod_LLKb", new ShowInfoBool()},
            {"ShowNumbers", new ShowInfoBool()},
            {"StyleUseHighContrast", new ShowInfoBool()},
            {"StyleUseHighContrastNames", new ShowInfoBool()},
            {"StyleProgressBarColored", new ShowInfoBool()},
            {"StyleSubscriptPB", new ShowInfoBool()},
            {"StyleUseRoman", new ShowInfoBool()},
            {"StyleHightlightCurrentSplit", new ShowInfoBool()},
        };
        public Dictionary<string, ShowInfoBool> ShowInfo { get => _ShowInfo; }

        public ICommand ToggleShowInfo { get; }

        public ICommand Capture { get; }

        /// <summary>
        /// Timer callback function
        /// Checks currently pressed keys and fires KeyDown events
        /// </summary>
        /// <returns>true = restart time, false = stop timer</returns>
        private bool CaptureKeysTick()
        {
            // Early cancellation point
            if (Shortcuts.SC_Type.SC_Type_MAX == CapturingId) return false;

            List<int> PressedKeys = App.CurrentApp.OsLayer.GetKeysPressedAsync();
            if (null == PressedKeys) return (Shortcuts.SC_Type.SC_Type_MAX != CapturingId);

            KeyEventArgs key = new KeyEventArgs(Keys.None);
            foreach (int KeyCode in PressedKeys)
            {
                if (((Keys)KeyCode < Keys.Back) || (Keys.OemClear <= (Keys)KeyCode)) continue; // Ignore mouse keys

                switch ((Keys)KeyCode)
                {
                    // When virtual modifiers are not set, we look for actual keys as well
                    case Keys.LShiftKey:
                    case Keys.RShiftKey: key.KeyData |= Keys.Shift; break;
                    case Keys.LControlKey:
                    case Keys.RControlKey: key.KeyData |= Keys.Control; break;
                    case Keys.LMenu:
                    case Keys.RMenu: key.KeyData |= Keys.Alt; break;

                    // Assign virtual modifiers
                    case Keys.ShiftKey: key.KeyData |= Keys.Shift; break;
                    case Keys.ControlKey: key.KeyData |= Keys.Control; break;
                    case Keys.Menu: key.KeyData |= Keys.Alt; break;

                    // Assign key code
                    default:
                        if (key.KeyCode != Keys.None) return (Shortcuts.SC_Type.SC_Type_MAX != CapturingId); // Only a single key can be captured
                        key.KeyData |= (Keys)KeyCode;
                        break;
                }
            }
            if (key.KeyCode != Keys.None) RegisterHotKey(key); // Was a key combination detected? -> Register key

            return Shortcuts.SC_Type.SC_Type_MAX != CapturingId;
        }

        /// <summary>
        /// Registers a hot key and stores it
        /// </summary>
        /// <param name="e">Key combination</param>
        private void RegisterHotKey(KeyEventArgs e)
        {
            ShortcutsKey key = new ShortcutsKey();
            Shortcuts.SC_Type Id = CapturingId;

            if (e.KeyCode == Keys.None) return;
            if (e.KeyCode == Keys.ShiftKey) return;
            if (e.KeyCode == Keys.ControlKey) return;
            if (e.KeyCode == Keys.Alt) return;
            if (e.KeyCode == Keys.Menu) return; // = Alt

            // register hotkey
            key.key = e;
            sc.Key_Set(Id, key);

            switch (Id)
            {
                case Shortcuts.SC_Type.SC_Type_Hit:
                    CallPropertyChanged(this, nameof(ShortcutHitDescription));
                    ShortcutHitEnable = true;
                    break;
                case Shortcuts.SC_Type.SC_Type_HitUndo:
                    CallPropertyChanged(this, nameof(ShortcutHitUndoDescription));
                    ShortcutHitUndoEnable = true;
                    break;
                case Shortcuts.SC_Type.SC_Type_WayHit:
                    CallPropertyChanged(this, nameof(ShortcutWayHitDescription));
                    ShortcutWayHitEnable = true;
                    break;
                case Shortcuts.SC_Type.SC_Type_WayHitUndo:
                    CallPropertyChanged(this, nameof(ShortcutWayHitUndoDescription));
                    ShortcutWayHitUndoEnable = true;
                    break;
                case Shortcuts.SC_Type.SC_Type_Split:
                    CallPropertyChanged(this, nameof(ShortcutSplitDescription));
                    ShortcutSplitEnable = true;
                    break;
                case Shortcuts.SC_Type.SC_Type_SplitPrev:
                    CallPropertyChanged(this, nameof(ShortcutSplitPrevDescription));
                    ShortcutSplitPrevEnable = true;
                    break;
                case Shortcuts.SC_Type.SC_Type_PB:
                    CallPropertyChanged(this, nameof(ShortcutPBDescription));
                    ShortcutPBEnable = true;
                    break;
                case Shortcuts.SC_Type.SC_Type_Reset:
                    CallPropertyChanged(this, nameof(ShortcutResetDescription));
                    ShortcutResetEnable = true;
                    break;
                case Shortcuts.SC_Type.SC_Type_TimerStart:
                    CallPropertyChanged(this, nameof(ShortcutTimerStartDescription));
                    ShortcutTimerStartEnable = true;
                    break;
                case Shortcuts.SC_Type.SC_Type_TimerStop:
                    CallPropertyChanged(this, nameof(ShortcutTimerStopDescription));
                    ShortcutTimerStopEnable = true;
                    break;
                default: break;
            }
        }

        public void OnAppearing()
        {
            if (!AppearedOnce)
            {
                AppearedOnce = true;
                // Shortcuts require a window handle to register on
                // but when this ViewModel gets created, the handle is not know yet
                // therefore the settings must be loaded again later (=here)
                // in order to present the correct data
                // TODO: Get rid of this by displaying stored keys instead of loaded keys?
                CallPropertyChanged(this, nameof(ShortcutHitRecordAction));
                CallPropertyChanged(this, nameof(ShortcutHitUndoRecordAction));
                CallPropertyChanged(this, nameof(ShortcutWayHitRecordAction));
                CallPropertyChanged(this, nameof(ShortcutWayHitUndoRecordAction));
                CallPropertyChanged(this, nameof(ShortcutSplitRecordAction));
                CallPropertyChanged(this, nameof(ShortcutSplitPrevRecordAction));
                CallPropertyChanged(this, nameof(ShortcutPBRecordAction));
                CallPropertyChanged(this, nameof(ShortcutResetRecordAction));
                CallPropertyChanged(this, nameof(ShortcutTimerStartRecordAction));
                CallPropertyChanged(this, nameof(ShortcutTimerStopRecordAction));

                CallPropertyChanged(this, nameof(ShortcutHitDescription));
                CallPropertyChanged(this, nameof(ShortcutHitUndoDescription));
                CallPropertyChanged(this, nameof(ShortcutWayHitDescription));
                CallPropertyChanged(this, nameof(ShortcutWayHitUndoDescription));
                CallPropertyChanged(this, nameof(ShortcutSplitDescription));
                CallPropertyChanged(this, nameof(ShortcutSplitPrevDescription));
                CallPropertyChanged(this, nameof(ShortcutPBDescription));
                CallPropertyChanged(this, nameof(ShortcutResetDescription));
                CallPropertyChanged(this, nameof(ShortcutTimerStartDescription));
                CallPropertyChanged(this, nameof(ShortcutTimerStopDescription));

                CallPropertyChanged(this, nameof(RadioHotKeyMethod_Sync));
                CallPropertyChanged(this, nameof(RadioHotKeyMethod_Async));
                CallPropertyChanged(this, nameof(RadioHotKeyMethod_LLKb));
            }

            App.CurrentApp.SettingsDialogOpen = true; // To disable hotkeys (for main application)
        }

#region Global Shortcuts
        public bool ShortcutHitEnable
        {
            get => Settings.ShortcutHitEnable;
            set
            {
                if (SetAndNotifyWhenChanged(this, ref Settings.ShortcutHitEnable, value, nameof(ShortcutHitEnable)))
                    sc.Key_SetState(Shortcuts.SC_Type.SC_Type_Hit, Settings.ShortcutHitEnable);
            }
        }
        public bool ShortcutHitUndoEnable
        {
            get => Settings.ShortcutHitUndoEnable;
            set
            {
                if (SetAndNotifyWhenChanged(this, ref Settings.ShortcutHitUndoEnable, value, nameof(ShortcutHitUndoEnable)))
                    sc.Key_SetState(Shortcuts.SC_Type.SC_Type_HitUndo, Settings.ShortcutHitUndoEnable);
            }
        }
        public bool ShortcutWayHitEnable
        {
            get => Settings.ShortcutWayHitEnable;
            set
            {
                if (SetAndNotifyWhenChanged(this, ref Settings.ShortcutWayHitEnable, value, nameof(ShortcutWayHitEnable)))
                    sc.Key_SetState(Shortcuts.SC_Type.SC_Type_WayHit, Settings.ShortcutWayHitEnable);
            }
        }
        public bool ShortcutWayHitUndoEnable
        {
            get => Settings.ShortcutWayHitUndoEnable;
            set
            {
                if (SetAndNotifyWhenChanged(this, ref Settings.ShortcutWayHitUndoEnable, value, nameof(ShortcutWayHitUndoEnable)))
                    sc.Key_SetState(Shortcuts.SC_Type.SC_Type_WayHitUndo, Settings.ShortcutWayHitUndoEnable);
            }
        }
        public bool ShortcutSplitEnable
        {
            get => Settings.ShortcutSplitEnable;
            set
            {
                if (SetAndNotifyWhenChanged(this, ref Settings.ShortcutSplitEnable, value, nameof(ShortcutSplitEnable)))
                    sc.Key_SetState(Shortcuts.SC_Type.SC_Type_Split, Settings.ShortcutSplitEnable);
            }
        }
        public bool ShortcutSplitPrevEnable
        {
            get => Settings.ShortcutSplitPrevEnable;
            set
            {
                if (SetAndNotifyWhenChanged(this, ref Settings.ShortcutSplitPrevEnable, value, nameof(ShortcutSplitPrevEnable)))
                    sc.Key_SetState(Shortcuts.SC_Type.SC_Type_SplitPrev, Settings.ShortcutSplitPrevEnable);
            }
        }
        public bool ShortcutPBEnable
        {
            get => Settings.ShortcutPBEnable;
            set
            {
                if (SetAndNotifyWhenChanged(this, ref Settings.ShortcutPBEnable, value, nameof(ShortcutPBEnable)))
                    sc.Key_SetState(Shortcuts.SC_Type.SC_Type_PB, Settings.ShortcutPBEnable);
            }
        }
        public bool ShortcutResetEnable
        {
            get => Settings.ShortcutResetEnable;
            set
            {
                if (SetAndNotifyWhenChanged(this, ref Settings.ShortcutResetEnable, value, nameof(ShortcutResetEnable)))
                    sc.Key_SetState(Shortcuts.SC_Type.SC_Type_Reset, Settings.ShortcutResetEnable);
            }
        }
        public bool ShortcutTimerStartEnable
        {
            get => Settings.ShortcutTimerStartEnable;
            set
            {
                if (SetAndNotifyWhenChanged(this, ref Settings.ShortcutTimerStartEnable, value, nameof(ShortcutTimerStartEnable)))
                    sc.Key_SetState(Shortcuts.SC_Type.SC_Type_TimerStart, Settings.ShortcutTimerStartEnable);
            }
        }
        public bool ShortcutTimerStopEnable
        {
            get => Settings.ShortcutTimerStopEnable;
            set
            {
                if (SetAndNotifyWhenChanged(this, ref Settings.ShortcutTimerStopEnable, value, nameof(ShortcutTimerStopEnable)))
                    sc.Key_SetState(Shortcuts.SC_Type.SC_Type_TimerStop, Settings.ShortcutTimerStopEnable);
            }
        }

        private void RecordActionChanged(Shortcuts.SC_Type type)
        {
            switch (type)
            {
                case Shortcuts.SC_Type.SC_Type_Hit: CallPropertyChanged(this, nameof(ShortcutHitRecordAction)); break;
                case Shortcuts.SC_Type.SC_Type_HitUndo: CallPropertyChanged(this, nameof(ShortcutHitUndoRecordAction)); break;
                case Shortcuts.SC_Type.SC_Type_WayHit: CallPropertyChanged(this, nameof(ShortcutWayHitRecordAction)); break;
                case Shortcuts.SC_Type.SC_Type_WayHitUndo: CallPropertyChanged(this, nameof(ShortcutWayHitUndoRecordAction)); break;
                case Shortcuts.SC_Type.SC_Type_Split: CallPropertyChanged(this, nameof(ShortcutSplitRecordAction)); break;
                case Shortcuts.SC_Type.SC_Type_SplitPrev: CallPropertyChanged(this, nameof(ShortcutSplitPrevRecordAction)); break;
                case Shortcuts.SC_Type.SC_Type_PB: CallPropertyChanged(this, nameof(ShortcutPBRecordAction)); break;
                case Shortcuts.SC_Type.SC_Type_Reset: CallPropertyChanged(this, nameof(ShortcutResetRecordAction)); break;
                case Shortcuts.SC_Type.SC_Type_TimerStart: CallPropertyChanged(this, nameof(ShortcutTimerStartRecordAction)); break;
                case Shortcuts.SC_Type.SC_Type_TimerStop: CallPropertyChanged(this, nameof(ShortcutTimerStopRecordAction)); break;
                default: break;
            }
        }
        public string ShortcutHitRecordAction => Shortcuts.SC_Type.SC_Type_Hit == CapturingId ? "Stop" : "Rec";
        public string ShortcutHitUndoRecordAction => Shortcuts.SC_Type.SC_Type_HitUndo == CapturingId ? "Stop" : "Rec";
        public string ShortcutWayHitRecordAction => Shortcuts.SC_Type.SC_Type_WayHit == CapturingId ? "Stop" : "Rec";
        public string ShortcutWayHitUndoRecordAction => Shortcuts.SC_Type.SC_Type_WayHitUndo == CapturingId ? "Stop" : "Rec";
        public string ShortcutSplitRecordAction => Shortcuts.SC_Type.SC_Type_Split == CapturingId ? "Stop" : "Rec";
        public string ShortcutSplitPrevRecordAction => Shortcuts.SC_Type.SC_Type_SplitPrev == CapturingId ? "Stop" : "Rec";
        public string ShortcutPBRecordAction => Shortcuts.SC_Type.SC_Type_PB == CapturingId ? "Stop" : "Rec";
        public string ShortcutResetRecordAction => Shortcuts.SC_Type.SC_Type_Reset == CapturingId ? "Stop" : "Rec";
        public string ShortcutTimerStartRecordAction => Shortcuts.SC_Type.SC_Type_TimerStart == CapturingId ? "Stop" : "Rec";
        public string ShortcutTimerStopRecordAction => Shortcuts.SC_Type.SC_Type_TimerStop == CapturingId ? "Stop" : "Rec";

        public string ShortcutHitDescription => sc.Key_Get(Shortcuts.SC_Type.SC_Type_Hit).GetDescriptionString();
        public string ShortcutHitUndoDescription => sc.Key_Get(Shortcuts.SC_Type.SC_Type_HitUndo).GetDescriptionString();
        public string ShortcutWayHitDescription => sc.Key_Get(Shortcuts.SC_Type.SC_Type_WayHit).GetDescriptionString();
        public string ShortcutWayHitUndoDescription => sc.Key_Get(Shortcuts.SC_Type.SC_Type_WayHitUndo).GetDescriptionString();
        public string ShortcutSplitDescription => sc.Key_Get(Shortcuts.SC_Type.SC_Type_Split).GetDescriptionString();
        public string ShortcutSplitPrevDescription => sc.Key_Get(Shortcuts.SC_Type.SC_Type_SplitPrev).GetDescriptionString();
        public string ShortcutPBDescription => sc.Key_Get(Shortcuts.SC_Type.SC_Type_PB).GetDescriptionString();
        public string ShortcutResetDescription => sc.Key_Get(Shortcuts.SC_Type.SC_Type_Reset).GetDescriptionString();
        public string ShortcutTimerStartDescription => sc.Key_Get(Shortcuts.SC_Type.SC_Type_TimerStart).GetDescriptionString();
        public string ShortcutTimerStopDescription => sc.Key_Get(Shortcuts.SC_Type.SC_Type_TimerStop).GetDescriptionString();

        private void SetNextShortcutMethod(Shortcuts.SC_HotKeyMethod next)
        {
            Shortcuts.SC_HotKeyMethod prev = sc.NextStart_Method;
            if (prev == next) return;

            sc.NextStart_Method = next;
            switch (prev)
            {
                case Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Sync: CallPropertyChanged(this, nameof(RadioHotKeyMethod_Sync)); break;
                case Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Async: CallPropertyChanged(this, nameof(RadioHotKeyMethod_Async)); break;
                case Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_LLKb: CallPropertyChanged(this, nameof(RadioHotKeyMethod_LLKb)); break;
                default: break;
            }
            switch (next)
            {
                case Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Sync: CallPropertyChanged(this, nameof(RadioHotKeyMethod_Sync)); break;
                case Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Async: CallPropertyChanged(this, nameof(RadioHotKeyMethod_Async)); break;
                case Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_LLKb: CallPropertyChanged(this, nameof(RadioHotKeyMethod_LLKb)); break;
                default: break;
            }
            App.CurrentApp.DisplayAlert("Restart required", "Changes only take effect after restarting the application.", "OK");
        }
        public bool RadioHotKeyMethod_Sync
        {
            get => sc.NextStart_Method == Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Sync;
            set { if (value) SetNextShortcutMethod(Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Sync); }
        }
        public bool RadioHotKeyMethod_Async
        {
            get => sc.NextStart_Method == Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Async;
            set { if (value) SetNextShortcutMethod(Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Async); }
        }
        public bool RadioHotKeyMethod_LLKb
        {
            get => sc.NextStart_Method == Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_LLKb;
            set { if (value) SetNextShortcutMethod(Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_LLKb); }
        }
#endregion

#region Style
        public bool StyleUseHighContrast
        {
            get => Settings.StyleUseHighContrast;
            set => SetAndNotifyWhenChanged(this, ref Settings.StyleUseHighContrast, value, nameof(StyleUseHighContrast));
        }
        public bool StyleUseHighContrastNames
        {
            get => Settings.StyleUseHighContrastNames;
            set => SetAndNotifyWhenChanged(this, ref Settings.StyleUseHighContrastNames, value, nameof(StyleUseHighContrastNames));
        }
        public bool StyleProgressBarColored
        {
            get => Settings.StyleProgressBarColored;
            set => SetAndNotifyWhenChanged(this, ref Settings.StyleProgressBarColored, value, nameof(StyleProgressBarColored));
        }
        public bool StyleSubscriptPB
        {
            get => Settings.StyleSubscriptPB;
            set => SetAndNotifyWhenChanged(this, ref Settings.StyleSubscriptPB, value, nameof(StyleSubscriptPB));
        }
        public bool StyleUseRoman
        {
            get => Settings.StyleUseRoman;
            set => SetAndNotifyWhenChanged(this, ref Settings.StyleUseRoman, value, nameof(StyleUseRoman));
        }
        public bool StyleHightlightCurrentSplit
        {
            get => Settings.StyleHightlightCurrentSplit;
            set => SetAndNotifyWhenChanged(this, ref Settings.StyleHightlightCurrentSplit, value, nameof(StyleHightlightCurrentSplit));
        }

        public int StyleDesiredHeight
        {
            get => Settings.StyleDesiredHeight;
            set => SetAndNotifyWhenNaturalNumberChanged(this, ref Settings.StyleDesiredHeight, value, nameof(StyleDesiredHeight));
        }
        public int StyleDesiredWidth
        {
            get => Settings.StyleDesiredWidth;
            set => SetAndNotifyWhenNaturalNumberChanged(this, ref Settings.StyleDesiredWidth, value, nameof(StyleDesiredWidth));
        }

        public bool StyleUseCustom
        {
            get => Settings.StyleUseCustom;
            set => SetAndNotifyWhenChanged(this, ref Settings.StyleUseCustom, value, nameof(StyleUseCustom));
        }
        private string _StyleFontName;
        public string StyleFontName
        {
            get => _StyleFontName;
            set => SetAndNotifyWhenChanged(this, ref _StyleFontName, value, nameof(StyleFontName));
        }
        private string _StyleFontUrl;
        public string StyleFontUrl
        {
            get => _StyleFontUrl;
            set => SetAndNotifyWhenChanged(this, ref _StyleFontUrl, value, nameof(StyleFontUrl));
        }
        private string _StyleCssUrl;
        public string StyleCssUrl
        {
            get => _StyleCssUrl;
            set => SetAndNotifyWhenChanged(this, ref _StyleCssUrl, value, nameof(StyleCssUrl));
        }
        public ICommand ApplyCssAndFont { get; }

#pragma warning disable CS0618 // Ignore deprecated (but without replacement, sure it is Launcher.OpenAsync in Xamarin.Essentials but requires additianal references
        public ICommand WebOpenGoogleFontsUrl { get; } = new Command(() => Device.OpenUri(new Uri("https://fonts.google.com")));
#pragma warning restore CS0618
#endregion

#region Behavior
        private void SetPurpose(OutModule.OM_Purpose next)
        {
            OutModule.OM_Purpose prev = om.Purpose;
            if (prev == next) return;

            om.Purpose = next;
            switch (prev)
            {
                case OutModule.OM_Purpose.OM_Purpose_SplitCounter: CallPropertyChanged(this, nameof(RadioPurpose_SplitCounter)); break;
                case OutModule.OM_Purpose.OM_Purpose_Checklist: CallPropertyChanged(this, nameof(RadioPurpose_Checklist)); break;
                case OutModule.OM_Purpose.OM_Purpose_NoDeath: CallPropertyChanged(this, nameof(RadioPurpose_NoDeath)); break;
                case OutModule.OM_Purpose.OM_Purpose_DeathCounter: CallPropertyChanged(this, nameof(RadioPurpose_DeathCounter)); break;
                case OutModule.OM_Purpose.OM_Purpose_ResetCounter: CallPropertyChanged(this, nameof(RadioPurpose_ResetCounter)); break;
                default: break;
            }
            switch (next)
            {
                case OutModule.OM_Purpose.OM_Purpose_SplitCounter: CallPropertyChanged(this, nameof(RadioPurpose_SplitCounter)); break;
                case OutModule.OM_Purpose.OM_Purpose_Checklist: CallPropertyChanged(this, nameof(RadioPurpose_Checklist)); break;
                case OutModule.OM_Purpose.OM_Purpose_NoDeath: CallPropertyChanged(this, nameof(RadioPurpose_NoDeath)); break;
                case OutModule.OM_Purpose.OM_Purpose_DeathCounter: CallPropertyChanged(this, nameof(RadioPurpose_DeathCounter)); break;
                case OutModule.OM_Purpose.OM_Purpose_ResetCounter: CallPropertyChanged(this, nameof(RadioPurpose_ResetCounter)); break;
                default: break;
            }
        }
        public bool RadioPurpose_SplitCounter
        {
            get => om.Purpose == OutModule.OM_Purpose.OM_Purpose_SplitCounter;
            set { if (value) SetPurpose(OutModule.OM_Purpose.OM_Purpose_SplitCounter); }
        }
        public bool RadioPurpose_Checklist
        {
            get => om.Purpose == OutModule.OM_Purpose.OM_Purpose_Checklist;
            set { if (value) SetPurpose(OutModule.OM_Purpose.OM_Purpose_Checklist); }
        }
        public bool RadioPurpose_NoDeath
        {
            get => om.Purpose == OutModule.OM_Purpose.OM_Purpose_NoDeath;
            set { if (value) SetPurpose(OutModule.OM_Purpose.OM_Purpose_NoDeath); }
        }
        public bool RadioPurpose_DeathCounter
        {
            get => om.Purpose == OutModule.OM_Purpose.OM_Purpose_DeathCounter;
            set { if (value) SetPurpose(OutModule.OM_Purpose.OM_Purpose_DeathCounter); }
        }
        public bool RadioPurpose_ResetCounter
        {
            get => om.Purpose == OutModule.OM_Purpose.OM_Purpose_ResetCounter;
            set { if (value) SetPurpose(OutModule.OM_Purpose.OM_Purpose_ResetCounter); }
        }

        private void SetHitSeverity(OutModule.OM_Severity next)
        {
            OutModule.OM_Severity prev = om.Severity;
            if (prev == next) return;

            om.Severity = next;
            switch (prev)
            {
                case OutModule.OM_Severity.OM_Severity_AnyHitsCritical: CallPropertyChanged(this, nameof(RadioHitSeverity_AnyHitCritical)); break;
                case OutModule.OM_Severity.OM_Severity_ComparePB: CallPropertyChanged(this, nameof(RadioHitSeverity_ComparePB)); break;
                case OutModule.OM_Severity.OM_Severity_BossHitCritical: CallPropertyChanged(this, nameof(RadioHitSeverity_BossHitCritical)); break;
                default: break;
            }
            switch (next)
            {
                case OutModule.OM_Severity.OM_Severity_AnyHitsCritical: CallPropertyChanged(this, nameof(RadioHitSeverity_AnyHitCritical)); break;
                case OutModule.OM_Severity.OM_Severity_ComparePB: CallPropertyChanged(this, nameof(RadioHitSeverity_ComparePB)); break;
                case OutModule.OM_Severity.OM_Severity_BossHitCritical: CallPropertyChanged(this, nameof(RadioHitSeverity_BossHitCritical)); break;
                default: break;
            }
        }
        public bool RadioHitSeverity_AnyHitCritical
        {
            get => om.Severity == OutModule.OM_Severity.OM_Severity_AnyHitsCritical;
            set { if (value) SetHitSeverity(OutModule.OM_Severity.OM_Severity_AnyHitsCritical); }
        }
        public bool RadioHitSeverity_ComparePB
        {
            get => om.Severity == OutModule.OM_Severity.OM_Severity_ComparePB;
            set { if (value) SetHitSeverity(OutModule.OM_Severity.OM_Severity_ComparePB); }
        }
        public bool RadioHitSeverity_BossHitCritical
        {
            get => om.Severity == OutModule.OM_Severity.OM_Severity_BossHitCritical;
            set { if (value) SetHitSeverity(OutModule.OM_Severity.OM_Severity_BossHitCritical); }
        }
        public bool ShowAttemptsCounter
        {
            get => Settings.ShowAttemptsCounter;
            set => SetAndNotifyWhenChanged(this, ref Settings.ShowAttemptsCounter, value, nameof(ShowAttemptsCounter));
        }
        public bool ShowHeadline
        {
            get => Settings.ShowHeadline;
            set => SetAndNotifyWhenChanged(this, ref Settings.ShowHeadline, value, nameof(ShowHeadline));
        }
        public bool ShowFooter
        {
            get => Settings.ShowFooter;
            set => SetAndNotifyWhenChanged(this, ref Settings.ShowFooter, value, nameof(ShowFooter));
        }
        public bool ShowSessionProgress
        {
            get => Settings.ShowSessionProgress;
            set => SetAndNotifyWhenChanged(this, ref Settings.ShowSessionProgress, value, nameof(ShowSessionProgress));
        }
        public bool ShowProgressBar
        {
            get => Settings.ShowProgressBar;
            set => SetAndNotifyWhenChanged(this, ref Settings.ShowProgressBar, value, nameof(ShowProgressBar));
        }
        public bool ShowHits
        {
            get => Settings.ShowHits;
            set => SetAndNotifyWhenChanged(this, ref Settings.ShowHits, value, nameof(ShowHits));
        }
        public bool ShowHitsCombined
        {
            get => Settings.ShowHitsCombined;
            set => SetAndNotifyWhenChanged(this, ref Settings.ShowHitsCombined, value, nameof(ShowHitsCombined));
        }
        public bool ShowPB
        {
            get => Settings.ShowPB;
            set => SetAndNotifyWhenChanged(this, ref Settings.ShowPB, value, nameof(ShowPB));
        }
        public bool ShowPBTotals
        {
            get => Settings.ShowPBTotals;
            set => SetAndNotifyWhenChanged(this, ref Settings.ShowPBTotals, value, nameof(ShowPBTotals));
        }
        public bool ShowDiff
        {
            get => Settings.ShowDiff;
            set => SetAndNotifyWhenChanged(this, ref Settings.ShowDiff, value, nameof(ShowDiff));
        }
        public bool ShowNumbers
        {
            get => Settings.ShowNumbers;
            set => SetAndNotifyWhenChanged(this, ref Settings.ShowNumbers, value, nameof(ShowNumbers));
        }
        public bool ShowTimeCurrent
        {
            get => Settings.ShowTimeCurrent;
            set => SetAndNotifyWhenChanged(this, ref Settings.ShowTimeCurrent, value, nameof(ShowTimeCurrent));
        }
        public bool ShowTimePB
        {
            get => Settings.ShowTimePB;
            set => SetAndNotifyWhenChanged(this, ref Settings.ShowTimePB, value, nameof(ShowTimePB));
        }
        public bool ShowTimeFooter
        {
            get => Settings.ShowTimeFooter;
            set => SetAndNotifyWhenChanged(this, ref Settings.ShowTimeFooter, value, nameof(ShowTimeFooter));
        }
        public bool ShowTimeDiff
        {
            get => Settings.ShowTimeDiff;
            set => SetAndNotifyWhenChanged(this, ref Settings.ShowTimeDiff, value, nameof(ShowTimeDiff));
        }

        public int ShowSplitsCountFinished
        {
            get => Settings.ShowSplitsCountFinished;
            set => SetAndNotifyWhenNaturalNumberChanged(this, ref Settings.ShowSplitsCountFinished, value, nameof(ShowSplitsCountFinished));
        }
        public int ShowSplitsCountUpcoming
        {
            get => Settings.ShowSplitsCountUpcoming;
            set => SetAndNotifyWhenNaturalNumberChanged(this, ref Settings.ShowSplitsCountUpcoming, value, nameof(ShowSplitsCountUpcoming));
        }
#endregion
    }
}
