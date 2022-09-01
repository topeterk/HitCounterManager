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

namespace AutoSplitterCore
{
    public class DefinitionsDs3
    {
        #region Boss.Ds3
        [Serializable]
        public class BossDs3
        {
            public string Title;
            public uint Id;
            public bool IsSplited = false;
            public string Mode;
        }

        public BossDs3 stringToEnumBoss(string boss)
        {
            BossDs3 cBoss = new BossDs3();
            switch (boss)
            {
                case "Iudex Gundyr": cBoss.Title = "Iudex Gundyr";cBoss.Id = 14000800; break;
                case "Vordt of the Boreal Valley": cBoss.Title = "Vordt of the Boreal Valley"; cBoss.Id = 13000800; break;
                case "Curse-Rotted Greatwood": cBoss.Title = "Curse-Rotted Greatwood"; cBoss.Id = 13100800; break;
                case "Crystal Sage": cBoss.Title = "Crystal Sage"; cBoss.Id = 13300850; break;
                case "Abyss Watchers": cBoss.Title = "Abyss Watchers"; cBoss.Id = 13300800; break;
                case "Deacons of the Deep": cBoss.Title = "Deacons of the Deep"; cBoss.Id = 13500800; break;
                case "High Lord Wolnir": cBoss.Title = "High Lord Wolnir"; cBoss.Id = 13800800; break;
                case "Old Demon King": cBoss.Title = "Old Demon King"; cBoss.Id = 13800830; break;
                case "Pontiff Sulyvahn": cBoss.Title = "Pontiff Sulyvahn"; cBoss.Id = 13700850; break;
                case "Yhorm the Giant": cBoss.Title = "Yhorm the Giant"; cBoss.Id = 13900800; break;
                case "Aldrich, Devourer of Gods": cBoss.Title = "Aldrich, Devourer of Gods"; cBoss.Id = 13700800; break;
                case "Dancer of the Boreal Valley": cBoss.Title = "Dancer of the Boreal Valley"; cBoss.Id = 13000890; break;
                case "Dragonslayer Armour": cBoss.Title = "Dragonslayer Armour"; cBoss.Id = 13010800; break;
                case "Oceiros, the Consumed King": cBoss.Title = "Oceiros, the Consumed King"; cBoss.Id = 13000830; break;
                case "Champion Gundyr": cBoss.Title = "Champion Gundyr"; cBoss.Id = 14000830; break;
                case "Lothric, Younger Prince": cBoss.Title = "Lothric, Younger Prince"; cBoss.Id = 13410830; break;
                case "Ancient Wyvern": cBoss.Title = "Ancient Wyvern"; cBoss.Id = 13200800; break;
                case "Nameless King": cBoss.Title = "Nameless King"; cBoss.Id = 13200850; break;
                case "Soul of Cinder": cBoss.Title = "Soul of Cinder"; cBoss.Id = 14100800; break;
                case "Sister Friede": cBoss.Title = "Sister Friede"; cBoss.Id = 14500800; break;
                case "Champion's Gravetender & Gravetender Greatwolf": cBoss.Title = "Champion's Gravetender & Gravetender Greatwolf"; cBoss.Id = 14500860; break;
                case "Demon in Pain & Demon From Below / Demon Prince": cBoss.Title = "Demon in Pain & Demon From Below / Demon Prince"; cBoss.Id = 15000800; break;
                case "Halflight, Spear of the Church": cBoss.Title = "Halflight, Spear of the Church"; cBoss.Id = 15100800; break;
                case "Darkeater Midir": cBoss.Title = "Darkeater Midir"; cBoss.Id = 15100850; break;
                case "Slave Knight Gael": cBoss.Title = "Slave Knight Gael"; cBoss.Id = 15110800; break;
                default: cBoss = null; break;
            }
            return cBoss;
        }
        #endregion
        #region Bonfire.Ds3
        [Serializable]
        public class BonfireDs3
        {
            public string Title;
            public uint Id;
            public bool IsSplited = false;
            public string Mode;
        }

