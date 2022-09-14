//MIT License

//Copyright (c) 2022 Ezequiel Medina

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
using SoulMemory;

namespace AutoSplitterCore
{
    public partial class Debug : Form
    {
        private AutoSplitterMainModule mainModule;
        private int GameActive = 0;
        private System.Windows.Forms.Timer _update_timer = new System.Windows.Forms.Timer() { Interval = 100 };

        public Debug(AutoSplitterMainModule mainModule)
        {
            InitializeComponent();
            this.mainModule = mainModule;
            mainModule.SetPointers();
            mainModule.LoadAutoSplitterSettings(new HitCounterManager.ProfilesControl(),null);
            mainModule.InitDebug();    
            _update_timer.Tick += (sender, args) => CheckInfo();
            _update_timer.Enabled = true;
        }

        private void Debug_Load(object sender, EventArgs e)
        {
            comboBoxIGTConversion.SelectedIndex = 1;
            switch (mainModule.GetSplitterEnable())
            {
                case 1: comboBoxGame.SelectedIndex = 1; break;
                case 2: comboBoxGame.SelectedIndex = 2; break;
                case 3: comboBoxGame.SelectedIndex = 3; break;
                case 4: comboBoxGame.SelectedIndex = 4; break;
                case 5: comboBoxGame.SelectedIndex = 5; break;
                case 6: comboBoxGame.SelectedIndex = 6; break;
                case 7: comboBoxGame.SelectedIndex = 7; break;
                case 8: comboBoxGame.SelectedIndex = 8; break;
                case 9: comboBoxGame.SelectedIndex = 9; break;
                case 0:
                default: comboBoxGame.SelectedIndex = 0; break;
            }
        }
        private void comboBoxGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainModule.EnableSplitting(0);
            mainModule.EnableSplitting(comboBoxGame.SelectedIndex);
            GameActive = comboBoxGame.SelectedIndex;
            mainModule.igtModule.gameSelect = GameActive;
        }

        private void CheckInfo()
        {
            this.textBoxX.Clear();
            this.textBoxY.Clear();
            this.textBoxZ.Clear();
            this.textBoxSceneName.Clear();
            this.textBoxIGT.Clear();
            bool status = false;
            bool debugSplit = false;
            int conv = 1;
            switch (comboBoxIGTConversion.SelectedIndex)
            {
                case 0: conv = 1; break;
                case 1: conv = 1000; break;
                case 2: conv = 60000; break;
            }

            switch (GameActive)
            {
                case 1: //Sekiro
                    var Vector1 = mainModule.sekiroSplitter.getCurrentPosition();
                    this.textBoxX.Paste(Vector1.X.ToString("0.00"));
                    this.textBoxY.Paste(Vector1.Y.ToString("0.00"));
                    this.textBoxZ.Paste(Vector1.Z.ToString("0.00"));
                    status = mainModule.sekiroSplitter._StatusSekiro;
                    debugSplit = mainModule.sekiroSplitter._SplitGo;
                    break;
                case 2: //Ds1
                    var Vector2 = mainModule.ds1Splitter.getCurrentPosition();
                    this.textBoxX.Paste(Vector2.X.ToString("0.00"));
                    this.textBoxY.Paste(Vector2.Y.ToString("0.00"));
                    this.textBoxZ.Paste(Vector2.Z.ToString("0.00"));
                    status = mainModule.ds1Splitter._StatusDs1;
                    debugSplit = mainModule.ds1Splitter._SplitGo;
                    break;
                case 3: //Ds2
                    var Vector3 = mainModule.ds2Splitter.getCurrentPosition();
                    this.textBoxX.Paste(Vector3.X.ToString("0.00"));
                    this.textBoxY.Paste(Vector3.Y.ToString("0.00"));
                    this.textBoxZ.Paste(Vector3.Z.ToString("0.00"));
                    status = mainModule.ds2Splitter._StatusDs2;
                    debugSplit = mainModule.ds2Splitter._SplitGo;
                    break;
                case 4: //Ds3
                    status = mainModule.ds3Splitter._StatusDs3;
                    break;
                case 5: //Elden
                   var Vector5 = mainModule.eldenSplitter.getCurrentPosition(); 
                    this.textBoxX.Paste(Vector5.X.ToString("0.00"));
                    this.textBoxY.Paste(Vector5.Y.ToString("0.00"));
                    this.textBoxZ.Paste(Vector5.Z.ToString("0.00"));
                    status = mainModule.eldenSplitter._StatusElden;
                    debugSplit = mainModule.ds3Splitter._SplitGo;
                    break;
                case 6: //Hollow
                    var Vector6 = mainModule.hollowSplitter.getCurrentPosition();
                    this.textBoxX.Paste(Vector6.X.ToString("0.00"));
                    this.textBoxY.Paste(Vector6.Y.ToString("0.00"));
                    this.textBoxSceneName.Paste(mainModule.hollowSplitter.currentPosition.sceneName.ToString());
                    status = mainModule.hollowSplitter._StatusHollow;
                    debugSplit = mainModule.eldenSplitter._SplitGo;
                    break;
                case 7: //Celeste
                    this.textBoxSceneName.Paste(mainModule.celesteSplitter.infoPlayer.levelName.ToString());
                    status = mainModule.celesteSplitter._StatusCeleste;
                    debugSplit = mainModule.celesteSplitter._SplitGo;
                    break;
                case 8: //Cuphead
                    this.textBoxSceneName.Paste(mainModule.cupSplitter.GetSceneName());
                    status = mainModule.cupSplitter._StatusCuphead;
                    debugSplit = mainModule.cupSplitter._SplitGo;
                    break;
                case 9:
                    debugSplit = mainModule.aslSplitter._SplitGo; break;
                case 0:
                default: break;
            }
            this.textBoxIGT.Paste((mainModule.GetIgtSplitterTimer(GameActive) / conv).ToString());
            if (status) { Running.Show(); NotRunning.Hide(); } else { NotRunning.Show(); Running.Hide(); }
            if (debugSplit) { btnStatusSplitting.BackColor = System.Drawing.Color.Green; } else { btnStatusSplitting.BackColor = System.Drawing.Color.Red; }

        }

