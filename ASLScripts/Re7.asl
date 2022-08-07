//Resident Evil 7 Autosplitter
//By CursedToast 1/28/2017
//Last updated 12/20/2021

//Special thanks to:
// Souzooka - helping me re-code this to reduce lag and improving my coding in ASL. Couldn't have done this without him :)
//CarcinogenSDA (you know why)
//Dchaps - programming support
//shiftweave - programming support
//mgr.inz.Player - inventory memory value support
//Theumer115 - inventory memory value support
//DarkByte - inventory memory value support
//Nexusphobiker - helping me find the most updated pointers in the 2/6/2017 RE7 update, and for teaching me how to find them again in the future.


state("re7", "1.4")
{
  	int gamePauseState: "re7.exe", 0x082186C0, 0x28, 0x428, 0x40, 0x28, 0x104;
	string128 map : "re7.exe", 0x081DE6A8, 0x700, 0x0;
	int isdying : "re7.exe", 0x081E4148, 0x60;
}

state("re7", "cerod")
{
	int gamePauseState: "re7.exe", 0x093698F0, 0x28, 0x428, 0x40, 0x28, 0x104;
	string128 map : "re7.exe", 0x0932F7E8, 0x700, 0x0;
	int isdying : "re7.exe", 0x093352C0, 0x60;
}

state("re7", "cerod_nvidia")
{
	int gamePauseState: "re7.exe", 0x9358310, 0x28, 0x428, 0x40, 0x28, 0x104;
	string128 map : "re7.exe", 0x931D8E8, 0x700, 0x0;
	int isdying : "re7.exe", 0x9322E10, 0x60;
}

state("re7", "Buy RE8!! Version")
{
	int gamePauseState: "re7.exe", 0x081FB9F8, 0x108;
	string128 map : "re7.exe", 0x081EACE0, 0x700, 0x0;
	int isdying : "re7.exe", 0x081F24E8, 0x60;
}

state("re7", "12/17 Update")
{
	int gamePauseState: "re7.exe", 0x081FA818, 0x108;
	string128 map : "re7.exe", 0x081E9B00, 0x700, 0x0;
	int isdying : "re7.exe", 0x081F1308, 0x60;
}

