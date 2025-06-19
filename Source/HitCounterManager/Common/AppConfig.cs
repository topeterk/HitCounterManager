//MIT License

//Copyright (c) 2016-2025 Peter Kirmeier

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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Avalonia.Platform;
using HitCounterManager.Common;
using HitCounterManager.Models;

namespace HitCounterManager
{
    #region Settings types

    /// <summary>
    /// Content of XML stored user data (settings)
    /// </summary>
    [Serializable]
    public class SettingsRoot
    {
        public int Version;
        public int MainWidth;
        public int MainHeight;
        public int MainPosX;
        public int MainPosY;
        public bool ReadOnlyMode;
        public bool AlwaysOnTop;
        public bool DarkMode;
        public bool CheckUpdatesOnStartup;
        public int HotKeyMethod;
        public bool ShortcutResetEnable;
        public int ShortcutResetKeyCode;            // Actually KeyData as it is combined with modifiers
        public bool ShortcutHitEnable;
        public int ShortcutHitKeyCode;              // Actually KeyData as it is combined with modifiers
        public bool ShortcutHitUndoEnable;
        public int ShortcutHitUndoKeyCode;          // Actually KeyData as it is combined with modifiers
        public bool ShortcutWayHitEnable;
        public int ShortcutWayHitKeyCode;           // Actually KeyData as it is combined with modifiers
        public bool ShortcutWayHitUndoEnable;
        public int ShortcutWayHitUndoKeyCode;       // Actually KeyData as it is combined with modifiers
        public bool ShortcutSplitEnable;
        public int ShortcutSplitKeyCode;            // Actually KeyData as it is combined with modifiers
        public bool ShortcutSplitPrevEnable;
        public int ShortcutSplitPrevKeyCode;        // Actually KeyData as it is combined with modifiers
        public bool ShortcutPBEnable;
        public int ShortcutPBKeyCode;               // Actually KeyData as it is combined with modifiers
        public bool ShortcutTimerStartEnable;
        public int ShortcutTimerStartKeyCode;       // Actually KeyData as it is combined with modifiers
        public bool ShortcutTimerStopEnable;
        public int ShortcutTimerStopKeyCode;        // Actually KeyData as it is combined with modifiers
        public bool ShortcutHitBossPrevEnable;
        public int ShortcutHitBossPrevKeyCode;      // Actually KeyData as it is combined with modifiers
        public bool ShortcutBossHitUndoPrevEnable;
        public int ShortcutBossHitUndoPrevKeyCode;  // Actually KeyData as it is combined with modifiers
        public bool ShortcutHitWayPrevEnable;
        public int ShortcutHitWayPrevKeyCode;       // Actually KeyData as it is combined with modifiers
        public bool ShortcutWayHitUndoPrevEnable;
        public int ShortcutWayHitUndoPrevKeyCode;   // Actually KeyData as it is combined with modifiers
        #region AutoSplitter
        public bool ShortcutPracticeEnable;
        public int ShortcutPracticeKeyCode;         // Actually KeyData as it is combined with modifiers
        #endregion
        public string Inputfile = "HitCounter.template";
        public string OutputFile = "HitCounter.html";
        public bool ShowAttemptsCounter;
        public bool ShowHeadline;
        public bool ShowFooter;
        public bool ShowSessionProgress;
        public bool ShowProgressBar;
        public int ShowSplitsCountFinished;
        public int ShowSplitsCountUpcoming;
        public bool ShowHits;
        public bool ShowHitsCombined;
        public bool ShowNumbers;
        public bool ShowPB;
        public bool ShowPBTotals;
        public bool ShowDiff;
        public bool ShowTimeCurrent;
        public bool ShowTimeDiff;
        public bool ShowTimePB;
        public bool ShowDurationCurrent;
        public bool ShowDurationDiff;
        public bool ShowDurationPB;
        public bool ShowDurationGold;
        public bool ShowTimeFooter;
        public int SubSplitVisibility;
        public int Purpose;
        public int Severity;
        public bool StyleUseHighContrast;
        public bool StyleUseHighContrastNames;
        public bool StyleUseRoman;
        public bool StyleProgressBarColored;
        public bool StyleUseCustom;
        public string StyleCssUrl = "stylesheet_pink.css";
        public string StyleFontUrl = "https://fonts.googleapis.com/css?family=Fontdiner%20Swanky";
        public string StyleFontName = "Fontdiner Swanky";
        public int StyleDesiredHeight;
        public int StyleDesiredWidth;
        public string StyleTableAlignment = "tblcenter";
        public bool StyleSubscriptPB;
        public bool StyleHightlightCurrentSplit;
        public string ProfileSelected = "Unnamed";
        public Profiles Profiles = new ();
    }

