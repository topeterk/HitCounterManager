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
using System.Threading;
using System.Threading.Tasks;
using SoulMemory.Sekiro;
using SoulMemory;

namespace HitCounterManager
{
    
    public class SekiroSplitter
    {
        
        public static Sekiro sekiro = new Sekiro();
        public bool _StatusProcedure = true;
        public bool _StatusSekiro = false;
        public DTSekiro dataSekiro;
        public DefinitionsSekiro defS = new DefinitionsSekiro();
        public ProfilesControl _profile;
        private bool _writeMemory = false;


        public DTSekiro getDataSekiro()
        {
            return this.dataSekiro;
        }

        public void setDataSekiro(DTSekiro data,ProfilesControl profile)
        {
            this.dataSekiro = data;
            this._profile = profile;
        }

        public void AddIdol(string idol,string mode)
        {
            DefinitionsSekiro.Idol cIdol = defS.idolToEnum(idol);
            cIdol.Mode = mode;
            dataSekiro.idolsTosplit.Add(cIdol);
        }
            

        public void AddBoss(string boss,string mode)
        {
            DefinitionsSekiro.BossS cBoss = defS.BossToEnum(boss);
            cBoss.Mode = mode;
            dataSekiro.bossToSplit.Add(cBoss);
        }

        public void AddPosition(Vector3f vector, string mode) //Exception: is Repited yet controlled in AutoSplitter
        {
            var position = new DefinitionsSekiro.PositionS();
            position.setVector(vector);
            position.Mode = mode;
            dataSekiro.positionsToSplit.Add(position);
        }

        public void RemoveBoss(int position)
        {
            listPendingB.RemoveAll(iboss => iboss.Id == dataSekiro.bossToSplit[position].Id);
            dataSekiro.bossToSplit.RemoveAt(position);
            
        }


        public void RemovePosition(int position)
        {
            listPendingP.RemoveAll(iposition => iposition.vector == dataSekiro.positionsToSplit[position].vector);
            dataSekiro.positionsToSplit.RemoveAt(position);
        }
        
        public void RemoveIdol (string fidol)
        {
            DefinitionsSekiro.Idol cIdol = defS.idolToEnum(fidol);
            listPendingI.RemoveAll(idol => idol.Id == cIdol.Id);
            dataSekiro.idolsTosplit.RemoveAll(idol => idol.Id == cIdol.Id);
                      
        }

        public string FindIdol(string fidol)
        {
            DefinitionsSekiro.Idol cIdol = defS.idolToEnum(fidol);
            var idolReturn = dataSekiro.idolsTosplit.Find(idol => idol.Id == cIdol.Id);
            if (idolReturn == null)
            {
                return "None";
            }
            else { return idolReturn.Mode; }
            

        }
        public void setPositionMargin(int select)
        {
            dataSekiro.positionMargin = select;
        }

        public void setStatusSplitting(bool status)
        {
            dataSekiro.enableSplitting = status;
            if (status) { LoadAutoSplitterProcedure(); }
        }



        public void clearData()
        {
            dataSekiro.bossToSplit.Clear();
            dataSekiro.idolsTosplit.Clear();
            dataSekiro.positionMargin = 3;
            dataSekiro.positionsToSplit.Clear();
            
        }

        public Vector3f getCurrentPosition()
        {
            if (!_StatusSekiro)
            {
                _StatusSekiro = getSekiroStatusProcess(0);
            }
           
            return sekiro.GetPlayerPosition();
        }


        public void setProcedure(bool procedure)
        {
            this._StatusProcedure = procedure;
            if (procedure) { LoadAutoSplitterProcedure(); }
        }

        
        public bool getSekiroStatusProcess(int delay) //Use Delay 0 only for first Starts
        {
            Thread.Sleep(delay);
            return _StatusSekiro = sekiro.Refresh(out Exception exc);
        }

        public int getTimeInGame()
        {
            return sekiro.GetInGameTimeMilliseconds();
        }

        public void resetSplited()
        {
            if (dataSekiro.getBossToSplit().Count > 0)
            {
                foreach (var b in dataSekiro.getBossToSplit())
                {
                    b.IsSplited = false;
                }  
            }

            if (dataSekiro.getidolsTosplit().Count > 0)
            {
                foreach (var b in dataSekiro.getidolsTosplit())
                {
                    b.IsSplited = false;
                }
            }

            if (dataSekiro.getPositionsToSplit().Count > 0)
            {
                foreach (var p in dataSekiro.getPositionsToSplit())
                {
                    p.IsSplited = false;
                }
            }
        }


