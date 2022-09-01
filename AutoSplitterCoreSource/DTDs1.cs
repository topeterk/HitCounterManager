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
using SoulMemory.DarkSouls1;

namespace AutoSplitterCore
{
    public class DefinitionsDs1
    {
        #region Boss.Ds1
        [Serializable]
        public class BossDs1
        {
            public string Title;
            public uint Id;
            public bool IsSplited = false;
            public string Mode;
        }

        public BossDs1 stringToEnumBoss(string boss)
        {
            BossDs1 cBoss = new BossDs1();
            switch (boss)
            {
                case "Asylum Demon": cBoss.Title = "Asylum Demon"; cBoss.Id = 16; break;
                case "Bell Gargoyle": cBoss.Title = "Bell Gargoyle"; cBoss.Id = 3; break;
                case "Capra Demon": cBoss.Title = "Capra Demon"; cBoss.Id = 11010902; break;
                case "Ceaseless Discharge": cBoss.Title = "Ceaseless Discharge"; cBoss.Id = 11410900; break;
                case "Centipede Demon": cBoss.Title = "Centipede Demon"; cBoss.Id = 11410901; break;
                case "Chaos Witch Quelaag": cBoss.Title = "Chaos Witch Quelaag"; cBoss.Id = 9; break;
                case "Crossbreed Priscilla": cBoss.Title = "Crossbreed Priscilla"; cBoss.Id = 4; break;
                case "Dark Sun Gwyndolin": cBoss.Title = "Dark Sun Gwyndolin"; cBoss.Id = 11510900; break;
                case "Demon Firesage": cBoss.Title = "Demon Firesage"; cBoss.Id = 11410410; break;
                case "Four Kings": cBoss.Title = "Four Kings"; cBoss.Id = 13; break;
                case "Gaping Dragon": cBoss.Title = "Gaping Dragon"; cBoss.Id = 2; break;
                case "Great Grey Wolf Sif": cBoss.Title = "Great Grey Wolf Sif"; cBoss.Id = 5; break;
                case "Gwyn Lord of Cinder": cBoss.Title = "Gwyn Lord of Cinder"; cBoss.Id = 15; break;
                case "Iron Golem": cBoss.Title = "Iron Golem"; cBoss.Id = 11; break;
                case "Moonlight Butterfly": cBoss.Title = "Moonlight Butterfly"; cBoss.Id = 11200900; break;
                case "Nito": cBoss.Title = "Nito"; cBoss.Id = 7; break;
                case "Ornstein And Smough": cBoss.Title = "Ornstein And Smough"; cBoss.Id = 12; break;
                case "Pinwheel": cBoss.Title = "Pinwheel"; cBoss.Id = 6; break;
                case "Seath the Scaleless": cBoss.Title = "Seath the Scaleless"; cBoss.Id = 14; break;
                case "Stray Demon": cBoss.Title = "Stray Demon"; cBoss.Id = 11810900; break;
                case "Taurus Demon": cBoss.Title = "Taurus Demon"; cBoss.Id = 11010901; break;
                case "The Bed of Chaos": cBoss.Title = "The Bed of Chaos"; cBoss.Id = 10; break;
                case "Artorias the Abysswalker": cBoss.Title = "Artorias the Abysswalker"; cBoss.Id = 11210001; break;
                case "Black Dragon Kalameet": cBoss.Title = "Black Dragon Kalameet"; cBoss.Id = 11210004; break;
                case "Manus, Father of the Abyss": cBoss.Title = "Manus, Father of the Abyss"; cBoss.Id = 11210002; break;
                case "Sanctuary Guardian": cBoss.Title = "Sanctuary Guardian"; cBoss.Id = 11210000; break;
                default: cBoss = null; break;
            }
            return cBoss;
        }
        #endregion
        #region Bonfire.Ds1
        public class BonfireDs1
        {
            public string Title;
            public Bonfire Id;
            public BonfireState Value;
            public bool IsSplited = false;
            public string Mode;
        }

        public BonfireState stringtoEnumBonfireState(string state)
        {
            switch (state)
            {
                case "Discovered": return BonfireState.Discovered;
                case "Unlocked (R)": return BonfireState.Unlocked;
                case "Kindled 1": return BonfireState.Kindled1;
                case "Kindled 2": return BonfireState.Kindled2;
                case "Kindled 3": return BonfireState.Kindled3;
                default: return BonfireState.Unknown;
            }
        }
        public BonfireDs1 stringToEnumBonfire(string bonfire)
        {
            BonfireDs1 cBon = new BonfireDs1();
            switch (bonfire)
            {
                case "Undead Asylum - Courtyard": cBon.Title = "Undead Asylum - Courtyard"; cBon.Id = Bonfire.UndeadAsylumCourtyard; break;
                case "Undead Asylum - Interior": cBon.Title = "Undead Asylum - Interior"; cBon.Id = Bonfire.UndeadAsylumInterior; break;
                case "Firelink Shrine": cBon.Title = "Firelink Shrine"; cBon.Id = Bonfire.FirelinkShrine; break;
                case "Firelink Altar - Lordvessel": cBon.Title = "Firelink Altar - Lordvessel"; cBon.Id = Bonfire.FirelinkAltarLordvessel; break;
                case "Undead Burg": cBon.Title = "Undead Burg"; cBon.Id = Bonfire.UndeadBurg; break;
                case "Undead Burg - Sunlight Altar": cBon.Title = "Undead Burg - Sunlight Altar"; cBon.Id = Bonfire.UndeadBurgSunlightAltar; break;
                case "Undead Parish": cBon.Title = "Undead Parish"; cBon.Id = Bonfire.UndeadParishAndre; break;
                case "Darkroot Garden": cBon.Title = "Darkroot Garden"; cBon.Id = Bonfire.DarkrootGarden; break;
                case "Darkroot Basin": cBon.Title = "Darkroot Basin"; cBon.Id = Bonfire.DarkrootBasin; break;
                case "Depths": cBon.Title = "Depths"; cBon.Id = Bonfire.Depths; break;
                case "Blighttown Catwalk": cBon.Title = "Blighttown Catwalk"; cBon.Id = Bonfire.BlighttownCatwalk; break;
                case "Blighttown Swap": cBon.Title = "Blighttown Swap"; cBon.Id = Bonfire.BlighttownSwamp; break;
                case "Quelaag's Domain - DaughterOfChaos": cBon.Title = "Quelaag's Domain - DaughterOfChaos"; cBon.Id = Bonfire.DaughterOfChaos; break;
                case "The Great Hollow": cBon.Title = "The Great Hollow"; cBon.Id = Bonfire.TheGreatHollow; break;
                case "Ash Lake": cBon.Title = "Ash Lake"; cBon.Id = Bonfire.AshLake; break;
                case "Ash Lake - Stone Dragon": cBon.Title = "Ash Lake - Stone Dragon"; cBon.Id = Bonfire.AshLakeDragon; break;
                case "Demon Ruins - Entrance": cBon.Title = "Demon Ruins - Entrance"; cBon.Id = Bonfire.DemonRuinsEntrance; break;
                case "Demon Ruins - Staircase": cBon.Title = "Demon Ruins - Staircase"; cBon.Id = Bonfire.DemonRuinsStaircase; break;
                case "Demon Ruins - Catacombs": cBon.Title = "Demon Ruins - Catacombs"; cBon.Id = Bonfire.DemonRuinsCatacombs; break;
                case "Lost Izalith - Lava Pits": cBon.Title = "Lost Izalith - Lava Pits"; cBon.Id = Bonfire.LostIzalithLavaPits; break;
                case "Lost Izalith - 2 (illusory wall)": cBon.Title = "Lost Izalith - 2 (illusory wall)"; cBon.Id = Bonfire.LostIzalith2; break;
                case "Lost Izalith Heart of Chaos": cBon.Title = "Lost Izalith Heart of Chaos"; cBon.Id = Bonfire.LostIzalithHeartOfChaos; break;
                case "Sen's Fortress": cBon.Title = "Sen's Fortress"; cBon.Id = Bonfire.SensFortress; break;
                case "Anor Londo": cBon.Title = "Anor Londo"; cBon.Id = Bonfire.AnorLondo; break;
                case "Anor Londo Darkmoon Tomb": cBon.Title = "Anor Londo Darkmoon Tomb"; cBon.Id = Bonfire.AnorLondoDarkmoonTomb; break;
                case "Anor Londo Residence": cBon.Title = "Anor Londo Residence"; cBon.Id = Bonfire.AnorLondoResidence; break;
                case "Anor Londo Chamber of the Princess": cBon.Title = "Anor Londo Chamber of the Princess"; cBon.Id = Bonfire.AnorLondoChamberOfThePrincess; break;
                case "Painted World of Ariamis": cBon.Title = "Painted World of Ariamis"; cBon.Id = Bonfire.PaintedWorldOfAriamis; break;
                case "The Duke's Archives 1 (entrance)": cBon.Title = "The Duke's Archives 1 (entrance)"; cBon.Id = Bonfire.DukesArchives1; break;
                case "The Duke's Archives 2 (prison cell)": cBon.Title = "The Duke's Archives 2 (prison cell)"; cBon.Id = Bonfire.DukesArchives2; break;
                case "The Duke's Archives 3 (balcony)": cBon.Title = "The Duke's Archives 3 (balcony)"; cBon.Id = Bonfire.DukesArchives3; break;
                case "Crystal Cave": cBon.Title = "Crystal Cave"; cBon.Id = Bonfire.CrystalCave; break;
                case "Catacombs 1 (necromancer)": cBon.Title = "Catacombs 1 (necromancer)"; cBon.Id = Bonfire.Catacombs1; break;
                case "Catacombs 2 (illusory wall)": cBon.Title = "Catacombs 2 (illusory wall)"; cBon.Id = Bonfire.Catacombs2; break;
                case "Tomb of the Giants - 1 (patches)": cBon.Title = "Tomb of the Giants - 1 (patches)"; cBon.Id = Bonfire.TombOfTheGiantsPatches; break;
                case "Tomb of the Giants - 2": cBon.Title = "Tomb of the Giants - 2"; cBon.Id = Bonfire.TombOfTheGiants2; break;
                case "Tomb of the Giants - Altar of the Gravelord": cBon.Title = "Tomb of the Giants - Altar of the Gravelord"; cBon.Id = Bonfire.TombOfTheGiantsAltarOfTheGravelord; break;
                case "The Abyss": cBon.Title = "The Abyss"; cBon.Id = Bonfire.TheAbyss; break;
                case "Oolacile - Sanctuary Garden": cBon.Title = "Oolacile - Sanctuary Garden"; cBon.Id = Bonfire.OolacileSanctuaryGarden; break;
                case "Oolacile - Sanctuary": cBon.Title = "Oolacile - Sanctuary"; cBon.Id = Bonfire.OolacileSanctuary; break;
                case "Oolacile - Township": cBon.Title = "Oolacile - Township"; cBon.Id = Bonfire.OolacileTownship; break;
                case "Oolacile - Township Dungeon": cBon.Title = "Oolacile - Township Dungeon"; cBon.Id = Bonfire.OolacileTownshipDungeon; break;
                case "Chasm of the Abyss": cBon.Title = "Chasm of the Abyss"; cBon.Id = Bonfire.ChasmOfTheAbyss; break;
                default: cBon = null; break;
            }
            return cBon;
        }
        #endregion
        #region Lvl.Ds1
        public class LvlDs1
        {
            public SoulMemory.DarkSouls1.Attribute Attribute;
            public uint Value;
            public bool IsSplited = false;
            public string Mode;
        }
        
