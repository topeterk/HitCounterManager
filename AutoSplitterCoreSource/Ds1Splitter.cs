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
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using SoulMemory.DarkSouls1;
using SoulMemory;
using HitCounterManager;

namespace AutoSplitterCore
{
    public class Ds1Splitter
    {
        public static DarkSouls1 Ds1 = new DarkSouls1();
        public bool _StatusProcedure = true;
        public bool _StatusDs1 = false;
        public bool _runStarted = false;
        public bool _SplitGo = false;
        public bool _PracticeMode = false;
        public DTDs1 dataDs1;
        public DefinitionsDs1 defDs1 = new DefinitionsDs1();
        public ProfilesControl _profile;       
        private List<Item> inventory = null;
        private static readonly object _object = new object();
        private System.Windows.Forms.Timer _update_timer = new System.Windows.Forms.Timer() { Interval = 1000 };
        public bool DebugMode = false;

        public DTDs1 getDataDs1()
        {
            return this.dataDs1;
        }

        public void setDataDs1(DTDs1 data, ProfilesControl profile)
        {
            this.dataDs1 = data;
            this._profile = profile;
            _update_timer.Tick += (sender, args) => SplitGo();
        }

        public void SplitGo()
        {
            if (_SplitGo)
            {
                if (!DebugMode) { try { _profile.ProfileSplitGo(+1); } catch (Exception) { } } else { Thread.Sleep(15000); }
                _SplitGo = false;
            }
        }

        private void SplitCheck()
        {
            lock (_object)
            {
                if (_SplitGo) { Thread.Sleep(2000); }
                _SplitGo = true;
            }
        }

        public void setStatusSplitting(bool status)
        {
            dataDs1.enableSplitting = status;
            if (status) { LoadAutoSplitterProcedure(); _update_timer.Enabled = true; } else { _update_timer.Enabled = false; }
        }

        public void setProcedure(bool procedure)
        {
            this._StatusProcedure = procedure;
            if (procedure) { LoadAutoSplitterProcedure(); _update_timer.Enabled = true; } else { _update_timer.Enabled = false; }
        }

        public bool getDs1StatusProcess(int delay) //Use Delay 0 only for first Starts
        {
            Thread.Sleep(delay);
            return _StatusDs1 = Ds1.Refresh(out Exception e);
        }

        public int getTimeInGame()
        {
            return Ds1.GetInGameTimeMilliseconds();
        }

        public void clearData()
        {
            listPendingB.Clear();
            listPendingBon.Clear();
            listPendingLvl.Clear();
            listPendingP.Clear();
            listPendingItem.Clear();

            dataDs1.bossToSplit.Clear();
            dataDs1.bonfireToSplit.Clear();
            dataDs1.lvlToSplit.Clear();
            dataDs1.positionsToSplit.Clear();
            dataDs1.itemToSplit.Clear();
            dataDs1.positionMargin = 3;
            _runStarted = false;           
        }

        public void resetSplited()
        {
            listPendingB.Clear();
            listPendingBon.Clear();
            listPendingLvl.Clear();
            listPendingP.Clear();
            listPendingItem.Clear();

            if (dataDs1.getBossToSplit().Count > 0)
            {
                foreach (var b in dataDs1.getBossToSplit())
                {
                    b.IsSplited = false;
                }
            }

            if (dataDs1.getBonfireToSplit().Count > 0)
            {
                foreach (var bf in dataDs1.getBonfireToSplit())
                {
                    bf.IsSplited = false;
                }
            }

            if (dataDs1.getLvlToSplit().Count > 0)
            {
                foreach (var l in dataDs1.getLvlToSplit())
                {
                    l.IsSplited = false;
                }
            }

            if (dataDs1.getPositionsToSplit().Count > 0)
            {
                foreach (var p in dataDs1.getPositionsToSplit())
                {
                    p.IsSplited = false;
                }
            }

            if (dataDs1.getItemsToSplit().Count > 0)
            {
                foreach (var i in dataDs1.getItemsToSplit())
                {
                    i.IsSplited = false;
                }
            }
            _runStarted = false;
        }

        public void LoadAutoSplitterProcedure()
        {
            var taskRefresh = new Task(() =>
            {
                RefreshDs1();
            });
            var taskCheckload = new Task(() =>
            {
                checkLoad();
            });
            var taskInventorySee = new Task(() =>
            {
                inventorySee();
            });
            var task1 = new Task(() =>
            {
                bossToSplit();
            });
            var task2 = new Task(() =>
            {
                bonfireToSplit();
            });

            var task3 = new Task(() =>
            {
                lvlToSplit();
            });

            var task4 = new Task(() =>
            {
                positionToSplit();
            });

            var task5 = new Task(() =>
            {
                itemToSplit();
            });


            taskRefresh.Start();
            taskCheckload.Start();
            taskInventorySee.Start();
            task1.Start();
            task2.Start();
            task3.Start();
            task4.Start();
            task5.Start();

        }

