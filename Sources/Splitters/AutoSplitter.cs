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
using System.Drawing;
using System.Windows.Forms;
using SoulMemory;
using System.Globalization;

namespace HitCounterManager
{
    public partial class AutoSplitter : Form
    {
        bool darkMode = false;
        SekiroSplitter sekiroSplitter;
        HollowSplitter hollowSplitter;
        EldenSplitter eldenSplitter;
        Ds3Splitter ds3Splitter;
        Ds2Splitter ds2Splitter;
        Ds1Splitter ds1Splitter;
        CelesteSplitter celesteSplitter;
        AslSplitter aslSplitter;
        CupheadSplitter cupSplitter;
        public AutoSplitter(SekiroSplitter sekiroSplitter, HollowSplitter hollowSplitter, EldenSplitter eldenSplitter, Ds3Splitter ds3Splitter, CelesteSplitter celesteSplitter, Ds2Splitter ds2Splitter, AslSplitter aslSplitter, CupheadSplitter cupSplitter, Ds1Splitter ds1Splitter, bool darkMode)
        {
            InitializeComponent();
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            this.sekiroSplitter = sekiroSplitter;
            this.hollowSplitter = hollowSplitter;
            this.eldenSplitter = eldenSplitter;
            this.ds3Splitter = ds3Splitter;
            this.ds2Splitter = ds2Splitter;
            this.ds1Splitter = ds1Splitter;
            this.celesteSplitter = celesteSplitter;
            this.aslSplitter = aslSplitter;
            this.cupSplitter = cupSplitter;
            this.darkMode = darkMode;
            refreshForm();
        }

        public void refreshForm()
        {
            #region GeneralUI
            if (darkMode)
            {
                DarkMode();
            }
            #endregion
            #region ControlTab
            TabControl2.TabPages.Clear();
            TabControl2.TabPages.Add(tabConfig);
            TabControl2.TabPages.Add(tabManual);
            #endregion
            #region SekiroTab       
            panelPositionS.Hide();
            panelBossS.Hide();
            panelCfSekiro.Hide();
            panelIdolsS.Hide();
            groupBoxAshinaOutskirts.Hide();
            groupBoxHirataEstate.Hide();
            groupBoxAshinaCastle.Hide();
            groupBoxAbandonedDungeon.Hide();
            groupBoxSenpouTemple.Hide();
            groupBoxSunkenValley.Hide();
            groupBoxAshinaDepths.Hide();
            groupBoxFountainhead.Hide();

            #endregion
            #region HollowTab
            panelBossH.Hide();
            panelItemH.Hide();
            groupBossH.Hide();
            groupBoxMBH.Hide();
            groupBoxPantheon.Hide();
            checkedListBoxPantheon.Hide();
            checkedListBoxPp.Hide();
            lbl_warning.Hide();
            groupBoxCharms.Hide();
            groupBoxSkillsH.Hide();
            panelPositionH.Hide();

            #endregion
            #region EldenTab
            panelBossER.Hide();
            panelGraceER.Hide();
            panelPositionsER.Hide();
            panelCfER.Hide();
            #endregion
            #region Ds3Tab
            panelBossDs3.Hide();
            panelBonfireDs3.Hide();
            panelLvlDs3.Hide();
            panelCfDs3.Hide();
            #endregion
            #region CelesteTab
            panelChapterCeleste.Hide();
            panelCheckpointsCeleste.Hide();
            #endregion
            #region Ds2Tab
            panelBossDS2.Hide();
            panelLvlDs2.Hide();
            panelPositionDs2.Hide();
            #endregion
            #region CupheadTab
            panelBossCuphead.Hide();
            panelLevelCuphead.Hide();
            #endregion
            #region Ds1Tab
            panelBossDs1.Hide();
            panelBonfireDs1.Hide();
            panelLvlDs1.Hide();
            panelPositionDs1.Hide();
            panelItemDs1.Hide();
            #endregion
            #region Timing
            groupBoxTSekiro.Hide();
            groupBoxTDs1.Hide();
            groupBoxTDs2.Hide();
            groupBoxTDs3.Hide();
            groupBoxTEr.Hide();
            groupBoxTHK.Hide();
            groupBoxTCeleste.Hide();
            groupBoxTCuphead.Hide();
            #endregion
            checkStatusGames();
        }

        public void DarkMode() //Is horrible but is accuareate that dark mode in main Form KEKW
        {
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.tabConfig.BackColor = Color.FromArgb(50, 50, 50);
            this.tabManual.BackColor = Color.FromArgb(50, 50, 50);
            this.TextBoxManual.BackColor = Color.DarkSlateGray;
            this.tabTiming.BackColor = Color.FromArgb(50, 50, 50);
            this.tabDs1.BackColor = Color.FromArgb(50, 50, 50);
            this.tabDs2.BackColor = Color.FromArgb(50, 50, 50);
            this.tabDs3.BackColor = Color.FromArgb(50, 50, 50);
            this.tabElden.BackColor = Color.FromArgb(50, 50, 50);
            this.tabHollow.BackColor = Color.FromArgb(50, 50, 50);
            this.tabSekiro.BackColor = Color.FromArgb(50, 50, 50);
            this.tabCeleste.BackColor = Color.FromArgb(50, 50, 50);
        }

