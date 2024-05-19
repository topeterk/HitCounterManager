//MIT License

//Copyright (c) 2021-2024 Peter Kirmeier

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
using ReactiveUI;
using Avalonia.Controls.Notifications;
using HitCounterManager.Common;
using HitCounterManager.Models;

namespace HitCounterManager.ViewModels
{
    public class SettingsPageViewModel : ViewModelWindowBase
    {
        public static SettingsRoot Settings => App.CurrentApp.Settings;
        private readonly Shortcuts sc = App.CurrentApp.sc;
        private readonly OutModule om = App.CurrentApp.om;
        private bool AppearedOnce = false;
        private SC_Type CapturingId = SC_Type.SC_Type_MAX;

        public SettingsPageViewModel()
        {
            _StyleFontName = Settings.StyleFontName;
            _StyleFontUrl = Settings.StyleFontUrl;
            _StyleCssUrl = Settings.StyleCssUrl;

            ToggleShowInfo = ReactiveCommand.Create((string name) => { ShowInfo[name].Value = !ShowInfo[name].Value; });

            Capture = ReactiveCommand.Create<SC_Type>((type) =>
            {
                SC_Type CapturingIdPrev = CapturingId;

                if ((SC_Type.SC_Type_MAX == type) || (CapturingId == type))
                {
                    CapturingId = SC_Type.SC_Type_MAX; // Stop capturing and implicitly stop timer
                    RecordActionChanged(CapturingIdPrev);
                }
                else if (SC_Type.SC_Type_MAX != CapturingId)
                {
                    CapturingId = type; // Switch to another key
                    RecordActionChanged(CapturingIdPrev);
                    RecordActionChanged(CapturingId);
                }
                else
                {
                    // Start capturing
                    CapturingId = type;
                    App.CurrentApp.StartApplicationTimer(TimerIDs.ShortcutsCapture, 20, CaptureKeysTick);
                    RecordActionChanged(CapturingId);
                }
            });

            ApplyCssAndFont = ReactiveCommand.Create(() =>
            {
                Settings.StyleFontName = _StyleFontName;
                Settings.StyleFontUrl = _StyleFontUrl;
                Settings.StyleCssUrl = _StyleCssUrl;

                // Implicitly enable custom settings
                SetAndNotifyWhenChanged(ref Settings.StyleUseCustom, true);

                CallPropertyChanged(nameof(ApplyCssAndFont));
            });

            PropertyChanged += SettingDataChangedHandler;
        }

        // Move this into a event handler of Settings itself, so we can run Update depending on the whole Settings from anywhere
        private void SettingDataChangedHandler(object? sender, PropertyChangedEventArgs e)
        {
            // Do not propagate local variables
            if (null != e.PropertyName && (
                    e.PropertyName.Equals(nameof(StyleFontName)) ||
                    e.PropertyName.Equals(nameof(StyleFontUrl)) ||
                    e.PropertyName.Equals(nameof(StyleCssUrl)))
                ) return;

            App.CurrentApp.ProfileViewViewModel?.OutputDataChangedHandler(sender, e);
        }

        public class ShowInfoBool : NotifyPropertyChangedImpl
        {
            private bool _Value = false;
            public bool Value
            {
                get => _Value;
                set => SetAndNotifyWhenChanged(ref _Value, value);
            }
        }

