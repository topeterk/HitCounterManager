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
using SoulMemory.DarkSouls2;
using SoulMemory;

namespace AutoSplitterCore
{
    public class DefinitionsDs2
    {
        #region Boss.Ds2
        [Serializable]
        public class BossDs2
        {
            public string Title;
            public BossType Id;
            public bool IsSplited =false;
            public string Mode;
        }

        public BossDs2 stringToEnumBoss(string boss)
        {
            BossDs2 cBoss = new BossDs2();
            switch (boss)
            {
                case "The Last Giant": cBoss.Title = "The Last Giant"; cBoss.Id = BossType.TheLastGiant; break;
                case "The Pursuer": cBoss.Title = "The Persuer"; cBoss.Id = BossType.ThePursuer; break;
                case "Executioners Chariot": cBoss.Title = "Executioners Chariot"; cBoss.Id = BossType.ExecutionersChariot; break;
                case "Looking Glass Knight": cBoss.Title = "Looking Glass Knight"; cBoss.Id = BossType.LookingGlassKnight; break;
                case "The Skeleton Lords": cBoss.Title = "The Skeleton Lords"; cBoss.Id = BossType.TheSkeletonLords; break;
                case "Flexile Sentry": cBoss.Title = "Flexile Sentry"; cBoss.Id = BossType.FlexileSentry; break;
                case "Lost Sinner": cBoss.Title = "Lost Sinner"; cBoss.Id = BossType.LostSinner; break;
                case "Belfry Gargoyles": cBoss.Title = "Belfry Gargoyles"; cBoss.Id = BossType.BelfryGargoyles; break;
                case "Ruin Sentinels": cBoss.Title = "Ruin Sentinels"; cBoss.Id = BossType.RuinSentinels; break;
                case "Royal Rat Vanguard": cBoss.Title = "Royal Rat Vanguard"; cBoss.Id = BossType.RoyalRatVanguard; break;
                case "Royal Rat Authority": cBoss.Title = "Royal Rat Authority"; cBoss.Id = BossType.RoyalRatAuthority; break;
                case "Scorpioness Najka": cBoss.Title = "Scorpioness Najka"; cBoss.Id = BossType.ScorpionessNajka; break;
                case "The Duke's Dear Freja": cBoss.Title = "The Duke's Dear Freja"; cBoss.Id = BossType.TheDukesDearFreja; break;
                case "Mytha, the Baneful Queen": cBoss.Title = "Mytha, the Baneful Queen"; cBoss.Id = BossType.MythaTheBanefulQueen; break;
                case "The Rotten": cBoss.Title = "The Rotten"; cBoss.Id = BossType.TheRotten; break;
                case "Old DragonSlayer": cBoss.Title = "Old DragonSlayer"; cBoss.Id = BossType.OldDragonSlayer; break;
                case "Covetous Demon": cBoss.Title = "Covetous Demon"; cBoss.Id = BossType.CovetousDemon; break;
                case "Smelter Demon": cBoss.Title = "Smelter Demon"; cBoss.Id = BossType.SmelterDemon; break;
                case "Old Iron King": cBoss.Title = "Old Iron King"; cBoss.Id = BossType.OldIronKing; break;
                case "Guardian Dragon": cBoss.Title = "Guardian Dragon"; cBoss.Id = BossType.GuardianDragon; break;
                case "Demon of Song": cBoss.Title = "Demon of Song"; cBoss.Id = BossType.DemonOfSong; break;
                case "Velstadt, The Royal Aegis": cBoss.Title = "Velstadt, The Royal Aegis"; cBoss.Id = BossType.VelstadtTheRoyalAegis; break;
                case "Vendrick": cBoss.Title = "Vendrick"; cBoss.Id = BossType.Vendrick; break;
                case "Darklurker": cBoss.Title = "Darklurker"; cBoss.Id = BossType.Darklurker; break;
                case "Dragonrider": cBoss.Title = "Dragonrider"; cBoss.Id = BossType.Dragonrider; break;
                case "Twin Dragonriders": cBoss.Title = "Twin Dragonriders"; cBoss.Id = BossType.TwinDragonriders; break;
                case "Prowling Magnus and Congregation": cBoss.Title = "Prowling Magnus and Congregation"; cBoss.Id = BossType.ProwlingMagnusAndCongregation; break;
                case "Giant Lord": cBoss.Title = "Giant Lord"; cBoss.Id = BossType.GiantLord; break;
                case "Ancient Dragon": cBoss.Title = "Ancient Dragon"; cBoss.Id = BossType.AncientDragon; break;
                case "Throne Watcher and Throne Defender": cBoss.Title = "Throne Watcher and Throne Defender"; cBoss.Id = BossType.ThroneWatcherAndThroneDefender; break;
                case "Nashandra": cBoss.Title = "Nashandra"; cBoss.Id = BossType.Nashandra; break;
                case "Aldia, Scholar of the First Sin": cBoss.Title = "Aldia, Scholar of the First Sin"; cBoss.Id = BossType.AldiaScholarOfTheFirstSin; break;
                case "Elana, Squalid Queen": cBoss.Title = "Elana, Squalid Queen"; cBoss.Id = BossType.ElanaSqualidQueen; break;
                case "Sinh, the Slumbering Dragon": cBoss.Title = "Sinh, the Slumbering Dragon"; cBoss.Id = BossType.SinhTheSlumberingDragon; break;
                case "Afflicted Graverobber, Ancient Soldier Varg, and Cerah the Old Explorer": cBoss.Title = "Afflicted Graverobber, Ancient Soldier Varg, and Cerah the Old Explorer"; cBoss.Id = BossType.AfflictedGraverobberAncientSoldierVargCerahTheOldExplorer; break;
                case "Blue Smelter Demon": cBoss.Title = "Blue Smelter Demon"; cBoss.Id = BossType.BlueSmelterDemon; break;
                case "Fume knight": cBoss.Title = "Fume knight"; cBoss.Id = BossType.Fumeknight; break;
                case "Sir Alonne": cBoss.Title = "Sir Alonne"; cBoss.Id = BossType.SirAlonne; break;
                case "Burnt Ivory King": cBoss.Title = "Burnt Ivory King"; cBoss.Id = BossType.BurntIvoryKing; break;
                case "Aava, the King's Pet": cBoss.Title = "Aava, the King's Pet"; cBoss.Id = BossType.AavaTheKingsPet; break;
                case "Lud and Zallen, the King's Pets": cBoss.Title = "Lud and Zallen, the King's Pets"; cBoss.Id = BossType.LudAndZallenTheKingsPets; break;
                default:cBoss = null; break;
            }
            return cBoss;
        }
        #endregion
        #region Lvl.Ds2
        public class LvlDs2
        {
            public string Title;
            public SoulMemory.DarkSouls2.Attribute Attribute;
            public uint Value;
            public bool IsSplited = false;
            public string Mode;
        }