        public Vector3f getCurrentPosition()
        {
            getDs1StatusProcess(0);
            return Ds1.GetPosition();
        }

        public void AddBoss(string boss, string mode)
        {
            DefinitionsDs1.BossDs1 cBoss = defDs1.stringToEnumBoss(boss);
            cBoss.Mode = mode;
            dataDs1.bossToSplit.Add(cBoss);
        }

        public void RemoveBoss(int position)
        {
            listPendingB.RemoveAll(iboss => iboss.Id == dataDs1.bossToSplit[position].Id);
            dataDs1.bossToSplit.RemoveAt(position);
        }

        public void AddBonfire(string Bonfire, string mode,string state)
        {
            DefinitionsDs1.BonfireDs1 cBonfire = defDs1.stringToEnumBonfire(Bonfire);
            cBonfire.Mode = mode;
            cBonfire.Value = defDs1.stringtoEnumBonfireState(state);
            dataDs1.bonfireToSplit.Add(cBonfire);
        }

        public BonfireState convertStringToState(string state)
        {
            return defDs1.stringtoEnumBonfireState(state);
        }

        public void RemoveBonfire(int position)
        {
            listPendingBon.RemoveAll(iposition => iposition.Id == dataDs1.bonfireToSplit[position].Id);
            dataDs1.bonfireToSplit.RemoveAt(position);
        }

        public void AddAttribute(string attribute, string mode, uint value)
        {
            DefinitionsDs1.LvlDs1 cLvl = new DefinitionsDs1.LvlDs1()
            {
                Attribute = defDs1.stringToEnumAttribute(attribute),
                Mode = mode,
                Value = value
            };
            dataDs1.lvlToSplit.Add(cLvl);
        }

        public void RemoveAttribute(int position)
        {
            listPendingLvl.RemoveAll(ilvl => ilvl.Attribute == dataDs1.lvlToSplit[position].Attribute && ilvl.Value == dataDs1.lvlToSplit[position].Value);
            dataDs1.lvlToSplit.RemoveAt(position);
        }

        public void AddPosition(Vector3f vector, string mode) 
        {
            var position = new DefinitionsDs1.PositionDs1()
            {
                vector = vector,
                Mode = mode
            };
            dataDs1.positionsToSplit.Add(position);
        }

        public void RemovePosition(int position)
        {
            listPendingP.RemoveAll(iposition => iposition.vector == dataDs1.positionsToSplit[position].vector);
            dataDs1.positionsToSplit.RemoveAt(position);
        }

        public void AddItem(string item, string mode)
        {
            DefinitionsDs1.ItemDs1 cItem = defDs1.stringToEnumItem(item);
            cItem.Mode = mode;
            dataDs1.itemToSplit.Add(cItem);
        }

        public void RemoveItem(int position)
        {
            listPendingItem.RemoveAll(iboss => iboss.Id == dataDs1.itemToSplit[position].Id);
            dataDs1.bossToSplit.RemoveAt(position);
        }

        public bool CheckFlag(uint id)
        {
            return Ds1.ReadEventFlag(id);
        }


        #region init()
        private void RefreshDs1()
        {
            int delay = 2000;
            getDs1StatusProcess(delay);
            while (_StatusProcedure && dataDs1.enableSplitting)
            {
                Thread.Sleep(10);
                getDs1StatusProcess(delay);
                if (!_StatusDs1)
                {
                    delay = 2000;
                }
                else
                {
                    delay = 20000;
                }
            }
        }

        public void inventorySee()
        {
            while (dataDs1.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(3000);
                inventory = Ds1.GetInventory();
            }            
        }

        List<DefinitionsDs1.BossDs1> listPendingB = new List<DefinitionsDs1.BossDs1>();
        List<DefinitionsDs1.BonfireDs1> listPendingBon = new List<DefinitionsDs1.BonfireDs1>();
        List<DefinitionsDs1.LvlDs1> listPendingLvl = new List<DefinitionsDs1.LvlDs1>();
        List<DefinitionsDs1.PositionDs1> listPendingP = new List<DefinitionsDs1.PositionDs1>();
        List<DefinitionsDs1.ItemDs1> listPendingItem = new List<DefinitionsDs1.ItemDs1>();


