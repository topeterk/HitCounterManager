//Resident Evil 3 Remake Autosplitter
//By CursedToast 04/03/2020
//Special thanks to Squirrelies for collaborating in finding memory values.
//Last updated 12/17/2021

state("re3", "12/17 Update")
{
	int map : "re3.exe", 0x54DF0F8, 0x88;
	int bossHP : "re3.exe", 0x8D868B0, 0x30, 0x20, 0x300, 0x58;
	int weapon1 : "re3.exe", 0x8D85BA0, 0x50, 0x98, 0x10, 0x20, 0x18, 0x10, 0x14;
	long active :  0x8DB3BA0, 0x60, 0x18;
	long cutscene :  0x8DB3BA0, 0x60, 0x20;
	long paused :  0x8DB3BA0, 0x60, 0x30;
}

state("re3", "1.3")
{
	int map : "re3.exe", 0x054DC0F8, 0x88;
	int bossHP : "re3.exe", 0x08D838B0, 0x30, 0x20, 0x300, 0x58;
	int weapon1 : "re3.exe", 0x08D82BA0, 0x50, 0x98, 0x10, 0x20, 0x18, 0x10, 0x14;
	long active :  0x08DB0BA0, 0x60, 0x18;
	long cutscene :  0x08DB0BA0, 0x60, 0x20;
	long paused :  0x08DB0BA0, 0x60, 0x30;
}

state("re3", "1.2")
{
	int map : "re3.exe", 0x054E30F8, 0x88;
	int bossHP : "re3.exe", 0x08D8A8A0, 0x30, 0x20, 0x300, 0x58;
	int weapon1 : "re3.exe", 0x08D89B90, 0x50, 0x98, 0x10, 0x20, 0x18, 0x10, 0x14;
	long active :  0x08DB7B90, 0x60, 0x18;
	long cutscene :  0x08DB7B90, 0x60, 0x20;
	long paused :  0x08DB7B90, 0x60, 0x30;
}

state("re3", "1.1")
{
	int map : "re3.exe", 0x054190F8, 0x88;
	int bossHP : "re3.exe", 0x08CB8618, 0x30, 0x20, 0x300, 0x58;
	int weapon1 : "re3.exe", 0x08CBA618, 0x50, 0x98, 0x10, 0x20, 0x18, 0x10, 0x14;
	long active :  0x08CE8430, 0x60, 0x18;
	long cutscene :  0x08CE8430, 0x60, 0x20;
	long paused :  0x08CE8430, 0x60, 0x30;
}

state("re3", "1.0")
{
	int map : "re3.exe", 0x054DB0F8, 0x88;
	int bossHP : "re3.exe", 0x08D7A5A8, 0x30, 0x20, 0x300, 0x58;
	int weapon1 : "re3.exe", 0x08D7C5E8, 0x50, 0x98, 0x10, 0x20, 0x18, 0x10, 0x14;
	long active :  0x08DAA3F0, 0x60, 0x18;
	long cutscene :  0x08DAA3F0, 0x60, 0x20;
	long paused :  0x08DAA3F0, 0x60, 0x30;
}

state("re3", "Cero Z 1.0")
{
	int map : "re3.exe", 0x054190F8, 0x88;
	int bossHP : "re3.exe", 0x08CB8618, 0x30, 0x20, 0x300, 0x58;
	int weapon1 : "re3.exe", 0x08CBA618, 0x50, 0x98, 0x10, 0x20, 0x18, 0x10, 0x14;
	long active :  0x08CE8430, 0x60, 0x18;
	long cutscene :  0x08CE8430, 0x60, 0x20;
	long paused :  0x08CE8430, 0x60, 0x30;
}