        private void AutoSplitter_Load(object sender, EventArgs e)
        {
            DTSekiro sekiroData = sekiroSplitter.getDataSekiro();
            #region SekiroLoad.Bosses
            foreach (DefinitionsSekiro.BossS boss in sekiroData.getBossToSplit())
            {
                listBoxBosses.Items.Add(boss.Title + " - " + boss.Mode);
            }
            #endregion
            #region SekiroLoad.Idols
            foreach (DefinitionsSekiro.Idol idols in sekiroData.getidolsTosplit())
            {
                for (int i = 0; i < checkedListBoxAshina.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxAshina.Items[i].ToString())
                    {
                        checkedListBoxAshina.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxHirataEstate.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxHirataEstate.Items[i].ToString())
                    {
                        checkedListBoxHirataEstate.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxAshinaCastle.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxAshinaCastle.Items[i].ToString())
                    {
                        checkedListBoxAshinaCastle.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxAbandonedDungeon.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxAbandonedDungeon.Items[i].ToString())
                    {
                        checkedListBoxAbandonedDungeon.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxSenpouTemple.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxSenpouTemple.Items[i].ToString())
                    {
                        checkedListBoxSenpouTemple.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxSunkenValley.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxSunkenValley.Items[i].ToString())
                    {
                        checkedListBoxSunkenValley.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxAshinaDepths.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxAshinaDepths.Items[i].ToString())
                    {
                        checkedListBoxAshinaDepths.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxFountainhead.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxFountainhead.Items[i].ToString())
                    {
                        checkedListBoxFountainhead.SetItemChecked(i, true);
                    }
                }

            }
            #endregion
            #region SekiroLoad.Position
            foreach (DefinitionsSekiro.PositionS position in sekiroData.getPositionsToSplit())
            {
                listBoxPositionsS.Items.Add(position.vector.X + "; " + position.vector.Y + "; " + position.vector.Z + " - " + position.Mode);
            }
            comboBoxMarginS.SelectedIndex = sekiroData.positionMargin;
            #endregion
            #region SekiroLoad.CustomFlag
            foreach (DefinitionsSekiro.CfSk cf in sekiroData.getFlagToSplit())
            {
                listBoxCfS.Items.Add(cf.Id + " - " + cf.Mode);
            }
            #endregion
            DTHollow hollowData = hollowSplitter.getDataHollow();
            #region HollowLoad.Boss
            foreach (var b in hollowData.getBosstoSplit())
            {
                for (int i = 0; i < checkedListBoxBossH.Items.Count; i++)
                {
                    if (b.Title == checkedListBoxBossH.Items[i].ToString())
                    {
                        checkedListBoxBossH.SetItemChecked(i, true);
                    }
                }
            }
            #endregion
            #region HollowLoad.MiniBoss
            foreach (var mb in hollowData.getMiniBossToSplit())
            {
                for (int i = 0; i < checkedListBoxHMB.Items.Count; i++)
                {
                    if (mb.Title == checkedListBoxHMB.Items[i].ToString())
                    {
                        checkedListBoxHMB.SetItemChecked(i, true);
                    }
                }
            }
            #endregion
            #region HollowLoad.Pantheon
            foreach (var p in hollowData.getPhanteonToSplit())
            {
                for (int i = 0; i < checkedListBoxPantheon.Items.Count; i++)
                {
                    if (p.Title == checkedListBoxPantheon.Items[i].ToString())
                    {
                        checkedListBoxPantheon.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxPp.Items.Count; i++)
                {
                    if (p.Title == checkedListBoxPp.Items[i].ToString())
                    {
                        checkedListBoxPp.SetItemChecked(i, true);
                    }
                }

            }
            comboBoxHowP.SelectedIndex = hollowData.PantheonMode;

            #endregion
            #region HollowLoad.Charm
            foreach (var c in hollowData.getCharmToSplit())
            {
                for (int i = 0; i < checkedListBoxCharms.Items.Count; i++)
                {
                    if (c.Title == checkedListBoxCharms.Items[i].ToString())
                    {
                        checkedListBoxCharms.SetItemChecked(i, true);
                    }
                }
            }
            #endregion
            #region HollowLoad.Skill
            foreach (var c in hollowData.getSkillsToSplit())
            {
                for (int i = 0; i < checkedListBoxSkillsH.Items.Count; i++)
                {
                    if (c.Title == checkedListBoxSkillsH.Items[i].ToString())
                    {
                        checkedListBoxSkillsH.SetItemChecked(i, true);
                    }
                }
            }
            #endregion
            #region HollowLoad.Position
            foreach (var p in hollowData.getPositionToSplit())
            {
                listBoxPositionH.Items.Add(p.position + " - " + p.sceneName);
            }
            comboBoxMarginH.SelectedIndex = hollowData.positionMargin;
            #endregion
            DTElden eldenData = eldenSplitter.getDataElden();
            #region EldenLoad.Boss
            foreach (DefinitionsElden.BossER boss in eldenData.getBossToSplit())
            {
                listBoxBossER.Items.Add(boss.Title + " - " + boss.Mode);
            }
            #endregion
            #region EldenLoad.Grace
            foreach (DefinitionsElden.Grace grace in eldenData.getGraceToSplit())
            {
                listBoxGrace.Items.Add(grace.Title + " - " + grace.Mode);
            }
            #endregion
            #region EldenLoad.Positions
            foreach (DefinitionsElden.PositionER position in eldenData.getPositionToSplit())
            {
                listBoxPositionsER.Items.Add(position.vector + " - " + position.Mode);
            }
            comboBoxMarginER.SelectedIndex = eldenData.positionMargin;
            #endregion
            #region EldenLoad.CustomFlags
            foreach (DefinitionsElden.CustomFlagER cf in eldenData.getFlagsToSplit())
            {
                listBoxCfER.Items.Add(cf.Id + " - " + cf.Mode);
            }
            #endregion
            DTDs3 ds3Data = ds3Splitter.getDataDs3();
            #region Ds3Load.Boss
            foreach (DefinitionsDs3.BossDs3 boss in ds3Data.getBossToSplit())
            {
                listBoxBossDs3.Items.Add(boss.Title + " - " + boss.Mode);
            }
            #endregion
            #region Ds3Load.Bonfire
            foreach (DefinitionsDs3.BonfireDs3 bon in ds3Data.getBonfireToSplit())
            {
                listBoxBonfireDs3.Items.Add(bon.Title + " - " + bon.Mode);
            }
            #endregion
            #region Ds3Load.Lvl
            foreach (DefinitionsDs3.LvlDs3 lvl in ds3Data.getLvlToSplit())
            {
                listBoxAttributesDs3.Items.Add(lvl.Attribute + ": " + lvl.Value + " - " + lvl.Mode);
            }
            #endregion
            #region Ds3Load.CustomFlags
            foreach (DefinitionsDs3.CfDs3 cf in ds3Data.getFlagToSplit())
            {
                listBoxCfDs3.Items.Add(cf.Id + " - " + cf.Mode);
            }
            #endregion
            DTCeleste celesteData = celesteSplitter.getDataCeleste();
            #region CelesteLoad.Chapters
            foreach (var c in celesteData.getChapterToSplit())
            {
                for (int i = 0; i < checkedListBoxChapterCeleste.Items.Count; i++)
                {
                    if (c.Title == checkedListBoxChapterCeleste.Items[i].ToString())
                    {
                        checkedListBoxChapterCeleste.SetItemChecked(i, true);
                    }
                }
            }
            #endregion
            #region CelesteLoad.Checkpoints
            foreach (var c in celesteData.getChapterToSplit())
            {
                for (int i = 0; i < checkedListBoxCheckpointsCeleste.Items.Count; i++)
                {
                    if (c.Title == checkedListBoxCheckpointsCeleste.Items[i].ToString())
                    {
                        checkedListBoxCheckpointsCeleste.SetItemChecked(i, true);
                    }
                }
            }
            #endregion
            DTDs2 ds2Data = ds2Splitter.getDataDs2();
            #region Ds2Load.Boss
            foreach (DefinitionsDs2.BossDs2 boss in ds2Data.getBossToSplit())
            {
                listBoxBossDs2.Items.Add(boss.Title + " - " + boss.Mode);
            }
            #endregion
            #region Ds2Load.Lvl
            foreach (DefinitionsDs2.LvlDs2 lvl in ds2Data.getLvlToSplit())
            {
                listBoxAttributeDs2.Items.Add(lvl.Attribute + ": " + lvl.Value + " - " + lvl.Mode);
            }
            #endregion
            #region Ds2Load.Position
            foreach (DefinitionsDs2.PositionDs2 position in ds2Data.getPositionsToSplit())
            {
                listBoxPositionsDs2.Items.Add(position.vector + " - " + position.Mode);
            }
            comboBoxMarginDs2.SelectedIndex = ds2Data.positionMargin;
            #endregion
            DTCuphead cupData = cupSplitter.getDataCuphead();
            #region CupheadLoad.Boss&Level
            foreach (var c in cupData.getElementToSplit())
            {
                for (int i = 0; i < checkedListBoxBossCuphead.Items.Count; i++)
                {
                    if (c.Title == checkedListBoxBossCuphead.Items[i].ToString())
                    {
                        checkedListBoxBossCuphead.SetItemChecked(i, true);
                    }
                }
                for (int i = 0; i < checkedListLevelCuphead.Items.Count; i++)
                {
                    if (c.Title == checkedListLevelCuphead.Items[i].ToString())
                    {
                        checkedListLevelCuphead.SetItemChecked(i, true);
                    }
                }
            }
            #endregion
            DTDs1 ds1Data = ds1Splitter.getDataDs1();
            #region Ds1Load.Boss
            foreach (DefinitionsDs1.BossDs1 boss in ds1Data.getBossToSplit())
            {
                listBoxBossDs1.Items.Add(boss.Title + " - " + boss.Mode);
            }
            #endregion
            #region Ds1Load.Bonfire
            foreach (DefinitionsDs1.BonfireDs1 bon in ds1Data.getBonfireToSplit())
            {
                listBoxBonfireDs1.Items.Add(bon.Title + " - " + bon.Value + " - " + bon.Mode);
            }
            #endregion
            #region Ds1Load.Lvl
            foreach (DefinitionsDs1.LvlDs1 lvl in ds1Data.getLvlToSplit())
            {
                listBoxAttributesDs1.Items.Add(lvl.Attribute + ": " + lvl.Value + " - " + lvl.Mode);
            }
            #endregion
            #region Ds1Load.Position
            foreach (DefinitionsDs1.PositionDs1 position in ds1Data.getPositionsToSplit())
            {
                listBoxPositionsDs1.Items.Add(position.vector + " - " + position.Mode);
            }
            comboBoxMarginDs1.SelectedIndex = ds1Data.positionMargin;
            #endregion
            #region Ds1Load.Items
            foreach (DefinitionsDs1.ItemDs1 Item in ds1Data.getItemsToSplit())
            {
                listBoxItemDs1.Items.Add(Item.Title + " - " + Item.Mode);
            }
            #endregion
            #region General
            if (sekiroData.autoTimer)
            {
                checkBoxATS.Checked = true;
                groupBoxTMS.Enabled = true;
            }
            else
            {
                checkBoxATS.Checked = false;
                groupBoxTMS.Enabled = false;
            }
            if (sekiroData.gameTimer)
            {
                radioIGTSTimer.Checked = true;
                radioRealTimerS.Checked = false;
            }
            else
            {
                radioIGTSTimer.Checked = false;
                radioRealTimerS.Checked = true;
            }

            if (ds1Data.autoTimer)
            {
                checkBoxATDs1.Checked = true;
                groupBoxTMDs1.Enabled = true;
            }
            else
            {
                checkBoxATDs1.Checked = false;
                groupBoxTMDs1.Enabled = false;
            }

            if (ds1Data.gameTimer)
            {
                radioIGTDs1.Checked = true;
                radioRealTimerDs1.Checked = false;
            }
            else
            {
                radioIGTDs1.Checked = false;
                radioRealTimerDs1.Checked = true;
            }

            if (ds2Data.autoTimer)
            {
                checkBoxATDs2.Checked = true;
                groupBoxTMDs2.Enabled = true;
            }
            else
            {
                checkBoxATDs2.Checked = false;
                groupBoxTMDs2.Enabled = false;
            }

            if (ds2Data.gameTimer)
            {
                radioIGTDs2.Checked = true;
                radioRealTimerDs2.Checked = false;
            }
            else
            {
                radioIGTDs2.Checked = false;
                radioRealTimerDs2.Checked = true;
            }

            if (ds3Data.autoTimer)
            {
                checkBoxATDs3.Checked = true;
                groupBoxTMDs3.Enabled = true;
            }
            else
            {
                checkBoxATDs3.Checked = false;
                groupBoxTMDs3.Enabled = false;
            }

            if (ds3Data.gameTimer)
            {
                radioIGTDs3.Checked = true;
                radioRealTimerDs3.Checked = false;
            }
            else
            {
                radioIGTDs3.Checked = false;
                radioRealTimerDs3.Checked = true;
            }


            if (eldenData.autoTimer)
            {
                checkBoxATEr.Checked = true;
                groupBoxTMEr.Enabled = true;
            }
            else
            {
                checkBoxATEr.Checked = false;
                groupBoxTMEr.Enabled = false;
            }

            if (eldenData.gameTimer)
            {
                radioIGTEr.Checked = true;
                radioRealTimerEr.Checked = false;
            }
            else
            {
                radioIGTEr.Checked = false;
                radioRealTimerEr.Checked = true;
            }

            if (hollowData.autoTimer)
            {
                checkBoxATHollow.Checked = true;
                groupBoxTMHollow.Enabled = true;
            }
            else
            {
                checkBoxATHollow.Checked = false;
                groupBoxTMHollow.Enabled = false;
            }

            if (hollowData.gameTimer)
            {
                radioIGTHollow.Checked = true;
                radioRealTimerHollow.Checked = false;
            }
            else
            {
                radioIGTHollow.Checked = false;
                radioRealTimerHollow.Checked = true;
            }

            if (celesteData.autoTimer)
            {
                checkBoxATCeleste.Checked = true;
                groupBoxTMCeleste.Enabled = true;
            }
            else
            {
                checkBoxATCeleste.Checked = false;
                groupBoxTMCeleste.Enabled = false;
            }

            if (celesteData.gameTimer)
            {
                radioIGTCeleste.Checked = true;
                radioRealTimerCeleste.Checked = false;
            }
            else
            {
                radioIGTCeleste.Checked = false;
                radioRealTimerCeleste.Checked = true;
            }

            if (cupData.autoTimer)
            {
                checkBoxATCuphead.Checked = true;
                groupBoxTMCuphead.Enabled = true;
            }
            else
            {
                checkBoxATCuphead.Checked = false;
                groupBoxTMCuphead.Enabled = false;
            }

            if (cupData.gameTimer)
            {
                radioIGTCuphead.Checked = true;
                radioRealTimerCuphead.Checked = false;
            }
            else
            {
                radioIGTCuphead.Checked = false;
                radioRealTimerCuphead.Checked = true;
            }

            #endregion
        }

        private void refresh_Btn(object sender, EventArgs e)
        {
            checkStatusGames();
        }

        public void checkStatusGames()
        {
            if (sekiroSplitter.getSekiroStatusProcess(0))
            {
                sekiroRunning.Show();
                SekiroNotRunning.Hide();
            }
            else
            {
                SekiroNotRunning.Show();
                sekiroRunning.Hide();
            }
            if (hollowSplitter.getHollowStatusProcess(0))
            {
                HollowRunning.Show();
                HollowNotRunning.Hide();
            }
            else
            {
                HollowRunning.Hide();
                HollowNotRunning.Show();
            }
            if (eldenSplitter.getEldenStatusProcess(0))
            {
                EldenRingRunning.Show();
                EldenRingNotRunning.Hide();
            }
            else
            {
                EldenRingRunning.Hide();
                EldenRingNotRunning.Show();
            }
            if (ds3Splitter.getDs3StatusProcess(0))
            {
                Ds3Running.Show();
                Ds3NotRunning.Hide();
            }
            else
            {
                Ds3Running.Hide();
                Ds3NotRunning.Show();
            }
            if (celesteSplitter.getCelesteStatusProcess(0))
            {
                CelesteRunning.Show();
                CelesteNotRunning.Hide();
            }
            else
            {
                CelesteNotRunning.Show();
                CelesteRunning.Hide();
            }
            if (ds2Splitter.getDs2StatusProcess(0))
            {
                Ds2Running.Show();
                Ds2NotRunning.Hide();
            }
            else
            {
                Ds2NotRunning.Show();
                Ds2Running.Hide();
            }
            if (cupSplitter.getCupheadStatusProcess(0))
            {
                CupheadRunning.Show();
                CupheadNotRunning.Hide();
            }
            else
            {
                CupheadNotRunning.Show();
                CupheadRunning.Hide();
            }
            if (ds1Splitter.getDs1StatusProcess(0))
            {
                Ds1Running.Show();
                Ds1NotRunning.Hide();
            }
            else
            {
                Ds1NotRunning.Show();
                Ds1Running.Hide();
            }
        }

        #region Config UI

        private void comboBoxTGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBoxTSekiro.Hide();
            groupBoxTDs1.Hide();
            groupBoxTDs2.Hide();
            groupBoxTDs3.Hide();
            groupBoxTEr.Hide();
            groupBoxTHK.Hide();
            groupBoxTCeleste.Hide();
            groupBoxTCuphead.Hide();