        private void checkLoad()
        {
            while (dataDs1.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(200);
                if (listPendingB.Count > 0 || listPendingBon.Count > 0 || listPendingLvl.Count > 0 || listPendingP.Count > 0 || listPendingItem.Count > 0)
                {
                    if (!Ds1.IsPlayerLoaded())
                    {
                        foreach (var boss in listPendingB)
                        {
                            SplitCheck();
                            var b = dataDs1.bossToSplit.FindIndex(iboss => iboss.Id == boss.Id);
                            dataDs1.bossToSplit[b].IsSplited = true;
                        }

                        foreach (var bone in listPendingBon)
                        {
                            SplitCheck();
                            var bo = dataDs1.bonfireToSplit.FindIndex(Ibone => Ibone.Id == bone.Id);
                            dataDs1.bonfireToSplit[bo].IsSplited = true;
                        }

                        foreach (var lvl in listPendingLvl)
                        {
                            SplitCheck();
                            var l = dataDs1.lvlToSplit.FindIndex(Ilvl => Ilvl.Attribute == lvl.Attribute && Ilvl.Value == lvl.Value);
                            dataDs1.lvlToSplit[l].IsSplited = true;
                        }

                        foreach (var position in listPendingP)
                        {
                            SplitCheck();
                            var p = dataDs1.positionsToSplit.FindIndex(fposition => fposition.vector == position.vector);
                            dataDs1.positionsToSplit[p].IsSplited = true;
                        }

                        foreach (var cf in listPendingItem)
                        {
                            SplitCheck();
                            var c = dataDs1.itemToSplit.FindIndex(icf => icf.Id == cf.Id);
                            dataDs1.itemToSplit[c].IsSplited = true;
                        }

                       
                        listPendingB.Clear();
                        listPendingBon.Clear();
                        listPendingLvl.Clear();
                        listPendingP.Clear();
                        listPendingItem.Clear();
                    }
                }
            }
        }

        private void bossToSplit()
        {
            while (dataDs1.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(3000);
                if (!_PracticeMode)
                {
                    foreach (var b in dataDs1.getBossToSplit())
                    {
                        if (!b.IsSplited && Ds1.ReadEventFlag(b.Id))
                        {
                            if (b.Mode == "Loading game after")
                            {
                                if (!listPendingB.Contains(b))
                                {
                                    listPendingB.Add(b);
                                }
                            }
                            else
                            {
                                b.IsSplited = true;
                                SplitCheck();
                            }
                        }
                    }
                }
            }
        }

        private void bonfireToSplit()
        {
            while (dataDs1.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(3000);
                if (!_PracticeMode)
                {
                    foreach (var bonfire in dataDs1.getBonfireToSplit())
                    {
                        Bonfire aux = bonfire.Id;
                        if (!bonfire.IsSplited && Ds1.GetBonfireState(bonfire.Id) == bonfire.Value)
                        {
                            if (bonfire.Mode == "Loading game after")
                            {
                                if (!listPendingBon.Contains(bonfire))
                                {
                                    listPendingBon.Add(bonfire);
                                }
                            }
                            else
                            {
                                bonfire.IsSplited = true;
                                SplitCheck();
                            }
                        }
                    }
                }
            }
        }

        private void lvlToSplit()
        {
            while (dataDs1.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(3000);
                if (!_PracticeMode)
                {
                    foreach (var lvl in dataDs1.getLvlToSplit())
                    {
                        if (!lvl.IsSplited && Ds1.GetAttribute(lvl.Attribute) >= lvl.Value)
                        {
                            if (lvl.Mode == "Loading game after")
                            {
                                if (!listPendingLvl.Contains(lvl))
                                {
                                    listPendingLvl.Add(lvl);
                                }
                            }
                            else
                            {
                                lvl.IsSplited = true;
                                SplitCheck();
                            }
                        }
                    }
                }
            }
        }

        private void positionToSplit()
        {
            while (dataDs1.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(100);
                if (!_PracticeMode)
                {
                    foreach (var p in dataDs1.getPositionsToSplit())
                    {
                        if (!p.IsSplited)
                        {
                            var currentlyPosition = Ds1.GetPosition();
                            var rangeX = ((currentlyPosition.X - p.vector.X) <= dataDs1.positionMargin) && ((currentlyPosition.X - p.vector.X) >= -dataDs1.positionMargin);
                            var rangeY = ((currentlyPosition.Y - p.vector.Y) <= dataDs1.positionMargin) && ((currentlyPosition.Y - p.vector.Y) >= -dataDs1.positionMargin);
                            var rangeZ = ((currentlyPosition.Z - p.vector.Z) <= dataDs1.positionMargin) && ((currentlyPosition.Z - p.vector.Z) >= -dataDs1.positionMargin);
                            if (rangeX && rangeY && rangeZ)
                            {
                                if (p.Mode == "Loading game after")
                                {
                                    if (!listPendingP.Contains(p))
                                    {
                                        listPendingP.Add(p);
                                    }
                                }
                                else
                                {
                                    p.IsSplited = true;
                                    SplitCheck();
                                }
                            }
                        }
                    }
                }
            }
        }

        private readonly List<Item> allItems = SoulMemory.DarkSouls1.Item.AllItems;
        private void itemToSplit()
        {
            while (dataDs1.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(3000);
                if (!_PracticeMode)
                {
                    foreach (var item in dataDs1.getItemsToSplit())
                    {
                        Item aux = allItems.Find(i => i.Id == item.Id);
                        if (!item.IsSplited && inventory.Exists(i => i.ItemType == aux.ItemType))
                        {
                            if (item.Mode == "Loading game after")
                            {
                                if (!listPendingItem.Contains(item))
                                {
                                    listPendingItem.Add(item);
                                }
                            }
                            else
                            {
                                item.IsSplited = true;
                                SplitCheck();
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
