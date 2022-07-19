//MIT License

//Copyright (c) 2019-2022 Ezequiel Medina

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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SoulMemory;

namespace HitCounterManager
{
    public partial class AutoSplitter : Form
    {
        SekiroSplitter sekiroSplitter;
        Vector3f Vector;

        public Exception Exception { get; set; }

        public AutoSplitter(SekiroSplitter sekiroSplitter)
        {
            InitializeComponent();
            this.sekiroSplitter = sekiroSplitter;
            refreshForm();
        }

        public void refreshForm()
        {
            #region SekiroTab       
            this.panel1.Hide();
            this.panel2.Hide();
            this.panel3.Hide();
            this.sekiroRunning.Hide();
            this.checkBossSekiro.Hide();
            this.zoneSelectSekiro.Hide();
            this.label_Zone1.Hide();
            this.listBoxAshinaOutskirts.Hide();
            this.listBoxHirataEstate.Hide();
            this.listBoxAshinaCastle.Hide();
            this.listBoxAbandonedDungeon.Hide();
            this.listBoxSenpouTemple.Hide();
            this.listBoxSunkenValley.Hide();
            this.listBoxAshinaDepths.Hide();
            this.listBoxFountainheadPalace.Hide();
            this.labelCoordinates.Hide();
            this.comboBoxMargin.Hide();
            this.btnGetPotition.Hide();
            this.textBoxX.Hide();
            this.textBoxY.Hide();
            this.textBoxZ.Hide();
            this.listBoxPositions.Hide();
            this.groupBoxEnablersSekiro.Hide();

            #endregion
            checkStatusGames();
        }