        private void btnSplitter_Click(object sender, EventArgs e)
        {
            mainModule.AutoSplitterForm(false);
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            mainModule.SaveAutoSplitterSettings();
            MessageBox.Show("Save Successfully", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRefreshGame_Click(object sender, EventArgs e)
        {
            switch (GameActive)
            {
                case 1: //Sekiro
                    mainModule.sekiroSplitter.getSekiroStatusProcess(0);
                    break;
                case 2: //Ds1
                    mainModule.ds1Splitter.getDs1StatusProcess(0);
                    break;
                case 3: //Ds2
                    mainModule.ds2Splitter.getDs2StatusProcess(0);
                    break;
                case 4: //Ds3
                    mainModule.ds3Splitter.getDs3StatusProcess(0);
                    break;
                case 5: //Elden
                    mainModule.eldenSplitter.getEldenStatusProcess(0);
                    break;
                case 6: //Hollow
                    mainModule.hollowSplitter.getHollowStatusProcess(0);
                    break;
                case 7: //Celeste
                    mainModule.celesteSplitter.getCelesteStatusProcess(0);
                    break;
                case 8: //Cuphead
                    mainModule.cupSplitter.getCupheadStatusProcess(0);
                    break;
                case 9:
                case 0:
                default: break;
            }
        }

        private void textBoxCfID_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCfID.Text != null && textBoxCfID.Text != String.Empty)
            {
                try
                {
                    var flag = uint.Parse(textBoxCfID.Text);
                    bool status = false;
                    switch (GameActive)
                    {
                        case 1: //Sekiro
                            status = mainModule.sekiroSplitter.CheckFlag(flag);
                            break;
                        case 2: //Ds1
                            status = mainModule.ds1Splitter.CheckFlag(flag);
                            break;
                        case 3: //Ds2
                            status = mainModule.ds2Splitter.CheckFlag(flag);
                            break;
                        case 4: //Ds3
                            status = mainModule.ds3Splitter.CheckFlag(flag);
                            break;
                        case 5: //Elden
                            status = mainModule.eldenSplitter.CheckFlag(flag);
                            break;
                        case 6: //Hollow
                            break;
                        case 7: //Celeste
                            break;
                        case 8: //Cuphead
                            break;
                        case 9:
                        case 0:
                        default: break;
                    }
                    if (status) { btnSplitCf.BackColor = System.Drawing.Color.Green; } else { btnSplitCf.BackColor = System.Drawing.Color.Red; }
                }
                catch (Exception)
                {
                    MessageBox.Show("Check Flag", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