        public void LoadAutoSplitterProcedure()
        {
            var taskRefresh = new Task(() =>
            {
                RefreshSekiro();
            });
            var taskCheckload = new Task(() =>
            {
                checkLoad();
            });
            var task1 = new Task(() =>
            {
                BossSplit();
            });
            var task2 = new Task(() =>
            {
                IdolSplit();
            });
            var task3 = new Task(() =>
            {
                PositionSplit();
            });
            taskRefresh.Start();
            taskCheckload.Start();
            task1.Start();
            task2.Start();
            task3.Start();


        }

        #region init()    
        public void RefreshSekiro()
        {
            int delay = 2000;
            _StatusSekiro = getSekiroStatusProcess(delay);
            while (_StatusProcedure && dataSekiro.enableSplitting)
            {
                Thread.Sleep(10);
                _StatusSekiro = getSekiroStatusProcess(delay);
               if (!_StatusSekiro)
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
                    sekiro.WriteInGameTimeMilliseconds(0);
                    _writeMemory = true;
                }
            }           
        }

        List<DefinitionsSekiro.PositionS> listPendingP = new List<DefinitionsSekiro.PositionS>();
        List<DefinitionsSekiro.BossS> listPendingB = new List<DefinitionsSekiro.BossS>();
        List<DefinitionsSekiro.Idol> listPendingI = new List<DefinitionsSekiro.Idol>();

        private void checkLoad()
        {
            while (dataSekiro.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(200);
                if (listPendingI.Count > 0 || listPendingB.Count > 0 || listPendingP.Count > 0)
                {
                    if (!sekiro.IsPlayerLoaded())
                    {
                        foreach (var idol in listPendingI)
                        {
                            try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                            var i = dataSekiro.idolsTosplit.FindIndex(fidol => fidol.Id == idol.Id);
                            dataSekiro.idolsTosplit[i].IsSplited = true;
                            
                        }

                        foreach (var boss in listPendingB)
                        {
                            try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                            var b = dataSekiro.bossToSplit.FindIndex(fboss => fboss.Id == boss.Id);
                            dataSekiro.bossToSplit[b].IsSplited = true;

                        }

                        foreach (var position in listPendingP)
                        {
                            try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                            var p = dataSekiro.positionsToSplit.FindIndex(fposition => fposition.vector == position.vector);
                            dataSekiro.positionsToSplit[p].IsSplited = true;
                        }

                        listPendingB.Clear();
                        listPendingI.Clear();
                        listPendingP.Clear();


                    }
                }
            }
        }

        private void BossSplit()
        {
            while (dataSekiro.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(5000);
                foreach (var b in dataSekiro.getBossToSplit())
                {
                    if (!b.IsSplited && sekiro.ReadEventFlag(b.Id))
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
                            try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                        }
                    }
                       
                }
            }
        }

        private void IdolSplit()
        {
            while (dataSekiro.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(5000);
                foreach (var i in dataSekiro.getidolsTosplit())
                {

                    if (!i.IsSplited && sekiro.ReadEventFlag(i.Id))
                    {
                        if (i.Mode == "Loading game after")
                        {
                            if (!listPendingI.Contains(i))
                            {
                                listPendingI.Add(i);
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

        private void PositionSplit() {
            
            while (dataSekiro.enableSplitting && _StatusProcedure)
            {              
                Thread.Sleep(100);
                foreach (var p in dataSekiro.getPositionsToSplit())
                {
                    if (!p.IsSplited)
                    {
                        var currentlyPosition = sekiro.GetPlayerPosition();
                        var rangeX = ((currentlyPosition.X - p.vector.X) <= dataSekiro.positionMargin) && ((currentlyPosition.X - p.vector.X) >= -dataSekiro.positionMargin);
                        var rangeY = ((currentlyPosition.Y - p.vector.Y) <= dataSekiro.positionMargin) && ((currentlyPosition.Y - p.vector.Y) >= -dataSekiro.positionMargin);
                        var rangeZ = ((currentlyPosition.Z - p.vector.Z) <= dataSekiro.positionMargin) && ((currentlyPosition.Z - p.vector.Z) >= -dataSekiro.positionMargin);
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
                                try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                            }                                                       
                        }
                        
                    }
                }
            }
        }
        #endregion

    }        
}