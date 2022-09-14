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
using System.Windows.Forms;
using System.Threading.Tasks;
using SoulMemory.DarkSouls2;
using SoulMemory;
using System.Threading;
using HitCounterManager;

namespace AutoSplitterCore
{
    public class Ds2Splitter
    {
        public static DarkSouls2 Ds2 = new DarkSouls2();
        public bool _StatusProcedure = true;
        public bool _StatusDs2 = false;
        public bool _runStarted = false;
        public bool _SplitGo = false;
        public bool _PracticeMode = false;
        public DTDs2 dataDs2;
        public DefinitionsDs2 defD2 = new DefinitionsDs2();
        public ProfilesControl _profile;
        private static readonly object _object = new object();
        private System.Windows.Forms.Timer _update_timer = new System.Windows.Forms.Timer() { Interval = 1000 };
        public bool DebugMode = false;


        public DTDs2 getDataDs2()
        {
            return this.dataDs2;
        }

        public void setDataDs2(DTDs2 data, ProfilesControl profile)
        {
            this.dataDs2 = data;
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
            dataDs2.enableSplitting = status;
            if (status) { LoadAutoSplitterProcedure(); _update_timer.Enabled = true; } else { _update_timer.Enabled = false; }
        }
        public void setProcedure(bool procedure)
        {
            this._StatusProcedure = procedure;
            if (procedure) { LoadAutoSplitterProcedure(); _update_timer.Enabled = true; } else { _update_timer.Enabled = false; }
        }

        public bool getDs2StatusProcess(int delay) //Use Delay 0 only for first Starts
        {
            Thread.Sleep(delay);
            return _StatusDs2 = Ds2.Refresh(out Exception exc);
        }

        public void clearData()
        {
            listPendingB.Clear();
            listPendingP.Clear();
            listPendingLvl.Clear();
            dataDs2.bossToSplit.Clear();
            dataDs2.positionsToSplit.Clear();
            dataDs2.lvlToSplit.Clear();
            dataDs2.positionMargin = 3;
            _runStarted = false;
        }

        public void resetSplited()
        {
            listPendingB.Clear();
            listPendingP.Clear();
            listPendingLvl.Clear();
            if (dataDs2.getBossToSplit().Count > 0)
            {
                foreach (var b in dataDs2.getBossToSplit())
                {
                    b.IsSplited = false;
                }
            }

            if (dataDs2.getLvlToSplit().Count > 0)
            {
                foreach (var l in dataDs2.getLvlToSplit())
                {
                    l.IsSplited = false;
                }
            }
            _runStarted = false;
        }
        
        public void LoadAutoSplitterProcedure()
        {
            var taskRefresh = new Task(() =>
            {
                RefreshDs2();
            });
            var taskCheckload = new Task(() =>
            {
                checkLoad();
            });
            var taskCheckStart= new Task(() =>
            {
                checkStart();
            });
            
            var task1 = new Task(() =>
            {
                bossToSplit();
            });
            var task2 = new Task(() =>
            {
                positionToSplit();
            });

            var task3 = new Task(() =>
            {
                lvlToSplit();
            });

            taskRefresh.Start();
            taskCheckload.Start();
            taskCheckStart.Start();
            task1.Start();
            task2.Start();
            task3.Start();
        }

        public void AddBoss(string boss, string mode)
        {
            DefinitionsDs2.BossDs2 cBoss = defD2.stringToEnumBoss(boss);
            cBoss.Mode = mode;
            dataDs2.bossToSplit.Add(cBoss);
        }

        public void RemoveBoss(int position)
        {
            dataDs2.bossToSplit.RemoveAt(position);
        }

        public void AddPosition(Vector3f vector, string mode)
        {
            var position = new DefinitionsDs2.PositionDs2()
            {
                vector = vector, Mode = mode
            };
            dataDs2.positionsToSplit.Add(position);
        }

        public void RemovePosition(int position)
        {
            listPendingP.RemoveAll(iposition => iposition.vector == dataDs2.positionsToSplit[position].vector);
            dataDs2.positionsToSplit.RemoveAt(position);
        }

        public void AddAttribute(string attribute, string mode, uint value)
        {
            DefinitionsDs2.LvlDs2 cLvl = new DefinitionsDs2.LvlDs2()
            {
                Attribute = defD2.stringToEnumAttribute(attribute),
                Mode = mode,
                Value = value
            };
            dataDs2.lvlToSplit.Add(cLvl);
        }

        public void RemoveAttribute(int position)
        {
            listPendingLvl.RemoveAll(ilvl => ilvl.Attribute == dataDs2.lvlToSplit[position].Attribute && ilvl.Value == dataDs2.lvlToSplit[position].Value);
            dataDs2.lvlToSplit.RemoveAt(position);
        }
        