        private readonly Dictionary<string, ShowInfoBool> _ShowInfo = new () {
            {"RadioHotKeyMethod_Sync", new ShowInfoBool()},
            {"RadioHotKeyMethod_Async", new ShowInfoBool()},
            {"RadioHotKeyMethod_LLKb", new ShowInfoBool()},
            {"ShowAttemptsCounter", new ShowInfoBool()},
            {"ShowHeadline", new ShowInfoBool()},
            {"ShowProgressBar", new ShowInfoBool()},
            {"ShowFooter", new ShowInfoBool()},
            {"ShowTimeFooter", new ShowInfoBool()},
            {"ShowHits", new ShowInfoBool()},
            {"ShowHitsCombined", new ShowInfoBool()},
            {"ShowDiff", new ShowInfoBool()},
            {"ShowPB", new ShowInfoBool()},
            {"ShowPBTotals", new ShowInfoBool()},
            {"ShowTimeCurrent", new ShowInfoBool()},
            {"ShowTimeDiff", new ShowInfoBool()},
            {"ShowTimePB", new ShowInfoBool()},
            {"ShowSessionProgress", new ShowInfoBool()},
            {"ShowNumbers", new ShowInfoBool()},
            {"StyleUseHighContrast", new ShowInfoBool()},
            {"StyleUseHighContrastNames", new ShowInfoBool()},
            {"StyleProgressBarColored", new ShowInfoBool()},
            {"StyleSubscriptPB", new ShowInfoBool()},
            {"StyleUseRoman", new ShowInfoBool()},
            {"StyleHightlightCurrentSplit", new ShowInfoBool()},
            {"RadioPurpose_SplitCounter", new ShowInfoBool()},
            {"RadioPurpose_Checklist", new ShowInfoBool()},
            {"RadioPurpose_NoDeath", new ShowInfoBool()},
            {"RadioPurpose_DeathCounter", new ShowInfoBool()},
            {"RadioPurpose_ResetCounter", new ShowInfoBool()},
            {"RadioHitSeverity_AnyHitCritical", new ShowInfoBool()},
            {"RadioHitSeverity_ComparePB", new ShowInfoBool()},
            {"RadioHitSeverity_BossHitCritical", new ShowInfoBool()},
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
            if (SC_Type.SC_Type_MAX == CapturingId) return false;

            List<int>? PressedKeys = App.GetKeysPressedAsync();
            if (null == PressedKeys) return (SC_Type.SC_Type_MAX != CapturingId);

            VirtualKeyStates keyData = VirtualKeyStates.None;
            foreach (int KeyCode in PressedKeys)
            {
                VirtualKeyStates keyCode = (VirtualKeyStates)KeyCode;
                if ((keyCode < VirtualKeyStates.VK_BACK) || (VirtualKeyStates.VK_OEM_CLEAR <= keyCode)) continue; // Ignore mouse keys

                switch (keyCode)
                {
                    // When virtual modifiers are not set, we look for actual keys as well
                    case VirtualKeyStates.VK_LSHIFT:
                    case VirtualKeyStates.VK_RSHIFT: keyData |= VirtualKeyStates.Shift; break;
                    case VirtualKeyStates.VK_LCONTROL:
                    case VirtualKeyStates.VK_RCONTROL: keyData |= VirtualKeyStates.Control; break;
                    case VirtualKeyStates.VK_LMENU:
                    case VirtualKeyStates.VK_RMENU: keyData |= VirtualKeyStates.Alt; break;

                    // Assign virtual modifiers
                    case VirtualKeyStates.VK_SHIFT: keyData |= VirtualKeyStates.Shift; break;
                    case VirtualKeyStates.VK_CONTROL: keyData |= VirtualKeyStates.Control; break;
                    case VirtualKeyStates.VK_MENU: keyData |= VirtualKeyStates.Alt; break;

                    // Assign key code
                    default:
                        if ((VirtualKeyStates.KeyCode & keyData) != VirtualKeyStates.None) return (SC_Type.SC_Type_MAX != CapturingId); // Only a single key can be captured
                        keyData |= keyCode;
                        break;
                }
            }
            if ((VirtualKeyStates.KeyCode & keyData) != VirtualKeyStates.None) RegisterHotKey(CapturingId, keyData); // Was a key combination detected? -> Register key

            return SC_Type.SC_Type_MAX != CapturingId;
        }