        public SoulMemory.DarkSouls1.Attribute stringToEnumAttribute(string attribute)
        {
            LvlDs1 cLvl = new LvlDs1();
            switch (attribute)
            {

                case "Vitality":
                    return SoulMemory.DarkSouls1.Attribute.Vitality;
                case "Attunement":
                    return SoulMemory.DarkSouls1.Attribute.Attunement;
                case "Endurance":
                    return SoulMemory.DarkSouls1.Attribute.Endurance;
                case "Strength":
                    return SoulMemory.DarkSouls1.Attribute.Strength;
                case "Dexterity":
                    return SoulMemory.DarkSouls1.Attribute.Dexterity;
                case "Resistance":
                    return SoulMemory.DarkSouls1.Attribute.Resistance;
                case "Intelligence":
                    return SoulMemory.DarkSouls1.Attribute.Intelligence;
                case "Faith":
                    return SoulMemory.DarkSouls1.Attribute.Faith;
                case "SoulLevel":
                    return SoulMemory.DarkSouls1.Attribute.SoulLevel;
                case "Humanity":
                    return SoulMemory.DarkSouls1.Attribute.Humanity;
                default: return SoulMemory.DarkSouls1.Attribute.SoulLevel;
            }
        }
        #endregion
        #region Position.Ds1
        public class PositionDs1
        {
            public Vector3f vector = new Vector3f();
            public bool IsSplited = false;
            public string Mode;
        }
        #endregion
        #region Items.Ds1
        public class ItemDs1
        {
            public string Title;
            public uint Id;
            public bool IsSplited = false;
            public string Mode;
        }

