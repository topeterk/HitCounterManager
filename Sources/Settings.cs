//MIT License

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
using System.IO;
using System.Windows.Forms;

namespace HitCounterManager
{
    public partial class Settings : Form
    {
        private Shortcuts sc;
        private OutModule om = null;

        #region Form

        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            sc = ((Form1)Owner).sc;
            om = ((Form1)Owner).om;

            ShortcutsKey key;
            key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_Reset);
            cbScReset.Checked = key.used;
            if (key.valid) txtReset.Text = key.GetDescriptionString();
            key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_Hit);
            cbScHit.Checked = key.used;
            if (key.valid) txtHit.Text = key.GetDescriptionString();
            key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_HitUndo);
            cbScHitUndo.Checked = key.used;
            if (key.valid) txtHitUndo.Text = key.GetDescriptionString();
            key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_Split);
            cbScNextSplit.Checked = key.used;
            if (key.valid) txtNextSplit.Text = key.GetDescriptionString();
            key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_SplitPrev);
            cbScPrevSplit.Checked = key.used;
            if (key.valid) txtPrevSplit.Text = key.GetDescriptionString();

            txtInput.Text = om.FilePathIn;
            txtOutput.Text = om.FilePathOut;

            radioHotKeyMethod_sync.Checked = (sc.NextStart_Method == Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Sync);
            radioHotKeyMethod_async.Checked = (sc.NextStart_Method == Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Async);

            cbShowAttempts.Checked = om.ShowAttemptsCounter;
            cbShowHeadline.Checked = om.ShowHeadline;
            cbShowSessionProgress.Checked = om.ShowSessionProgress;
            numShowSplitsCountFinished.Value = om.ShowSplitsCountFinished;
            numShowSplitsCountUpcoming.Value = om.ShowSplitsCountUpcoming;
            cbApCustomCss.Checked = om.StyleUseCustom;
            txtCssUrl.Text = om.StyleCssUrl;
            txtFontUrl.Text = om.StyleFontUrl;
            txtFontName.Text = om.StyleFontName;
            numStyleDesiredWidth.Value = om.StyleDesiredWidth;
            cbApHighContrast.Checked = om.StyleUseHighContrast;

            if (!sc.IsGlobalHotKeySupported)
            {
                tab_globalshortcuts.Text = tab_globalshortcuts.Text + " (OS not supported)";
                tab_globalshortcuts.Enabled = false;
            }
        }

        #endregion
        #region Functions

        private void RegisterHotKey(TextBox txt, Shortcuts.SC_Type Id, KeyEventArgs e)
        {
            ShortcutsKey key = new ShortcutsKey();

            if (e.KeyCode == Keys.ShiftKey) return;
            if (e.KeyCode == Keys.ControlKey) return;
            if (e.KeyCode == Keys.Alt) return;
            if (e.KeyCode == Keys.Menu) return; // = Alt

            // register hotkey
            key.key = e;
            sc.Key_Set(Id, key);

            txt.Text = key.GetDescriptionString();
        }

        private void ApplyAppearance(object sender, EventArgs e)
        {
            if (null == om) return;

            om.ShowAttemptsCounter = cbShowAttempts.Checked;
            om.ShowHeadline = cbShowHeadline.Checked;
            om.ShowSessionProgress = cbShowSessionProgress.Checked;
            om.ShowSplitsCountFinished = (int)numShowSplitsCountFinished.Value;
            om.ShowSplitsCountUpcoming = (int)numShowSplitsCountUpcoming.Value;
            om.StyleUseHighContrast = cbApHighContrast.Checked;
            om.StyleDesiredWidth = (int)numStyleDesiredWidth.Value;
            om.Update(true);
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

        private void txtReset_KeyDown(object sender, KeyEventArgs e)
        {
            RegisterHotKey(txtReset, Shortcuts.SC_Type.SC_Type_Reset, e);
            cbScReset.Checked = true;
        }

        private void txtHit_KeyDown(object sender, KeyEventArgs e)
        {
            RegisterHotKey(txtHit, Shortcuts.SC_Type.SC_Type_Hit, e);
            cbScHit.Checked = true;
        }

        private void txtHitUndo_KeyDown(object sender, KeyEventArgs e)
        {
            RegisterHotKey(txtHitUndo, Shortcuts.SC_Type.SC_Type_HitUndo, e);
            cbScHitUndo.Checked = true;
        }

        private void txtNextSplit_KeyDown(object sender, KeyEventArgs e)
        {
            RegisterHotKey(txtNextSplit, Shortcuts.SC_Type.SC_Type_Split, e);
            cbScNextSplit.Checked = true;
        }

        private void txtPrevSplit_KeyDown(object sender, KeyEventArgs e)
        {
            RegisterHotKey(txtPrevSplit, Shortcuts.SC_Type.SC_Type_SplitPrev, e);
            cbScPrevSplit.Checked = true;
        }
        
        private void cbScReset_CheckedChanged(object sender, EventArgs e)
        {
            sc.Key_SetState(Shortcuts.SC_Type.SC_Type_Reset, cbScReset.Checked);
        }

        private void cbScHit_CheckedChanged(object sender, EventArgs e)
        {
            sc.Key_SetState(Shortcuts.SC_Type.SC_Type_Hit, cbScHit.Checked);
        }

        private void cbScHitUndo_CheckedChanged(object sender, EventArgs e)
        {
            sc.Key_SetState(Shortcuts.SC_Type.SC_Type_HitUndo, cbScHitUndo.Checked);
        }

        private void cbScNextSplit_CheckedChanged(object sender, EventArgs e)
        {
            sc.Key_SetState(Shortcuts.SC_Type.SC_Type_Split, cbScNextSplit.Checked);
        }

        private void cbScPrevSplit_CheckedChanged(object sender, EventArgs e)
        {
            sc.Key_SetState(Shortcuts.SC_Type.SC_Type_SplitPrev, cbScPrevSplit.Checked);
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            string Filename = AskForFilename(txtInput.Text, "*.template", "Templates (*.template)|*.template|All files (*.*)|*.*");
            if (null != Filename)
            {
                om.FilePathIn = txtInput.Text = Filename;
                om.Update(true);
            }
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            string Filename = AskForFilename(txtOutput.Text, "*.html", "HTML (*.html)|*.html|All files (*.*)|*.*");
            if (null != Filename)
            {
                om.FilePathOut = txtOutput.Text = Filename;
                om.Update(true);
            }
        }

        private void radioHotKeyMethod_CheckedChanged(object sender, EventArgs e)
        {
            if (null == sc) return; // when invoked during initialization

            if (radioHotKeyMethod_sync.Checked)
            {
                if (sc.NextStart_Method != Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Sync)
                {
                    sc.NextStart_Method = Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Sync;
                    MessageBox.Show("Changes only take effect after restarting the application.", "Restart required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (radioHotKeyMethod_async.Checked)
            {
                if (sc.NextStart_Method != Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Async)
                {
                    sc.NextStart_Method = Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Async;
                    MessageBox.Show("Changes only take effect after restarting the application.", "Restart required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnApApply_Click(object sender, EventArgs e)
        {
            om.StyleUseCustom = cbApCustomCss.Checked;
            om.StyleCssUrl = txtCssUrl.Text;
            om.StyleFontUrl = txtFontUrl.Text;
            om.StyleFontName = txtFontName.Text;
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
