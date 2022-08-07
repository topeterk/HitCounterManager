// Resident Evil HD Remaster Autosplitter Version 3.0
// Supports room-room splits for every category, in addition to key-item and key-event splits.
// Split files may be obtained from: 
// by CursedToast 2/22/2016 (1.0 initial release) to 5/18/2018 (3.0 release)

// Special thanks to:
// Fatalis - original game time
// Dchaps - initial scripting/data mining support
// Pessimism - split order support, recorded runs with CE running for room ID data so I didn't have to.
// LileyaCelestie - recorded runs with CE running for room ID data so I didn't have to.
// wooferzfg - LiveSplit coding support and for developing LiveSplit so this script could happen
// ZerothGames - item and inventory values
// GrowthKasei - split order support.

// Beta testers:
// Pessimism, LileyaCelestie, GrowthKasei, Bawkbasoup, ZerothGames.

// Donators that supported this script's development:
// Pessimism

// Thank you to all the above people for helping me make this possible.
// -CursedToast/Nate

state("bhd")
{
    float time : "bhd.exe", 0x97C9C0, 0xe474c;
    int slot1 : "bhd.exe", 0x97C9C0, 0x38;
	int slot2 : "bhd.exe", 0x97C9C0, 0x40;
	int slot3 : "bhd.exe", 0x97C9C0, 0x48;
	int slot4 : "bhd.exe", 0x97C9C0, 0x50;
	int slot5 : "bhd.exe", 0x97C9C0, 0x58;
	int slot6 : "bhd.exe", 0x97C9C0, 0x60;
	int slot7 : "bhd.exe", 0x97C9C0, 0x68;
	int slot8 : "bhd.exe", 0x97C9C0, 0x70;
	byte roomid : "bhd.exe", 0x0097C9C0, 0xE4754;
	byte prevroom : "bhd.exe", 0x0097C9C0, 0xE4760;
	int cameraangle : "bhd.exe", 0x0097C9C0, 0xE478C;
}


start
{
    return current.time < old.time && old.prevroom != 0;
}

