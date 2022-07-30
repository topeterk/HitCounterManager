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

namespace HitCounterManager
{
    public class EldenSplitter
    {
        public static EldenRing elden = new EldenRing();
        public bool _StatusProcedure = true;
        public bool _StatusElden = false;
        public DTElden dataElden;
        public DefinitionsElden defE = new DefinitionsElden();
        public ProfilesControl _profile;
        private bool _writeMemory = false;

        public DTElden getDataElden()
        {
            return this.dataElden;
        }

        public void setDataElden(DTElden data, ProfilesControl profile)
        {
            this.dataElden = data;
            this._profile = profile;
        }

        public void setStatusSplitting(bool status)
        {
            dataElden.enableSplitting = status;
            if (status) { LoadAutoSplitterProcedure(); }
        }

        public void setProcedure(bool procedure)
        {
            this._StatusProcedure = procedure;
            if (procedure) { LoadAutoSplitterProcedure(); }
        }


        public bool getEldenStatusProcess(out Exception exception, int delay) //Use Delay 0 only for first Starts
        {
            Thread.Sleep(delay);
            return _StatusElden = elden.Refresh(out exception);
        }

        public void setPositionMargin(int select)
        {
            dataElden.positionMargin = select;
        }

        public SoulMemory.EldenRing.Position getCurrentPosition()
        {
            return elden.GetPosition();
        }

        public int getTimeInGame()
        {
            return elden.GetInGameTimeMilliseconds();
        }
        public void clearData()
        {
            dataElden.positionMargin = 3;
            dataElden.bossToSplit.Clear();
            dataElden.graceToSplit.Clear();
            dataElden.positionToSplit.Clear();

        }

        public void resetSplited() 
        {
            if (dataElden.getBossToSplit().Count != 0)
            {
                foreach (var b in dataElden.getBossToSplit())
                {
                    b.IsSplited = false;
                }
            }

            if (dataElden.getGraceToSplit().Count != 0)
            {
                foreach (var g in dataElden.getGraceToSplit())
                {
                    g.IsSplited = false;
                }
            }

            if (dataElden.getPositionToSplit().Count != 0)
            {
                foreach (var p in dataElden.getPositionToSplit())
                {
                    p.IsSplited = false;
                }
            }
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
            taskRefresh.Start();
            taskCheckload.Start();
            task1.Start();
            task2.Start();
            task3.Start();
           


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
            { vector = vector, mode = mode };
            dataElden.positionToSplit.Add(cPosition);   
        }

        public void RemoveBoss(int position)
        {
            dataElden.bossToSplit.RemoveAt(position);
        }
        public void RemoveGrace(int position)
        {
            dataElden.graceToSplit.RemoveAt(position);
        }

        public void RemovePosition(int position)
        {
            dataElden.positionToSplit.RemoveAt(position);
        }

        #region init()
        public void RefreshElden()
        {
            _StatusElden = getEldenStatusProcess(out Exception exception, 0);
            while (!_StatusProcedure)
            {
                _StatusElden = getEldenStatusProcess(out exception, 45000);
                if (!_StatusElden)
                {
                    _writeMemory = false;
                }

                if (!_writeMemory)
                {
                    elden.WriteInGameTimeMilliseconds(0);
                    _writeMemory = true;
                }
            }
        }

       
        List<DefinitionsElden.BossER> listPendingB_LD = new List<DefinitionsElden.BossER>();
        List<DefinitionsElden.Grace> listPendingG_LD = new List<DefinitionsElden.Grace>();
        List<DefinitionsElden.PositionER> listPendingP_LD = new List<DefinitionsElden.PositionER>();

      
        private void checkLoad()
        {
            while (dataElden.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(200);
                if (listPendingB_LD.Count > 0 || listPendingG_LD.Count > 0 || listPendingP_LD.Count > 0)
                {
                    if (!elden.IsPlayerLoaded())
                    {                      
                        foreach (var boss in listPendingB_LD)
                        {
                            try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                            var b = dataElden.bossToSplit.FindIndex(iboss => iboss.Id == boss.Id);
                            dataElden.bossToSplit[b].IsSplited = true;
                        }

                        foreach (var grace in listPendingG_LD)
                        {
                            try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                            var g = dataElden.graceToSplit.FindIndex(igrace => igrace.Id == grace.Id);
                            dataElden.graceToSplit[g].IsSplited = true;
                        }

                        foreach (var position in listPendingP_LD)
                        {
                            try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                            var p = dataElden.positionToSplit.FindIndex(iposition => iposition.vector == position.vector);
                            dataElden.graceToSplit[p].IsSplited = true;
                        }

                        listPendingB_LD.Clear();
                        listPendingG_LD.Clear();
                        listPendingP_LD.Clear();


                    }
                }
            }
        }

        private void bossToSplit()
        {
            while (dataElden.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(5000);
                foreach (var b in dataElden.getBossToSplit())
                {
                    if (!b.IsSplited && elden.ReadEventFlag(b.Id))
                    {
                        if (b.Mode == "Loading game after")
                        {
                            if (!listPendingB_LD.Contains(b))
                            {
                                listPendingB_LD.Add(b);
                            }
                        }
                        else
                        {
                            b.IsSplited = true;
                            try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                                                       
                        }
                    }

                }
            }
        }

        private void graceToSplit()
        {
            while (dataElden.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(5000);
                foreach (var i in dataElden.getGraceToSplit())
                {

                    if (!i.IsSplited && elden.ReadEventFlag(i.Id))
                    {
                        if (i.Mode == "Loading game after")
                        {
                            if (!listPendingG_LD.Contains(i))
                            {
                                listPendingG_LD.Add(i);
                            }
                        }
                        else
                        {
                            i.IsSplited = true;
                            try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
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
                            if (p.mode == "Loading game after")
                            {

                                if (!listPendingP_LD.Contains(p))
                                {
                                    listPendingP_LD.Add(p);
                                }

                            }
                            else
                            {
                                p.IsSplited = true;
                                try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                            }
                        }

                    }
                }
            }
        }
    }
    #endregion
}