            switch (comboBoxTGame.SelectedIndex)
            {
                case 0: groupBoxTSekiro.Show(); break;
                case 1: groupBoxTDs1.Show(); break;
                case 2: groupBoxTDs2.Show(); break;
                case 3: groupBoxTDs3.Show(); break;
                case 4: groupBoxTEr.Show(); break;
                case 5: groupBoxTHK.Show(); break;
                case 6: groupBoxTCeleste.Show(); break;
                case 7: groupBoxTCuphead.Show(); break;
            }
        }

        private void radioIGTSTimer_CheckedChanged(object sender, EventArgs e)
        {
            _ = radioIGTSTimer.Checked ? sekiroSplitter.dataSekiro.gameTimer = true : sekiroSplitter.dataSekiro.gameTimer = false;
        }

        private void checkBoxATS_CheckedChanged(object sender, EventArgs e)
        {
            _ = checkBoxATS.Checked ? sekiroSplitter.dataSekiro.autoTimer = true : sekiroSplitter.dataSekiro.autoTimer = false;
            _ = checkBoxATS.Checked ? groupBoxTMS.Enabled = true : groupBoxTMS.Enabled = false;
            if (!checkBoxATS.Checked) { sekiroSplitter.dataSekiro.gameTimer = false; radioIGTSTimer.Checked = false; radioRealTimerS.Checked = true; }
        }

        private void checkBoxATDs1_CheckedChanged_1(object sender, EventArgs e)
        {
            _ = checkBoxATDs1.Checked ? ds1Splitter.dataDs1.autoTimer = true : ds1Splitter.dataDs1.autoTimer = false;
            _ = checkBoxATDs1.Checked ? groupBoxTMDs1.Enabled = true : groupBoxTMDs1.Enabled = false;
            if (!checkBoxATDs1.Checked) { ds1Splitter.dataDs1.gameTimer = false; radioIGTDs1.Checked = false; radioRealTimerDs1.Checked = true; }
        }

        private void radioIGTDs1_CheckedChanged(object sender, EventArgs e)
        {
            _ = radioIGTDs1.Checked ? ds1Splitter.dataDs1.gameTimer = true : ds1Splitter.dataDs1.gameTimer = false;
        }

        private void checkBoxATDs2_CheckedChanged_1(object sender, EventArgs e)
        {
            _ = checkBoxATDs2.Checked ? ds2Splitter.dataDs2.autoTimer = true : ds2Splitter.dataDs2.autoTimer = false;
            _ = checkBoxATDs2.Checked ? groupBoxTMDs2.Enabled = true : groupBoxTMDs2.Enabled = false;
            if (!checkBoxATDs2.Checked) { ds2Splitter.dataDs2.gameTimer = false; radioIGTDs2.Checked = false; radioRealTimerDs2.Checked = true; }
        }

        private void radioIGTDs2_CheckedChanged(object sender, EventArgs e)
        {
            _ = radioIGTDs2.Checked ? ds2Splitter.dataDs2.gameTimer = true : ds2Splitter.dataDs2.gameTimer = false;
        }

        private void checkBoxATDs3_CheckedChanged_1(object sender, EventArgs e)
        {
            _ = checkBoxATDs3.Checked ? ds3Splitter.dataDs3.autoTimer = true : ds3Splitter.dataDs3.autoTimer = false;
            _ = checkBoxATDs3.Checked ? groupBoxTMDs3.Enabled = true : groupBoxTMDs3.Enabled = false;
            if (!checkBoxATDs3.Checked) { ds3Splitter.dataDs3.gameTimer = false; radioIGTDs3.Checked = false; radioRealTimerDs3.Checked = true; }
        }


        private void radioIGTDs3_CheckedChanged(object sender, EventArgs e)
        {
            _ = radioIGTDs3.Checked ? ds3Splitter.dataDs3.gameTimer = true : ds3Splitter.dataDs3.gameTimer = false;
        }

        private void checkBoxATEr_CheckedChanged_1(object sender, EventArgs e)
        {
            _ = checkBoxATEr.Checked ? eldenSplitter.dataElden.autoTimer = true : eldenSplitter.dataElden.autoTimer = false;
            _ = checkBoxATEr.Checked ? groupBoxTMEr.Enabled = true : groupBoxTMEr.Enabled = false;
            if (!checkBoxATEr.Checked) { eldenSplitter.dataElden.gameTimer = false; radioIGTEr.Checked = false; radioRealTimerEr.Checked = true; }
        }

        private void radioIGTEr_CheckedChanged(object sender, EventArgs e)
        {
            _ = radioIGTEr.Checked ? eldenSplitter.dataElden.gameTimer = true : eldenSplitter.dataElden.gameTimer = false;
        }

        private void checkBoxATCeleste_CheckedChanged_1(object sender, EventArgs e)
        {
            _ = checkBoxATCeleste.Checked ? celesteSplitter.dataCeleste.autoTimer = true : celesteSplitter.dataCeleste.autoTimer = false;
            _ = checkBoxATCeleste.Checked ? groupBoxTMCeleste.Enabled = true : groupBoxTMCeleste.Enabled = false;
            if (!checkBoxATCeleste.Checked) { celesteSplitter.dataCeleste.gameTimer = false; radioIGTCeleste.Checked = false; radioRealTimerCeleste.Checked = true; }
        }

        private void radioIGTCeleste_CheckedChanged(object sender, EventArgs e)
        {
            _ = radioIGTEr.Checked == true ? celesteSplitter.dataCeleste.gameTimer = true : celesteSplitter.dataCeleste.gameTimer = false;
        }

        private void checkBoxATCuphead_CheckedChanged_1(object sender, EventArgs e)
        {
            _ = checkBoxATCuphead.Checked ? cupSplitter.dataCuphead.autoTimer = true : cupSplitter.dataCuphead.autoTimer = false;
            _ = checkBoxATCuphead.Checked ? groupBoxTMCuphead.Enabled = true : groupBoxTMCuphead.Enabled = false;
            if (!checkBoxATCuphead.Checked) { cupSplitter.dataCuphead.gameTimer = false; radioIGTCuphead.Checked = false; radioRealTimerCuphead.Checked = true; }
        }

        private void radioIGTCuphead_CheckedChanged(object sender, EventArgs e)
        {
            _ = radioIGTEr.Checked ? cupSplitter.dataCuphead.gameTimer = true : cupSplitter.dataCuphead.gameTimer = false;
        }

        private void checkBoxATHollow_CheckedChanged(object sender, EventArgs e)
        {
            _ = checkBoxATHollow.Checked ? hollowSplitter.dataHollow.autoTimer = true : hollowSplitter.dataHollow.autoTimer = false;
            _ = checkBoxATHollow.Checked ? groupBoxTMHollow.Enabled = true : groupBoxTMHollow.Enabled = false;
            if (!checkBoxATHollow.Checked) { hollowSplitter.dataHollow.gameTimer = false; radioIGTHollow.Checked = false; radioRealTimerHollow.Checked = true; }
        }

        private void radioIGTHollow_CheckedChanged(object sender, EventArgs e)
        {
            _ = radioIGTHollow.Checked == true ? hollowSplitter.dataHollow.gameTimer = true : hollowSplitter.dataHollow.gameTimer = false;
        }

        private void btnSekiro_Click(object sender, EventArgs e)
        {
            if (!TabControl2.TabPages.Contains(tabSekiro))
            {
                TabControl2.TabPages.Add(tabSekiro);
            }
            TabControl2.SelectTab(tabSekiro);

        }

        private void btnDs1_Click(object sender, EventArgs e)
        {
            if (!TabControl2.TabPages.Contains(tabDs1))
            {
                TabControl2.TabPages.Add(tabDs1);
            }
            TabControl2.SelectTab(tabDs1);
        }

        private void btnDs2_Click(object sender, EventArgs e)
        {
            if (!TabControl2.TabPages.Contains(tabDs2))
            {
                TabControl2.TabPages.Add(tabDs2);
            }
            TabControl2.SelectTab(tabDs2);
        }

        private void btnDs3_Click(object sender, EventArgs e)
        {
            if (!TabControl2.TabPages.Contains(tabDs3))
            {
                TabControl2.TabPages.Add(tabDs3);
            }
            TabControl2.SelectTab(tabDs3);
        }

        private void btnHollow_Click(object sender, EventArgs e)
        {
            if (!TabControl2.TabPages.Contains(tabHollow))
            {
                TabControl2.TabPages.Add(tabHollow);
            }
            TabControl2.SelectTab(tabHollow);

        }

        private void btnCeleste_Click(object sender, EventArgs e)
        {
            if (!TabControl2.TabPages.Contains(tabCeleste))
            {
                TabControl2.TabPages.Add(tabCeleste);
            }
            TabControl2.SelectTab(tabCeleste);
        }

        private void btnCuphead_Click(object sender, EventArgs e)
        {
            if (!TabControl2.TabPages.Contains(tabCuphead))
            {
                TabControl2.TabPages.Add(tabCuphead);
            }
            TabControl2.SelectTab(tabCuphead);
        }

        private void btnElden_Click(object sender, EventArgs e)
        {
            if (!TabControl2.TabPages.Contains(tabElden))
            {
                TabControl2.TabPages.Add(tabElden);
            }
            TabControl2.SelectTab(tabElden);
        }
        private void btnTiming_Click(object sender, EventArgs e)
        {
            if (!TabControl2.TabPages.Contains(tabTiming))
            {
                TabControl2.TabPages.Add(tabTiming);
            }
            TabControl2.SelectTab(tabTiming);
        }
        private void btnASL_Click(object sender, EventArgs e)
        {
            Form form = new AslConfigurator(aslSplitter, darkMode);
            form.ShowDialog();
        }