        public BonfireDs3 stringToEnumBonfire(string Bonfire)
        {
            BonfireDs3 cBonfire = new BonfireDs3();
            switch (Bonfire)
            {
                case "Firelink Shrine": cBonfire.Title = "Firelink Shrine"; cBonfire.Id = 14000000; break;
                case "Cemetery of Ash": cBonfire.Title = "Cemetery of Ash"; cBonfire.Id = 14000001; break;
                case "Iudex Gundyr": cBonfire.Title = "Iudex Gundyr"; cBonfire.Id = 14000002; break;
                case "Untended Graves": cBonfire.Title = "Untended Graves"; cBonfire.Id = 14000003; break;
                case "Champion Gundyr": cBonfire.Title = "Champion Gundyr"; cBonfire.Id = 14000004; break;
                case "High Wall of Lothric": cBonfire.Title = "High Wall of Lothric"; cBonfire.Id = 13000009; break;
                case "Tower on the Wall": cBonfire.Title = "Tower on the Wall"; cBonfire.Id = 13000005; break;
                case "Vordt of the Boreal Valley": cBonfire.Title = "Vordt of the Boreal Valley"; cBonfire.Id = 13000002; break;
                case "Dancer of the Boreal Valley": cBonfire.Title = "Dancer of the Boreal Valley"; cBonfire.Id = 13000004; break;
                case "Oceiros, the Consumed King": cBonfire.Title = "Oceiros, the Consumed King"; cBonfire.Id = 13000001; break;
                case "Foot of the High Wall": cBonfire.Title = "Foot of the High Wall"; cBonfire.Id = 13100004; break;
                case "Undead Settlement": cBonfire.Title = "Undead Settlement"; cBonfire.Id = 13100000; break;
                case "Cliff Underside": cBonfire.Title = "Cliff Underside"; cBonfire.Id = 13100002; break;
                case "Dilapidated Bridge": cBonfire.Title = "Dilapidated Bridge"; cBonfire.Id = 13100003; break;
                case "Pit of Hollows": cBonfire.Title = "Pit of Hollows"; cBonfire.Id = 13100001; break;
                case "Road of Sacrifices": cBonfire.Title = "Road of Sacrifices"; cBonfire.Id = 13300006; break;
                case "Halfway Fortress": cBonfire.Title = "Halfway Fortress"; cBonfire.Id = 13300000; break;
                case "Crucifixion Woods": cBonfire.Title = "Crucifixion Woods"; cBonfire.Id = 13300007; break;
                case "Crystal Sage": cBonfire.Title = "Crystal Sage"; cBonfire.Id = 13300002; break;
                case "Farron Keep": cBonfire.Title = "Farron Keep"; cBonfire.Id = 13300003; break;
                case "Keep Ruins": cBonfire.Title = "Keep Ruins"; cBonfire.Id = 13300004; break;
                case "Farron Keep Perimeter": cBonfire.Title = "Farron Keep Perimeter"; cBonfire.Id = 13300008; break;
                case "Old Wolf of Farron": cBonfire.Title = "Old Wolf of Farron"; cBonfire.Id = 13300005; break;
                case "Abyss Watchers": cBonfire.Title = "Abyss Watchers"; cBonfire.Id = 13300001; break;
                case "Cathedral of the Deep": cBonfire.Title = "Cathedral of the Deep"; cBonfire.Id = 13500003; break;
                case "Cleansing Chapel": cBonfire.Title = "Cleansing Chapel"; cBonfire.Id = 13500000; break;
                case "Rosaria's Bed Chamber": cBonfire.Title = "Rosaria's Bed Chamber"; cBonfire.Id = 13500002; break;
                case "Deacons of the Deep": cBonfire.Title = "Deacons of the Deep"; cBonfire.Id = 13500001; break;
                case "Catacombs of Carthus": cBonfire.Title = "Catacombs of Carthus"; cBonfire.Id = 13800006; break;
                case "High Lord Wolnir": cBonfire.Title = "High Lord Wolnir"; cBonfire.Id = 13800000; break;
                case "Abandoned Tomb": cBonfire.Title = "Abandoned Tomb"; cBonfire.Id = 13800001; break;
                case "Old King's Antechamber": cBonfire.Title = "Old King's Antechamber"; cBonfire.Id = 13800002; break;
                case "Demon Ruins": cBonfire.Title = "Demon Ruins"; cBonfire.Id = 13800003; break;
                case "Old Demon King": cBonfire.Title = "Old Demon King"; cBonfire.Id = 13800004; break;
                case "Irithyll of the Boreal Valley": cBonfire.Title = "Irithyll of the Boreal Valley"; cBonfire.Id = 13700007; break;
                case "Central Irithyll": cBonfire.Title = "Central Irithyll"; cBonfire.Id = 13700004; break;
                case "Church of Yorshka": cBonfire.Title = "Church of Yorshka"; cBonfire.Id = 13700000; break;
                case "Distant Manor": cBonfire.Title = "Distant Manor"; cBonfire.Id = 13700005; break;
                case "Pontiff Sulyvahn": cBonfire.Title = "Pontiff Sulyvahn"; cBonfire.Id = 13700001; break;
                case "Water Reserve": cBonfire.Title = "Water Reserve"; cBonfire.Id = 13700006; break;
                case "Anor Londo": cBonfire.Title = "Anor Londo"; cBonfire.Id = 13700003; break;
                case "Prison Tower": cBonfire.Title = "Prison Tower"; cBonfire.Id = 13700008; break;
                case "Aldrich, Devourer of Gods": cBonfire.Title = "Aldrich, Devourer of Gods"; cBonfire.Id = 13700002; break;
                case "Irithyll Dungeon": cBonfire.Title = "Irithyll Dungeon"; cBonfire.Id = 13900000; break;
                case "Profaned Capital": cBonfire.Title = "Profaned Capital"; cBonfire.Id = 13900002; break;
                case "Yhorm the Giant": cBonfire.Title = "Yhorm the Giant"; cBonfire.Id = 13900001; break;
                case "Lothric Castle": cBonfire.Title = "Lothric Castle"; cBonfire.Id = 13010000; break;
                case "Dragon Barracks": cBonfire.Title = "Dragon Barracks"; cBonfire.Id = 13010002; break;
                case "Dragonslayer Armour": cBonfire.Title = "Dragonslayer Armour"; cBonfire.Id = 13010001; break;
                case "Grand Archives": cBonfire.Title = "Grand Archives"; cBonfire.Id = 13410001; break;
                case "Twin Princes": cBonfire.Title = "Twin Princes"; cBonfire.Id = 13410000; break;
                case "Archdragon Peak": cBonfire.Title = "Archdragon Peak"; cBonfire.Id = 13200000; break;
                case "Dragon-Kin Mausoleum": cBonfire.Title = "Dragon-Kin Mausoleum"; cBonfire.Id = 13200003; break;
                case "Great Belfry": cBonfire.Title = "Great Belfry"; cBonfire.Id = 13200002; break;
                case "Nameless King": cBonfire.Title = "Nameless King"; cBonfire.Id = 13200001; break;
                case "Flameless Shrine": cBonfire.Title = "Flameless Shrine"; cBonfire.Id = 14100000; break;
                case "Kiln of the First Flame": cBonfire.Title = "Kiln of the First Flame"; cBonfire.Id = 14100001; break;
                case "Snowfield": cBonfire.Title = "Snowfield"; cBonfire.Id = 14500001; break;
                case "Rope Bridge Cave": cBonfire.Title = "Rope Bridge Cave"; cBonfire.Id = 14500002; break;
                case "Corvian Settlement": cBonfire.Title = "Corvian Settlement"; cBonfire.Id = 14500003; break;
                case "Snowy Mountain Pass": cBonfire.Title = "Snowy Mountain Pass"; cBonfire.Id = 14500004; break;
                case "Ariandel Chapel": cBonfire.Title = "Ariandel Chapel"; cBonfire.Id = 14500005; break;
                case "Sister Friede": cBonfire.Title = "Sister Friede"; cBonfire.Id = 14500000; break;
                case "Depths of the Painting": cBonfire.Title = "Depths of the Painting"; cBonfire.Id = 14500007; break;
                case "Champion's Gravetender": cBonfire.Title = "Champion's Gravetender"; cBonfire.Id = 14500006; break;
                case "The Dreg Heap": cBonfire.Title = "The Dreg Heap"; cBonfire.Id = 15000001; break;
                case "Earthen Peak Ruins": cBonfire.Title = "Earthen Peak Ruins"; cBonfire.Id = 15000002; break;
                case "Within the Earthen Peak Ruins": cBonfire.Title = "Within the Earthen Peak Ruins"; cBonfire.Id = 15000003; break;
                case "The Demon Prince": cBonfire.Title = "The Demon Prince"; cBonfire.Id = 15000000; break;
                case "Mausoleum Lookout": cBonfire.Title = "Mausoleum Lookout"; cBonfire.Id = 15100002; break;
                case "Ringed Inner Wall": cBonfire.Title = "Ringed Inner Wall"; cBonfire.Id = 15100003; break;
                case "Ringed City Streets": cBonfire.Title = "Ringed City Streets"; cBonfire.Id = 15100004; break;
                case "Shared Grave": cBonfire.Title = "Shared Grave"; cBonfire.Id = 15100005; break;
                case "Church of Filianore": cBonfire.Title = "Church of Filianore"; cBonfire.Id = 15100000; break;
                case "Darkeater Midir": cBonfire.Title = "Darkeater Midir"; cBonfire.Id = 15100001; break;
                case "Filianore's Rest": cBonfire.Title = "Filianore's Rest"; cBonfire.Id = 15110001; break;
                case "Slave Knight Gael": cBonfire.Title = "Slave Knight Gael"; cBonfire.Id = 15110000; break;
                default: cBonfire = null; break;
            }
            return cBonfire;
        }
        #endregion
        #region Lvl.Ds3
        [Serializable]
        public class LvlDs3
        {
            public SoulMemory.DarkSouls3.Attribute Attribute;
            public uint Value;
            public bool IsSplited = false;
            public string Mode;
        }

