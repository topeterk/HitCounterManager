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
using SoulMemory.EldenRing;
using System.Threading;
using HitCounterManager;

namespace AutoSplitterCore
{
    public class EldenSplitter
    {
        public static EldenRing elden = new EldenRing();
        public bool _StatusProcedure = true;
        public bool _StatusElden = false;
        public bool _SplitGo = false;
        public bool _PracticeMode = false;
        public DTElden dataElden;
        public DefinitionsElden defE = new DefinitionsElden();
        public ProfilesControl _profile;
        public bool _runStarted = false;
        private bool _writeMemory = false;      
        private static readonly object _object = new object();
        private System.Windows.Forms.Timer _update_timer = new System.Windows.Forms.Timer() { Interval = 1000 };
        public bool DebugMode = false;

        public DTElden getDataElden()
        {
            return this.dataElden;
        }

        public void setDataElden(DTElden data, ProfilesControl profile)
        {
            this.dataElden = data;
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
            dataElden.enableSplitting = status;
            if (status) { LoadAutoSplitterProcedure(); _update_timer.Enabled = true; } else { _update_timer.Enabled = false; }
        }

        public void setProcedure(bool procedure)
        {
            this._StatusProcedure = procedure;
            if (procedure) { LoadAutoSplitterProcedure(); _update_timer.Enabled = true; } else { _update_timer.Enabled = false; }
        }


        public bool getEldenStatusProcess(int delay) //Use Delay 0 only for first Starts
        {
            Thread.Sleep(delay);
            return _StatusElden = elden.Refresh(out Exception exc);
        }

        public void setPositionMargin(int select)
        {
            dataElden.positionMargin = select;
        }

        public SoulMemory.EldenRing.Position getCurrentPosition()
        {
            getEldenStatusProcess(0);
            return elden.GetPosition();
        }

        public int getTimeInGame()
        {
            return elden.GetInGameTimeMilliseconds();
        }
        public void clearData()
        {
            listPendingB.Clear();
            listPendingG.Clear();
            listPendingP.Clear();
            listPendingCf.Clear();
            dataElden.positionMargin = 3;
            dataElden.bossToSplit.Clear();
            dataElden.graceToSplit.Clear();
            dataElden.positionToSplit.Clear();
            dataElden.flagsToSplit.Clear();
            _runStarted = false;
        }

        public void resetSplited() 
        {
            listPendingB.Clear();
            listPendingG.Clear();
            listPendingP.Clear();
            listPendingCf.Clear();

            if (dataElden.getBossToSplit().Count > 0)
            {
                foreach (var b in dataElden.getBossToSplit())
                {
                    b.IsSplited = false;
                }
            }

            if (dataElden.getGraceToSplit().Count > 0)
            {
                foreach (var g in dataElden.getGraceToSplit())
                {
                    g.IsSplited = false;
                }
            }

            if (dataElden.getPositionToSplit().Count > 0)
            {
                foreach (var p in dataElden.getPositionToSplit())
                {
                    p.IsSplited = false;
                }
            }

            if (dataElden.getFlagsToSplit().Count > 0)
            {
                foreach (var cf in dataElden.getFlagsToSplit())
                {
                    cf.IsSplited = false;
                }
            }
            _runStarted = false;
        }


        public void LoadAutoSplitterProcedure()
        {
            var taskRefresh = new Task(() =>
            {
                RefreshElden();
            });
            var taskCheckload = new Task(() =>
            {
                checkLoad();
            });
            var task1 = new Task(() => 
            {
                bossToSplit();
            });
            var task2 = new Task(() =>
            {
                graceToSplit();
            });

            var task3 = new Task(() =>
            {
                positionToSplit();
            });
            var task4 = new Task(() =>
            {
                flagsToSplit();
            });
            
            taskRefresh.Start();
            taskCheckload.Start();
            task1.Start();
            task2.Start();
            task3.Start();
            task4.Start();
        }

        public void AddBoss(string boss, string mode)
        {
            DefinitionsElden.BossER cBoss = defE.stringToEnumBoss(boss);
            cBoss.Mode = mode;
            dataElden.bossToSplit.Add(cBoss);
        }

        public void AddGrace (string grace, string mode)
        {
            DefinitionsElden.Grace cGrace = defE.stringToGraceEnum(grace);
            cGrace.Mode = mode;
            dataElden.graceToSplit.Add(cGrace);
        }

        public void AddPosition(SoulMemory.EldenRing.Position vector, string mode)
        {
            DefinitionsElden.PositionER cPosition = new DefinitionsElden.PositionER()
            { vector = vector, Mode = mode };
            dataElden.positionToSplit.Add(cPosition);   
        }

        public void AddCustomFlag(uint id, string mode)
        {
            DefinitionsElden.CustomFlagER cf = new DefinitionsElden.CustomFlagER()
            { Id = id, Mode = mode };
            dataElden.flagsToSplit.Add(cf);
        }

        public void RemoveBoss(int position)
        {
            listPendingB.RemoveAll(iboss => iboss.Id == dataElden.bossToSplit[position].Id);
            dataElden.bossToSplit.RemoveAt(position);
        }
        public void RemoveGrace(int position)
        {
            listPendingG.RemoveAll(igrace => igrace.Id == dataElden.graceToSplit[position].Id);
            dataElden.graceToSplit.RemoveAt(position);
        }

