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
using HitCounterManager;

namespace AutoSplitterCore
{
    public class SekiroSplitter
    {
        public static Sekiro sekiro = new Sekiro();
        public bool _StatusProcedure = true;
        public bool _StatusSekiro = false;
        public bool _runStarted = false;
        public bool _SplitGo = false;
        public bool _PracticeMode = false;
        public DTSekiro dataSekiro;
        public DefinitionsSekiro defS = new DefinitionsSekiro();
        public ProfilesControl _profile;
        private bool _writeMemory = false;
        private static readonly object _object = new object();
        private System.Windows.Forms.Timer _update_timer = new System.Windows.Forms.Timer() { Interval = 1000 };
        public bool DebugMode = false;


        public DTSekiro getDataSekiro()
        {
            return this.dataSekiro;
        }

        public void setDataSekiro(DTSekiro data, ProfilesControl profile)
        {
            this.dataSekiro = data;
            this._profile = profile;
            _update_timer.Tick += (sender, args) => SplitGo();
        }

        public void SplitGo()
        {
            if (_SplitGo)
            {
                if (!DebugMode) { try { _profile.ProfileSplitGo(+1); } catch (Exception) { } }else { Thread.Sleep(15000); }
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

        public void AddPosition(float X, float Y, float Z , string mode)
        {
            var position = new DefinitionsSekiro.PositionS();
            position.setVector(new Vector3f(X,Y,Z));
            position.Mode = mode;
            dataSekiro.positionsToSplit.Add(position);
        }

        public void AddCustomFlag(uint id,string mode)
        {
            DefinitionsSekiro.CfSk cFlag = new DefinitionsSekiro.CfSk()
            {
                Id = id,
                Mode = mode
            };
            dataSekiro.flagToSplit.Add(cFlag);
        }

        public void RemoveCustomFlag(int position)
        {
            listPendingCf.RemoveAll(iflag => iflag.Id == dataSekiro.flagToSplit[position].Id);
            dataSekiro.flagToSplit.RemoveAt(position);
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
            if (status) { LoadAutoSplitterProcedure(); _update_timer.Enabled = true; } else { _update_timer.Enabled = false; }
        }

        public void setProcedure(bool procedure)
        {
            this._StatusProcedure = procedure;
            if (procedure) { LoadAutoSplitterProcedure(); _update_timer.Enabled = true; } else { _update_timer.Enabled = false; }
        }


        public void clearData()
        { 
            listPendingB.Clear();
            listPendingI.Clear();
            listPendingP.Clear();
            listPendingCf.Clear();
            PendingMortal.Clear();
            dataSekiro.bossToSplit.Clear();
            dataSekiro.idolsTosplit.Clear();
            dataSekiro.positionsToSplit.Clear();
            dataSekiro.flagToSplit.Clear();
            notSplited(ref MortalJourneyData);
            dataSekiro.positionMargin = 3;           
            dataSekiro.mortalJourneyRun = false;
            _runStarted = false;
            index = 0;           
        }

        public Vector3f getCurrentPosition()
        {
            getSekiroStatusProcess(0);
            return sekiro.GetPlayerPosition();
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


        public bool CheckFlag(uint id)
        {
            return sekiro.ReadEventFlag(id);
        }
        public void resetSplited()
        {
            listPendingB.Clear();
            listPendingI.Clear();
            listPendingP.Clear();
            listPendingCf.Clear();

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

            if(dataSekiro.getFlagToSplit().Count > 0)
            {
                foreach (var cf in dataSekiro.getFlagToSplit())
                {
                    cf.IsSplited = false;
                }
            }
            _runStarted = false;
            index = 0;
            notSplited(ref MortalJourneyData);
            PendingMortal.Clear();
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
            var task4 = new Task(() =>
            {
                customFlagToSplit();
            });
            var task5 = new Task(() =>
            {
                MortalJourney();
            });
            taskRefresh.Start();
            taskCheckload.Start();
            task1.Start();
            task2.Start();
            task3.Start();
            task4.Start();
            task5.Start();
        }

        #region init()    
        private void RefreshSekiro()
        {
            int delay = 2000;
            getSekiroStatusProcess(delay);
            while (_StatusProcedure && dataSekiro.enableSplitting)
            {
                Thread.Sleep(10);
               getSekiroStatusProcess(delay);
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
                    if (sekiro.GetInGameTimeMilliseconds() < 1)
                    {
                        sekiro.WriteInGameTimeMilliseconds(0);
                    }                   
                    _writeMemory = true;
                }
            }           
        }

        List<DefinitionsSekiro.PositionS> listPendingP = new List<DefinitionsSekiro.PositionS>();
        List<DefinitionsSekiro.BossS> listPendingB = new List<DefinitionsSekiro.BossS>();
        List<DefinitionsSekiro.Idol> listPendingI = new List<DefinitionsSekiro.Idol>();
        List<DefinitionsSekiro.CfSk> listPendingCf = new List<DefinitionsSekiro.CfSk>();


        private void checkLoad()
        {
            while (dataSekiro.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(200);
                if (listPendingI.Count > 0 || listPendingB.Count > 0 || listPendingP.Count > 0 || listPendingCf.Count >0)
                {
                    if (!sekiro.IsPlayerLoaded())
                    {
                        foreach (var idol in listPendingI)
                        {
                            SplitCheck();
                            var i = dataSekiro.idolsTosplit.FindIndex(fidol => fidol.Id == idol.Id);
                            dataSekiro.idolsTosplit[i].IsSplited = true;
                            
                        }

                        foreach (var boss in listPendingB)
                        {
                            SplitCheck();
                            var b = dataSekiro.bossToSplit.FindIndex(fboss => fboss.Id == boss.Id);
                            dataSekiro.bossToSplit[b].IsSplited = true;

                        }

                        foreach (var position in listPendingP)
                        {
                            SplitCheck();
                            var p = dataSekiro.positionsToSplit.FindIndex(fposition => fposition.vector == position.vector);
                            dataSekiro.positionsToSplit[p].IsSplited = true;
                        }

                        foreach (var cf in listPendingCf)
                        {
                            SplitCheck();
                            var c = dataSekiro.flagToSplit.FindIndex(icf => icf.Id == cf.Id);
                            dataSekiro.flagToSplit[c].IsSplited = true;
                        }

                        listPendingB.Clear();
                        listPendingI.Clear();
                        listPendingP.Clear();
                        listPendingCf.Clear();
                    }
                }
            }
        }

        private void BossSplit()
        {
            while (dataSekiro.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(5000);
                if (!_PracticeMode)
                {
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
                                SplitCheck();
                            }
                        }
                    }
                }
            }
        }

