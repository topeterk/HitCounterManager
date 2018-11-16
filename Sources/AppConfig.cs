//MIT License

//Copyright(c) 2016-2018 Peter Kirmeier

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
        public bool ShortcutSplitEnable;
        public int ShortcutSplitKeyCode;
        public bool ShortcutSplitPrevEnable;
        public int ShortcutSplitPrevKeyCode;
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
                _settings.MainWidth += 31; // added new checkbox to datagrid
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

            pi.SetActiveSplit(0);
            pi.SetSessionProgress(0, true);

            if (_settings.MainWidth > 400) this.Width = _settings.MainWidth;
            if (_settings.MainHeight > 400) this.Height = _settings.MainHeight;

            om.FilePathIn = _settings.Inputfile;
            om.FilePathOut = _settings.OutputFile;
            om.ShowAttemptsCounter = _settings.ShowAttemptsCounter;
            om.ShowHeadline = _settings.ShowHeadline;
            om.ShowSessionProgress = _settings.ShowSessionProgress;
            om.ShowSplitsCountFinished = _settings.ShowSplitsCountFinished;
            om.ShowSplitsCountUpcoming = _settings.ShowSplitsCountUpcoming;
            om.StyleUseHighContrast = _settings.StyleUseHighContrast;
            om.StyleUseCustom = _settings.StyleUseCustom;
            om.StyleCssUrl = _settings.StyleCssUrl;
            om.StyleFontUrl = _settings.StyleFontUrl;
            om.StyleDesiredWidth = _settings.StyleDesiredWidth;
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
            key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_Split);
            _settings.ShortcutSplitEnable = key.used;
            _settings.ShortcutSplitKeyCode = (int)key.key.KeyData;
            key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_SplitPrev);
            _settings.ShortcutSplitPrevEnable = key.used;
            _settings.ShortcutSplitPrevKeyCode = (int)key.key.KeyData;
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
            _settings.StyleDesiredWidth = om.StyleDesiredWidth;
            _settings.ProfileSelected = (string)ComboBox1.SelectedItem;
            _settings.Profiles = profs;
            sm.WriteXML(_settings);
        }
    }
}
