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
        public bool ShowSessionProgress;
        public int ShowSplitsCountFinished;
        public int ShowSplitsCountUpcoming;
        public bool ShowHitsCombined;
        public bool ShowNumbers;
        public bool ShowPB;
        public int Purpose;
        public int Severity;
        public bool StyleUseHighContrast;
        public bool StyleUseHighContrastNames;
        public bool StyleUseCustom;
        public string StyleCssUrl;
        public string StyleFontUrl;
        public string StyleFontName;
        public int StyleDesiredWidth;
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
            bool bNewSettings = false;
            bool isKeyInvalid = false;

            om.DataUpdatePending = true;

            sm = new SaveModule<SettingsRoot>(Application.ProductName + "Save.xml");
            _settings = sm.ReadXML();

            if (null == _settings)
            {
                // When no user save file is available, try loading the init file instead to provide predefined profiles and settings
                _settings = sm.ReadXML(Application.ProductName + "Init.xml");
            }
            if (null == _settings)
            {
                _settings = new SettingsRoot();
                bNewSettings = true; // no settings loaded, so we create complete new one

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
            if (_settings.Version == 2) // Coming from version 1.11 to 1.14
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
                _settings.MainWidth += 50; // added "WayHits" textbox to datagrid
                _settings.MainHeight += 13; // added second line to datagrid column header
                _settings.ShortcutWayHitEnable = false;
                _settings.ShortcutWayHitKeyCode = 0x10000 | 0x74; // Shift F5
                _settings.ShortcutWayHitUndoEnable = false;
                _settings.ShortcutWayHitUndoKeyCode = 0x10000 | 0x7A; // Shift F11
                _settings.ShortcutPBEnable = false;
                _settings.ShortcutPBKeyCode = 0x10000 | 0x73; // Shift F4
                _settings.ShowHitsCombined = true;
                _settings.ShowNumbers = true;
                _settings.ShowPB = true;
                _settings.Purpose = (int)OutModule.OM_Purpose.OM_Purpose_SplitCounter;
                _settings.Severity = (int)OutModule.OM_Severity.OM_Severity_AnyHitsCritical;
                _settings.StyleUseHighContrastNames = false;
            }

            if (bNewSettings)
            {
                // Use different hot keys when loaded the first time
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

            // Apply settings..
            sc.Initialize((Shortcuts.SC_HotKeyMethod)_settings.HotKeyMethod);
            profs = _settings.Profiles;

            this.ComboBox1.Items.AddRange(profs.GetProfileList());
            if (this.ComboBox1.Items.Count == 0) this.ComboBox1.Items.Add("Unnamed");
            this.ComboBox1.SelectedItem = _settings.ProfileSelected;

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

            if (_settings.MainWidth > 400) this.Width = _settings.MainWidth;
            if (_settings.MainHeight > 400) this.Height = _settings.MainHeight;

            om.ShowAttemptsCounter = _settings.ShowAttemptsCounter;
            om.ShowHeadline = _settings.ShowHeadline;
            om.ShowSessionProgress = _settings.ShowSessionProgress;
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
            om.StyleUseCustom = _settings.StyleUseCustom;
            om.StyleCssUrl = _settings.StyleCssUrl;
            om.StyleFontUrl = _settings.StyleFontUrl;
            om.StyleFontName = _settings.StyleFontName;
            om.StyleDesiredWidth = _settings.StyleDesiredWidth;

            om.FilePathIn = _settings.Inputfile;
            om.FilePathOut = _settings.OutputFile; // setting output filepath will allow writing output, so keep this line last
            om.DataUpdatePending = false;
        }

        /// <summary>
        /// Stores user data in XML
        /// </summary>
        private void SaveSettings()
        {
            ShortcutsKey key = new ShortcutsKey();

            _settings.MainWidth = this.Width;
            _settings.MainHeight = this.Height;
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
            _settings.ShowSessionProgress = om.ShowSessionProgress;
            _settings.ShowSplitsCountFinished = om.ShowSplitsCountFinished;
            _settings.ShowSplitsCountUpcoming = om.ShowSplitsCountUpcoming;
            _settings.ShowHitsCombined = om.ShowHitsCombined;
            _settings.ShowNumbers = om.ShowNumbers;
            _settings.ShowPB = om.ShowPB;
            _settings.Purpose = (int)om.Purpose;
            _settings.Severity = (int)om.Severity;

            _settings.StyleUseHighContrast = om.StyleUseHighContrast;
            _settings.StyleUseHighContrastNames = om.StyleUseHighContrastNames;
            _settings.StyleUseCustom = om.StyleUseCustom;
            _settings.StyleCssUrl = om.StyleCssUrl;
            _settings.StyleFontUrl = om.StyleFontUrl;
            _settings.StyleFontName = om.StyleFontName;
            _settings.StyleDesiredWidth = om.StyleDesiredWidth;

            _settings.ProfileSelected = (string)ComboBox1.SelectedItem;
            _settings.Profiles = profs;
            sm.WriteXML(_settings);
        }
    }
}