        private void btnDesactiveAllTiming_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to disable everything?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                sekiroSplitter.dataSekiro.autoTimer = false;
                sekiroSplitter.dataSekiro.gameTimer = false;
                ds1Splitter.dataDs1.autoTimer = false;
                ds1Splitter.dataDs1.gameTimer = false;
                ds2Splitter.dataDs2.autoTimer = false;
                ds2Splitter.dataDs2.gameTimer = false;
                ds3Splitter.dataDs3.autoTimer = false;
                ds3Splitter.dataDs3.gameTimer = false;
                eldenSplitter.dataElden.autoTimer = false;
                eldenSplitter.dataElden.gameTimer = false;
                hollowSplitter.dataHollow.autoTimer = false;
                hollowSplitter.dataHollow.gameTimer = false;
                celesteSplitter.dataCeleste.autoTimer = false;
                celesteSplitter.dataCeleste.gameTimer = false;
                cupSplitter.dataCuphead.autoTimer = false;
                cupSplitter.dataCuphead.gameTimer = false;
                this.Controls.Clear();
                this.InitializeComponent();
                refreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
                TabControl2.TabPages.Add(tabTiming);
                TabControl2.SelectTab(tabTiming);
            }
        }

        #endregion
        #region Sekiro.UI
        private void toSplitSelectSekiro_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.panelPositionS.Hide();
            this.panelBossS.Hide();
            this.panelIdolsS.Hide();
            this.panelCfSekiro.Hide();


            switch (toSplitSelectSekiro.SelectedIndex)
            {
                case 0: //Kill a Boss
                    this.panelBossS.Show();
                    break;
                case 1: // Is Activated a Idol
                    this.panelIdolsS.Show();
                    break;
                case 2: //Target Position
                    this.panelPositionS.Show();
                    break;
                case 3: //CustomFlags
                    this.panelCfSekiro.Show(); break;
            }
        }


        private void btnGetPosition_Click(object sender, EventArgs e)
        {
            var Vector = sekiroSplitter.getCurrentPosition();
            this.textBoxX.Clear();
            this.textBoxY.Clear();
            this.textBoxZ.Clear();
            this.textBoxX.Paste(Vector.X.ToString("0.00"));
            this.textBoxY.Paste(Vector.Y.ToString("0.00"));
            this.textBoxZ.Paste(Vector.Z.ToString("0.00"));

        }

        private void btnAddPosition_Click(object sender, EventArgs e)
        {
            if (textBoxX.Text != null || textBoxY.Text != null || textBoxZ.Text != null)
            {
                if (comboBoxHowPosition.SelectedIndex == -1)
                {
                    MessageBox.Show("Select 'How' do you want split ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        var X = float.Parse(textBoxX.Text, new CultureInfo("en-US"));
                        var Y = float.Parse(textBoxY.Text, new CultureInfo("en-US"));
                        var Z = float.Parse(textBoxZ.Text, new CultureInfo("en-US"));
                        var contains1 = !listBoxPositionsS.Items.Contains(X + "; " + Y + "; " + Z + " - " + "Inmediatly");
                        var contains2 = !listBoxPositionsS.Items.Contains(X + "; " + Y + "; " + Z + " - " + "Loading game after");
                        if (contains1 && contains2)
                        {
                            if (X == 0.00 && Y == 0.00 && Z == 0.00)
                            {
                                MessageBox.Show("Dont use cords 0,0,0", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                sekiroSplitter.setProcedure(false);
                                listBoxPositionsS.Items.Add(X + "; " + Y + "; " + Z + " - " + comboBoxHowPosition.Text.ToString());
                                MessageBox.Show("Move your character to avoid autosplitting", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                sekiroSplitter.AddPosition(X, Y, Z, comboBoxHowPosition.Text.ToString());
                                sekiroSplitter.setProcedure(true);
                            }
                        }
                        else
                        {
                            MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Check Coordinates and try again", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }              
            }
            else
            {
                MessageBox.Show("Plase get/set a position ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBoxPositions_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (this.listBoxPositionsS.SelectedItem != null)
            {
                int i = listBoxPositionsS.Items.IndexOf(listBoxPositionsS.SelectedItem);
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.RemovePosition(i);
                sekiroSplitter.setProcedure(true);
                listBoxPositionsS.Items.Remove(listBoxPositionsS.SelectedItem);
            }
        }

        private void comboBoxMargin_SelectedIndexChanged(object sender, EventArgs e)
        {
            int select = comboBoxMarginS.SelectedIndex;
            sekiroSplitter.setPositionMargin(select);
        }

        private void btn_AddBoss_Click(object sender, EventArgs e)
        {         
            if (comboBoxBoss.SelectedIndex == -1 || comboBoxHowBoss.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select boss and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var contains1 = !listBoxBosses.Items.Contains(comboBoxBoss.Text.ToString() + " - " + "Inmediatly");
                var contains2 = !listBoxBosses.Items.Contains(comboBoxBoss.Text.ToString() + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    sekiroSplitter.setProcedure(false);
                    sekiroSplitter.AddBoss(comboBoxBoss.Text.ToString(), comboBoxHowBoss.Text.ToString());
                    listBoxBosses.Items.Add(comboBoxBoss.Text.ToString() + " - " + comboBoxHowBoss.Text.ToString());
                    sekiroSplitter.setProcedure(true);
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxBosses_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listBoxBosses.SelectedItem != null)
            {
                int i = listBoxBosses.Items.IndexOf(listBoxBosses.SelectedItem);
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.RemoveBoss(i);
                sekiroSplitter.setProcedure(true);
                listBoxBosses.Items.Remove(listBoxBosses.SelectedItem);
            }
        }

        private void comboBoxZoneSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBoxAshinaOutskirts.Hide();
            groupBoxHirataEstate.Hide();
            groupBoxAshinaCastle.Hide();
            groupBoxAbandonedDungeon.Hide();
            groupBoxSenpouTemple.Hide();
            groupBoxSunkenValley.Hide();
            groupBoxAshinaDepths.Hide();
            groupBoxFountainhead.Hide();

            switch (comboBoxZoneSelectS.SelectedIndex)
            {
                case 0: //Ashina Outskirts
                    groupBoxAshinaOutskirts.Show();
                    break;
                case 1: //Hirata Estate
                    groupBoxHirataEstate.Show();
                    break;
                case 2: //Ashina Castle
                    groupBoxAshinaCastle.Show();
                    break;
                case 3: //Abandoned Dungeon
                    groupBoxAbandonedDungeon.Show();
                    break;
                case 4: //Senpou Temple
                    groupBoxSenpouTemple.Show();
                    break;
                case 5: //Sunken Valley
                    groupBoxSunkenValley.Show();
                    break;
                case 6: //Ashina Depths
                    groupBoxAshinaDepths.Show();
                    break;
                case 7: //Fountainhead Palace
                    groupBoxFountainhead.Show();
                    break;
            }
        }

        private void listBoxAshinaOutskirts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAshinaOutskirts.SelectedIndex != -1)
            {
                labelIdolSelectedAO.Text = listBoxAshinaOutskirts.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxAshinaOutskirts.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmAO.Checked = false;
                    radioLagAO.Checked = true;

                }
                else
                {
                    radioImmAO.Checked = true;
                    radioLagAO.Checked = false;
                }
            }
        }

        private void listBoxHirataEstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxHirataEstate.SelectedIndex != -1)
            {
                labelIdolSelectedHE.Text = listBoxHirataEstate.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxHirataEstate.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmHE.Checked = false;
                    radioLagHE.Checked = true;

                }
                else
                {
                    radioImmHE.Checked = true;
                    radioLagHE.Checked = false;
                }
            }

        }

        private void listBoxAshinaCastle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAshinaCastle.SelectedIndex != -1)
            {
                labelIdolSelectedAC.Text = listBoxAshinaCastle.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxAshinaCastle.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmAC.Checked = false;
                    radioLagAC.Checked = true;

                }
                else
                {
                    radioImmAC.Checked = true;
                    radioLagAC.Checked = false;
                }
            }
        }

        private void listBoxAbandonedDungeon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAbandonedDungeon.SelectedIndex != -1)
            {
                labelIdolSelectedAD.Text = listBoxAbandonedDungeon.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxAbandonedDungeon.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmAD.Checked = false;
                    radioLagAD.Checked = true;

                }
                else
                {
                    radioImmAD.Checked = true;
                    radioLagAD.Checked = false;
                }
            }
        }

        private void listBoxSunkenValley_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSunkenValley.SelectedIndex != -1)
            {
                labelIdolSelectedSV.Text = listBoxSunkenValley.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxSunkenValley.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmSV.Checked = false;
                    radioLagSV.Checked = true;

                }
                else
                {
                    radioImmSV.Checked = true;
                    radioLagSV.Checked = false;
                }
            }
        }

        private void listBoxAshinaDepths_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAshinaDepths.SelectedIndex != -1)
            {
                labelIdolSelectedADe.Text = listBoxAshinaDepths.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxAshinaDepths.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmADe.Checked = false;
                    radioLagADe.Checked = true;

                }
                else
                {
                    radioImmADe.Checked = true;
                    radioLagADe.Checked = false;
                }
            }
        }

        private void listBoxSenpouTemple_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSenpouTemple.SelectedIndex != -1)
            {
                labelIdolSelectedTS.Text = listBoxSenpouTemple.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxSenpouTemple.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmTS.Checked = false;
                    radioLagTS.Checked = true;

                }
                else
                {
                    radioImmTS.Checked = true;
                    radioLagTS.Checked = false;
                }
            }
        }

        private void listBoxFountainhead_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxFountainhead.SelectedIndex != -1)
            {
                labelIdolSelectedF.Text = listBoxFountainhead.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxFountainhead.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmF.Checked = false;
                    radioLagF.Checked = true;

                }
                else
                {
                    radioImmF.Checked = true;
                    radioLagF.Checked = false;
                }
            }
        }

        private void btnAddAshinaOutskirts_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxAshinaOutskirts.SelectedIndex != -1)
            {
                if (!checkedListBoxAshina.GetItemChecked(listBoxAshinaOutskirts.SelectedIndex))
                {
                    checkedListBoxAshina.SetItemChecked(listBoxAshinaOutskirts.SelectedIndex, true);
                    sekiroSplitter.setProcedure(false);
                    if (radioImmAO.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxAshinaOutskirts.SelectedItem.ToString(), mode);
                    sekiroSplitter.setProcedure(true);
                }
                else
                {
                    checkedListBoxAshina.SetItemChecked(listBoxAshinaOutskirts.SelectedIndex, false);
                    sekiroSplitter.setProcedure(false);
                    sekiroSplitter.RemoveIdol(listBoxAshinaOutskirts.SelectedItem.ToString());
                    sekiroSplitter.setProcedure(true);
                    radioImmAO.Checked = true;
                    radioLagAO.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddHirata_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxHirataEstate.SelectedIndex != -1)
            {
                if (!checkedListBoxHirataEstate.GetItemChecked(listBoxHirataEstate.SelectedIndex))
                {
                    checkedListBoxHirataEstate.SetItemChecked(listBoxHirataEstate.SelectedIndex, true);
                    sekiroSplitter.setProcedure(false);
                    if (radioImmHE.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxHirataEstate.SelectedItem.ToString(), mode);
                    sekiroSplitter.setProcedure(true);
                }
                else
                {
                    checkedListBoxHirataEstate.SetItemChecked(listBoxHirataEstate.SelectedIndex, false);
                    sekiroSplitter.setProcedure(false);
                    sekiroSplitter.RemoveIdol(listBoxHirataEstate.SelectedItem.ToString());
                    sekiroSplitter.setProcedure(true);
                    radioImmHE.Checked = true;
                    radioLagHE.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddAC_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxAshinaCastle.SelectedIndex != -1)
            {
                if (!checkedListBoxAshinaCastle.GetItemChecked(listBoxAshinaCastle.SelectedIndex))
                {
                    checkedListBoxAshinaCastle.SetItemChecked(listBoxAshinaCastle.SelectedIndex, true);
                    sekiroSplitter.setProcedure(false);
                    if (radioImmAC.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxAshinaCastle.SelectedItem.ToString(), mode);
                    sekiroSplitter.setProcedure(true);
                }
                else
                {
                    checkedListBoxAshinaCastle.SetItemChecked(listBoxAshinaCastle.SelectedIndex, false);
                    sekiroSplitter.setProcedure(false);
                    sekiroSplitter.RemoveIdol(listBoxAshinaCastle.SelectedItem.ToString());
                    sekiroSplitter.setProcedure(true);
                    radioImmAC.Checked = true;
                    radioLagAC.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddAD_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxAbandonedDungeon.SelectedIndex != -1)
            {
                if (!checkedListBoxAbandonedDungeon.GetItemChecked(listBoxAbandonedDungeon.SelectedIndex))
                {
                    checkedListBoxAbandonedDungeon.SetItemChecked(listBoxAbandonedDungeon.SelectedIndex, true);
                    sekiroSplitter.setProcedure(false);
                    if (radioImmAC.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxAbandonedDungeon.SelectedItem.ToString(), mode);
                    sekiroSplitter.setProcedure(true);
                }
                else
                {
                    checkedListBoxAbandonedDungeon.SetItemChecked(listBoxAbandonedDungeon.SelectedIndex, false);
                    sekiroSplitter.setProcedure(false);
                    sekiroSplitter.RemoveIdol(listBoxAbandonedDungeon.SelectedItem.ToString());
                    sekiroSplitter.setProcedure(true);
                    radioImmAC.Checked = true;
                    radioLagAC.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddTS_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxSenpouTemple.SelectedIndex != -1)
            {
                if (!checkedListBoxSenpouTemple.GetItemChecked(listBoxSenpouTemple.SelectedIndex))
                {
                    checkedListBoxSenpouTemple.SetItemChecked(listBoxSenpouTemple.SelectedIndex, true);
                    sekiroSplitter.setProcedure(false);
                    if (radioImmTS.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxSenpouTemple.SelectedItem.ToString(), mode);
                    sekiroSplitter.setProcedure(true);
                }
                else
                {
                    checkedListBoxSenpouTemple.SetItemChecked(listBoxSenpouTemple.SelectedIndex, false);
                    sekiroSplitter.setProcedure(false);
                    sekiroSplitter.RemoveIdol(listBoxSenpouTemple.SelectedItem.ToString());
                    sekiroSplitter.setProcedure(true);
                    radioImmTS.Checked = true;
                    radioLagTS.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddSV_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxSunkenValley.SelectedIndex != -1)
            {
                if (!checkedListBoxSunkenValley.GetItemChecked(listBoxSunkenValley.SelectedIndex))
                {
                    checkedListBoxSunkenValley.SetItemChecked(listBoxSunkenValley.SelectedIndex, true);
                    sekiroSplitter.setProcedure(false);
                    if (radioImmTS.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxSunkenValley.SelectedItem.ToString(), mode);
                    sekiroSplitter.setProcedure(true);
                }
                else
                {
                    checkedListBoxSunkenValley.SetItemChecked(listBoxSunkenValley.SelectedIndex, false);
                    sekiroSplitter.setProcedure(false);
                    sekiroSplitter.RemoveIdol(listBoxSunkenValley.SelectedItem.ToString());
                    sekiroSplitter.setProcedure(true);
                    radioImmTS.Checked = true;
                    radioLagTS.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddADe_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxAshinaDepths.SelectedIndex != -1)
            {
                if (!checkedListBoxAshinaDepths.GetItemChecked(listBoxAshinaDepths.SelectedIndex))
                {
                    checkedListBoxAshinaDepths.SetItemChecked(listBoxAshinaDepths.SelectedIndex, true);
                    sekiroSplitter.setProcedure(false);
                    if (radioImmTS.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxAshinaDepths.SelectedItem.ToString(), mode);
                    sekiroSplitter.setProcedure(true);
                }
                else
                {
                    checkedListBoxAshinaDepths.SetItemChecked(listBoxAshinaDepths.SelectedIndex, false);
                    sekiroSplitter.setProcedure(false);
                    sekiroSplitter.RemoveIdol(listBoxAshinaDepths.SelectedItem.ToString());
                    sekiroSplitter.setProcedure(true);
                    radioImmTS.Checked = true;
                    radioLagTS.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddF_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxFountainhead.SelectedIndex != -1)
            {
                if (!checkedListBoxFountainhead.GetItemChecked(listBoxFountainhead.SelectedIndex))
                {
                    checkedListBoxFountainhead.SetItemChecked(listBoxFountainhead.SelectedIndex, true);
                    sekiroSplitter.setProcedure(false);
                    if (radioImmF.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxFountainhead.SelectedItem.ToString(), mode);
                    sekiroSplitter.setProcedure(true);
                }
                else
                {
                    checkedListBoxFountainhead.SetItemChecked(listBoxFountainhead.SelectedIndex, false);
                    sekiroSplitter.setProcedure(false);
                    sekiroSplitter.RemoveIdol(listBoxFountainhead.SelectedItem.ToString());
                    sekiroSplitter.setProcedure(true);
                    radioImmF.Checked = true;
                    radioLagF.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGetListFlagsSekiro_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://docs.google.com/spreadsheets/d/1Nwp6XwURGksUu-_jCVhcyXh4KH7hTCXYsJTCHbw87JQ/edit?usp=sharing");
        }

        private void btnAddCfS_Click(object sender, EventArgs e)
        {
            
            if (textBoxCfIdS.Text == null || comboBoxHowCfS.SelectedIndex == -1)
            {
                MessageBox.Show("Plase set a ID and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    var id = uint.Parse(textBoxCfIdS.Text);
                    var contains1 = !listBoxCfS.Items.Contains(id + " - " + "Inmediatly");
                    var contains2 = !listBoxCfS.Items.Contains(id + " - " + "Loading game after");
                    if (contains1 && contains2)
                    {
                        sekiroSplitter.setProcedure(false);
                        sekiroSplitter.AddCustomFlag(id, comboBoxHowCfS.Text.ToString());
                        listBoxCfS.Items.Add(id + " - " + comboBoxHowCfS.Text.ToString());
                        sekiroSplitter.setProcedure(true);
                    }
                    else
                    {
                        MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Wrong ID", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void listBoxCfS_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxCfS.SelectedItem != null)
            {
                int i = listBoxCfS.Items.IndexOf(listBoxCfS.SelectedItem);
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.RemoveCustomFlag(i);
                sekiroSplitter.setProcedure(true);
                listBoxCfS.Items.Remove(listBoxCfS.SelectedItem);
            }
        }
        private void btnDesactiveSekiro_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to disable everything?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.clearData();
                sekiroSplitter.setProcedure(true);
                this.Controls.Clear();
                this.InitializeComponent();
                refreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
                TabControl2.TabPages.Add(tabSekiro);
                TabControl2.SelectTab(tabSekiro);
            }

        }

        #endregion
        #region Hollow UI
        private void toSplitSelectHollow_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelBossH.Hide();
            panelItemH.Hide();
            panelPositionH.Hide();

            switch (toSplitSelectHollow.SelectedIndex)
            {
                case 0: //Kill a enemy
                    panelBossH.Show();
                    break;
                case 1: //Obtain a item
                    panelItemH.Show();
                    break;
                case 2: //Get Position
                    panelPositionH.Show();
                    break;
            }
        }

        private void comboBoxSelectKindBoss_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBossH.Hide();
            groupBoxMBH.Hide();
            groupBoxPantheon.Hide();

            switch (comboBoxSelectKindBoss.SelectedIndex)
            {
                case 0: //Boss
                    groupBossH.Show();
                    break;
                case 1: //Phanteom
                    groupBoxPantheon.Show();
                    break;
                case 2: //MiniBoss - Dreamers- Coliseum and Others
                    groupBoxMBH.Show();
                    break;
            }
        }

        private void checkedListBoxBossH_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxBossH.SelectedIndex != -1)
            {
                if (checkedListBoxBossH.GetItemChecked(checkedListBoxBossH.SelectedIndex) == false)
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.AddBoss(checkedListBoxBossH.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
                else
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.RemoveBoss(checkedListBoxBossH.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
            }
        }

        private void checkedListBoxMBH_ItemCheck_1(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxHMB.SelectedIndex != -1)
            {
                if (checkedListBoxHMB.GetItemChecked(checkedListBoxHMB.SelectedIndex) == false)
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.AddMiniBoss(checkedListBoxHMB.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
                else
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.RemoveMiniBoss(checkedListBoxHMB.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
            }
        }

        private void checkedListBoxPantheon_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxPantheon.SelectedIndex != -1)
            {
                if (checkedListBoxPantheon.GetItemChecked(checkedListBoxPantheon.SelectedIndex) == false)
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.AddPantheon(checkedListBoxPantheon.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
                else
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.RemovePantheon(checkedListBoxPantheon.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
            }
        }

        private void checkedListBoxPp_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxPp.SelectedIndex != -1)
            {
                if (checkedListBoxPp.GetItemChecked(checkedListBoxPp.SelectedIndex) == false)
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.AddPantheon(checkedListBoxPp.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
                else
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.RemovePantheon(checkedListBoxPp.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
            }
        }


        private void checkedListBoxCharms_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxCharms.SelectedIndex != -1)
            {
                if (checkedListBoxCharms.GetItemChecked(checkedListBoxCharms.SelectedIndex) == false)
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.AddCharm(checkedListBoxCharms.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
                else
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.RemoveCharm(checkedListBoxCharms.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
            }
        }

        private void checkedListBoxSkillsH_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxSkillsH.SelectedIndex != -1)
            {
                if (checkedListBoxSkillsH.GetItemChecked(checkedListBoxSkillsH.SelectedIndex) == false)
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.AddSkill(checkedListBoxSkillsH.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
                else
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.RemoveSkill(checkedListBoxSkillsH.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
            }
        }

        private void comboBoxHowP_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBoxPantheon.Hide();
            checkedListBoxPp.Hide();
            lbl_warning.Hide();

            if (comboBoxHowP.SelectedIndex != -1)
            {
                switch (comboBoxHowP.SelectedIndex)
                {
                    case 0: //P1+P2+P3+P4 or P5                       
                        checkedListBoxPantheon.Show();
                        lbl_warning.Show();
                        hollowSplitter.dataHollow.PantheonMode = 0;
                        break;
                    case 1: //Split one per Pantheon
                        checkedListBoxPp.Show();
                        hollowSplitter.dataHollow.PantheonMode = 1;
                        break;
                }
            }
        }

        private void comboBoxItemSelectH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxItemSelectH.SelectedIndex != -1)
            {
                groupBoxCharms.Hide();
                groupBoxSkillsH.Hide();
                switch (comboBoxItemSelectH.SelectedIndex)
                {
                    case 0: //Skills                     
                        groupBoxSkillsH.Show();
                        break;
                    case 1: //Charms
                        groupBoxCharms.Show();
                        break;
                }
            }
        }

        PointF VectorH;
        private void btn_getPositionH_Click(object sender, EventArgs e)
        {

            var Vector = hollowSplitter.getCurrentPosition();
            this.VectorH = Vector;
            this.textBoxXh.Clear();
            this.textBoxYh.Clear();
            this.textBoxSh.Clear();
            this.textBoxXh.Paste(Vector.X.ToString("0.00"));
            this.textBoxYh.Paste(Vector.Y.ToString("0.00"));
            this.textBoxSh.Paste(hollowSplitter.currentPosition.sceneName == String.Empty ? "NULL" : hollowSplitter.currentPosition.sceneName);
        }

        private void comboBoxMarginH_SelectedIndexChanged(object sender, EventArgs e)
        {
            int select = comboBoxMarginH.SelectedIndex;
            hollowSplitter.dataHollow.positionMargin = select;
        }

        private void btn_AddPositionH_Click(object sender, EventArgs e)
        {          
            if (this.VectorH != null)
            {
                var contains1 = !listBoxPositionH.Items.Contains(this.VectorH + " - " + textBoxSh.Text);
                if (contains1)
                {
                    if (this.VectorH.X == 0 && this.VectorH.Y == 0 && textBoxSh.Text == "NULL")
                    {
                        MessageBox.Show("Dont use cords 0,0 or Scene Null", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        listBoxPositionH.Items.Add(this.VectorH + " - " + textBoxSh.Text);
                        MessageBox.Show("Move your character to avoid autosplitting ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        hollowSplitter.setProcedure(false);
                        hollowSplitter.AddPosition(this.VectorH, textBoxSh.Text);
                        hollowSplitter.setProcedure(true);
                    }
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Plase get a position ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void listBoxPositionH_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxPositionH.SelectedItem != null)
            {
                int i = listBoxPositionH.Items.IndexOf(listBoxPositionH.SelectedItem);
                hollowSplitter.setProcedure(false);
                hollowSplitter.RemovePosition(i);
                hollowSplitter.setProcedure(true);
                listBoxPositionH.Items.Remove(listBoxPositionH.SelectedItem);
            }
        }

        private void btn_DesactiveAllH_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to disable everything?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                hollowSplitter.setProcedure(false);
                hollowSplitter.clearData();
                hollowSplitter.setProcedure(true);
                this.Controls.Clear();
                this.InitializeComponent();
                refreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
                TabControl2.TabPages.Add(tabHollow);
                TabControl2.SelectTab(tabHollow);
            }
        }

        #endregion
        #region Elden UI
        private void comboBoxToSplitEldenRing_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelBossER.Hide();
            panelGraceER.Hide();
            panelPositionsER.Hide();
            panelCfER.Hide();

            switch (comboBoxToSplitEldenRing.SelectedIndex)
            {
                case 0:
                    panelBossER.Show(); break;
                case 1:
                    panelGraceER.Show(); break;
                case 2:
                    panelPositionsER.Show(); break;
                case 3:
                    panelCfER.Show(); break;
            }
        }

        private void btnAddBossER_Click(object sender, EventArgs e)
        {           
            if (comboBoxBossER.SelectedIndex == -1 || comboBoxHowBossER.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select boss and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var contains1 = !listBoxBossER.Items.Contains(comboBoxBossER.Text.ToString() + " - " + "Inmediatly");
                var contains2 = !listBoxBossER.Items.Contains(comboBoxBossER.Text.ToString() + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    eldenSplitter.setProcedure(false);
                    eldenSplitter.AddBoss(comboBoxBossER.Text.ToString(), comboBoxHowBossER.Text.ToString());
                    listBoxBossER.Items.Add(comboBoxBossER.Text.ToString() + " - " + comboBoxHowBossER.Text.ToString());
                    eldenSplitter.setProcedure(true);
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxBossER_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxBossER.SelectedItem != null)
            {
                int i = listBoxBossER.Items.IndexOf(listBoxBossER.SelectedItem);
                eldenSplitter.setProcedure(false);
                eldenSplitter.RemoveBoss(i);
                eldenSplitter.setProcedure(true);
                listBoxBossER.Items.Remove(listBoxBossER.SelectedItem);
            }
        }

        private void btnAddGraceER_Click(object sender, EventArgs e)
        {          
            if (comboBoxZoneSelectER.SelectedIndex == -1 || comboBoxHowGraceER.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select grace and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var contains1 = !listBoxGrace.Items.Contains(comboBoxZoneSelectER.Text.ToString() + " - " + "Inmediatly");
                var contains2 = !listBoxGrace.Items.Contains(comboBoxZoneSelectER.Text.ToString() + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    eldenSplitter.setProcedure(false);
                    eldenSplitter.AddGrace(comboBoxZoneSelectER.Text.ToString(), comboBoxHowGraceER.Text.ToString());
                    listBoxGrace.Items.Add(comboBoxZoneSelectER.Text.ToString() + " - " + comboBoxHowGraceER.Text.ToString());
                    eldenSplitter.setProcedure(true);
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxGrace_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxGrace.SelectedItem != null)
            {
                int i = listBoxGrace.Items.IndexOf(listBoxGrace.SelectedItem);
                eldenSplitter.setProcedure(false);
                eldenSplitter.RemoveGrace(i);
                eldenSplitter.setProcedure(true);
                listBoxBossER.Items.Remove(listBoxGrace.SelectedItem);
            }
        }

        SoulMemory.EldenRing.Position VectorER;
        private void comboBoxMarginER_SelectedIndexChanged(object sender, EventArgs e)
        {
            eldenSplitter.dataElden.positionMargin = comboBoxMarginER.SelectedIndex; ;
        }

        private void btnGetPosition_Click_1(object sender, EventArgs e)
        {
            var Vector = eldenSplitter.getCurrentPosition();
            this.VectorER = Vector;
            this.textBoxXEr.Clear();
            this.textBoxYEr.Clear();
            this.textBoxZEr.Clear();
            this.textBoxXEr.Paste(Vector.X.ToString("0.00"));
            this.textBoxYEr.Paste(Vector.Y.ToString("0.00"));
            this.textBoxZEr.Paste(Vector.Z.ToString("0.00"));
        }

        private void btnAddPositionER_Click(object sender, EventArgs e)
        {
            
            if (this.VectorER != null)
            {
                var contains1 = !listBoxPositionsER.Items.Contains(this.VectorER + " - " + "Inmediatly");
                var contains2 = !listBoxPositionsER.Items.Contains(this.VectorER + " - " + "Loading game after");
                if (contains1 && contains2)
                {

                    if (comboBoxHowPositionsER.SelectedIndex == -1)
                    {
                        MessageBox.Show("Select 'How' do you want split ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (this.VectorER.X == 0 && this.VectorER.Y == 0 && this.VectorER.Z == 0)
                        {
                            MessageBox.Show("Dont use cords 0,0,0", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            eldenSplitter.setProcedure(false);
                            listBoxPositionsER.Items.Add(this.VectorER + " - " + comboBoxHowPositionsER.Text.ToString());
                            MessageBox.Show("Move your character to avoid autosplitting ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            eldenSplitter.AddPosition(this.VectorER, comboBoxHowPositionsER.Text.ToString());
                            eldenSplitter.setProcedure(true);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Plase get a position ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBoxPositionsER_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxPositionsER.SelectedItem != null)
            {
                int i = listBoxPositionsER.Items.IndexOf(listBoxPositionsER.SelectedItem);
                eldenSplitter.setProcedure(false);
                eldenSplitter.RemovePosition(i);
                eldenSplitter.setProcedure(true);
                listBoxPositionsER.Items.Remove(listBoxPositionsER.SelectedItem);
            }
        }

        private void btnGetListER_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://pastebin.com/p8gByAgU");
        }

        private void btnAddCfER_Click(object sender, EventArgs e)
        {         
            if (textBoxIdER.Text == null || comboBoxHowCfER.SelectedIndex == -1)
            {
                MessageBox.Show("Plase set a ID and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    var id = uint.Parse(textBoxIdER.Text);
                    var contains1 = !listBoxCfER.Items.Contains(id + " - " + "Inmediatly");
                    var contains2 = !listBoxCfER.Items.Contains(id + " - " + "Loading game after");
                    if (contains1 && contains2)
                    {
                        eldenSplitter.setProcedure(false);
                        eldenSplitter.AddCustomFlag(id, comboBoxHowCfER.Text.ToString());
                        listBoxCfER.Items.Add(id + " - " + comboBoxHowCfER.Text.ToString());
                        eldenSplitter.setProcedure(true);
                    }
                    else
                    {
                        MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Wrong ID", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void listBoxCfER_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxCfER.SelectedItem != null)
            {
                int i = listBoxCfER.Items.IndexOf(listBoxCfER.SelectedItem);
                eldenSplitter.setProcedure(false);
                eldenSplitter.RemoveCustomFlag(i);
                eldenSplitter.setProcedure(true);
                listBoxCfER.Items.Remove(listBoxCfER.SelectedItem);
            }
        }
        private void btn_DesactiveAllElden_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to disable everything?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                eldenSplitter.setProcedure(false);
                eldenSplitter.clearData();
                eldenSplitter.setProcedure(true);
                this.Controls.Clear();
                this.InitializeComponent();
                refreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
                TabControl2.TabPages.Add(tabElden);
                TabControl2.SelectTab(tabElden);
            }
        }

        #endregion
        #region Ds3 UI

        private void comboBoxToSplitSelectDs3_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelBossDs3.Hide();
            panelBonfireDs3.Hide();
            panelLvlDs3.Hide();
            panelCfDs3.Hide();


            switch (comboBoxToSplitSelectDs3.SelectedIndex)
            {
                case 0:
                    panelBossDs3.Show();
                    break;
                case 1:
                    panelBonfireDs3.Show();
                    break;
                case 2:
                    panelLvlDs3.Show();
                    break;
                case 3:
                    panelCfDs3.Show();
                    break;
            }
        }

        private void btnAddBossDs3_Click(object sender, EventArgs e)
        {           
            if (comboBoxBossDs3.SelectedIndex == -1 || comboBoxHowBossDs3.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select boss and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var contains1 = !listBoxBossDs3.Items.Contains(comboBoxBossDs3.Text.ToString() + " - " + "Inmediatly");
                var contains2 = !listBoxBossDs3.Items.Contains(comboBoxBossDs3.Text.ToString() + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    ds3Splitter.setProcedure(false);
                    ds3Splitter.AddBoss(comboBoxBossDs3.Text.ToString(), comboBoxHowBossDs3.Text.ToString());
                    listBoxBossDs3.Items.Add(comboBoxBossDs3.Text.ToString() + " - " + comboBoxHowBossDs3.Text.ToString());
                    ds3Splitter.setProcedure(true);
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxBossDs3_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxBossDs3.SelectedItem != null)
            {
                int i = listBoxBossDs3.Items.IndexOf(listBoxBossDs3.SelectedItem);
                ds3Splitter.setProcedure(false);
                ds3Splitter.RemoveBoss(i);
                ds3Splitter.setProcedure(true);
                listBoxBossDs3.Items.Remove(listBoxBossDs3.SelectedItem);
            }
        }

        private void btnAddBonfire_Click(object sender, EventArgs e)
        {            
            if (comboBoxBonfireDs3.SelectedIndex == -1 || comboBoxHowBonfireDs3.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select bonefire and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var contains1 = !listBoxBonfireDs3.Items.Contains(comboBoxBonfireDs3.Text.ToString() + " - " + "Inmediatly");
                var contains2 = !listBoxBonfireDs3.Items.Contains(comboBoxBonfireDs3.Text.ToString() + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    ds3Splitter.setProcedure(false);
                    ds3Splitter.AddBonfire(comboBoxBonfireDs3.Text.ToString(), comboBoxHowBonfireDs3.Text.ToString());
                    listBoxBonfireDs3.Items.Add(comboBoxBonfireDs3.Text.ToString() + " - " + comboBoxHowBonfireDs3.Text.ToString());
                    ds3Splitter.setProcedure(true);
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxBonfireDs3_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxBonfireDs3.SelectedItem != null)
            {
                int i = listBoxBonfireDs3.Items.IndexOf(listBoxBonfireDs3.SelectedItem);
                ds3Splitter.setProcedure(false);
                ds3Splitter.RemoveBonfire(i);
                ds3Splitter.setProcedure(true);
                listBoxBonfireDs3.Items.Remove(listBoxBonfireDs3.SelectedItem);
            }
        }


        private void btnAddAttributeDs3_Click(object sender, EventArgs e)
        {           
            if (comboBoxAttributeDs3.SelectedIndex == -1 || comboBoxHowAttributeDs3.SelectedIndex == -1 || textBoxValueDs3.Text == null)
            {
                MessageBox.Show("Plase select Attribute, Value and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    var value = uint.Parse(textBoxValueDs3.Text);
                    var contains1 = !listBoxAttributesDs3.Items.Contains(comboBoxAttributeDs3.Text.ToString() + ": " + value + " - " + "Inmediatly");
                    var contains2 = !listBoxAttributesDs3.Items.Contains(comboBoxAttributeDs3.Text.ToString() + ": " + value + " - " + "Loading game after");
                    if (contains1 && contains2)
                    {
                        ds3Splitter.setProcedure(false);
                        ds3Splitter.AddAttribute(comboBoxAttributeDs3.Text.ToString(), comboBoxHowAttributeDs3.Text.ToString(), value);
                        listBoxAttributesDs3.Items.Add(comboBoxAttributeDs3.Text.ToString() + ": " + value + " - " + comboBoxHowAttributeDs3.Text.ToString());
                        ds3Splitter.setProcedure(true);
                    }
                    else
                    {
                        MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Check Value and try again", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxAttributesDs3_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxAttributesDs3.SelectedItem != null)
            {
                int i = listBoxAttributesDs3.Items.IndexOf(listBoxAttributesDs3.SelectedItem);
                ds3Splitter.setProcedure(false);
                ds3Splitter.RemoveAttribute(i);
                ds3Splitter.setProcedure(true);
                listBoxAttributesDs3.Items.Remove(listBoxAttributesDs3.SelectedItem);
            }
        }

        private void btnGetListFlagDs3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://pastebin.com/3DyjrgUN");
        }

        private void btnAddCfeDs3_Click(object sender, EventArgs e)
        {           
            if (textBoxIdDs3.Text == null || comboBoxHowCfDs3.SelectedIndex == -1)
            {
                MessageBox.Show("Plase set a ID and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    var id = uint.Parse(textBoxIdDs3.Text);
                    var contains1 = !listBoxCfDs3.Items.Contains(id + " - " + "Inmediatly");
                    var contains2 = !listBoxCfDs3.Items.Contains(id + " - " + "Loading game after");
                    if (contains1 && contains2)
                    {
                        ds3Splitter.setProcedure(false);
                        ds3Splitter.AddCustomFlag(id, comboBoxHowCfDs3.Text.ToString());
                        listBoxCfDs3.Items.Add(id + " - " + comboBoxHowCfDs3.Text.ToString());
                        ds3Splitter.setProcedure(true);
                    }
                    else
                    {
                        MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Wrong ID", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void listBoxCfDs3_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxCfDs3.SelectedItem != null)
            {
                int i = listBoxCfDs3.Items.IndexOf(listBoxCfDs3.SelectedItem);
                ds3Splitter.setProcedure(false);
                ds3Splitter.RemoveCustomFlag(i);
                ds3Splitter.setProcedure(true);
                listBoxCfDs3.Items.Remove(listBoxCfDs3.SelectedItem);
            }
        }

        private void btnDesactiveAllDs3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to disable everything?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ds3Splitter.setProcedure(false);
                ds3Splitter.clearData();
                ds3Splitter.setProcedure(true);
                this.Controls.Clear();
                this.InitializeComponent();
                refreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
                TabControl2.TabPages.Add(tabDs3);
                TabControl2.SelectTab(tabDs3);
            }
        }

        #endregion
        #region Celeste UI
        private void comboBoxToSplitCeleste_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelChapterCeleste.Hide();
            panelCheckpointsCeleste.Hide();

            switch (comboBoxToSplitCeleste.SelectedIndex)
            {
                case 0:
                    panelChapterCeleste.Show(); break;
                case 1:
                    panelCheckpointsCeleste.Show(); break;
            }

        }
        private void checkedListBoxCeleste_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxChapterCeleste.SelectedIndex != -1)
            {
                if (checkedListBoxChapterCeleste.GetItemChecked(checkedListBoxChapterCeleste.SelectedIndex) == false)
                {
                    celesteSplitter.setProcedure(false);
                    celesteSplitter.AddChapter(checkedListBoxChapterCeleste.SelectedItem.ToString());
                    celesteSplitter.setProcedure(true);
                }
                else
                {
                    celesteSplitter.setProcedure(false);
                    celesteSplitter.RemoveChapter(checkedListBoxChapterCeleste.SelectedItem.ToString());
                    celesteSplitter.setProcedure(true);
                }
            }
        }


        private void checkedListBoxCheckpointsCeleste_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxCheckpointsCeleste.SelectedIndex != -1)
            {
                if (checkedListBoxCheckpointsCeleste.GetItemChecked(checkedListBoxCheckpointsCeleste.SelectedIndex) == false)
                {
                    celesteSplitter.setProcedure(false);
                    celesteSplitter.AddChapter(checkedListBoxCheckpointsCeleste.SelectedItem.ToString());
                    celesteSplitter.setProcedure(true);
                }
                else
                {
                    celesteSplitter.setProcedure(false);
                    celesteSplitter.RemoveChapter(checkedListBoxCheckpointsCeleste.SelectedItem.ToString());
                    celesteSplitter.setProcedure(true);
                }
            }
        }

        private void btnRemoveAllCeleste_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to disable everything?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                celesteSplitter.setProcedure(false);
                celesteSplitter.clearData();
                celesteSplitter.setProcedure(true);
                this.Controls.Clear();
                this.InitializeComponent();
                refreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
                TabControl2.TabPages.Add(tabCeleste);
                TabControl2.SelectTab(tabCeleste);
            }
        }



        #endregion
        #region Ds2 UI
        private void comboBoxToSplitDs2_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelBossDS2.Hide();
            panelLvlDs2.Hide();
            panelPositionDs2.Hide();


            switch (comboBoxToSplitDs2.SelectedIndex)
            {
                case 0: panelBossDS2.Show(); break;
                case 1: panelLvlDs2.Show(); break;
                case 2: panelPositionDs2.Show(); break;
            }
        }

        private void btnAddBossDS2_Click(object sender, EventArgs e)
        {
            
            if (comboBoxBossDs2.SelectedIndex == -1 || comboBoxHowBossDs2.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select boss and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var contains1 = !listBoxBossDs2.Items.Contains(comboBoxBossDs2.Text.ToString() + " - " + "Inmediatly");
                var contains2 = !listBoxBossDs2.Items.Contains(comboBoxBossDs2.Text.ToString() + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    ds2Splitter.setProcedure(false);
                    ds2Splitter.AddBoss(comboBoxBossDs2.Text.ToString(), comboBoxHowBossDs2.Text.ToString());
                    listBoxBossDs2.Items.Add(comboBoxBossDs2.Text.ToString() + " - " + comboBoxHowBossDs2.Text.ToString());
                    ds2Splitter.setProcedure(true);
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxBossDs2_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxBossDs2.SelectedItem != null)
            {
                int i = listBoxBossDs2.Items.IndexOf(listBoxBossDs2.SelectedItem);
                ds2Splitter.setProcedure(false);
                ds2Splitter.RemoveBoss(i);
                ds2Splitter.setProcedure(true);
                listBoxBossDs2.Items.Remove(listBoxBossDs2.SelectedItem);
            }
        }
        private void btnAddAttributeDs2_Click(object sender, EventArgs e)
        { 
            if (comboBoxAttributeDs2.SelectedIndex == -1 || comboBoxHowAttributeDs2.SelectedIndex == -1 || textBoxValueDs2.Text == null)
            {
                MessageBox.Show("Plase select Attribute, Value and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    var value = uint.Parse(textBoxValueDs2.Text);
                    var contains1 = !listBoxAttributeDs2.Items.Contains(comboBoxAttributeDs2.Text.ToString() + ": " + value + " - " + "Inmediatly");
                    var contains2 = !listBoxAttributeDs2.Items.Contains(comboBoxAttributeDs2.Text.ToString() + ": " + value + " - " + "Loading game after");
                    if (contains1 && contains2)
                    {
                        ds2Splitter.setProcedure(false);
                        ds2Splitter.AddAttribute(comboBoxAttributeDs2.Text.ToString(), comboBoxHowAttributeDs2.Text.ToString(), value);
                        listBoxAttributeDs2.Items.Add(comboBoxAttributeDs2.Text.ToString() + ": " + value + " - " + comboBoxHowAttributeDs2.Text.ToString());
                        ds2Splitter.setProcedure(true);
                    }
                    else
                    {
                        MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Check Value and try again", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxAttributeDs2_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxAttributeDs2.SelectedItem != null)
            {
                int i = listBoxAttributeDs2.Items.IndexOf(listBoxAttributeDs2.SelectedItem);
                ds2Splitter.setProcedure(false);
                ds2Splitter.RemoveAttribute(i);
                ds2Splitter.setProcedure(true);
                listBoxAttributeDs2.Items.Remove(listBoxAttributeDs2.SelectedItem);
            }
        }

        Vector3f VectorDs2;
        private void btnGetPositionDs2_Click(object sender, EventArgs e)
        {
            var Vector = ds2Splitter.getCurrentPosition();
            this.VectorDs2 = Vector;
            this.textBoxXDs2.Clear();
            this.textBoxYDs2.Clear();
            this.textBoxZDs2.Clear();
            this.textBoxXDs2.Paste(Vector.X.ToString("0.00"));
            this.textBoxYDs2.Paste(Vector.Y.ToString("0.00"));
            this.textBoxZDs2.Paste(Vector.Z.ToString("0.00"));
        }

        private void comboBoxMarginDs2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds2Splitter.dataDs2.positionMargin = comboBoxMarginDs2.SelectedIndex;
        }

        private void btnAddPositionDs2_Click(object sender, EventArgs e)
        {            
            if (this.VectorDs2 != null)
            {
                var contains1 = !listBoxPositionsDs2.Items.Contains(this.VectorDs2 + " - " + "Inmediatly");
                var contains2 = !listBoxPositionsDs2.Items.Contains(this.VectorDs2 + " - " + "Loading game after");
                if (contains1 && contains2)
                {

                    if (comboBoxHowPositionsDs2.SelectedIndex == -1)
                    {
                        MessageBox.Show("Select 'How' do you want split ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (this.VectorDs2.X == 0 && this.VectorDs2.Y == 0 && this.VectorDs2.Z == 0)
                        {
                            MessageBox.Show("Dont use cords 0,0,0", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            ds2Splitter.setProcedure(false);
                            listBoxPositionsDs2.Items.Add(this.VectorDs2 + " - " + comboBoxHowPositionsDs2.Text.ToString());
                            MessageBox.Show("Move your character to avoid autosplitting ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ds2Splitter.AddPosition(this.VectorDs2, comboBoxHowPositionsDs2.Text.ToString());
                            ds2Splitter.setProcedure(true);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Plase get a position ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBoxPositionsDs2_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxPositionsDs2.SelectedItem != null)
            {
                int i = listBoxPositionsDs2.Items.IndexOf(listBoxPositionsDs2.SelectedItem);
                ds2Splitter.setProcedure(false);
                ds2Splitter.RemovePosition(i);
                ds2Splitter.setProcedure(true);
                listBoxPositionsDs2.Items.Remove(listBoxPositionsDs2.SelectedItem);
            }
        }

        private void btnDesactiveAllDs2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to disable everything?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ds2Splitter.setProcedure(false);
                ds2Splitter.clearData();
                ds2Splitter.setProcedure(true);
                this.Controls.Clear();
                this.InitializeComponent();
                refreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
                TabControl2.TabPages.Add(tabDs2);
                TabControl2.SelectTab(tabDs2);
            }
        }


        #endregion
        #region Cuphead UI
        private void comboBoxToSplitCuphead_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelBossCuphead.Hide();
            panelLevelCuphead.Hide();
            switch (comboBoxToSplitCuphead.SelectedIndex)
            {
                case 0:
                    panelBossCuphead.Show(); break;
                case 1:
                    panelLevelCuphead.Show(); break;
            }
        }

        private void checkedListBoxBossCuphead_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxBossCuphead.SelectedIndex != -1)
            {
                if (checkedListBoxBossCuphead.GetItemChecked(checkedListBoxBossCuphead.SelectedIndex) == false)
                {
                    cupSplitter.setProcedure(false);
                    cupSplitter.AddElement(checkedListBoxBossCuphead.SelectedItem.ToString());
                    cupSplitter.setProcedure(true);
                }
                else
                {
                    cupSplitter.setProcedure(false);
                    cupSplitter.RemoveElement(checkedListBoxBossCuphead.SelectedItem.ToString());
                    cupSplitter.setProcedure(true);
                }
            }
        }

        private void checkedListLevelCuphead_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListLevelCuphead.SelectedIndex != -1)
            {
                if (checkedListLevelCuphead.GetItemChecked(checkedListLevelCuphead.SelectedIndex) == false)
                {
                    cupSplitter.setProcedure(false);
                    cupSplitter.AddElement(checkedListLevelCuphead.SelectedItem.ToString());
                    cupSplitter.setProcedure(true);
                }
                else
                {
                    cupSplitter.setProcedure(false);
                    cupSplitter.RemoveElement(checkedListLevelCuphead.SelectedItem.ToString());
                    cupSplitter.setProcedure(true);
                }
            }
        }

        private void btnRemoveAllCuphead_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to disable everything?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                cupSplitter.setProcedure(false);
                cupSplitter.clearData();
                cupSplitter.setProcedure(true);
                this.Controls.Clear();
                this.InitializeComponent();
                refreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
                TabControl2.TabPages.Add(tabCuphead);
                TabControl2.SelectTab(tabCuphead);
            }
        }


        #endregion
        #region Ds1 UI
        private void comboBoxToSplitDs1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelBossDs1.Hide();
            panelBonfireDs1.Hide();
            panelLvlDs1.Hide();
            panelPositionDs1.Hide();
            panelItemDs1.Hide();

            switch (comboBoxToSplitDs1.SelectedIndex)
            {
                case 0: panelBossDs1.Show(); break;
                case 1: panelBonfireDs1.Show(); break;
                case 2: panelLvlDs1.Show(); break;
                case 3: panelPositionDs1.Show(); break;
                case 4: panelItemDs1.Show(); break;
            }
        }

        private void btnAddBossDs1_Click(object sender, EventArgs e)
        {            
            if (comboBoxBossDs1.SelectedIndex == -1 || comboBoxHowBossDs1.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select boss and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var contains1 = !listBoxBossDs1.Items.Contains(comboBoxBossDs1.Text.ToString() + " - " + "Inmediatly");
                var contains2 = !listBoxBossDs1.Items.Contains(comboBoxBossDs1.Text.ToString() + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    ds1Splitter.setProcedure(false);
                    ds1Splitter.AddBoss(comboBoxBossDs1.Text.ToString(), comboBoxHowBossDs1.Text.ToString());
                    listBoxBossDs1.Items.Add(comboBoxBossDs1.Text.ToString() + " - " + comboBoxHowBossDs1.Text.ToString());
                    ds1Splitter.setProcedure(true);
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxBossDs1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxBossDs1.SelectedItem != null)
            {
                int i = listBoxBossDs1.Items.IndexOf(listBoxBossDs1.SelectedItem);
                ds1Splitter.setProcedure(false);
                ds1Splitter.RemoveBoss(i);
                ds1Splitter.setProcedure(true);
                listBoxBossDs1.Items.Remove(listBoxBossDs1.SelectedItem);
            }
        }

        private void btnAddBonfireDs1_Click(object sender, EventArgs e)
        {           
            if (comboBoxBonfireDs1.SelectedIndex == -1 || comboBoxHowBonfireDs1.SelectedIndex == -1 || comboBoxStateDs1.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select state, bonefire and 'How' do you want split", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var contains1 = !listBoxBonfireDs1.Items.Contains(comboBoxBonfireDs1.Text.ToString() + " - " + ds1Splitter.convertStringToState(comboBoxStateDs1.Text.ToString()) + " - " + "Inmediatly");
                var contains2 = !listBoxBonfireDs1.Items.Contains(comboBoxBonfireDs1.Text.ToString() + " - " + ds1Splitter.convertStringToState(comboBoxStateDs1.Text.ToString()) + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    ds1Splitter.setProcedure(false);
                    ds1Splitter.AddBonfire(comboBoxBonfireDs1.Text.ToString(), comboBoxHowBonfireDs1.Text.ToString(), comboBoxStateDs1.Text.ToString());
                    listBoxBonfireDs1.Items.Add(comboBoxBonfireDs1.Text.ToString() + " - " + ds1Splitter.convertStringToState(comboBoxStateDs1.Text.ToString()) + " - " + comboBoxHowBonfireDs1.Text.ToString());
                    ds1Splitter.setProcedure(true);
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxBonfireDs1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxBonfireDs1.SelectedItem != null)
            {
                int i = listBoxBonfireDs1.Items.IndexOf(listBoxBonfireDs1.SelectedItem);
                ds1Splitter.setProcedure(false);
                ds1Splitter.RemoveBonfire(i);
                ds1Splitter.setProcedure(true);
                listBoxBonfireDs1.Items.Remove(listBoxBonfireDs1.SelectedItem);
            }
        }

        private void btnAddAttributeDs1_Click(object sender, EventArgs e)
        {         
            if (comboBoxAttributesDs1.SelectedIndex == -1 || comboBoxHowAttributesDs1.SelectedIndex == -1 || textBoxValueDs1.Text == null)
            {
                MessageBox.Show("Plase select Attribute, Value and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    var value = uint.Parse(textBoxValueDs1.Text);
                    var contains1 = !listBoxAttributesDs1.Items.Contains(comboBoxAttributesDs1.Text.ToString() + ": " + value + " - " + "Inmediatly");
                    var contains2 = !listBoxAttributesDs1.Items.Contains(comboBoxAttributesDs1.Text.ToString() + ": " + value + " - " + "Loading game after");
                    if (contains1 && contains2)
                    {
                        ds1Splitter.setProcedure(false);
                        ds1Splitter.AddAttribute(comboBoxAttributesDs1.Text.ToString(), comboBoxHowAttributesDs1.Text.ToString(), value);
                        listBoxAttributesDs1.Items.Add(comboBoxAttributesDs1.Text.ToString() + ": " + value + " - " + comboBoxHowAttributesDs1.Text.ToString());
                        ds1Splitter.setProcedure(true);
                    }
                    else
                    {
                        MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Check Value and try again", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxAttributeDs1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxAttributesDs1.SelectedItem != null)
            {
                int i = listBoxAttributesDs1.Items.IndexOf(listBoxAttributesDs1.SelectedItem);
                ds1Splitter.setProcedure(false);
                ds1Splitter.RemoveAttribute(i);
                ds1Splitter.setProcedure(true);
                listBoxAttributesDs1.Items.Remove(listBoxAttributesDs1.SelectedItem);
            }
        }

        Vector3f VectorDs1;
        private void btnGetPositionDs1_Click(object sender, EventArgs e)
        {
            var Vector = ds1Splitter.getCurrentPosition();
            this.VectorDs1 = Vector;
            this.textBoxXDs1.Clear();
            this.textBoxYDs1.Clear();
            this.textBoxZDs1.Clear();
            this.textBoxXDs1.Paste(Vector.X.ToString("0.00"));
            this.textBoxYDs1.Paste(Vector.Y.ToString("0.00"));
            this.textBoxZDs1.Paste(Vector.Z.ToString("0.00"));
        }

        private void listBoxPositionDs1_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxPositionsDs1.SelectedItem != null)
            {
                int i = listBoxPositionsDs1.Items.IndexOf(listBoxPositionsDs1.SelectedItem);
                ds1Splitter.setProcedure(false);
                ds1Splitter.RemovePosition(i);
                ds1Splitter.setProcedure(true);
                listBoxPositionsDs1.Items.Remove(listBoxPositionsDs1.SelectedItem);
            }
        }

        private void comboBoxMarginDs1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds1Splitter.dataDs1.positionMargin = comboBoxMarginDs1.SelectedIndex;
        }

        private void btnAddPositionDs1_Click(object sender, EventArgs e)
        {
            
            if (this.VectorDs1 != null)
            {
                var contains1 = !listBoxPositionsDs1.Items.Contains(this.VectorDs1 + " - " + "Inmediatly");
                var contains2 = !listBoxPositionsDs1.Items.Contains(this.VectorDs1 + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    if (comboBoxHowPositionsDs1.SelectedIndex == -1)
                    {
                        MessageBox.Show("Select 'How' do you want split ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (this.VectorDs1.X == 0 && this.VectorDs1.Y == 0 && this.VectorDs1.Z == 0)
                        {
                            MessageBox.Show("Dont use cords 0,0,0", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            ds1Splitter.setProcedure(false);
                            listBoxPositionsDs1.Items.Add(this.VectorDs1 + " - " + comboBoxHowPositionsDs1.Text.ToString());
                            MessageBox.Show("Move your character to avoid autosplitting ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ds1Splitter.AddPosition(this.VectorDs1, comboBoxHowPositionsDs1.Text.ToString());
                            ds1Splitter.setProcedure(true);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Plase get a position ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnAddItem_Click(object sender, EventArgs e)
        {
            
            if (comboBoxItemDs1.SelectedIndex == -1 || comboBoxHowItemDs1.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select Item and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var contains1 = !listBoxItemDs1.Items.Contains(comboBoxItemDs1.Text.ToString() + " - " + "Inmediatly");
                var contains2 = !listBoxItemDs1.Items.Contains(comboBoxItemDs1.Text.ToString() + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    ds1Splitter.setProcedure(false);
                    ds1Splitter.AddItem(comboBoxItemDs1.Text.ToString(), comboBoxHowItemDs1.Text.ToString());
                    listBoxItemDs1.Items.Add(comboBoxItemDs1.Text.ToString() + " - " + comboBoxHowItemDs1.Text.ToString());
                    ds1Splitter.setProcedure(true);
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxItemDs1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxItemDs1.SelectedItem != null)
            {
                int i = listBoxItemDs1.Items.IndexOf(listBoxItemDs1.SelectedItem);
                ds1Splitter.setProcedure(false);
                ds1Splitter.RemoveItem(i);
                ds1Splitter.setProcedure(true);
                listBoxItemDs1.Items.Remove(listBoxItemDs1.SelectedItem);
            }
        }

        private void btnDesactiveAllDs1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to disable everything?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ds1Splitter.setProcedure(false);
                ds1Splitter.clearData();
                ds1Splitter.setProcedure(true);
                this.Controls.Clear();
                this.InitializeComponent();
                refreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
                TabControl2.TabPages.Add(tabDs1);
                TabControl2.SelectTab(tabDs1);
            }
        }


        #endregion      
    }
}
