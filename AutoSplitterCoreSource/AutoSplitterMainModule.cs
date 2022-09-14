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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HitCounterManager;
using System.Windows.Forms;

namespace AutoSplitterCore
{
    public class AutoSplitterMainModule
    {
        public SekiroSplitter sekiroSplitter = new SekiroSplitter();
        public Ds1Splitter ds1Splitter = new Ds1Splitter();
        public Ds2Splitter ds2Splitter = new Ds2Splitter();
        public Ds3Splitter ds3Splitter = new Ds3Splitter();
        public EldenSplitter eldenSplitter = new EldenSplitter();
        public HollowSplitter hollowSplitter = new HollowSplitter();
        public CelesteSplitter celesteSplitter = new CelesteSplitter();
        public CupheadSplitter cupSplitter = new CupheadSplitter();
        public AslSplitter aslSplitter = new AslSplitter();
        public IGTModule igtModule = new IGTModule();
        public SaveModule saveModule = new SaveModule();
        public bool DebugMode = false;
        public bool _PracticeMode = false;
        private ProfilesControl profCtrl;
        private Form1 main;
        private System.Windows.Forms.Timer _update_timer = new System.Windows.Forms.Timer() { Interval = 1000 };

        public void InitDebug()
        {
            sekiroSplitter.DebugMode = true;
            ds1Splitter.DebugMode = true;
            ds2Splitter.DebugMode = true;
            ds3Splitter.DebugMode = true;
            eldenSplitter.DebugMode = true;
            hollowSplitter.DebugMode = true;
            celesteSplitter.DebugMode = true;
            cupSplitter.DebugMode = true;
            aslSplitter.DebugMode = true;
        }

        public void AutoSplitterForm(bool darkMode)
        {
            Form form = new AutoSplitter(sekiroSplitter, hollowSplitter, eldenSplitter, ds3Splitter, celesteSplitter, ds2Splitter, aslSplitter, cupSplitter, ds1Splitter, darkMode);
            form.ShowDialog();
        }

        public void SetPointers()
        {
            igtModule.setSplitterPointers(sekiroSplitter, eldenSplitter, ds3Splitter, celesteSplitter, cupSplitter, ds1Splitter);
            saveModule.SetPointers(sekiroSplitter, ds1Splitter, ds2Splitter, ds3Splitter, eldenSplitter, hollowSplitter, celesteSplitter, cupSplitter, aslSplitter);
        }
        public void LoadAutoSplitterSettings(ProfilesControl profiles, Form1 main)
        {
            saveModule.LoadAutoSplitterSettings(profiles);
            this.profCtrl = profiles;
            this.main = main;         
            if (main != null)
            {
                _update_timer.Tick += (senderT, args) => CheckAutoTimers();
                _update_timer.Enabled = true;
            }         
        }

        public void SaveAutoSplitterSettings()
        {
            saveModule.SaveAutoSplitterSettings();
        }

        public int GetSplitterEnable()
        {
            if (sekiroSplitter.dataSekiro.enableSplitting) { return 1; }
            if (ds1Splitter.dataDs1.enableSplitting) { return 2; }
            if (ds2Splitter.dataDs2.enableSplitting) { return 3; }
            if (ds3Splitter.dataDs3.enableSplitting) { return 4; }
            if (eldenSplitter.dataElden.enableSplitting) { return 5; }
            if (hollowSplitter.dataHollow.enableSplitting) { return 6; }
            if (celesteSplitter.dataCeleste.enableSplitting) { return 7; }
            if (cupSplitter.dataCuphead.enableSplitting) { return 8; }
            if (aslSplitter.enableSplitting) { return 9; }
            return 0;
        }

        public int ReturnCurrentIGTM()
        {
            return igtModule.ReturnCurrentIGT();
        }