    public enum Settings_SubSplitVisibility
    {
        Settings_SubSplitVisibility_ShowAll = 0,
        Settings_SubSplitVisibility_CollapseNonActive = 1,
        Settings_SubSplitVisibility_HideAll = 2
    };

    #endregion

    public partial class App
    {
        private SaveModule<SettingsRoot> sm;

        /// <summary>
        /// Validates a hotkey and when enabled registers it
        /// </summary>
        private bool LoadHotKeySettings(SC_Type Type, int KeyData, bool Enable)
        {
            ShortcutsKey key = new((VirtualKeyStates)KeyData);
            if (Enable)
            {
                sc.Key_Set(Type, key);
                if (!sc.Key_Get(Type).valid)
                    return false;
            }
            else
                sc.Key_PreSet(Type, key);

            return true;
        }

        /// <summary>
        /// Registers all configured hot keys
        /// </summary>
        /// <returns>true = success, false = error occurred</returns>
        private bool LoadAllHotKeySettings()
        {
            bool Success = true;

            if (Statics.GlobalHotKeySupport)
            {
                sc.Initialize((Shortcuts.SC_HotKeyMethod)Settings.HotKeyMethod, NativeWindowHandle);

                if (!LoadHotKeySettings(SC_Type.SC_Type_Reset, Settings.ShortcutResetKeyCode, Settings.ShortcutResetEnable)) Success = false;
                if (!LoadHotKeySettings(SC_Type.SC_Type_Hit, Settings.ShortcutHitKeyCode, Settings.ShortcutHitEnable)) Success = false;
                if (!LoadHotKeySettings(SC_Type.SC_Type_HitUndo, Settings.ShortcutHitUndoKeyCode, Settings.ShortcutHitUndoEnable)) Success = false;
                if (!LoadHotKeySettings(SC_Type.SC_Type_WayHit, Settings.ShortcutWayHitKeyCode, Settings.ShortcutWayHitEnable)) Success = false;
                if (!LoadHotKeySettings(SC_Type.SC_Type_WayHitUndo, Settings.ShortcutWayHitUndoKeyCode, Settings.ShortcutWayHitUndoEnable)) Success = false;
                if (!LoadHotKeySettings(SC_Type.SC_Type_Split, Settings.ShortcutSplitKeyCode, Settings.ShortcutSplitEnable)) Success = false;
                if (!LoadHotKeySettings(SC_Type.SC_Type_SplitPrev, Settings.ShortcutSplitPrevKeyCode, Settings.ShortcutSplitPrevEnable)) Success = false;
                if (!LoadHotKeySettings(SC_Type.SC_Type_PB, Settings.ShortcutPBKeyCode, Settings.ShortcutPBEnable)) Success = false;
                if (!LoadHotKeySettings(SC_Type.SC_Type_TimerStart, Settings.ShortcutTimerStartKeyCode, Settings.ShortcutTimerStartEnable)) Success = false;
                if (!LoadHotKeySettings(SC_Type.SC_Type_TimerStop, Settings.ShortcutTimerStopKeyCode, Settings.ShortcutTimerStopEnable)) Success = false;
                if (!LoadHotKeySettings(SC_Type.SC_Type_HitBossPrev, Settings.ShortcutHitBossPrevKeyCode, Settings.ShortcutHitBossPrevEnable)) Success = false;
                if (!LoadHotKeySettings(SC_Type.SC_Type_BossHitUndoPrev, Settings.ShortcutBossHitUndoPrevKeyCode, Settings.ShortcutBossHitUndoPrevEnable)) Success = false;
                if (!LoadHotKeySettings(SC_Type.SC_Type_HitWayPrev, Settings.ShortcutHitWayPrevKeyCode, Settings.ShortcutHitWayPrevEnable)) Success = false;
                if (!LoadHotKeySettings(SC_Type.SC_Type_WayHitUndoPrev, Settings.ShortcutWayHitUndoPrevKeyCode, Settings.ShortcutWayHitUndoPrevEnable)) Success = false;
                #region AutoSplitter
                if (AutoSplitterCoreModule.AutoSplitterCoreLoaded)
                {
                    if (!LoadHotKeySettings(SC_Type.SC_Type_Practice, Settings.ShortcutPracticeKeyCode, Settings.ShortcutPracticeEnable)) Success = false;
                }
                else
                {
                    Settings.ShortcutPracticeEnable = false;
                }
                #endregion
            }

            return Success;
        }