startup
{
	settings.Add("autosplit", false, "Use the auto-splitter?");
	settings.SetToolTip("autosplit", "Keep this checked unless you want only the in-game timer.");
	settings.Add("doorsplit", false, "Auto-split on all doors?", "autosplit");
	settings.SetToolTip("doorsplit", "Make sure you grab the right splits file either way!");

    settings.Add("arrow", false, "Arrow", "autosplit");
    settings.Add("swordKey", false, "Sword Key", "autosplit");
    settings.Add("chemical", false, "Chemical", "autosplit");
    settings.Add("dogWhistle", false, "Dog Whistle", "autosplit");
    settings.Add("dogCollar", false, "Dog Collar", "autosplit");
    settings.Add("armorKey", false, "Armor Key", "autosplit");
    settings.Add("emblemKey", false, "Emblem Key", "autosplit");
    settings.Add("galleryKey", false, "Gallery Key", "autosplit");
    settings.Add("musicalScore1", false, "Musical Score 1", "autosplit");
    settings.Add("musicalScore2", false, "Musical Score 2", "autosplit");
    settings.Add("shieldKey", false, "Shield Key", "autosplit");
    settings.Add("helmetKey", false, "Helmet Key", "autosplit");
    settings.Add("residenceKey001", false, "Residence Key 001", "autosplit");
    settings.Add("residenceKey003", false, "Residence Key 003", "autosplit");
    settings.Add("lastBook1", false, "Last Book 1", "autosplit");
    settings.Add("lastBook2", false, "Last Book 2", "autosplit");
    settings.Add("squareCrank", false, "Square Crank", "autosplit");
    settings.Add("hexCrank", false, "Hex Crank", "autosplit");
    settings.Add("battery", false, "Battery", "autosplit");
    settings.Add("serum", false, "Serum", "autosplit");
    settings.Add("redBook", false, "Red Book", "autosplit");
    settings.Add("fuse", false, "Fuse", "autosplit");
    settings.Add("moDisk1", false, "MO Disk 1", "autosplit");
    settings.Add("moDisk2", false, "MO Disk 2", "autosplit");
    settings.Add("moDisk3", false, "MO Disk 3", "autosplit");
    settings.Add("jewelryBox", false, "Jewelry Box", "autosplit");
    settings.Add("insectSpray", false, "Insect Spray", "autosplit");
    settings.Add("brokenFlamethrower", false, "Broken Flamethrower", "autosplit");
    settings.Add("cylinder", false, "Cylinder", "autosplit");
    settings.Add("cylinderShaft", false, "Jewelry Box", "autosplit");
    settings.Add("deathMask1", false, "Death Mask 1", "autosplit");
    settings.Add("deathMask2", false, "Death Mask 2", "autosplit");
    settings.Add("deathMask3", false, "Death Mask 3", "autosplit");
    settings.Add("deathMask4", false, "Death Mask 4", "autosplit");
    settings.Add("controlRoomKey", false, "Control Room Key", "autosplit");
    settings.Add("lighter", false, "Lighter", "autosplit");
    settings.Add("redGem", false, "Red Gem", "autosplit");
    settings.Add("blueGem", false, "Blue Gem", "autosplit");
    settings.Add("yellowGem", false, "Yellow Gem", "autosplit");
    settings.Add("goldEmblem", false, "Gold Emblem", "autosplit");
    settings.Add("elderCrimson", false, "Elder Crimson", "autosplit");
    settings.Add("gemBox", false, "Gem Box (Lisa's Room)", "autosplit");
    settings.Add("grenadeLauncher", false, "Grenade Launcher", "autosplit");
    settings.Add("labKey", false, "Lab Key", "autosplit");
    settings.Add("lisaTrevor", false, "Lisa Trevor", "autosplit");
    settings.Add("blackTiger", false, "Black Tiger", "autosplit");
    settings.Add("tyrant", false, "Tyrant", "autosplit");
    settings.Add("end", false, "End", "autosplit");
    settings.Add("8675309", false, "~~~~~~~Misc./Category Specific~~~~~~~", "autosplit");
    settings.Add("oldKey1", false, "Old Key 1", "autosplit");
    settings.Add("oldKey2", false, "Old Key 2", "autosplit");
    settings.Add("oldKey3", false, "Old Key 3", "autosplit");
    settings.Add("assaultShotgun", false, "Assault Shotgun", "autosplit");
}

isLoading
{
    return true;
}

init
{
    vars.arrow = 0;
    vars.swordKey = 0;
    vars.chemical = 0;
    vars.dogWhistle = 0;
    vars.dogCollar = 0;
    vars.armorKey = 0;
    vars.emblemKey = 0;
    vars.galleryKey = 0;
    vars.musicalScore1 = 0;
    vars.musicalScore2 = 0;
    vars.shieldKey = 0;
    vars.helmetKey = 0;
    vars.residenceKey001 = 0;
    vars.residenceKey003 = 0;
    vars.lastBook1 = 0;
    vars.lastBook2 = 0;
    vars.squareCrank = 0;
    vars.hexCrank = 0;
    vars.battery = 0;
    vars.serum = 0;
    vars.redBook = 0;
    vars.fuse = 0;
    vars.moDisk1 = 0;
    vars.moDisk2 = 0;
    vars.moDisk3 = 0;
    vars.jewelryBox = 0;
    vars.insectSpray = 0;
    vars.brokenFlamethrower = 0;
    vars.cylinder = 0;
    vars.cylinderShaft = 0;
    vars.deathMask1 = 0;
    vars.deathMask2 = 0;
    vars.deathMask3 = 0;
    vars.deathMask4 = 0;
    vars.controlRoomKey = 0;
    vars.lighter = 0;
    vars.redGem = 0;
    vars.blueGem = 0;
    vars.yellowGem = 0;
    vars.goldEmblem = 0;
    vars.elderCrimson = 0;
    vars.gemBox = 0;
    vars.grenadeLauncher = 0;
    vars.labKey = 0;
    vars.lisaTrevor = 0;
    vars.blackTiger = 0;
    vars.tyrant = 0;
    vars.end = 0;
    vars.oldKey1 = 0;
    vars.oldKey2 = 0;
    vars.oldKey3 = 0;
    vars.assaultShotgun = 0;
}

