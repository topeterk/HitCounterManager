//MIT License

//Copyright (c) 2016-2020 Peter Kirmeier

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
using System.IO;
using System.Windows.Forms;

namespace HitCounterManager
{
    public partial class Settings : Form
    {
        private Shortcuts sc;
        private OutModule om;
        private SettingsRoot _settings;
        private bool IsFormLoaded = false;

        #region Form

        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            sc = ((Form1)Owner).sc;
            om = ((Form1)Owner).profCtrl.om;
            _settings = om.Settings;

            LoadHotKey(Shortcuts.SC_Type.SC_Type_Reset, cbScReset, txtReset);
            LoadHotKey(Shortcuts.SC_Type.SC_Type_Hit, cbScHit, txtHit);
            LoadHotKey(Shortcuts.SC_Type.SC_Type_HitUndo, cbScHitUndo, txtHitUndo);
            LoadHotKey(Shortcuts.SC_Type.SC_Type_WayHit, cbScWayHit, txtWayHit);
            LoadHotKey(Shortcuts.SC_Type.SC_Type_WayHitUndo, cbScWayHitUndo, txtWayHitUndo);
            LoadHotKey(Shortcuts.SC_Type.SC_Type_Split, cbScNextSplit, txtNextSplit);
            LoadHotKey(Shortcuts.SC_Type.SC_Type_SplitPrev, cbScPrevSplit, txtPrevSplit);
            LoadHotKey(Shortcuts.SC_Type.SC_Type_PB, cbScPB, txtPB);
            LoadHotKey(Shortcuts.SC_Type.SC_Type_TimerStart, cbScTimerStart, txtTimerStart);
            LoadHotKey(Shortcuts.SC_Type.SC_Type_TimerStop, cbScTimerStop, txtTimerStop);

            radioHotKeyMethod_sync.Checked = (sc.NextStart_Method == Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Sync);
            radioHotKeyMethod_async.Checked = (sc.NextStart_Method == Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Async);

            if (!sc.IsGlobalHotKeySupported)
            {
                tab_globalshortcuts.Text = tab_globalshortcuts.Text + " (OS not supported)";
                tab_globalshortcuts.Enabled = false;
            }

            // Style
            numStyleDesiredWidth.Value = _settings.StyleDesiredWidth;
            cbHighContrast.Checked = _settings.StyleUseHighContrast;
            cbHighContrastNames.Checked = _settings.StyleUseHighContrastNames;
            cbUseRoman.Checked = _settings.StyleUseRoman;
            cbHighlightCurrentSplit.Checked = _settings.StyleHightlightCurrentSplit;
            cbProgressBarColored.Checked = !_settings.StyleProgressBarColored;
            cbSuperscriptPB.Checked = _settings.StyleSuperscriptPB;
            cbApCustomCss.Checked = _settings.StyleUseCustom;
            txtCssUrl.Text = _settings.StyleCssUrl;
            txtFontUrl.Text = _settings.StyleFontUrl;
            txtFontName.Text = _settings.StyleFontName;

            // Behavior
            cbShowAttempts.Checked = _settings.ShowAttemptsCounter;
            cbShowHeadline.Checked = _settings.ShowHeadline;
            cbShowFooter.Checked = _settings.ShowFooter;
            cbShowSessionProgress.Checked = _settings.ShowSessionProgress;
            cbShowProgressBar.Checked = _settings.ShowProgressBar;
            cbSuccessionToProgressBar.Checked = _settings.Succession.IntegrateIntoProgressBar;
            numShowSplitsCountFinished.Value = _settings.ShowSplitsCountFinished;
            numShowSplitsCountUpcoming.Value = _settings.ShowSplitsCountUpcoming;
            cbShowHitsCombined.Checked = _settings.ShowHitsCombined;
            cbShowNumbers.Checked = _settings.ShowNumbers;
            cbShowPB.Checked = _settings.ShowPB;
            cbShowDiff.Checked = _settings.ShowDiff;
            cbShowTimeCurrent.Checked = _settings.ShowTimeCurrent;
            cbShowTimePB.Checked = _settings.ShowTimePB;
            cbShowTimeDiff.Checked = _settings.ShowTimeDiff;
            radioPurposeChecklist.Checked = (om.Purpose == OutModule.OM_Purpose.OM_Purpose_Checklist);
            radioPurposeDeathCounter.Checked = (om.Purpose == OutModule.OM_Purpose.OM_Purpose_DeathCounter);
            radioPurposeSplitCounter.Checked = (om.Purpose == OutModule.OM_Purpose.OM_Purpose_SplitCounter);
            radioPurposeNoDeath.Checked = (om.Purpose == OutModule.OM_Purpose.OM_Purpose_NoDeath);
            radioSeverityBossHitCritical.Checked = (om.Severity == OutModule.OM_Severity.OM_Severity_BossHitCritical);
            radioSeverityComparePB.Checked = (om.Severity == OutModule.OM_Severity.OM_Severity_ComparePB);
            radioSeverityAnyHitCritical.Checked = (om.Severity == OutModule.OM_Severity.OM_Severity_AnyHitsCritical);