startup
{
	settings.Add("part1", true, "Part 1");
	settings.Add("escapedApartment", true, "Escaped Apartment", "part1");
	settings.Add("byeBrad", true, "Escaped Bar Jack", "part1");
	settings.Add("dario", true, "Met Dario", "part1");
	settings.Add("garageRoof", true, "Parking Garage", "part1");
	settings.Add("reachedSubway", true, "Reached Subway (first time)", "part1");
	settings.Add("drinkOfWater", true, "Tall Drink of Water (cutscene)", "part1");
	settings.Add("kitebros", true, "Reached Kitebros", "part1");
	settings.Add("fireHose", true, "Fire Hose", "part1");
	settings.Add("murphy", true, "Reached Murphy", "part1");
	settings.Add("boltCutters", false, "Boltcutters", "part1");
	
	settings.Add("part2", true, "Part 2");
	settings.Add("powerSubstation", true, "Reached Power Substation", "part2");
	settings.Add("lockPick", true, "Lockpick (In Box)", "part2");
	settings.Add("finishedPowerSubstation", true, "Finished Power Substation", "part2");
	settings.Add("kitebrosControl", true, "Kitebros Control Room", "part2");
	settings.Add("reachedSewers", true, "Reached Sewers", "part2");
	settings.Add("batteryPack", true, "Battery Pack (Sewers Key)", "part2");
	settings.Add("sewersExit", true, "Escaped Sewers", "part2");
	settings.Add("flameNemmy", true, "Nemesis 1 (Flamethrower)", "part2");
		settings.Add("kendos", true, "Reached Kendo's", "part2");
	settings.Add("kendoKey", true, "Kendo's Gate Key", "part2");
	settings.Add("escapedRocketNemmy", true, "Escaped Rocket Nemesis", "part2");
	
	settings.Add("part3", true, "Part 3");
	settings.Add("carlosRPDStart", true, "Carlos RPD Start", "part3");
	settings.Add("battery", true, "Battery (for detonator)", "part3");
	settings.Add("emptyDetonator", true, "Detonator", "part3");
	settings.Add("stars", true, "Reached STARS Office", "part3");
	
	settings.Add("part4", true, "Part 4");
	settings.Add("subwayTunnelExit", true, "Exited Subway Tunnel", "part4");
	settings.Add("clockNemmy", true, "Nemesis 2 (Clocktower)", "part4");
	settings.Add("tapePlayer", true, "Tape Player", "part4");
	settings.Add("lockerKey", true, "Locker Key", "part4");
	settings.Add("hospitalIdCard", true, "Hospital ID Card", "part4");
	settings.Add("cassette", true, "Audiocassette Tape", "part4");
	settings.Add("vaccine", true, "Vaccine", "part4");
	settings.Add("defendedJill", true, "Defended Jill", "part4");
	
	settings.Add("part5", true, "Part 5");
	settings.Add("hospitalLift", true, "Hospital Underground Lift", "part5");
	settings.Add("fuse1", true, "Fuse 1", "part5");
	settings.Add("fuse2", true, "Fuse 2", "part5");
	settings.Add("fuse3", true, "Fuse 3", "part5");
	
	settings.Add("part6", true, "Part 6");
	settings.Add("nest2", true, "Reached NEST2", "part6");
	settings.Add("overrideKey", true, "Override Key", "part6");
	settings.Add("cultureSample", true, "Culture Sample", "part6");
	settings.Add("liquidTube", true, "Liquid Filled Test Tube", "part6");
	settings.Add("vaccineBase", true, "Vaccine Base", "part6");
	settings.Add("vaccineCompleted", false, "Vaccine", "part6");
	settings.Add("disposalNemmy", true, "Nemesis 3 (Waste Disposal)", "part6");
	settings.Add("finalNemmy", true, "Nemesis 4 (Final)", "part6");
	settings.Add("end", true, "The Last Escape (End)", "part6");
}