        /// <summary>
        /// Registers a hot key and stores it
        /// </summary>
        /// <param name="Id">Configuration type to be assigned to hot key</param>
        /// <param name="e">Key combination</param>
        private void RegisterHotKey(SC_Type Id, VirtualKeyStates keyData)
        {
            ShortcutsKey key = new(keyData);

            if (key.KeyCode == VirtualKeyStates.None) return;
            if (key.KeyCode == VirtualKeyStates.VK_SHIFT) return;
            if (key.KeyCode == VirtualKeyStates.VK_CONTROL) return;
            if (key.KeyCode == VirtualKeyStates.Alt) return;
            if (key.KeyCode == VirtualKeyStates.VK_MENU) return; // = Alt

            // register hotkey
            sc.Key_Set(Id, key);

            switch (Id)
            {
                case SC_Type.SC_Type_Hit:
                    CallPropertyChanged(nameof(ShortcutHitDescription));
                    ShortcutHitEnable = true;
                    break;
                case SC_Type.SC_Type_HitUndo:
                    CallPropertyChanged(nameof(ShortcutHitUndoDescription));
                    ShortcutHitUndoEnable = true;
                    break;
                case SC_Type.SC_Type_WayHit:
                    CallPropertyChanged(nameof(ShortcutWayHitDescription));
                    ShortcutWayHitEnable = true;
                    break;
                case SC_Type.SC_Type_WayHitUndo:
                    CallPropertyChanged(nameof(ShortcutWayHitUndoDescription));
                    ShortcutWayHitUndoEnable = true;
                    break;
                case SC_Type.SC_Type_Split:
                    CallPropertyChanged(nameof(ShortcutSplitDescription));
                    ShortcutSplitEnable = true;
                    break;
                case SC_Type.SC_Type_SplitPrev:
                    CallPropertyChanged(nameof(ShortcutSplitPrevDescription));
                    ShortcutSplitPrevEnable = true;
                    break;
                case SC_Type.SC_Type_PB:
                    CallPropertyChanged(nameof(ShortcutPBDescription));
                    ShortcutPBEnable = true;
                    break;
                case SC_Type.SC_Type_Reset:
                    CallPropertyChanged(nameof(ShortcutResetDescription));
                    ShortcutResetEnable = true;
                    break;
                case SC_Type.SC_Type_TimerStart:
                    CallPropertyChanged(nameof(ShortcutTimerStartDescription));
                    ShortcutTimerStartEnable = true;
                    break;
                case SC_Type.SC_Type_TimerStop:
                    CallPropertyChanged(nameof(ShortcutTimerStopDescription));
                    ShortcutTimerStopEnable = true;
                    break;
                default: break;
            }
        }

        public sealed override void OnAppearing()
        {
            if (!AppearedOnce)
            {
                AppearedOnce = true;
                // Shortcuts require a window handle to register on
                // but when this ViewModel gets created, the handle is not know yet
                // therefore the settings must be loaded again later (=here)
                // in order to present the correct data
                CallPropertyChanged(nameof(ShortcutHitRecordAction));
                CallPropertyChanged(nameof(ShortcutHitUndoRecordAction));
                CallPropertyChanged(nameof(ShortcutWayHitRecordAction));
                CallPropertyChanged(nameof(ShortcutWayHitUndoRecordAction));
                CallPropertyChanged(nameof(ShortcutSplitRecordAction));
                CallPropertyChanged(nameof(ShortcutSplitPrevRecordAction));
                CallPropertyChanged(nameof(ShortcutPBRecordAction));
                CallPropertyChanged(nameof(ShortcutResetRecordAction));
                CallPropertyChanged(nameof(ShortcutTimerStartRecordAction));
                CallPropertyChanged(nameof(ShortcutTimerStopRecordAction));

                CallPropertyChanged(nameof(ShortcutHitDescription));
                CallPropertyChanged(nameof(ShortcutHitUndoDescription));
                CallPropertyChanged(nameof(ShortcutWayHitDescription));
                CallPropertyChanged(nameof(ShortcutWayHitUndoDescription));
                CallPropertyChanged(nameof(ShortcutSplitDescription));
                CallPropertyChanged(nameof(ShortcutSplitPrevDescription));
                CallPropertyChanged(nameof(ShortcutPBDescription));
                CallPropertyChanged(nameof(ShortcutResetDescription));
                CallPropertyChanged(nameof(ShortcutTimerStartDescription));
                CallPropertyChanged(nameof(ShortcutTimerStopDescription));

                CallPropertyChanged(nameof(RadioHotKeyMethod_Sync));
                CallPropertyChanged(nameof(RadioHotKeyMethod_Async));
                CallPropertyChanged(nameof(RadioHotKeyMethod_LLKb));
            }

            App.CurrentApp.SettingsDialogOpen = true; // To disable hotkeys (for main application)
        }

        public sealed override void OnDisappearing()
        {
            Capture.Execute(SC_Type.SC_Type_MAX); // Stop any running capture
            App.CurrentApp.SettingsDialogOpen = false; // Re-Enable hotkeys (for main application)
        }