            // Filepaths
            txtInput.Text = _settings.Inputfile;
            txtOutput.Text = _settings.OutputFile;

            ApplyAppearance(sender, null);
            this.UpdateDarkMode();
            IsFormLoaded = true;
        }

        #endregion
        #region Functions

        /// <summary>
        /// Reads the hot key configuration and setup the UI elements accordingly
        /// </summary>
        /// <param name="Id">Configuration type to be read</param>
        /// <param name="cb">Checkbox to show enable/disable status</param>
        /// <param name="txt">Textbox to show description string</param>
        private void LoadHotKey(Shortcuts.SC_Type Id, CheckBox cb, TextBox txt)
        {
            ShortcutsKey key = sc.Key_Get(Id);
            cb.Checked = key.used;
            txt.Text = key.GetDescriptionString();
        }

        private void SetHotKeyMethod(Shortcuts.SC_HotKeyMethod Method)
        {
            if (sc.NextStart_Method != Method) // has setting changed?
            {
                sc.NextStart_Method = Method;
                MessageBox.Show("Changes only take effect after restarting the application.", "Restart required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Registers a hot key and stores current configuration
        /// </summary>
        /// <param name="Id">Configuration type to be assigned to hot key</param>
        /// <param name="cb">Checkbox that will be checked</param>
        /// <param name="txt">Textbox to show description string</param>
        /// <param name="e">HotKey configuration</param>
        private void RegisterHotKey(Shortcuts.SC_Type Id, CheckBox cb, TextBox txt, KeyEventArgs e)
        {
            ShortcutsKey key = new ShortcutsKey();

            if (e.KeyCode == Keys.ShiftKey) return;
            if (e.KeyCode == Keys.ControlKey) return;
            if (e.KeyCode == Keys.Alt) return;
            if (e.KeyCode == Keys.Menu) return; // = Alt

            // register hotkey
            key.key = e;
            sc.Key_Set(Id, key);

            cb.Checked = true;
            txt.Text = key.GetDescriptionString();
        }

        private void ApplyAppearance(object sender, EventArgs e)
        {
            lblShowSplitCount.Text = "Current configuration will show up to " + (1 + numShowSplitsCountFinished.Value + numShowSplitsCountUpcoming.Value) + " splits.";

            if (!IsFormLoaded) return;

            // Style
            _settings.StyleUseHighContrast = cbHighContrast.Checked;
            _settings.StyleUseHighContrastNames = cbHighContrastNames.Checked;
            _settings.StyleUseRoman = cbUseRoman.Checked;
            _settings.StyleHightlightCurrentSplit = cbHighlightCurrentSplit.Checked;
            _settings.StyleProgressBarColored = !cbProgressBarColored.Checked;
            _settings.StyleSuperscriptPB = cbSuperscriptPB.Checked;
            _settings.StyleDesiredWidth = (int)numStyleDesiredWidth.Value;

            // Behavior
            _settings.ShowAttemptsCounter = cbShowAttempts.Checked;
            _settings.ShowHeadline = cbShowHeadline.Checked;
            _settings.ShowFooter = cbShowFooter.Checked;
            _settings.ShowSessionProgress = cbShowSessionProgress.Checked;
            _settings.ShowProgressBar = cbShowProgressBar.Checked;
            _settings.Succession.IntegrateIntoProgressBar = cbSuccessionToProgressBar.Checked;
            _settings.ShowSplitsCountFinished = (int)numShowSplitsCountFinished.Value;
            _settings.ShowSplitsCountUpcoming = (int)numShowSplitsCountUpcoming.Value;
            _settings.ShowHitsCombined = cbShowHitsCombined.Checked;
            _settings.ShowNumbers = cbShowNumbers.Checked;
            _settings.ShowPB = cbShowPB.Checked;
            _settings.ShowDiff = cbShowDiff.Checked;
            _settings.ShowTimeCurrent = cbShowTimeCurrent.Checked;
            _settings.ShowTimePB = cbShowTimePB.Checked;
            _settings.ShowTimeDiff = cbShowTimeDiff.Checked;
            if (radioPurposeChecklist.Checked)
                om.Purpose = OutModule.OM_Purpose.OM_Purpose_Checklist;
            else if (radioPurposeDeathCounter.Checked)
                om.Purpose = OutModule.OM_Purpose.OM_Purpose_DeathCounter;
            else if (radioPurposeNoDeath.Checked)
                om.Purpose = OutModule.OM_Purpose.OM_Purpose_NoDeath;
            else
                om.Purpose = OutModule.OM_Purpose.OM_Purpose_SplitCounter;
            if (radioSeverityBossHitCritical.Checked)
                om.Severity = OutModule.OM_Severity.OM_Severity_BossHitCritical;
            else if (radioSeverityComparePB.Checked)
                om.Severity = OutModule.OM_Severity.OM_Severity_ComparePB;
            else
                om.Severity = OutModule.OM_Severity.OM_Severity_AnyHitsCritical;

            om.Update();
        }

        private string AskForFilename(string StartFilename, string StartFilter, string Filter)
        {
            string result = null;
            if (File.Exists(StartFilename))
            {
                OpenFileDialog1.InitialDirectory = new FileInfo(StartFilename).Directory.FullName;
                OpenFileDialog1.FileName = Path.GetFileName(StartFilename);
            }
            else
            {
                OpenFileDialog1.InitialDirectory = Environment.CurrentDirectory;
                OpenFileDialog1.FileName = StartFilter;
            }

            OpenFileDialog1.Filter = Filter;
            OpenFileDialog1.FilterIndex = 0;
            if (DialogResult.OK == OpenFileDialog1.ShowDialog(this)) result = OpenFileDialog1.FileName;
            return result;
        }

        #endregion
        #region UI

        private void txtReset_KeyDown(object sender, KeyEventArgs e) { RegisterHotKey(Shortcuts.SC_Type.SC_Type_Reset, cbScReset, txtReset, e); }
        private void txtHit_KeyDown(object sender, KeyEventArgs e) { RegisterHotKey(Shortcuts.SC_Type.SC_Type_Hit, cbScHit, txtHit, e); }
        private void txtHitUndo_KeyDown(object sender, KeyEventArgs e) { RegisterHotKey(Shortcuts.SC_Type.SC_Type_HitUndo, cbScHitUndo, txtHitUndo, e); }
        private void TxtWayHit_KeyDown(object sender, KeyEventArgs e) { RegisterHotKey(Shortcuts.SC_Type.SC_Type_WayHit, cbScWayHit, txtWayHit, e); }
        private void TxtWayHitUndo_KeyDown(object sender, KeyEventArgs e) { RegisterHotKey(Shortcuts.SC_Type.SC_Type_WayHitUndo, cbScWayHitUndo, txtWayHitUndo, e); }
        private void txtNextSplit_KeyDown(object sender, KeyEventArgs e) { RegisterHotKey(Shortcuts.SC_Type.SC_Type_Split, cbScNextSplit, txtNextSplit, e); }
        private void txtPrevSplit_KeyDown(object sender, KeyEventArgs e) { RegisterHotKey(Shortcuts.SC_Type.SC_Type_SplitPrev, cbScPrevSplit, txtPrevSplit, e); }
        private void txtPB_KeyDown(object sender, KeyEventArgs e) { RegisterHotKey(Shortcuts.SC_Type.SC_Type_PB, cbScPB, txtPB, e); }
        private void txtTimerStart_KeyDown(object sender, KeyEventArgs e) { RegisterHotKey(Shortcuts.SC_Type.SC_Type_TimerStart, cbScTimerStart, txtTimerStart, e); }
        private void txtTimerStop_KeyDown(object sender, KeyEventArgs e) { RegisterHotKey(Shortcuts.SC_Type.SC_Type_TimerStop, cbScTimerStop, txtTimerStop, e); }

        private void cbScReset_CheckedChanged(object sender, EventArgs e) { sc.Key_SetState(Shortcuts.SC_Type.SC_Type_Reset, cbScReset.Checked); }
        private void cbScHit_CheckedChanged(object sender, EventArgs e) { sc.Key_SetState(Shortcuts.SC_Type.SC_Type_Hit, cbScHit.Checked); }
        private void cbScHitUndo_CheckedChanged(object sender, EventArgs e) { sc.Key_SetState(Shortcuts.SC_Type.SC_Type_HitUndo, cbScHitUndo.Checked); }
        private void CbScWayHit_CheckedChanged(object sender, EventArgs e) { sc.Key_SetState(Shortcuts.SC_Type.SC_Type_WayHit, cbScWayHit.Checked); }
        private void CbScWayHitUndo_CheckedChanged(object sender, EventArgs e) { sc.Key_SetState(Shortcuts.SC_Type.SC_Type_WayHitUndo, cbScWayHitUndo.Checked); }
        private void cbScNextSplit_CheckedChanged(object sender, EventArgs e) { sc.Key_SetState(Shortcuts.SC_Type.SC_Type_Split, cbScNextSplit.Checked); }
        private void cbScPrevSplit_CheckedChanged(object sender, EventArgs e) { sc.Key_SetState(Shortcuts.SC_Type.SC_Type_SplitPrev, cbScPrevSplit.Checked); }
        private void cbScPB_CheckedChanged(object sender, EventArgs e) { sc.Key_SetState(Shortcuts.SC_Type.SC_Type_PB, cbScPB.Checked); }
        private void cbScTimerStart_CheckedChanged(object sender, EventArgs e) { sc.Key_SetState(Shortcuts.SC_Type.SC_Type_TimerStart, cbScTimerStart.Checked); }
        private void cbScTimerStop_CheckedChanged(object sender, EventArgs e) { sc.Key_SetState(Shortcuts.SC_Type.SC_Type_TimerStop, cbScTimerStop.Checked); }

        private void btnInput_Click(object sender, EventArgs e)
        {
            string Filename = AskForFilename(txtInput.Text, "*.template", "Templates (*.template)|*.template|All files (*.*)|*.*");
            if (null != Filename)
            {
                _settings.Inputfile = txtInput.Text = Filename;
                om.ReloadFileHandles();
                om.Update();
            }
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            string Filename = AskForFilename(txtOutput.Text, "*.html", "HTML (*.html)|*.html|All files (*.*)|*.*");
            if (null != Filename)
            {
                _settings.OutputFile = txtOutput.Text = Filename;
                om.ReloadFileHandles();
                om.Update();
            }
        }

        private void radioHotKeyMethod_CheckedChanged(object sender, EventArgs e)
        {
            if (radioHotKeyMethod_sync.Checked) SetHotKeyMethod(Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Sync);
            else if (radioHotKeyMethod_async.Checked) SetHotKeyMethod(Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Async);
        }

        private void btnApApply_Click(object sender, EventArgs e)
        {
            _settings.StyleUseCustom = cbApCustomCss.Checked;
            _settings.StyleCssUrl = txtCssUrl.Text;
            _settings.StyleFontUrl = txtFontUrl.Text;
            _settings.StyleFontName = txtFontName.Text;
            ApplyAppearance(sender, e);
        }

        private void cbApCustomCss_CheckedChanged(object sender, EventArgs e)
        {
            txtCssUrl.Enabled = cbApCustomCss.Checked;
            txtFontUrl.Enabled = cbApCustomCss.Checked;
            txtFontName.Enabled = cbApCustomCss.Checked;
        }

        #endregion
    }
}