        List<DefinitionsSekiro.PositionS> MortalJourneyData = new List<DefinitionsSekiro.PositionS>()
        {
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-100.3826, (float)-69.91195, (float)38.01242)}, //Gyobu
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-217, (float)-787.8057, (float)569.8)},//Lady Butterfly - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-102.9621, (float)53.90246, (float)243.125)}, //Genichiro - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-626.7878, (float)-296.0899, (float)757.6398)}, //Guardian Ape - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-180.1189, (float)-397.2622, (float)1406.365)}, //Corrupted Monk - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-117.93, (float)54.07684, (float)231.449)},  //Owl - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-197.3754,(float) -377.826, (float)632.1486)},//Double Apes - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-103.509,(float) 53.90246,(float) 243.125)}, //Emma - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-103.508,(float) 53.90246,(float) 243.125)}, //Isshin - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)91.34,(float) 154.8877,(float) 69.23)}, //True Monk - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-206.346, (float)-787.7436,(float) 565.117)}, //Owl Father - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-122.3998,(float) -71.81956, (float)-1.056416)}, //Demon of Hatred - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-372.1678,(float) -46.28377, (float)184.5305)}, //Genichiro Way of Tomoe - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-372.1679,(float) -46.28377,(float) 184.5305)}, //Sword Saint Isshin - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-102.9759,(float) 53.90246, (float)243.125)}, //Inner Genichiro
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-206.347, (float)-787.7436, (float)565.117)}, //Inner Father
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-372.1677, (float)-46.28377, (float)184.5305)}, //Inner Isshin
            
        };

        private void notSplited(ref List<DefinitionsSekiro.PositionS> p)
        {
            foreach (var i in p)
            {
                i.IsSplited = false;
            }
        }

        int index = 0;
        List<DefinitionsSekiro.PositionS> PendingMortal = new List<DefinitionsSekiro.PositionS>();
        private void MortalJourney()
        {
            DefinitionsSekiro.PositionS p;
            while (dataSekiro.enableSplitting)
            {
                Thread.Sleep(100);
                if (!_PracticeMode)
                {
                    if (dataSekiro.mortalJourneyRun && index < MortalJourneyData.Count)
                    {
                        p = MortalJourneyData[index];
                        if (PendingMortal.Count == 0)
                        {
                            if (!p.IsSplited)
                            {
                                var currentlyPosition = sekiro.GetPlayerPosition();
                                var rangeX = ((currentlyPosition.X - p.vector.X) <= 10) && ((currentlyPosition.X - p.vector.X) >= -10);
                                var rangeY = ((currentlyPosition.Y - p.vector.Y) <= 10) && ((currentlyPosition.Y - p.vector.Y) >= -10);
                                var rangeZ = ((currentlyPosition.Z - p.vector.Z) <= 10) && ((currentlyPosition.Z - p.vector.Z) >= -10);
                                if (rangeX && rangeY && rangeZ)
                                {
                                    if (!PendingMortal.Contains(p))
                                    {
                                        PendingMortal.Add(p);
                                    }
                                }
                            }
                            else
                            {
                                index++;
                            }
                        }
                        else
                        {
                            if (!sekiro.IsPlayerLoaded())
                            {
                                SplitCheck();
                                p.IsSplited = true;
                                PendingMortal.Clear();
                            }
                        }
                    }
                    else
                    {
                        index = 0;
                        notSplited(ref MortalJourneyData);
                    }
                }
            }
        }

        private void IdolSplit()
        {
            while (dataSekiro.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(3000);
                if (!_PracticeMode)
                {
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
                                SplitCheck();
                            }
                        }
                    }
                }
            }
        }

        private void PositionSplit() {
            
            while (dataSekiro.enableSplitting && _StatusProcedure)
            {              
                Thread.Sleep(100);
                if (!_PracticeMode)
                {
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
                                    SplitCheck();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void customFlagToSplit()
        {
            while (dataSekiro.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(3000);
                if (!_PracticeMode)
                {
                    foreach (var cf in dataSekiro.getFlagToSplit())
                    {
                        if (!cf.IsSplited && sekiro.ReadEventFlag(cf.Id))
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
        #endregion

    }        
}