        #region Global Shortcuts
        public bool ShortcutHitEnable
        {
            get => Settings.ShortcutHitEnable;
            set
            {
                if (SetAndNotifyWhenChanged(ref Settings.ShortcutHitEnable, value))
                    sc.Key_SetState(SC_Type.SC_Type_Hit, Settings.ShortcutHitEnable);
            }
        }
        public bool ShortcutHitUndoEnable
        {
            get => Settings.ShortcutHitUndoEnable;
            set
            {
                if (SetAndNotifyWhenChanged(ref Settings.ShortcutHitUndoEnable, value))
                    sc.Key_SetState(SC_Type.SC_Type_HitUndo, Settings.ShortcutHitUndoEnable);
            }
        }
        public bool ShortcutWayHitEnable
        {
            get => Settings.ShortcutWayHitEnable;
            set
            {
                if (SetAndNotifyWhenChanged(ref Settings.ShortcutWayHitEnable, value))
                    sc.Key_SetState(SC_Type.SC_Type_WayHit, Settings.ShortcutWayHitEnable);
            }
        }
        public bool ShortcutWayHitUndoEnable
        {
            get => Settings.ShortcutWayHitUndoEnable;
            set
            {
                if (SetAndNotifyWhenChanged(ref Settings.ShortcutWayHitUndoEnable, value))
                    sc.Key_SetState(SC_Type.SC_Type_WayHitUndo, Settings.ShortcutWayHitUndoEnable);
            }
        }
        public bool ShortcutSplitEnable
        {
            get => Settings.ShortcutSplitEnable;
            set
            {
                if (SetAndNotifyWhenChanged(ref Settings.ShortcutSplitEnable, value))
                    sc.Key_SetState(SC_Type.SC_Type_Split, Settings.ShortcutSplitEnable);
            }
        }
        public bool ShortcutSplitPrevEnable
        {
            get => Settings.ShortcutSplitPrevEnable;
            set
            {
                if (SetAndNotifyWhenChanged(ref Settings.ShortcutSplitPrevEnable, value))
                    sc.Key_SetState(SC_Type.SC_Type_SplitPrev, Settings.ShortcutSplitPrevEnable);
            }
        }
        public bool ShortcutPBEnable
        {
            get => Settings.ShortcutPBEnable;
            set
            {
                if (SetAndNotifyWhenChanged(ref Settings.ShortcutPBEnable, value))
                    sc.Key_SetState(SC_Type.SC_Type_PB, Settings.ShortcutPBEnable);
            }
        }
        public bool ShortcutResetEnable
        {
            get => Settings.ShortcutResetEnable;
            set
            {
                if (SetAndNotifyWhenChanged(ref Settings.ShortcutResetEnable, value))
                    sc.Key_SetState(SC_Type.SC_Type_Reset, Settings.ShortcutResetEnable);
            }
        }
        public bool ShortcutTimerStartEnable
        {
            get => Settings.ShortcutTimerStartEnable;
            set
            {
                if (SetAndNotifyWhenChanged(ref Settings.ShortcutTimerStartEnable, value))
                    sc.Key_SetState(SC_Type.SC_Type_TimerStart, Settings.ShortcutTimerStartEnable);
            }
        }
        public bool ShortcutTimerStopEnable
        {
            get => Settings.ShortcutTimerStopEnable;
            set
            {
                if (SetAndNotifyWhenChanged(ref Settings.ShortcutTimerStopEnable, value))
                    sc.Key_SetState(SC_Type.SC_Type_TimerStop, Settings.ShortcutTimerStopEnable);
            }
        }

