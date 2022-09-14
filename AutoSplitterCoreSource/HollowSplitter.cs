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
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;
using LiveSplit.HollowKnight;
using HitCounterManager;

namespace AutoSplitterCore
{
    public class HollowSplitter
    {
        public static HollowKnightInfo hollow = new HollowKnightInfo();
        public DefinitionHollow defH = new DefinitionHollow();      
        public bool _StatusProcedure = true;
        public bool _StatusHollow = false;
        public bool _runStarted = false;
        public bool _SplitGo = false;
        public bool _PracticeMode = false;
        public DTHollow dataHollow;
        public ProfilesControl _profile;    
        public DefinitionHollow.Vector3F currentPosition = new DefinitionHollow.Vector3F();
        private static readonly object _object = new object();
        private System.Windows.Forms.Timer _update_timer = new System.Windows.Forms.Timer() { Interval = 1000 };
        public bool DebugMode = false;
        public DTHollow getDataHollow()
        {
            return this.dataHollow;
        }

        public void setDataHollow(DTHollow data, ProfilesControl profile)
        {
            this.dataHollow = data;
            this._profile = profile;
            _update_timer.Tick += (sender, args) => SplitGo();
        }

        public bool getHollowStatusProcess(int delay) //Use Delay 0 only for first Starts
        {
            Thread.Sleep(delay);
            return _StatusHollow = hollow.Memory.HookProcess();
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

        public PointF getCurrentPosition()
        {
            manualRefreshPosition();
            return this.currentPosition.position;
        }

        public void setProcedure(bool procedure)
        {
            this._StatusProcedure = procedure;
            if (procedure) { LoadAutoSplitterProcedure(); _update_timer.Enabled = true; } else { _update_timer.Enabled = false; }
        }

        public void setStatusSplitting(bool status)
        {
            dataHollow.enableSplitting = status;
            if (status) { LoadAutoSplitterProcedure(); _update_timer.Enabled = true; } else { _update_timer.Enabled = false; }
        }

        public void LoadAutoSplitterProcedure()
        {
            var taskRefresh = new Task(() =>
            {
                RefreshHollow();
            });

            var taskRefreshPosition = new Task(() =>
            {
                RefreshPosition();
            });

            var taskCheckStart = new Task(() =>
            {
                checkStart();
            });

            var task1 = new Task(() =>
            {
                bossToSplit();
            });

            var task2 = new Task(() =>
            {
                miniBossToSplit();
            });

            var task3 = new Task(() =>
            {
                pantheonToSplit();
            });

            var task4 = new Task(() =>
            {
                charmToSplit();
            });

            var task5 = new Task(() =>
            {
                skillsToSplit();
            });

            var task6 = new Task(() =>
            {
                positionToSplit();
            });
            taskRefresh.Start();
            taskRefreshPosition.Start();
            taskCheckStart.Start();
            task1.Start();
            task2.Start();
            task3.Start();
            task4.Start();
            task5.Start();
            task6.Start();
        }


        public void AddBoss(string boss)
        {
            DefinitionHollow.ElementToSplitH element = defH.stringToEnum(boss);
            dataHollow.bossToSplit.Add(element);

        }
        public void AddMiniBoss(string boss)
        {
            DefinitionHollow.ElementToSplitH element = defH.stringToEnum(boss);
            dataHollow.miniBossToSplit.Add(element);
        }


        public void AddPantheon(string Pantheon)
        {
            DefinitionHollow.Pantheon phan = new DefinitionHollow.Pantheon() { Title = Pantheon };
            dataHollow.phanteonToSplit.Add(phan);
        }

        public void AddCharm(string charm)
        {
            DefinitionHollow.ElementToSplitH element = defH.stringToEnum(charm);
            dataHollow.charmToSplit.Add(element);

        }

        public void AddSkill(string skill)
        {
            DefinitionHollow.ElementToSplitH element = defH.stringToEnum(skill);
            dataHollow.skillsToSplit.Add(element);

        }

        public void AddPosition(PointF position, string scene) 
        {
            DefinitionHollow.Vector3F vector = new DefinitionHollow.Vector3F() 
            {position = position, sceneName = scene,previousScene = null };
            dataHollow.positionToSplit.Add(vector);
        
        }

        public void RemoveBoss(string boss)
        {
            dataHollow.bossToSplit.RemoveAll(i=> i.Title == boss);
        }

        public void RemoveMiniBoss(string boss)
        {
            dataHollow.miniBossToSplit.RemoveAll(i => i.Title == boss);
            
        }

        public void RemovePantheon(string Pantheon)
        {
            dataHollow.phanteonToSplit.RemoveAll(i => i.Title == Pantheon);
        }
        

        public void RemoveCharm(string charm)
        {
            dataHollow.charmToSplit.RemoveAll(i => i.Title == charm);
        }

        public void RemoveSkill(string skill)
        {
            dataHollow.skillsToSplit.RemoveAll(i => i.Title == skill);
            
        }

        public void RemovePosition(int position)
        {
            dataHollow.positionToSplit.RemoveAt(position);
        }

        public void resetSplited()
        {
            if (dataHollow.getBosstoSplit().Count > 0)
            {
                foreach (var b in dataHollow.getBosstoSplit())
                {
                    b.IsSplited = false;
                }
            }

            if (dataHollow.getMiniBossToSplit().Count > 0)
            {
                foreach (var mb in dataHollow.getMiniBossToSplit())
                {
                    mb.IsSplited = false;
                }
            }

            if (dataHollow.getPhanteonToSplit().Count > 0)
            {
                foreach (var p in dataHollow.getPhanteonToSplit())
                {
                    p.IsSplited = false;
                }
            }

            if (dataHollow.getCharmToSplit().Count > 0)
            {
                foreach (var c in dataHollow.getCharmToSplit())
                {
                    c.IsSplited = false;
                }
            }

            if (dataHollow.getSkillsToSplit().Count > 0)
            {
                foreach (var s in dataHollow.getSkillsToSplit())
                {
                    s.IsSplited = false;
                }
            }

            if (dataHollow.getPositionToSplit().Count > 0)
            {
                foreach (var p in dataHollow.getPositionToSplit())
                {
                    p.IsSplited = false;
                }
            }
            _runStarted = false;
        }

        public void clearData()
        {
            dataHollow.bossToSplit.Clear();
            dataHollow.miniBossToSplit.Clear();
            dataHollow.phanteonToSplit.Clear();
            dataHollow.charmToSplit.Clear();
            dataHollow.skillsToSplit.Clear();
            dataHollow.positionToSplit.Clear();
            dataHollow.positionMargin = 3;
            _runStarted = false;
        }

        public bool getIsLoading()
        {
            return hollow.Memory.GameState() == GameState.LOADING ? true : false;
        }

        #region init()

        private void RefreshHollow()
        {
            int delay = 2000;
            getHollowStatusProcess(0);
            while (_StatusProcedure && dataHollow.enableSplitting)
            {
                Thread.Sleep(10);
                getHollowStatusProcess(delay);
                if (!_StatusHollow) { delay = 2000; } else { delay = 20000; }
            }
        }

        private void checkStart()
        {
            while (dataHollow.enableSplitting)
            {
                Thread.Sleep(500);
                if (!_PracticeMode)
                {
                    if (hollow.Memory.GameState() == GameState.PLAYING)
                    {
                        _runStarted = true;
                    }
                    if (dataHollow.gameTimer && (hollow.Memory.GameState() == GameState.LOADING || hollow.Memory.GameState() == GameState.CUTSCENE))
                    {
                        do
                        {
                            _runStarted = false;
                        } while (hollow.Memory.GameState() == GameState.LOADING || hollow.Memory.GameState() == GameState.CUTSCENE);
                        _runStarted = true;
                    }

                    if (currentPosition.sceneName.StartsWith("Quit_To_Menu") || currentPosition.sceneName.StartsWith("Menu_Title") || currentPosition.sceneName.StartsWith("PermaDeath") || hollow.Memory.GameState() == GameState.MAIN_MENU)
                    {
                        _runStarted = false;
                    }
                }
            }
        }

        private void RefreshPosition()
        {     
            while (dataHollow.enableSplitting)
            {
                Thread.Sleep(10);
                currentPosition.position = hollow.Memory.GetCameraTarget();
                if (hollow.Memory.SceneName() != currentPosition.sceneName)
                {
                    currentPosition.previousScene = currentPosition.sceneName;
                    currentPosition.sceneName = hollow.Memory.SceneName();
                }
            }
        }

        private void manualRefreshPosition()
        {
            getHollowStatusProcess(0);
            currentPosition.position = hollow.Memory.GetCameraTarget();
            currentPosition.sceneName = hollow.Memory.SceneName();
        }

       

        private void bossToSplit()
        {
            while (dataHollow.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(3000);
                if (!_PracticeMode)
                {
                    foreach (var element in dataHollow.getBosstoSplit())
                    {
                        if (!element.IsSplited && hollow.Memory.PlayerData<bool>(element.Offset))
                        {
                            element.IsSplited = true;
                            SplitCheck();
                        }
                    }
                }
            }
        }

        private void miniBossToSplit()
        {
            while (dataHollow.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(3000);
                foreach (var element in dataHollow.getMiniBossToSplit())
                {
                    if (!element.IsSplited)
                    {
                        if (element.intMethod)
                        {
                            if (_StatusHollow && hollow.Memory.PlayerData<int>(element.Offset) == element.intCompare)
                            {
                                element.IsSplited = true;
                                SplitCheck();
                            }
                        }
                        else
                        {
                            if (hollow.Memory.PlayerData<bool>(element.Offset))
                            {
                                element.IsSplited = true;
                                SplitCheck();
                            }
                        }
                    }
                }
            }
        }

        private void charmToSplit()
        {
            while (dataHollow.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(3000);
                if (!_PracticeMode)
                {
                    foreach (var element in dataHollow.getCharmToSplit())
                    {
                        if (!element.IsSplited)
                        {
                            if (element.intMethod)
                            {
                                if (_StatusHollow && hollow.Memory.PlayerData<int>(element.Offset) == element.intCompare)
                                {
                                    element.IsSplited = true;
                                    SplitCheck();
                                }
                            }
                            else
                            {
                                if (hollow.Memory.PlayerData<bool>(element.Offset) && !element.kingSoulsCase)
                                {
                                    element.IsSplited = true;
                                    SplitCheck();
                                }
                                else
                                {
                                    if (hollow.Memory.PlayerData<int>(Offset.charmCost_36) == 5 && hollow.Memory.PlayerData<int>(Offset.royalCharmState) == 3)
                                    {
                                        element.IsSplited = true;
                                        SplitCheck();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void skillsToSplit()
        {
            while (dataHollow.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(3000);
                if (!_PracticeMode)
                {
                    foreach (var element in dataHollow.getSkillsToSplit())
                    {
                        if (!element.IsSplited)
                        {
                            if (element.intMethod)
                            {
                                if (_StatusHollow && hollow.Memory.PlayerData<int>(element.Offset) == element.intCompare)
                                {
                                    element.IsSplited = true;
                                    SplitCheck();
                                }
                            }
                            else
                            {
                                if (hollow.Memory.PlayerData<bool>(element.Offset))
                                {
                                    element.IsSplited = true;
                                    SplitCheck();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void positionToSplit()
        {
            while (dataHollow.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(100);
                if (!_PracticeMode)
                {
                    foreach (var p in dataHollow.getPositionToSplit())
                    {
                        if (!p.IsSplited)
                        {
                            var currentlyPosition = this.currentPosition;
                            var rangeX = ((currentlyPosition.position.X - p.position.X) <= dataHollow.positionMargin) && ((currentlyPosition.position.X - p.position.X) >= -dataHollow.positionMargin);
                            var rangeY = ((currentlyPosition.position.Y - p.position.Y) <= dataHollow.positionMargin) && ((currentlyPosition.position.Y - p.position.Y) >= -dataHollow.positionMargin);
                            var rangeZ = currentPosition.sceneName == p.sceneName;
                            if (rangeX && rangeY && rangeZ)
                            {
                                p.IsSplited = true;
                                SplitCheck();
                            }
                        }
                    }
                }
            }
        }


        private bool PantheonCase(string title)
        {
            bool shouldSplit = false;
            switch (title)
            {
                case "Vengefly King":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Vengefly") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")) ; break;
                case "Gruz Mother":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Gruz_Mother") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "False Knight":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_False_Knight") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Massive Moss Charger":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Mega_Moss_Charger") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Hornet (Protector)":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Hornet_1") && !(!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Gorb":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Ghost_Gorb") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Dung Defender":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Dung_Defender") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Soul Warrior":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Mage_Knight") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Brooding Mawlek":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Brooding_Mawlek") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Oro & Mato Nail Bros":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Nailmasters") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Xero":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Ghost_Xero") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Crystal Guardian":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Crystal_Guardian") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Soul Master":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Soul_Master") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Oblobbles":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Oblobble") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Sisters of Battle":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Mantis_Lord") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Marmu":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Ghost_Marmu") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Flukemarm":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Flukemarm") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Broken Vessel":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Broken_Vessel") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Galien":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Ghost_Galien") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Paintmaster Sheo":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Painter") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Hive Knight":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Hive_Knight") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Elder Hu":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Ghost_Hu") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "The Collector":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Collector") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "God Tamer":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_God_Tamer") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Troupe Master Grim":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Grimm") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Watcher Knights":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Watcher_Knights") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Uumuu":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Uumuu") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Nosk":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Nosk") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Winged Nosk":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Nosk_Hornet") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Great Nailsage Slay":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Sly") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Hornet (Sentinel)":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Hornet_2") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Enraged Guardian":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Crystal_Guardian_2") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Lost Kin":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Lost_Kin") && !(!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "No Eyes":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Ghost_No_Eyes") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Traitor Lord":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Traitor_Lord") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "White Defender":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_White_Defender") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Soul Tyrant":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Soul_Tyrant") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Markoth":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Ghost_Markoth") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Grey Prince Zote":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Grey_Prince_Zote") && !(!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Failed Champion":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Failed_Champion") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Nightmare King Grimm":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Grimm_Nightmare") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Pure Vessel":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Hollow_Knight") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath")); break;
                case "Absolute Radiance":
                    shouldSplit = currentPosition.sceneName.StartsWith("Cinematic_Ending_E"); break;
                default: shouldSplit = false; break;
            }
            return shouldSplit;
        }

        private void notSplited(ref List<DefinitionHollow.Pantheon> p)
        {
            foreach (var i in p)
            {
                i.IsSplited = false;
            }
        }
        private void pantheonToSplit()
        {
            int p3s = 0, p5s = 0, nSplit = 0;

            foreach (var i in dataHollow.getPhanteonToSplit())
            {
                if (i.Title == "Grey Prince Zote")
                {
                    p3s = 10;
                    p5s = 42;
                    break;
                }
                p3s = 9;
                p5s = 41;
            }

            int tSplit = 30 + p3s;

            while (dataHollow.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(10);
                if (!_PracticeMode)
                {
                    if (dataHollow.PantheonMode == 0)
                    {

                        foreach (var element in dataHollow.getPhanteonToSplit())
                        {
                            if (!element.IsSplited && PantheonCase(element.Title))
                            {
                                element.IsSplited = true;
                                SplitCheck();
                                nSplit++;
                            }
                        }
                        if ((nSplit == tSplit && currentPosition.sceneName.StartsWith("GG_Atrium")) || (nSplit == p5s && currentPosition.sceneName.StartsWith("Cinematic_Ending_E")))
                        { //P1+2+3+4 o p5
                            notSplited(ref dataHollow.phanteonToSplit);
                            nSplit = 0;
                        }
                    }
                    else
                    {
                        foreach (var element in dataHollow.getPhanteonToSplit())
                        {
                            if (element.Title == "Pantheon of the Master" && !element.IsSplited)
                            {
                                if (currentPosition.previousScene.StartsWith("GG_Nailmasters") && !currentPosition.sceneName.StartsWith("GG_Atrium"))
                                {
                                    SplitCheck();
                                    element.IsSplited = true;
                                }
                            }


                            if (element.Title == "Pantheon of the Artist" && !element.IsSplited)
                            {
                                if (currentPosition.previousScene.StartsWith("GG_Painter") && !currentPosition.sceneName.StartsWith("GG_Atrium"))
                                {
                                    SplitCheck();
                                    element.IsSplited = true;
                                }
                            }

                            if (element.Title == "Pantheon of the Sage" && !element.IsSplited)
                            {
                                if (currentPosition.previousScene.StartsWith("GG_Sly") && !currentPosition.sceneName.StartsWith("GG_Atrium"))
                                {
                                    SplitCheck();
                                    element.IsSplited = true;
                                }

                            }

                            if (element.Title == "Pantheon of the Knight" && !element.IsSplited)
                            {
                                if (currentPosition.previousScene.StartsWith("GG_Hollow_Knight") && !currentPosition.sceneName.StartsWith("GG_Atrium"))
                                {
                                    SplitCheck();
                                    element.IsSplited = true;
                                }
                            }

                            if (element.Title == "Pantheon of Hallownest" && !element.IsSplited)
                            {
                                if (currentPosition.sceneName.StartsWith("Cinematic_Ending_E"))
                                {
                                    SplitCheck();
                                    element.IsSplited = true;
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