        public ItemDs1 stringToEnumItem(string item)
        {
            ItemDs1 cItem = new ItemDs1();
            switch (item)
            {
                case "Catarina Helm": cItem.Title = "Catarina Helm"; cItem.Id = 10000; break;
                case "Catarina Armor": cItem.Title = "Catarina Armor"; cItem.Id = 11000; break;
                case "Catarina Gauntlets": cItem.Title = "Catarina Gauntlets"; cItem.Id = 12000; break;
                case "Catarina Leggings": cItem.Title = "Catarina Leggings"; cItem.Id = 13000; break;
                case "Paladin Helm": cItem.Title = "Paladin Helm"; cItem.Id = 20000; break;
                case "Paladin Armor": cItem.Title = "Paladin Armor"; cItem.Id = 21000; break;
                case "Paladin Gauntlets": cItem.Title = "Paladin Gauntlets"; cItem.Id = 22000; break;
                case "Paladin Leggings": cItem.Title = "Paladin Leggings"; cItem.Id = 23000; break;
                case "Dark Mask": cItem.Title = "Dark Mask"; cItem.Id = 40000; break;
                case "Dark Armor": cItem.Title = "Dark Armor"; cItem.Id = 41000; break;
                case "Dark Gauntlets": cItem.Title = "Dark Gauntlets"; cItem.Id = 42000; break;
                case "Dark Leggings": cItem.Title = "Dark Leggings"; cItem.Id = 43000; break;
                case "Brigand Hood": cItem.Title = "Brigand Hood"; cItem.Id = 50000; break;
                case "Brigand Armor": cItem.Title = "Brigand Armor"; cItem.Id = 51000; break;
                case "Brigand Gauntlets": cItem.Title = "Brigand Gauntlets"; cItem.Id = 52000; break;
                case "Brigand Trousers": cItem.Title = "Brigand Trousers"; cItem.Id = 53000; break;
                case "Shadow Mask": cItem.Title = "Shadow Mask"; cItem.Id = 60000; break;
                case "Shadow Garb": cItem.Title = "Shadow Garb"; cItem.Id = 61000; break;
                case "Shadow Gauntlets": cItem.Title = "Shadow Gauntlets"; cItem.Id = 62000; break;
                case "Shadow Leggings": cItem.Title = "Shadow Leggings"; cItem.Id = 63000; break;
                case "Black Iron Helm": cItem.Title = "Black Iron Helm"; cItem.Id = 70000; break;
                case "Black Iron Armor": cItem.Title = "Black Iron Armor"; cItem.Id = 71000; break;
                case "Black Iron Gauntlets": cItem.Title = "Black Iron Gauntlets"; cItem.Id = 72000; break;
                case "Black Iron Leggings": cItem.Title = "Black Iron Leggings"; cItem.Id = 73000; break;
                case "Smough's Helm": cItem.Title = "Smough's Helm"; cItem.Id = 80000; break;
                case "Smough's Armor": cItem.Title = "Smough's Armor"; cItem.Id = 81000; break;
                case "Smough's Gauntlets": cItem.Title = "Smough's Gauntlets"; cItem.Id = 82000; break;
                case "Smough's Leggings": cItem.Title = "Smough's Leggings"; cItem.Id = 83000; break;
                case "Six-Eyed Helm of the Channelers": cItem.Title = "Six-Eyed Helm of the Channelers"; cItem.Id = 90000; break;
                case "Robe of the Channelers": cItem.Title = "Robe of the Channelers"; cItem.Id = 91000; break;
                case "Gauntlets of the Channelers": cItem.Title = "Gauntlets of the Channelers"; cItem.Id = 92000; break;
                case "Waistcloth of the Channelers": cItem.Title = "Waistcloth of the Channelers"; cItem.Id = 93000; break;
                case "Helm of Favor": cItem.Title = "Helm of Favor"; cItem.Id = 100000; break;
                case "Embraced Armor of Favor": cItem.Title = "Embraced Armor of Favor"; cItem.Id = 101000; break;
                case "Gauntlets of Favor": cItem.Title = "Gauntlets of Favor"; cItem.Id = 102000; break;
                case "Leggings of Favor": cItem.Title = "Leggings of Favor"; cItem.Id = 103000; break;
                case "Helm of the Wise": cItem.Title = "Helm of the Wise"; cItem.Id = 110000; break;
                case "Armor of the Glorious": cItem.Title = "Armor of the Glorious"; cItem.Id = 111000; break;
                case "Gauntlets of the Vanquisher": cItem.Title = "Gauntlets of the Vanquisher"; cItem.Id = 112000; break;
                case "Boots of the Explorer": cItem.Title = "Boots of the Explorer"; cItem.Id = 113000; break;
                case "Stone Helm": cItem.Title = "Stone Helm"; cItem.Id = 120000; break;
                case "Stone Armor": cItem.Title = "Stone Armor"; cItem.Id = 121000; break;
                case "Stone Gauntlets": cItem.Title = "Stone Gauntlets"; cItem.Id = 122000; break;
                case "Stone Leggings": cItem.Title = "Stone Leggings"; cItem.Id = 123000; break;
                case "Crystalline Helm": cItem.Title = "Crystalline Helm"; cItem.Id = 130000; break;
                case "Crystalline Armor": cItem.Title = "Crystalline Armor"; cItem.Id = 131000; break;
                case "Crystalline Gauntlets": cItem.Title = "Crystalline Gauntlets"; cItem.Id = 132000; break;
                case "Crystalline Leggings": cItem.Title = "Crystalline Leggings"; cItem.Id = 133000; break;
                case "Mask of the Sealer": cItem.Title = "Mask of the Sealer"; cItem.Id = 140000; break;
                case "Crimson Robe": cItem.Title = "Crimson Robe"; cItem.Id = 141000; break;
                case "Crimson Gloves": cItem.Title = "Crimson Gloves"; cItem.Id = 142000; break;
                case "Crimson Waistcloth": cItem.Title = "Crimson Waistcloth"; cItem.Id = 143000; break;
                case "Mask of Velka": cItem.Title = "Mask of Velka"; cItem.Id = 150000; break;
                case "Black Cleric Robe": cItem.Title = "Black Cleric Robe"; cItem.Id = 151000; break;
                case "Black Manchette": cItem.Title = "Black Manchette"; cItem.Id = 152000; break;
                case "Black Tights": cItem.Title = "Black Tights"; cItem.Id = 153000; break;
                case "Iron Helm": cItem.Title = "Iron Helm"; cItem.Id = 160000; break;
                case "Armor of the Sun": cItem.Title = "Armor of the Sun"; cItem.Id = 161000; break;
                case "Iron Bracelet": cItem.Title = "Iron Bracelet"; cItem.Id = 162000; break;
                case "Iron Leggings": cItem.Title = "Iron Leggings"; cItem.Id = 163000; break;
                case "Chain Helm": cItem.Title = "Chain Helm"; cItem.Id = 170000; break;
                case "Chain Armor": cItem.Title = "Chain Armor"; cItem.Id = 171000; break;
                case "Leather Gauntlets": cItem.Title = "Leather Gauntlets"; cItem.Id = 172000; break;
                case "Chain Leggings": cItem.Title = "Chain Leggings"; cItem.Id = 173000; break;
                case "Cleric Helm": cItem.Title = "Cleric Helm"; cItem.Id = 180000; break;
                case "Cleric Armor": cItem.Title = "Cleric Armor"; cItem.Id = 181000; break;
                case "Cleric Gauntlets": cItem.Title = "Cleric Gauntlets"; cItem.Id = 182000; break;
                case "Cleric Leggings": cItem.Title = "Cleric Leggings"; cItem.Id = 183000; break;
                case "Sunlight Maggot": cItem.Title = "Sunlight Maggot"; cItem.Id = 190000; break;
                case "Helm of Thorns": cItem.Title = "Helm of Thorns"; cItem.Id = 200000; break;
                case "Armor of Thorns": cItem.Title = "Armor of Thorns"; cItem.Id = 201000; break;
                case "Gauntlets of Thorns": cItem.Title = "Gauntlets of Thorns"; cItem.Id = 202000; break;
                case "Leggings of Thorns": cItem.Title = "Leggings of Thorns"; cItem.Id = 203000; break;
                case "Standard Helm": cItem.Title = "Standard Helm"; cItem.Id = 210000; break;
                case "Hard Leather Armor": cItem.Title = "Hard Leather Armor"; cItem.Id = 211000; break;
                case "Hard Leather Gauntlets": cItem.Title = "Hard Leather Gauntlets"; cItem.Id = 212000; break;
                case "Hard Leather Boots": cItem.Title = "Hard Leather Boots"; cItem.Id = 213000; break;
                case "Sorcerer Hat": cItem.Title = "Sorcerer Hat"; cItem.Id = 220000; break;
                case "Sorcerer Cloak": cItem.Title = "Sorcerer Cloak"; cItem.Id = 221000; break;
                case "Sorcerer Gauntlets": cItem.Title = "Sorcerer Gauntlets"; cItem.Id = 222000; break;
                case "Sorcerer Boots": cItem.Title = "Sorcerer Boots"; cItem.Id = 223000; break;
                case "Tattered Cloth Hood": cItem.Title = "Tattered Cloth Hood"; cItem.Id = 230000; break;
                case "Tattered Cloth Robe": cItem.Title = "Tattered Cloth Robe"; cItem.Id = 231000; break;
                case "Tattered Cloth Manchette": cItem.Title = "Tattered Cloth Manchette"; cItem.Id = 232000; break;
                case "Heavy Boots": cItem.Title = "Heavy Boots"; cItem.Id = 233000; break;
                case "Pharis's Hat": cItem.Title = "Pharis's Hat"; cItem.Id = 240000; break;
                case "Leather Armor": cItem.Title = "Leather Armor"; cItem.Id = 241000; break;
                case "Leather Gloves": cItem.Title = "Leather Gloves"; cItem.Id = 242000; break;
                case "Leather Boots": cItem.Title = "Leather Boots"; cItem.Id = 243000; break;
                case "Painting Guardian Hood": cItem.Title = "Painting Guardian Hood"; cItem.Id = 250000; break;
                case "Painting Guardian Robe": cItem.Title = "Painting Guardian Robe"; cItem.Id = 251000; break;
                case "Painting Guardian Gloves": cItem.Title = "Painting Guardian Gloves"; cItem.Id = 252000; break;
                case "Painting Guardian Waistcloth": cItem.Title = "Painting Guardian Waistcloth"; cItem.Id = 253000; break;
                case "Ornstein's Helm": cItem.Title = "Ornstein's Helm"; cItem.Id = 270000; break;
                case "Ornstein's Armor": cItem.Title = "Ornstein's Armor"; cItem.Id = 271000; break;
                case "Ornstein's Gauntlets": cItem.Title = "Ornstein's Gauntlets"; cItem.Id = 272000; break;
                case "Ornstein's Leggings": cItem.Title = "Ornstein's Leggings"; cItem.Id = 273000; break;
                case "Eastern Helm": cItem.Title = "Eastern Helm"; cItem.Id = 280000; break;
                case "Eastern Armor": cItem.Title = "Eastern Armor"; cItem.Id = 281000; break;
                case "Eastern Gauntlets": cItem.Title = "Eastern Gauntlets"; cItem.Id = 282000; break;
                case "Eastern Leggings": cItem.Title = "Eastern Leggings"; cItem.Id = 283000; break;
                case "Xanthous Crown": cItem.Title = "Xanthous Crown"; cItem.Id = 290000; break;
                case "Xanthous Overcoat": cItem.Title = "Xanthous Overcoat"; cItem.Id = 291000; break;
                case "Xanthous Gloves": cItem.Title = "Xanthous Gloves"; cItem.Id = 292000; break;
                case "Xanthous Waistcloth": cItem.Title = "Xanthous Waistcloth"; cItem.Id = 293000; break;
                case "Thief Mask": cItem.Title = "Thief Mask"; cItem.Id = 300000; break;
                case "Black Leather Armor": cItem.Title = "Black Leather Armor"; cItem.Id = 301000; break;
                case "Black Leather Gloves": cItem.Title = "Black Leather Gloves"; cItem.Id = 302000; break;
                case "Black Leather Boots": cItem.Title = "Black Leather Boots"; cItem.Id = 303000; break;
                case "Priest's Hat": cItem.Title = "Priest's Hat"; cItem.Id = 310000; break;
                case "Holy Robe": cItem.Title = "Holy Robe"; cItem.Id = 311000; break;
                case "Traveling Gloves (Holy)": cItem.Title = "Traveling Gloves (Holy)"; cItem.Id = 312000; break;
                case "Holy Trousers": cItem.Title = "Holy Trousers"; cItem.Id = 313000; break;
                case "Black Knight Helm": cItem.Title = "Black Knight Helm"; cItem.Id = 320000; break;
                case "Black Knight Armor": cItem.Title = "Black Knight Armor"; cItem.Id = 321000; break;
                case "Black Knight Gauntlets": cItem.Title = "Black Knight Gauntlets"; cItem.Id = 322000; break;
                case "Black Knight Leggings": cItem.Title = "Black Knight Leggings"; cItem.Id = 323000; break;
                case "Crown of Dusk": cItem.Title = "Crown of Dusk"; cItem.Id = 330000; break;
                case "Antiquated Dress": cItem.Title = "Antiquated Dress"; cItem.Id = 331000; break;
                case "Antiquated Gloves": cItem.Title = "Antiquated Gloves"; cItem.Id = 332000; break;
                case "Antiquated Skirt": cItem.Title = "Antiquated Skirt"; cItem.Id = 333000; break;
                case "Witch Hat": cItem.Title = "Witch Hat"; cItem.Id = 340000; break;
                case "Witch Cloak": cItem.Title = "Witch Cloak"; cItem.Id = 341000; break;
                case "Witch Gloves": cItem.Title = "Witch Gloves"; cItem.Id = 342000; break;
                case "Witch Skirt": cItem.Title = "Witch Skirt"; cItem.Id = 343000; break;
                case "Elite Knight Helm": cItem.Title = "Elite Knight Helm"; cItem.Id = 350000; break;
                case "Elite Knight Armor": cItem.Title = "Elite Knight Armor"; cItem.Id = 351000; break;
                case "Elite Knight Gauntlets": cItem.Title = "Elite Knight Gauntlets"; cItem.Id = 352000; break;
                case "Elite Knight Leggings": cItem.Title = "Elite Knight Leggings"; cItem.Id = 353000; break;
                case "Wanderer Hood": cItem.Title = "Wanderer Hood"; cItem.Id = 360000; break;
                case "Wanderer Coat": cItem.Title = "Wanderer Coat"; cItem.Id = 361000; break;
                case "Wanderer Manchette": cItem.Title = "Wanderer Manchette"; cItem.Id = 362000; break;
                case "Wanderer Boots": cItem.Title = "Wanderer Boots"; cItem.Id = 363000; break;
                case "Big Hat": cItem.Title = "Big Hat"; cItem.Id = 380000; break;
                case "Sage Robe": cItem.Title = "Sage Robe"; cItem.Id = 381000; break;
                case "Traveling Gloves (Sage)": cItem.Title = "Traveling Gloves (Sage)"; cItem.Id = 382000; break;
                case "Traveling Boots": cItem.Title = "Traveling Boots"; cItem.Id = 383000; break;
                case "Knight Helm": cItem.Title = "Knight Helm"; cItem.Id = 390000; break;
                case "Knight Armor": cItem.Title = "Knight Armor"; cItem.Id = 391000; break;
                case "Knight Gauntlets": cItem.Title = "Knight Gauntlets"; cItem.Id = 392000; break;
                case "Knight Leggings": cItem.Title = "Knight Leggings"; cItem.Id = 393000; break;
                case "Dingy Hood": cItem.Title = "Dingy Hood"; cItem.Id = 400000; break;
                case "Dingy Robe": cItem.Title = "Dingy Robe"; cItem.Id = 401000; break;
                case "Dingy Gloves": cItem.Title = "Dingy Gloves"; cItem.Id = 402000; break;
                case "Blood-Stained Skirt": cItem.Title = "Blood-Stained Skirt"; cItem.Id = 403000; break;
                case "Maiden Hood": cItem.Title = "Maiden Hood"; cItem.Id = 410000; break;
                case "Maiden Robe": cItem.Title = "Maiden Robe"; cItem.Id = 411000; break;
                case "Maiden Gloves": cItem.Title = "Maiden Gloves"; cItem.Id = 412000; break;
                case "Maiden Skirt": cItem.Title = "Maiden Skirt"; cItem.Id = 413000; break;
                case "Silver Knight Helm": cItem.Title = "Silver Knight Helm"; cItem.Id = 420000; break;
                case "Silver Knight Armor": cItem.Title = "Silver Knight Armor"; cItem.Id = 421000; break;
                case "Silver Knight Gauntlets": cItem.Title = "Silver Knight Gauntlets"; cItem.Id = 422000; break;
                case "Silver Knight Leggings": cItem.Title = "Silver Knight Leggings"; cItem.Id = 423000; break;
                case "Havel's Helm": cItem.Title = "Havel's Helm"; cItem.Id = 440000; break;
                case "Havel's Armor": cItem.Title = "Havel's Armor"; cItem.Id = 441000; break;
                case "Havel's Gauntlets": cItem.Title = "Havel's Gauntlets"; cItem.Id = 442000; break;
                case "Havel's Leggings": cItem.Title = "Havel's Leggings"; cItem.Id = 443000; break;
                case "Brass Helm": cItem.Title = "Brass Helm"; cItem.Id = 450000; break;
                case "Brass Armor": cItem.Title = "Brass Armor"; cItem.Id = 451000; break;
                case "Brass Gauntlets": cItem.Title = "Brass Gauntlets"; cItem.Id = 452000; break;
                case "Brass Leggings": cItem.Title = "Brass Leggings"; cItem.Id = 453000; break;
                case "Gold-Hemmed Black Hood": cItem.Title = "Gold-Hemmed Black Hood"; cItem.Id = 460000; break;
                case "Gold-Hemmed Black Cloak": cItem.Title = "Gold-Hemmed Black Cloak"; cItem.Id = 461000; break;
                case "Gold-Hemmed Black Gloves": cItem.Title = "Gold-Hemmed Black Gloves"; cItem.Id = 462000; break;
                case "Gold-Hemmed Black Skirt": cItem.Title = "Gold-Hemmed Black Skirt"; cItem.Id = 463000; break;
                case "Golem Helm": cItem.Title = "Golem Helm"; cItem.Id = 470000; break;
                case "Golem Armor": cItem.Title = "Golem Armor"; cItem.Id = 471000; break;
                case "Golem Gauntlets": cItem.Title = "Golem Gauntlets"; cItem.Id = 472000; break;
                case "Golem Leggings": cItem.Title = "Golem Leggings"; cItem.Id = 473000; break;
                case "Hollow Soldier Helm": cItem.Title = "Hollow Soldier Helm"; cItem.Id = 480000; break;
                case "Hollow Soldier Armor": cItem.Title = "Hollow Soldier Armor"; cItem.Id = 481000; break;
                case "Hollow Soldier Waistcloth": cItem.Title = "Hollow Soldier Waistcloth"; cItem.Id = 483000; break;
                case "Steel Helm": cItem.Title = "Steel Helm"; cItem.Id = 490000; break;
                case "Steel Armor": cItem.Title = "Steel Armor"; cItem.Id = 491000; break;
                case "Steel Gauntlets": cItem.Title = "Steel Gauntlets"; cItem.Id = 492000; break;
                case "Steel Leggings": cItem.Title = "Steel Leggings"; cItem.Id = 493000; break;
                case "Hollow Thief's Hood": cItem.Title = "Hollow Thief's Hood"; cItem.Id = 500000; break;
                case "Hollow Thief's Leather Armor": cItem.Title = "Hollow Thief's Leather Armor"; cItem.Id = 501000; break;
                case "Hollow Thief's Tights": cItem.Title = "Hollow Thief's Tights"; cItem.Id = 503000; break;
                case "Balder Helm": cItem.Title = "Balder Helm"; cItem.Id = 510000; break;
                case "Balder Armor": cItem.Title = "Balder Armor"; cItem.Id = 511000; break;
                case "Balder Gauntlets": cItem.Title = "Balder Gauntlets"; cItem.Id = 512000; break;
                case "Balder Leggings": cItem.Title = "Balder Leggings"; cItem.Id = 513000; break;
                case "Hollow Warrior Helm": cItem.Title = "Hollow Warrior Helm"; cItem.Id = 520000; break;
                case "Hollow Warrior Armor": cItem.Title = "Hollow Warrior Armor"; cItem.Id = 521000; break;
                case "Hollow Warrior Waistcloth": cItem.Title = "Hollow Warrior Waistcloth"; cItem.Id = 523000; break;
                case "Giant Helm": cItem.Title = "Giant Helm"; cItem.Id = 530000; break;
                case "Giant Armor": cItem.Title = "Giant Armor"; cItem.Id = 531000; break;
                case "Giant Gauntlets": cItem.Title = "Giant Gauntlets"; cItem.Id = 532000; break;
                case "Giant Leggings": cItem.Title = "Giant Leggings"; cItem.Id = 533000; break;
                case "Crown of the Dark Sun": cItem.Title = "Crown of the Dark Sun"; cItem.Id = 540000; break;
                case "Moonlight Robe": cItem.Title = "Moonlight Robe"; cItem.Id = 541000; break;
                case "Moonlight Gloves": cItem.Title = "Moonlight Gloves"; cItem.Id = 542000; break;
                case "Moonlight Waistcloth": cItem.Title = "Moonlight Waistcloth"; cItem.Id = 543000; break;
                case "Crown of the Great Lord": cItem.Title = "Crown of the Great Lord"; cItem.Id = 550000; break;
                case "Robe of the Great Lord": cItem.Title = "Robe of the Great Lord"; cItem.Id = 551000; break;
                case "Bracelet of the Great Lord": cItem.Title = "Bracelet of the Great Lord"; cItem.Id = 552000; break;
                case "Anklet of the Great Lord": cItem.Title = "Anklet of the Great Lord"; cItem.Id = 553000; break;
                case "Sack": cItem.Title = "Sack"; cItem.Id = 560000; break;
                case "Symbol of Avarice": cItem.Title = "Symbol of Avarice"; cItem.Id = 570000; break;
                case "Royal Helm": cItem.Title = "Royal Helm"; cItem.Id = 580000; break;
                case "Mask of the Father": cItem.Title = "Mask of the Father"; cItem.Id = 590000; break;
                case "Mask of the Mother": cItem.Title = "Mask of the Mother"; cItem.Id = 600000; break;
                case "Mask of the Child": cItem.Title = "Mask of the Child"; cItem.Id = 610000; break;
                case "Fang Boar Helm": cItem.Title = "Fang Boar Helm"; cItem.Id = 620000; break;
                case "Gargoyle Helm": cItem.Title = "Gargoyle Helm"; cItem.Id = 630000; break;
                case "Black Sorcerer Hat": cItem.Title = "Black Sorcerer Hat"; cItem.Id = 640000; break;
                case "Black Sorcerer Cloak": cItem.Title = "Black Sorcerer Cloak"; cItem.Id = 641000; break;
                case "Black Sorcerer Gauntlets": cItem.Title = "Black Sorcerer Gauntlets"; cItem.Id = 642000; break;
                case "Black Sorcerer Boots": cItem.Title = "Black Sorcerer Boots"; cItem.Id = 643000; break;
                case "Helm of Artorias": cItem.Title = "Helm of Artorias"; cItem.Id = 660000; break;
                case "Armor of Artorias": cItem.Title = "Armor of Artorias"; cItem.Id = 661000; break;
                case "Gauntlets of Artorias": cItem.Title = "Gauntlets of Artorias"; cItem.Id = 662000; break;
                case "Leggings of Artorias": cItem.Title = "Leggings of Artorias"; cItem.Id = 663000; break;
                case "Porcelain Mask": cItem.Title = "Porcelain Mask"; cItem.Id = 670000; break;
                case "Lord's Blade Robe": cItem.Title = "Lord's Blade Robe"; cItem.Id = 671000; break;
                case "Lord's Blade Gloves": cItem.Title = "Lord's Blade Gloves"; cItem.Id = 672000; break;
                case "Lord's Blade Waistcloth": cItem.Title = "Lord's Blade Waistcloth"; cItem.Id = 673000; break;
                case "Gough's Helm": cItem.Title = "Gough's Helm"; cItem.Id = 680000; break;
                case "Gough's Armor": cItem.Title = "Gough's Armor"; cItem.Id = 681000; break;
                case "Gough's Gauntlets": cItem.Title = "Gough's Gauntlets"; cItem.Id = 682000; break;
                case "Gough's Leggings": cItem.Title = "Gough's Leggings"; cItem.Id = 683000; break;
                case "Guardian Helm": cItem.Title = "Guardian Helm"; cItem.Id = 690000; break;
                case "Guardian Armor": cItem.Title = "Guardian Armor"; cItem.Id = 691000; break;
                case "Guardian Gauntlets": cItem.Title = "Guardian Gauntlets"; cItem.Id = 692000; break;
                case "Guardian Leggings": cItem.Title = "Guardian Leggings"; cItem.Id = 693000; break;
                case "Snickering Top Hat": cItem.Title = "Snickering Top Hat"; cItem.Id = 700000; break;
                case "Chester's Long Coat": cItem.Title = "Chester's Long Coat"; cItem.Id = 701000; break;
                case "Chester's Gloves": cItem.Title = "Chester's Gloves"; cItem.Id = 702000; break;
                case "Chester's Trousers": cItem.Title = "Chester's Trousers"; cItem.Id = 703000; break;
                case "Bloated Head": cItem.Title = "Bloated Head"; cItem.Id = 710000; break;
                case "Bloated Sorcerer Head": cItem.Title = "Bloated Sorcerer Head"; cItem.Id = 720000; break;
                case "Eye of Death": cItem.Title = "Eye of Death"; cItem.Id = 109; break;
                case "Cracked Red Eye Orb": cItem.Title = "Cracked Red Eye Orb"; cItem.Id = 111; break;
                case "Estus Flask": cItem.Title = "Estus Flask"; cItem.Id = 200; break;
                case "Elizabeth's Mushroom": cItem.Title = "Elizabeth's Mushroom"; cItem.Id = 230; break;
                case "Divine Blessing": cItem.Title = "Divine Blessing"; cItem.Id = 240; break;
                case "Green Blossom": cItem.Title = "Green Blossom"; cItem.Id = 260; break;
                case "Bloodred Moss Clump": cItem.Title = "Bloodred Moss Clump"; cItem.Id = 270; break;
                case "Purple Moss Clump": cItem.Title = "Purple Moss Clump"; cItem.Id = 271; break;
                case "Blooming Purple Moss Clump": cItem.Title = "Blooming Purple Moss Clump"; cItem.Id = 272; break;
                case "Purging Stone": cItem.Title = "Purging Stone"; cItem.Id = 274; break;
                case "Egg Vermifuge": cItem.Title = "Egg Vermifuge"; cItem.Id = 275; break;
                case "Repair Powder": cItem.Title = "Repair Powder"; cItem.Id = 280; break;
                case "Throwing Knife": cItem.Title = "Throwing Knife"; cItem.Id = 290; break;
                case "Poison Throwing Knife": cItem.Title = "Poison Throwing Knife"; cItem.Id = 291; break;
                case "Firebomb": cItem.Title = "Firebomb"; cItem.Id = 292; break;
                case "Dung Pie": cItem.Title = "Dung Pie"; cItem.Id = 293; break;
                case "Alluring Skull": cItem.Title = "Alluring Skull"; cItem.Id = 294; break;
                case "Lloyd's Talisman": cItem.Title = "Lloyd's Talisman"; cItem.Id = 296; break;
                case "Black Firebomb": cItem.Title = "Black Firebomb"; cItem.Id = 297; break;
                case "Charcoal Pine Resin": cItem.Title = "Charcoal Pine Resin"; cItem.Id = 310; break;
                case "Gold Pine Resin": cItem.Title = "Gold Pine Resin"; cItem.Id = 311; break;
                case "Transient Curse": cItem.Title = "Transient Curse"; cItem.Id = 312; break;
                case "Rotten Pine Resin": cItem.Title = "Rotten Pine Resin"; cItem.Id = 313; break;
                case "Homeward Bone": cItem.Title = "Homeward Bone"; cItem.Id = 330; break;
                case "Prism Stone": cItem.Title = "Prism Stone"; cItem.Id = 370; break;
                case "Indictment": cItem.Title = "Indictment"; cItem.Id = 373; break;
                case "Souvenir of Reprisal": cItem.Title = "Souvenir of Reprisal"; cItem.Id = 374; break;
                case "Sunlight Medal": cItem.Title = "Sunlight Medal"; cItem.Id = 375; break;
                case "Pendant": cItem.Title = "Pendant"; cItem.Id = 376; break;
                case "Rubbish": cItem.Title = "Rubbish"; cItem.Id = 380; break;
                case "Copper Coin": cItem.Title = "Copper Coin"; cItem.Id = 381; break;
                case "Silver Coin": cItem.Title = "Silver Coin"; cItem.Id = 382; break;
                case "Gold Coin": cItem.Title = "Gold Coin"; cItem.Id = 383; break;
                case "Fire Keeper Soul (Anastacia of Astora)": cItem.Title = "Fire Keeper Soul (Anastacia of Astora)"; cItem.Id = 390; break;
                case "Fire Keeper Soul (Darkmoon Knightess)": cItem.Title = "Fire Keeper Soul (Darkmoon Knightess)"; cItem.Id = 391; break;
                case "Fire Keeper Soul (Daughter of Chaos)": cItem.Title = "Fire Keeper Soul (Daughter of Chaos)"; cItem.Id = 392; break;
                case "Fire Keeper Soul (New Londo)": cItem.Title = "Fire Keeper Soul (New Londo)"; cItem.Id = 393; break;
                case "Fire Keeper Soul (Blighttown)": cItem.Title = "Fire Keeper Soul (Blighttown)"; cItem.Id = 394; break;
                case "Fire Keeper Soul (Duke's Archives)": cItem.Title = "Fire Keeper Soul (Duke's Archives)"; cItem.Id = 395; break;
                case "Fire Keeper Soul (Undead Parish)": cItem.Title = "Fire Keeper Soul (Undead Parish)"; cItem.Id = 396; break;
                case "Soul of a Lost Undead": cItem.Title = "Soul of a Lost Undead"; cItem.Id = 400; break;
                case "Large Soul of a Lost Undead": cItem.Title = "Large Soul of a Lost Undead"; cItem.Id = 401; break;
                case "Soul of a Nameless Soldier": cItem.Title = "Soul of a Nameless Soldier"; cItem.Id = 402; break;
                case "Large Soul of a Nameless Soldier": cItem.Title = "Large Soul of a Nameless Soldier"; cItem.Id = 403; break;
                case "Soul of a Proud Knight": cItem.Title = "Soul of a Proud Knight"; cItem.Id = 404; break;
                case "Large Soul of a Proud Knight": cItem.Title = "Large Soul of a Proud Knight"; cItem.Id = 405; break;
                case "Soul of a Brave Warrior": cItem.Title = "Soul of a Brave Warrior"; cItem.Id = 406; break;
                case "Large Soul of a Brave Warrior": cItem.Title = "Large Soul of a Brave Warrior"; cItem.Id = 407; break;
                case "Soul of a Hero": cItem.Title = "Soul of a Hero"; cItem.Id = 408; break;
                case "Soul of a Great Hero": cItem.Title = "Soul of a Great Hero"; cItem.Id = 409; break;
                case "Humanity": cItem.Title = "Humanity"; cItem.Id = 500; break;
                case "Twin Humanities": cItem.Title = "Twin Humanities"; cItem.Id = 501; break;
                case "Soul of Quelaag": cItem.Title = "Soul of Quelaag"; cItem.Id = 700; break;
                case "Soul of Sif": cItem.Title = "Soul of Sif"; cItem.Id = 701; break;
                case "Soul of Gwyn, Lord of Cinder": cItem.Title = "Soul of Gwyn, Lord of Cinder"; cItem.Id = 702; break;
                case "Core of an Iron Golem": cItem.Title = "Core of an Iron Golem"; cItem.Id = 703; break;
                case "Soul of Ornstein": cItem.Title = "Soul of Ornstein"; cItem.Id = 704; break;
                case "Soul of the Moonlight Butterfly": cItem.Title = "Soul of the Moonlight Butterfly"; cItem.Id = 705; break;
                case "Soul of Smough": cItem.Title = "Soul of Smough"; cItem.Id = 706; break;
                case "Soul of Priscilla": cItem.Title = "Soul of Priscilla"; cItem.Id = 707; break;
                case "Soul of Gwyndolin": cItem.Title = "Soul of Gwyndolin"; cItem.Id = 708; break;
                case "Guardian Soul": cItem.Title = "Guardian Soul"; cItem.Id = 709; break;
                case "Soul of Artorias": cItem.Title = "Soul of Artorias"; cItem.Id = 710; break;
                case "Soul of Manus": cItem.Title = "Soul of Manus"; cItem.Id = 711; break;
                case "Peculiar Doll": cItem.Title = "Peculiar Doll"; cItem.Id = 384; break;
                case "Basement Key": cItem.Title = "Basement Key"; cItem.Id = 2001; break;
                case "Crest of Artorias": cItem.Title = "Crest of Artorias"; cItem.Id = 2002; break;
                case "Cage Key": cItem.Title = "Cage Key"; cItem.Id = 2003; break;
                case "Archive Tower Cell Key": cItem.Title = "Archive Tower Cell Key"; cItem.Id = 2004; break;
                case "Archive Tower Giant Door Key": cItem.Title = "Archive Tower Giant Door Key"; cItem.Id = 2005; break;
                case "Archive Tower Giant Cell Key": cItem.Title = "Archive Tower Giant Cell Key"; cItem.Id = 2006; break;
                case "Blighttown Key": cItem.Title = "Blighttown Key"; cItem.Id = 2007; break;
                case "Key to New Londo Ruins": cItem.Title = "Key to New Londo Ruins"; cItem.Id = 2008; break;
                case "Annex Key": cItem.Title = "Annex Key"; cItem.Id = 2009; break;
                case "Dungeon Cell Key": cItem.Title = "Dungeon Cell Key"; cItem.Id = 2010; break;
                case "Big Pilgrim's Key": cItem.Title = "Big Pilgrim's Key"; cItem.Id = 2011; break;
                case "Undead Asylum F2 East Key": cItem.Title = "Undead Asylum F2 East Key"; cItem.Id = 2012; break;
                case "Key to the Seal": cItem.Title = "Key to the Seal"; cItem.Id = 2013; break;
                case "Key to Depths": cItem.Title = "Key to Depths"; cItem.Id = 2014; break;
                case "Undead Asylum F2 West Key": cItem.Title = "Undead Asylum F2 West Key"; cItem.Id = 2016; break;
                case "Mystery Key": cItem.Title = "Mystery Key"; cItem.Id = 2017; break;
                case "Sewer Chamber Key": cItem.Title = "Sewer Chamber Key"; cItem.Id = 2018; break;
                case "Watchtower Basement Key": cItem.Title = "Watchtower Basement Key"; cItem.Id = 2019; break;
                case "Archive Prison Extra Key": cItem.Title = "Archive Prison Extra Key"; cItem.Id = 2020; break;
                case "Residence Key": cItem.Title = "Residence Key"; cItem.Id = 2021; break;
                case "Crest Key": cItem.Title = "Crest Key"; cItem.Id = 2022; break;
                case "Master Key": cItem.Title = "Master Key"; cItem.Id = 2100; break;
                case "Lord Soul (Nito)": cItem.Title = "Lord Soul (Nito)"; cItem.Id = 2500; break;
                case "Lord Soul (Bed of Chaos)": cItem.Title = "Lord Soul (Bed of Chaos)"; cItem.Id = 2501; break;
                case "Bequeathed Lord Soul Shard (Four Kings)": cItem.Title = "Bequeathed Lord Soul Shard (Four Kings)"; cItem.Id = 2502; break;
                case "Bequeathed Lord Soul Shard (Seath)": cItem.Title = "Bequeathed Lord Soul Shard (Seath)"; cItem.Id = 2503; break;
                case "Lordvessel": cItem.Title = "Lordvessel"; cItem.Id = 2510; break;
                case "Broken Pendant": cItem.Title = "Broken Pendant"; cItem.Id = 2520; break;
                case "Weapon Smithbox": cItem.Title = "Weapon Smithbox"; cItem.Id = 2600; break;
                case "Armor Smithbox": cItem.Title = "Armor Smithbox"; cItem.Id = 2601; break;
                case "Repairbox": cItem.Title = "Repairbox"; cItem.Id = 2602; break;
                case "Rite of Kindling": cItem.Title = "Rite of Kindling"; cItem.Id = 2607; break;
                case "Bottomless Box": cItem.Title = "Bottomless Box"; cItem.Id = 2608; break;
                case "Dagger": cItem.Title = "Dagger"; cItem.Id = 100000; break;
                case "Parrying Dagger": cItem.Title = "Parrying Dagger"; cItem.Id = 101000; break;
                case "Ghost Blade": cItem.Title = "Ghost Blade"; cItem.Id = 102000; break;
                case "Bandit's Knife": cItem.Title = "Bandit's Knife"; cItem.Id = 103000; break;
                case "Priscilla's Dagger": cItem.Title = "Priscilla's Dagger"; cItem.Id = 104000; break;
                case "Shortsword": cItem.Title = "Shortsword"; cItem.Id = 200000; break;
                case "Longsword": cItem.Title = "Longsword"; cItem.Id = 201000; break;
                case "Broadsword": cItem.Title = "Broadsword"; cItem.Id = 202000; break;
                case "Broken Straight Sword": cItem.Title = "Broken Straight Sword"; cItem.Id = 203000; break;
                case "Balder Side Sword": cItem.Title = "Balder Side Sword"; cItem.Id = 204000; break;
                case "Crystal Straight Sword": cItem.Title = "Crystal Straight Sword"; cItem.Id = 205000; break;
                case "Sunlight Straight Sword": cItem.Title = "Sunlight Straight Sword"; cItem.Id = 206000; break;
                case "Barbed Straight Sword": cItem.Title = "Barbed Straight Sword"; cItem.Id = 207000; break;
                case "Silver Knight Straight Sword": cItem.Title = "Silver Knight Straight Sword"; cItem.Id = 208000; break;
                case "Astora's Straight Sword": cItem.Title = "Astora's Straight Sword"; cItem.Id = 209000; break;
                case "Darksword": cItem.Title = "Darksword"; cItem.Id = 210000; break;
                case "Drake Sword": cItem.Title = "Drake Sword"; cItem.Id = 211000; break;
                case "Straight Sword Hilt": cItem.Title = "Straight Sword Hilt"; cItem.Id = 212000; break;
                case "Bastard Sword": cItem.Title = "Bastard Sword"; cItem.Id = 300000; break;
                case "Claymore": cItem.Title = "Claymore"; cItem.Id = 301000; break;
                case "Man-serpent Greatsword": cItem.Title = "Man-serpent Greatsword"; cItem.Id = 302000; break;
                case "Flamberge": cItem.Title = "Flamberge"; cItem.Id = 303000; break;
                case "Crystal Greatsword": cItem.Title = "Crystal Greatsword"; cItem.Id = 304000; break;
                case "Stone Greatsword": cItem.Title = "Stone Greatsword"; cItem.Id = 306000; break;
                case "Greatsword of Artorias": cItem.Title = "Greatsword of Artorias"; cItem.Id = 307000; break;
                case "Moonlight Greatsword": cItem.Title = "Moonlight Greatsword"; cItem.Id = 309000; break;
                case "Black Knight Sword": cItem.Title = "Black Knight Sword"; cItem.Id = 310000; break;
                case "Greatsword of Artorias (Cursed)": cItem.Title = "Greatsword of Artorias (Cursed)"; cItem.Id = 311000; break;
                case "Great Lord Greatsword": cItem.Title = "Great Lord Greatsword"; cItem.Id = 314000; break;
                case "Zweihander": cItem.Title = "Zweihander"; cItem.Id = 350000; break;
                case "Greatsword": cItem.Title = "Greatsword"; cItem.Id = 351000; break;
                case "Demon Great Machete": cItem.Title = "Demon Great Machete"; cItem.Id = 352000; break;
                case "Dragon Greatsword": cItem.Title = "Dragon Greatsword"; cItem.Id = 354000; break;
                case "Black Knight Greatsword": cItem.Title = "Black Knight Greatsword"; cItem.Id = 355000; break;
                case "Scimitar": cItem.Title = "Scimitar"; cItem.Id = 400000; break;
                case "Falchion": cItem.Title = "Falchion"; cItem.Id = 401000; break;
                case "Shotel": cItem.Title = "Shotel"; cItem.Id = 402000; break;
                case "Jagged Ghost Blade": cItem.Title = "Jagged Ghost Blade"; cItem.Id = 403000; break;
                case "Painting Guardian Sword": cItem.Title = "Painting Guardian Sword"; cItem.Id = 405000; break;
                case "Quelaag's Furysword": cItem.Title = "Quelaag's Furysword"; cItem.Id = 406000; break;
                case "Server": cItem.Title = "Server"; cItem.Id = 450000; break;
                case "Murakumo": cItem.Title = "Murakumo"; cItem.Id = 451000; break;
                case "Gravelord Sword": cItem.Title = "Gravelord Sword"; cItem.Id = 453000; break;
                case "Uchigatana": cItem.Title = "Uchigatana"; cItem.Id = 500000; break;
                case "Washing Pole": cItem.Title = "Washing Pole"; cItem.Id = 501000; break;
                case "Iaito": cItem.Title = "Iaito"; cItem.Id = 502000; break;
                case "Chaos Blade": cItem.Title = "Chaos Blade"; cItem.Id = 503000; break;
                case "Mail Breaker": cItem.Title = "Mail Breaker"; cItem.Id = 600000; break;
                case "Rapier": cItem.Title = "Rapier"; cItem.Id = 601000; break;
                case "Estoc": cItem.Title = "Estoc"; cItem.Id = 602000; break;
                case "Velka's Rapier": cItem.Title = "Velka's Rapier"; cItem.Id = 603000; break;
                case "Ricard's Rapier": cItem.Title = "Ricard's Rapier"; cItem.Id = 604000; break;
                case "Hand Axe": cItem.Title = "Hand Axe"; cItem.Id = 700000; break;
                case "Battle Axe": cItem.Title = "Battle Axe"; cItem.Id = 701000; break;
                case "Crescent Axe": cItem.Title = "Crescent Axe"; cItem.Id = 702000; break;
                case "Butcher Knife": cItem.Title = "Butcher Knife"; cItem.Id = 703000; break;
                case "Golem Axe": cItem.Title = "Golem Axe"; cItem.Id = 704000; break;
                case "Gargoyle Tail Axe": cItem.Title = "Gargoyle Tail Axe"; cItem.Id = 705000; break;
                case "Greataxe": cItem.Title = "Greataxe"; cItem.Id = 750000; break;
                case "Demon's Greataxe": cItem.Title = "Demon's Greataxe"; cItem.Id = 751000; break;
                case "Dragon King Greataxe": cItem.Title = "Dragon King Greataxe"; cItem.Id = 752000; break;
                case "Black Knight Greataxe": cItem.Title = "Black Knight Greataxe"; cItem.Id = 753000; break;
                case "Club": cItem.Title = "Club"; cItem.Id = 800000; break;
                case "Mace": cItem.Title = "Mace"; cItem.Id = 801000; break;
                case "Morning Star": cItem.Title = "Morning Star"; cItem.Id = 802000; break;
                case "Warpick": cItem.Title = "Warpick"; cItem.Id = 803000; break;
                case "Pickaxe": cItem.Title = "Pickaxe"; cItem.Id = 804000; break;
                case "Reinforced Club": cItem.Title = "Reinforced Club"; cItem.Id = 809000; break;
                case "Blacksmith Hammer": cItem.Title = "Blacksmith Hammer"; cItem.Id = 810000; break;
                case "Blacksmith Giant Hammer": cItem.Title = "Blacksmith Giant Hammer"; cItem.Id = 811000; break;
                case "Hammer of Vamos": cItem.Title = "Hammer of Vamos"; cItem.Id = 812000; break;
                case "Great Club": cItem.Title = "Great Club"; cItem.Id = 850000; break;
                case "Grant": cItem.Title = "Grant"; cItem.Id = 851000; break;
                case "Demon's Great Hammer": cItem.Title = "Demon's Great Hammer"; cItem.Id = 852000; break;
                case "Dragon Tooth": cItem.Title = "Dragon Tooth"; cItem.Id = 854000; break;
                case "Large Club": cItem.Title = "Large Club"; cItem.Id = 855000; break;
                case "Smough's Hammer": cItem.Title = "Smough's Hammer"; cItem.Id = 856000; break;
                case "Caestus": cItem.Title = "Caestus"; cItem.Id = 901000; break;
                case "Claw": cItem.Title = "Claw"; cItem.Id = 902000; break;
                case "Dragon Bone Fist": cItem.Title = "Dragon Bone Fist"; cItem.Id = 903000; break;
                case "Dark Hand": cItem.Title = "Dark Hand"; cItem.Id = 904000; break;
                case "Spear": cItem.Title = "Spear"; cItem.Id = 1000000; break;
                case "Winged Spear": cItem.Title = "Winged Spear"; cItem.Id = 1001000; break;
                case "Partizan": cItem.Title = "Partizan"; cItem.Id = 1002000; break;
                case "Demon's Spear": cItem.Title = "Demon's Spear"; cItem.Id = 1003000; break;
                case "Channeler's Trident": cItem.Title = "Channeler's Trident"; cItem.Id = 1004000; break;
                case "Silver Knight Spear": cItem.Title = "Silver Knight Spear"; cItem.Id = 1006000; break;
                case "Pike": cItem.Title = "Pike"; cItem.Id = 1050000; break;
                case "Dragonslayer Spear": cItem.Title = "Dragonslayer Spear"; cItem.Id = 1051000; break;
                case "Moonlight Butterfly Horn": cItem.Title = "Moonlight Butterfly Horn"; cItem.Id = 1052000; break;
                case "Halberd": cItem.Title = "Halberd"; cItem.Id = 1100000; break;
                case "Giant's Halberd": cItem.Title = "Giant's Halberd"; cItem.Id = 1101000; break;
                case "Titanite Catch Pole": cItem.Title = "Titanite Catch Pole"; cItem.Id = 1102000; break;
                case "Gargoyle's Halberd": cItem.Title = "Gargoyle's Halberd"; cItem.Id = 1103000; break;
                case "Black Knight Halberd": cItem.Title = "Black Knight Halberd"; cItem.Id = 1105000; break;
                case "Lucerne": cItem.Title = "Lucerne"; cItem.Id = 1106000; break;
                case "Scythe": cItem.Title = "Scythe"; cItem.Id = 1107000; break;
                case "Great Scythe": cItem.Title = "Great Scythe"; cItem.Id = 1150000; break;
                case "Lifehunt Scythe": cItem.Title = "Lifehunt Scythe"; cItem.Id = 1151000; break;
                case "Whip": cItem.Title = "Whip"; cItem.Id = 1600000; break;
                case "Notched Whip": cItem.Title = "Notched Whip"; cItem.Id = 1601000; break;
                case "Gold Tracer": cItem.Title = "Gold Tracer"; cItem.Id = 9010000; break;
                case "Dark Silver Tracer": cItem.Title = "Dark Silver Tracer"; cItem.Id = 9011000; break;
                case "Abyss Greatsword": cItem.Title = "Abyss Greatsword"; cItem.Id = 9012000; break;
                case "Stone Greataxe": cItem.Title = "Stone Greataxe"; cItem.Id = 9015000; break;
                case "Four-pronged Plow": cItem.Title = "Four-pronged Plow"; cItem.Id = 9016000; break;
                case "Guardian Tail": cItem.Title = "Guardian Tail"; cItem.Id = 9019000; break;
                case "Obsidian Greatsword": cItem.Title = "Obsidian Greatsword"; cItem.Id = 9020000; break;
                case "Short Bow": cItem.Title = "Short Bow"; cItem.Id = 1200000; break;
                case "Longbow": cItem.Title = "Longbow"; cItem.Id = 1201000; break;
                case "Black Bow of Pharis": cItem.Title = "Black Bow of Pharis"; cItem.Id = 1202000; break;
                case "Dragonslayer Greatbow": cItem.Title = "Dragonslayer Greatbow"; cItem.Id = 1203000; break;
                case "Composite Bow": cItem.Title = "Composite Bow"; cItem.Id = 1204000; break;
                case "Darkmoon Bow": cItem.Title = "Darkmoon Bow"; cItem.Id = 1205000; break;
                case "Light Crossbow": cItem.Title = "Light Crossbow"; cItem.Id = 1250000; break;
                case "Heavy Crossbow": cItem.Title = "Heavy Crossbow"; cItem.Id = 1251000; break;
                case "Avelyn": cItem.Title = "Avelyn"; cItem.Id = 1252000; break;
                case "Sniper Crossbow": cItem.Title = "Sniper Crossbow"; cItem.Id = 1253000; break;
                case "Gough's Greatbow": cItem.Title = "Gough's Greatbow"; cItem.Id = 9021000; break;
                case "Standard Arrow": cItem.Title = "Standard Arrow"; cItem.Id = 2000000; break;
                case "Large Arrow": cItem.Title = "Large Arrow"; cItem.Id = 2001000; break;
                case "Feather Arrow": cItem.Title = "Feather Arrow"; cItem.Id = 2002000; break;
                case "Fire Arrow": cItem.Title = "Fire Arrow"; cItem.Id = 2003000; break;
                case "Poison Arrow": cItem.Title = "Poison Arrow"; cItem.Id = 2004000; break;
                case "Moonlight Arrow": cItem.Title = "Moonlight Arrow"; cItem.Id = 2005000; break;
                case "Wooden Arrow": cItem.Title = "Wooden Arrow"; cItem.Id = 2006000; break;
                case "Dragonslayer Arrow": cItem.Title = "Dragonslayer Arrow"; cItem.Id = 2007000; break;
                case "Gough's Great Arrow": cItem.Title = "Gough's Great Arrow"; cItem.Id = 2008000; break;
                case "Standard Bolt": cItem.Title = "Standard Bolt"; cItem.Id = 2100000; break;
                case "Heavy Bolt": cItem.Title = "Heavy Bolt"; cItem.Id = 2101000; break;
                case "Sniper Bolt": cItem.Title = "Sniper Bolt"; cItem.Id = 2102000; break;
                case "Wood Bolt": cItem.Title = "Wood Bolt"; cItem.Id = 2103000; break;
                case "Lightning Bolt": cItem.Title = "Lightning Bolt"; cItem.Id = 2104000; break;
                case "Havel's Ring": cItem.Title = "Havel's Ring"; cItem.Id = 100; break;
                case "Red Tearstone Ring": cItem.Title = "Red Tearstone Ring"; cItem.Id = 101; break;
                case "Darkmoon Blade Covenant Ring": cItem.Title = "Darkmoon Blade Covenant Ring"; cItem.Id = 102; break;
                case "Cat Covenant Ring": cItem.Title = "Cat Covenant Ring"; cItem.Id = 103; break;
                case "Cloranthy Ring": cItem.Title = "Cloranthy Ring"; cItem.Id = 104; break;
                case "Flame Stoneplate Ring": cItem.Title = "Flame Stoneplate Ring"; cItem.Id = 105; break;
                case "Thunder Stoneplate Ring": cItem.Title = "Thunder Stoneplate Ring"; cItem.Id = 106; break;
                case "Spell Stoneplate Ring": cItem.Title = "Spell Stoneplate Ring"; cItem.Id = 107; break;
                case "Speckled Stoneplate Ring": cItem.Title = "Speckled Stoneplate Ring"; cItem.Id = 108; break;
                case "Bloodbite Ring": cItem.Title = "Bloodbite Ring"; cItem.Id = 109; break;
                case "Poisonbite Ring": cItem.Title = "Poisonbite Ring"; cItem.Id = 110; break;
                case "Tiny Being's Ring": cItem.Title = "Tiny Being's Ring"; cItem.Id = 111; break;
                case "Cursebite Ring": cItem.Title = "Cursebite Ring"; cItem.Id = 113; break;
                case "White Seance Ring": cItem.Title = "White Seance Ring"; cItem.Id = 114; break;
                case "Bellowing Dragoncrest Ring": cItem.Title = "Bellowing Dragoncrest Ring"; cItem.Id = 115; break;
                case "Dusk Crown Ring": cItem.Title = "Dusk Crown Ring"; cItem.Id = 116; break;
                case "Hornet Ring": cItem.Title = "Hornet Ring"; cItem.Id = 117; break;
                case "Hawk Ring": cItem.Title = "Hawk Ring"; cItem.Id = 119; break;
                case "Ring of Steel Protection": cItem.Title = "Ring of Steel Protection"; cItem.Id = 120; break;
                case "Covetous Gold Serpent Ring": cItem.Title = "Covetous Gold Serpent Ring"; cItem.Id = 121; break;
                case "Covetous Silver Serpent Ring": cItem.Title = "Covetous Silver Serpent Ring"; cItem.Id = 122; break;
                case "Slumbering Dragoncrest Ring": cItem.Title = "Slumbering Dragoncrest Ring"; cItem.Id = 123; break;
                case "Ring of Fog": cItem.Title = "Ring of Fog"; cItem.Id = 124; break;
                case "Rusted Iron Ring": cItem.Title = "Rusted Iron Ring"; cItem.Id = 125; break;
                case "Ring of Sacrifice": cItem.Title = "Ring of Sacrifice"; cItem.Id = 126; break;
                case "Rare Ring of Sacrifice": cItem.Title = "Rare Ring of Sacrifice"; cItem.Id = 127; break;
                case "Dark Wood Grain Ring": cItem.Title = "Dark Wood Grain Ring"; cItem.Id = 128; break;
                case "Ring of the Sun Princess": cItem.Title = "Ring of the Sun Princess"; cItem.Id = 130; break;
                case "Old Witch's Ring": cItem.Title = "Old Witch's Ring"; cItem.Id = 137; break;
                case "Covenant of Artorias": cItem.Title = "Covenant of Artorias"; cItem.Id = 138; break;
                case "Orange Charred Ring": cItem.Title = "Orange Charred Ring"; cItem.Id = 139; break;
                case "Lingering Dragoncrest Ring": cItem.Title = "Lingering Dragoncrest Ring"; cItem.Id = 141; break;
                case "Ring of the Evil Eye": cItem.Title = "Ring of the Evil Eye"; cItem.Id = 142; break;
                case "Ring of Favor and Protection": cItem.Title = "Ring of Favor and Protection"; cItem.Id = 143; break;
                case "Leo Ring": cItem.Title = "Leo Ring"; cItem.Id = 144; break;
                case "East Wood Grain Ring": cItem.Title = "East Wood Grain Ring"; cItem.Id = 145; break;
                case "Wolf Ring": cItem.Title = "Wolf Ring"; cItem.Id = 146; break;
                case "Blue Tearstone Ring": cItem.Title = "Blue Tearstone Ring"; cItem.Id = 147; break;
                case "Ring of the Sun's Firstborn": cItem.Title = "Ring of the Sun's Firstborn"; cItem.Id = 148; break;
                case "Darkmoon Seance Ring": cItem.Title = "Darkmoon Seance Ring"; cItem.Id = 149; break;
                case "Calamity Ring": cItem.Title = "Calamity Ring"; cItem.Id = 150; break;
                case "Skull Lantern": cItem.Title = "Skull Lantern"; cItem.Id = 1396000; break;
                case "East-West Shield": cItem.Title = "East-West Shield"; cItem.Id = 1400000; break;
                case "Wooden Shield": cItem.Title = "Wooden Shield"; cItem.Id = 1401000; break;
                case "Large Leather Shield": cItem.Title = "Large Leather Shield"; cItem.Id = 1402000; break;
                case "Small Leather Shield": cItem.Title = "Small Leather Shield"; cItem.Id = 1403000; break;
                case "Target Shield": cItem.Title = "Target Shield"; cItem.Id = 1404000; break;
                case "Buckler": cItem.Title = "Buckler"; cItem.Id = 1405000; break;
                case "Cracked Round Shield": cItem.Title = "Cracked Round Shield"; cItem.Id = 1406000; break;
                case "Leather Shield": cItem.Title = "Leather Shield"; cItem.Id = 1408000; break;
                case "Plank Shield": cItem.Title = "Plank Shield"; cItem.Id = 1409000; break;
                case "Caduceus Round Shield": cItem.Title = "Caduceus Round Shield"; cItem.Id = 1410000; break;
                case "Crystal Ring Shield": cItem.Title = "Crystal Ring Shield"; cItem.Id = 1411000; break;
                case "Heater Shield": cItem.Title = "Heater Shield"; cItem.Id = 1450000; break;
                case "Knight Shield": cItem.Title = "Knight Shield"; cItem.Id = 1451000; break;
                case "Tower Kite Shield": cItem.Title = "Tower Kite Shield"; cItem.Id = 1452000; break;
                case "Grass Crest Shield": cItem.Title = "Grass Crest Shield"; cItem.Id = 1453000; break;
                case "Hollow Soldier Shield": cItem.Title = "Hollow Soldier Shield"; cItem.Id = 1454000; break;
                case "Balder Shield": cItem.Title = "Balder Shield"; cItem.Id = 1455000; break;
                case "Crest Shield": cItem.Title = "Crest Shield"; cItem.Id = 1456000; break;
                case "Dragon Crest Shield": cItem.Title = "Dragon Crest Shield"; cItem.Id = 1457000; break;
                case "Warrior's Round Shield": cItem.Title = "Warrior's Round Shield"; cItem.Id = 1460000; break;
                case "Iron Round Shield": cItem.Title = "Iron Round Shield"; cItem.Id = 1461000; break;
                case "Spider Shield": cItem.Title = "Spider Shield"; cItem.Id = 1462000; break;
                case "Spiked Shield": cItem.Title = "Spiked Shield"; cItem.Id = 1470000; break;
                case "Crystal Shield": cItem.Title = "Crystal Shield"; cItem.Id = 1471000; break;
                case "Sunlight Shield": cItem.Title = "Sunlight Shield"; cItem.Id = 1472000; break;
                case "Silver Knight Shield": cItem.Title = "Silver Knight Shield"; cItem.Id = 1473000; break;
                case "Black Knight Shield": cItem.Title = "Black Knight Shield"; cItem.Id = 1474000; break;
                case "Pierce Shield": cItem.Title = "Pierce Shield"; cItem.Id = 1475000; break;
                case "Red and White Round Shield": cItem.Title = "Red and White Round Shield"; cItem.Id = 1476000; break;
                case "Caduceus Kite Shield": cItem.Title = "Caduceus Kite Shield"; cItem.Id = 1477000; break;
                case "Gargoyle's Shield": cItem.Title = "Gargoyle's Shield"; cItem.Id = 1478000; break;
                case "Eagle Shield": cItem.Title = "Eagle Shield"; cItem.Id = 1500000; break;
                case "Tower Shield": cItem.Title = "Tower Shield"; cItem.Id = 1501000; break;
                case "Giant Shield": cItem.Title = "Giant Shield"; cItem.Id = 1502000; break;
                case "Stone Greatshield": cItem.Title = "Stone Greatshield"; cItem.Id = 1503000; break;
                case "Havel's Greatshield": cItem.Title = "Havel's Greatshield"; cItem.Id = 1505000; break;
                case "Bonewheel Shield": cItem.Title = "Bonewheel Shield"; cItem.Id = 1506000; break;
                case "Greatshield of Artorias": cItem.Title = "Greatshield of Artorias"; cItem.Id = 1507000; break;
                case "Effigy Shield": cItem.Title = "Effigy Shield"; cItem.Id = 9000000; break;
                case "Sanctus": cItem.Title = "Sanctus"; cItem.Id = 9001000; break;
                case "Bloodshield": cItem.Title = "Bloodshield"; cItem.Id = 9002000; break;
                case "Black Iron Greatshield": cItem.Title = "Black Iron Greatshield"; cItem.Id = 9003000; break;
                case "Cleansing Greatshield": cItem.Title = "Cleansing Greatshield"; cItem.Id = 9014000; break;
                case "Sorcery: Soul Arrow": cItem.Title = "Sorcery: Soul Arrow"; cItem.Id = 3000; break;
                case "Sorcery: Great Soul Arrow": cItem.Title = "Sorcery: Great Soul Arrow"; cItem.Id = 3010; break;
                case "Sorcery: Heavy Soul Arrow": cItem.Title = "Sorcery: Heavy Soul Arrow"; cItem.Id = 3020; break;
                case "Sorcery: Great Heavy Soul Arrow": cItem.Title = "Sorcery: Great Heavy Soul Arrow"; cItem.Id = 3030; break;
                case "Sorcery: Homing Soulmass": cItem.Title = "Sorcery: Homing Soulmass"; cItem.Id = 3040; break;
                case "Sorcery: Homing Crystal Soulmass": cItem.Title = "Sorcery: Homing Crystal Soulmass"; cItem.Id = 3050; break;
                case "Sorcery: Soul Spear": cItem.Title = "Sorcery: Soul Spear"; cItem.Id = 3060; break;
                case "Sorcery: Crystal Soul Spear": cItem.Title = "Sorcery: Crystal Soul Spear"; cItem.Id = 3070; break;
                case "Sorcery: Magic Weapon": cItem.Title = "Sorcery: Magic Weapon"; cItem.Id = 3100; break;
                case "Sorcery: Great Magic Weapon": cItem.Title = "Sorcery: Great Magic Weapon"; cItem.Id = 3110; break;
                case "Sorcery: Crystal Magic Weapon": cItem.Title = "Sorcery: Crystal Magic Weapon"; cItem.Id = 3120; break;
                case "Sorcery: Magic Shield": cItem.Title = "Sorcery: Magic Shield"; cItem.Id = 3300; break;
                case "Sorcery: Strong Magic Shield": cItem.Title = "Sorcery: Strong Magic Shield"; cItem.Id = 3310; break;
                case "Sorcery: Hidden Weapon": cItem.Title = "Sorcery: Hidden Weapon"; cItem.Id = 3400; break;
                case "Sorcery: Hidden Body": cItem.Title = "Sorcery: Hidden Body"; cItem.Id = 3410; break;
                case "Sorcery: Cast Light": cItem.Title = "Sorcery: Cast Light"; cItem.Id = 3500; break;
                case "Sorcery: Hush": cItem.Title = "Sorcery: Hush"; cItem.Id = 3510; break;
                case "Sorcery: Aural Decoy": cItem.Title = "Sorcery: Aural Decoy"; cItem.Id = 3520; break;
                case "Sorcery: Repair": cItem.Title = "Sorcery: Repair"; cItem.Id = 3530; break;
                case "Sorcery: Fall Control": cItem.Title = "Sorcery: Fall Control"; cItem.Id = 3540; break;
                case "Sorcery: Chameleon": cItem.Title = "Sorcery: Chameleon"; cItem.Id = 3550; break;
                case "Sorcery: Resist Curse": cItem.Title = "Sorcery: Resist Curse"; cItem.Id = 3600; break;
                case "Sorcery: Remedy": cItem.Title = "Sorcery: Remedy"; cItem.Id = 3610; break;
                case "Sorcery: White Dragon Breath": cItem.Title = "Sorcery: White Dragon Breath"; cItem.Id = 3700; break;
                case "Sorcery: Dark Orb": cItem.Title = "Sorcery: Dark Orb"; cItem.Id = 3710; break;
                case "Sorcery: Dark Bead": cItem.Title = "Sorcery: Dark Bead"; cItem.Id = 3720; break;
                case "Sorcery: Dark Fog": cItem.Title = "Sorcery: Dark Fog"; cItem.Id = 3730; break;
                case "Sorcery: Pursuers": cItem.Title = "Sorcery: Pursuers"; cItem.Id = 3740; break;
                case "Pyromancy: Fireball": cItem.Title = "Pyromancy: Fireball"; cItem.Id = 4000; break;
                case "Pyromancy: Fire Orb": cItem.Title = "Pyromancy: Fire Orb"; cItem.Id = 4010; break;
                case "Pyromancy: Great Fireball": cItem.Title = "Pyromancy: Great Fireball"; cItem.Id = 4020; break;
                case "Pyromancy: Firestorm": cItem.Title = "Pyromancy: Firestorm"; cItem.Id = 4030; break;
                case "Pyromancy: Fire Tempest": cItem.Title = "Pyromancy: Fire Tempest"; cItem.Id = 4040; break;
                case "Pyromancy: Fire Surge": cItem.Title = "Pyromancy: Fire Surge"; cItem.Id = 4050; break;
                case "Pyromancy: Fire Whip": cItem.Title = "Pyromancy: Fire Whip"; cItem.Id = 4060; break;
                case "Pyromancy: Combustion": cItem.Title = "Pyromancy: Combustion"; cItem.Id = 4100; break;
                case "Pyromancy: Great Combustion": cItem.Title = "Pyromancy: Great Combustion"; cItem.Id = 4110; break;
                case "Pyromancy: Poison Mist": cItem.Title = "Pyromancy: Poison Mist"; cItem.Id = 4200; break;
                case "Pyromancy: Toxic Mist": cItem.Title = "Pyromancy: Toxic Mist"; cItem.Id = 4210; break;
                case "Pyromancy: Acid Surge": cItem.Title = "Pyromancy: Acid Surge"; cItem.Id = 4220; break;
                case "Pyromancy: Iron Flesh": cItem.Title = "Pyromancy: Iron Flesh"; cItem.Id = 4300; break;
                case "Pyromancy: Flash Sweat": cItem.Title = "Pyromancy: Flash Sweat"; cItem.Id = 4310; break;
                case "Pyromancy: Undead Rapport": cItem.Title = "Pyromancy: Undead Rapport"; cItem.Id = 4360; break;
                case "Pyromancy: Power Within": cItem.Title = "Pyromancy: Power Within"; cItem.Id = 4400; break;
                case "Pyromancy: Great Chaos Fireball": cItem.Title = "Pyromancy: Great Chaos Fireball"; cItem.Id = 4500; break;
                case "Pyromancy: Chaos Storm": cItem.Title = "Pyromancy: Chaos Storm"; cItem.Id = 4510; break;
                case "Pyromancy: Chaos Fire Whip": cItem.Title = "Pyromancy: Chaos Fire Whip"; cItem.Id = 4520; break;
                case "Pyromancy: Black Flame": cItem.Title = "Pyromancy: Black Flame"; cItem.Id = 4530; break;
                case "Miracle: Heal": cItem.Title = "Miracle: Heal"; cItem.Id = 5000; break;
                case "Miracle: Great Heal": cItem.Title = "Miracle: Great Heal"; cItem.Id = 5010; break;
                case "Miracle: Great Heal Excerpt": cItem.Title = "Miracle: Great Heal Excerpt"; cItem.Id = 5020; break;
                case "Miracle: Soothing Sunlight": cItem.Title = "Miracle: Soothing Sunlight"; cItem.Id = 5030; break;
                case "Miracle: Replenishment": cItem.Title = "Miracle: Replenishment"; cItem.Id = 5040; break;
                case "Miracle: Bountiful Sunlight": cItem.Title = "Miracle: Bountiful Sunlight"; cItem.Id = 5050; break;
                case "Miracle: Gravelord Sword Dance": cItem.Title = "Miracle: Gravelord Sword Dance"; cItem.Id = 5100; break;
                case "Miracle: Gravelord Greatsword Dance": cItem.Title = "Miracle: Gravelord Greatsword Dance"; cItem.Id = 5110; break;
                case "Miracle: Homeward": cItem.Title = "Miracle: Homeward"; cItem.Id = 5210; break;
                case "Miracle: Force": cItem.Title = "Miracle: Force"; cItem.Id = 5300; break;
                case "Miracle: Wrath of the Gods": cItem.Title = "Miracle: Wrath of the Gods"; cItem.Id = 5310; break;
                case "Miracle: Emit Force": cItem.Title = "Miracle: Emit Force"; cItem.Id = 5320; break;
                case "Miracle: Seek Guidance": cItem.Title = "Miracle: Seek Guidance"; cItem.Id = 5400; break;
                case "Miracle: Lightning Spear": cItem.Title = "Miracle: Lightning Spear"; cItem.Id = 5500; break;
                case "Miracle: Great Lightning Spear": cItem.Title = "Miracle: Great Lightning Spear"; cItem.Id = 5510; break;
                case "Miracle: Sunlight Spear": cItem.Title = "Miracle: Sunlight Spear"; cItem.Id = 5520; break;
                case "Miracle: Magic Barrier": cItem.Title = "Miracle: Magic Barrier"; cItem.Id = 5600; break;
                case "Miracle: Great Magic Barrier": cItem.Title = "Miracle: Great Magic Barrier"; cItem.Id = 5610; break;
                case "Miracle: Karmic Justice": cItem.Title = "Miracle: Karmic Justice"; cItem.Id = 5700; break;
                case "Miracle: Tranquil Walk of Peace": cItem.Title = "Miracle: Tranquil Walk of Peace"; cItem.Id = 5800; break;
                case "Miracle: Vow of Silence": cItem.Title = "Miracle: Vow of Silence"; cItem.Id = 5810; break;
                case "Miracle: Sunlight Blade": cItem.Title = "Miracle: Sunlight Blade"; cItem.Id = 5900; break;
                case "Miracle: Darkmoon Blade": cItem.Title = "Miracle: Darkmoon Blade"; cItem.Id = 5910; break;
                case "Sorcerer's Catalyst": cItem.Title = "Sorcerer's Catalyst"; cItem.Id = 1300000; break;
                case "Beatrice's Catalyst": cItem.Title = "Beatrice's Catalyst"; cItem.Id = 1301000; break;
                case "Tin Banishment Catalyst": cItem.Title = "Tin Banishment Catalyst"; cItem.Id = 1302000; break;
                case "Logan's Catalyst": cItem.Title = "Logan's Catalyst"; cItem.Id = 1303000; break;
                case "Tin Darkmoon Catalyst": cItem.Title = "Tin Darkmoon Catalyst"; cItem.Id = 1304000; break;
                case "Oolacile Ivory Catalyst": cItem.Title = "Oolacile Ivory Catalyst"; cItem.Id = 1305000; break;
                case "Tin Crystallization Catalyst": cItem.Title = "Tin Crystallization Catalyst"; cItem.Id = 1306000; break;
                case "Demon's Catalyst": cItem.Title = "Demon's Catalyst"; cItem.Id = 1307000; break;
                case "Izalith Catalyst": cItem.Title = "Izalith Catalyst"; cItem.Id = 1308000; break;
                case "Pyromancy Flame": cItem.Title = "Pyromancy Flame"; cItem.Id = 1330000; break;
                case "Pyromancy Flame (Ascended)": cItem.Title = "Pyromancy Flame (Ascended)"; cItem.Id = 1332000; break;
                case "Talisman": cItem.Title = "Talisman"; cItem.Id = 1360000; break;
                case "Canvas Talisman": cItem.Title = "Canvas Talisman"; cItem.Id = 1361000; break;
                case "Thorolund Talisman": cItem.Title = "Thorolund Talisman"; cItem.Id = 1362000; break;
                case "Ivory Talisman": cItem.Title = "Ivory Talisman"; cItem.Id = 1363000; break;
                case "Sunlight Talisman": cItem.Title = "Sunlight Talisman"; cItem.Id = 1365000; break;
                case "Darkmoon Talisman": cItem.Title = "Darkmoon Talisman"; cItem.Id = 1366000; break;
                case "Velka's Talisman": cItem.Title = "Velka's Talisman"; cItem.Id = 1367000; break;
                case "Manus Catalyst": cItem.Title = "Manus Catalyst"; cItem.Id = 9017000; break;
                case "Oolacile Catalyst": cItem.Title = "Oolacile Catalyst"; cItem.Id = 9018000; break;
                case "Large Ember": cItem.Title = "Large Ember"; cItem.Id = 800; break;
                case "Very Large Ember": cItem.Title = "Very Large Ember"; cItem.Id = 801; break;
                case "Crystal Ember": cItem.Title = "Crystal Ember"; cItem.Id = 802; break;
                case "Large Magic Ember": cItem.Title = "Large Magic Ember"; cItem.Id = 806; break;
                case "Enchanted Ember": cItem.Title = "Enchanted Ember"; cItem.Id = 807; break;
                case "Divine Ember": cItem.Title = "Divine Ember"; cItem.Id = 808; break;
                case "Large Divine Ember": cItem.Title = "Large Divine Ember"; cItem.Id = 809; break;
                case "Dark Ember": cItem.Title = "Dark Ember"; cItem.Id = 810; break;
                case "Large Flame Ember": cItem.Title = "Large Flame Ember"; cItem.Id = 812; break;
                case "Chaos Flame Ember": cItem.Title = "Chaos Flame Ember"; cItem.Id = 813; break;
                case "Titanite Shard": cItem.Title = "Titanite Shard"; cItem.Id = 1000; break;
                case "Large Titanite Shard": cItem.Title = "Large Titanite Shard"; cItem.Id = 1010; break;
                case "Green Titanite Shard": cItem.Title = "Green Titanite Shard"; cItem.Id = 1020; break;
                case "Titanite Chunk": cItem.Title = "Titanite Chunk"; cItem.Id = 1030; break;
                case "Blue Titanite Chunk": cItem.Title = "Blue Titanite Chunk"; cItem.Id = 1040; break;
                case "White Titanite Chunk": cItem.Title = "White Titanite Chunk"; cItem.Id = 1050; break;
                case "Red Titanite Chunk": cItem.Title = "Red Titanite Chunk"; cItem.Id = 1060; break;
                case "Titanite Slab": cItem.Title = "Titanite Slab"; cItem.Id = 1070; break;
                case "Blue Titanite Slab": cItem.Title = "Blue Titanite Slab"; cItem.Id = 1080; break;
                case "White Titanite Slab": cItem.Title = "White Titanite Slab"; cItem.Id = 1090; break;
                case "Red Titanite Slab": cItem.Title = "Red Titanite Slab"; cItem.Id = 1100; break;
                case "Dragon Scale": cItem.Title = "Dragon Scale"; cItem.Id = 1110; break;
                case "Demon Titanite": cItem.Title = "Demon Titanite"; cItem.Id = 1120; break;
                case "Twinkling Titanite": cItem.Title = "Twinkling Titanite"; cItem.Id = 1130; break;
                case "White Sign Soapstone": cItem.Title = "White Sign Soapstone"; cItem.Id = 100; break;
                case "Red Sign Soapstone": cItem.Title = "Red Sign Soapstone"; cItem.Id = 101; break;
                case "Red Eye Orb": cItem.Title = "Red Eye Orb"; cItem.Id = 102; break;
                case "Black Separation Crystal": cItem.Title = "Black Separation Crystal"; cItem.Id = 103; break;
                case "Orange Guidance Soapstone": cItem.Title = "Orange Guidance Soapstone"; cItem.Id = 106; break;
                case "Book of the Guilty": cItem.Title = "Book of the Guilty"; cItem.Id = 108; break;
                case "Servant Roster": cItem.Title = "Servant Roster"; cItem.Id = 112; break;
                case "Blue Eye Orb": cItem.Title = "Blue Eye Orb"; cItem.Id = 113; break;
                case "Dragon Eye": cItem.Title = "Dragon Eye"; cItem.Id = 114; break;
                case "Black Eye Orb": cItem.Title = "Black Eye Orb"; cItem.Id = 115; break;
                case "Darksign": cItem.Title = "Darksign"; cItem.Id = 117; break;
                case "Purple Coward's Crystal": cItem.Title = "Purple Coward's Crystal"; cItem.Id = 118; break;
                case "Silver Pendant": cItem.Title = "Silver Pendant"; cItem.Id = 220; break;
                case "Binoculars": cItem.Title = "Binoculars"; cItem.Id = 371; break;
                case "Dragon Head Stone": cItem.Title = "Dragon Head Stone"; cItem.Id = 377; break;
                case "Dragon Torso Stone": cItem.Title = "Dragon Torso Stone"; cItem.Id = 378; break;
                case "Dried Finger": cItem.Title = "Dried Finger"; cItem.Id = 385; break;
                case "Hello Carving": cItem.Title = "Hello Carving"; cItem.Id = 510; break;
                case "Thank you Carving": cItem.Title = "Thank you Carving"; cItem.Id = 511; break;
                case "Very good! Carving": cItem.Title = "Very good! Carving"; cItem.Id = 512; break;
                case "I'm sorry Carving": cItem.Title = "I'm sorry Carving"; cItem.Id = 513; break;
                case "Help me! Carving": cItem.Title = "Help me! Carving"; cItem.Id = 514; break;
                default: cItem = null; break;
            }
            return cItem;
        }
        #endregion
    }