update
{
    List<int> list = new List<int>();

    list.Add(current.slot1);
    list.Add(current.slot2);
    list.Add(current.slot3);
    list.Add(current.slot4);
    list.Add(current.slot5);
    list.Add(current.slot6);
    list.Add(current.slot7);
    list.Add(current.slot8);

    int[] inventorySlots = list.ToArray();
    vars.inventorySlots = inventorySlots;

    if (timer.CurrentPhase == TimerPhase.NotRunning)
    {
        vars.arrow = 0;
        vars.swordKey = 0;
        vars.chemical = 0;
        vars.dogWhistle = 0;
        vars.dogCollar = 0;
        vars.armorKey = 0;
        vars.emblemKey = 0;
        vars.galleryKey = 0;
        vars.musicalScore1 = 0;
        vars.musicalScore2 = 0;
        vars.shieldKey = 0;
        vars.helmetKey = 0;
        vars.residenceKey001 = 0;
        vars.residenceKey003 = 0;
        vars.lastBook1 = 0;
        vars.lastBook2 = 0;
        vars.squareCrank = 0;
        vars.hexCrank = 0;
        vars.battery = 0;
        vars.serum = 0;
        vars.redBook = 0;
        vars.fuse = 0;
        vars.moDisk1 = 0;
        vars.moDisk2 = 0;
        vars.moDisk3 = 0;
        vars.jewelryBox = 0;
        vars.insectSpray = 0;
        vars.brokenFlamethrower = 0;
        vars.cylinder = 0;
        vars.cylinderShaft = 0;
        vars.deathMask1 = 0;
        vars.deathMask2 = 0;
        vars.deathMask3 = 0;
        vars.deathMask4 = 0;
        vars.controlRoomKey = 0;
        vars.lighter = 0;
        vars.redGem = 0;
        vars.blueGem = 0;
        vars.yellowGem = 0;
        vars.goldEmblem = 0;
        vars.elderCrimson = 0;
        vars.gemBox = 0;
        vars.grenadeLauncher = 0;
        vars.labKey = 0;
        vars.lisaTrevor = 0;
        vars.blackTiger = 0;
        vars.tyrant = 0;
        vars.end = 0;
        vars.oldKey1 = 0;
        vars.oldKey2 = 0;
        vars.oldKey3 = 0;
        vars.assaultShotgun = 0;
    }
}

