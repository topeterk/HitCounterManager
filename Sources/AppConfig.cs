﻿//MIT License

//Copyright(c) 2016-2019 Peter Kirmeier

//Permission Is hereby granted, free Of charge, to any person obtaining a copy
//of this software And associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, And/Or sell
//copies of the Software, And to permit persons to whom the Software Is
//furnished to do so, subject to the following conditions:

//The above copyright notice And this permission notice shall be included In all
//copies Or substantial portions of the Software.

//THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or
//IMPLIED, INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE And NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS Or COPYRIGHT HOLDERS BE LIABLE For ANY CLAIM, DAMAGES Or OTHER
//LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or OTHERWISE, ARISING FROM,
//OUT OF Or IN CONNECTION WITH THE SOFTWARE Or THE USE Or OTHER DEALINGS IN THE
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
        public bool StyleUseHighContrast;
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
        /// Loads user data from XML
        /// </summary>
        private void LoadSettings()
        {
            ShortcutsKey key = new ShortcutsKey();
            bool bNewSettings = false;

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
            }

            if (bNewSettings)
            {
                // Use different hot keys when loaded the first time
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

            key.key = new KeyEventArgs((Keys)_settings.ShortcutResetKeyCode);
            if (_settings.ShortcutResetEnable)
                sc.Key_Set(Shortcuts.SC_Type.SC_Type_Reset, key);
            else
                sc.Key_PreSet(Shortcuts.SC_Type.SC_Type_Reset, key);
            key.key = new KeyEventArgs((Keys)_settings.ShortcutHitKeyCode);
            if (_settings.ShortcutHitEnable)
                sc.Key_Set(Shortcuts.SC_Type.SC_Type_Hit, key);
            else
                sc.Key_PreSet(Shortcuts.SC_Type.SC_Type_Hit, key);
            key.key = new KeyEventArgs((Keys)_settings.ShortcutHitUndoKeyCode);
            if (_settings.ShortcutHitUndoEnable)
                sc.Key_Set(Shortcuts.SC_Type.SC_Type_HitUndo, key);
            else
                sc.Key_PreSet(Shortcuts.SC_Type.SC_Type_HitUndo, key);
            key.key = new KeyEventArgs((Keys)_settings.ShortcutWayHitKeyCode);
            if (_settings.ShortcutWayHitEnable)
                sc.Key_Set(Shortcuts.SC_Type.SC_Type_WayHit, key);
            else
                sc.Key_PreSet(Shortcuts.SC_Type.SC_Type_WayHit, key);
            key.key = new KeyEventArgs((Keys)_settings.ShortcutWayHitUndoKeyCode);
            if (_settings.ShortcutWayHitUndoEnable)
                sc.Key_Set(Shortcuts.SC_Type.SC_Type_WayHitUndo, key);
            else
                sc.Key_PreSet(Shortcuts.SC_Type.SC_Type_WayHitUndo, key);
            key.key = new KeyEventArgs((Keys)_settings.ShortcutSplitKeyCode);
            if (_settings.ShortcutSplitEnable)
                sc.Key_Set(Shortcuts.SC_Type.SC_Type_Split, key);
            else
                sc.Key_PreSet(Shortcuts.SC_Type.SC_Type_Split, key);
            key.key = new KeyEventArgs((Keys)_settings.ShortcutSplitPrevKeyCode);
            if (_settings.ShortcutSplitPrevEnable)
                sc.Key_Set(Shortcuts.SC_Type.SC_Type_SplitPrev, key);
            else
                sc.Key_PreSet(Shortcuts.SC_Type.SC_Type_SplitPrev, key);
            key.key = new KeyEventArgs((Keys)_settings.ShortcutPBKeyCode);
            if (_settings.ShortcutPBEnable)
                sc.Key_Set(Shortcuts.SC_Type.SC_Type_PB, key);
            else
                sc.Key_PreSet(Shortcuts.SC_Type.SC_Type_PB, key);

            pi.SetActiveSplit(0);
            pi.SetSessionProgress(0, true);

            if (_settings.MainWidth > 400) this.Width = _settings.MainWidth;
            if (_settings.MainHeight > 400) this.Height = _settings.MainHeight;

            om.ShowAttemptsCounter = _settings.ShowAttemptsCounter;
            om.ShowHeadline = _settings.ShowHeadline;
            om.ShowSessionProgress = _settings.ShowSessionProgress;
            om.ShowSplitsCountFinished = _settings.ShowSplitsCountFinished;
            om.ShowSplitsCountUpcoming = _settings.ShowSplitsCountUpcoming;
            om.StyleUseHighContrast = _settings.StyleUseHighContrast;
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
            _settings.StyleUseHighContrast = om.StyleUseHighContrast;
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