    [Serializable]
    public class DTDs1
    { 
        //var Settings
        public bool enableSplitting = false;
        public bool autoTimer = false;
        public bool gameTimer = false;
        public int positionMargin = 3;
        //Flags to Split
        public List<DefinitionsDs1.BossDs1> bossToSplit = new List<DefinitionsDs1.BossDs1>();
        public List<DefinitionsDs1.BonfireDs1> bonfireToSplit = new List<DefinitionsDs1.BonfireDs1>();
        public List<DefinitionsDs1.LvlDs1> lvlToSplit = new List<DefinitionsDs1.LvlDs1>();
        public List<DefinitionsDs1.PositionDs1> positionsToSplit = new List<DefinitionsDs1.PositionDs1>();
        public List<DefinitionsDs1.ItemDs1> itemToSplit = new List<DefinitionsDs1.ItemDs1>();


        public List<DefinitionsDs1.BossDs1> getBossToSplit()
        {
            return this.bossToSplit;
        }

        public List<DefinitionsDs1.BonfireDs1> getBonfireToSplit()
        {
            return this.bonfireToSplit;
        }

        public List<DefinitionsDs1.LvlDs1> getLvlToSplit()
        {
            return this.lvlToSplit;
        }

        public List<DefinitionsDs1.PositionDs1> getPositionsToSplit()
        {
            return this.positionsToSplit;
        }

        public List<DefinitionsDs1.ItemDs1> getItemsToSplit()
        {
            return this.itemToSplit;
        }
    }
}