        public void SetPracticeMode(bool status)
        {
            _PracticeMode = status;
            sekiroSplitter._PracticeMode = status;
            ds1Splitter._PracticeMode = status;
            ds2Splitter._PracticeMode = status;
            ds3Splitter._PracticeMode = status;
            eldenSplitter._PracticeMode = status;
            hollowSplitter._PracticeMode = status;
            celesteSplitter._PracticeMode = status;
            cupSplitter._PracticeMode = status;
            aslSplitter._PracticeMode = status;
        }

        private int gameActive = 0;
        private long? _lastInGameTime;
        private bool anyGameTime = false;

        public bool GetIsIGTActive()
        {
            return this.anyGameTime;
        }

        public void CheckAutoTimers()
        {
            if (_PracticeMode) { anyGameTime = false; }
            switch (gameActive)
            {
                case 1: //Sekiro
                    if (sekiroSplitter.dataSekiro.autoTimer && !_PracticeMode)
                    {
                        anyGameTime = false;
                        if (!sekiroSplitter.dataSekiro.gameTimer)
                        {
                            if (!sekiroSplitter._runStarted && sekiroSplitter.getTimeInGame() > 0)
                            {
                                main.StartStopTimer(true);
                                sekiroSplitter._runStarted = true;
                            }
                            else
                            if (sekiroSplitter._runStarted && sekiroSplitter.getTimeInGame() == 0)
                            {
                                main.StartStopTimer(false);
                                sekiroSplitter._runStarted = false;
                            }
                        }
                        else
                        {
                            anyGameTime = true;
                        }
                    }
                    break;
                case 2: //DS1
                    if (ds1Splitter.dataDs1.autoTimer && !_PracticeMode)
                    {
                        if (!ds1Splitter.dataDs1.gameTimer)
                        {
                            anyGameTime = false;
                            if (!ds1Splitter._runStarted && ds1Splitter.getTimeInGame() > 0)
                            {
                                main.StartStopTimer(true);
                                ds1Splitter._runStarted = true;
                            }
                            else if (ds1Splitter._runStarted && ds1Splitter.getTimeInGame() == 0)
                            {
                                main.StartStopTimer(false);
                                ds1Splitter._runStarted = false;
                            }
                        }
                        else
                        {
                            anyGameTime = true;
                        }
                    }
                    break;
                case 4: //Ds3
                    if (ds3Splitter.dataDs3.autoTimer && !_PracticeMode)
                    {
                        if (!ds3Splitter.dataDs3.gameTimer)
                        {
                            anyGameTime = false;
                            if (!ds3Splitter._runStarted && ds3Splitter.getTimeInGame() > 0)
                            {
                                main.StartStopTimer(true);
                                ds3Splitter._runStarted = true;
                            }
                            else if (ds3Splitter.dataDs3.autoTimer && ds3Splitter._runStarted && ds3Splitter.getTimeInGame() == 0)
                            {
                                main.StartStopTimer(false);
                                ds3Splitter._runStarted = false;
                            }
                        }
                        else
                        {
                            anyGameTime = true;
                        }
                    }
                    break;
                case 5: //Elden
                    if (eldenSplitter.dataElden.autoTimer && !_PracticeMode)
                    {
                        if (!eldenSplitter.dataElden.gameTimer)
                        {
                            anyGameTime = false;
                            if (!eldenSplitter._runStarted && eldenSplitter.getTimeInGame() > 0)
                            {
                                main.StartStopTimer(true);
                                eldenSplitter._runStarted = true;
                            }
                            else if (eldenSplitter._runStarted && eldenSplitter.getTimeInGame() == 0)
                            {
                                main.StartStopTimer(false);
                                eldenSplitter._runStarted = false;
                            }
                        }
                        else
                        {
                            anyGameTime = true;
                        }
                    }
                    break;
                case 7: //Celeste
                    if (celesteSplitter.dataCeleste.autoTimer && !_PracticeMode)
                    {
                        if (!celesteSplitter.dataCeleste.gameTimer)
                        {
                            anyGameTime = false;
                            if (celesteSplitter.dataCeleste.autoTimer && !celesteSplitter._runStarted && celesteSplitter.getTimeInGame() > 0)
                            {
                                main.StartStopTimer(true);
                                celesteSplitter._runStarted = true;
                            }
                            else if (celesteSplitter._runStarted && celesteSplitter.getTimeInGame() == 0)
                            {
                                main.StartStopTimer(false);
                                celesteSplitter._runStarted = false;
                            }
                        }
                        else
                        {
                            anyGameTime = true;
                        }
                    }
                    break;
                case 8: //Cuphead
                    if (cupSplitter.dataCuphead.autoTimer && !_PracticeMode)
                    {
                        if (!cupSplitter.dataCuphead.gameTimer)
                        {
                            anyGameTime = false;
                            if (!cupSplitter._runStarted && cupSplitter.getTimeInGame() > 0)
                            {
                                main.StartStopTimer(true);
                                cupSplitter._runStarted = true;
                            }
                            else if (cupSplitter._runStarted && cupSplitter.getTimeInGame() == 0)
                            {
                                main.StartStopTimer(false);
                                cupSplitter._runStarted = false;
                            }
                        }
                        else
                        {
                            anyGameTime = true;
                        }
                    }
                    break;

                case 3: //DS2
                    if (ds2Splitter.dataDs2.autoTimer && !_PracticeMode)
                    {
                        anyGameTime = false;
                        if (ds2Splitter._runStarted)
                        {
                            main.StartStopTimer(true);
                        }
                        else
                        {
                            if (!ds2Splitter._runStarted)
                            {
                                main.StartStopTimer(false);
                            }
                        }
                    }
                    break;
                case 6: //Hollow
                    if (hollowSplitter.dataHollow.autoTimer && !_PracticeMode)
                    {
                        anyGameTime = false;
                        if (hollowSplitter._runStarted)
                        {
                            main.StartStopTimer(true);
                        }
                        else
                        {
                            if (!hollowSplitter._runStarted)
                            {
                                main.StartStopTimer(false);
                            }
                        }
                    }
                    break;


                case 0:
                case 9:
                default: anyGameTime = false; break;
            }

            if (anyGameTime)
            {
                var inGameTime = igtModule.ReturnCurrentIGT();
                if (inGameTime > 0 && !profCtrl.TimerRunning)
                    main.StartStopTimer(true);
                else if (inGameTime == 0 && profCtrl.TimerRunning)
                    main.StartStopTimer(false);
                else if (inGameTime > 0 && _lastInGameTime == inGameTime && profCtrl.TimerRunning)
                    main.StartStopTimer(false);
                else if (inGameTime > 0 && _lastInGameTime != inGameTime && !profCtrl.TimerRunning)
                    main.StartStopTimer(true);
                if (inGameTime > 0)
                    _lastInGameTime = inGameTime;
            }
        }

