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
using SoulMemory;


namespace AutoSplitterCore
{
    public class DefinitionsSekiro
    {

        #region Boss.Sekiro
        [Serializable]
        public class BossS
        {
            public string Title;
            public uint Id;
            public bool IsSplited;
            public string Mode;

            public void BossDate(string title, uint id)
            {
                this.Title = title;
                this.Id = id;
                this.IsSplited = false;
            }

        }

        public BossS BossToEnum(string Nboss)
        {
            BossS boss = new BossS();
            switch (Nboss)
            {
                case "Genichiro Ashina - Tutorial":
                    boss.BossDate("Genichiro Ashina - Tutorial", 11120803); break;
                case "Gyoubu Masataka Oniwa":
                      boss.BossDate("Gyoubu Masataka Oniwa", 9301);
                    break;
                case "Lady Butterfly":
                    boss.BossDate("Lady Butterfly", 9302);
                    break;
                case "Genichiro Ashina":
                    boss.BossDate("Genichiro Ashina", 9303);
                    break;
                case "Folding Screen Monkeys":
                    boss.BossDate("Folding Screen Monkeys", 9305);
                    break;
                case "Guardian Ape":
                    boss.BossDate("Guardian Ape", 9304);
                    break;
                case "Headless Ape":
                    boss.BossDate("Headless Ape", 9307);
                    break;
                case "Corrupted Monk (ghost)":
                    boss.BossDate("Corrupted Monk (ghost)", 9306);
                    break;
                case "Emma, the Gentle Blade":
                    boss.BossDate("Emma, the Gentle Blade", 9315);
                    break;
                case "Isshin Ashina":
                    boss.BossDate("Isshin Ashina", 9316);
                    break;
                case "Great Shinobi Owl":
                    boss.BossDate("Great Shinobi Owl", 9308);
                    break;
                case "True Corrupted Monk":
                    boss.BossDate("True Corrupted Monk", 9309);
                    break;
                case "Divine Dragon":
                    boss.BossDate("Divine Dragon", 9310);
                    break;
                case "Owl (Father)":
                    boss.BossDate("Owl (Father)", 9317);
                    break;
                case "Demon of Hatred":
                    boss.BossDate("Demon of Hatred", 9313);
                    break;
                case "Isshin, the Sword Saint":
                    boss.BossDate("Isshin, the Sword Saint", 9312);
                    break;
            }
            return boss;
        }
        #endregion
        #region Idol.Sekiro
        [Serializable]
        public class Idol
        {
            public string Title;
            public string Location;
            public uint Id;
            public bool IsSplited = false;
            public string Mode;
            public void IdolDate(string Title, string Location, uint Id)
            {
                this.Title = Title;
                this.Location = Location;
                this.Id = Id;
                this.IsSplited = false;
            }
        }
        public Idol idolToEnum(string NIdol)
        {
            Idol idol = new Idol();
            switch (NIdol)
            {
                //Ashina Outskirts
                case "Dilapidated Temple":
                    idol.IdolDate("Dilapidated Temple", "Ashina Outskirts", 11100000);
                    break;
                case "Ashina Outskirts":
                    idol.IdolDate("Ashina Outskirts", "Ashina Outskirts", 11100006);
                    break;
                case "Outskirts Wall - Gate Path":
                    idol.IdolDate("Outskirts Wall - Gate Path", "Ashina Outskirts", 11100001);
                    break;
                case "Outskirts Wall - Stairway":
                    idol.IdolDate("Outskirts Wall - Stairway", "Ashina Outskirts", 11100002);
                    break;
                case "Underbridge Valley":
                    idol.IdolDate("Underbridge Valley", "Ashina Outskirts", 11100003);
                    break;
                case "Ashina Castle Fortress":
                    idol.IdolDate("Ashina Castle Fortress", "Ashina Outskirts", 11100004);
                    break;
                case "Ashina Castle Gate":
                    idol.IdolDate("Ashina Castle Gate", "Ashina Outskirts", 11100005);
                    break;
                case "Flames of Hatred":
                    idol.IdolDate("Flames of Hatred", "Ashina Outskirts", 11100007);
                    break;
                //Hirata Estate
                case "Dragonspring - Hirata Estate":
                    idol.IdolDate("Dragonspring - Hirata Estate", "Hirata Estate", 11000000);
                    break;
                case "Estate Path":
                    idol.IdolDate("Estate Path", "Hirata Estate", 11000001);
                    break;
                case "Bamboo Thicket Slope":
                    idol.IdolDate("Bamboo Thicket Slope", "Hirata Estate", 11000002);
                    break;
                case "Hirata Estate - Main Hal":
                    idol.IdolDate("Hirata Estate - Main Hal", "Hirata Estate", 11000003);
                    break;
                case "Hirata Estate - Hidden Temple":
                    idol.IdolDate("Hirata Estate - Hidden Temple", "Hirata Estate", 11000004);
                    break;
                case "Hirata Audience Chamber":
                    idol.IdolDate("Hirata Audience Chamber", "Hirata Estate", 11000005);
                    break;
                //Ashina Castle
                case "Ashina Castle":
                    idol.IdolDate("Ashina Castle", "Ashina Castle", 11110000);
                    break;
                case "Upper Tower - Antechamber":
                    idol.IdolDate("Upper Tower - Antechamber", "Ashina Castle", 11110001);
                    break;
                case "Upper Tower - Ashina Dojo":
                    idol.IdolDate("Upper Tower - Ashina Dojo", "Ashina Castle", 11110007);
                    break;
                case "Castle Tower Lookout":
                    idol.IdolDate("Castle Tower Lookout", "Ashina Castle", 11110002);
                    break;
                case "Upper Tower - Kuro's Room":
                    idol.IdolDate("Upper Tower - Kuro's Room", "Ashina Castle", 11110006);
                    break;
                case "Old Grave":
                    idol.IdolDate("Old Grave", "Ashina Castle", 11110003);
                    break;
                case "Great Serpent Shrine":
                    idol.IdolDate("Great Serpent Shrine", "Ashina Castle", 11110004);
                    break;
                case "Abandoned Dungeon Entrance":
                    idol.IdolDate("Abandoned Dungeon Entrance", "Ashina Castle", 11110005);
                    break;
                case "Ashina Reservoir":
                    idol.IdolDate("Ashina Reservoir", "Ashina Castle", 11120001);
                    break;
                case "Near Secret Passage":
                    idol.IdolDate("Near Secret Passage", "Ashina Castle", 11120000);
                    break;
                //Abandoned Dungeon
                case "Underground Waterway":
                    idol.IdolDate("Underground Waterway", "Abandoned Dungeon", 11300000);
                    break;
                case "Bottomless Hole":
                    idol.IdolDate("Bottomless Hole", "Abandoned Dungeon", 11300001);
                    break;
                //Senpou Temple
                case "Senpou Temple, Mt. Kongo":
                    idol.IdolDate("Senpou Temple, Mt. Kongo", "Senpou Temple, Mt. Kongo", 12000000);
                    break;
                case "Shugendo":
                    idol.IdolDate("Shugendo", "Senpou Temple, Mt. Kongo", 12000001);
                    break;
                case "Temple Grounds":
                    idol.IdolDate("Temple Grounds", "Senpou Temple, Mt. Kongo", 12000002);
                    break;
                case "Main Hall":
                    idol.IdolDate("Main Hall", "Senpou Temple, Mt. Kongo", 12000003);
                    break;
                case "Inner Sanctum":
                    idol.IdolDate("Inner Sanctum", "Senpou Temple, Mt. Kongo", 12000004);
                    break;
                case "Sunken Valley Cavern":
                    idol.IdolDate("Sunken Valley Cavern", "Senpou Temple, Mt. Kongo", 12000005);
                    break;
                case "Bell Demon's Temple":
                    idol.IdolDate("Bell Demon's Temple", "Senpou Temple, Mt. Kongo", 12000006);
                    break;
                //Sunken Valley
                case "Under-Shrine Valley":
                    idol.IdolDate("Under-Shrine Valley", "Sunken Valley", 11700007);
                    break;
                case "Sunken Valley":
                    idol.IdolDate("Sunken Valley", "Sunken Valley", 11700000);
                    break;
                case "Gun Fort":
                    idol.IdolDate("Gun Fort", "Sunken Valley", 11700001);
                    break;
                case "Riven Cave":
                    idol.IdolDate("Riven Cave", "Sunken Valley", 11700002);
                    break;
                case "Bodhisattva Valley":
                    idol.IdolDate("Bodhisattva Valley", "Sunken Valley", 11700008);
                    break;
                case "Guardian Ape's Watering Hole":
                    idol.IdolDate("Guardian Ape's Watering Hole", "Sunken Valley", 11700003);
                    break;
                //Ashina Depths
                case "Ashina Depths":
                    idol.IdolDate("Ashina Depths", "Ashina Depths", 11700005);
                    break;
                case "Poison Pool":
                    idol.IdolDate("Poison Pool", "Ashina Depths", 11700004);
                    break;
                case "Guardian Ape's Burrow":
                    idol.IdolDate("Guardian Ape's Burrow", "Ashina Depths", 11700006);
                    break;
                case "Hidden Forest":
                    idol.IdolDate("Hidden Forest", "Ashina Depths", 11500000);
                    break;
                case "Mibu Village":
                    idol.IdolDate("Mibu Village", "Ashina Depths", 11500001);
                    break;
                case "Water Mill":
                    idol.IdolDate("Water Mill", "Ashina Depths", 11500002);
                    break;
                case "Wedding Cave Door":
                    idol.IdolDate("Wedding Cave Door", "Ashina Depths", 11500003);
                    break;
                //Fountainhead Palace
                case "Fountainhead Palace":
                    idol.IdolDate("Fountainhead Palace", "Fountainhead Palace", 12500000);
                    break;
                case "Vermilion Bridge":
                    idol.IdolDate("Vermilion Bridge", "Fountainhead Palace", 12500001);
                    break;
                case "Mibu Manor":
                    idol.IdolDate("Mibu Manor", "Fountainhead Palace", 12500006);
                    break;
                case "Flower Viewing Stage":
                    idol.IdolDate("Flower Viewing Stage", "Fountainhead Palace", 12500002);
                    break;
                case "Great Sakura":
                    idol.IdolDate("Great Sakura", "Fountainhead Palace", 12500003);
                    break;
                case "Palace Grounds":
                    idol.IdolDate("Palace Grounds", "Fountainhead Palace", 12500004);
                    break;
                case "Feeding Grounds":
                    idol.IdolDate("Feeding Grounds", "Fountainhead Palace", 12500007);
                    break;
                case "Near Pot Noble":
                    idol.IdolDate("Near Pot Noble", "Fountainhead Palace", 12500008);
                    break;
                case "Sanctuary":
                    idol.IdolDate("Sanctuary", "Fountainhead Palace", 12500005);
                    break;
            }
            return idol;
        }
        #endregion
        #region Position.Sekiro
        [Serializable]
        public class PositionS
        {
            public Vector3f vector = new Vector3f();
            public bool IsSplited = false;
            public string Mode;
            public void setVector(Vector3f vector)
            {
                this.vector = vector;
            }
        }


