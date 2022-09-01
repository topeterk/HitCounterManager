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
using LiveSplit.HollowKnight;

namespace AutoSplitterCore
{
    public class DefinitionHollow
    {
        [Serializable]
        public class ElementToSplitH
        {
            public string Title;
            public Offset Offset = new Offset();
            public bool IsSplited = false;
            public bool intMethod = false;
            public int intCompare = 0;
            public bool kingSoulsCase = false;

            public void setData(Offset offset, string title)
            {
                this.Offset = offset;
                this.Title = title;
            }

            public void setMethod(int intCompare)
            {
                this.intMethod = true;
                this.intCompare = intCompare;
            }

            public void ksC()
            {
                this.kingSoulsCase = true;
            }
        }

        public ElementToSplitH stringToEnum(string element)
        {
            ElementToSplitH elem = new ElementToSplitH();
            switch (element)
            {
                #region Boss
                case "Broken Vessel":
                    elem.setData(Offset.killedInfectedKnight, "Broken Vessel");
                    break;
                case "Brooding Mawlek":
                    elem.setData(Offset.killedMawlek, "Brooding Mawlek");
                    break;
                case "Collector":
                    elem.setData(Offset.collectorDefeated, "Collector");
                    break;
                case "Crystal Guardian":
                    elem.setData(Offset.defeatedMegaBeamMiner, "Crystal Guardian");
                    break;            
                case "Dung Defender":
                    elem.setData(Offset.killedDungDefender, "Dung Defender");
                    break;
                case "Elder Hu":
                    elem.setData(Offset.killedGhostHu, "Elder Hu");
                    break;
                case "False Knight":
                    elem.setData(Offset.killedFalseKnight, "False Knight");
                    break;
                case "Failed Champion":
                    elem.setData(Offset.falseKnightDreamDefeated, "Failed Champion");
                    break;
                case "Flukemarm":
                    elem.setData(Offset.killedFlukeMother, "Flukemarm");
                    break;
                case "Galien":
                    elem.setData(Offset.killedGhostGalien, "Galien");
                    break;
                case "God Tamer":
                    elem.setData(Offset.killedLobsterLancer, "God Tamer");
                    break;
                case "Gorb":
                    elem.setData(Offset.killedGhostAladar, "Gorb");
                    break;
                case "Grey Prince Zote":
                    elem.setData(Offset.killedGreyPrince, "Grey Prince Zote");
                    break;
                case "Gruz Mother":
                    elem.setData(Offset.killedBigFly, "Gruz Mother");
                    break;
                case "Hive Knight":
                    elem.setData(Offset.killedHiveKnight, "Hive Knight");
                    break;
                case "Hornet (Greenpath)":
                    elem.setData(Offset.killedHornet, "Hornet (Greenpath)");
                    break;
                case "Hornet (Kingdom's Edge)":
                    elem.setData(Offset.hornetOutskirtsDefeated, "Hornet (Kingdom's Edge)");
                    break;
                case "Lost Kin":
                    elem.setData(Offset.infectedKnightDreamDefeated, "Lost Kin");
                    break;
                case "Mantis Lords":
                    elem.setData(Offset.defeatedMantisLords, "Mantis Lords");
                    break;
                case "Markoth":
                    elem.setData(Offset.killedGhostMarkoth, "Markoth");
                    break;
                case "Marmu":
                    elem.setData(Offset.killedGhostMarmu, "Marmu");
                    break;
                case "Massive Moss Charger":
                    elem.setData(Offset.megaMossChargerDefeated, "Massive Moss Charger");
                    break;
                case "Nightmare King Grimm":
                    elem.setData(Offset.killedNightmareGrimm, "Nightmare King Grimm");
                    break;
                case "No Eyes":
                    elem.setData(Offset.killedGhostNoEyes, "No Eyes");
                    break;
                case "Nosk":
                    elem.setData(Offset.killedMimicSpider, "Nosk");
                    break;
                case "Oro & Mato Nail Bros":
                    elem.setData(Offset.killedNailBros, "Oro & Mato Nail Bros");
                    break;
                case "Pure Vessel":
                    elem.setData(Offset.killedHollowKnightPrime, "Pure Vessel");
                    break;
                case "Radiance":
                    elem.setData(Offset.killedFinalBoss, "Radiance");
                    break;
                case "The Hollow Knight":
                    elem.setData(Offset.killedHollowKnight, "The Hollow Knight");
                    break;
                case "Paintmaster Sheo":
                    elem.setData(Offset.killedPaintmaster, "Paintmaster Sheo");
                    break;
                case "Great Nailsage Sly":
                    elem.setData(Offset.killedNailsage, "Great Nailsage Sly");
                    break;
                case "Soul Master":
                    elem.setData(Offset.killedMageLord, "Soul Master");
                    break;
                case "Soul Tyrant":
                    elem.setData(Offset.mageLordDreamDefeated, "Soul Tyrant");
                    break;
                case "Traitor Lord":
                    elem.setData(Offset.killedTraitorLord, "Traitor Lord");
                    break;
                case "Troupe Master Grimm":
                    elem.setData(Offset.killedGrimm, "Troupe Master Grimm");
                    break;
                case "Uumuu":
                    elem.setData(Offset.killedMegaJellyfish, "Uumuu");
                    break;
                case "Watcher Knight":
                    elem.setData(Offset.killedBlackKnight, "Watcher Knight");
                    break;
                case "White Defender":
                    elem.setData(Offset.killedWhiteDefender, "White Defender");
                    break;
                case "Xero":
                    elem.setData(Offset.killedGhostXero, "Xero");
                    break;
                #endregion
                #region MiniBoss- Dreamers - Others
                case "Enraged Guardian":
                    elem.setData(Offset.killsMegaBeamMiner, "Enraged Guardian");
                    elem.setMethod(0);
                    break;
                case "Oblobbles":
                    elem.setData(Offset.killsOblobble, "Oblobbles");
                    elem.setMethod(1);
                    break;
                case "Aspid Hunter":
                    elem.setData(Offset.killsSpitter, "Aspid Hunter");
                    elem.setMethod(17);
                    break;
                case "Moss Knight":
                    elem.setData(Offset.killedMossKnight, "Moss Knight");
                    break;
                case "Shrumal Ogres":
                    elem.setData(Offset.killsMushroomBrawler, "Shrumal Ogres");
                    elem.setMethod(6);
                    break;
                case "Zote Rescued - Vengefly King":
                    elem.setData(Offset.zoteRescuedBuzzer, "Zote Rescued - Vengefly King");
                    break;
                case "Zote Rescued - Deepnest":
                    elem.setData(Offset.zoteRescuedDeepnest, "Zote Rescued - Deepnest");
                    break;
                case "Zote Defeated - Colosseum":
                    elem.setData(Offset.killedZote, "Zote Defeated - Colosseum");
                    break;
                case "First Dreamer":
                    elem.setData(Offset.guardiansDefeated, "First Dreamer");
                    elem.setMethod(1);
                    break;
                case "Second Dreamer":
                    elem.setData(Offset.guardiansDefeated, "Second Dreamer");
                    elem.setMethod(2);
                    break;
                case "Third Dreamer":
                    elem.setData(Offset.guardiansDefeated, "Third Dreamer");
                    elem.setMethod(3);
                    break;
                case "Colosseum Warrior Completed":
                    elem.setData(Offset.colosseumBronzeCompleted, "Colosseum Warrior Completed");
                    break;
                case "Colosseum Conqueror Completed":
                    elem.setData(Offset.colosseumSilverCompleted, "Colosseum Conqueror Completed");
                    break;
                case "Colosseum Fool Completed":
                    elem.setData(Offset.colosseumGoldCompleted, "Colosseum Fool Completed");
                     break;
                case "Aluba":
                    elem.setData(Offset.killedLazyFlyer, "Aluba");
                    break;
                case "Great Hopper":
                    elem.setData(Offset.killedGiantHopper, "Great Hopper");
                    break;
                case "Gorgeous Husk":
                    elem.setData(Offset.killedGorgeousHusk, "Gorgeous Husk");
                    break;
                case "Menderbug":
                    elem.setData(Offset.killedMenderBug, "Menderbug");
                    break;
                case "Soul Warrior":
                    elem.setData(Offset.killedMageKnight, "Soul Warrior");
                    break;
                case "Soul Twister":
                    elem.setData(Offset.killedMage, "Soul Twister");
                    break;
                case "Mimic 1":
                    elem.setData(Offset.killsGrubMimic, "Mimic 1");
                    elem.setMethod(4);
                    break;
                case "Mimic 2":
                    elem.setData(Offset.killsGrubMimic, "Mimic 2");
                    elem.setMethod(3);
                    break;
                case "Mimic 3":
                    elem.setData(Offset.killsGrubMimic, "Mimic 3");
                    elem.setMethod(2);
                    break;
                case "Mimic 4":
                    elem.setData(Offset.killsGrubMimic, "Mimic 4");
                    elem.setMethod(1);
                    break;
                case "Mimic 5":
                    elem.setData(Offset.killsGrubMimic, "Mimic 5");
                    elem.setMethod(0);
                    break;
                case "Path of Pain - Completed":
                    elem.setData(Offset.newDataBindingSeal, "Path of Pain - Completed");
                    break;
                case "Flower Quest - Completed":
                    elem.setData(Offset.xunRewardGiven, "Flower Quest - Completed");
                    break;
                #endregion
                #region Charms
                case "Baldur Shell":
                    elem.setData(Offset.gotCharm_5, "Baldur Shell");
                    break;
                case "Dashmaster":
                    elem.setData(Offset.gotCharm_31, "Dashmaster");
                    break;
                case "Deep Focus":
                    elem.setData(Offset.gotCharm_34, "Deep Focus");
                    break;
                case "Defenders Crest":
                    elem.setData(Offset.gotCharm_10, "Defenders Crest");
                    break;
                case "Dreamshield":
                    elem.setData(Offset.gotCharm_38, "Dreamshield");
                    break;
                case "Dream Wielder":
                    elem.setData(Offset.gotCharm_30, "Dream Wielder");
                    break;
                case "Flukenest":
                    elem.setData(Offset.gotCharm_11, "Flukenest");
                    break;
                case "Fragile Greed":
                    elem.setData(Offset.gotCharm_24, "Fragile Greed");
                    break;
                case "Fragile Heart":
                    elem.setData(Offset.gotCharm_23, "Fragile Heart");
                    break;
                case "Fragile Strength":
                    elem.setData(Offset.gotCharm_25, "Fragile Strength");
                    break;
                case "Fury of the Fallen":
                    elem.setData(Offset.gotCharm_6, "Fury of the Fallen");
                    break;
                case "Gathering Swarm":
                    elem.setData(Offset.gotCharm_1, "Gathering Swarm");
                    break;
                case "Glowing Womb":
                    elem.setData(Offset.gotCharm_22, "Glowing Womb");
                    break;
                case "Grimmchild":
                    elem.setData(Offset.gotCharm_40, "Grimmchild");
                    break;
                case "Grimmchild Lvl 2":
                    elem.setData(Offset.grimmChildLevel, "Grimmchild Lvl 2");
                    elem.setMethod(2);
                    break;
                case "Grimmchild Lvl 3":
                    elem.setData(Offset.grimmChildLevel, "Grimmchild Lvl 3");
                    elem.setMethod(3);
                    break;
                case "Grimmchild Lvl 4":
                    elem.setData(Offset.grimmChildLevel, "Grimmchild Lvl 4");
                    elem.setMethod(4);
                    break;
                case "Grubberfly's Elegy":
                    elem.setData(Offset.gotCharm_35, "Grubberfly's Elegy");
                    break;
                case "Grubsong":
                    elem.setData(Offset.gotCharm_3, "Grubsong");
                    break;
                case "Heavy Blow":
                    elem.setData(Offset.gotCharm_15, "Heavy Blow");
                    break;
                case "Hiveblood":
                    elem.setData(Offset.gotCharm_29, "Hiveblood");
                    break;
                case "Joni's Blessing":
                    elem.setData(Offset.gotCharm_27, "Joni's Blessing");
                    break;
                case "White Fragment - Queen's":
                    elem.setData(Offset.gotQueenFragment, "White Fragment - Queen's");
                    break;
                case "White Fragment - King's":
                    elem.setData(Offset.gotKingFragment, "White Fragment - King's");
                    break;
                case "Kingsoul":
                    elem.setData(Offset.ore, "Kingsoul");
                    elem.ksC();
                    break;
                case "Lifeblood Core":
                    elem.setData(Offset.gotCharm_9, "Lifeblood Core");
                    break;
                case "Lifeblood Heart":
                    elem.setData(Offset.gotCharm_8, "Lifeblood Heart");
                    break;
                case "Longnail":
                    elem.setData(Offset.gotCharm_18, "Longnail");
                    break;
                case "Mark of Pride":
                    elem.setData(Offset.gotCharm_13, "Mark of Pride");
                    break;
                case "Nailmaster's Glory":
                    elem.setData(Offset.gotCharm_26, "Nailmaster's Glory");
                    break;
                case "Quick Focus":
                    elem.setData(Offset.gotCharm_7, "Quick Focus");
                    break;
                case "Quick Slash":
                    elem.setData(Offset.gotCharm_32, "Quick Slash");
                    break;
                case "Shaman Stone":
                    elem.setData(Offset.gotCharm_19, "Shaman Stone");
                    break;
                case "Shape of Unn":
                    elem.setData(Offset.gotCharm_28, "Shape of Unn");
                    break;
                case "Sharp Shadow":
                    elem.setData(Offset.gotCharm_16, "Sharp Shadow");
                    break;
                case "Soul Catcher":
                    elem.setData(Offset.gotCharm_20, "Soul Catcher");
                    break;
                case "Soul Eater":
                    elem.setData(Offset.gotCharm_21, "Soul Eater");
                    break;
                case "Spell Twister":
                    elem.setData(Offset.gotCharm_33, "Spell Twister");
                    break;
                case "Spore Shroom":
                    elem.setData(Offset.gotCharm_17, "Spore Shroom");
                    break;
                case "Sprintmaster":
                    elem.setData(Offset.gotCharm_37, "Sprintmaster");
                    break;
                case "Stalwart Shell":
                 elem.setData(Offset.gotCharm_4, "Stalwart Shell");
                    break;
                case "Steady Body":
                    elem.setData(Offset.gotCharm_14, "Steady Body");
                    break;
                case "Thorns of Agony":
                    elem.setData(Offset.gotCharm_12, "Thorns of Agony");
                    break;
                case "Unbreakable Greed":
                    elem.setData(Offset.fragileGreed_unbreakable, "Unbreakable Greed");
                    break;
                case "Unbreakable Heart":
                    elem.setData(Offset.fragileHealth_unbreakable, "Unbreakable Heart");
                    break;
                case "Unbreakable Strength":
                    elem.setData(Offset.fragileStrength_unbreakable, "Unbreakable Strength");
                    break;
                case "Void Heart":
                    elem.setData(Offset.gotShadeCharm, "Void Heart");
                    break;
                case "Wayward Compass":
                    elem.setData(Offset.gotCharm_2, "Wayward Compass");
                    break;
                case "Weaversong":
                    elem.setData(Offset.gotCharm_39, "Weaversong");
                    break;
                case "Shrumal Ogres (Charm)":
                    elem.setData(Offset.notchShroomOgres, "Shrumal Ogres (Charm)");
                    break;
                case "Fog Canyon":
                    elem.setData(Offset.notchFogCanyon, "Fog Canyon");
                    break;
                case "Salubra 1":
                    elem.setData(Offset.salubraNotch1, "Salubra 1");
                    break;
                case "Salubra 2":
                    elem.setData(Offset.salubraNotch2, "Salubra 2");
                    break;
                case "Salubra 3":
                    elem.setData(Offset.salubraNotch3, "Salubra 3");
                    break;
                case "Salubra 4":
                    elem.setData(Offset.salubraNotch4, "Salubra 4");
                    break;

                #endregion
                #region Skills
                case "Abyss Shriek":
                    elem.setData(Offset.screamLevel, "Abyss Shriek");
                    elem.setMethod(2);
                    break;
                case "Crystal Heart":
                    elem.setData(Offset.hasSuperDash, "Crystal Heart");
                    break;
                case "Cyclone Slash":
                    elem.setData(Offset.hasCyclone, "Cyclone Slash");
                    break;
                case "Dash Slash":
                    elem.setData(Offset.hasUpwardSlash, "Dash Slash");
                    break;
                case "Descending Dark":
                    elem.setData(Offset.quakeLevel, "Descending Dark");
                    elem.setMethod(2);
                    break;
                case "Desolate Dive":
                    elem.setData(Offset.quakeLevel, "Desolate Dive");
                    elem.setMethod(1);
                    break;
                case "Dream Nail":
                    elem.setData(Offset.hasDreamNail, "Dream Nail");
                    break;
                case "Dream Nail - Awoken":
                    elem.setData(Offset.dreamNailUpgraded, "Dream Nail - Awoken");
                    break;
                case "Dream Gate":
                    elem.setData(Offset.hasDreamGate, "Dream Gate");
                    break;
                case "Great Slash":
                    elem.setData(Offset.hasDashSlash, "Great Slash");
                    break;
                case "Howling Wraiths":
                    elem.setData(Offset.screamLevel, "Howling Wraiths");
                    elem.setMethod(1);
                    break;
                case "Isma's Tear":
                    elem.setData(Offset.hasAcidArmour, "Isma's Tear");
                    break;
                case "Mantis Claw":
                    elem.setData(Offset.hasWallJump, "Mantis Claw");
                    break;
                case "Monarch Wings":
                    elem.setData(Offset.hasDoubleJump, "Monarch Wings");
                    break;
                case "Mothwing Cloak":
                    elem.setData(Offset.hasDash, "Mothwing Cloak");
                    break;
                case "Shade Cloak":
                    elem.setData(Offset.hasShadowDash, "Shade Cloak");
                    break;
                case "Shade Soul":
                    elem.setData(Offset.fireballLevel, "Shade Soul");
                    elem.setMethod(2);
                    break;
                case "Vengeful Spirit":
                    elem.setData(Offset.fireballLevel, "Vengeful Spirit");
                    elem.setMethod(1);
                    break;










                    #endregion


            }
            return elem;
        }
        #region Vector3F
        [Serializable]
        public class Vector3F
        {
            public PointF position = new PointF(0,0);
            public string sceneName = "None";
            public string previousScene = "None";
            public bool IsSplited = false;
        }
        #endregion