init
{
	vars.inventoryPtr = IntPtr.Zero;
    
    vars.fas = 0;
	
	switch (modules.First().ModuleMemorySize)
	{
		default:
			version = "Unknown Version - Contact CT";
			vars.inventoryPtr = 0x08D7C5E8;
			break;
		case (613351424):
			version = "1.0";
			vars.inventoryPtr = 0x08D7C5E8;
			break;
		case (156102656):
			version = "12/17 Update";
			vars.inventoryPtr = 0x8D85BA0;
			break;
		case (609722368):
			version = "1.1";
			vars.inventoryPtr = 0x08CBA618;
			break;
		case (603697152):
			version = "1.2";
			vars.inventoryPtr = 0x08D89B90;
			break;
		case (645672960):
			version = "1.2";
			vars.inventoryPtr = 0x08D89B90;
			break;
		case (156090368):
			version = "1.3";
			vars.inventoryPtr = 0x08D82BA0;
			break;
		case (640299008):
			version = "Cero Z 1.0";
			vars.inventoryPtr = 0x08CBA618;
			break;
	}

    // Track inventory IDs
    current.inventory = new int[20];
    for (int i = 0; i < current.inventory.Length; ++i)
    {
        int itemID = 0;
        IntPtr ptr;
        new DeepPointer(vars.inventoryPtr, 0x50, 0x98, 0x10, 0x20 + (i * 8), 0x18, 0x10, 0x10).DerefOffsets(memory, out ptr);
        memory.ReadValue<int>(ptr, out itemID);
        current.inventory[i] = itemID;
    }
}


start
{	
	if (current.map == 1)
	{
		return true;
	}
}

update
{
	//print(modules.First().ModuleMemorySize.ToString());
	// Track inventory IDs
    current.inventory = new int[20];
    for (int i = 0; i < current.inventory.Length; ++i)
    {
        int itemID = 0;
        IntPtr ptr;
        new DeepPointer(vars.inventoryPtr, 0x50, 0x98, 0x10, 0x20 + (i * 8), 0x18, 0x10, 0x10).DerefOffsets(memory, out ptr);
        memory.ReadValue<int>(ptr, out itemID);
        current.inventory[i] = itemID;
    }
	
	if (timer.CurrentPhase == TimerPhase.NotRunning)
	{
		vars.reachedEnd = 0;
		vars.battery = 0;
		vars.emptyDetonator = 0;
		vars.tapePlayer = 0;
		vars.lockerKey = 0;
		vars.hospitalIdCard = 0;
		vars.cassette = 0;
		vars.vaccine = 0;
		vars.fuse1 = 0;
		vars.fuse2 = 0;
		vars.fuse3 = 0;
		vars.overrideKey = 0;
		vars.cultureSample = 0;
		vars.liquidTube = 0;
		vars.vaccineBase = 0;
		vars.fireHose = 0;
		vars.lockPick = 0;
		vars.batteryPack = 0;
		vars.kendoKey = 0;
		vars.boltCutters = 0;
		vars.escapedApartment = 0;
		vars.byeBrad = 0;
		vars.dario = 0;
		vars.garageRoof = 0;
		vars.reachedSubway = 0;
		vars.drinkOfWater = 0;
		vars.kitebros = 0;
		vars.murphy = 0;
		vars.powerSubstation = 0;
		vars.finishedPowerSubstation = 0;
		vars.kitebrosControl = 0;
		vars.reachedSewers = 0;
		vars.sewersExit = 0;
		vars.flameNemmy = 0;
		vars.kendos = 0;
		vars.escapedRocketNemmy = 0;
		vars.carlosRPDStart = 0;
		vars.stars = 0;
		vars.subwayTunnelExit = 0;
		vars.clockNemmy = 0;
		vars.defendedJill = 0;
		vars.hospitalLift = 0;
		vars.nest2 = 0;
		vars.vaccineCompleted = 0;
		vars.disposalNemmy = 0;
		vars.finalNemmy = 0;
		vars.end = 0;
	}
	
	if (current.map == 323)
	{
		vars.reachedEnd = 1;
	}
}

