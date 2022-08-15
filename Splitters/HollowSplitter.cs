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

namespace HitCounterManager
{
    public class HollowSplitter
    {
        public static HollowKnightInfo hollow = new HollowKnightInfo();
        public DefinitionHollow defH = new DefinitionHollow();
        public DTHollow dataHollow;
        public bool _StatusProcedure = true;
        public bool _StatusHollow = false;
        public ProfilesControl _profile;
        public bool _runStarted = false;
        public DefinitionHollow.Vector3F currentPosition = new DefinitionHollow.Vector3F();
        public DTHollow getDataHollow()
        {
            return this.dataHollow;
        }

        public void setDataHollow(DTHollow data, ProfilesControl profile)
        {
            this.dataHollow = data;
            this._profile = profile;
        }

        public bool getHollowStatusProcess(int delay) //Use Delay 0 only for first Starts
        {
            Thread.Sleep(delay);
            return _StatusHollow = hollow.Memory.HookProcess();
        }


        public PointF getCurrentPosition()
        {
            if (!dataHollow.enableSplitting)
            {
                manualRefreshPosition();
            }
            return this.currentPosition.position;
        }

        public void setProcedure(bool procedure)
        {
            this._StatusProcedure = procedure;
            if (procedure) { LoadAutoSplitterProcedure(); }
        }

        public void setStatusSplitting(bool status)
        {
            dataHollow.enableSplitting = status;
            if (status) { LoadAutoSplitterProcedure(); }
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


        #region init()

        public void RefreshHollow()
        {
            int delay = 2000;
            _StatusHollow = getHollowStatusProcess(0);
            while (_StatusProcedure && dataHollow.enableSplitting)
            {
                Thread.Sleep(10);
                _StatusHollow = getHollowStatusProcess(delay);
                if (!_StatusHollow) { delay = 2000; } else { delay = 20000; }
            }
        }

        private void checkStart()
        {
            while (dataHollow.enableSplitting && dataHollow.autoTimer)
            {
                Thread.Sleep(2000);
                PointF p = new PointF(35, 14);
                var rangeX = ((currentPosition.position.X - p.X) <= dataHollow.positionMargin) && ((currentPosition.position.X - p.X) >= -dataHollow.positionMargin);
                var rangeY = ((currentPosition.position.Y - p.Y) <= dataHollow.positionMargin) && ((currentPosition.position.Y - p.Y) >= -dataHollow.positionMargin);
                if (rangeX && rangeY && currentPosition.sceneName.StartsWith("Tutorial"))
                {
                    _runStarted = true;
                }else if (currentPosition.sceneName.StartsWith("Quit_To_Menu") || currentPosition.sceneName.StartsWith("Menu_Title"))
                {
                    _runStarted=false;
                }
            }
        }

        public void RefreshPosition()
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
            currentPosition.position = hollow.Memory.GetCameraTarget();
            currentPosition.sceneName = hollow.Memory.SceneName();
        }

        private void bossToSplit()
        {
            while (dataHollow.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(5000);
                foreach (var element in dataHollow.getBosstoSplit())
                {
                    if (!element.IsSplited && hollow.Memory.PlayerData<bool>(element.Offset))
                    {
                        element.IsSplited = true;
                        try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                    }
                }
            }
        }