split
{
    if (settings["autosplit"] == true)
    {
		
		if (settings["doorsplit"] == true)
		{
			if (current.roomid != old.roomid)
			{
				return true;
			}
		}
		
		foreach (int item in vars.inventorySlots) 
        {
            switch((int)item)
            {
                case 21:
                    if (vars.arrow == 0)
                    {
                        vars.arrow = 1;
                        return settings["arrow"];
                    }
                    break;

                case 45:
                    if (vars.swordKey == 0)
                    {
                        vars.swordKey = 1;
                        return settings["swordKey"];
                    }
                    break;

                case 76:
                    if (vars.chemical == 0)
                    {
                        vars.chemical = 1;
                        return settings["chemical"];
                    }
                    break;

                case 96:
                    if (vars.dogWhistle == 0)
                    {
                        vars.dogWhistle = 1;
                        return settings["dogWhistle"];
                    }
                    break;

                case 24:
                    if (vars.dogCollar == 0)
                    {
                        vars.dogCollar = 1;
                        return settings["dogCollar"];
                    }
                    break;

                case 46:
                    if (vars.armorKey == 0)
                    {
                        vars.armorKey = 1;
                        return settings["armorKey"];
                    }
                    break;

                case 63:
                    if (vars.emblemKey == 0)
                    {
                        vars.emblemKey = 1;
                        return settings["emblemKey"];
                    }
                    break;

                case 30:
                    if (vars.galleryKey == 0)
                    {
                        vars.galleryKey = 1;
                        return settings["galleryKey"];
                    }
                    break;

                case 129:
                    if (vars.musicalScore1 == 0)
                    {
                        vars.musicalScore1 = 1;
                        return settings["musicalScore1"];
                    }
                    break;

                case 32:
                    if (vars.musicalScore2 == 0)
                    {
                        vars.musicalScore2 = 1;
                        return settings["musicalScore2"];
                    }
                    break;

                case 47:
                    if (vars.shieldKey == 0)
                    {
                        vars.shieldKey = 1;
                        return settings["shieldKey"];
                    }
                    break;

                case 48:
                    if (vars.helmetKey == 0)
                    {
                        vars.helmetKey = 1;
                        return settings["helmetKey"];
                    }
                    break;

                case 49:
                    if (vars.residenceKey001 == 0)
                    {
                        vars.residenceKey001 = 1;
                        return settings["residenceKey001"];
                    }
                    break;

                case 50:
                    if (vars.residenceKey003 == 0)
                    {
                        vars.residenceKey003 = 1;
                        return settings["residenceKey003"];
                    }
                    break;

                case 91:
                    if (vars.lastBook1 == 0)
                    {
                        vars.lastBook1 = 1;
                        return settings["lastBook1"];
                    }
                    break;

                case 92:
                    if (vars.lastBook2 == 0)
                    {
                        vars.lastBook2 = 1;
                        return settings["lastBook2"];
                    }
                    break;

                case 60:
                    if (vars.squareCrank == 0)
                    {
                        vars.squareCrank = 1;
                        return settings["squareCrank"];
                    }
                    break;

                case 61:
                    if (vars.hexCrank == 0)
                    {
                        vars.hexCrank = 1;
                        return settings["hexCrank"];
                    }
                    break;

                case 67:
                    if (vars.battery == 0)
                    {
                        vars.battery = 1;
                        return settings["battery"];
                    }
                    break;

                case 78:
                    if (vars.serum == 0)
                    {
                        vars.serum = 1;
                        return settings["serum"];
                    }
                    break;

                case 90:
                    if (vars.redBook == 0)
                    {
                        vars.redBook = 1;
                        return settings["redBook"];
                    }
                    break;

                case 97:
                    if (vars.fuse == 0)
                    {
                        vars.fuse = 1;
                        return settings["fuse"];
                    }
                    break;

                case 93:
                    if (vars.moDisk1 == 0)
                    {
                        vars.moDisk1 = 1;
                        return settings["moDisk1"];
                    }
                    break;

                case 94:
                    if (vars.moDisk2 == 0)
                    {
                        vars.moDisk2 = 1;
                        return settings["moDisk2"];
                    }
                    break;

                case 95:
                    if (vars.moDisk3 == 0)
                    {
                        vars.moDisk3 = 1;
                        return settings["moDisk3"];
                    }
                    break;

                case 99:
                    if (vars.jewelryBox == 0)
                    {
                        vars.jewelryBox = 1;
                        return settings["jewelryBox"];
                    }
                    break;

                case 100:
                    if (vars.insectSpray == 0)
                    {
                        vars.insectSpray = 1;
                        return settings["insectSpray"];
                    }
                    break;

                case 107:
                    if (vars.brokenFlamethrower == 0)
                    {
                        vars.brokenFlamethrower = 1;
                        return settings["brokenFlamethrower"];
                    }
                    break;

                case 123:
                    if (vars.cylinder == 0)
                    {
                        vars.cylinder = 1;
                        return settings["cylinder"];
                    }
                    break;

                case 124:
                    if (vars.cylinderShaft == 0)
                    {
                        vars.cylinderShaft = 1;
                        return settings["cylinderShaft"];
                    }
                    break;

                case 126:
                    if (vars.deathMask1 == 0)
                    {
                        vars.deathMask1 = 1;
                        return settings["deathMask1"];
                    }
                    break;

                case 127:
                    if (vars.deathMask2 == 0)
                    {
                        vars.deathMask2 = 1;
                        return settings["deathMask2"];
                    }
                    break;

                case 128:
                    if (vars.deathMask4 == 0)
                    {
                        vars.deathMask4 = 1;
                        return settings["deathMask4"];
                    }
                    break;

                case 103:
                    if (vars.deathMask3 == 0)
                    {
                        vars.deathMask3 = 1;
                        return settings["deathMask3"];
                    }
                    break;

                case 110:
                    if (vars.controlRoomKey == 0)
                    {
                        vars.controlRoomKey = 1;
                        return settings["controlRoomKey"];
                    }
                    break;

                case 77:
                    if (vars.lighter == 0)
                    {
                        vars.lighter = 1;
                        return settings["lighter"];
                    }
                    break;

                case 72:
                    if (vars.redGem == 0)
                    {
                        vars.redGem = 1;
                        return settings["redGem"];
                    }
                    break;

                case 73:
                    if (vars.blueGem == 0)
                    {
                        vars.blueGem = 1;
                        return settings["blueGem"];
                    }
                    break;

                case 74:
                    if (vars.yellowGem == 0)
                    {
                        vars.yellowGem = 1;
                        return settings["yellowGem"];
                    }
                    break;

                case 70:
                    if (vars.goldEmblem == 0)
                    {
                        vars.goldEmblem = 1;
                        return settings["goldEmblem"];
                    }
                    break;

                case 53:
                    if (vars.elderCrimson == 0)
                    {
                        vars.elderCrimson = 1;
                        return settings["elderCrimson"];
                    }
                    break;

                case 102:
                    if (vars.gemBox == 0)
                    {
                        vars.gemBox = 1;
                        return settings["gemBox"];
                    }
                    break;

                case 7:
                    if (vars.grenadeLauncher == 0)
                    {
                        vars.grenadeLauncher = 1;
                        return settings["grenadeLauncher"];
                    }
                    break;

                case 54:
                    if (vars.labKey == 0)
                    {
                        vars.labKey = 1;
                        return settings["labKey"];
                    }
                    break;

                case 13:
                    if (vars.assaultShotgun == 0)
                    {
                        vars.assaultShotgun = 1;
                        return settings["assaultShotgun"];
                    }
                    break;

                default:
                    break;
            }
        }

        if (current.roomid != old.roomid)
        {
            switch ((int)current.roomid)
            {
                case 21:
                    if (vars.lisaTrevor == 0 && current.prevroom == 26 && current.cameraangle == 7)
                    {
                        vars.lisaTrevor = 1;
                        return settings["lisaTrevor"];
                    }
                    break;

                case 13:
                    if (vars.blackTiger == 0 && current.prevroom == 12)
                    {
                        vars.blackTiger = 1;
                        return settings["blackTiger"];
                    }
                    break;

                case 20:
                    if (vars.tyrant == 0 && current.prevroom == 19)
                    {
                        vars.tyrant = 1;
                        return settings["tyrant"];
                    }
                    break;

                default:
                    break;
            }
        }

        if (current.roomid == 0 && current.prevroom == 3 && vars.end == 0)
        {
            vars.end = 1;
            return settings["end"];
        }
    }
}

gameTime
{
	return TimeSpan.FromSeconds(current.time);
}