        /// <summary>
        /// Loads user data from XML
        /// </summary>
        [MemberNotNull(nameof(Settings), nameof(sm))]
        private void LoadSettings()
        {
            int baseVersion = -1;
            SettingsRoot? loadedSettings;
            string storageDir = Statics.ApplicationStorageDir ?? "";

            sm = new (Path.Combine(storageDir, Statics.ApplicationName + "Save.xml"));
            loadedSettings = sm.ReadXML(true);
            if (null != loadedSettings)
                IsCleanStart = false;
            else
            {
                // When no user save file is available, try loading the init file instead to provide predefined profiles and settings
                loadedSettings = sm.ReadXML(false, Path.Combine(storageDir, Statics.ApplicationName + "Init.xml"));
                if (null == loadedSettings)
                {
                    // When init file cannot be read, fallback to defaults from resources
                    string resourceNameDefaultSettingsXML = ".DefaultSettingsXML";
                    loadedSettings = sm.ReadXML(AssetLoader.Open(new Uri($"resm:{Assembly.GetExecutingAssembly().GetName().Name ?? string.Empty}{resourceNameDefaultSettingsXML}")));
                }
            }
            if (null != loadedSettings)
            {
                // successfully loaded Save or Init file, so remember original version for upgrade
                Settings = loadedSettings;
                baseVersion = Settings.Version;
            }
            else
            {
                Settings = new()
                {
                    // prepare defaults..
                    Version = 0,
                    MainWidth = 559,
                    MainHeight = 723,
                    HotKeyMethod = (int)Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Async,
                    ShortcutResetEnable = false,
                    ShortcutResetKeyCode = (int)VirtualKeyStates.Shift | (int)VirtualKeyStates.VK_F6, // Shift F6
                    ShortcutHitEnable = false,
                    ShortcutHitKeyCode = (int)VirtualKeyStates.Shift | (int)VirtualKeyStates.VK_F7, // Shift F7
                    ShortcutSplitEnable = false,
                    ShortcutSplitKeyCode = (int)VirtualKeyStates.Shift | (int)VirtualKeyStates.VK_F8 // Shift F8
                };
                // Settings.InputFile       is new but default value is in ctor
                // Settings.OutputFile      is new but default value is in ctor
                // Settings.ProfileSelected is new but default value is in ctor
            }
            if (Settings.Version == 0) // Coming from version 1.9 or older
            {
                Settings.Version = 1;
                Settings.ShowAttemptsCounter = true;
                Settings.ShowHeadline = true;
                Settings.ShowSplitsCountFinished = 999;
                Settings.ShowSplitsCountUpcoming = 999;
                Settings.StyleUseHighContrast = false;
                Settings.StyleUseCustom = false;
                // Settings.StyleCssUrl  is new but default value is in ctor
                // Settings.StyleFontUrl is new but default value is in ctor
            }
            if (Settings.Version == 1) // Coming from version 1.10
            {
                Settings.Version = 2;
                Settings.MainWidth += 31; // added "SP" checkbox to datagrid
                Settings.ShowSessionProgress = true;
                Settings.StyleDesiredWidth = 0;
            }
            if (Settings.Version == 2) // Coming from version 1.11 - 1.14
            {
                Settings.Version = 3;
                Settings.ShortcutHitUndoEnable = false;
                Settings.ShortcutHitUndoKeyCode = (int)VirtualKeyStates.Shift | (int)VirtualKeyStates.VK_F9; // Shift F9
                Settings.ShortcutSplitPrevEnable = false;
                Settings.ShortcutSplitPrevKeyCode = (int)VirtualKeyStates.Shift | (int)VirtualKeyStates.VK_F10; // Shift F10
            }
            if (Settings.Version == 3) // Coming from version 1.15
            {
                Settings.Version = 4;
                // Settings.StyleFontName is new but default value is in ctor
            }
            if (Settings.Version == 4) // Coming from version 1.16
            {
                Settings.Version = 5;
                Settings.MainWidth += 50; // added "WayHits" textbox to datagrid (50)
                Settings.MainHeight += 13 + 70; // added second line to datagrid column header(13) and "Succession" group box
                Settings.ShortcutWayHitEnable = false;
                Settings.ShortcutWayHitKeyCode = (int)VirtualKeyStates.Shift | (int)VirtualKeyStates.VK_F5; // Shift F5
                Settings.ShortcutWayHitUndoEnable = false;
                Settings.ShortcutWayHitUndoKeyCode = (int)VirtualKeyStates.Shift | (int)VirtualKeyStates.VK_F11; // Shift F11
                Settings.ShortcutPBEnable = false;
                Settings.ShortcutPBKeyCode = (int)VirtualKeyStates.Shift | (int)VirtualKeyStates.VK_F4; // Shift F4
                Settings.ShowFooter = true;
                Settings.ShowHitsCombined = true;
                Settings.ShowNumbers = true;
                Settings.ShowPB = true;
                Settings.Purpose = (int)OutModule.OM_Purpose.OM_Purpose_SplitCounter;
                Settings.Severity = (int)OutModule.OM_Severity.OM_Severity_AnyHitsCritical;
                Settings.StyleUseHighContrastNames = false;

                if (baseVersion < 0)
                {
                    // Use different hot keys when loaded without any previous settings
                    // (we don't have to take care of previous user/default settings)
                    Settings.ShortcutHitKeyCode = (int)VirtualKeyStates.Shift | (int)VirtualKeyStates.VK_F1; // Shift F1
                    Settings.ShortcutWayHitKeyCode = (int)VirtualKeyStates.Shift | (int)VirtualKeyStates.VK_F2; // Shift F2
                    Settings.ShortcutSplitKeyCode = (int)VirtualKeyStates.Shift | (int)VirtualKeyStates.VK_F3; // Shift F3
                    Settings.ShortcutPBKeyCode = (int)VirtualKeyStates.Shift | (int)VirtualKeyStates.VK_F4; // Shift F4
                    Settings.ShortcutHitUndoKeyCode = (int)VirtualKeyStates.Shift | (int)VirtualKeyStates.VK_F5; // Shift F5
                    Settings.ShortcutWayHitUndoKeyCode = (int)VirtualKeyStates.Shift | (int)VirtualKeyStates.VK_F6; // Shift F6
                    Settings.ShortcutSplitPrevKeyCode = (int)VirtualKeyStates.Shift | (int)VirtualKeyStates.VK_F7; // Shift F7
                    Settings.ShortcutResetKeyCode = (int)VirtualKeyStates.Shift | (int)VirtualKeyStates.VK_F8; // Shift F8
                }
            }
            if (Settings.Version == 5) // Coming from version 1.17
            {
                Settings.Version = 6;
                Settings.MainHeight -= 59; // "Succession" group box starts collapsed
                Settings.AlwaysOnTop = false;

                // Only enable progress bar when new settings were created
                Settings.ShowProgressBar = baseVersion < 0;
                // Introduced with true in version 5, keep user setting when this version was used
                Settings.StyleProgressBarColored = baseVersion == 5;
            }
            if (Settings.Version == 6) // Coming from version 1.18
            {
                Settings.Version = 7;
                Settings.MainWidth += 6; // added tabs (6)
                Settings.MainHeight += 27; // added tabs (27)
                Settings.MainPosX = 100; // Assume a default value (will not be applied if not on any screen later on)
                Settings.MainPosY = 100; // Assume a default value (will not be applied if not on any screen later on)
                Settings.ReadOnlyMode = false;
                Settings.StyleUseRoman = false;
                Settings.StyleHightlightCurrentSplit = false;
                // Introduced with false in version 6, keep user setting when this version was used
                Settings.StyleProgressBarColored = baseVersion != 6;
            }
            if (Settings.Version == 7) // Coming from version 1.19
            {
                Settings.Version = 8;
                Settings.CheckUpdatesOnStartup = false;

                Settings.DarkMode = IsDarkModeActive();
                // Only use latest hot key method as default when new settings were created
                if (baseVersion < 0) Settings.HotKeyMethod = (int)Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_LLKb;

                // Only enable time column when new settings were created
                Settings.ShowTimeCurrent = baseVersion < 0;
                Settings.ShowHits = true;
                Settings.ShowDiff = Settings.ShowPB; // was combined in previous versions
                Settings.ShowTimePB = false;
                Settings.ShowTimeDiff = false;
                Settings.ShowTimeFooter = false;
                Settings.ShortcutTimerStartEnable = false;
                Settings.ShortcutTimerStartKeyCode = (int)VirtualKeyStates.Shift | (int)VirtualKeyStates.VK_ADD; // Shift Add-Num
                Settings.ShortcutTimerStopEnable = false;
                Settings.ShortcutTimerStopKeyCode = (int)VirtualKeyStates.Shift | (int)VirtualKeyStates.VK_SUBTRACT; // Shift Subtract-Num
            }
            if (Settings.Version == 8) // Coming from version 1.20
            {
                Settings.Version = 9;
                Settings.ShowPBTotals = true;
                Settings.StyleDesiredHeight = 0;
                // In version 1.x value taken from StyleSuperscriptPB but got removed, so we reset to default
                Settings.StyleSubscriptPB = false;
            }
            if (Settings.Version == 9) // Coming from version 1.20 (without Autosplitter integration)
            {
                Settings.Version = 10;
                Settings.ShortcutHitBossPrevEnable = false;
                Settings.ShortcutHitBossPrevKeyCode = (int)VirtualKeyStates.None;
                Settings.ShortcutBossHitUndoPrevEnable = false;
                Settings.ShortcutBossHitUndoPrevKeyCode = (int)VirtualKeyStates.None;
                Settings.ShortcutHitWayPrevEnable = false;
                Settings.ShortcutHitWayPrevKeyCode = (int)VirtualKeyStates.None;
                Settings.ShortcutWayHitUndoPrevEnable = false;
                Settings.ShortcutWayHitUndoPrevKeyCode = (int)VirtualKeyStates.None;
                #region AutoSplitter
                Settings.ShortcutPracticeEnable = false;
                Settings.ShortcutPracticeKeyCode = (int)VirtualKeyStates.None;
                #endregion
            }
            if (Settings.Version == 10) // Coming from version 1.21
            {
                Settings.Version = 11;
                Settings.SubSplitVisibility = (int)Settings_SubSplitVisibility.Settings_SubSplitVisibility_ShowAll;
                Settings.StyleTableAlignment = "tblcenter";
                Settings.ShowDurationCurrent = false;
                Settings.ShowDurationDiff = false;
                Settings.ShowDurationPB = false;
                Settings.ShowDurationGold = false;

                // Hotkeys (introduced with AutoSplitter) will be made available in general.
                // Keep user settings unless values are not set, then change to default values.
                if (!Settings.ShortcutHitBossPrevEnable && Settings.ShortcutHitBossPrevKeyCode == (int)VirtualKeyStates.None) Settings.ShortcutHitBossPrevKeyCode = (int)VirtualKeyStates.Control | (int)VirtualKeyStates.VK_F1; // Ctrl F1
                if (!Settings.ShortcutBossHitUndoPrevEnable && Settings.ShortcutBossHitUndoPrevKeyCode == (int)VirtualKeyStates.None) Settings.ShortcutBossHitUndoPrevKeyCode = (int)VirtualKeyStates.Control | (int)VirtualKeyStates.VK_F5; // Ctrl F5
                if (!Settings.ShortcutHitWayPrevEnable && Settings.ShortcutHitWayPrevKeyCode == (int)VirtualKeyStates.None) Settings.ShortcutHitWayPrevKeyCode = (int)VirtualKeyStates.Control | (int)VirtualKeyStates.VK_F2; // Ctrl F2
                if (!Settings.ShortcutWayHitUndoPrevEnable && Settings.ShortcutWayHitUndoPrevKeyCode == (int)VirtualKeyStates.None) Settings.ShortcutWayHitUndoPrevKeyCode = (int)VirtualKeyStates.Control | (int)VirtualKeyStates.VK_F6; // Ctrl F6
            }
            /*if (Settings.Version == 11) // Coming from version x.xx
            {
                Settings.Version = 12;

            }*/

            // Load profile data..
            if (Settings.Profiles.ProfileList.Count == 0)
            {
                // There is no profile at all, initially create a clean one
                Profile unnamed = new()
                {
                    Name = "Unnamed"
                };
                Settings.Profiles.ProfileList.Add(unnamed);
            }
            else Settings.Profiles.ProfileList.Sort((a, b) => a.Name.CompareTo(b.Name)); // Sort by name
        }