        private void RecordActionChanged(SC_Type type)
        {
            switch (type)
            {
                case SC_Type.SC_Type_Hit: CallPropertyChanged(nameof(ShortcutHitRecordAction)); break;
                case SC_Type.SC_Type_HitUndo: CallPropertyChanged(nameof(ShortcutHitUndoRecordAction)); break;
                case SC_Type.SC_Type_WayHit: CallPropertyChanged(nameof(ShortcutWayHitRecordAction)); break;
                case SC_Type.SC_Type_WayHitUndo: CallPropertyChanged(nameof(ShortcutWayHitUndoRecordAction)); break;
                case SC_Type.SC_Type_Split: CallPropertyChanged(nameof(ShortcutSplitRecordAction)); break;
                case SC_Type.SC_Type_SplitPrev: CallPropertyChanged(nameof(ShortcutSplitPrevRecordAction)); break;
                case SC_Type.SC_Type_PB: CallPropertyChanged(nameof(ShortcutPBRecordAction)); break;
                case SC_Type.SC_Type_Reset: CallPropertyChanged(nameof(ShortcutResetRecordAction)); break;
                case SC_Type.SC_Type_TimerStart: CallPropertyChanged(nameof(ShortcutTimerStartRecordAction)); break;
                case SC_Type.SC_Type_TimerStop: CallPropertyChanged(nameof(ShortcutTimerStopRecordAction)); break;
                default: break;
            }
        }
        public string ShortcutHitRecordAction => SC_Type.SC_Type_Hit == CapturingId ? "Stop" : "Rec";
        public string ShortcutHitUndoRecordAction => SC_Type.SC_Type_HitUndo == CapturingId ? "Stop" : "Rec";
        public string ShortcutWayHitRecordAction => SC_Type.SC_Type_WayHit == CapturingId ? "Stop" : "Rec";
        public string ShortcutWayHitUndoRecordAction => SC_Type.SC_Type_WayHitUndo == CapturingId ? "Stop" : "Rec";
        public string ShortcutSplitRecordAction => SC_Type.SC_Type_Split == CapturingId ? "Stop" : "Rec";
        public string ShortcutSplitPrevRecordAction => SC_Type.SC_Type_SplitPrev == CapturingId ? "Stop" : "Rec";
        public string ShortcutPBRecordAction => SC_Type.SC_Type_PB == CapturingId ? "Stop" : "Rec";
        public string ShortcutResetRecordAction => SC_Type.SC_Type_Reset == CapturingId ? "Stop" : "Rec";
        public string ShortcutTimerStartRecordAction => SC_Type.SC_Type_TimerStart == CapturingId ? "Stop" : "Rec";
        public string ShortcutTimerStopRecordAction => SC_Type.SC_Type_TimerStop == CapturingId ? "Stop" : "Rec";

        public string ShortcutHitDescription => sc.Key_Get(SC_Type.SC_Type_Hit).GetDescriptionString();
        public string ShortcutHitUndoDescription => sc.Key_Get(SC_Type.SC_Type_HitUndo).GetDescriptionString();
        public string ShortcutWayHitDescription => sc.Key_Get(SC_Type.SC_Type_WayHit).GetDescriptionString();
        public string ShortcutWayHitUndoDescription => sc.Key_Get(SC_Type.SC_Type_WayHitUndo).GetDescriptionString();
        public string ShortcutSplitDescription => sc.Key_Get(SC_Type.SC_Type_Split).GetDescriptionString();
        public string ShortcutSplitPrevDescription => sc.Key_Get(SC_Type.SC_Type_SplitPrev).GetDescriptionString();
        public string ShortcutPBDescription => sc.Key_Get(SC_Type.SC_Type_PB).GetDescriptionString();
        public string ShortcutResetDescription => sc.Key_Get(SC_Type.SC_Type_Reset).GetDescriptionString();
        public string ShortcutTimerStartDescription => sc.Key_Get(SC_Type.SC_Type_TimerStart).GetDescriptionString();
        public string ShortcutTimerStopDescription => sc.Key_Get(SC_Type.SC_Type_TimerStop).GetDescriptionString();

