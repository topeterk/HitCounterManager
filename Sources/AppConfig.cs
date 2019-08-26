//MIT License

//Copyright (c) 2016-2019 Peter Kirmeier

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
using System.Windows.Forms;

namespace HitCounterManager
{
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
        public bool AlwaysOnTop;
        public int HotKeyMethod;
        public bool ShortcutResetEnable;
        public int ShortcutResetKeyCode;
        public bool ShortcutHitEnable;
        public int ShortcutHitKeyCode;
        public bool ShortcutHitUndoEnable;
        public int ShortcutHitUndoKeyCode;
        public bool ShortcutWayHitEnable;
        public int ShortcutWayHitKeyCode;
        public bool ShortcutWayHitUndoEnable;
        public int ShortcutWayHitUndoKeyCode;
        public bool ShortcutSplitEnable;
        public int ShortcutSplitKeyCode;
        public bool ShortcutSplitPrevEnable;
        public int ShortcutSplitPrevKeyCode;
        public bool ShortcutPBEnable;
        public int ShortcutPBKeyCode;
        public string Inputfile;
        public string OutputFile;
        public bool ShowAttemptsCounter;
        public bool ShowHeadline;
        public bool ShowFooter;
        public bool ShowSessionProgress;
        public bool ShowProgressBar;
        public int ShowSplitsCountFinished;
        public int ShowSplitsCountUpcoming;
        public bool ShowHitsCombined;
        public bool ShowNumbers;
        public bool ShowPB;
        public bool ShowSuccession;
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
        public string SuccessionTitle;
        public int SuccessionHits;    // obsolete since version 7 - keep for backwards compatibility
        public int SuccessionHitsWay; // obsolete since version 7 - keep for backwards compatibility
        public int SuccessionHitsPB;  // obsolete since version 7 - keep for backwards compatibility
        public string ProfileSelected;
        public Profiles Profiles;
    }

    public partial class Form1 : Form
    {
        private SaveModule<SettingsRoot> sm;
        private SettingsRoot _settings;

        /// <summary>
        /// Validates a hotkey and when enabled registers it
        /// </summary>
        private bool LoadHotKeySettings(Shortcuts.SC_Type Type, int KeyCode, bool Enable)
        {
            ShortcutsKey key = new ShortcutsKey();
            key.key = new KeyEventArgs((Keys)KeyCode);
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
        /// Loads user data from XML
        /// </summary>
        private void LoadSettings()
        {
            int baseVersion = -1;
            bool isKeyInvalid = false;

            sm = new SaveModule<SettingsRoot>(Application.ProductName + "Save.xml");
            _settings = sm.ReadXML();

            if (null == _settings)
            {
                // When no user save file is available, try loading the init file instead to provide predefined profiles and settings
                _settings = sm.ReadXML(Application.ProductName + "Init.xml");
            }
            if (null != _settings)
                baseVersion = _settings.Version; // successfully loaded Save or Init file, so remember original version for upgrade
            else
            {
                _settings = new SettingsRoot();

                // prepare defaults..
                _settings.Version = 0;
                _settings.MainWidth = 559;
                _settings.MainHeight = 723;
                _settings.HotKeyMethod = (int)Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Async;
                _settings.ShortcutResetEnable = false;
                _settings.ShortcutResetKeyCode = 0x10000 | 0x75; // Shift F6
                _settings.ShortcutHitEnable = false;
                _settings.ShortcutHitKeyCode = 0x10000 | 0x76; // Shift F7
                _settings.ShortcutSplitEnable = false;
                _settings.ShortcutSplitKeyCode = 0x10000 | 0x77; // Shift F8
                _settings.Inputfile = "HitCounter.template";
                _settings.OutputFile = "HitCounter.html";
                _settings.ProfileSelected = "Unnamed";
                _settings.Profiles = new Profiles();
            }
            if (_settings.Version == 0) // Coming from version 1.9 or older
            {
                _settings.Version = 1;
                _settings.ShowAttemptsCounter = true;
                _settings.ShowHeadline = true;
                _settings.ShowSplitsCountFinished = 999;
                _settings.ShowSplitsCountUpcoming = 999;
                _settings.StyleUseHighContrast = false;
                _settings.StyleUseCustom = false;
                _settings.StyleCssUrl = "stylesheet_pink.css";
                _settings.StyleFontUrl = "https://fonts.googleapis.com/css?family=Fontdiner%20Swanky";
            }
            if (_settings.Version == 1) // Coming from version 1.10
            {
                _settings.Version = 2;
                _settings.MainWidth += 31; // added "SP" checkbox to datagrid
                _settings.ShowSessionProgress = true;
                _settings.StyleDesiredWidth = 0;
            }
            if (_settings.Version == 2) // Coming from version 1.11 - 1.14
            {
                _settings.Version = 3;
                _settings.ShortcutHitUndoEnable = false;
                _settings.ShortcutHitUndoKeyCode = 0x10000 | 0x78; // Shift F9
                _settings.ShortcutSplitPrevEnable = false;
                _settings.ShortcutSplitPrevKeyCode = 0x10000 | 0x79; // Shift F10
            }
            if (_settings.Version == 3) // Coming from version 1.15
            {
                _settings.Version = 4;
                _settings.StyleFontName = "Fontdiner Swanky";
            }
            if (_settings.Version == 4) // Coming from version 1.16
            {
                _settings.Version = 5;
                _settings.MainWidth += 50; // added "WayHits" textbox to datagrid (50)
                _settings.MainHeight += 13 + 70; // added second line to datagrid column header(13) and "Succession" group box
                _settings.ShortcutWayHitEnable = false;
                _settings.ShortcutWayHitKeyCode = 0x10000 | 0x74; // Shift F5
                _settings.ShortcutWayHitUndoEnable = false;
                _settings.ShortcutWayHitUndoKeyCode = 0x10000 | 0x7A; // Shift F11
                _settings.ShortcutPBEnable = false;
                _settings.ShortcutPBKeyCode = 0x10000 | 0x73; // Shift F4
                _settings.ShowFooter = true;
                _settings.ShowHitsCombined = true;
                _settings.ShowNumbers = true;
                _settings.ShowPB = true;
                _settings.Purpose = (int)OutModule.OM_Purpose.OM_Purpose_SplitCounter;
                _settings.Severity = (int)OutModule.OM_Severity.OM_Severity_AnyHitsCritical;
                _settings.StyleUseHighContrastNames = false;
                _settings.SuccessionTitle = "Predecessors";
                _settings.SuccessionHits = 0;
                _settings.SuccessionHitsWay = 0;
                _settings.SuccessionHitsPB = 0;

                if (baseVersion < 0)
                {
                    // Use different hot keys when loaded without any previous settings
                    // (we don't have to take care of previous user/default settings)
                    _settings.ShortcutHitKeyCode = 0x10000 | 0x70; // Shift F1
                    _settings.ShortcutWayHitKeyCode = 0x10000 | 0x71; // Shift F2
                    _settings.ShortcutSplitKeyCode = 0x10000 | 0x72; // Shift F3
                    _settings.ShortcutPBKeyCode = 0x10000 | 0x73; // Shift F4
                    _settings.ShortcutHitUndoKeyCode = 0x10000 | 0x74; // Shift F5
                    _settings.ShortcutWayHitUndoKeyCode = 0x10000 | 0x75; // Shift F6
                    _settings.ShortcutSplitPrevKeyCode = 0x10000 | 0x76; // Shift F7
                    _settings.ShortcutResetKeyCode = 0x10000 | 0x77; // Shift F8
                }
            }
            if (_settings.Version == 5) // Coming from version 1.17
            {
                _settings.Version = 6;
                _settings.MainHeight -= 59; // "Succession" group box starts collapsed
                _settings.AlwaysOnTop = false;

                // Only enable progress bar when new settings were created
                _settings.ShowSessionProgress = (baseVersion < 0 ? true : false);
                // Introduced with true in version 5, keep user setting when this version was used
                _settings.StyleProgressBarColored = (baseVersion == 5 ? true : false);
            }
            if (_settings.Version == 6) // Coming from version 1.18
            {
                _settings.Version = 7;
                _settings.MainWidth += 6; // added tabs (6)
                _settings.MainHeight += 27; // added tabs (27)
                _settings.MainPosX = this.Left;
                _settings.MainPosY = this.Top;
                _settings.StyleUseRoman = false;
                // Introduced with false in version 6, keep user setting when this version was used
                _settings.StyleProgressBarColored = (baseVersion == 6 ? false : true);

                if (baseVersion < 0)
                {
                    // Never seen the original default name, so we change it to most commonly used
                    _settings.SuccessionTitle = "Previous";
                }
            }

            // Apply settings..
            sc.Initialize((Shortcuts.SC_HotKeyMethod)_settings.HotKeyMethod);
            profs = _settings.Profiles;
            profs.SetProfileInfo(pi);

            tabControl1.SelectedProfileViewControl.SetProfileList(profs.GetProfileList(), _settings.ProfileSelected);

            if (!LoadHotKeySettings(Shortcuts.SC_Type.SC_Type_Reset, _settings.ShortcutResetKeyCode , _settings.ShortcutResetEnable)) isKeyInvalid = true;
            if (!LoadHotKeySettings(Shortcuts.SC_Type.SC_Type_Hit, _settings.ShortcutHitKeyCode , _settings.ShortcutHitEnable)) isKeyInvalid = true;
            if (!LoadHotKeySettings(Shortcuts.SC_Type.SC_Type_HitUndo, _settings.ShortcutHitUndoKeyCode , _settings.ShortcutHitUndoEnable)) isKeyInvalid = true;
            if (!LoadHotKeySettings(Shortcuts.SC_Type.SC_Type_WayHit, _settings.ShortcutWayHitKeyCode , _settings.ShortcutWayHitEnable)) isKeyInvalid = true;
            if (!LoadHotKeySettings(Shortcuts.SC_Type.SC_Type_WayHitUndo, _settings.ShortcutWayHitUndoKeyCode , _settings.ShortcutWayHitUndoEnable)) isKeyInvalid = true;
            if (!LoadHotKeySettings(Shortcuts.SC_Type.SC_Type_Split, _settings.ShortcutSplitKeyCode , _settings.ShortcutSplitEnable)) isKeyInvalid = true;
            if (!LoadHotKeySettings(Shortcuts.SC_Type.SC_Type_SplitPrev, _settings.ShortcutSplitPrevKeyCode , _settings.ShortcutSplitPrevEnable)) isKeyInvalid = true;
            if (!LoadHotKeySettings(Shortcuts.SC_Type.SC_Type_PB, _settings.ShortcutPBKeyCode , _settings.ShortcutPBEnable)) isKeyInvalid = true;
            if (isKeyInvalid)
                MessageBox.Show("Not all enabled hot keys could be registered successfully!", "Error setting up hot keys!");

            pi.SetSessionProgress(0, true);
            SetSuccession(_settings.SuccessionTitle, _settings.ShowSuccession);
            SuccessionChanged(null, null);

            if (_settings.MainWidth < this.MinimumSize.Width) _settings.MainWidth = this.MinimumSize.Width;
            if (_settings.MainHeight < this.MinimumSize.Height) _settings.MainHeight = this.MinimumSize.Height;
            // set window size and when possible also set location (just make sure window is not outside of screen)
            this.SetBounds(_settings.MainPosX, _settings.MainPosY, _settings.MainWidth, _settings.MainHeight,
                IsOnScreen(_settings.MainPosX, _settings.MainPosY, _settings.MainWidth, _settings.MainHeight) ? BoundsSpecified.All : BoundsSpecified.Size);
            SetAlwaysOnTop(_settings.AlwaysOnTop);

            om.ShowAttemptsCounter = _settings.ShowAttemptsCounter;
            om.ShowHeadline = _settings.ShowHeadline;
            om.ShowFooter = _settings.ShowFooter;
            om.ShowSessionProgress = _settings.ShowSessionProgress;
            om.ShowProgressBar = _settings.ShowProgressBar;
            om.ShowSplitsCountFinished = _settings.ShowSplitsCountFinished;
            om.ShowSplitsCountUpcoming = _settings.ShowSplitsCountUpcoming;
            om.ShowHitsCombined = _settings.ShowHitsCombined;
            om.ShowNumbers = _settings.ShowNumbers;
            om.ShowPB = _settings.ShowPB;
            if (_settings.Purpose < (int)OutModule.OM_Purpose.OM_Purpose_MAX)
                om.Purpose = (OutModule.OM_Purpose)_settings.Purpose;
            else
                om.Purpose = OutModule.OM_Purpose.OM_Purpose_SplitCounter;
            if (_settings.Severity < (int)OutModule.OM_Severity.OM_Severity_MAX)
                om.Severity = (OutModule.OM_Severity)_settings.Severity;
            else
                om.Severity = OutModule.OM_Severity.OM_Severity_AnyHitsCritical;

            om.StyleUseHighContrast = _settings.StyleUseHighContrast;
            om.StyleUseHighContrastNames = _settings.StyleUseHighContrastNames;
            om.StyleUseRoman = _settings.StyleUseRoman;
            om.StyleProgressBarColored = _settings.StyleProgressBarColored;
            om.StyleUseCustom = _settings.StyleUseCustom;
            om.StyleCssUrl = _settings.StyleCssUrl;
            om.StyleFontUrl = _settings.StyleFontUrl;
            om.StyleFontName = _settings.StyleFontName;
            om.StyleDesiredWidth = _settings.StyleDesiredWidth;
            om.StyleSuperscriptPB = _settings.StyleSuperscriptPB;

            om.FilePathIn = _settings.Inputfile;
            om.FilePathOut = _settings.OutputFile; // setting output filepath will allow writing output, so keep this line last
        }

        /// <summary>
        /// Stores user data in XML
        /// </summary>
        private void SaveSettings()
        {
            ShortcutsKey key = new ShortcutsKey();

            if (this.WindowState == FormWindowState.Normal) // Don't save window size and location when maximized or minimized
            {
                _settings.MainWidth = this.Width;
                _settings.MainHeight = this.Height - gpSuccession.Height + gpSuccession_Height; // always save expandend values
                if (IsOnScreen(_settings.MainPosX, _settings.MainPosY, _settings.MainWidth, _settings.MainHeight))
                {
                    // remember values when not outside of screen
                    _settings.MainPosX = this.Left;
                    _settings.MainPosY = this.Top;
                }
            }
            _settings.AlwaysOnTop = this.TopMost;
            _settings.HotKeyMethod = (int)sc.NextStart_Method;
            key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_Reset);
            _settings.ShortcutResetEnable = key.used;
            _settings.ShortcutResetKeyCode = (int)key.key.KeyData;
            key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_Hit);
            _settings.ShortcutHitEnable = key.used;
            _settings.ShortcutHitKeyCode = (int)key.key.KeyData;
            key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_HitUndo);
            _settings.ShortcutHitUndoEnable = key.used;
            _settings.ShortcutHitUndoKeyCode = (int)key.key.KeyData;
            key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_WayHit);
            _settings.ShortcutWayHitEnable = key.used;
            _settings.ShortcutWayHitKeyCode = (int)key.key.KeyData;
            key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_WayHitUndo);
            _settings.ShortcutWayHitUndoEnable = key.used;
            _settings.ShortcutWayHitUndoKeyCode = (int)key.key.KeyData;
            key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_Split);
            _settings.ShortcutSplitEnable = key.used;
            _settings.ShortcutSplitKeyCode = (int)key.key.KeyData;
            key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_SplitPrev);
            _settings.ShortcutSplitPrevEnable = key.used;
            _settings.ShortcutSplitPrevKeyCode = (int)key.key.KeyData;
            key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_PB);
            _settings.ShortcutPBEnable = key.used;
            _settings.ShortcutPBKeyCode = (int)key.key.KeyData;

            _settings.Inputfile = om.FilePathIn;
            _settings.OutputFile = om.FilePathOut;

            _settings.ShowAttemptsCounter = om.ShowAttemptsCounter;
            _settings.ShowHeadline = om.ShowHeadline;
            _settings.ShowFooter = om.ShowFooter;
            _settings.ShowSessionProgress = om.ShowSessionProgress;
            _settings.ShowProgressBar = om.ShowProgressBar;
            _settings.ShowSplitsCountFinished = om.ShowSplitsCountFinished;
            _settings.ShowSplitsCountUpcoming = om.ShowSplitsCountUpcoming;
            _settings.ShowHitsCombined = om.ShowHitsCombined;
            _settings.ShowNumbers = om.ShowNumbers;
            _settings.ShowPB = om.ShowPB;
            _settings.ShowSuccession = om.ShowSuccession;
            _settings.Purpose = (int)om.Purpose;
            _settings.Severity = (int)om.Severity;

            _settings.StyleUseHighContrast = om.StyleUseHighContrast;
            _settings.StyleUseHighContrastNames = om.StyleUseHighContrastNames;
            _settings.StyleUseRoman = om.StyleUseRoman;
            _settings.StyleProgressBarColored = om.StyleProgressBarColored;
            _settings.StyleUseCustom = om.StyleUseCustom;
            _settings.StyleCssUrl = om.StyleCssUrl;
            _settings.StyleFontUrl = om.StyleFontUrl;
            _settings.StyleFontName = om.StyleFontName;
            _settings.StyleDesiredWidth = om.StyleDesiredWidth;
            _settings.StyleSuperscriptPB = om.StyleSuperscriptPB;

            _settings.SuccessionTitle = om.SuccessionTitle;
            _settings.SuccessionHits = om.SuccessionHits;       // obsolete since version 7 - keep for backwards compatibility
            _settings.SuccessionHitsWay = om.SuccessionHitsWay; // obsolete since version 7 - keep for backwards compatibility
            _settings.SuccessionHitsPB = om.SuccessionHitsPB;   // obsolete since version 7 - keep for backwards compatibility

            _settings.ProfileSelected = tabControl1.SelectedProfileViewControl.SelectedProfile;

            profs.SaveProfile(); // Make sure all changes have been saved eventually
            _settings.Profiles = profs;

            sm.WriteXML(_settings);
        }
    }
}