        public void RemovePosition(int position)
        {
            listPendingP.RemoveAll(iposition => iposition.vector == dataElden.positionToSplit[position].vector);
            dataElden.positionToSplit.RemoveAt(position);
        }

        public void RemoveCustomFlag(int position)
        {
            listPendingCf.RemoveAll(iCf => iCf.Id == dataElden.flagsToSplit[position].Id);
            dataElden.flagsToSplit.RemoveAt(position);
        }

        public bool CheckFlag(uint id)
        {
            return elden.ReadEventFlag(id);
        }

        #region init()
        private void RefreshElden()
        {
            int delay = 2000;
            getEldenStatusProcess(delay);
            while (_StatusProcedure && dataElden.enableSplitting)
            {
                Thread.Sleep(10);
                getEldenStatusProcess(20000);
                if (!_StatusElden)
                {
                    _writeMemory = false;
                    delay = 2000;
                }
                else
                {
                    delay = 20000;
                }

                if (!_writeMemory)
                {
                    if (elden.GetInGameTimeMilliseconds() < 1) { elden.WriteInGameTimeMilliseconds(0); }                   
                    _writeMemory = true;
                }
            }
        }

       
        List<DefinitionsElden.BossER> listPendingB = new List<DefinitionsElden.BossER>();
        List<DefinitionsElden.Grace> listPendingG = new List<DefinitionsElden.Grace>();
        List<DefinitionsElden.PositionER> listPendingP = new List<DefinitionsElden.PositionER>();
        List<DefinitionsElden.CustomFlagER> listPendingCf = new List<DefinitionsElden.CustomFlagER>();

      
        private void checkLoad()
        {
            while (dataElden.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(200);
                if (listPendingB.Count > 0 || listPendingG.Count > 0 || listPendingP.Count > 0 || listPendingCf.Count >0)
                {
                    if (!elden.IsPlayerLoaded())
                    {                      
                        foreach (var boss in listPendingB)
                        {
                            SplitCheck();
                            var b = dataElden.bossToSplit.FindIndex(iboss => iboss.Id == boss.Id);
                            dataElden.bossToSplit[b].IsSplited = true;
                        }

                        foreach (var grace in listPendingG)
                        {
                            SplitCheck();
                            var g = dataElden.graceToSplit.FindIndex(igrace => igrace.Id == grace.Id);
                            dataElden.graceToSplit[g].IsSplited = true;
                        }

                        foreach (var position in listPendingP)
                        {
                            SplitCheck();
                            var p = dataElden.positionToSplit.FindIndex(iposition => iposition.vector == position.vector);
                            dataElden.positionToSplit[p].IsSplited = true;
                        }

                        foreach (var cf in listPendingCf)
                        {
                            SplitCheck();
                            var c = dataElden.flagsToSplit.FindIndex(iflag => iflag.Id == cf.Id);
                            dataElden.flagsToSplit[c].IsSplited = true;
                        }

                        listPendingB.Clear();
                        listPendingG.Clear();
                        listPendingP.Clear();
                        listPendingCf.Clear();


                    }
                }
            }
        }


        private void bossToSplit()
        {
            while (dataElden.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(3000);
                if (!_PracticeMode)
                {
                    foreach (var b in dataElden.getBossToSplit())
                    {
                        if (!b.IsSplited && elden.ReadEventFlag(b.Id))
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

        private void graceToSplit()
        {
            while (dataElden.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(3000);
                if (!_PracticeMode)
                {
                    foreach (var i in dataElden.getGraceToSplit())
                    {

                        if (!i.IsSplited && elden.ReadEventFlag(i.Id))
                        {
                            if (i.Mode == "Loading game after")
                            {
                                if (!listPendingG.Contains(i))
                                {
                                    listPendingG.Add(i);
                                }
                            }
                            else
                            {
                                i.IsSplited = true;
                                SplitCheck();
                            }
                        }
                    }
                }
            }
        }

        private void flagsToSplit()
        {
            while (dataElden.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(3000);
                if (!_PracticeMode)
                {
                    foreach (var cf in dataElden.getFlagsToSplit())
                    {

                        if (!cf.IsSplited && elden.ReadEventFlag(cf.Id))
                        {
                            if (cf.Mode == "Loading game after")
                            {
                                if (!listPendingCf.Contains(cf))
                                {
                                    listPendingCf.Add(cf);
                                }
                            }
                            else
                            {
                                cf.IsSplited = true;
                                SplitCheck();
                            }
                        }
                    }
                }
            }
        }

        private void positionToSplit()
        {
            while (dataElden.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(100);
                if (!_PracticeMode)
                {
                    foreach (var p in dataElden.getPositionToSplit())
                    {
                        if (!p.IsSplited)
                        {
                            var currentlyPosition = elden.GetPosition();
                            var rangeX = ((currentlyPosition.X - p.vector.X) <= dataElden.positionMargin) && ((currentlyPosition.X - p.vector.X) >= -dataElden.positionMargin);
                            var rangeY = ((currentlyPosition.Y - p.vector.Y) <= dataElden.positionMargin) && ((currentlyPosition.Y - p.vector.Y) >= -dataElden.positionMargin);
                            var rangeZ = ((currentlyPosition.Z - p.vector.Z) <= dataElden.positionMargin) && ((currentlyPosition.Z - p.vector.Z) >= -dataElden.positionMargin);
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

        
    }
    #endregion
}

