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
    public class DefinitionsElden
    {
        #region Boss.Elden
        [Serializable]
        public class BossER
        {
            public string Title;
            public uint Id;
            public bool IsSplited = false;
            public string Mode;

        }

        public BossER stringToEnumBoss(string boss)
        {
            BossER cBoss = new BossER();
            switch (boss)
            {
                case "Godrick the Grafted - Stormveil Castle": cBoss.Title = "Godrick the Grafted - Stormveil Castle"; cBoss.Id = 10000800; break;
                case "Margit, the Fell Omen - Stormveil Castle": cBoss.Title = "Margit, the Fell Omen - Stormveil Castle"; cBoss.Id = 10000850; break;
                case "Grafted Scion - Chapel of Anticipation": cBoss.Title = "Grafted Scion - Chapel of Anticipation"; cBoss.Id = 10010800; break;
                case "Morgott, the Omen King - Leyndell": cBoss.Title = "Morgott, the Omen King - Leyndell"; cBoss.Id = 11000800; break;
                case "Godfrey, First Elden Lord - Leyndell": cBoss.Title = "Godfrey, First Elden Lord - Leyndell"; cBoss.Id = 11000850; break;
                case "Hoarah Loux - Leyndell": cBoss.Title = "Hoarah Loux - Leyndell"; cBoss.Id = 11050800; break;
                case "Sir Gideon Ofnir, the All-Knowing - Leyndell": cBoss.Title = "Sir Gideon Ofnir, the All-Knowing - Leyndell"; cBoss.Id = 11050850; break;
                case "Dragonkin Soldier of Nokstella - Ainsel River": cBoss.Title = "Dragonkin Soldier of Nokstella - Ainsel River"; cBoss.Id = 12010800; break;
                case "Dragonkin Soldier - Lake of Rot": cBoss.Title = "Dragonkin Soldier - Lake of Rot"; cBoss.Id = 12010850; break;
                case "Valiant Gargoyles - Siofra River": cBoss.Title = "Valiant Gargoyles - Siofra River"; cBoss.Id = 12020800; break;
                case "Dragonkin Soldier - Siofra River": cBoss.Title = "Dragonkin Soldier - Siofra River"; cBoss.Id = 12020830; break;
                case "Mimic Tear - Siofra River": cBoss.Title = "Mimic Tear - Siofra River"; cBoss.Id = 12020850; break;
                case "Crucible Knight Sirulia - Deeproot Depths": cBoss.Title = "Crucible Knight Sirulia - Deeproot Depths"; cBoss.Id = 12030390; break;
                case "Fia's Champion - Deeproot Depths": cBoss.Title = "Fia's Champion - Deeproot Depths"; cBoss.Id = 12030800; break;
                case "Lichdragon Fortissax - Deeproot Depths": cBoss.Title = "Lichdragon Fortissax - Deeproot Depths"; cBoss.Id = 12030850; break;
                case "Astel, Naturalborn of the Void - Lake of Rot": cBoss.Title = "Astel, Naturalborn of the Void - Lake of Rot"; cBoss.Id = 12040800; break;
                case "Mohg, Lord of Blood - Mohgwyn Palace": cBoss.Title = "Mohg, Lord of Blood - Mohgwyn Palace"; cBoss.Id = 12050800; break;
                case "Ancestor Spirit - Siofra River": cBoss.Title = "Ancestor Spirit - Siofra River"; cBoss.Id = 12080800; break;
                case "Regal Ancestor Spirit - Nokron, Eternal City": cBoss.Title = "Regal Ancestor Spirit - Nokron, Eternal City"; cBoss.Id = 12090800; break;
                case "Maliketh, The Black Blade - Crumbling Farum Azula": cBoss.Title = "Maliketh, The Black Blade - Crumbling Farum Azula"; cBoss.Id = 13000800; break;
                case "Dragonlord Placidusax - Crumbling Farum Azula": cBoss.Title = "Dragonlord Placidusax - Crumbling Farum Azula"; cBoss.Id = 13000830; break;
                case "Godskin Duo - Crumbling Farum Azula": cBoss.Title = "Godskin Duo - Crumbling Farum Azula"; cBoss.Id = 13000850; break;
                case "Rennala, Queen of the Full Moon - Academy of Raya Lucaria": cBoss.Title = "Rennala, Queen of the Full Moon - Academy of Raya Lucaria"; cBoss.Id = 14000800; break;
                case "Red Wolf of Radagon - Academy of Raya Lucaria": cBoss.Title = "Red Wolf of Radagon - Academy of Raya Lucaria"; cBoss.Id = 14000850; break;
                case "Malenia, Blade of Miquella - Miquella's Haligtree": cBoss.Title = "Malenia, Blade of Miquella - Miquella's Haligtree"; cBoss.Id = 15000800; break;
                case "Loretta, Knight of the Haligtree - Miquella's Haligtree": cBoss.Title = "Loretta, Knight of the Haligtree - Miquella's Haligtree"; cBoss.Id = 15000850; break;
                case "Rykard, Lord of Blasphemy - Volcano Manor": cBoss.Title = "Rykard, Lord of Blasphemy - Volcano Manor"; cBoss.Id = 16000800; break;
                case "Godskin Noble - Volcano Manor": cBoss.Title = "Godskin Noble - Volcano Manor"; cBoss.Id = 16000850; break;
                case "Abductor Virgins - Volcano Manor": cBoss.Title = "Abductor Virgins - Volcano Manor"; cBoss.Id = 16000860; break;
                case "Ulcerated Tree Spirit - Stranded Graveyard": cBoss.Title = "Ulcerated Tree Spirit - Stranded Graveyard"; cBoss.Id = 18000800; break;
                case "Soldier of Godrick - Stranded Graveyard": cBoss.Title = "Soldier of Godrick - Stranded Graveyard"; cBoss.Id = 18000850; break;
                case "Elden Beast - Elden Throne": cBoss.Title = "Elden Beast - Elden Throne"; cBoss.Id = 19000800; break;
                case "Mohg, The Omen - Subterranean Shunning-Grounds (Leyndell)": cBoss.Title = "Mohg, The Omen - Subterranean Shunning-Grounds (Leyndell)"; cBoss.Id = 35000800; break;
                case "Esgar, Priest of Blood - Subterranean Shunning-Grounds (Leyndell)": cBoss.Title = "Esgar, Priest of Blood - Subterranean Shunning-Grounds (Leyndell)"; cBoss.Id = 35000850; break;
                case "Magma Wyrm Makar - Ruin-Strewn Precipice (Liurnia)": cBoss.Title = "Magma Wyrm Makar - Ruin-Strewn Precipice (Liurnia)"; cBoss.Id = 39200800; break;
                case "Cemetery Shade - Tombsward Catacombs (Limgrave)": cBoss.Title = "Cemetery Shade - Tombsward Catacombs (Limgrave)"; cBoss.Id = 30000800; break;
                case "Erdtree Burial Watchdog - Impaler's Catacombs (Weeping Penisula)": cBoss.Title = "Erdtree Burial Watchdog - Impaler's Catacombs (Weeping Penisula)"; cBoss.Id = 30010800; break;
                case "Erdtree Burial Watchdog - Stormfoot Catacombs (Limgrave)": cBoss.Title = "Erdtree Burial Watchdog - Stormfoot Catacombs (Limgrave)"; cBoss.Id = 30020800; break;
                case "Black Knife Assassin - Deathtouched Catacombs (Limgrave)": cBoss.Title = "Black Knife Assassin - Deathtouched Catacombs (Limgrave)"; cBoss.Id = 30110800; break;
                case "Grave Warden Duelist - Murkwater Catacombs (Limgrave)": cBoss.Title = "Grave Warden Duelist - Murkwater Catacombs (Limgrave)"; cBoss.Id = 30040800; break;
                case "Cemetery Shade - Black Knife Catacombs (Liurnia)": cBoss.Title = "Cemetery Shade - Black Knife Catacombs (Liurnia)"; cBoss.Id = 30050800; break;
                case "Black Knife Assassin - Black Knife Catacombs (Liurnia)": cBoss.Title = "Black Knife Assassin - Black Knife Catacombs (Liurnia)"; cBoss.Id = 30050850; break;
                case "Spirit-Caller Snail - Road's End Catacombs (Liurnia)": cBoss.Title = "Spirit-Caller Snail - Road's End Catacombs (Liurnia)"; cBoss.Id = 30030800; break;
                case "Erdtree Burial Watchdog - Cliffbottom Catacombs (Liurnia)": cBoss.Title = "Erdtree Burial Watchdog - Cliffbottom Catacombs (Liurnia)"; cBoss.Id = 30060800; break;
                case "Ancient Hero of Zamor - Sainted Hero's Grave (Altus Plateau)": cBoss.Title = "Ancient Hero of Zamor - Sainted Hero's Grave (Altus Plateau)"; cBoss.Id = 30080800; break;
                case "Red Wolf of the Champion - Gelmir Hero's Grave (Mt. Gelmir)": cBoss.Title = "Red Wolf of the Champion - Gelmir Hero's Grave (Mt. Gelmir)"; cBoss.Id = 30090800; break;
                case "Crucible Knight Ordovis - Auriza Hero's Grave (Altus Plateau)": cBoss.Title = "Crucible Knight Ordovis - Auriza Hero's Grave (Altus Plateau)"; cBoss.Id = 30100800; break;
                case "Crucible Knight (Tree Spear) - Auriza Hero's Grave (Altus Plateau)": cBoss.Title = "Crucible Knight (Tree Spear) - Auriza Hero's Grave (Altus Plateau)"; cBoss.Id = 30100800; break;
                case "Misbegotten Warrior - Unsightly Catacombs (Mt. Gelmir)": cBoss.Title = "Misbegotten Warrior - Unsightly Catacombs (Mt. Gelmir)"; cBoss.Id = 30120800; break;
                case "Perfumer Tricia - Unsightly Catacombs (Mt. Gelmir)": cBoss.Title = "Perfumer Tricia - Unsightly Catacombs (Mt. Gelmir)"; cBoss.Id = 30120800; break;
                case "Erdtree Burial Watchdog - Wyndham Catacombs (Altus Plateau)": cBoss.Title = "Erdtree Burial Watchdog - Wyndham Catacombs (Altus Plateau)"; cBoss.Id = 30070800; break;
                case "Grave Warden Duelist - Auriza Side Tomb (Altus Plateau)": cBoss.Title = "Grave Warden Duelist - Auriza Side Tomb (Altus Plateau)"; cBoss.Id = 30130800; break;
                case "Erdtree Burial Watchdog - Minor Erdtree Catacombs (Caelid)": cBoss.Title = "Erdtree Burial Watchdog - Minor Erdtree Catacombs (Caelid)"; cBoss.Id = 30140800; break;
                case "Cemetery Shade - Caelid Catacombs (Caelid)": cBoss.Title = "Cemetery Shade - Caelid Catacombs (Caelid)"; cBoss.Id = 30150800; break;
                case "Putrid Tree Spirit - War-Dead Catacombs (Caelid)": cBoss.Title = "Putrid Tree Spirit - War-Dead Catacombs (Caelid)"; cBoss.Id = 30160800; break;
                case "Ancient Hero of Zamor - Giant-Conquering Hero's Grave (Mountaintops)": cBoss.Title = "Ancient Hero of Zamor - Giant-Conquering Hero's Grave (Mountaintops)"; cBoss.Id = 30170800; break;
                case "Ulcerated Tree Sprit - Giants' Mountaintop Catacombs (Mountaintops)": cBoss.Title = "Ulcerated Tree Sprit - Giants' Mountaintop Catacombs (Mountaintops)"; cBoss.Id = 30180800; break;
                case "Putrid Grave Warden Duelist - Consecrated Snowfield Catacombs (Snowfield)": cBoss.Title = "Putrid Grave Warden Duelist - Consecrated Snowfield Catacombs (Snowfield)"; cBoss.Id = 30190800; break;
                case "Stray Mimic Tear - Hidden Path to the Haligtree": cBoss.Title = "Stray Mimic Tear - Hidden Path to the Haligtree"; cBoss.Id = 30202800; break;
                case "Patches - Murkwater Cave (Limgrave)": cBoss.Title = "Patches - Murkwater Cave (Limgrave)"; cBoss.Id = 31000800; break;
                case "Runebear - Earthbore Cave (Weeping Penisula)": cBoss.Title = "Runebear - Earthbore Cave (Weeping Penisula)"; cBoss.Id = 31010800; break;
                case "Miranda the Blighted Bloom - Tombsward Cave (Limgrave)": cBoss.Title = "Miranda the Blighted Bloom - Tombsward Cave (Limgrave)"; cBoss.Id = 31020800; break;
                case "Beastman of Farum Azula - Groveside Cave (Limgrave)": cBoss.Title = "Beastman of Farum Azula - Groveside Cave (Limgrave)"; cBoss.Id = 31030800; break;
                case "Demi-Human Chief - Coastal Cave (Limgrave)": cBoss.Title = "Demi-Human Chief - Coastal Cave (Limgrave)"; cBoss.Id = 31150800; break;
                case "Guardian Golem - Highroad Cave (Limgrave)": cBoss.Title = "Guardian Golem - Highroad Cave (Limgrave)"; cBoss.Id = 31170800; break;
                case "Cleanrot Knight - Stillwater Cave (Liurnia)": cBoss.Title = "Cleanrot Knight - Stillwater Cave (Liurnia)"; cBoss.Id = 31040800; break;
                case "Bloodhound Knight - Lakeside Crystal Cave (Liurnia)": cBoss.Title = "Bloodhound Knight - Lakeside Crystal Cave (Liurnia)"; cBoss.Id = 31050800; break;
                case "Crystalians - Academy Crystal Cave (Liurnia)": cBoss.Title = "Crystalians - Academy Crystal Cave (Liurnia)"; cBoss.Id = 31060800; break;
                case "Kindred of Rot - Seethewater Cave (Mt. Gelmir)": cBoss.Title = "Kindred of Rot - Seethewater Cave (Mt. Gelmir)"; cBoss.Id = 31070800; break;
                case "Demi-Human Queen Margot - Volcano Cave (Mt. Gelmir)": cBoss.Title = "Demi-Human Queen Margot - Volcano Cave (Mt. Gelmir)"; cBoss.Id = 31090800; break;
                case "Miranda the Blighted Bloom - Perfumer's Grotto (Altus Plateau)": cBoss.Title = "Miranda the Blighted Bloom - Perfumer's Grotto (Altus Plateau)"; cBoss.Id = 31180800; break;
                case "Black Knife Assassin - Sage's Cave (Altus Plateau)": cBoss.Title = "Black Knife Assassin - Sage's Cave (Altus Plateau)"; cBoss.Id = 31190800; break;
                case "Necromancer Garris - Sage's Cave (Altus Plateau)": cBoss.Title = "Necromancer Garris - Sage's Cave (Altus Plateau)"; cBoss.Id = 31190850; break;
                case "Frenzied Duelist - Gaol Cave (Caelid)": cBoss.Title = "Frenzied Duelist - Gaol Cave (Caelid)"; cBoss.Id = 31210800; break;
                case "Beastman of Farum Azula - Dragonbarrow Cave (Dragonbarrow)": cBoss.Title = "Beastman of Farum Azula - Dragonbarrow Cave (Dragonbarrow)"; cBoss.Id = 31100800; break;
                case "Cleanrot Knight - Abandoned Cave (Caelid)": cBoss.Title = "Cleanrot Knight - Abandoned Cave (Caelid)"; cBoss.Id = 31200800; break;
                case "Putrid Crystalians - Sellia Hideaway (Caelid)": cBoss.Title = "Putrid Crystalians - Sellia Hideaway (Caelid)"; cBoss.Id = 31110800; break;
                case "Misbegotten Crusader - Cave of the Forlorn (Mountaintops)": cBoss.Title = "Misbegotten Crusader - Cave of the Forlorn (Mountaintops)"; cBoss.Id = 31120800; break;
                case "Spirit-Caller Snail - Spiritcaller's Cave (Mountaintops)": cBoss.Title = "Spirit-Caller Snail - Spiritcaller's Cave (Mountaintops)"; cBoss.Id = 31220800; break;
                case "Scaly Misbegotten - Morne Tunnel (Weeping Penisula)": cBoss.Title = "Scaly Misbegotten - Morne Tunnel (Weeping Penisula)"; cBoss.Id = 32000800; break;
                case "Stonedigger Troll - Limgrave Tunnels (Limgrave)": cBoss.Title = "Stonedigger Troll - Limgrave Tunnels (Limgrave)"; cBoss.Id = 32010800; break;
                case "Crystalian (Ringblade) - Raya Lucaria Crystal Tunnel (Liurnia)": cBoss.Title = "Crystalian (Ringblade) - Raya Lucaria Crystal Tunnel (Liurnia)"; cBoss.Id = 32020800; break;
                case "Stonedigger Troll - Old Altus Tunnel (Altus Plateau)": cBoss.Title = "Stonedigger Troll - Old Altus Tunnel (Altus Plateau)"; cBoss.Id = 32040800; break;
                case "Onyx Lord - Divine Tower of West Altus (Altus Plateau)": cBoss.Title = "Onyx Lord - Divine Tower of West Altus (Altus Plateau)"; cBoss.Id = 34120800; break;
                case "Crystalian (Ringblade) - Altus Tunnel (Altus Plateau)": cBoss.Title = "Crystalian (Ringblade) - Altus Tunnel (Altus Plateau)"; cBoss.Id = 32050800; break;
                case "Crystalian (Spear) - Altus Tunnel (Altus Plateau)": cBoss.Title = "Crystalian (Spear) - Altus Tunnel (Altus Plateau)"; cBoss.Id = 32050800; break;
                case "Magma Wyrm - Gael Tunnel (Caelid)": cBoss.Title = "Magma Wyrm - Gael Tunnel (Caelid)"; cBoss.Id = 32070800; break;
                case "Fallingstar Beast - Sellia Crystal Tunnel (Caelid)": cBoss.Title = "Fallingstar Beast - Sellia Crystal Tunnel (Caelid)"; cBoss.Id = 32080800; break;
                case "Astel, Stars of Darkness - Yelough Anix Tunnel (Snowfield)": cBoss.Title = "Astel, Stars of Darkness - Yelough Anix Tunnel (Snowfield)"; cBoss.Id = 32110800; break;
                case "Godskin Apostle - Divine Tower of Caelid (Caelid)": cBoss.Title = "Godskin Apostle - Divine Tower of Caelid (Caelid)"; cBoss.Id = 34130800; break;
                case "Fell Twins - Divine Tower of East Altus (Capital Outskirts)": cBoss.Title = "Fell Twins - Divine Tower of East Altus (Capital Outskirts)"; cBoss.Id = 34140850; break;
                case "Mad Pumpkin Head - Waypoint Ruins (Limgrave)": cBoss.Title = "Mad Pumpkin Head - Waypoint Ruins (Limgrave)"; cBoss.Id = 1044360800; break;
                case "Night's Cavalry - Agheel Lake North (Limgrave)": cBoss.Title = "Night's Cavalry - Agheel Lake North (Limgrave)"; cBoss.Id = 1043370800; break;
                case "Death Rite Bird - Stormgate (Limgrave)": cBoss.Title = "Death Rite Bird - Stormgate (Limgrave)"; cBoss.Id = 1042380800; break;
                case "Ball-Bearing Hunter - Warmaster's Shack (Limgrave)": cBoss.Title = "Ball-Bearing Hunter - Warmaster's Shack (Limgrave)"; cBoss.Id = 1042380850; break;
                case "Ancient Hero of Zamor - Weeping Evergaol (Weeping Penisula)": cBoss.Title = "Ancient Hero of Zamor - Weeping Evergaol (Weeping Penisula)"; cBoss.Id = 1042330800; break;
                case "Bloodhound Knight Darriwill - Forlorn Hound Evergaol (Limgrave)": cBoss.Title = "Bloodhound Knight Darriwill - Forlorn Hound Evergaol (Limgrave)"; cBoss.Id = 1044350800; break;
                case "Crucible Knight - Stormhill Evergaol (Limgrave)": cBoss.Title = "Crucible Knight - Stormhill Evergaol (Limgrave)"; cBoss.Id = 1042370800; break;
                case "Erdtree Avatar - Minor Erdtree (Weeping Penisula)": cBoss.Title = "Erdtree Avatar - Minor Erdtree (Weeping Penisula)"; cBoss.Id = 1043330800; break;
                case "Night's Cavalry - Castle Morne Approach (Weeping Penisula)": cBoss.Title = "Night's Cavalry - Castle Morne Approach (Weeping Penisula)"; cBoss.Id = 1044320850; break;
                case "Death Rite Bird - Castle Morne Approach (Weeping Penisula)": cBoss.Title = "Death Rite Bird - Castle Morne Approach (Weeping Penisula)"; cBoss.Id = 1044320800; break;
                case "Leonine Misbegotten - Castle Morne (Weeping Penisula)": cBoss.Title = "Leonine Misbegotten - Castle Morne (Weeping Penisula)"; cBoss.Id = 1043300800; break;
                case "Tree Sentinel - Church of Elleh (Limgrave)": cBoss.Title = "Tree Sentinel - Church of Elleh (Limgrave)"; cBoss.Id = 1042360800; break;
                case "Flying Dragon Agheel - Dragon-Burnt Ruins (Limgrave)": cBoss.Title = "Flying Dragon Agheel - Dragon-Burnt Ruins (Limgrave)"; cBoss.Id = 1043360800; break;
                case "Tibia Mariner - Summonwater Village (Limgrave)": cBoss.Title = "Tibia Mariner - Summonwater Village (Limgrave)"; cBoss.Id = 1045390800; break;
                case "Royal Revenant - Kingsrealm Ruins (Liurnia)": cBoss.Title = "Royal Revenant - Kingsrealm Ruins (Liurnia)"; cBoss.Id = 1034480800; break;
                case "Adan, Thief of Fire - Malefactor's Evergaol (Liurnia)": cBoss.Title = "Adan, Thief of Fire - Malefactor's Evergaol (Liurnia)"; cBoss.Id = 1038410800; break;
                case "Bols, Carian Knight - Cuckoo's Evergaol (Liurnia)": cBoss.Title = "Bols, Carian Knight - Cuckoo's Evergaol (Liurnia)"; cBoss.Id = 1033450800; break;
                case "Onyx Lord - Royal Grave Evergaol (Liurnia)": cBoss.Title = "Onyx Lord - Royal Grave Evergaol (Liurnia)"; cBoss.Id = 1036500800; break;
                case "Alecto, Black Knife Ringleader - Moonlight Altar (Liurnia)": cBoss.Title = "Alecto, Black Knife Ringleader - Moonlight Altar (Liurnia)"; cBoss.Id = 1033420800; break;
                case "Erdtree Avatar - Revenger's Shack (Liurnia)": cBoss.Title = "Erdtree Avatar - Revenger's Shack (Liurnia)"; cBoss.Id = 1033430800; break;
                case "Erdtree Avatar - Minor Erdtree (Liurnia)": cBoss.Title = "Erdtree Avatar - Minor Erdtree (Liurnia)"; cBoss.Id = 1038480800; break;
                case "Royal Knight Loretta - Carian Manor (Liurnia)": cBoss.Title = "Royal Knight Loretta - Carian Manor (Liurnia)"; cBoss.Id = 1035500800; break;
                case "Ball-Bearing Hunter - Church of Vows (Liurnia)": cBoss.Title = "Ball-Bearing Hunter - Church of Vows (Liurnia)"; cBoss.Id = 1037460800; break;
                case "Night's Cavalry - Liurnia Highway Far North (Liurnia)": cBoss.Title = "Night's Cavalry - Liurnia Highway Far North (Liurnia)"; cBoss.Id = 1039430800; break;
                case "Night's Cavalry - East Raya Lucaria Gate (Liurnia)": cBoss.Title = "Night's Cavalry - East Raya Lucaria Gate (Liurnia)"; cBoss.Id = 1036480800; break;
                case "Deathbird - Laskyar Ruins (Liurnia)": cBoss.Title = "Deathbird - Laskyar Ruins (Liurnia)"; cBoss.Id = 1037420800; break;
                case "Death Rite Bird - Gate Town Northwest (Liurnia)": cBoss.Title = "Death Rite Bird - Gate Town Northwest (Liurnia)"; cBoss.Id = 1036450800; break;
                case "Glintstone Dragon Smarag - Meeting Place (Liurnia)": cBoss.Title = "Glintstone Dragon Smarag - Meeting Place (Liurnia)"; cBoss.Id = 1034450800; break;
                case "Glintstone Dragon Adula - Ranni's Rise (Liurnia)": cBoss.Title = "Glintstone Dragon Adula - Ranni's Rise (Liurnia)"; cBoss.Id = 1034500800; break;
                case "Glintstone Dragon Adula - Moonfolk Ruins (Liurnia)": cBoss.Title = "Glintstone Dragon Adula - Moonfolk Ruins (Liurnia)"; cBoss.Id = 1034420800; break;
                case "Omenkiller - Village of the Albinaurics (Liurnia)": cBoss.Title = "Omenkiller - Village of the Albinaurics (Liurnia)"; cBoss.Id = 1035420800; break;
                case "Tibia Mariner - Jarburg (Liurnia)": cBoss.Title = "Tibia Mariner - Jarburg (Liurnia)"; cBoss.Id = 1039440800; break;
                case "Ancient Dragon Lansseax - Abandoned Coffin (Altus Plateau)": cBoss.Title = "Ancient Dragon Lansseax - Abandoned Coffin (Altus Plateau)"; cBoss.Id = 1037510800; break;
                case "Ancient Dragon Lansseax - Rampartside Path (Altus Plateau)": cBoss.Title = "Ancient Dragon Lansseax - Rampartside Path (Altus Plateau)"; cBoss.Id = 1041520800; break;
                case "Demi-Human Queen - Lux Ruins (Altus Plateau)": cBoss.Title = "Demi-Human Queen - Lux Ruins (Altus Plateau)"; cBoss.Id = 1038510800; break;
                case "Fallingstar Beast - South of Tree Sentinel Duo (Altus Plateau)": cBoss.Title = "Fallingstar Beast - South of Tree Sentinel Duo (Altus Plateau)"; cBoss.Id = 1041500800; break;
                case "Sanguine Noble - Writheblood Ruins (Altus Plateau)": cBoss.Title = "Sanguine Noble - Writheblood Ruins (Altus Plateau)"; cBoss.Id = 1040530800; break;
                case "Tree Sentinel - Tree Sentinel Duo (Altus Plateau)": cBoss.Title = "Tree Sentinel - Tree Sentinel Duo (Altus Plateau)"; cBoss.Id = 1041510800; break;
                case "Godskin Apostle - Windmill Heights (Altus Plateau)": cBoss.Title = "Godskin Apostle - Windmill Heights (Altus Plateau)"; cBoss.Id = 1042550800; break;
                case "Black Knife Assassin - Sainted Hero's Grave Entrance (Altus Plateau)": cBoss.Title = "Black Knife Assassin - Sainted Hero's Grave Entrance (Altus Plateau)"; cBoss.Id = 1040520800; break;
                case "Draconic Tree Sentinel - Capital Rampart (Capital Outskirts)": cBoss.Title = "Draconic Tree Sentinel - Capital Rampart (Capital Outskirts)"; cBoss.Id = 1045520800; break;
                case "Godefroy the Grafted - Golden Lineage Evergaol (Altus Plateau)": cBoss.Title = "Godefroy the Grafted - Golden Lineage Evergaol (Altus Plateau)"; cBoss.Id = 1039500800; break;
                case "Wormface - Woodfolk Ruins (Altus Plateau)": cBoss.Title = "Wormface - Woodfolk Ruins (Altus Plateau)"; cBoss.Id = 1041530800; break;
                case "Night's Cavalry - Altus Highway Junction (Altus Plateau)": cBoss.Title = "Night's Cavalry - Altus Highway Junction (Altus Plateau)"; cBoss.Id = 1043530800; break;
                case "Death Rite Bird - Minor Erdtree (Capital Outskirts)": cBoss.Title = "Death Rite Bird - Minor Erdtree (Capital Outskirts)"; cBoss.Id = 1043530800; break;
                case "Ball-Bearing Hunter - Hermit Merchant's Shack (Capital Outskirts)": cBoss.Title = "Ball-Bearing Hunter - Hermit Merchant's Shack (Capital Outskirts)"; cBoss.Id = 1043530800; break;
                case "Demi-Human Queen - Primeval Sorcerer Azur (Mt. Gelmir)": cBoss.Title = "Demi-Human Queen - Primeval Sorcerer Azur (Mt. Gelmir)"; cBoss.Id = 1037530800; break;
                case "Magma Wyrm - Seethewater Terminus (Mt. Gelmir)": cBoss.Title = "Magma Wyrm - Seethewater Terminus (Mt. Gelmir)"; cBoss.Id = 1035530800; break;
                case "Full-Grown Fallingstar Beast - Crater (Mt. Gelmir)": cBoss.Title = "Full-Grown Fallingstar Beast - Crater (Mt. Gelmir)"; cBoss.Id = 1036540800; break;
                case "Elemer of the Briar - Shaded Castle (Altus Plateau)": cBoss.Title = "Elemer of the Briar - Shaded Castle (Altus Plateau)"; cBoss.Id = 1039540800; break;
                case "Ulcerated Tree Spirit - Minor Erdtree (Mt. Gelmir)": cBoss.Title = "Ulcerated Tree Spirit - Minor Erdtree (Mt. Gelmir)"; cBoss.Id = 1037540810; break;
                case "Tibia Mariner - Wyndham Ruins (Altus Plateau)": cBoss.Title = "Tibia Mariner - Wyndham Ruins (Altus Plateau)"; cBoss.Id = 1038520800; break;
                case "Putrid Avatar - Minor Erdtree (Caelid)": cBoss.Title = "Putrid Avatar - Minor Erdtree (Caelid)"; cBoss.Id = 1047400800; break;
                case "Decaying Ekzykes - Caelid Highway South (Caelid)": cBoss.Title = "Decaying Ekzykes - Caelid Highway South (Caelid)"; cBoss.Id = 1048370800; break;
                case "Monstrous Dog - Southwest of Caelid Highway South (Caelid)": cBoss.Title = "Monstrous Dog - Southwest of Caelid Highway South (Caelid)"; cBoss.Id = 1048400800; break;
                case "Night's Cavalry - Southern Aeonia Swamp Bank (Caelid)": cBoss.Title = "Night's Cavalry - Southern Aeonia Swamp Bank (Caelid)"; cBoss.Id = 1049370800; break;
                case "Death Rite Bird - Southern Aeonia Swamp Bank (Caelid)": cBoss.Title = "Death Rite Bird - Southern Aeonia Swamp Bank (Caelid)"; cBoss.Id = 1049370850; break;
                case "Commander O'Neil - East Aeonia Swamp (Caelid)": cBoss.Title = "Commander O'Neil - East Aeonia Swamp (Caelid)"; cBoss.Id = 1049380800; break;
                case "Crucible Knight - Redmane Castle (Caelid)": cBoss.Title = "Crucible Knight - Redmane Castle (Caelid)"; cBoss.Id = 1051360800; break;
                case "Starscourge Radahn - Battlefield (Caelid)": cBoss.Title = "Starscourge Radahn - Battlefield (Caelid)"; cBoss.Id = 1252380800; break;
                case "Nox Priest - West Sellia (Caelid)": cBoss.Title = "Nox Priest - West Sellia (Caelid)"; cBoss.Id = 1049390800; break;
                case "Bell-Bearing Hunter - Isolated Merchant's Shack (Dragonbarrow)": cBoss.Title = "Bell-Bearing Hunter - Isolated Merchant's Shack (Dragonbarrow)"; cBoss.Id = 1048410800; break;
                case "Battlemage Hugues - Sellia Crystal Tunnel Entrance (Caelid)": cBoss.Title = "Battlemage Hugues - Sellia Crystal Tunnel Entrance (Caelid)"; cBoss.Id = 1049390850; break;
                case "Putrid Avatar - Dragonbarrow Fork (Caelid)": cBoss.Title = "Putrid Avatar - Dragonbarrow Fork (Caelid)"; cBoss.Id = 1051400800; break;
                case "Flying Dragon Greyll - Dragonbarrow (Caelid)": cBoss.Title = "Flying Dragon Greyll - Dragonbarrow (Caelid)"; cBoss.Id = 1052410800; break;
                case "Night's Cavalry - Dragonbarrow (Caelid)": cBoss.Title = "Night's Cavalry - Dragonbarrow (Caelid)"; cBoss.Id = 1052410850; break;
                case "Black Blade Kindred - Bestial Sanctum (Caelid)": cBoss.Title = "Black Blade Kindred - Bestial Sanctum (Caelid)"; cBoss.Id = 1051430800; break;
                case "Night's Cavalry - Forbidden Lands (Mountaintops)": cBoss.Title = "Night's Cavalry - Forbidden Lands (Mountaintops)"; cBoss.Id = 1048510800; break;
                case "Black Blade Kindred - Before Grand Lift of Rold (Mountaintops)": cBoss.Title = "Black Blade Kindred - Before Grand Lift of Rold (Mountaintops)"; cBoss.Id = 1049520800; break;
                case "Borealis the Freezing Fog - Freezing Fields (Mountaintops)": cBoss.Title = "Borealis the Freezing Fog - Freezing Fields (Mountaintops)"; cBoss.Id = 1254560800; break;
                case "Roundtable Knight Vyke - Lord Contender's Evergaol (Mountaintops)": cBoss.Title = "Roundtable Knight Vyke - Lord Contender's Evergaol (Mountaintops)"; cBoss.Id = 1053560800; break;
                case "Fire Giant - Giant's Forge (Mountaintops)": cBoss.Title = "Fire Giant - Giant's Forge (Mountaintops)"; cBoss.Id = 1052520800; break;
                case "Erdtree Avatar - Minor Erdtree (Mountaintops)": cBoss.Title = "Erdtree Avatar - Minor Erdtree (Mountaintops)"; cBoss.Id = 1052560800; break;
                case "Death Rite Bird - West of Castle So (Mountaintops)": cBoss.Title = "Death Rite Bird - West of Castle So (Mountaintops)"; cBoss.Id = 1050570800; break;
                case "Putrid Avatar - Minor Erdtree (Snowfield)": cBoss.Title = "Putrid Avatar - Minor Erdtree (Snowfield)"; cBoss.Id = 1050570850; break;
                case "Commander Niall - Castle Soul (Mountaintops)": cBoss.Title = "Commander Niall - Castle Soul (Mountaintops)"; cBoss.Id = 1051570800; break;
                case "Great Wyrm Theodorix - Albinauric Rise (Mountaintops)": cBoss.Title = "Great Wyrm Theodorix - Albinauric Rise (Mountaintops)"; cBoss.Id = 1050560800; break;
                case "Night's Cavalry - Sourthwest (Mountaintops)": cBoss.Title = "Night's Cavalry - Sourthwest (Mountaintops)"; cBoss.Id = 1248550800; break;
                case "Death Rite Bird - Ordina, Liturgical Town (Snowfield)": cBoss.Title = "Death Rite Bird - Ordina, Liturgical Town (Snowfield)"; cBoss.Id = 1048570800; break;
                default: cBoss = null; break;
            }
            return cBoss;
        }


        #endregion
        #region Grace.Elden
        [Serializable]
        public class Grace
        {
            public string Title;
            public uint Id;
            public bool IsSplited = false;
            public string Mode;
        }

        public Grace stringToGraceEnum(string grace)
        {
            Grace cGrace = new Grace();
            switch (grace)
            {
                case "Raya Lucaria Grand Library": cGrace.Title = "Raya Lucaria Grand Library"; cGrace.Id = 71400; break;
                case "Debate Parlor": cGrace.Title = "Debate Parlor"; cGrace.Id = 71401; break;
                case "Church of the Cuckoo": cGrace.Title = "Church of the Cuckoo"; cGrace.Id = 71402; break;
                case "Schoolhouse Classroom": cGrace.Title = "Schoolhouse Classroom"; cGrace.Id = 71403; break;
                case "Dragonkin Soldier of Nokstella": cGrace.Title = "Dragonkin Soldier of Nokstella"; cGrace.Id = 71210; break;
                case "Ainsel River Well Depths": cGrace.Title = "Ainsel River Well Depths"; cGrace.Id = 71211; break;
                case "Ainsel River Sluice Gate": cGrace.Title = "Ainsel River Sluice Gate"; cGrace.Id = 71212; break;
                case "Ainsel River Downstream": cGrace.Title = "Ainsel River Downstream"; cGrace.Id = 71213; break;
                case "Astel, Naturalborn of the Void": cGrace.Title = "Astel, Naturalborn of the Void"; cGrace.Id = 71240; break;
                case "Ainsel River Main": cGrace.Title = "Ainsel River Main"; cGrace.Id = 71214; break;
                case "Nokstella, Eternal City": cGrace.Title = "Nokstella, Eternal City"; cGrace.Id = 71215; break;
                case "Nokstella Waterfall Basin": cGrace.Title = "Nokstella Waterfall Basin"; cGrace.Id = 71219; break;
                case "Sainted Hero's Grave": cGrace.Title = "Sainted Hero's Grave"; cGrace.Id = 73008; break;
                case "Unsightly Catacombs": cGrace.Title = "Unsightly Catacombs"; cGrace.Id = 73012; break;
                case "Perfumer's Grotto": cGrace.Title = "Perfumer's Grotto"; cGrace.Id = 73118; break;
                case "Sage's Cave": cGrace.Title = "Sage's Cave"; cGrace.Id = 73119; break;
                case "Old Altus Tunnel": cGrace.Title = "Old Altus Tunnel"; cGrace.Id = 73204; break;
                case "Altus Tunnel": cGrace.Title = "Altus Tunnel"; cGrace.Id = 73205; break;
                case "Abandoned Coffin": cGrace.Title = "Abandoned Coffin"; cGrace.Id = 76300; break;
                case "Altus Plateau": cGrace.Title = "Altus Plateau"; cGrace.Id = 76301; break;
                case "Erdtree-Gazing Hill": cGrace.Title = "Erdtree-Gazing Hill"; cGrace.Id = 76302; break;
                case "Altus Highway Junction": cGrace.Title = "Altus Highway Junction"; cGrace.Id = 76303; break;
                case "Forest-Spanning Greatbridge": cGrace.Title = "Forest-Spanning Greatbridge"; cGrace.Id = 76304; break;
                case "Rampartside Path": cGrace.Title = "Rampartside Path"; cGrace.Id = 76305; break;
                case "Bower of Bounty": cGrace.Title = "Bower of Bounty"; cGrace.Id = 76306; break;
                case "Road of Iniquity Side Path": cGrace.Title = "Road of Iniquity Side Path"; cGrace.Id = 76307; break;
                case "Windmill Village": cGrace.Title = "Windmill Village"; cGrace.Id = 76308; break;
                case "Windmill Heights": cGrace.Title = "Windmill Heights"; cGrace.Id = 76313; break;
                case "Shaded Castle Ramparts": cGrace.Title = "Shaded Castle Ramparts"; cGrace.Id = 76320; break;
                case "Shaded Castle Inner Gate": cGrace.Title = "Shaded Castle Inner Gate"; cGrace.Id = 76321; break;
                case "Castellan's Hall": cGrace.Title = "Castellan's Hall"; cGrace.Id = 76322; break;
                case "East Raya Lucaria Gate": cGrace.Title = "East Raya Lucaria Gate"; cGrace.Id = 76207; break;
                case "Bellum Church": cGrace.Title = "Bellum Church"; cGrace.Id = 76208; break;
                case "Frenzied Flame Village Outskirts": cGrace.Title = "Frenzied Flame Village Outskirts"; cGrace.Id = 76239; break;
                case "Church of Inhibition": cGrace.Title = "Church of Inhibition"; cGrace.Id = 76240; break;
                case "Minor Erdtree Catacombs": cGrace.Title = "Minor Erdtree Catacombs"; cGrace.Id = 73014; break;
                case "Caelid Catacombs": cGrace.Title = "Caelid Catacombs"; cGrace.Id = 73015; break;
                case "War-Dead Catacombs": cGrace.Title = "War-Dead Catacombs"; cGrace.Id = 73016; break;
                case "Abandoned Cave": cGrace.Title = "Abandoned Cave"; cGrace.Id = 73120; break;
                case "Gaol Cave": cGrace.Title = "Gaol Cave"; cGrace.Id = 73121; break;
                case "Gael Tunnel": cGrace.Title = "Gael Tunnel"; cGrace.Id = 73207; break;
                case "Rear Gael Tunnel Entrance": cGrace.Title = "Rear Gael Tunnel Entrance"; cGrace.Id = 73207; break;
                case "Sellia Crystal Tunnel": cGrace.Title = "Sellia Crystal Tunnel"; cGrace.Id = 73208; break;
                case "Smoldering Church": cGrace.Title = "Smoldering Church"; cGrace.Id = 76400; break;
                case "Rotview Balcony": cGrace.Title = "Rotview Balcony"; cGrace.Id = 76401; break;
                case "Fort Gael North": cGrace.Title = "Fort Gael North"; cGrace.Id = 76402; break;
                case "Caelem Ruins": cGrace.Title = "Caelem Ruins"; cGrace.Id = 76403; break;
                case "Cathedral of Dragon Communion": cGrace.Title = "Cathedral of Dragon Communion"; cGrace.Id = 76404; break;
                case "Caelid Highway South": cGrace.Title = "Caelid Highway South"; cGrace.Id = 76405; break;
                case "Smoldering Wall": cGrace.Title = "Smoldering Wall"; cGrace.Id = 76409; break;
                case "Deep Siofra Well": cGrace.Title = "Deep Siofra Well"; cGrace.Id = 76410; break;
                case "Southern Aeonia Swamp Bank": cGrace.Title = "Southern Aeonia Swamp Bank"; cGrace.Id = 76411; break;
                case "Sellia Backstreets": cGrace.Title = "Sellia Backstreets"; cGrace.Id = 76414; break;
                case "Chair-Crypt of Sellia": cGrace.Title = "Chair-Crypt of Sellia"; cGrace.Id = 76415; break;
                case "Sellia Under-Stair": cGrace.Title = "Sellia Under-Stair"; cGrace.Id = 76416; break;
                case "Impassable Greatbridge": cGrace.Title = "Impassable Greatbridge"; cGrace.Id = 76417; break;
                case "Church of the Plague": cGrace.Title = "Church of the Plague"; cGrace.Id = 76418; break;
                case "Redmane Castle Plaza": cGrace.Title = "Redmane Castle Plaza"; cGrace.Id = 76419; break;
                case "Chamber Outside the Plaza": cGrace.Title = "Chamber Outside the Plaza"; cGrace.Id = 76420; break;
                case "Starscourge Radahn": cGrace.Title = "Starscourge Radahn"; cGrace.Id = 76422; break;
                case "Auriza Hero's Grave": cGrace.Title = "Auriza Hero's Grave"; cGrace.Id = 73010; break;
                case "Auriza Side Tomb": cGrace.Title = "Auriza Side Tomb"; cGrace.Id = 73013; break;
                case "Divine Tower of West Altus": cGrace.Title = "Divine Tower of West Altus"; cGrace.Id = 73430; break;
                case "Sealed Tunnel": cGrace.Title = "Sealed Tunnel"; cGrace.Id = 73431; break;
                case "Divine Tower of West Altus: Gate": cGrace.Title = "Divine Tower of West Altus: Gate"; cGrace.Id = 73432; break;
                case "Outer Wall Phantom Tree": cGrace.Title = "Outer Wall Phantom Tree"; cGrace.Id = 76309; break;
                case "Minor Erdtree Church": cGrace.Title = "Minor Erdtree Church"; cGrace.Id = 76310; break;
                case "Hermit Merchant's Shack": cGrace.Title = "Hermit Merchant's Shack"; cGrace.Id = 76311; break;
                case "Outer Wall Battleground": cGrace.Title = "Outer Wall Battleground"; cGrace.Id = 76312; break;
                case "Capital Rampart": cGrace.Title = "Capital Rampart"; cGrace.Id = 76314; break;
                case "Consecrated Snowfield Catacombs": cGrace.Title = "Consecrated Snowfield Catacombs"; cGrace.Id = 73019; break;
                case "Cave of the Forlorn": cGrace.Title = "Cave of the Forlorn"; cGrace.Id = 73112; break;
                case "Yelough Anix Tunnel": cGrace.Title = "Yelough Anix Tunnel"; cGrace.Id = 73211; break;
                case "Consecrated Snowfield": cGrace.Title = "Consecrated Snowfield"; cGrace.Id = 76550; break;
                case "Inner Consecrated Snowfield": cGrace.Title = "Inner Consecrated Snowfield"; cGrace.Id = 76551; break;
                case "Ordina, Liturgical Town": cGrace.Title = "Ordina, Liturgical Town"; cGrace.Id = 76652; break;
                case "Apostate Derelict": cGrace.Title = "Apostate Derelict"; cGrace.Id = 76653; break;
                case "Maliketh, the Black Blade": cGrace.Title = "Maliketh, the Black Blade"; cGrace.Id = 71300; break;
                case "Dragonlord Placidusax": cGrace.Title = "Dragonlord Placidusax"; cGrace.Id = 71301; break;
                case "Dragon Temple Altar": cGrace.Title = "Dragon Temple Altar"; cGrace.Id = 71302; break;
                case "Crumbling Beast Grave": cGrace.Title = "Crumbling Beast Grave"; cGrace.Id = 71303; break;
                case "Crumbling Beast Grave Depths": cGrace.Title = "Crumbling Beast Grave Depths"; cGrace.Id = 71304; break;
                case "Tempest-Facing Balcony": cGrace.Title = "Tempest-Facing Balcony"; cGrace.Id = 71305; break;
                case "Dragon Temple": cGrace.Title = "Dragon Temple"; cGrace.Id = 71306; break;
                case "Dragon Temple Transept": cGrace.Title = "Dragon Temple Transept"; cGrace.Id = 71307; break;
                case "Dragon Temple Lift": cGrace.Title = "Dragon Temple Lift"; cGrace.Id = 71308; break;
                case "Dragon Temple Rooftop": cGrace.Title = "Dragon Temple Rooftop"; cGrace.Id = 71309; break;
                case "Beside the Great Bridge": cGrace.Title = "Beside the Great Bridge"; cGrace.Id = 71310; break;
                case "Prince of Death's Throne": cGrace.Title = "Prince of Death's Throne"; cGrace.Id = 71230; break;
                case "Root-Facing Cliffs": cGrace.Title = "Root-Facing Cliffs"; cGrace.Id = 71231; break;
                case "Great Waterfall Crest": cGrace.Title = "Great Waterfall Crest"; cGrace.Id = 71232; break;
                case "Deeproot Depths": cGrace.Title = "Deeproot Depths"; cGrace.Id = 71233; break;
                case "The Nameless Eternal City": cGrace.Title = "The Nameless Eternal City"; cGrace.Id = 71234; break;
                case "Across the Roots": cGrace.Title = "Across the Roots"; cGrace.Id = 71235; break;
                case "Fractured Marika": cGrace.Title = "Fractured Marika"; cGrace.Id = 71900; break;
                case "Malenia, Goddess of Rot": cGrace.Title = "Malenia, Goddess of Rot"; cGrace.Id = 71500; break;
                case "Prayer Room": cGrace.Title = "Prayer Room"; cGrace.Id = 71501; break;
                case "Elphael Inner Wall": cGrace.Title = "Elphael Inner Wall"; cGrace.Id = 71502; break;
                case "Drainage Channel": cGrace.Title = "Drainage Channel"; cGrace.Id = 71503; break;
                case "Haligtree Roots": cGrace.Title = "Haligtree Roots"; cGrace.Id = 71504; break;
                case "Giant-Conquering Hero's Grave": cGrace.Title = "Giant-Conquering Hero's Grave"; cGrace.Id = 73017; break;
                case "Giants' Mountaintop Catacombs": cGrace.Title = "Giants' Mountaintop Catacombs"; cGrace.Id = 73018; break;
                case "Giants' Gravepost": cGrace.Title = "Giants' Gravepost"; cGrace.Id = 76506; break;
                case "Church of Repose": cGrace.Title = "Church of Repose"; cGrace.Id = 76507; break;
                case "Foot of the Forge": cGrace.Title = "Foot of the Forge"; cGrace.Id = 76508; break;
                case "Fire Giant": cGrace.Title = "Fire Giant"; cGrace.Id = 76509; break;
                case "Forge of the Giants": cGrace.Title = "Forge of the Giants"; cGrace.Id = 76510; break;
                case "Hidden Path to the Haligtree": cGrace.Title = "Hidden Path to the Haligtree"; cGrace.Id = 73020; break;
                case "Divine Tower of East Altus: Gate": cGrace.Title = "Divine Tower of East Altus: Gate"; cGrace.Id = 73450; break;
                case "Divine Tower of East Altus": cGrace.Title = "Divine Tower of East Altus"; cGrace.Id = 73451; break;
                case "Forbidden Lands": cGrace.Title = "Forbidden Lands"; cGrace.Id = 76500; break;
                case "Grand Lift of Rold": cGrace.Title = "Grand Lift of Rold"; cGrace.Id = 76502; break;
                case "Dragonbarrow Cave": cGrace.Title = "Dragonbarrow Cave"; cGrace.Id = 73110; break;
                case "Sellia Hideaway": cGrace.Title = "Sellia Hideaway"; cGrace.Id = 73111; break;
                case "Divine Tower of Caelid": cGrace.Title = "Divine Tower of Caelid"; cGrace.Id = 73440; break;
                case "Divine Tower of Caelid: Center": cGrace.Title = "Divine Tower of Caelid: Center"; cGrace.Id = 73441; break;
                case "Isolated Divine Tower": cGrace.Title = "Isolated Divine Tower"; cGrace.Id = 73460; break;
                case "Dragonbarrow West": cGrace.Title = "Dragonbarrow West"; cGrace.Id = 76450; break;
                case "Isolated Merchant's Shack (Greyoll's Dragonbarrow)": cGrace.Title = "Isolated Merchant's Shack (Greyoll's Dragonbarrow)"; cGrace.Id = 76451; break;
                case "Dragonbarrow Fork": cGrace.Title = "Dragonbarrow Fork"; cGrace.Id = 76452; break;
                case "Fort Faroth": cGrace.Title = "Fort Faroth"; cGrace.Id = 76453; break;
                case "Bestial Sanctum": cGrace.Title = "Bestial Sanctum"; cGrace.Id = 76454; break;
                case "Lenne's Rise": cGrace.Title = "Lenne's Rise"; cGrace.Id = 76455; break;
                case "Farum Greatbridge": cGrace.Title = "Farum Greatbridge"; cGrace.Id = 76456; break;
                case "Lake of Rot Shoreside": cGrace.Title = "Lake of Rot Shoreside"; cGrace.Id = 71216; break;
                case "Grand Cloister": cGrace.Title = "Grand Cloister"; cGrace.Id = 71218; break;
                case "Elden Throne (Leyndell, Ashen Capital)": cGrace.Title = "Elden Throne (Leyndell, Ashen Capital)"; cGrace.Id = 71120; break;
                case "Erdtree Sanctuary (Leyndell, Ashen Capital)": cGrace.Title = "Erdtree Sanctuary (Leyndell, Ashen Capital)"; cGrace.Id = 71121; break;
                case "East Capital Rampart (Leyndell, Ashen Capital)": cGrace.Title = "East Capital Rampart (Leyndell, Ashen Capital)"; cGrace.Id = 71122; break;
                case "Leyndell, Capital of Ash": cGrace.Title = "Leyndell, Capital of Ash"; cGrace.Id = 71123; break;
                case "Queen's Bedchamber (Leyndell, Ashen Capital)": cGrace.Title = "Queen's Bedchamber (Leyndell, Ashen Capital)"; cGrace.Id = 71124; break;
                case "Divine Bridge (Leyndell, Ashen Capital)": cGrace.Title = "Divine Bridge (Leyndell, Ashen Capital)"; cGrace.Id = 71125; break;
                case "Elden Throne (Leyndell, Royal Capital)": cGrace.Title = "Elden Throne (Leyndell, Royal Capital)"; cGrace.Id = 71100; break;
                case "Erdtree Sanctuary (Leyndell, Royal Capital)": cGrace.Title = "Erdtree Sanctuary (Leyndell, Royal Capital)"; cGrace.Id = 71101; break;
                case "East Capital Rampart (Leyndell, Royal Capital)": cGrace.Title = "East Capital Rampart (Leyndell, Royal Capital)"; cGrace.Id = 71102; break;
                case "Lower Capital Church": cGrace.Title = "Lower Capital Church"; cGrace.Id = 71103; break;
                case "Avenue Balcony": cGrace.Title = "Avenue Balcony"; cGrace.Id = 71104; break;
                case "West Capital Rampart": cGrace.Title = "West Capital Rampart"; cGrace.Id = 71105; break;
                case "Queen's Bedchamber (Leyndell, Royal Capital)": cGrace.Title = "Queen's Bedchamber (Leyndell, Royal Capital)"; cGrace.Id = 71107; break;
                case "Fortified Manor, First Floor": cGrace.Title = "Fortified Manor, First Floor"; cGrace.Id = 71108; break;
                case "Divine Bridge (Leyndell, Royal Capital)": cGrace.Title = "Divine Bridge (Leyndell, Royal Capital)"; cGrace.Id = 71109; break;
                case "Stormfoot Catacombs": cGrace.Title = "Stormfoot Catacombs"; cGrace.Id = 73002; break;
                case "Murkwater Catacombs": cGrace.Title = "Murkwater Catacombs"; cGrace.Id = 73004; break;
                case "Murkwater Cave": cGrace.Title = "Murkwater Cave"; cGrace.Id = 73100; break;
                case "Groveside Cave": cGrace.Title = "Groveside Cave"; cGrace.Id = 73103; break;
                case "Coastal Cave": cGrace.Title = "Coastal Cave"; cGrace.Id = 73115; break;
                case "Highroad Cave": cGrace.Title = "Highroad Cave"; cGrace.Id = 73117; break;
                case "Limgrave Tunnels": cGrace.Title = "Limgrave Tunnels"; cGrace.Id = 73201; break;
                case "Church of Elleh": cGrace.Title = "Church of Elleh"; cGrace.Id = 76100; break;
                case "The First Step": cGrace.Title = "The First Step"; cGrace.Id = 76101; break;
                case "Artist's Shack (Limgrave)": cGrace.Title = "Artist's Shack (Limgrave)"; cGrace.Id = 76103; break;
                case "Third Church of Marika": cGrace.Title = "Third Church of Marika"; cGrace.Id = 76104; break;
                case "Fort Haight West": cGrace.Title = "Fort Haight West"; cGrace.Id = 76105; break;
                case "Agheel Lake South": cGrace.Title = "Agheel Lake South"; cGrace.Id = 76106; break;
                case "Agheel Lake North": cGrace.Title = "Agheel Lake North"; cGrace.Id = 76108; break;
                case "Church of Dragon Communion": cGrace.Title = "Church of Dragon Communion"; cGrace.Id = 76110; break;
                case "Gatefront": cGrace.Title = "Gatefront"; cGrace.Id = 76111; break;
                case "Seaside Ruins": cGrace.Title = "Seaside Ruins"; cGrace.Id = 76113; break;
                case "Mistwood Outskirts": cGrace.Title = "Mistwood Outskirts"; cGrace.Id = 76114; break;
                case "Murkwater Coast": cGrace.Title = "Murkwater Coast"; cGrace.Id = 76116; break;
                case "Summonwater Village Outskirts": cGrace.Title = "Summonwater Village Outskirts"; cGrace.Id = 76119; break;
                case "Waypoint Ruins Cellar": cGrace.Title = "Waypoint Ruins Cellar"; cGrace.Id = 76120; break;
                case "Road's End Catacombs": cGrace.Title = "Road's End Catacombs"; cGrace.Id = 73003; break;
                case "Black Knife Catacombs": cGrace.Title = "Black Knife Catacombs"; cGrace.Id = 73005; break;
                case "Cliffbottom Catacombs": cGrace.Title = "Cliffbottom Catacombs"; cGrace.Id = 73006; break;
                case "Stillwater Cave": cGrace.Title = "Stillwater Cave"; cGrace.Id = 73104; break;
                case "Lakeside Crystal Cave": cGrace.Title = "Lakeside Crystal Cave"; cGrace.Id = 73105; break;
                case "Academy Crystal Cave": cGrace.Title = "Academy Crystal Cave"; cGrace.Id = 73106; break;
                case "Raya Lucaria Crystal Tunnel": cGrace.Title = "Raya Lucaria Crystal Tunnel"; cGrace.Id = 73202; break;
                case "Study Hall Entrance": cGrace.Title = "Study Hall Entrance"; cGrace.Id = 73420; break;
                case "Liurnia Tower Bridge": cGrace.Title = "Liurnia Tower Bridge"; cGrace.Id = 73421; break;
                case "Divine Tower of Liurnia": cGrace.Title = "Divine Tower of Liurnia"; cGrace.Id = 73422; break;
                case "Uld Palace Ruins": cGrace.Title = "Uld Palace Ruins"; cGrace.Id = 76200; break;
                case "Liurnia Lake Shore": cGrace.Title = "Liurnia Lake Shore"; cGrace.Id = 76201; break;
                case "Laskyar Ruins": cGrace.Title = "Laskyar Ruins"; cGrace.Id = 76202; break;
                case "Scenic Isle": cGrace.Title = "Scenic Isle"; cGrace.Id = 76203; break;
                case "Academy Gate Town": cGrace.Title = "Academy Gate Town"; cGrace.Id = 76204; break;
                case "South Raya Lucaria Gate": cGrace.Title = "South Raya Lucaria Gate"; cGrace.Id = 76205; break;
                case "Main Academy Gate": cGrace.Title = "Main Academy Gate"; cGrace.Id = 76206; break;
                case "Grand Lift of Dectus": cGrace.Title = "Grand Lift of Dectus"; cGrace.Id = 76209; break;
                case "Foot of the Four Belfries": cGrace.Title = "Foot of the Four Belfries"; cGrace.Id = 76210; break;
                case "Sorcerer's Isle": cGrace.Title = "Sorcerer's Isle"; cGrace.Id = 76211; break;
                case "Northern Liurnia Lake Shore": cGrace.Title = "Northern Liurnia Lake Shore"; cGrace.Id = 76212; break;
                case "Road to the Manor": cGrace.Title = "Road to the Manor"; cGrace.Id = 76213; break;
                case "Main Caria Manor Gate": cGrace.Title = "Main Caria Manor Gate"; cGrace.Id = 76214; break;
                case "Slumbering Wolf's Shack": cGrace.Title = "Slumbering Wolf's Shack"; cGrace.Id = 76215; break;
                case "Boilprawn Shack": cGrace.Title = "Boilprawn Shack"; cGrace.Id = 76216; break;
                case "Artist's Shack (Liurnia of the Lakes)": cGrace.Title = "Artist's Shack (Liurnia of the Lakes)"; cGrace.Id = 76217; break;
                case "Revenger's Shack": cGrace.Title = "Revenger's Shack"; cGrace.Id = 76218; break;
                case "Folly on the Lake": cGrace.Title = "Folly on the Lake"; cGrace.Id = 76219; break;
                case "Village of the Albinaurics": cGrace.Title = "Village of the Albinaurics"; cGrace.Id = 76220; break;
                case "Liurnia Highway North": cGrace.Title = "Liurnia Highway North"; cGrace.Id = 76221; break;
                case "Gate Town Bridge": cGrace.Title = "Gate Town Bridge"; cGrace.Id = 76222; break;
                case "Eastern Liurnia Lake Shore": cGrace.Title = "Eastern Liurnia Lake Shore"; cGrace.Id = 76223; break;
                case "Church of Vows": cGrace.Title = "Church of Vows"; cGrace.Id = 76224; break;
                case "Ruined Labyrinth": cGrace.Title = "Ruined Labyrinth"; cGrace.Id = 76225; break;
                case "Mausoleum Compound": cGrace.Title = "Mausoleum Compound"; cGrace.Id = 76226; break;
                case "The Four Belfries": cGrace.Title = "The Four Belfries"; cGrace.Id = 76227; break;
                case "Ranni's Rise": cGrace.Title = "Ranni's Rise"; cGrace.Id = 76228; break;
                case "Ravine-Veiled Village": cGrace.Title = "Ravine-Veiled Village"; cGrace.Id = 76229; break;
                case "Manor Upper Level": cGrace.Title = "Manor Upper Level"; cGrace.Id = 76230; break;
                case "Manor Lower Level": cGrace.Title = "Manor Lower Level"; cGrace.Id = 76231; break;
                case "Royal Moongazing Grounds": cGrace.Title = "Royal Moongazing Grounds"; cGrace.Id = 76232; break;
                case "Gate Town North": cGrace.Title = "Gate Town North"; cGrace.Id = 76233; break;
                case "Eastern Tableland": cGrace.Title = "Eastern Tableland"; cGrace.Id = 76234; break;
                case "The Ravine": cGrace.Title = "The Ravine"; cGrace.Id = 76235; break;
                case "Fallen Ruins of the Lake": cGrace.Title = "Fallen Ruins of the Lake"; cGrace.Id = 76236; break;
                case "Converted Tower": cGrace.Title = "Converted Tower"; cGrace.Id = 76237; break;
                case "Behind Caria Manor": cGrace.Title = "Behind Caria Manor"; cGrace.Id = 76238; break;
                case "Temple Quarter": cGrace.Title = "Temple Quarter"; cGrace.Id = 76241; break;
                case "East Gate Bridge Trestle": cGrace.Title = "East Gate Bridge Trestle"; cGrace.Id = 76242; break;
                case "Crystalline Woods": cGrace.Title = "Crystalline Woods"; cGrace.Id = 76243; break;
                case "Liurnia Highway South": cGrace.Title = "Liurnia Highway South"; cGrace.Id = 76244; break;
                case "Jarburg": cGrace.Title = "Jarburg"; cGrace.Id = 76245; break;
                case "Ranni's Chamber": cGrace.Title = "Ranni's Chamber"; cGrace.Id = 76247; break;
                case "Haligtree Promenade": cGrace.Title = "Haligtree Promenade"; cGrace.Id = 71505; break;
                case "Haligtree Canopy": cGrace.Title = "Haligtree Canopy"; cGrace.Id = 71506; break;
                case "Haligtree Town": cGrace.Title = "Haligtree Town"; cGrace.Id = 71507; break;
                case "Haligtree Town Plaza": cGrace.Title = "Haligtree Town Plaza"; cGrace.Id = 71508; break;
                case "Cocoon of the Empyrean": cGrace.Title = "Cocoon of the Empyrean"; cGrace.Id = 71250; break;
                case "Palace Approach Ledge-Road": cGrace.Title = "Palace Approach Ledge-Road"; cGrace.Id = 71251; break;
                case "Dynasty Mausoleum Entrance": cGrace.Title = "Dynasty Mausoleum Entrance"; cGrace.Id = 71252; break;
                case "Dynasty Mausoleum Midpoint": cGrace.Title = "Dynasty Mausoleum Midpoint"; cGrace.Id = 71253; break;
                case "Moonlight Altar": cGrace.Title = "Moonlight Altar"; cGrace.Id = 76250; break;
                case "Cathedral of Manus Celes": cGrace.Title = "Cathedral of Manus Celes"; cGrace.Id = 76251; break;
                case "Altar South": cGrace.Title = "Altar South"; cGrace.Id = 76252; break;
                case "Spiritcaller's Cave": cGrace.Title = "Spiritcaller's Cave"; cGrace.Id = 73122; break;
                case "Zamor Ruins": cGrace.Title = "Zamor Ruins"; cGrace.Id = 76501; break;
                case "Ancient Snow Valley Ruins": cGrace.Title = "Ancient Snow Valley Ruins"; cGrace.Id = 76503; break;
                case "Freezing Lake": cGrace.Title = "Freezing Lake"; cGrace.Id = 76504; break;
                case "First Church of Marika": cGrace.Title = "First Church of Marika"; cGrace.Id = 76505; break;
                case "Whiteridge Road": cGrace.Title = "Whiteridge Road"; cGrace.Id = 76520; break;
                case "Snow Valley Ruins Overlook": cGrace.Title = "Snow Valley Ruins Overlook"; cGrace.Id = 76521; break;
                case "Castle Sol Main Gate": cGrace.Title = "Castle Sol Main Gate"; cGrace.Id = 76522; break;
                case "Church of the Eclipse": cGrace.Title = "Church of the Eclipse"; cGrace.Id = 76523; break;
                case "Castle Sol Rooftop": cGrace.Title = "Castle Sol Rooftop"; cGrace.Id = 76524; break;
                case "Wyndham Catacombs": cGrace.Title = "Wyndham Catacombs"; cGrace.Id = 73007; break;
                case "Gelmir Hero's Grave": cGrace.Title = "Gelmir Hero's Grave"; cGrace.Id = 73009; break;
                case "Seethewater Cave": cGrace.Title = "Seethewater Cave"; cGrace.Id = 73107; break;
                case "Volcano Cave": cGrace.Title = "Volcano Cave"; cGrace.Id = 73109; break;
                case "Bridge of Iniquity": cGrace.Title = "Bridge of Iniquity"; cGrace.Id = 76350; break;
                case "First Mt. Gelmir Campsite": cGrace.Title = "First Mt. Gelmir Campsite"; cGrace.Id = 76351; break;
                case "Ninth Mt. Gelmir Campsite": cGrace.Title = "Ninth Mt. Gelmir Campsite"; cGrace.Id = 76352; break;
                case "Road of Iniquity": cGrace.Title = "Road of Iniquity"; cGrace.Id = 76353; break;
                case "Seethewater River": cGrace.Title = "Seethewater River"; cGrace.Id = 76354; break;
                case "Seethewater Terminus": cGrace.Title = "Seethewater Terminus"; cGrace.Id = 76355; break;
                case "Craftsman's Shack": cGrace.Title = "Craftsman's Shack"; cGrace.Id = 76356; break;
                case "Primeval Sorcerer Azur": cGrace.Title = "Primeval Sorcerer Azur"; cGrace.Id = 76357; break;
                case "Great Waterfall Basin": cGrace.Title = "Great Waterfall Basin"; cGrace.Id = 71220; break;
                case "Mimic Tear": cGrace.Title = "Mimic Tear"; cGrace.Id = 71221; break;
                case "Ancestral Woods": cGrace.Title = "Ancestral Woods"; cGrace.Id = 71224; break;
                case "Aqueduct-Facing Cliffs": cGrace.Title = "Aqueduct-Facing Cliffs"; cGrace.Id = 71225; break;
                case "Night's Sacred Ground": cGrace.Title = "Night's Sacred Ground"; cGrace.Id = 71226; break;
                case "Nokron, Eternal City": cGrace.Title = "Nokron, Eternal City"; cGrace.Id = 71271; break;
                case "Table of Lost Grace": cGrace.Title = "Table of Lost Grace"; cGrace.Id = 71190; break;
                case "Magma Wyrm": cGrace.Title = "Magma Wyrm"; cGrace.Id = 73900; break;
                case "Ruin-Strewn Precipice": cGrace.Title = "Ruin-Strewn Precipice"; cGrace.Id = 73901; break;
                case "Ruin-Strewn Precipice Overlook": cGrace.Title = "Ruin-Strewn Precipice Overlook"; cGrace.Id = 73902; break;
                case "Siofra River Bank": cGrace.Title = "Siofra River Bank"; cGrace.Id = 71222; break;
                case "Worshippers' Woods": cGrace.Title = "Worshippers' Woods"; cGrace.Id = 71223; break;
                case "Below the Well": cGrace.Title = "Below the Well"; cGrace.Id = 71227; break;
                case "Siofra River Well Depths": cGrace.Title = "Siofra River Well Depths"; cGrace.Id = 71270; break;
                case "Deathtouched Catacombs": cGrace.Title = "Deathtouched Catacombs"; cGrace.Id = 73011; break;
                case "Limgrave Tower Bridge": cGrace.Title = "Limgrave Tower Bridge"; cGrace.Id = 73410; break;
                case "Divine Tower of Limgrave": cGrace.Title = "Divine Tower of Limgrave"; cGrace.Id = 73412; break;
                case "Stormhill Shack": cGrace.Title = "Stormhill Shack"; cGrace.Id = 76102; break;
                case "Saintsbridge": cGrace.Title = "Saintsbridge"; cGrace.Id = 76117; break;
                case "Warmaster's Shack": cGrace.Title = "Warmaster's Shack"; cGrace.Id = 76118; break;
                case "Godrick the Grafted": cGrace.Title = "Godrick the Grafted"; cGrace.Id = 71000; break;
                case "Margit, the Fell Omen": cGrace.Title = "Margit, the Fell Omen"; cGrace.Id = 71001; break;
                case "Castleward Tunnel": cGrace.Title = "Castleward Tunnel"; cGrace.Id = 71002; break;
                case "Gateside Chamber": cGrace.Title = "Gateside Chamber"; cGrace.Id = 71003; break;
                case "Stormveil Cliffside": cGrace.Title = "Stormveil Cliffside"; cGrace.Id = 71004; break;
                case "Rampart Tower": cGrace.Title = "Rampart Tower"; cGrace.Id = 71005; break;
                case "Liftside Chamber": cGrace.Title = "Liftside Chamber"; cGrace.Id = 71006; break;
                case "Secluded Cell": cGrace.Title = "Secluded Cell"; cGrace.Id = 71007; break;
                case "Stormveil Main Gate": cGrace.Title = "Stormveil Main Gate"; cGrace.Id = 71008; break;
                case "Cave of Knowledge": cGrace.Title = "Cave of Knowledge"; cGrace.Id = 71800; break;
                case "Stranded Graveyard": cGrace.Title = "Stranded Graveyard"; cGrace.Id = 71801; break;
                case "Cathedral of the Forsaken": cGrace.Title = "Cathedral of the Forsaken"; cGrace.Id = 73500; break;
                case "Underground Roadside": cGrace.Title = "Underground Roadside"; cGrace.Id = 73501; break;
                case "Forsaken Depths": cGrace.Title = "Forsaken Depths"; cGrace.Id = 73502; break;
                case "Leyndell Catacombs": cGrace.Title = "Leyndell Catacombs"; cGrace.Id = 73503; break;
                case "Frenzied Flame Proscription": cGrace.Title = "Frenzied Flame Proscription"; cGrace.Id = 73504; break;
                case "Aeonia Swamp Shore": cGrace.Title = "Aeonia Swamp Shore"; cGrace.Id = 76406; break;
                case "Astray from Caelid Highway North": cGrace.Title = "Astray from Caelid Highway North"; cGrace.Id = 76407; break;
                case "Heart of Aeonia": cGrace.Title = "Heart of Aeonia"; cGrace.Id = 76412; break;
                case "Inner Aeonia": cGrace.Title = "Inner Aeonia"; cGrace.Id = 76413; break;
                case "Rykard, Lord of Blasphemy": cGrace.Title = "Rykard, Lord of Blasphemy"; cGrace.Id = 71600; break;
                case "Temple of Eiglay": cGrace.Title = "Temple of Eiglay"; cGrace.Id = 71601; break;
                case "Volcano Manor": cGrace.Title = "Volcano Manor"; cGrace.Id = 71602; break;
                case "Prison Town Church": cGrace.Title = "Prison Town Church"; cGrace.Id = 71603; break;
                case "Guest Hall": cGrace.Title = "Guest Hall"; cGrace.Id = 71604; break;
                case "Audience Pathway": cGrace.Title = "Audience Pathway"; cGrace.Id = 71605; break;
                case "Abductor Virgin": cGrace.Title = "Abductor Virgin"; cGrace.Id = 71606; break;
                case "Subterranean Inquisition Chamber": cGrace.Title = "Subterranean Inquisition Chamber"; cGrace.Id = 71607; break;
                case "Tombsward Catacombs": cGrace.Title = "Tombsward Catacombs"; cGrace.Id = 73000; break;
                case "Impaler's Catacombs": cGrace.Title = "Impaler's Catacombs"; cGrace.Id = 73001; break;
                case "Earthbore Cave": cGrace.Title = "Earthbore Cave"; cGrace.Id = 73101; break;
                case "Tombsward Cave": cGrace.Title = "Tombsward Cave"; cGrace.Id = 73102; break;
                case "Morne Tunnel": cGrace.Title = "Morne Tunnel"; cGrace.Id = 73200; break;
                case "Church of Pilgrimage": cGrace.Title = "Church of Pilgrimage"; cGrace.Id = 76150; break;
                case "Castle Morne Rampart": cGrace.Title = "Castle Morne Rampart"; cGrace.Id = 76151; break;
                case "Tombsward": cGrace.Title = "Tombsward"; cGrace.Id = 76152; break;
                case "South of the Lookout Tower": cGrace.Title = "South of the Lookout Tower"; cGrace.Id = 76153; break;
                case "Ailing Village Outskirts": cGrace.Title = "Ailing Village Outskirts"; cGrace.Id = 76154; break;
                case "Beside the Crater-Pocked Glade": cGrace.Title = "Beside the Crater-Pocked Glade"; cGrace.Id = 76155; break;
                case "Isolated Merchant's Shack (Weeping Peninsula)": cGrace.Title = "Isolated Merchant's Shack (Weeping Peninsula)"; cGrace.Id = 76156; break;
                case "Bridge of Sacrifice": cGrace.Title = "Bridge of Sacrifice"; cGrace.Id = 76157; break;
                case "Castle Morne Lift": cGrace.Title = "Castle Morne Lift"; cGrace.Id = 76158; break;
                case "Behind the Castle": cGrace.Title = "Behind the Castle"; cGrace.Id = 76159; break;
                case "Beside the Rampart Gaol": cGrace.Title = "Beside the Rampart Gaol"; cGrace.Id = 76160; break;
                case "Morne Moangrave": cGrace.Title = "Morne Moangrave"; cGrace.Id = 76161; break;
                case "Fourth Church of Marika": cGrace.Title = "Fourth Church of Marika"; cGrace.Id = 76162; break;
                default: cGrace = null; break;
            }
            return cGrace;
        }

        #endregion
        #region Position.Elden
        [Serializable]
        public class PositionER
        {
            public SoulMemory.EldenRing.Position vector = new SoulMemory.EldenRing.Position();
            public bool IsSplited = false;
            public string Mode;
        }
        #endregion
        #region CustomFlag.Elden
        public class CustomFlagER
        {
            public uint Id;
            public bool IsSplited = false;
            public string Mode;

        }
        #endregion
    }

    [Serializable]
    public class DTElden
    {
        //var Settings
        public bool enableSplitting = false;
        public bool autoTimer = false;
        public bool gameTimer = false;
        public int positionMargin = 3;
        //Flags to Split
        public List<DefinitionsElden.BossER> bossToSplit = new List<DefinitionsElden.BossER>();
        public List<DefinitionsElden.Grace> graceToSplit = new List<DefinitionsElden.Grace>();
        public List<DefinitionsElden.PositionER> positionToSplit = new List<DefinitionsElden.PositionER>();
        public List<DefinitionsElden.CustomFlagER> flagsToSplit = new List<DefinitionsElden.CustomFlagER>();


        public List<DefinitionsElden.BossER> getBossToSplit()
        {
            return this.bossToSplit;
        }

        public List<DefinitionsElden.Grace> getGraceToSplit()
        {
            return this.graceToSplit;
        }

        public List<DefinitionsElden.PositionER> getPositionToSplit()
        {
            return this.positionToSplit;
        }

        public List<DefinitionsElden.CustomFlagER> getFlagsToSplit()
        {
            return this.flagsToSplit;
        }
    }
}