        public void EnableSplitting(int splitter)
        {
            gameActive = splitter;
            igtModule.gameSelect = splitter;
            anyGameTime = false;
            if (splitter == 0)
            {
                sekiroSplitter.setStatusSplitting(false);
                ds1Splitter.setStatusSplitting(false);
                ds2Splitter.setStatusSplitting(false);
                ds3Splitter.setStatusSplitting(false);
                eldenSplitter.setStatusSplitting(false);
                hollowSplitter.setStatusSplitting(false);
                celesteSplitter.setStatusSplitting(false);
                cupSplitter.setStatusSplitting(false);
                aslSplitter.setStatusSplitting(false);
            }
            else
            {
                switch (splitter)
                {
                    case 1: sekiroSplitter.setStatusSplitting(true); break;
                    case 2: ds1Splitter.setStatusSplitting(true); break;
                    case 3: ds2Splitter.setStatusSplitting(true); break;
                    case 4: ds3Splitter.setStatusSplitting(true); break;
                    case 5: eldenSplitter.setStatusSplitting(true); break;
                    case 6: hollowSplitter.setStatusSplitting(true); break;
                    case 7: celesteSplitter.setStatusSplitting(true); break;
                    case 8: cupSplitter.setStatusSplitting(true); break;
                    case 9: aslSplitter.setStatusSplitting(true); break;
                }
            }
        }