        #endregion
        #region CustomFlag.Sekiro
        [Serializable]
        public class CfSk
        {
            public uint Id;
            public bool IsSplited = false;
            public string Mode;
        }

        #endregion
    }


    [Serializable]
    public class DTSekiro
    {
        //Settings Vars
        public bool enableSplitting = false;
        public bool autoTimer = false;
        public bool gameTimer = false;
        //Flags to Split
        public bool mortalJourneyRun = false;
        public List<DefinitionsSekiro.BossS> bossToSplit = new List<DefinitionsSekiro.BossS>();
        public List<DefinitionsSekiro.Idol> idolsTosplit = new List<DefinitionsSekiro.Idol>();
        public List<DefinitionsSekiro.PositionS> positionsToSplit = new List<DefinitionsSekiro.PositionS>();
        public List<DefinitionsSekiro.CfSk> flagToSplit = new List<DefinitionsSekiro.CfSk>();
        public int positionMargin = 3;


        public List<DefinitionsSekiro.BossS> getBossToSplit()
        {
            return this.bossToSplit;
        }

        public List<DefinitionsSekiro.Idol> getidolsTosplit()
        {
            return this.idolsTosplit;
        }

        public List<DefinitionsSekiro.PositionS> getPositionsToSplit()
        {
            return this.positionsToSplit;
        }

        public List<DefinitionsSekiro.CfSk> getFlagToSplit()
        {
            return this.flagToSplit;
        }
    }
}