        private void SetNextShortcutMethod(Shortcuts.SC_HotKeyMethod next)
        {
            Shortcuts.SC_HotKeyMethod prev = sc.NextStart_Method;
            if (prev == next) return;

            sc.NextStart_Method = next;
            switch (prev)
            {
                case Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Sync: CallPropertyChanged(nameof(RadioHotKeyMethod_Sync)); break;
                case Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Async: CallPropertyChanged(nameof(RadioHotKeyMethod_Async)); break;
                case Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_LLKb: CallPropertyChanged(nameof(RadioHotKeyMethod_LLKb)); break;
                default: break;
            }
            switch (next)
            {
                case Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Sync: CallPropertyChanged(nameof(RadioHotKeyMethod_Sync)); break;
                case Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Async: CallPropertyChanged(nameof(RadioHotKeyMethod_Async)); break;
                case Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_LLKb: CallPropertyChanged(nameof(RadioHotKeyMethod_LLKb)); break;
                default: break;
            }
            App.CurrentApp.DisplayAlert("Restart required", "Changes only take effect after restarting the application.", NotificationType.Information);
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
            set => SetAndNotifyWhenChanged(ref Settings.StyleUseHighContrast, value);
        }
        public bool StyleUseHighContrastNames
        {
            get => Settings.StyleUseHighContrastNames;
            set => SetAndNotifyWhenChanged(ref Settings.StyleUseHighContrastNames, value);
        }
        public bool StyleProgressBarColored
        {
            get => Settings.StyleProgressBarColored;
            set => SetAndNotifyWhenChanged(ref Settings.StyleProgressBarColored, value);
        }
        public bool StyleSubscriptPB
        {
            get => Settings.StyleSubscriptPB;
            set => SetAndNotifyWhenChanged(ref Settings.StyleSubscriptPB, value);
        }
        public bool StyleUseRoman
        {
            get => Settings.StyleUseRoman;
            set => SetAndNotifyWhenChanged(ref Settings.StyleUseRoman, value);
        }
        public bool StyleHightlightCurrentSplit
        {
            get => Settings.StyleHightlightCurrentSplit;
            set => SetAndNotifyWhenChanged(ref Settings.StyleHightlightCurrentSplit, value);
        }

        public int StyleDesiredHeight
        {
            get => Settings.StyleDesiredHeight;
            set => SetAndNotifyWhenNaturalNumberChanged(ref Settings.StyleDesiredHeight, value);
        }
        public int StyleDesiredWidth
        {
            get => Settings.StyleDesiredWidth;
            set => SetAndNotifyWhenNaturalNumberChanged(ref Settings.StyleDesiredWidth, value);
        }

        public bool StyleUseCustom
        {
            get => Settings.StyleUseCustom;
            set => SetAndNotifyWhenChanged(ref Settings.StyleUseCustom, value);
        }
        private string _StyleFontName;
        public string StyleFontName
        {
            get => _StyleFontName;
            set => SetAndNotifyWhenChanged(ref _StyleFontName, value);
        }
        private string _StyleFontUrl;
        public string StyleFontUrl
        {
            get => _StyleFontUrl;
            set => SetAndNotifyWhenChanged(ref _StyleFontUrl, value);
        }
        private string _StyleCssUrl;
        public string StyleCssUrl
        {
            get => _StyleCssUrl;
            set => SetAndNotifyWhenChanged(ref _StyleCssUrl, value);
        }
        public ICommand ApplyCssAndFont { get; }