        public Vector3f getCurrentPosition()
        {
            getDs2StatusProcess(0);
            return Ds2.GetPosition();
        }

        public bool getIsLoading()
        {
            return Ds2.IsLoading();
        }

        public bool CheckFlag(uint id)
        {
            return Ds2.ReadEventFlag(id);
        }


        #region Init()
        private void RefreshDs2()
        {
            int delay = 2000;
            getDs2StatusProcess(delay);
            while (_StatusProcedure && dataDs2.enableSplitting)
            {
                Thread.Sleep(10);
                getDs2StatusProcess(delay);
                if (!_StatusDs2) { delay = 2000; }else { delay = 20000; }
            }
        }

        private void checkStart()
        {
            while (dataDs2.enableSplitting)
            {
                Thread.Sleep(500);
                if (!_PracticeMode)
                {
                    var position = Ds2.GetPosition();
                    if (
                        position.Y < -322.0f && position.Y > -323.0f &&
                        position.X < -213.0f && position.X > -214.0f)
                    {
                        _runStarted = true;
                    }
                    if (position.X == 0.00 && position.Y == 0.00 && position.Z == 0.00)
                    {
                        _runStarted = false;
                    }
                    if (Ds2.IsLoading() && dataDs2.gameTimer)
                    {
                        do
                        {
                            _runStarted = false;
                        } while (Ds2.IsLoading());
                        _runStarted = true;
                    }
                }
            }
        }

        List<DefinitionsDs2.BossDs2> listPendingB = new List<DefinitionsDs2.BossDs2>();
        List<DefinitionsDs2.PositionDs2> listPendingP = new List<DefinitionsDs2.PositionDs2>();
        List<DefinitionsDs2.LvlDs2> listPendingLvl = new List<DefinitionsDs2.LvlDs2>();

        private void checkLoad()
        {
            while (dataDs2.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(200);
                if (listPendingB.Count > 0 || listPendingP.Count > 0 || listPendingLvl.Count > 0)
                {
                    if (Ds2.IsLoading())
                    {
                        foreach (var boss in listPendingB)
                        {
                            SplitCheck();
                            var b = dataDs2.bossToSplit.FindIndex(iboss => iboss.Id == boss.Id);
                            dataDs2.bossToSplit[b].IsSplited = true;
                        }

                        foreach (var position in listPendingP)
                        {
                            SplitCheck();
                            var p = dataDs2.positionsToSplit.FindIndex(fposition => fposition.vector == position.vector);
                            dataDs2.positionsToSplit[p].IsSplited = true;
                        }

                        foreach (var lvl in listPendingLvl)
                        {
                            SplitCheck();
                            var l = dataDs2.lvlToSplit.FindIndex(Ilvl => Ilvl.Attribute == lvl.Attribute && Ilvl.Value == lvl.Value);
                            dataDs2.lvlToSplit[l].IsSplited = true;
                        }

                        listPendingB.Clear();
                        listPendingP.Clear();
                        listPendingLvl.Clear();
                    }
                }
            }
        }

        private void bossToSplit()
        {
            while (dataDs2.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(3000);
                if (!_PracticeMode)
                {
                    foreach (var b in dataDs2.getBossToSplit())
                    {
                        if (!b.IsSplited && Ds2.GetBossKillCount(b.Id) > 0)
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

        private void lvlToSplit()
        {
            while (dataDs2.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(3000);
                if (!_PracticeMode)
                {
                    foreach (var lvl in dataDs2.getLvlToSplit())
                    {
                        if (!lvl.IsSplited && Ds2.GetAttribute(lvl.Attribute) >= lvl.Value)
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
            while (dataDs2.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(100);
                if (!_PracticeMode)
                {
                    foreach (var p in dataDs2.getPositionsToSplit())
                    {
                        if (!p.IsSplited)
                        {
                            var currentlyPosition = Ds2.GetPosition();
                            var rangeX = ((currentlyPosition.X - p.vector.X) <= dataDs2.positionMargin) && ((currentlyPosition.X - p.vector.X) >= -dataDs2.positionMargin);
                            var rangeY = ((currentlyPosition.Y - p.vector.Y) <= dataDs2.positionMargin) && ((currentlyPosition.Y - p.vector.Y) >= -dataDs2.positionMargin);
                            var rangeZ = ((currentlyPosition.Z - p.vector.Z) <= dataDs2.positionMargin) && ((currentlyPosition.Z - p.vector.Z) >= -dataDs2.positionMargin);
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
      
        #endregion
    }
}