startup
{
	settings.Add("maingame", false, "Main Campaign");
	settings.Add("meme", false, "No Guest House%", "maingame");
	settings.Add("c01_Corridor01", false, "Reached the Guest House", "maingame");
	settings.Add("ChainCutter", false, "Bolt Cutters", "maingame");
	settings.Add("HandAxe", false, "Axe (first time)", "maingame");
	settings.Add("Fuse", false, "Fuse (Guest House)", "maingame");
	settings.Add("welcome2family", false, "Welcome to the family (splits during drag scene)", "maingame");
	settings.Add("100family", false, "100% run (splits during Zoe stapling hand)", "welcome2family");
	
	settings.Add("FloorDoorKey", false, "Hatch Key", "maingame");
	settings.Add("Knife", false, "Knife", "maingame");
	settings.Add("Handgun_M19", false, "M19 (Guest House gun)", "maingame");
	settings.Add("Handgun_G17", false, "G17 (Garage gun)", "maingame");
	settings.Add("EthanCarKey", false, "Car Key", "maingame");
	settings.Add("EntranceHallKey", false, "Ox Statuette", "maingame");
	settings.Add("PendulumClock", false, "Clock Pendulum", "maingame");
	settings.Add("3CrestKeyA", false, "Blue Dog Head", "maingame");
	settings.Add("SilhouettePazzlePiece", false, "Wooden Statuette", "maingame");
	settings.Add("3CrestKeyB", false, "White Dog Head", "maingame");
	settings.Add("WorkroomKey", false, "Dissection Room Key", "maingame");
	settings.Add("3CrestKeyC", false, "Red Dog Head", "maingame");
	settings.Add("ChainSaw", false, "Chainsaw", "maingame");
	settings.Add("chainsawduel", false, "Finished chainsaw fight", "maingame");
	settings.Add("MorgueKey", false, "Scorpion Key", "maingame");
	settings.Add("c03_TrailerHouse", false, "Reached the trailer (first time)", "maingame");
	settings.Add("c03_OldHouse1FEntrance01", false, "Reached the Old House", "maingame");
	settings.Add("BurnerPartsA", false, "Burner Grip", "maingame");
	settings.Add("BurnerPartsB", false, "Burner Nozzle", "maingame");
	settings.Add("SilhouettePazzlePieceOldHouse", false, "Stone Statue", "maingame");
	settings.Add("Crank", false, "Crank", "maingame");
	settings.Add("TalismanKey", false, "Crow Key", "maingame");
	settings.Add("c03_OldHouse2FHallway02", false, "Entered Crow Door (after falling down hole)", "maingame");
	settings.Add("Lantern", false, "Lantern", "maingame");
	settings.Add("usedlantern", false, "Placed Lantern (to unlock door)", "maingame");
	settings.Add("SerumMaterialA", false, "D-Series Arm", "maingame");
	settings.Add("Magnum", false, "Magnum", "maingame");
	settings.Add("MasterKey", false, "Snake Key", "maingame");
	settings.Add("GrenadeLauncher", false, "Grenade Launcher", "maingame");
	settings.Add("LucasCardKey2", false, "Red Key Card", "maingame");
	settings.Add("LucasCardKey", false, "Blue Keycard", "maingame");
	settings.Add("c03_LeftArea2FTvRoom", false, "Reached Lucas TV room", "maingame");
	settings.Add("Battery", false, "Battery", "maingame");
	settings.Add("Candle", false, "Candle", "maingame");
	settings.Add("Valve", false, "Valve Handle", "maingame");
	settings.Add("Timebomb", false, "Timebomb", "maingame");
	settings.Add("SerumMaterialB", false, "D-Series Head", "maingame");
	settings.Add("SerumComplete", false, "Completed Serum", "maingame");
	settings.Add("hittheroadjack", false, "Injected Jack", "maingame");
	settings.Add("EvelynRadar1", false, "Mia Start", "maingame");
	settings.Add("fuse2", false, "Fuse 2 (ship)", "maingame");
	settings.Add("FoundFootage050", false, "Mia Videotape (picked up)", "maingame");
	settings.Add("videotapeend", false, "Mia Tape End", "maingame");
	settings.Add("EvOpener", false, "Lug Wrench", "maingame");
	settings.Add("EvCable", false, "Power Cable", "maingame");
	settings.Add("fuse3", false, "Retrieved Fuse (from door on ship)", "maingame");
	settings.Add("EvelynRadar4", false, "Mia Complete", "maingame");
	settings.Add("SerumTypeE", false, "Necrotoxin", "maingame");
	settings.Add("injectedbitch", false, "Injected Evie", "maingame");
	settings.Add("Handgun_Albert", false, "Albert Gun (Playtime's over)", "maingame");
	settings.Add("end", false, "End", "maingame");
	settings.Add("nah", false, "Not a Hero");
	settings.Add("c08_SaltMineCorridor01", false, "Reached the new section of the mine", "nah");
	settings.Add("KeyItem05Ch8", false, "Gear", "nah");
	settings.Add("KeyItem03Ch8", false, "Crank", "nah");
	settings.Add("Ch8CageKey", false, "Cage Key", "nah");
	settings.Add("c08_MiningRoom01", false, "Entered soldier 2's cell", "nah");
	settings.Add("Handgun_Albert_C", false, "Handgun (Professional mode only!)", "nah");
	settings.Add("AlbertHandgunBulletL", false, "Ramrods (first time, near night vision)", "nah");
	settings.Add("MineMasterKey", false, "Clown Key", "nah");
	settings.Add("Shotgun_Albert", false, "Shotgun (Professional mode only!)", "nah");
	settings.Add("c08_TrainPassage05", false, "Saved soldier 3 (triggers in room with ladder)", "nah");
	settings.Add("c08_MiningTunnel01", false, "Reached fat molded boss room", "nah");
	settings.Add("nah_bombgone", false, "Beat fat molded/Removed bomb (triggers after leaving area after removing bomb)", "nah");
	settings.Add("c08_ShieldMachine_Entra", false, "Entered the Blast Door", "nah");
	settings.Add("c08_MineTerminal01", false, "Escaped the explosion (post-horde)", "nah");
	settings.Add("lucsbossRoom", false, "Reached the Lucas boss room", "nah");
	settings.Add("nah_end", false, "End", "nah");
	settings.Add("eoz", false, "End of Zoe");
	settings.Add("NumaItem030", false, "Cure for Type-E Infection", "eoz");
	settings.Add("eoz_usedcure", false, "Used Cure", "eoz");
	settings.Add("c09_CampSafeRoomInside", false, "Placed Zoe in Safe Room (triggers upon entering)", "eoz");
	settings.Add("c09_SteamBoat3F", false, "Reached the Boat", "eoz");
	settings.Add("NumaItem031", false, "Second Cure for Type-E Infection (on boat)", "eoz");
	settings.Add("c09_SteamBoatB1FCorridor01", false, "Defeated Jack (on boat)", "eoz");
	settings.Add("c09_MoldSwampGround05", false, "Made it through the Alligator Swamp", "eoz");
	settings.Add("c09_Cemetery", false, "Reached the Cemetery", "eoz");
	settings.Add("c09_Church", false, "Reached the Church", "eoz");
	settings.Add("CH9_WP001", false, "AMG-78", "eoz");
	settings.Add("c03_MainHouseHall", false, "Reached the Baker's Main Hall", "eoz");
	settings.Add("eoz_end", false, "End", "eoz");
}