        private void miniBossToSplit()
        {
            while (dataHollow.enableSplitting && _StatusProcedure)
            {
                Thread.Sleep(5000);
                foreach (var element in dataHollow.getMiniBossToSplit())
                {
                    if (!element.IsSplited)
                    {
                        if (element.intMethod)
                        {
                            if (_StatusHollow && hollow.Memory.PlayerData<int>(element.Offset) == element.intCompare)
                            {
                                element.IsSplited = true;
                                try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                            }
                        }
                        else
                        {
                            if (hollow.Memory.PlayerData<bool>(element.Offset))
                            {
                                element.IsSplited = true;
                                try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
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
                Thread.Sleep(5000);
                foreach (var element in dataHollow.getCharmToSplit())
                {
                    if (!element.IsSplited)
                    {
                        if (element.intMethod)
                        {
                            if (_StatusHollow && hollow.Memory.PlayerData<int>(element.Offset) == element.intCompare)
                            {
                                element.IsSplited = true;
                                try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                            }
                        }
                        else
                        {
                            if (hollow.Memory.PlayerData<bool>(element.Offset) && !element.kingSoulsCase)
                            {
                                element.IsSplited = true;
                                try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                            }
                            else
                            {
                                if(hollow.Memory.PlayerData<int>(Offset.charmCost_36) == 5 && hollow.Memory.PlayerData<int>(Offset.royalCharmState) == 3)
                                {
                                    element.IsSplited = true;
                                    try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
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
                Thread.Sleep(5000);
                foreach (var element in dataHollow.getSkillsToSplit())
                {
                    if (!element.IsSplited)
                    {
                        if (element.intMethod)
                        {
                            if (_StatusHollow && hollow.Memory.PlayerData<int>(element.Offset) == element.intCompare)
                            {
                                element.IsSplited = true;
                                try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                            }
                        }
                        else
                        {
                            if (hollow.Memory.PlayerData<bool>(element.Offset))
                            {
                                element.IsSplited = true;
                                try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
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
                            try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                        }
                    }
                }
            }
        }



        /* Internal Data Pantheon
        private List<DefinitionHollow.Pantheon> p1 = new List<DefinitionHollow.Pantheon>
        {
            new DefinitionHollow.Pantheon() { Title = "Vengefly King"},
            new DefinitionHollow.Pantheon() { Title = "Gruz Mother"},
            new DefinitionHollow.Pantheon() { Title = "False Knight"},
            new DefinitionHollow.Pantheon() { Title = "Massive Moss Charger"},
            new DefinitionHollow.Pantheon() { Title = "Hornet (Protector)"},
            new DefinitionHollow.Pantheon() { Title = "Gorb"},
            new DefinitionHollow.Pantheon() { Title = "Dung Defender"},
            new DefinitionHollow.Pantheon() { Title = "Soul Warrior"},
            new DefinitionHollow.Pantheon() { Title = "Brooding Mawlek"},
            new DefinitionHollow.Pantheon() { Title = "Oro & Mato Nail Bros"}

        };
        private List<DefinitionHollow.Pantheon> p2 = new List<DefinitionHollow.Pantheon>
        {
             new DefinitionHollow.Pantheon() { Title = "Xero"},
             new DefinitionHollow.Pantheon() { Title = "Crystal Guardian"},
             new DefinitionHollow.Pantheon() { Title = "Soul Master"},
             new DefinitionHollow.Pantheon() { Title = "Oblobbles"},
             new DefinitionHollow.Pantheon() { Title = "Sisters of Battle"},
             new DefinitionHollow.Pantheon() { Title = "Marmu"},
             new DefinitionHollow.Pantheon() { Title = "Nosk"},
             new DefinitionHollow.Pantheon() { Title = "Flukemarm"},
             new DefinitionHollow.Pantheon() { Title = "Broken Vessel"},
             new DefinitionHollow.Pantheon() { Title = "Paintmaster Sheo"}
        };
        private List<DefinitionHollow.Pantheon> p3 = new List<DefinitionHollow.Pantheon>
        {
            new DefinitionHollow.Pantheon(){Title = "Hive Knight"},
            new DefinitionHollow.Pantheon(){Title = "Elder Hu"},
            new DefinitionHollow.Pantheon(){Title = "The Collector"},
            new DefinitionHollow.Pantheon(){Title = "God Tamer"},
            new DefinitionHollow.Pantheon(){Title = "Troupe Master Grimm"},
            new DefinitionHollow.Pantheon(){Title = "Galien"},
            new DefinitionHollow.Pantheon(){Title = "Grey Prince Zote"},
            new DefinitionHollow.Pantheon(){Title = "Uumuu"},
            new DefinitionHollow.Pantheon(){Title = "Hornet (Sentinel)"},
            new DefinitionHollow.Pantheon(){Title = "Great Nailsage Sly"}
        };
        private List<DefinitionHollow.Pantheon> p4 = new List<DefinitionHollow.Pantheon>()
        {
            new DefinitionHollow.Pantheon(){Title = "Enraged Guardian" },
            new DefinitionHollow.Pantheon(){Title = "Lost Kin" },
            new DefinitionHollow.Pantheon(){Title = "No Eyes" },
            new DefinitionHollow.Pantheon(){Title = "Traitor Lord" },
            new DefinitionHollow.Pantheon(){Title = "White Defender" },
            new DefinitionHollow.Pantheon(){Title = "Failed Champion" },
            new DefinitionHollow.Pantheon(){Title = "Markoth" },
            new DefinitionHollow.Pantheon(){Title = "Watcher Knight" },
            new DefinitionHollow.Pantheon(){Title = "Soul Tyrant" },
            new DefinitionHollow.Pantheon(){Title = "Pure Vessel" }
        };
        private List<DefinitionHollow.Pantheon> p5 = new List<DefinitionHollow.Pantheon>()
        {
            new DefinitionHollow.Pantheon(){Title = "Grey Prince Zote"},
            new DefinitionHollow.Pantheon(){Title = "Vengefly King"},
            new DefinitionHollow.Pantheon(){Title = "Gruz Mother"},
            new DefinitionHollow.Pantheon(){Title = "False Knight"},
            new DefinitionHollow.Pantheon(){Title = "Massive Moss Charger"},
            new DefinitionHollow.Pantheon(){Title = "Hornet (Protector)"},
            new DefinitionHollow.Pantheon(){Title = "Gorb"},
            new DefinitionHollow.Pantheon(){Title = "Dung Defender"},
            new DefinitionHollow.Pantheon(){Title = "Soul Warrior"},
            new DefinitionHollow.Pantheon(){Title = "Brooding Mawlek"},
            new DefinitionHollow.Pantheon(){Title = "Oro & Mato Nail Bros"},
            new DefinitionHollow.Pantheon(){Title = "Xero"},
            new DefinitionHollow.Pantheon(){Title = "Crystal Guardian"},
            new DefinitionHollow.Pantheon(){Title = "Soul Master"},
            new DefinitionHollow.Pantheon(){Title = "Oblobbles"},
            new DefinitionHollow.Pantheon(){Title = "Sisters of Battle"},
            new DefinitionHollow.Pantheon(){Title = "Marmu"},
            new DefinitionHollow.Pantheon(){Title = "Flukemarm"},
            new DefinitionHollow.Pantheon(){Title = "Broken Vessel"},
            new DefinitionHollow.Pantheon(){Title = "Galien"},
            new DefinitionHollow.Pantheon(){Title = "Paintmaster Sheo"},
            new DefinitionHollow.Pantheon(){Title = "Hive Knight"},
            new DefinitionHollow.Pantheon(){Title = "Elder Hu"},
            new DefinitionHollow.Pantheon(){Title = "The Collector"},
            new DefinitionHollow.Pantheon(){Title = "God Tamer"},
            new DefinitionHollow.Pantheon(){Title = "Troupe Master Grim"},
            new DefinitionHollow.Pantheon(){Title = "Watcher Knights"},
            new DefinitionHollow.Pantheon(){Title = "Uumuu"},
            new DefinitionHollow.Pantheon(){Title = "Winged Nosk"},
            new DefinitionHollow.Pantheon(){Title = "Great Nailsage Slay"},
            new DefinitionHollow.Pantheon(){Title = "Hornet (Sentinel)"},
            new DefinitionHollow.Pantheon(){Title = "Enraged Guardian"},
            new DefinitionHollow.Pantheon(){Title = "Lost Kin"},
            new DefinitionHollow.Pantheon(){Title = "No Eyes"},
            new DefinitionHollow.Pantheon(){Title = "Traitor Lord"},
            new DefinitionHollow.Pantheon(){Title = "White Defender"},
            new DefinitionHollow.Pantheon(){Title = "Soul Tyrant"},
            new DefinitionHollow.Pantheon(){Title = "Markoth"},
            new DefinitionHollow.Pantheon(){Title = "Failed Champion"},
            new DefinitionHollow.Pantheon(){Title = "Nightmare King Grimm"},
            new DefinitionHollow.Pantheon(){Title = "Pure Vessel"},
            new DefinitionHollow.Pantheon(){Title = "Absolute Radiance"}

        };*/

        private bool PantheonCase(string title)
        {
            bool shouldSplit = false;
            switch (title)
            {
                case "Vengefly King":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Vengefly") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")) ; break;
                case "Gruz Mother":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Gruz_Mother") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "False Knight":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_False_Knight") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Massive Moss Charger":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Mega_Moss_Charger") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Hornet (Protector)":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Hornet_1") && !(!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Gorb":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Ghost_Gorb") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Dung Defender":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Dung_Defender") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Soul Warrior":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Mage_Knight") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Brooding Mawlek":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Brooding_Mawlek") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Oro & Mato Nail Bros":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Nailmasters") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Xero":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Ghost_Xero") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Crystal Guardian":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Crystal_Guardian") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Soul Master":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Soul_Master") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Oblobbles":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Oblobble") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Sisters of Battle":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Mantis_Lord") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Marmu":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Ghost_Marmu") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Flukemarm":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Flukemarm") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Broken Vessel":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Broken_Vessel") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Galien":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Ghost_Galien") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Paintmaster Sheo":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Painter") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Hive Knight":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Hive_Knight") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Elder Hu":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Ghost_Hu") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "The Collector":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Collector") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "God Tamer":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_God_Tamer") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Troupe Master Grim":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Grimm") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Watcher Knights":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Watcher_Knights") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Uumuu":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Uumuu") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Nosk":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Nosk") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Winged Nosk":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Nosk_Hornet") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Great Nailsage Slay":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Sly") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Hornet (Sentinel)":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Hornet_2") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Enraged Guardian":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Crystal_Guardian_2") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Lost Kin":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Lost_Kin") && !(!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "No Eyes":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Ghost_No_Eyes") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Traitor Lord":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Traitor_Lord") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "White Defender":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_White_Defender") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Soul Tyrant":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Soul_Tyrant") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Markoth":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Ghost_Markoth") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Grey Prince Zote":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Grey_Prince_Zote") && !(!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Failed Champion":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Failed_Champion") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Nightmare King Grimm":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Grimm_Nightmare") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
                case "Pure Vessel":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Hollow_Knight") && (!currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu")); break;
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
                if (dataHollow.PantheonMode == 0)
                {
                    
                    foreach (var element in dataHollow.getPhanteonToSplit())
                    {
                        if (!element.IsSplited && PantheonCase(element.Title))
                        {
                            element.IsSplited = true;
                            try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                            
                            nSplit++;
                        }
                    }
                    if ( (nSplit == tSplit && currentPosition.sceneName.StartsWith("GG_Atrium")) || (nSplit == p5s && currentPosition.sceneName.StartsWith("Cinematic_Ending_E"))){ //P1+2+3+4 o p5
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
                                try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                                element.IsSplited = true;
                            }
                        }


                        if (element.Title == "Pantheon of the Artist" && !element.IsSplited)
                        {
                            if (currentPosition.previousScene.StartsWith("GG_Painter") && !currentPosition.sceneName.StartsWith("GG_Atrium"))
                            {
                                try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                                element.IsSplited = true;
                            }
                        }

                        if (element.Title == "Pantheon of the Sage" && !element.IsSplited)
                        {
                            if (currentPosition.previousScene.StartsWith("GG_Sly") && !currentPosition.sceneName.StartsWith("GG_Atrium"))
                            {
                                try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                                element.IsSplited = true;
                            }
                            
                        }

                        if (element.Title == "Pantheon of the Knight" && !element.IsSplited)
                        {
                            if (currentPosition.previousScene.StartsWith("GG_Hollow_Knight") && !currentPosition.sceneName.StartsWith("GG_Atrium"))
                            {
                                try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                                element.IsSplited = true;
                            }
                        }

                        if (element.Title == "Pantheon of Hallownest" && !element.IsSplited)
                        {
                            if (currentPosition.sceneName.StartsWith("Cinematic_Ending_E"))
                            {
                                try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                                element.IsSplited = true;
                            }
                        }
                    }
                }
            }
        } 
       
        #endregion
    }
}