        public SoulMemory.DarkSouls2.Attribute stringToEnumAttribute(string attribute)
        {
            switch (attribute)
            {
                case "Vigor":
                    return SoulMemory.DarkSouls2.Attribute.Vigor;
                case "Attunement":
                    return SoulMemory.DarkSouls2.Attribute.Attunement;
                case "Endurance":
                    return SoulMemory.DarkSouls2.Attribute.Endurance;
                case "Vitality":
                    return SoulMemory.DarkSouls2.Attribute.Vitality;
                case "Strength":
                    return SoulMemory.DarkSouls2.Attribute.Strength;
                case "Dexterity":
                    return SoulMemory.DarkSouls2.Attribute.Dexterity;
                case "Intelligence":
                    return SoulMemory.DarkSouls2.Attribute.Intelligence;
                case "Faith":
                    return SoulMemory.DarkSouls2.Attribute.Faith;
                case "Adaptability":
                    return SoulMemory.DarkSouls2.Attribute.Adaptability;
                case "SoulLevel":
                    return SoulMemory.DarkSouls2.Attribute.SoulLevel;
                default: return SoulMemory.DarkSouls2.Attribute.SoulLevel;
            }
        }
        #endregion
        #region Position.Ds2
        [Serializable]
        public class PositionDs2
        {
            public Vector3f vector = new Vector3f();
            public bool IsSplited = false;
            public string Mode;
        }
        #endregion
    }

    [Serializable]
    public class DTDs2
    {
        //var Settings
        public bool enableSplitting = false;
        public bool autoTimer = false;
        public bool gameTimer = false;
        public int positionMargin = 3;
        //Flags to Split
        public List<DefinitionsDs2.BossDs2> bossToSplit = new List<DefinitionsDs2.BossDs2>();
        public List<DefinitionsDs2.LvlDs2> lvlToSplit = new List<DefinitionsDs2.LvlDs2>();
        public List<DefinitionsDs2.PositionDs2> positionsToSplit = new List<DefinitionsDs2.PositionDs2>();

        public List<DefinitionsDs2.BossDs2> getBossToSplit()
        {
            return this.bossToSplit;
        }
        public List<DefinitionsDs2.LvlDs2> getLvlToSplit()
        {
            return this.lvlToSplit;
        }

        public List<DefinitionsDs2.PositionDs2> getPositionsToSplit()
        {
            return this.positionsToSplit;
        }

    }
}