        public SoulMemory.DarkSouls3.Attribute stringToEnumAttribute(string attribute)
        {
            switch (attribute)
            {
                case "Vigor":
                    return SoulMemory.DarkSouls3.Attribute.Vigor;
                case "Attunement":
                    return SoulMemory.DarkSouls3.Attribute.Attunement;
                case "Endurance":
                    return SoulMemory.DarkSouls3.Attribute.Endurance;
                case "Vitality":
                    return SoulMemory.DarkSouls3.Attribute.Vitality;
                case "Strength":
                    return SoulMemory.DarkSouls3.Attribute.Strength;
                case "Dexterity":
                    return SoulMemory.DarkSouls3.Attribute.Dexterity;
                case "Intelligence":
                    return SoulMemory.DarkSouls3.Attribute.Intelligence;
                case "Faith":
                    return SoulMemory.DarkSouls3.Attribute.Faith;
                case "Luck":
                    return SoulMemory.DarkSouls3.Attribute.Luck;
                case "SoulLevel":
                    return SoulMemory.DarkSouls3.Attribute.SoulLevel;
                case "Humanity":
                    return SoulMemory.DarkSouls3.Attribute.Humanity;
                default: return SoulMemory.DarkSouls3.Attribute.Luck;
            }
        }
        #endregion
        #region CustomFlag.Ds3
        public class CfDs3
        {
            public uint Id;
            public bool IsSplited = false;
            public string Mode;
        }
        #endregion
    }

    [Serializable]
    public class DTDs3
    {
        //var Settings
        public bool enableSplitting = false;
        public bool autoTimer = false;
        public bool gameTimer = false;
        //Flags to Split
        public List<DefinitionsDs3.BossDs3> bossToSplit = new List<DefinitionsDs3.BossDs3>();
        public List<DefinitionsDs3.BonfireDs3> bonfireToSplit = new List<DefinitionsDs3.BonfireDs3>();
        public List<DefinitionsDs3.LvlDs3> lvlToSplit = new List<DefinitionsDs3.LvlDs3>();
        public List<DefinitionsDs3.CfDs3> flagToSplit = new List<DefinitionsDs3.CfDs3>();

        public List<DefinitionsDs3.BossDs3> getBossToSplit()
        {
            return this.bossToSplit;
        }

        public List<DefinitionsDs3.BonfireDs3> getBonfireToSplit()
        {
            return this.bonfireToSplit;
        }

        public List<DefinitionsDs3.LvlDs3> getLvlToSplit()
        {
            return this.lvlToSplit;
        }

        public List<DefinitionsDs3.CfDs3> getFlagToSplit()
        {
            return this.flagToSplit;
        }
    }
}