        #region Pantheon
        [Serializable]
        public class Pantheon
        {
            public string Title;
            public bool IsSplited = false;
        }
        #endregion
    }

    [Serializable]
    public class DTHollow
    {
        //Settings Vars
        public bool enableSplitting = false;
        public bool autoTimer = false;
        public bool gameTimer = false;
        public int PantheonMode = 0;
        public int positionMargin = 3;
        //Flags to Split
        public List<DefinitionHollow.ElementToSplitH> bossToSplit = new List<DefinitionHollow.ElementToSplitH>();
        public List<DefinitionHollow.ElementToSplitH> miniBossToSplit = new List<DefinitionHollow.ElementToSplitH>();
        public List<DefinitionHollow.ElementToSplitH> charmToSplit = new List<DefinitionHollow.ElementToSplitH>();
        public List<DefinitionHollow.ElementToSplitH> skillsToSplit = new List<DefinitionHollow.ElementToSplitH>();
        public List<DefinitionHollow.Pantheon> phanteonToSplit = new List<DefinitionHollow.Pantheon>();
        public List<DefinitionHollow.Vector3F> positionToSplit = new List<DefinitionHollow.Vector3F>();

        public List<DefinitionHollow.ElementToSplitH> getBosstoSplit()
        {
            return this.bossToSplit;
        }

        public List<DefinitionHollow.ElementToSplitH> getMiniBossToSplit()
        {
            return this.miniBossToSplit;
        }

        public List<DefinitionHollow.Pantheon> getPhanteonToSplit()
        {
            return this.phanteonToSplit;
        }

        public List<DefinitionHollow.ElementToSplitH> getCharmToSplit()
        {
            return this.charmToSplit;
        }

        public List<DefinitionHollow.ElementToSplitH> getSkillsToSplit()
        {
            return this.skillsToSplit;
        }


        public List<DefinitionHollow.Vector3F> getPositionToSplit()
        {
            return this.positionToSplit;
        }
    }
}
