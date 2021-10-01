//MIT License

//Copyright (c) 2016-2021 Peter Kirmeier

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
using HitCounterManager.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace HitCounterManager
{
    #region Settings types

    /// <summary>
    /// Content of XML stored user data (succession entry)
    /// </summary>
    [Serializable]
    public class SuccessionEntry
    {
        public string ProfileSelected;
    }

    /// <summary>
    /// Content of XML stored user data (succession)
    /// </summary>
    [Serializable]
    public class Succession
    {
        public int ActiveIndex = 0;
        public int Attempts = 0;
        public string HistorySplitTitle = "Previous";
        public bool HistorySplitVisible = false;
        public bool IntegrateIntoProgressBar = false;
        public List<SuccessionEntry> SuccessionList = new List<SuccessionEntry>();
    }

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
        public bool CheckUpdatesOnStartup { get; set; }
        public int HotKeyMethod;
        public bool ShortcutResetEnable;
        public int ShortcutResetKeyCode;      // Actually KeyData as it is combined with modifiers
        public bool ShortcutHitEnable;
        public int ShortcutHitKeyCode;        // Actually KeyData as it is combined with modifiers
        public bool ShortcutHitUndoEnable;
        public int ShortcutHitUndoKeyCode;    // Actually KeyData as it is combined with modifiers
        public bool ShortcutWayHitEnable;
        public int ShortcutWayHitKeyCode;     // Actually KeyData as it is combined with modifiers
        public bool ShortcutWayHitUndoEnable;
        public int ShortcutWayHitUndoKeyCode; // Actually KeyData as it is combined with modifiers
        public bool ShortcutSplitEnable;
        public int ShortcutSplitKeyCode;      // Actually KeyData as it is combined with modifiers
        public bool ShortcutSplitPrevEnable;
        public int ShortcutSplitPrevKeyCode;  // Actually KeyData as it is combined with modifiers
        public bool ShortcutPBEnable;
        public int ShortcutPBKeyCode;         // Actually KeyData as it is combined with modifiers
        public bool ShortcutTimerStartEnable;
        public int ShortcutTimerStartKeyCode; // Actually KeyData as it is combined with modifiers
        public bool ShortcutTimerStopEnable;
        public int ShortcutTimerStopKeyCode;  // Actually KeyData as it is combined with modifiers
        public string Inputfile;
        public string OutputFile;
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
        public bool ShowDiff;
        public bool ShowTimeCurrent;
        public bool ShowTimePB;
        public bool ShowTimeDiff;
        public bool ShowTimeFooter;
        public bool ShowSuccession; // obsolete since version 7 - keep for backwards compatibility (use Succession.HistorySplitVisible instead)
        public int Purpose;
        public int Severity;
        public bool StyleUseHighContrast;
        public bool StyleUseHighContrastNames;
        public bool StyleUseRoman;
        public bool StyleProgressBarColored;
        public bool StyleUseCustom;
        public string StyleCssUrl;
        public string StyleFontUrl;
        public string StyleFontName;
        public int StyleDesiredWidth;
        public bool StyleSuperscriptPB;
        public bool StyleHightlightCurrentSplit;
        public string SuccessionTitle; // obsolete since version 7 - keep for backwards compatibility (use Succession.SuccessionTitle instead)
        public int SuccessionHits;     // obsolete since version 7 - keep for backwards compatibility (will be calculated, now)
        public int SuccessionHitsWay;  // obsolete since version 7 - keep for backwards compatibility (will be calculated, now)
        public int SuccessionHitsPB;   // obsolete since version 7 - keep for backwards compatibility (will be calculated, now)
        public Succession Succession { get; set; } = new Succession();
        public string ProfileSelected; // obsolete since version 7 - keep for backwards compatibility (use Succession.SuccessionList[0].ProfileSelected instead)
        public Profiles Profiles = new Profiles();
    }

    #endregion

    public partial class App
    {
        private SaveModule<SettingsRoot> sm;

        /// <summary>
        /// Validates a hotkey and when enabled registers it
        /// </summary>
        private bool LoadHotKeySettings(Shortcuts.SC_Type Type, int KeyData, bool Enable)
        {
            ShortcutsKey key = new ShortcutsKey();
            key.key = new KeyEventArgs((Keys)KeyData);
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
        private void LoadAllHotKeySettings()
        {
            bool isKeyInvalid = false;

            if (sc.IsGlobalHotKeySupported)
            {
                sc.Initialize((Shortcuts.SC_HotKeyMethod)Settings.HotKeyMethod, PlatformLayer.ApplicationWindowHandle);

                if (!LoadHotKeySettings(Shortcuts.SC_Type.SC_Type_Reset, Settings.ShortcutResetKeyCode, Settings.ShortcutResetEnable)) isKeyInvalid = true;
                if (!LoadHotKeySettings(Shortcuts.SC_Type.SC_Type_Hit, Settings.ShortcutHitKeyCode, Settings.ShortcutHitEnable)) isKeyInvalid = true;
                if (!LoadHotKeySettings(Shortcuts.SC_Type.SC_Type_HitUndo, Settings.ShortcutHitUndoKeyCode, Settings.ShortcutHitUndoEnable)) isKeyInvalid = true;
                if (!LoadHotKeySettings(Shortcuts.SC_Type.SC_Type_WayHit, Settings.ShortcutWayHitKeyCode, Settings.ShortcutWayHitEnable)) isKeyInvalid = true;
                if (!LoadHotKeySettings(Shortcuts.SC_Type.SC_Type_WayHitUndo, Settings.ShortcutWayHitUndoKeyCode, Settings.ShortcutWayHitUndoEnable)) isKeyInvalid = true;
                if (!LoadHotKeySettings(Shortcuts.SC_Type.SC_Type_Split, Settings.ShortcutSplitKeyCode, Settings.ShortcutSplitEnable)) isKeyInvalid = true;
                if (!LoadHotKeySettings(Shortcuts.SC_Type.SC_Type_SplitPrev, Settings.ShortcutSplitPrevKeyCode, Settings.ShortcutSplitPrevEnable)) isKeyInvalid = true;
                if (!LoadHotKeySettings(Shortcuts.SC_Type.SC_Type_PB, Settings.ShortcutPBKeyCode, Settings.ShortcutPBEnable)) isKeyInvalid = true;
                if (!LoadHotKeySettings(Shortcuts.SC_Type.SC_Type_TimerStart, Settings.ShortcutTimerStartKeyCode, Settings.ShortcutTimerStartEnable)) isKeyInvalid = true;
                if (!LoadHotKeySettings(Shortcuts.SC_Type.SC_Type_TimerStop, Settings.ShortcutTimerStopKeyCode, Settings.ShortcutTimerStopEnable)) isKeyInvalid = true;
#if TODO
                if (isKeyInvalid)
                    MessageBox.Show("Not all enabled hot keys could be registered successfully!", "Error setting up hot keys!");
#else
                isKeyInvalid = !isKeyInvalid; // just to avoid compiler warning about (yet) unused variable
#endif
            }
        }


        /// <summary>
        /// Loads user data from XML
        /// </summary>
        private void LoadSettings()
        {
            bool cleanStart = true;
            int baseVersion = -1;

            sm = new SaveModule<SettingsRoot>(Statics.ApplicationName + "Save.xml");
            Settings = sm.ReadXML(true);
            if (null != Settings)
                cleanStart = false;
            else
            {
                // When no user save file is available, try loading the init file instead to provide predefined profiles and settings
                Settings = sm.ReadXML(false, Statics.ApplicationName + "Init.xml");
            }
            if (null != Settings)
                baseVersion = Settings.Version; // successfully loaded Save or Init file, so remember original version for upgrade
            else
            {
                Settings = new SettingsRoot();

                // prepare defaults..
                Settings.Version = 0;
                Settings.MainWidth = 559;
                Settings.MainHeight = 723;
                Settings.HotKeyMethod = (int)Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Async;
                Settings.ShortcutResetEnable = false;
                Settings.ShortcutResetKeyCode = 0x10000 | 0x75; // Shift F6
                Settings.ShortcutHitEnable = false;
                Settings.ShortcutHitKeyCode = 0x10000 | 0x76; // Shift F7
                Settings.ShortcutSplitEnable = false;
                Settings.ShortcutSplitKeyCode = 0x10000 | 0x77; // Shift F8
                Settings.Inputfile = "HitCounter.template";
                Settings.OutputFile = "HitCounter.html";
                Settings.ProfileSelected = "Unnamed";
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
                Settings.StyleCssUrl = "stylesheet_pink.css";
                Settings.StyleFontUrl = "https://fonts.googleapis.com/css?family=Fontdiner%20Swanky";
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
                Settings.ShortcutHitUndoKeyCode = 0x10000 | 0x78; // Shift F9
                Settings.ShortcutSplitPrevEnable = false;
                Settings.ShortcutSplitPrevKeyCode = 0x10000 | 0x79; // Shift F10
            }
            if (Settings.Version == 3) // Coming from version 1.15
            {
                Settings.Version = 4;
                Settings.StyleFontName = "Fontdiner Swanky";
            }
            if (Settings.Version == 4) // Coming from version 1.16
            {
                Settings.Version = 5;
                Settings.MainWidth += 50; // added "WayHits" textbox to datagrid (50)
                Settings.MainHeight += 13 + 70; // added second line to datagrid column header(13) and "Succession" group box
                Settings.ShortcutWayHitEnable = false;
                Settings.ShortcutWayHitKeyCode = 0x10000 | 0x74; // Shift F5
                Settings.ShortcutWayHitUndoEnable = false;
                Settings.ShortcutWayHitUndoKeyCode = 0x10000 | 0x7A; // Shift F11
                Settings.ShortcutPBEnable = false;
                Settings.ShortcutPBKeyCode = 0x10000 | 0x73; // Shift F4
                Settings.ShowFooter = true;
                Settings.ShowHitsCombined = true;
                Settings.ShowNumbers = true;
                Settings.ShowPB = true;
                Settings.Purpose = (int)OutModule.OM_Purpose.OM_Purpose_SplitCounter;
                Settings.Severity = (int)OutModule.OM_Severity.OM_Severity_AnyHitsCritical;
                Settings.StyleUseHighContrastNames = false;
                Settings.SuccessionTitle = "Predecessors";
                Settings.SuccessionHits = 0;
                Settings.SuccessionHitsWay = 0;
                Settings.SuccessionHitsPB = 0;

                if (baseVersion < 0)
                {
                    // Use different hot keys when loaded without any previous settings
                    // (we don't have to take care of previous user/default settings)
                    Settings.ShortcutHitKeyCode = 0x10000 | 0x70; // Shift F1
                    Settings.ShortcutWayHitKeyCode = 0x10000 | 0x71; // Shift F2
                    Settings.ShortcutSplitKeyCode = 0x10000 | 0x72; // Shift F3
                    Settings.ShortcutPBKeyCode = 0x10000 | 0x73; // Shift F4
                    Settings.ShortcutHitUndoKeyCode = 0x10000 | 0x74; // Shift F5
                    Settings.ShortcutWayHitUndoKeyCode = 0x10000 | 0x75; // Shift F6
                    Settings.ShortcutSplitPrevKeyCode = 0x10000 | 0x76; // Shift F7
                    Settings.ShortcutResetKeyCode = 0x10000 | 0x77; // Shift F8
                }
            }
            if (Settings.Version == 5) // Coming from version 1.17
            {
                Settings.Version = 6;
                Settings.MainHeight -= 59; // "Succession" group box starts collapsed
                Settings.AlwaysOnTop = false;

                // Only enable progress bar when new settings were created
                Settings.ShowProgressBar = (baseVersion < 0 ? true : false);
                // Introduced with true in version 5, keep user setting when this version was used
                Settings.StyleProgressBarColored = (baseVersion == 5 ? true : false);
            }
            if (Settings.Version == 6) // Coming from version 1.18
            {
                Settings.Version = 7;
                Settings.MainWidth += 6; // added tabs (6)
                Settings.MainHeight += 27; // added tabs (27)
                Settings.MainPosX = (int)PlatformLayer.ApplicationWindowPosition.X;
                Settings.MainPosY = (int)PlatformLayer.ApplicationWindowPosition.Y;
                Settings.ReadOnlyMode = false;
                Settings.StyleUseRoman = false;
                Settings.StyleHightlightCurrentSplit = false;
                // Only enable progress bar integration of succession when new settings were created
                Settings.Succession.IntegrateIntoProgressBar = (baseVersion < 0 ? true : false);
                // Create succession with only one entry (there was only one available in older versions)
                SuccessionEntry suc_entry = new SuccessionEntry();
                suc_entry.ProfileSelected = Settings.ProfileSelected;
                Settings.Succession.SuccessionList.Add(suc_entry);
                // Introduced with false in version 6, keep user setting when this version was used
                Settings.StyleProgressBarColored = (baseVersion == 6 ? false : true);
            }
            if (Settings.Version == 7) // Coming from version 1.19
            {
                Settings.Version = 8;
                Settings.CheckUpdatesOnStartup = false;

                Settings.DarkMode = OsLayer.IsDarkModeActive();
                // Only use latest hot key method as default when new settings were created
                if (baseVersion < 0) Settings.HotKeyMethod = (int)Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_LLKb;

                // Only enable time column when new settings were created
                Settings.ShowTimeCurrent = (baseVersion < 0 ? true : false);
                Settings.ShowHits = true;
                Settings.ShowDiff = Settings.ShowPB; // was combined in previous versions
                Settings.ShowTimePB = false;
                Settings.ShowTimeDiff = false;
                Settings.ShowTimeFooter = false;
                Settings.ShortcutTimerStartEnable = false;
                Settings.ShortcutTimerStartKeyCode = 0x10000 | 0x6B; // Shift Add-Num
                Settings.ShortcutTimerStopEnable = false;
                Settings.ShortcutTimerStopKeyCode = 0x10000 | 0x6D; // Shift Subtract-Num
            }
            /*if (Settings.Version == 8) // Coming from version 1.20
            {
                Settings.Version = 9;

            }*/

            // Apply settings..
            if (!cleanStart && PlatformLayer.IsTitleBarOnScreen(Settings.MainPosX, Settings.MainPosY, Settings.MainWidth))
            {
                // Set window position when application is not started the first time and window would not end up outside of all screens
                PlatformLayer.ApplicationWindowPosition = new Point(Settings.MainPosX, Settings.MainPosY);
            }
            PlatformLayer.ApplicationWindowSize = new Size(Settings.MainWidth, Settings.MainHeight);
            PlatformLayer.ApplicationWindowTopMost = Settings.AlwaysOnTop;

            Current.UserAppTheme = Settings.DarkMode ? OSAppTheme.Dark : OSAppTheme.Light; // Controls the XAML binding: {AppThemeBinding Dark=xx, Light=xx}

            // Load profile data..
            if (Settings.Profiles.ProfileList.Count == 0)
            {
                // There is no profile at all, initially create a clean one
                Profile unnamed = new Profile();
                unnamed.Name = "Unnamed";
                Settings.Profiles.ProfileList.Add(unnamed);
            }
            else Settings.Profiles.ProfileList.Sort((a, b) => a.Name.CompareTo(b.Name)); // Sort by name
            if (Settings.Succession.SuccessionList.Count == 0)
            {
                // There is no succession at all create an empty succession
                SuccessionEntry first = new SuccessionEntry();
                first.ProfileSelected = Settings.Profiles.ProfileList[0].Name;
                Settings.Succession.SuccessionList.Add(first);
            }
            if (Settings.Succession.SuccessionList.Count <= Settings.Succession.ActiveIndex) Settings.Succession.ActiveIndex = 0;

#if TODO
            profCtrl.InitializeProfilesControl(_settings.Profiles, _settings.Succession);
            profCtrl.om.Settings = _settings;
            profCtrl.om.Update(); // Write first output once after application start
#endif
        }

        /// <summary>
        /// Stores user data in XML
        /// </summary>
        public void SaveSettings()
        {
            ShortcutsKey key = new ShortcutsKey();

            // Remember window position and sates
            Point position = PlatformLayer.ApplicationWindowPosition;
            Size size = PlatformLayer.ApplicationWindowSize;
            Settings.MainWidth = (int)size.Width;
            Settings.MainHeight = (int)size.Height;
            if (PlatformLayer.IsTitleBarOnScreen((int)position.X, (int)position.Y, (int)size.Width))
            {
                // remember values when not outside of screen
                Settings.MainPosX = (int)position.X;
                Settings.MainPosY = (int)position.Y;

            }

            // Store hot keys..
            if (sc.IsGlobalHotKeySupported)
            {
                key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_Reset);
                Settings.ShortcutResetEnable = key.used; // TODO: Remove as Option is directly accessed by GUI!
                Settings.ShortcutResetKeyCode = (int)key.key.KeyData;
                key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_Hit);
                Settings.ShortcutHitEnable = key.used; // TODO: Remove as Option is directly accessed by GUI!
                Settings.ShortcutHitKeyCode = (int)key.key.KeyData;
                key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_HitUndo);
                Settings.ShortcutHitUndoEnable = key.used; // TODO: Remove as Option is directly accessed by GUI!
                Settings.ShortcutHitUndoKeyCode = (int)key.key.KeyData;
                key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_WayHit);
                Settings.ShortcutWayHitEnable = key.used; // TODO: Remove as Option is directly accessed by GUI!
                Settings.ShortcutWayHitKeyCode = (int)key.key.KeyData;
                key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_WayHitUndo);
                Settings.ShortcutWayHitUndoEnable = key.used; // TODO: Remove as Option is directly accessed by GUI!
                Settings.ShortcutWayHitUndoKeyCode = (int)key.key.KeyData;
                key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_Split);
                Settings.ShortcutSplitEnable = key.used; // TODO: Remove as Option is directly accessed by GUI!
                Settings.ShortcutSplitKeyCode = (int)key.key.KeyData;
                key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_SplitPrev);
                Settings.ShortcutSplitPrevEnable = key.used; // TODO: Remove as Option is directly accessed by GUI!
                Settings.ShortcutSplitPrevKeyCode = (int)key.key.KeyData;
                key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_PB);
                Settings.ShortcutPBEnable = key.used; // TODO: Remove as Option is directly accessed by GUI!
                Settings.ShortcutPBKeyCode = (int)key.key.KeyData;
                key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_TimerStart);
                Settings.ShortcutTimerStartEnable = key.used; // TODO: Remove as Option is directly accessed by GUI!
                Settings.ShortcutTimerStartKeyCode = (int)key.key.KeyData;
                key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_TimerStop);
                Settings.ShortcutTimerStopEnable = key.used; // TODO: Remove as Option is directly accessed by GUI!
                Settings.ShortcutTimerStopKeyCode = (int)key.key.KeyData;
            }
#if TODO
            // Store customizing..
            int TotalSplits, TotalActiveSplit, SuccessionHits, SuccessionHitsWay, SuccessionHitsPB;
            long TotalTime;
// TODO: Succession will be removed!
            profCtrl.GetCalculatedSums(out TotalSplits, out TotalActiveSplit, out SuccessionHits, out SuccessionHitsWay, out SuccessionHitsPB, out TotalTime, true);
            _settings.SuccessionHits = SuccessionHits;                                          // obsolete since version 7 - keep for backwards compatibility
            _settings.SuccessionHitsWay = SuccessionHitsWay;                                    // obsolete since version 7 - keep for backwards compatibility
            _settings.SuccessionHitsPB = SuccessionHitsPB;                                      // obsolete since version 7 - keep for backwards compatibility
            _settings.SuccessionTitle = _settings.Succession.HistorySplitTitle;                 // obsolete since version 7 - keep for backwards compatibility

            // Store profile data..
            _settings.ProfileSelected = profCtrl.SelectedProfile; // obsolete since version 7 - keep for backwards compatibility
#endif

            sm.WriteXML(Settings);
        }
    }
}