        /// <summary>
        /// Stores user data in XML
        /// </summary>
        public void SaveSettings()
        {
            // Store hot keys..
            if (Statics.GlobalHotKeySupport)
            {
                ShortcutsKey key;
                Settings.HotKeyMethod = (int)sc.NextStart_Method;
                key = sc.Key_Get(SC_Type.SC_Type_Reset);
                Settings.ShortcutResetKeyCode = (int)key.KeyData;
                key = sc.Key_Get(SC_Type.SC_Type_Hit);
                Settings.ShortcutHitKeyCode = (int)key.KeyData;
                key = sc.Key_Get(SC_Type.SC_Type_HitUndo);
                Settings.ShortcutHitUndoKeyCode = (int)key.KeyData;
                key = sc.Key_Get(SC_Type.SC_Type_WayHit);
                Settings.ShortcutWayHitKeyCode = (int)key.KeyData;
                key = sc.Key_Get(SC_Type.SC_Type_WayHitUndo);
                Settings.ShortcutWayHitUndoKeyCode = (int)key.KeyData;
                key = sc.Key_Get(SC_Type.SC_Type_Split);
                Settings.ShortcutSplitKeyCode = (int)key.KeyData;
                key = sc.Key_Get(SC_Type.SC_Type_SplitPrev);
                Settings.ShortcutSplitPrevKeyCode = (int)key.KeyData;
                key = sc.Key_Get(SC_Type.SC_Type_PB);
                Settings.ShortcutPBKeyCode = (int)key.KeyData;
                key = sc.Key_Get(SC_Type.SC_Type_TimerStart);
                Settings.ShortcutTimerStartKeyCode = (int)key.KeyData;
                key = sc.Key_Get(SC_Type.SC_Type_TimerStop);
                Settings.ShortcutTimerStopKeyCode = (int)key.KeyData;
                key = sc.Key_Get(SC_Type.SC_Type_HitBossPrev);
                Settings.ShortcutHitBossPrevKeyCode = (int)key.KeyData;
                key = sc.Key_Get(SC_Type.SC_Type_BossHitUndoPrev);
                Settings.ShortcutBossHitUndoPrevKeyCode = (int)key.KeyData;
                key = sc.Key_Get(SC_Type.SC_Type_HitWayPrev);
                Settings.ShortcutHitWayPrevKeyCode = (int)key.KeyData;
                key = sc.Key_Get(SC_Type.SC_Type_WayHitUndoPrev);
                Settings.ShortcutWayHitUndoPrevKeyCode = (int)key.KeyData;
                #region AutoSplitter
                key = sc.Key_Get(SC_Type.SC_Type_Practice);
                Settings.ShortcutPracticeKeyCode = (int)key.KeyData;
                #endregion
            }

            sm.WriteXML(Settings);
        }
    }
}