        private void AutoSplitter_Load(object sender, EventArgs e)
        {
            DTSekiro sekiroData = sekiroSplitter.getDataSekiro();
            #region SekiroLoad.Propierties
            if (sekiroData.enableSplitting)
            {
                checkBoxESekiroSplitting.Checked = true;
            }

            if (sekiroData.enableTimer)
            {
                checkBoxSekiroTimer.Checked = true;
            }



            #endregion
            #region SekiroLoad.Boss

            List<DefinitionsSekiro.Boss> bossesToSplit = sekiroData.getBossToSplit();
            foreach (DefinitionsSekiro.Boss bs in bossesToSplit)
            {
                for (int i = 0; i < checkBossSekiro.Items.Count; i++)
                {
                    if (bs.Title == checkBossSekiro.Items[i].ToString())
                    {
                        checkBossSekiro.SetItemChecked(i, true);
                    }
                }
            }
            #endregion
            #region SekiroLoad.Idol
            List<DefinitionsSekiro.Idol> idolsTosplit = sekiroData.getidolsTosplit();
            foreach (DefinitionsSekiro.Idol idol in idolsTosplit)
            {
                for (int i = 0; i < listBoxAshinaOutskirts.Items.Count; i++)
                {
                    if (idol.Title == listBoxAshinaOutskirts.Items[i].ToString())
                    {
                        listBoxAshinaOutskirts.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < listBoxHirataEstate.Items.Count; i++)
                {
                    if (idol.Title == listBoxHirataEstate.Items[i].ToString())
                    {
                        listBoxHirataEstate.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < listBoxAshinaCastle.Items.Count; i++)
                {
                    if (idol.Title == listBoxAshinaCastle.Items[i].ToString())
                    {
                        listBoxAshinaCastle.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < listBoxAbandonedDungeon.Items.Count; i++)
                {
                    if (idol.Title == listBoxAbandonedDungeon.Items[i].ToString())
                    {
                        listBoxAbandonedDungeon.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < listBoxSenpouTemple.Items.Count; i++)
                {
                    if (idol.Title == listBoxSenpouTemple.Items[i].ToString())
                    {
                        listBoxSenpouTemple.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < listBoxSunkenValley.Items.Count; i++)
                {
                    if (idol.Title == listBoxSunkenValley.Items[i].ToString())
                    {
                        listBoxSunkenValley.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < listBoxAshinaDepths.Items.Count; i++)
                {
                    if (idol.Title == listBoxAshinaDepths.Items[i].ToString())
                    {
                        listBoxAshinaDepths.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < listBoxFountainheadPalace.Items.Count; i++)
                {
                    if (idol.Title == listBoxFountainheadPalace.Items[i].ToString())
                    {
                        listBoxFountainheadPalace.SetItemChecked(i, true);
                    }
                }
            }
            #endregion
            #region SekiroLoad.Position
            foreach (DefinitionsSekiro.Position position in sekiroData.getPositionsToSplit())
            {
                listBoxPositions.Items.Add(position.vector);            
            }
            comboBoxMargin.SelectedIndex = sekiroData.positionMargin;
            #endregion
            #region UpdateLoad
            string fullPath = Path.GetFullPath("SoulMemory.dll");
            this.lblVersionCurrent.Text = System.Reflection.Assembly.LoadFile(fullPath).GetName().Version.ToString();
            #endregion


        }

        private void refresh_Btn(object sender, EventArgs e)
        {
            checkStatusGames();
        }

        public void checkStatusGames()
        {
            Exception excS = null;
            if (sekiroSplitter.getSekiroStatusProcess(out excS,0))
            {
                this.sekiroRunning.Show();
                this.SekiroNotRunning.Hide();
            }
            else
            {
                this.SekiroNotRunning.Show();
                this.sekiroRunning.Hide();
            }
        }

        #region Sekiro.UI
        private void btn_applySekiro_Click(object sender, EventArgs e)
        {
            #region Split.Boss
            foreach (object itemChecked in checkBossSekiro.CheckedItems)
            {
                string boss = itemChecked.ToString();
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.AddBoss(boss);
                sekiroSplitter.setProcedure(true);
                sekiroSplitter.LoadAutoSplitterProcedure();
            }


            IEnumerable<object> notCheckedB = (from object item in checkBossSekiro.Items
                                              where !checkBossSekiro.CheckedItems.Contains(item)
                                              select item);

            foreach (object itemUnchecked in notCheckedB)
            {
                string boss = itemUnchecked.ToString();
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.RemoveBoss(boss);
                sekiroSplitter.setProcedure(true);
                sekiroSplitter.LoadAutoSplitterProcedure();
            }
            #endregion
            #region Split.Idol
            //Asshina Outskirts
            foreach (object itemCheckedAO in listBoxAshinaOutskirts.CheckedItems)
            {
                string idol = itemCheckedAO.ToString();
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.AddIdol(idol);
                sekiroSplitter.setProcedure(true);
                sekiroSplitter.LoadAutoSplitterProcedure();
            }


            IEnumerable<object> notCheckedI_AO = (from object item in this.listBoxAshinaOutskirts.Items
                                              where !this.listBoxAshinaOutskirts.CheckedItems.Contains(item)
                                              select item);

            foreach (object itemUncheckedAO in notCheckedI_AO)
            {
                string idol = itemUncheckedAO.ToString();
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.RemoveIdol(idol);
                sekiroSplitter.setProcedure(true);
                sekiroSplitter.LoadAutoSplitterProcedure();
            }
            //Hirata Estate
           
            foreach (object itemChecked in listBoxHirataEstate.CheckedItems)
            {
                string idol = itemChecked.ToString();
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.AddIdol(idol);
                sekiroSplitter.setProcedure(true);
                sekiroSplitter.LoadAutoSplitterProcedure();
            }


            IEnumerable<object> notCheckedI_HE = (from object item in listBoxHirataEstate.Items
                                                  where !listBoxHirataEstate.CheckedItems.Contains(item)
                                                  select item);

            foreach (object itemUnchecked in notCheckedI_HE)
            {
                string idol = itemUnchecked.ToString();
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.RemoveIdol(idol);
                sekiroSplitter.setProcedure(true);
                sekiroSplitter.LoadAutoSplitterProcedure();
            }
            
            //Ashina Castle

            foreach (object itemChecked in listBoxAshinaCastle.CheckedItems)
            {
                string idol = itemChecked.ToString();
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.AddIdol(idol);
                sekiroSplitter.setProcedure(true);
                sekiroSplitter.LoadAutoSplitterProcedure();
            }


            IEnumerable<object> notCheckedI_AC = (from object item in listBoxAshinaCastle.Items
                                                  where !listBoxAshinaCastle.CheckedItems.Contains(item)
                                                  select item);

            foreach (object itemUnchecked in notCheckedI_AC)
            {
                string idol = itemUnchecked.ToString();
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.RemoveIdol(idol);
                sekiroSplitter.setProcedure(true);
                sekiroSplitter.LoadAutoSplitterProcedure();
            }
            //Abandoned Dungeon
            foreach (object itemChecked in listBoxAbandonedDungeon.CheckedItems)
            {
                string idol = itemChecked.ToString();
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.AddIdol(idol);
                sekiroSplitter.setProcedure(true);
                sekiroSplitter.LoadAutoSplitterProcedure();
            }


            IEnumerable<object> notCheckedI_AD = (from object item in listBoxAbandonedDungeon.Items
                                                  where !listBoxAbandonedDungeon.CheckedItems.Contains(item)
                                                  select item);

            foreach (object itemUnchecked in notCheckedI_AD)
            {
                string idol = itemUnchecked.ToString();
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.RemoveIdol(idol);
                sekiroSplitter.setProcedure(true);
                sekiroSplitter.LoadAutoSplitterProcedure();
            }
            // Senpou Temple

            foreach (object itemChecked in listBoxSenpouTemple.CheckedItems)
            {
                string idol = itemChecked.ToString();
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.AddIdol(idol);
                sekiroSplitter.setProcedure(true);
                sekiroSplitter.LoadAutoSplitterProcedure();
            }


            IEnumerable<object> notCheckedI_ST = (from object item in listBoxSenpouTemple.Items
                                                    where !listBoxSenpouTemple.CheckedItems.Contains(item)
                                                    select item);

            foreach (object itemUnchecked in notCheckedI_ST)
            {
                string idol = itemUnchecked.ToString();
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.RemoveIdol(idol);
                sekiroSplitter.setProcedure(true);
                sekiroSplitter.LoadAutoSplitterProcedure();
            }

            //Sunkey Valley
            foreach (object itemChecked in listBoxSunkenValley.CheckedItems)
            {
                string idol = itemChecked.ToString();
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.AddIdol(idol);
                sekiroSplitter.setProcedure(true);
                sekiroSplitter.LoadAutoSplitterProcedure();
            }


            IEnumerable<object> notCheckedI_SV = (from object item in listBoxSunkenValley.Items
                                                  where !listBoxSunkenValley.CheckedItems.Contains(item)
                                                  select item);

            foreach (object itemUnchecked in notCheckedI_SV)
            {
                string idol = itemUnchecked.ToString();
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.RemoveIdol(idol);
                sekiroSplitter.setProcedure(true);
                sekiroSplitter.LoadAutoSplitterProcedure();
            }

            // Asshina Depths
            foreach (object itemChecked in listBoxAshinaDepths.CheckedItems)
            {
                string idol = itemChecked.ToString();
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.AddIdol(idol);
                sekiroSplitter.setProcedure(true);
                sekiroSplitter.LoadAutoSplitterProcedure();
            }


            IEnumerable<object> notCheckedI_ASD = (from object item in listBoxAshinaDepths.Items
                                                  where !listBoxAshinaDepths.CheckedItems.Contains(item)
                                                  select item);

            foreach (object itemUnchecked in notCheckedI_ASD)
            {
                string idol = itemUnchecked.ToString();
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.RemoveIdol(idol);
                sekiroSplitter.setProcedure(true);
                sekiroSplitter.LoadAutoSplitterProcedure();
            }

            //Fountainhead Palace

            foreach (object itemChecked in listBoxFountainheadPalace.CheckedItems)
            {
                string idol = itemChecked.ToString();
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.AddIdol(idol);
                sekiroSplitter.setProcedure(true);
                sekiroSplitter.LoadAutoSplitterProcedure();
            }


            IEnumerable<object> notCheckedI_FP = (from object item in listBoxFountainheadPalace.Items
                                                   where !listBoxFountainheadPalace.CheckedItems.Contains(item)
                                                   select item);

            foreach (object itemUnchecked in notCheckedI_FP)
            {
                string idol = itemUnchecked.ToString();
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.RemoveIdol(idol);
                sekiroSplitter.setProcedure(true);
                sekiroSplitter.LoadAutoSplitterProcedure();
            }
            #endregion
            //Split.Position is added automatically in btnAddPosition_Click and remove in listBoxPositions_MouseDoubleClick
            #region Propierties
            if (checkBoxESekiroSplitting.Checked)
            {
                sekiroSplitter.setStatusSplitting(true);
            }
            else
            {
                sekiroSplitter.setStatusSplitting(false);
            }


            if (checkBoxSekiroTimer.Checked)
            {
                sekiroSplitter.setStatusTimer(true);
            }
            else
            {
                sekiroSplitter.setStatusTimer(false);
            }
            

            #endregion

        }

        private void toSplitSelectSekiro_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.checkBossSekiro.Hide();
            this.zoneSelectSekiro.Hide();
            this.label_Zone1.Hide();
            this.panel1.Hide();
            this.panel2.Hide();
            this.panel3.Hide();
            this.labelCoordinates.Hide();
            this.btnGetPotition.Hide();
            this.textBoxX.Hide();
            this.textBoxY.Hide();
            this.textBoxZ.Hide();
            this.listBoxPositions.Hide();
            this.comboBoxMargin.Hide();
            this.groupBoxEnablersSekiro.Hide();

            switch (toSplitSelectSekiro.SelectedIndex)
            {
                case 0: //Propierties
                    this.panel3.Show();
                    this.groupBoxEnablersSekiro.Show();

                    break;
                case 1: //Kill a Boss
                    this.panel3.Hide();
                    this.checkBossSekiro.Show();
                    break;
                case 2: // Is Activated a Idol
                    this.zoneSelectSekiro.Show();
                    this.label_Zone1.Show();
                    this.panel1.Show();
                    break;
                case 3: //Target Position
                    this.panel2.Show();
                    this.labelCoordinates.Show();
                    this.btnGetPotition.Show();
                    this.textBoxX.Show();
                    this.textBoxY.Show();
                    this.textBoxZ.Show();
                    this.listBoxPositions.Show();
                    this.comboBoxMargin.Show();
                    break;
            }
        }

        private void zoneSelectSekiro_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listBoxAshinaOutskirts.Hide();
            this.listBoxHirataEstate.Hide();
            this.listBoxAshinaCastle.Hide();
            this.listBoxAbandonedDungeon.Hide();
            this.listBoxSenpouTemple.Hide();
            this.listBoxSunkenValley.Hide();
            this.listBoxAshinaDepths.Hide();
            this.listBoxFountainheadPalace.Hide();
            switch (zoneSelectSekiro.SelectedIndex)
            {
                case 0: //Ashina Outskirts
                    this.listBoxAshinaOutskirts.Show();
                    break;
                case 1: // Hirata Estate
                    this.listBoxHirataEstate.Show();
                    break;
                case 2: //Ashina Castle
                    this.listBoxAshinaCastle.Show();
                    break;
                case 3: //Abandoned Dungeon
                    this.listBoxAbandonedDungeon.Show();
                    break;
                case 4: //Senpou Temple, Mt. Kongo
                    this.listBoxSenpouTemple.Show();
                    break;
                case 5: //Sunken Valley
                    this.listBoxSunkenValley.Show();
                    break;
                case 6: // Ashina Depths
                    this.listBoxAshinaDepths.Show();
                    break;
                case 7: //Fountainhead Palace
                    this.listBoxFountainheadPalace.Show();
                    break;
            }
        }

        
        private void btnGetPotition_Click(object sender, EventArgs e)
        {
            var Vector = sekiroSplitter.getCurrentPosition();
            this.Vector = Vector;
            this.textBoxX.Clear();
            this.textBoxY.Clear();
            this.textBoxZ.Clear();
            this.textBoxX.Paste(Vector.X.ToString("0.00"));
            this.textBoxY.Paste(Vector.Y.ToString("0.00"));
            this.textBoxZ.Paste(Vector.Z.ToString("0.00"));

        }

        private void btnAddPosition_Click(object sender, EventArgs e)
        {
            if (!this.listBoxPositions.Items.Contains(this.Vector)) 
            { 
                sekiroSplitter.setProcedure(false);
                DialogResult result = MessageBox.Show("Move your charapter to evit autosplitting ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.listBoxPositions.Items.Add(this.Vector);
                sekiroSplitter.AddPosition(this.Vector);
                sekiroSplitter.setProcedure(true);
                sekiroSplitter.LoadAutoSplitterProcedure();

            }
            else
            {
                DialogResult result = MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBoxPositions_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int i = listBoxPositions.Items.IndexOf(this.listBoxPositions.SelectedItem);
            sekiroSplitter.setProcedure(false);
            sekiroSplitter.RemovePosition(i);
            sekiroSplitter.setProcedure(true);
            sekiroSplitter.LoadAutoSplitterProcedure();
            this.listBoxPositions.Items.Remove(this.listBoxPositions.SelectedItem);

        }

        private void comboBoxMargin_SelectedIndexChanged(object sender, EventArgs e)
        {
            int select = comboBoxMargin.SelectedIndex;
            sekiroSplitter.setPositionMargin(select);
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
                this.AutoSplitter_Load(null,null);//Load Others Games Settings
            }
            
        }


        #endregion

        private void btnCheckVersion_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/FrankvdStam/SoulSplitter/releases");
        }
    }
}