init
{
	vars.splits = new HashSet<string>();
	vars.inventoryPtr = IntPtr.Zero;
  vars.fuse3PickedUp = 0;
  vars.fuse2PickedUp = 0;

	switch (modules.First().ModuleMemorySize)
	{
		case (241680384):
			version = "1.2";
			vars.inventoryPtr = 0x7091CA0;
			break;
		case (162590720):
			version = "cerod_nvidia";
			vars.inventoryPtr = 0x9322E10;
			break;
		case (248446976):
			version = "1.3";
			vars.inventoryPtr = 0x70DADB0;
			break;
		case (272252928):
		case (142016512):
			version = "1.4";
			vars.inventoryPtr = 0x81E4148;
			break;
		case (162668544):
		case (342978560):
			version = "cerod";
			vars.inventoryPtr = 0x93352C0;
			break;
		case (142069760):
			version = "Buy RE8!! Version";
			vars.inventoryPtr = 0x081F24E8;
			break;
		case (142065664):
			version = "12/17 Update";
			vars.inventoryPtr = 0x081F1308;
			break;
		default:
			version = "1.1";
			vars.inventoryPtr = 0x707FCD0;
			break;
	}

	// Track inventory IDs
	current.inventory = new string[20].Select((_, i) => {
		StringBuilder sb = new StringBuilder(300);
		IntPtr ptr;
		new DeepPointer(vars.inventoryPtr, 0x60, 0x20, 0x30 + (i * 8), 0x28, 0x80, 0x24).DerefOffsets(memory, out ptr);
		memory.ReadString(ptr, sb);
		return sb.ToString();
	}).ToArray();
}


start
{	
	if (settings["nah"])
	{
		return (current.map == "c04_CavePassage01" && current.inventory[0] == "CKnife" || current.map == "c04_Ship3FInfirmaryPast");
	}
	if (settings["eoz"])
	{
		return (current.map == "sm0878_Carpet08A" || current.map == "c04_Ship3FInfirmaryPast");
	}
	if (settings["meme"])
	{
		return (current.map == "c03_MainHouse1FWash" && current.inventory[0] == "Knife");
	}

	return current.map == "c04_Ship3FInfirmaryPast";
}

update
{
//print(modules.First().ModuleMemorySize.ToString());
	// Update inventory IDs
	current.inventory = new string[20].Select((_, i) => {
		StringBuilder sb = new StringBuilder(100);
		IntPtr ptr;
		new DeepPointer(vars.inventoryPtr, 0x60, 0x20, 0x30 + (i * 8), 0x28, 0x80, 0x24).DerefOffsets(memory, out ptr);
		memory.ReadString(ptr, sb);
		return sb.ToString();
	}).ToArray();

  vars.isdead = (current.isdying == 0 ? 1 : 0);
	if (timer.CurrentPhase == TimerPhase.NotRunning) { vars.splits.Clear(); vars.fuse2PickedUp = 0; vars.fuse3PickedUp = 0; }
}