        public void ResetSplitterFlags()
        {
            sekiroSplitter.resetSplited();
            ds1Splitter.resetSplited();
            ds2Splitter.resetSplited();
            ds3Splitter.resetSplited();
            eldenSplitter.resetSplited();
            hollowSplitter.resetSplited();
            celesteSplitter.resetSplited();
            cupSplitter.resetSplited();
        }

        public bool CheckAutoTimerFlag(int game)
        {
            switch (game)
            {
                case 1: return sekiroSplitter.dataSekiro.autoTimer;
                case 2: return ds1Splitter.dataDs1.autoTimer;
                case 3: return ds2Splitter.dataDs2.autoTimer;
                case 4: return ds3Splitter.dataDs3.autoTimer;
                case 5: return eldenSplitter.dataElden.autoTimer;
                case 6: return hollowSplitter.dataHollow.autoTimer;
                case 7: return celesteSplitter.dataCeleste.autoTimer;
                case 8: return cupSplitter.dataCuphead.autoTimer;
                case 9:
                case 0:
                default: return false;
            }
        }

        public bool CheckGameTimerFlag(int game)
        {
            switch (game)
            {
                case 1: return sekiroSplitter.dataSekiro.gameTimer;
                case 2: return ds1Splitter.dataDs1.gameTimer;
                case 3: return ds2Splitter.dataDs2.gameTimer;
                case 4: return ds3Splitter.dataDs3.gameTimer;
                case 5: return eldenSplitter.dataElden.gameTimer;
                case 6: return hollowSplitter.dataHollow.gameTimer;
                case 7: return celesteSplitter.dataCeleste.gameTimer;
                case 8: return cupSplitter.dataCuphead.gameTimer;
                case 9:
                case 0:
                default: return false;
            }
        }

        public int GetIgtSplitterTimer(int game)
        {
            switch (game)
            {
                case 1: return sekiroSplitter.getTimeInGame();
                case 2: return ds1Splitter.getTimeInGame();
                case 4: return ds3Splitter.getTimeInGame();
                case 5: return eldenSplitter.getTimeInGame();
                case 7: return celesteSplitter.getTimeInGame();
                case 8: return cupSplitter.getTimeInGame();

                case 3:
                case 6:
                case 9:
                case 0:
                default: return -1;
            }
        }

        public bool CheckSplitterRunStarted(int game)
        {
            switch (game)
            {
                case 1: return sekiroSplitter._runStarted;
                case 2: return ds1Splitter._runStarted;
                case 3: return ds2Splitter._runStarted;
                case 4: return ds3Splitter._runStarted;
                case 5: return eldenSplitter._runStarted;
                case 6: return hollowSplitter._runStarted;
                case 7: return celesteSplitter._runStarted;
                case 8: return cupSplitter._runStarted;
                case 9:
                case 0:
                default: return false;
            }
        }

        public void SetSplitterRunStarted(int game, bool status)
        {
            switch (game)
            {
                case 1: sekiroSplitter._runStarted = status; break;
                case 2: ds1Splitter._runStarted = status; break;
                case 3: ds2Splitter._runStarted = status; break;
                case 4: ds3Splitter._runStarted = status; break;
                case 5: eldenSplitter._runStarted = status; break;
                case 6: hollowSplitter._runStarted = status; break;
                case 7: celesteSplitter._runStarted = status; break;
                case 8: cupSplitter._runStarted = status; break;
                case 9:
                case 0:
                default: break;
            }
        }

    }
}