        public ICommand WebOpenGoogleFontsUrl { get; } = ReactiveCommand.Create(() => Extensions.OpenWithBrowser(new Uri("https://fonts.google.com")));
#endregion

#region Behavior
        private void SetPurpose(OutModule.OM_Purpose next)
        {
            OutModule.OM_Purpose prev = om.Purpose;
            if (prev == next) return;

            om.Purpose = next;
            switch (prev)
            {
                case OutModule.OM_Purpose.OM_Purpose_SplitCounter: CallPropertyChanged(nameof(RadioPurpose_SplitCounter)); break;
                case OutModule.OM_Purpose.OM_Purpose_Checklist: CallPropertyChanged(nameof(RadioPurpose_Checklist)); break;
                case OutModule.OM_Purpose.OM_Purpose_NoDeath: CallPropertyChanged(nameof(RadioPurpose_NoDeath)); break;
                case OutModule.OM_Purpose.OM_Purpose_DeathCounter: CallPropertyChanged(nameof(RadioPurpose_DeathCounter)); break;
                case OutModule.OM_Purpose.OM_Purpose_ResetCounter: CallPropertyChanged(nameof(RadioPurpose_ResetCounter)); break;
                default: break;
            }
            switch (next)
            {
                case OutModule.OM_Purpose.OM_Purpose_SplitCounter: CallPropertyChanged(nameof(RadioPurpose_SplitCounter)); break;
                case OutModule.OM_Purpose.OM_Purpose_Checklist: CallPropertyChanged(nameof(RadioPurpose_Checklist)); break;
                case OutModule.OM_Purpose.OM_Purpose_NoDeath: CallPropertyChanged(nameof(RadioPurpose_NoDeath)); break;
                case OutModule.OM_Purpose.OM_Purpose_DeathCounter: CallPropertyChanged(nameof(RadioPurpose_DeathCounter)); break;
                case OutModule.OM_Purpose.OM_Purpose_ResetCounter: CallPropertyChanged(nameof(RadioPurpose_ResetCounter)); break;
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
                case OutModule.OM_Severity.OM_Severity_AnyHitsCritical: CallPropertyChanged(nameof(RadioHitSeverity_AnyHitCritical)); break;
                case OutModule.OM_Severity.OM_Severity_ComparePB: CallPropertyChanged(nameof(RadioHitSeverity_ComparePB)); break;
                case OutModule.OM_Severity.OM_Severity_BossHitCritical: CallPropertyChanged(nameof(RadioHitSeverity_BossHitCritical)); break;
                default: break;
            }
            switch (next)
            {
                case OutModule.OM_Severity.OM_Severity_AnyHitsCritical: CallPropertyChanged(nameof(RadioHitSeverity_AnyHitCritical)); break;
                case OutModule.OM_Severity.OM_Severity_ComparePB: CallPropertyChanged(nameof(RadioHitSeverity_ComparePB)); break;
                case OutModule.OM_Severity.OM_Severity_BossHitCritical: CallPropertyChanged(nameof(RadioHitSeverity_BossHitCritical)); break;
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
            set => SetAndNotifyWhenChanged(ref Settings.ShowAttemptsCounter, value);
        }
        public bool ShowHeadline
        {
            get => Settings.ShowHeadline;
            set => SetAndNotifyWhenChanged(ref Settings.ShowHeadline, value);
        }
        public bool ShowFooter
        {
            get => Settings.ShowFooter;
            set => SetAndNotifyWhenChanged(ref Settings.ShowFooter, value);
        }
        public bool ShowSessionProgress
        {
            get => Settings.ShowSessionProgress;
            set => SetAndNotifyWhenChanged(ref Settings.ShowSessionProgress, value);
        }
        public bool ShowProgressBar
        {
            get => Settings.ShowProgressBar;
            set => SetAndNotifyWhenChanged(ref Settings.ShowProgressBar, value);
        }
        public bool ShowHits
        {
            get => Settings.ShowHits;
            set => SetAndNotifyWhenChanged(ref Settings.ShowHits, value);
        }
        public bool ShowHitsCombined
        {
            get => Settings.ShowHitsCombined;
            set => SetAndNotifyWhenChanged(ref Settings.ShowHitsCombined, value);
        }
        public bool ShowPB
        {
            get => Settings.ShowPB;
            set => SetAndNotifyWhenChanged(ref Settings.ShowPB, value);
        }
        public bool ShowPBTotals
        {
            get => Settings.ShowPBTotals;
            set => SetAndNotifyWhenChanged(ref Settings.ShowPBTotals, value);
        }
        public bool ShowDiff
        {
            get => Settings.ShowDiff;
            set => SetAndNotifyWhenChanged(ref Settings.ShowDiff, value);
        }
        public bool ShowNumbers
        {
            get => Settings.ShowNumbers;
            set => SetAndNotifyWhenChanged(ref Settings.ShowNumbers, value);
        }
        public bool ShowTimeCurrent
        {
            get => Settings.ShowTimeCurrent;
            set => SetAndNotifyWhenChanged(ref Settings.ShowTimeCurrent, value);
        }
        public bool ShowTimePB
        {
            get => Settings.ShowTimePB;
            set => SetAndNotifyWhenChanged(ref Settings.ShowTimePB, value);
        }
        public bool ShowTimeFooter
        {
            get => Settings.ShowTimeFooter;
            set => SetAndNotifyWhenChanged(ref Settings.ShowTimeFooter, value);
        }
        public bool ShowTimeDiff
        {
            get => Settings.ShowTimeDiff;
            set => SetAndNotifyWhenChanged(ref Settings.ShowTimeDiff, value);
        }

        public int ShowSplitsCountFinished
        {
            get => Settings.ShowSplitsCountFinished;
            set => SetAndNotifyWhenNaturalNumberChanged(ref Settings.ShowSplitsCountFinished, value);
        }
        public int ShowSplitsCountUpcoming
        {
            get => Settings.ShowSplitsCountUpcoming;
            set => SetAndNotifyWhenNaturalNumberChanged(ref Settings.ShowSplitsCountUpcoming, value);
        }
#endregion
    }
}