split
{
	// Item splits
	string[] currentInventory = (current.inventory as string[]);
	string[] oldInventory = (old.inventory as string[]); // throws error first update, will be fine afterwards.
	if (!currentInventory.SequenceEqual(oldInventory))
	{
		string[] delta = (currentInventory as string[]).Where((v, i) => v != oldInventory[i]).ToArray();

		foreach (string item in delta)
		{
			if (item == "FuseCh4")
            {
                if (vars.fuse2PickedUp == 0 && current.map != "c04_Ship1FCorridor") 
                {
                    vars.fuse2PickedUp = 1;
                    return settings["fuse2"];
                }
                else if (vars.fuse3PickedUp == 0 && current.map == "c04_Ship1FCorridor") 
                {
                    if (settings["fuse2"])
                    {
                        if (vars.fuse2PickedUp == 1)
                        {
                            vars.fuse3PickedUp = 1;
                            return settings["fuse3"];
                        }
                    }
                    else
                    {
                        vars.fuse3PickedUp = 1;
                        return settings["fuse3"];
                    }
                    
                }
            }
            else if (!vars.splits.Contains(item))
			{
				vars.splits.Add(item);
				return settings[item];
			}
		}
	}


    //Removed item splits

    var removedItems = oldInventory.Except(currentInventory);

    if (removedItems.Contains("ChainSaw") && vars.isdead == 0)
    {
        if (!vars.splits.Contains("removedSaw"))
        {
            vars.splits.Add("removedSaw");
            return settings["chainsawduel"];
        }
    }

    if (currentInventory.Count(v => v == "SerumComplete") == 1)
    {
        if (!vars.splits.Contains("injectedJack"))
        {
            vars.splits.Add("injectedJack");
            return settings["hittheroadjack"];
        }
    }

    if (removedItems.Contains("Lantern") && vars.isdead == 0)
    {
        if (!vars.splits.Contains("usedTheLantern"))
        {
            vars.splits.Add("usedTheLantern");
            return settings["usedlantern"];
        }
    }

    if (removedItems.Contains("SerumTypeE") && vars.isdead == 0 && current.map == "c04_c013F")
    {
        if (!vars.splits.Contains("injectedEvieWithSerum"))
        {
            vars.splits.Add("injectedEvieWithSerum");
            return settings["injectedbitch"];
        }
    }

    if (removedItems.Contains("Handgun_Albert"))
    {
        if (!vars.splits.Contains("endMainCampaign"))
        {
            vars.splits.Add("endMainCampaign");
            return settings["end"];
        }
    }

    if (removedItems.Contains("MachineGun"))
    {
        if (!vars.splits.Contains("videotapeend"))
        {
            vars.splits.Add("videotapeend");
            return settings["videotapeend"];
        }
    }

	if (settings["eoz"])
	{
		if (!vars.splits.Contains(""))
		{
			vars.splits.Add("");
		}
	}
	
	if (removedItems.Contains("NumaItem030"))
    {
        if (!vars.splits.Contains("zoecuresplit"))
        {
            vars.splits.Add("zoecuresplit");
            return settings["eoz_usedcure"];
        }
    }

	// Map splits
    if (current.map != old.map)
	{
		if (current.map == "c08_MiningPassage02_static" && old.map == "c08_MiningTunnel01" && !vars.splits.Contains(current.map) || current.map == "c08_MiningPassage02" && old.map == "c08_MiningTunnel01" && !vars.splits.Contains(current.map))
        {
            vars.splits.Add(current.map);
            return settings["nah_bombgone"];
        }
        else if (!vars.splits.Contains(current.map))
		{
			vars.splits.Add(current.map);
			return settings[current.map];
		}
	}

	if (current.map == "c01_Outside01" && current.inventory[0] != "MailMia" && settings["100family"] == false && !vars.splits.Contains("welcome2family"))
	{
		vars.splits.Add("welcome2family");
		return settings["welcome2family"];
	}
	
	if (current.map == "c03_MainHouse1FLDK" && !vars.splits.Contains("100family"))
	{
		vars.splits.Add("100family");
		return settings["100family"];
	}
	
	if (current.map == "c08_BossRoom01" && !vars.splits.Contains("lucsbossRoom"))
	{
		vars.splits.Add("lucsbossRoom");
		return settings["lucsbossRoom"];
	}

    if (old.map == "c08_BossRoom01" && current.inventory[0] != old.inventory[0] && !vars.splits.Contains("nah_end"))
	{
		vars.splits.Add("nah_end");
		return settings["nah_end"];
	}

    if (current.map == "c03_MainHouseHall" && current.inventory[0] != "NumaItem031" && !vars.splits.Contains("eoz_end"))
	{
		vars.splits.Add("eoz_end");
		return settings["eoz_end"];
	}
}

isLoading
{
	return current.gamePauseState != 0 && current.gamePauseState != 262400 && current.gamePauseState != 8 && current.gamePauseState != 262144;
}
