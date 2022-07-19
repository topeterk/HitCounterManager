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
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SoulMemory.Sekiro;
using SoulMemory;

namespace HitCounterManager
{
    
    public class SekiroSplitter
    {
        
        public Sekiro sekiro = new Sekiro();
        public bool _StatusProcedure = true;
        public bool _StatusSekiro = false;
        public DTSekiro dataSekiro;
        public DefinitionsSekiro defS = new DefinitionsSekiro();
        public ProfilesControl _profile;
        public bool _runStarted = false;



        public DTSekiro getDataSekiro()
        {
            return this.dataSekiro;
        }

        public void setDataSekiro(DTSekiro data,ProfilesControl profile)
        {
            this.dataSekiro = data;
            this._profile = profile;
        }

        public void AddIdol(string idol)
        {
            DefinitionsSekiro.Idol cIdol = defS.idolToEnum(idol);
            bool exist = false;
            foreach (DefinitionsSekiro.Idol b in dataSekiro.getidolsTosplit())
            {
                if (b.Id == cIdol.Id)
                {
                    exist = true;
                    break;
                }
            }

            if (!exist)
            {
                dataSekiro.DTAddIdol(cIdol);
            }
        }
            
        public void RemoveIdol(string idol)
        {
            DefinitionsSekiro.Idol cIdol = defS.idolToEnum(idol);
            foreach (DefinitionsSekiro.Idol b in dataSekiro.getidolsTosplit())
            {
                if (b.Id == cIdol.Id)
                {
                    dataSekiro.DTRemoveIdol(b);
                    break;
                }
            }
        }

        public void AddBoss(string boss)
        {
            DefinitionsSekiro.Boss cBoss = defS.BossToEnum(boss);
            bool exist = false;


            foreach (DefinitionsSekiro.Boss b in dataSekiro.getBossToSplit())
            {
                if (b.Id== cBoss.Id)
                {
                    exist = true;
                    break;
                }
            }

            if (!exist)
            {
                dataSekiro.DTAddBoss(cBoss);
            }
        }

        public void RemoveBoss(string boss)
        {
            DefinitionsSekiro.Boss cBoss = defS.BossToEnum(boss);
            foreach (DefinitionsSekiro.Boss b in dataSekiro.getBossToSplit())
            {
                if (b.Id == cBoss.Id)
                {
                    dataSekiro.DTRemoveBoss(b);
                    break;
                }
            }  
        }

        public void AddPosition(Vector3f vector) //Exception: is Repited yet controlled in AutoSplitter
        {
            var position = new DefinitionsSekiro.Position();
            position.setVector(vector);
            dataSekiro.DTAddPosition(position);
        }

        public void RemovePosition(int position)
        {
            dataSekiro.DTRemovePosition(position);
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

        public void setStatusTimer(bool status)
        {
            dataSekiro.enableTimer = status;
        }


        public void clearData()
        {
            dataSekiro.bossToSplit.Clear();
            dataSekiro.idolsTosplit.Clear();
            dataSekiro.positionMargin = 3;
            dataSekiro.positionsToSplit.Clear();
            dataSekiro.enableSplitting = false;
            dataSekiro.enableTimer = false;
        }

        public Vector3f getCurrentPosition()
        {
            return sekiro.GetPlayerPosition();
        }

        public int getGameTime()
        {
            return sekiro.GetInGameTimeMilliseconds();
        }

        public void setProcedure(bool procedure)
        {
            this._StatusProcedure = procedure;
        }

        
        public bool getSekiroStatusProcess(out Exception exception, int delay) //Use Delay 0 only for first Starts
        {
            Thread.Sleep(delay);
            return _StatusSekiro = sekiro.Refresh(out exception) == true;
        }

        public void resetSplited()
        {
            if (dataSekiro.getBossToSplit().Count != 0)
            {
                foreach (var b in dataSekiro.getBossToSplit())
                {
                    b.IsSplited = false;
                }  
            }

            if (dataSekiro.getidolsTosplit().Count != 0)
            {
                foreach (var b in dataSekiro.getidolsTosplit())
                {
                    b.IsSplited = false;
                }
            }

            if (dataSekiro.getPositionsToSplit().Count != 0)
            {
                foreach (var p in dataSekiro.getPositionsToSplit())
                {
                    p.IsSplited = false;
                }
            }
            _runStarted = false;
        }

        public bool initTimer()
        {
            _StatusSekiro = getSekiroStatusProcess(out Exception exception,0);
            if (dataSekiro.enableTimer && !_runStarted)
            {
                if (_StatusSekiro)
                {
                    sekiro.WriteInGameTimeMilliseconds(0);
                }
                if (sekiro.GetInGameTimeMilliseconds() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void LoadAutoSplitterProcedure()
        {
            var taskRefresh = new Task(() =>
            {
                RefreshSekiro();
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
            task1.Start();
            task2.Start();
            task3.Start();


        }

        #region init()    
        public void RefreshSekiro()
        {
            _StatusSekiro = getSekiroStatusProcess(out Exception exception,0);
            while (_StatusProcedure)
                if (_StatusSekiro)
                {
                    _StatusSekiro = getSekiroStatusProcess(out exception, 120000);//Wait 120s to optimize cpu usage
                }
                else
                {
                    _StatusSekiro = getSekiroStatusProcess(out exception, 10000);//Wait 10s
                }
            
        }

        private void BossSplit()
        {
            while (dataSekiro.enableSplitting && _StatusProcedure)
            {
                foreach (var b in dataSekiro.getBossToSplit())
                {
                    if (sekiro.ReadEventFlag(b.Id) == true && b.IsSplited != true)
                    {
                        b.IsSplited = true;
                        _profile.ProfileSplitGo(+1);
                    }
                }
            }
        }

        private void IdolSplit()
        {
            while (dataSekiro.enableSplitting && _StatusProcedure)
            {
                foreach (var i in dataSekiro.getidolsTosplit())
                {
                    if (sekiro.ReadEventFlag(i.Id) == true && i.IsSplited != true)
                    {
                        i.IsSplited = true;
                        _profile.ProfileSplitGo(+1);
                    }
                }
            }
        }

        private void PositionSplit() {
            while (dataSekiro.enableSplitting && _StatusProcedure)
            {
                foreach (var p in dataSekiro.getPositionsToSplit())
                {
                    var currentlyPosition = sekiro.GetPlayerPosition();
                    var rangeX = ((currentlyPosition.X - p.vector.X) <= dataSekiro.positionMargin) && ((currentlyPosition.X - p.vector.X) >= -dataSekiro.positionMargin);
                    var rangeY = ((currentlyPosition.Y - p.vector.Y) <= dataSekiro.positionMargin) && ((currentlyPosition.Y - p.vector.Y) >= -dataSekiro.positionMargin);
                    var rangeZ = ((currentlyPosition.Z - p.vector.Z) <= dataSekiro.positionMargin) && ((currentlyPosition.Z - p.vector.Z) >= -dataSekiro.positionMargin);
                    if (rangeX && rangeY & rangeZ && p.IsSplited != true)
                    {
                        p.IsSplited = true;
                        _profile.ProfileSplitGo(+1);
                    }
                }
            }
        }
        #endregion

    }        
}