split
{
	// Item splits
    int[] currentInventory = (current.inventory as int[]);
    int[] oldInventory = (old.inventory as int[]); // throws error first update, will be fine afterwards.

    for (int i = 0; i < currentInventory.Length; i++)
    {
        if (currentInventory[i] != oldInventory[i])
        {
			switch (oldInventory[i])
			{
				case 0x000000D9:
				{
					if (vars.defendedJill == 0)
					{
						vars.defendedJill = 1;
						return settings["defendedJill"];
					}
					break;
				}
				default:
					break;
			}
			
			switch (currentInventory[i])
            {
				case 0x000000A1:
				{
					if (vars.battery == 0)
					{
						vars.battery = 1;
						return settings["battery"];
					}
					break;
				}
				case 0x000000A5:
				{
					if (vars.emptyDetonator == 0)
					{
						vars.emptyDetonator = 1;
						return settings["emptyDetonator"];
					}
					break;
				}
				case 0x000000D6:
				{
					if (vars.tapePlayer == 0)
					{
						vars.tapePlayer = 1;
						return settings["tapePlayer"];
					}
					break;
				}
				case 0x000000DA:
				{
					if (vars.lockerKey == 0)
					{
						vars.lockerKey = 1;
						return settings["lockerKey"];
					}
					break;
				}
				case 0x000000D3:
				{
					if (vars.hospitalIdCard == 0)
					{
						vars.hospitalIdCard = 1;
						return settings["hospitalIdCard"];
					}
					break;
				}
				case 0x000000D5:
				{
					if (vars.cassette == 0)
					{
						vars.cassette = 1;
						return settings["cassette"];
					}
					break;
				}
				case 0x000000D7:
				{
					if (vars.vaccine == 0)
					{
						vars.vaccine = 1;
						return settings["vaccine"];
					}
					break;
				}
				case 0x000000DE:
				{
					if (vars.fuse3 == 0)
					{
						vars.fuse3 = 1;
						return settings["fuse3"];
					}
					break;
				}
				case 0x000000E0:
				{
					if (vars.fuse1 == 0)
					{
						vars.fuse1 = 1;
						return settings["fuse1"];
					}
					break;
				}
				case 0x000000DF:
				{
					if (vars.fuse2 == 0)
					{
						vars.fuse2 = 1;
						return settings["fuse2"];
					}
					break;
				}
				case 0x000000E8:
				{
					if (vars.overrideKey == 0)
					{
						vars.overrideKey = 1;
						return settings["overrideKey"];
					}
					break;
				}
				case 0x000000EA:
				{
					if (vars.cultureSample == 0)
					{
						vars.cultureSample = 1;
						return settings["cultureSample"];
					}
					break;
				}
				case 0x000000EB:
				{
					if (vars.liquidTube == 0)
					{
						vars.liquidTube = 1;
						return settings["liquidTube"];
					}
					break;
				}
				case 0x000000EC:
				{
					if (vars.vaccineBase == 0)
					{
						vars.vaccineBase = 1;
						return settings["vaccineBase"];
					}
					break;
				}
				case 0x000000B5:
				{
					if (vars.fireHose == 0)
					{
						vars.fireHose = 1;
						return settings["fireHose"];
					}
					break;
				}
				case 0x000000B9:
				{
					if (vars.lockPick == 0)
					{
						vars.lockPick = 1;
						return settings["lockPick"];
					}
					break;
				}
				case 0x000000BA:
				{
					if (vars.batteryPack == 0)
					{
						vars.batteryPack = 1;
						return settings["batteryPack"];
					}
					break;
				}
				case 0x000000B6:
				{
					if (vars.kendoKey == 0)
					{
						vars.kendoKey = 1;
						return settings["kendoKey"];
					}
					break;
				}
				case 0x00000098:
				{
					if (vars.boltCutters == 0)
					{
						vars.boltCutters = 1;
						return settings["boltCutters"];
					}
					break;
				}
				case 0x000000E9:
				{
					if (vars.vaccineCompleted == 0)
					{
						vars.vaccineCompleted = 1;
						return settings["vaccineCompleted"];
					}
					break;
				}
				
                default:
                {
                    break; // No work to do.
                }
            }
        }
    }
	
	// Map splits
	if (current.map != old.map)
	{
		if (current.map == 12 && vars.escapedApartment == 0)
		{
			vars.escapedApartment = 1;
			return settings["escapedApartment"];
		}
		
		if (current.map == 18 && vars.byeBrad == 0)
		{
			vars.byeBrad = 1;
			return settings["byeBrad"];
		}
		
		if (current.map == 21 && vars.dario == 0)
		{
			vars.dario = 1;
			return settings["dario"];
		}
		
		if (current.map == 26 && vars.garageRoof == 0)
		{
			vars.garageRoof = 1;
			return settings["garageRoof"];
		}
		
		if (current.map == 187 && vars.reachedSubway == 0)
		{
			vars.reachedSubway = 1;
			return settings["reachedSubway"];
		}
		
		if (current.map == 142 && vars.drinkOfWater == 0)
		{
			vars.drinkOfWater = 1;
			return settings["drinkOfWater"];
		}
		
		if (current.map == 203 && vars.kitebros == 0)
		{
			vars.kitebros = 1;
			return settings["kitebros"];
		}
		
		if (current.map == 155 && vars.murphy == 0)
		{
			vars.murphy = 1;
			return settings["murphy"];
		}
		
		if (current.map == 199 && vars.powerSubstation == 0)
		{
			vars.powerSubstation = 1;
			return settings["powerSubstation"];
		}
		
		if (current.map == 206 && vars.reachedSewers == 0)
		{
			vars.reachedSewers = 1;
			return settings["reachedSewers"];
		}
		
		if (current.map == 162 && vars.sewersExit == 0)
		{
			vars.sewersExit = 1;
			return settings["sewersExit"];
		}
		
		if (current.map == 124 && vars.kendos == 0)
		{
			vars.kendos = 1;
			return settings["kendos"];
		}
		
		if (current.map == 138 && vars.escapedRocketNemmy == 0)
		{
			vars.escapedRocketNemmy = 1;
			return settings["escapedRocketNemmy"];
		}
		
		if (current.map == 36 && vars.carlosRPDStart == 0)
		{
			vars.carlosRPDStart = 1;
			return settings["carlosRPDStart"];
		}
		
		if (current.map == 71 && vars.stars == 0)
		{
			vars.stars = 1;
			return settings["stars"];
		}
		
		if (current.map == 240 && vars.subwayTunnelExit == 0)
		{
			vars.subwayTunnelExit = 1;
			return settings["subwayTunnelExit"];
		}
		
		if (current.map == 294 && vars.hospitalLift == 0)
		{
			vars.hospitalLift = 1;
			return settings["hospitalLift"];
		}
		
		if (current.map == 324 && vars.nest2 == 0)
		{
			vars.nest2 = 1;
			return settings["nest2"];
		}
	}
	
	//End split
	if (vars.reachedEnd == 1 && current.map == 0 && current.weapon1 != old.weapon1)
	{
		vars.end = 1;
		return settings["end"];
	}
	
	if (current.map == 200 && current.cutscene > old.cutscene && vars.finishedPowerSubstation == 0)
	{
		vars.finishedPowerSubstation = 1;
		return settings["finishedPowerSubstation"];
	}
		
	if (current.map == 204 && current.cutscene > old.cutscene && vars.kitebrosControl == 0)
	{
		vars.kitebrosControl = 1;
		return settings["kitebrosControl"];
	}
	
	if (current.map == 242 && current.bossHP < 1 && vars.clockNemmy == 0)
	{
		vars.clockNemmy = 1;
		return settings["clockNemmy"];
	}
	
	if (current.map == 231 && current.bossHP < 1 && vars.flameNemmy == 0)
	{
		vars.flameNemmy = 1;
		return settings["flameNemmy"];
	}
	
	if (current.map == 316 && !(current.bossHP >= 1) && vars.disposalNemmy == 0)
	{
		vars.disposalNemmy = 1;
		return settings["disposalNemmy"];
	}
	
	if (current.map == 319 && current.bossHP < 5000  && vars.finalNemmy == 0)
	{
		vars.finalNemmy = 1;
		return settings["finalNemmy"];
	}
}

gameTime
{
	return TimeSpan.FromSeconds((current.active - current.cutscene - current.paused) / 1000000.0);
}

isLoading
{
	return true;
}
