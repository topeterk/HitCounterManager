/*
By deserteagle417
Thanks to Dchaps for the original scripts and for finding the memory values and to Darazanjoll and StevenMayte 
for later versions of the scritps. I only contributed the seconds value for hunk to the memory values.
Thanks to StevenMayte for giving me a run of the EX Battle optimal route with roomIDs up so I didn't have to.
Thanks to Gemini for providing me with the game code for displaying the timer during Hunk/Tofu and the address for its frame counter.
Thanks to CarcinogenSDA for the checkpoint splits, I just fixed a couple missing splits (lab ladder and Irons elevator).
Thanks to Mysterion for help with my RE0 autosplitter, which has become the basis for several of my ASLs now.
*/

state("bio2", "v1.0")
{
    uint time       : 0x280538;
    short hp        : 0x589FE6;
    uint gameState  : 0x589E0C;
    byte frame      : 0x28053C;
    byte roomID     : 0x58EAB6;
    byte oldRoom    : 0x58EABA;
    byte stageID    : 0x58EAB4;
    byte camID      : 0x58EAB8;
    byte hunkSec    : 0x58EACA;
    byte hunkFrames : 0x289774;
}

state("bio2 v1.1", "v1.1")
{
    uint time       : 0x280588;
    short hp        : 0x58A046;
    uint gameState  : 0x589E6C;
    byte frame      : 0x28058C;
    byte roomID     : 0x58EB16;
    byte oldRoom    : 0x58EB1A;
    byte stageID    : 0x58EB14;
    byte camID      : 0x58EB18;
    byte hunkSec    : 0x58EB2A;
    byte hunkFrames : 0x2897C4;
}

startup
{   
    //Enable Door Splits
	settings.Add("doors", true, "Enable door splits for every door in the route");
        settings.CurrentDefaultParent = "doors";
		settings.Add("leona", true, "Leon A Door Splits (168 total splits)");
		settings.Add("clairea", false, "Claire A Door Splits (185 total splits)");
        settings.Add("leonb", false, "Leon B Door Splits (172 total splits)");
        settings.Add("claireb", false, "Claire B Door Splits (178 total splits)");
            settings.SetToolTip("claireb", "If you're using the old Claire B splits, you'll need to add an extra split after split 121.");
        settings.Add("low", false, "Remove Magnum Doors for Low% and/or NG+");
            settings.SetToolTip("low", "Skips Watchman's Room in Leon A and STARS Office in Leon B.");
        settings.Add("hunk", false, "4th Survivor/Tofu Door Splits (25 total splits)");
        settings.CurrentDefaultParent = null;
    
    //EX Battle Splits
    settings.Add("ex", false, "EX Battle. Choose below which style of splits to use for Stage 3");
        settings.SetToolTip("ex", "Must go toward parking garage at the end of Stage 2, not Irons' Office.");
        settings.CurrentDefaultParent = "ex";
        settings.Add("bombs", false, "Stage 3 splits on bomb pickups.");
        settings.Add("exdoors", false, "Stage 3 splits every door transition.");
        settings.Add("degen", false, "Degen mode: optimal route only. Doors and bombs split.");
            settings.SetToolTip("degen", "Requires getting the best 4 bombs and resetting if you do not, otherwise splits will be messed up.");
        settings.CurrentDefaultParent = null;
    
    //Enable Set Splits
	settings.Add("set", false, "Enable Set Splits");
        settings.CurrentDefaultParent = "set";
		settings.Add("setla", false, "Leon A Set Splits (12 total splits)");
		settings.Add("setca", false, "Claire A Set Splits (15 total splits)");
        settings.Add("setlb", false, "Leon B Set Splits (11 total splits)");
        settings.Add("setcb", false, "Claire B Set Splits (14 total splits)");
        settings.CurrentDefaultParent = null;
    
    //Enable Checkpoint Splits
    settings.Add("check", false, "Enable Checkpoint Splits (disables all of the above options)");
        settings.SetToolTip("check", "Splits on every door transition but will hold up at certain checkpoints based on specific split names.");

    //Basic Door Splits
    settings.Add("basic", false, "Basic Door Splits (disables all of the above options)");
        settings.SetToolTip("basic", "Splits on every door transition.");
    
    //Common Items
	vars.CommonItems = new List<int>()
    {
        88, 53, 71, 89, 67, 
                50, 90, 91, 
        92, 74, 72, 73, 93, 
        94, 77, 76, 97, 96, 
        95, 98, 99, 84, 17
    };

	vars.CommonItemSettings = new List<String>()
	{
        "Cabin Key", "Blue Keycard", "Unicorn Medal", "Spade Key", "Square Crank",
        "Red Jewel (Statue)", "Red Jewel (Fireplace)", "Valve Handle", "Diamond Key", "Heart Key", 
        "Club Key", "Gold Cog", "Eagle Medal", "Wolf Medal", "Down Key", 
        "Up Key", "Fuse Case", "Main Fuse", "Umbrella Keycard", "MO Disk", 
        "Power Room Key", "Master Key", "Platform Key", "Joint Plug Blue", "Rocket Launcher"
    };

	settings.Add("item", false, "Common Key Item Splits");
		settings.CurrentDefaultParent = "item";
		for(int i = 0; i < 5; i++)
        {
        	settings.Add("" + vars.CommonItems[i].ToString(), true, "" + vars.CommonItemSettings[i].ToString());
    	}
        settings.Add("red1", true, "" + vars.CommonItemSettings[5].ToString());
        settings.Add("red2", true, "" + vars.CommonItemSettings[6].ToString());
        for(int i = 5; i < 23; i++){
        	settings.Add("" + vars.CommonItems[i].ToString(), true, "" + vars.CommonItemSettings[i+2].ToString());
    	}
		settings.CurrentDefaultParent = null;

    //Leon Specific Items
	vars.LeonItems = new List<int>()
    {
        59, 62, 75, 5, 60, 61
    };

	vars.LeonItemSettings = new List<String>()
	{
        "Bishop Plug", "King Plug", "Manhole Opener", "Magnum", "Rook Plug", "Knight Plug"
    };

	settings.Add("itemL", false, "Leon Specific Key Item Splits");
		settings.CurrentDefaultParent = "itemL";
		for(int i = 0; i < 6; i++)
        {
        	settings.Add("" + vars.LeonItems[i].ToString(), true, "" + vars.LeonItemSettings[i].ToString());
    	}
		settings.CurrentDefaultParent = null;

    //Claire Specific Items
	vars.ClaireItems = new List<int>()
    {
        9, 47, 65, 64, 56, 58, 57, 54, 81, 79, 78
    };

	vars.ClaireItemSettings = new List<String>()
	{
        "Grenade Launcher", "Lighter", "Explosive", "Detonator", "Jaguar Stone (L)", "Eagle Stone", "Jaguar Stone (R)", "Serpent Stone", "Vaccine Cartridge", "Vaccine Base", "Vaccine"
    };

	settings.Add("itemC", false, "Claire Specific Key Item Splits");
		settings.CurrentDefaultParent = "itemC";
		for(int i = 0; i < 11; i++)
        {
        	settings.Add("" + vars.ClaireItems[i].ToString(), true, "" + vars.ClaireItemSettings[i].ToString());
    	}
		settings.CurrentDefaultParent = null;

    //Extra Items to Split On
    vars.ExtraItems = new List<int>()
	{
        32,  4,  7, 34,  8, 
        33,  6, 12, 13, 14, 
        15, 16, 18, 52, 63, 
        85, 55, 66, 83, 86, 
        68, 69, 70, 80
    };

	vars.ExtraItemsSettings = new List<String>()
	{
        "Handgun Parts", "Custom Handgun", "Shotgun", "Shotgun Parts", "Custom Shotgun",
        "Magnum Parts", "Custom Magnum", "Bowgun", "Colt SAA", "Spark Shot", 
        "Sub Machine Gun", "Flamethrower", "Gatling Gun", "Red Keycard", "W. Box Key", 
        "Joint Plug Red", "Jaguar Stone", "Detonator + Explosive", "Special Key", "Cord", 
        "Film A", "Film B", "Film C", "Film D"
    };

	//Additional Item Split Options
	settings.Add("extra", false, "Additional Items to Split On");
		settings.CurrentDefaultParent = "extra";
		for(int i = 0; i < 24; i++)
        {
        	settings.Add("" + vars.ExtraItems[i].ToString(), false, "" + vars.ExtraItemsSettings[i].ToString());
    	}
		settings.CurrentDefaultParent = null;

    //Message to Switch to Game Time
	if (timer.CurrentTimingMethod == TimingMethod.RealTime)
    {        
        var timingMessage = MessageBox.Show (
            "This game uses In-Game Time (IGT) as the timing method.\n"+
            "LiveSplit is currently set to show Real Time (RTA).\n"+
            "Would you like to set the timing method to Game Time? This is required for verification.",
            "LiveSplit | Resident Evil 2",
            MessageBoxButtons.YesNo,MessageBoxIcon.Question);
        if (timingMessage == DialogResult.Yes)
        {
            timer.CurrentTimingMethod = TimingMethod.GameTime;
        }
    }
}

init
{    
    //Initialize Variables
    vars.completedSplits = new List<int>();
	vars.Inventory = new byte[10];
	vars.doorIterator = 0;
    vars.versionDetector = 0;
    vars.redJewels = 0;
    vars.redJewelsPrevious = 0;
    vars.fireplaceJewel = 0;
    vars.statueJewel = 0;
    vars.stage1 = 0;
    vars.stage2 = 0;
    vars.bombCounter = 0;
    vars.bombCounterPrevious = 0;
    vars.vanilla = 1;
    vars.endSplitFlag = 0;

    //Determine Version
    switch (modules.First().ModuleName)
	{
		case ("bio2.exe"):
            vars.versionDetector = 1;
			version = "v1.0";
			break;
        case ("bio2 v1.1.exe"):
			version = "v1.1";
			break;
	};

    //Leon A Doors List -- skip 64 for Ada Shot
    vars.leonADoors = new List<Tuple<int, int, int>>
    {
        Tuple.Create(  0,  0,  1), Tuple.Create(  1,  1,  2), Tuple.Create(  2,  2, 24), Tuple.Create(  3, 24, 25), Tuple.Create(  4, 25, 26), Tuple.Create(  5, 26, 27), Tuple.Create(  6, 27,  3), Tuple.Create(  7,  3,  0), Tuple.Create(  8,  0,  2), Tuple.Create(  9,  2,  0),
        Tuple.Create( 10,  0,  1), Tuple.Create( 11,  1,  4), Tuple.Create( 12,  4,  5), Tuple.Create( 13,  5,  7), Tuple.Create( 14,  7, 12), Tuple.Create( 15, 12, 20), Tuple.Create( 16, 20, 21), Tuple.Create( 17, 21, 20), Tuple.Create( 18, 20, 12), Tuple.Create( 19, 12,  7),
        Tuple.Create( 20,  7,  5), Tuple.Create( 21,  5,  4), Tuple.Create( 22,  4,  1), Tuple.Create( 23,  1,  0), Tuple.Create( 24,  0,  1), Tuple.Create( 25,  1,  4), Tuple.Create( 26,  4,  3), Tuple.Create( 27,  3,  4), Tuple.Create( 28,  4,  5), Tuple.Create( 29,  5,  6),
        Tuple.Create( 30,  6,  5), Tuple.Create( 31,  5,  7), Tuple.Create( 32,  7, 12), Tuple.Create( 33, 12, 20), Tuple.Create( 34, 20, 19), Tuple.Create( 35, 19, 18), Tuple.Create( 36, 18, 16), Tuple.Create( 37, 16, 15), Tuple.Create( 38, 15, 10), Tuple.Create( 39, 10,  9),
        Tuple.Create( 40,  9,  8), Tuple.Create( 41,  8,  7), Tuple.Create( 42,  7,  6), Tuple.Create( 43,  6,  7), Tuple.Create( 44,  7,  8), Tuple.Create( 45,  8,  9), Tuple.Create( 46,  9, 10), Tuple.Create( 47, 10, 11), Tuple.Create( 48, 11, 10), Tuple.Create( 49, 10, 15),
        Tuple.Create( 50, 15, 16), Tuple.Create( 51, 16,  0), Tuple.Create( 52,  0,  1), Tuple.Create( 53,  1,  4), Tuple.Create( 54,  4,  5), Tuple.Create( 55,  5,  7), Tuple.Create( 56,  7,  9), Tuple.Create( 57,  9,  2), Tuple.Create( 58,  2,  0), Tuple.Create( 59,  0, 10),
        Tuple.Create( 60, 10, 11), Tuple.Create( 61, 11, 15), Tuple.Create( 62, 15, 17), Tuple.Create( 63, 17, 22),                            Tuple.Create( 65, 22, 25), Tuple.Create( 66, 25,  1), Tuple.Create( 67,  1, 25), Tuple.Create( 68, 25, 26), Tuple.Create( 69, 26,  2),
        Tuple.Create( 70,  2, 11), Tuple.Create( 71, 11,  3), Tuple.Create( 72,  3, 11), Tuple.Create( 73, 11,  5), Tuple.Create( 74,  5,  6), Tuple.Create( 75,  6,  5), Tuple.Create( 76,  5, 11), Tuple.Create( 77, 11,  2), Tuple.Create( 78,  2, 26), Tuple.Create( 79, 26, 25),
        Tuple.Create( 80, 25, 22), Tuple.Create( 81, 22, 17), Tuple.Create( 82, 17, 15), Tuple.Create( 83, 15, 16), Tuple.Create( 84, 16, 15), Tuple.Create( 85, 15, 11), Tuple.Create( 86, 11, 10), Tuple.Create( 87, 10, 12), Tuple.Create( 88, 12, 14), Tuple.Create( 89, 14, 12),
        Tuple.Create( 90, 12, 13), Tuple.Create( 91, 13, 12), Tuple.Create( 92, 12, 10), Tuple.Create( 93, 10,  0), Tuple.Create( 94,  0, 16), Tuple.Create( 95, 16, 18), Tuple.Create( 96, 18, 23), Tuple.Create( 97, 23, 17), Tuple.Create( 98, 17, 25), Tuple.Create( 99, 25,  1),
        Tuple.Create(100,  1, 25), Tuple.Create(101, 25, 26), Tuple.Create(102, 26,  2), Tuple.Create(103,  2, 11), Tuple.Create(104, 11,  3), Tuple.Create(105,  3,  4), Tuple.Create(106,  4,  2), Tuple.Create(107,  2,  3), Tuple.Create(108,  3,  4), Tuple.Create(109,  4,  5),
        Tuple.Create(110,  5, 11), Tuple.Create(111, 11,  9), Tuple.Create(112,  9, 16), Tuple.Create(113, 16,  4), Tuple.Create(114,  4,  5), Tuple.Create(115,  5,  7), Tuple.Create(116,  7,  8), Tuple.Create(117,  8, 10), Tuple.Create(118, 10, 16), Tuple.Create(119, 16,  9),
        Tuple.Create(120,  9, 11), Tuple.Create(121, 11,  5), Tuple.Create(122,  5,  7), Tuple.Create(123,  7, 13), Tuple.Create(124, 13, 14), Tuple.Create(125, 14,  0), Tuple.Create(126,  0,  1), Tuple.Create(127,  1,  2), Tuple.Create(128,  2,  8), Tuple.Create(129,  8,  3),
        Tuple.Create(130,  3,  4), Tuple.Create(131,  4,  5), Tuple.Create(132,  5,  4), Tuple.Create(133,  4,  5), Tuple.Create(134,  5,  9), Tuple.Create(135,  9,  5), Tuple.Create(136,  5,  7), Tuple.Create(137,  7,  6), Tuple.Create(138,  6,  8), Tuple.Create(139,  8, 12),
        Tuple.Create(140, 12, 13), Tuple.Create(141, 13, 12), Tuple.Create(142, 12,  8), Tuple.Create(143,  8, 11), Tuple.Create(144, 11, 14), Tuple.Create(145, 14, 14), Tuple.Create(146, 14, 22), Tuple.Create(147, 22, 18), Tuple.Create(148, 18, 20), Tuple.Create(149, 20, 21),
        Tuple.Create(150, 21, 20), Tuple.Create(151, 20, 18), Tuple.Create(152, 18, 22), Tuple.Create(153, 22, 14), Tuple.Create(154, 14, 14), Tuple.Create(155, 14, 11), Tuple.Create(156, 11,  8), Tuple.Create(157,  8, 12), Tuple.Create(158, 12, 16), Tuple.Create(159, 16, 12),
        Tuple.Create(160, 12, 17), Tuple.Create(161, 17, 11), Tuple.Create(162, 11, 14), Tuple.Create(163, 14, 14), Tuple.Create(164, 14, 22), Tuple.Create(165, 22, 23), Tuple.Create(166, 23,  0)
    };

    //Claire A Doors List -- skip 65 for Irons Cutscene and 114 for re-entering elevator after getting Sherry after G embryo fight
    vars.claireADoors = new List<Tuple<int, int, int>>
    {
        Tuple.Create(  0,  0,  1), Tuple.Create(  1,  1,  2), Tuple.Create(  2,  2, 24), Tuple.Create(  3, 24, 25), Tuple.Create(  4, 25, 26), Tuple.Create(  5, 26, 27), Tuple.Create(  6, 27,  3), Tuple.Create(  7,  3,  0), Tuple.Create(  8,  0,  2), Tuple.Create(  9,  2,  0),
        Tuple.Create( 10,  0,  1), Tuple.Create( 11,  1,  4), Tuple.Create( 12,  4,  5), Tuple.Create( 13,  5,  7), Tuple.Create( 14,  7, 12), Tuple.Create( 15, 12, 20), Tuple.Create( 16, 20, 21), Tuple.Create( 17, 21, 20), Tuple.Create( 18, 20, 12), Tuple.Create( 19, 12,  7),
        Tuple.Create( 20,  7,  5), Tuple.Create( 21,  5,  4), Tuple.Create( 22,  4,  1), Tuple.Create( 23,  1,  0), Tuple.Create( 24,  0,  1), Tuple.Create( 25,  1,  4), Tuple.Create( 26,  4,  3), Tuple.Create( 27,  3,  4), Tuple.Create( 28,  4,  5), Tuple.Create( 29,  5,  7),
        Tuple.Create( 30,  7, 12), Tuple.Create( 31, 12, 20), Tuple.Create( 32, 20, 19), Tuple.Create( 33, 19, 18), Tuple.Create( 34, 18, 16), Tuple.Create( 35, 16, 15), Tuple.Create( 36, 15, 10), Tuple.Create( 37, 10,  9), Tuple.Create( 38,  9,  8), Tuple.Create( 39,  8,  7),
        Tuple.Create( 40,  7,  6), Tuple.Create( 41,  6,  7), Tuple.Create( 42,  7,  8), Tuple.Create( 43,  8,  9), Tuple.Create( 44,  9, 10), Tuple.Create( 45, 10, 11), Tuple.Create( 46, 11, 10), Tuple.Create( 47, 10, 15), Tuple.Create( 48, 15, 16), Tuple.Create( 49, 16,  0),
        Tuple.Create( 50,  0,  1), Tuple.Create( 51,  1,  4), Tuple.Create( 52,  4,  5), Tuple.Create( 53,  5,  6), Tuple.Create( 54,  6,  5), Tuple.Create( 55,  5,  7), Tuple.Create( 56,  7,  9), Tuple.Create( 57,  9,  2), Tuple.Create( 58,  2,  0), Tuple.Create( 59,  0, 16),
        Tuple.Create( 60, 16, 15), Tuple.Create( 61, 15, 10), Tuple.Create( 62, 10, 11), Tuple.Create( 63, 11, 10), Tuple.Create( 64, 10, 27),                            Tuple.Create( 66, 27, 13), Tuple.Create( 67, 13, 14), Tuple.Create( 68, 14, 13), Tuple.Create( 69, 13, 27),
        Tuple.Create( 70, 27, 10), Tuple.Create( 71, 10,  9), Tuple.Create( 72,  9, 22), Tuple.Create( 73, 22, 11), Tuple.Create( 74, 11, 15), Tuple.Create( 75, 15, 17), Tuple.Create( 76, 17, 18), Tuple.Create( 77, 18,  7), Tuple.Create( 78,  7, 13), Tuple.Create( 79, 13,  7),
        Tuple.Create( 80,  7,  5), Tuple.Create( 81,  5,  6), Tuple.Create( 82,  6,  5), Tuple.Create( 83,  5,  7), Tuple.Create( 84,  7, 18), Tuple.Create( 85, 18, 17), Tuple.Create( 86, 17, 15), Tuple.Create( 87, 15, 11), Tuple.Create( 88, 11, 10), Tuple.Create( 89, 10, 12),
        Tuple.Create( 90, 12, 14), Tuple.Create( 91, 14, 12), Tuple.Create( 92, 12, 13), Tuple.Create( 93, 13, 12), Tuple.Create( 94, 12, 10), Tuple.Create( 95, 10,  0), Tuple.Create( 96,  0, 16), Tuple.Create( 97, 16, 18), Tuple.Create( 98, 18, 23), Tuple.Create( 99, 23, 17),
        Tuple.Create(100, 17, 23), Tuple.Create(101, 23, 18), Tuple.Create(102, 18, 16), Tuple.Create(103, 16, 15), Tuple.Create(104, 15, 10), Tuple.Create(105, 10, 27), Tuple.Create(106, 27, 27), Tuple.Create(107, 27,  8), Tuple.Create(108,  8,  0), Tuple.Create(109,  0,  9),
        Tuple.Create(110,  9,  0), Tuple.Create(111,  0,  8), Tuple.Create(112,  8, 27), Tuple.Create(113, 27, 27),                            Tuple.Create(115, 27,  8), Tuple.Create(116,  8,  0), Tuple.Create(117,  0,  9), Tuple.Create(118,  9, 10), Tuple.Create(119, 10,  0),
        Tuple.Create(120,  0,  1), Tuple.Create(121,  1, 15), Tuple.Create(122, 15, 12), Tuple.Create(123, 12,  0), Tuple.Create(124,  0,  1), Tuple.Create(125,  1,  4), Tuple.Create(126,  4,  5), Tuple.Create(127,  5,  7), Tuple.Create(128,  7,  8), Tuple.Create(129,  8, 10),
        Tuple.Create(130, 10, 16), Tuple.Create(131, 16,  9), Tuple.Create(132,  9, 11), Tuple.Create(133, 11,  5), Tuple.Create(134,  5,  7), Tuple.Create(135,  7, 13), Tuple.Create(136, 13, 14), Tuple.Create(137, 14,  1), Tuple.Create(138,  1,  2), Tuple.Create(139,  2,  8),
        Tuple.Create(140,  8,  3), Tuple.Create(141,  3,  4), Tuple.Create(142,  4,  5), Tuple.Create(143,  5,  4), Tuple.Create(144,  4,  5), Tuple.Create(145,  5,  9), Tuple.Create(146,  9,  5), Tuple.Create(147,  5,  7), Tuple.Create(148,  7,  6), Tuple.Create(149,  6,  8),
        Tuple.Create(150,  8, 12), Tuple.Create(151, 12, 13), Tuple.Create(152, 13, 12), Tuple.Create(153, 12,  8), Tuple.Create(154,  8, 11), Tuple.Create(155, 11, 14), Tuple.Create(156, 14, 14), Tuple.Create(157, 14, 22), Tuple.Create(158, 22, 18), Tuple.Create(159, 18, 20),
        Tuple.Create(160, 20, 21), Tuple.Create(161, 21, 20), Tuple.Create(162, 20, 18), Tuple.Create(163, 18, 22), Tuple.Create(164, 22, 14), Tuple.Create(165, 14, 14), Tuple.Create(166, 14, 11), Tuple.Create(167, 11,  8), Tuple.Create(168,  8, 12), Tuple.Create(169, 12, 16),
        Tuple.Create(170, 16, 12), Tuple.Create(171, 12,  8), Tuple.Create(172,  8, 11), Tuple.Create(173, 11, 14), Tuple.Create(174, 14, 14), Tuple.Create(175, 14, 22), Tuple.Create(176, 22, 18), Tuple.Create(177, 18, 20), Tuple.Create(178, 20, 21), Tuple.Create(179, 21, 20),
        Tuple.Create(180, 20, 18), Tuple.Create(181, 18, 22), Tuple.Create(182, 22, 23), Tuple.Create(183, 23,  0)
    };

    //Leon B Doors List -- skip 37 for Ada Shot
    vars.leonBDoors = new List<Tuple<int, int, int>>
    {
        Tuple.Create(  0,  4,  5), Tuple.Create(  1,  5,  6), Tuple.Create(  2,  6,  7), Tuple.Create(  3,  7,  8), Tuple.Create(  4,  8,  9), Tuple.Create(  5,  9, 22), Tuple.Create(  6, 22, 11), Tuple.Create(  7, 11, 22), Tuple.Create(  8, 22,  9), Tuple.Create(  9,  9,  8),
        Tuple.Create( 10,  8,  9), Tuple.Create( 11,  9, 10), Tuple.Create( 12, 10, 11), Tuple.Create( 13, 11, 10), Tuple.Create( 14, 10, 15), Tuple.Create( 15, 15, 16), Tuple.Create( 16, 16,  0), Tuple.Create( 17,  0, 16), Tuple.Create( 18, 16, 18), Tuple.Create( 19, 18, 19),
        Tuple.Create( 20, 19, 20), Tuple.Create( 21, 20, 21), Tuple.Create( 22, 21, 20), Tuple.Create( 23, 20, 12), Tuple.Create( 24, 12,  7), Tuple.Create( 25,  7,  5), Tuple.Create( 26,  5,  6), Tuple.Create( 27,  6,  5), Tuple.Create( 28,  5,  7), Tuple.Create( 29,  7,  9),
        Tuple.Create( 30,  9,  2), Tuple.Create( 31,  2,  0), Tuple.Create( 32,  0, 10), Tuple.Create( 33, 10, 11), Tuple.Create( 34, 11, 15), Tuple.Create( 35, 15, 17), Tuple.Create( 36, 17, 22),                            Tuple.Create( 38, 22, 25), Tuple.Create( 39, 25,  1),
        Tuple.Create( 40,  1, 25), Tuple.Create( 41, 25, 26), Tuple.Create( 42, 26,  2), Tuple.Create( 43,  2, 11), Tuple.Create( 44, 11,  3), Tuple.Create( 45,  3, 11), Tuple.Create( 46, 11,  5), Tuple.Create( 47,  5,  6), Tuple.Create( 48,  6,  5), Tuple.Create( 49,  5, 11),
        Tuple.Create( 50, 11,  2), Tuple.Create( 51,  2, 26), Tuple.Create( 52, 26, 25), Tuple.Create( 53, 25, 22), Tuple.Create( 54, 22, 17), Tuple.Create( 55, 17, 15), Tuple.Create( 56, 15, 11), Tuple.Create( 57, 11, 10), Tuple.Create( 58, 10, 12), Tuple.Create( 59, 12, 14),
        Tuple.Create( 60, 14, 12), Tuple.Create( 61, 12, 13), Tuple.Create( 62, 13, 12), Tuple.Create( 63, 12, 10), Tuple.Create( 64, 10, 11), Tuple.Create( 65, 11, 22), Tuple.Create( 66, 22,  9), Tuple.Create( 67,  9, 10), Tuple.Create( 68, 10, 11), Tuple.Create( 69, 11, 10),
        Tuple.Create( 70, 10, 27), Tuple.Create( 71, 27, 13), Tuple.Create( 72, 13, 14), Tuple.Create( 73, 14, 13), Tuple.Create( 74, 13, 27), Tuple.Create( 75, 27, 10), Tuple.Create( 76, 10, 15), Tuple.Create( 77, 15, 16), Tuple.Create( 78, 16, 18), Tuple.Create( 79, 18, 23),
        Tuple.Create( 80, 23, 17), Tuple.Create( 81, 17, 25), Tuple.Create( 82, 25,  1), Tuple.Create( 83,  1, 25), Tuple.Create( 84, 25, 26), Tuple.Create( 85, 26,  2), Tuple.Create( 86,  2, 11), Tuple.Create( 87, 11,  3), Tuple.Create( 88,  3,  4), Tuple.Create( 89,  4,  2),
        Tuple.Create( 90,  2,  3), Tuple.Create( 91,  3,  4), Tuple.Create( 92,  4,  5), Tuple.Create( 93,  5, 11), Tuple.Create( 94, 11,  9), Tuple.Create( 95,  9, 16), Tuple.Create( 96, 16,  4), Tuple.Create( 97,  4,  5), Tuple.Create( 98,  5,  7), Tuple.Create( 99,  7,  8),
        Tuple.Create(100,  8, 10), Tuple.Create(101, 10, 16), Tuple.Create(102, 16,  9), Tuple.Create(103,  9, 11), Tuple.Create(104, 11,  5), Tuple.Create(105,  5,  7), Tuple.Create(106,  7, 13), Tuple.Create(107, 13, 14), Tuple.Create(108, 14,  0), Tuple.Create(109,  0,  1),
        Tuple.Create(110,  1,  2), Tuple.Create(111,  2,  8), Tuple.Create(112,  8,  3), Tuple.Create(113,  3,  6), Tuple.Create(114,  6,  7), Tuple.Create(115,  7,  6), Tuple.Create(116,  6,  3), Tuple.Create(117,  3,  4), Tuple.Create(118,  4,  5), Tuple.Create(119,  5,  9),
        Tuple.Create(120,  9,  5), Tuple.Create(121,  5,  0), Tuple.Create(122,  0,  2), Tuple.Create(123,  2,  3), Tuple.Create(124,  3,  5), Tuple.Create(125,  5,  3), Tuple.Create(126,  3,  2), Tuple.Create(127,  2,  1), Tuple.Create(128,  1,  6), Tuple.Create(129,  6,  8),
        Tuple.Create(130,  8, 12), Tuple.Create(131, 12, 13), Tuple.Create(132, 13, 12), Tuple.Create(133, 12,  8), Tuple.Create(134,  8, 11), Tuple.Create(135, 11, 14), Tuple.Create(136, 14, 14), Tuple.Create(137, 14, 22), Tuple.Create(138, 22, 18), Tuple.Create(139, 18, 20),
        Tuple.Create(140, 20, 21), Tuple.Create(141, 21, 20), Tuple.Create(142, 20, 18), Tuple.Create(143, 18, 22), Tuple.Create(144, 22, 14), Tuple.Create(145, 14, 14), Tuple.Create(146, 14, 11), Tuple.Create(147, 11,  8), Tuple.Create(148,  8,  6), Tuple.Create(149,  6,  1),
        Tuple.Create(150,  1,  2), Tuple.Create(151,  2,  3), Tuple.Create(152,  3,  4), Tuple.Create(153,  4,  3), Tuple.Create(154,  3,  2), Tuple.Create(155,  2,  1), Tuple.Create(156,  1,  6), Tuple.Create(157,  6,  7), Tuple.Create(158,  7,  1), Tuple.Create(159,  1,  0),
        Tuple.Create(160,  0,  3), Tuple.Create(161,  3,  4), Tuple.Create(162,  4,  3), Tuple.Create(163,  3,  0), Tuple.Create(164,  0,  1), Tuple.Create(165,  1,  2), Tuple.Create(166,  2,  1), Tuple.Create(167,  1,  0), Tuple.Create(168,  0,  3), Tuple.Create(169,  3,  4),
        Tuple.Create(170,  4,  3)
    };

    //Claire B Doors List -- skip 40 for Irons Cutscene and 95 for re-entering elevator after getting Sherry after G1 fight
    vars.claireBDoors = new List<Tuple<int, int, int>>
    {
        Tuple.Create(  0,  4,  5), Tuple.Create(  1,  5,  6), Tuple.Create(  2,  6,  7), Tuple.Create(  3,  7,  8), Tuple.Create(  4,  8,  9), Tuple.Create(  5,  9, 22), Tuple.Create(  6, 22, 11), Tuple.Create(  7, 11, 22), Tuple.Create(  8, 22,  9), Tuple.Create(  9,  9,  8),
        Tuple.Create( 10,  8,  9), Tuple.Create( 11,  9, 10), Tuple.Create( 12, 10, 11), Tuple.Create( 13, 11, 10), Tuple.Create( 14, 10, 15), Tuple.Create( 15, 15, 16), Tuple.Create( 16, 16,  0), Tuple.Create( 17,  0,  1), Tuple.Create( 18,  1,  4), Tuple.Create( 19,  4,  3),
        Tuple.Create( 20,  3,  4), Tuple.Create( 21,  4,  5), Tuple.Create( 22,  5,  6), Tuple.Create( 23,  6,  5), Tuple.Create( 24,  5,  7), Tuple.Create( 25,  7, 12), Tuple.Create( 26, 12, 20), Tuple.Create( 27, 20, 21), Tuple.Create( 28, 21, 20), Tuple.Create( 29, 20, 12),
        Tuple.Create( 30, 12,  7), Tuple.Create( 31,  7,  9), Tuple.Create( 32,  9,  2), Tuple.Create( 33,  2,  0), Tuple.Create( 34,  0, 16), Tuple.Create( 35, 16, 15), Tuple.Create( 36, 15, 10), Tuple.Create( 37, 10, 11), Tuple.Create( 38, 11, 10), Tuple.Create( 39, 10, 27),
                                   Tuple.Create( 41, 27, 13), Tuple.Create( 42, 13, 14), Tuple.Create( 43, 14, 13), Tuple.Create( 44, 13, 27), Tuple.Create( 45, 27, 10), Tuple.Create( 46, 10,  9), Tuple.Create( 47,  9, 22), Tuple.Create( 48, 22, 11), Tuple.Create( 49, 11, 15),
        Tuple.Create( 50, 15, 17), Tuple.Create( 51, 17, 18), Tuple.Create( 52, 18,  7), Tuple.Create( 53,  7, 13), Tuple.Create( 54, 13,  7), Tuple.Create( 55,  7,  5), Tuple.Create( 56,  5,  6), Tuple.Create( 57,  6,  5), Tuple.Create( 58,  5,  7), Tuple.Create( 59,  7, 18),
        Tuple.Create( 60, 18, 17), Tuple.Create( 61, 17, 22), Tuple.Create( 62, 22, 25), Tuple.Create( 63, 25, 26), Tuple.Create( 64, 26, 25), Tuple.Create( 65, 25, 22), Tuple.Create( 66, 22, 17), Tuple.Create( 67, 17, 15), Tuple.Create( 68, 15, 11), Tuple.Create( 69, 11, 10),
        Tuple.Create( 70, 10, 12), Tuple.Create( 71, 12, 14), Tuple.Create( 72, 14, 12), Tuple.Create( 73, 12, 13), Tuple.Create( 74, 13, 12), Tuple.Create( 75, 12, 10), Tuple.Create( 76, 10,  0), Tuple.Create( 77,  0, 16), Tuple.Create( 78, 16, 18), Tuple.Create( 79, 18, 23),
        Tuple.Create( 80, 23, 17), Tuple.Create( 81, 17, 23), Tuple.Create( 82, 23, 18), Tuple.Create( 83, 18, 16), Tuple.Create( 84, 16, 15), Tuple.Create( 85, 15, 10), Tuple.Create( 86, 10, 27), Tuple.Create( 87, 27, 27), Tuple.Create( 88, 27,  8), Tuple.Create( 89,  8,  0),
        Tuple.Create( 90,  0,  9), Tuple.Create( 91,  9,  0), Tuple.Create( 92,  0,  8), Tuple.Create( 93,  8, 27), Tuple.Create( 94, 27, 27),                            Tuple.Create( 96, 27,  8), Tuple.Create( 97,  8,  0), Tuple.Create( 98,  0,  9), Tuple.Create( 99,  9, 10),
        Tuple.Create(100, 10,  0), Tuple.Create(101,  0,  1), Tuple.Create(102,  1,  4), Tuple.Create(103,  4,  5), Tuple.Create(104,  5,  7), Tuple.Create(105,  7,  8), Tuple.Create(106,  8, 10), Tuple.Create(107, 10, 16), Tuple.Create(108, 16,  9), Tuple.Create(109,  9, 11),
        Tuple.Create(110, 11,  5), Tuple.Create(111,  5,  7), Tuple.Create(112,  7, 13), Tuple.Create(113, 13, 14), Tuple.Create(114, 14,  1), Tuple.Create(115,  1,  2), Tuple.Create(116,  2,  8), Tuple.Create(117,  8,  3), Tuple.Create(118,  3,  6), Tuple.Create(119,  6,  7),
        Tuple.Create(120,  7,  6), Tuple.Create(121,  6,  3), Tuple.Create(122,  3,  4), Tuple.Create(123,  4,  5), Tuple.Create(124,  5,  9), Tuple.Create(125,  9,  5), Tuple.Create(126,  5,  0), Tuple.Create(127,  0,  2), Tuple.Create(128,  2,  3), Tuple.Create(129,  3,  5),
        Tuple.Create(130,  5,  3), Tuple.Create(131,  3,  2), Tuple.Create(132,  2,  1), Tuple.Create(133,  1,  6), Tuple.Create(134,  6,  8), Tuple.Create(135,  8, 12), Tuple.Create(136, 12, 13), Tuple.Create(137, 13, 12), Tuple.Create(138, 12,  8), Tuple.Create(139,  8, 11),
        Tuple.Create(140, 11, 14), Tuple.Create(141, 14, 14), Tuple.Create(142, 14, 22), Tuple.Create(143, 22, 18), Tuple.Create(144, 18, 20), Tuple.Create(145, 20, 21), Tuple.Create(146, 21, 20), Tuple.Create(147, 20, 18), Tuple.Create(148, 18, 22), Tuple.Create(149, 22, 14),
        Tuple.Create(150, 14, 14), Tuple.Create(151, 14, 11), Tuple.Create(152, 11,  8), Tuple.Create(153,  8,  6), Tuple.Create(154,  6,  1), Tuple.Create(155,  1,  2), Tuple.Create(156,  2,  3), Tuple.Create(157,  3,  4), Tuple.Create(158,  4,  3), Tuple.Create(159,  3,  2),
        Tuple.Create(160,  2,  1), Tuple.Create(161,  1,  6), Tuple.Create(162,  6,  8), Tuple.Create(163,  8,  6), Tuple.Create(164,  6,  1), Tuple.Create(165,  1,  0), Tuple.Create(166,  0,  3), Tuple.Create(167,  3,  4), Tuple.Create(168,  4,  3), Tuple.Create(169,  3,  0),
        Tuple.Create(170,  0,  1), Tuple.Create(171,  1,  2), Tuple.Create(172,  2,  1), Tuple.Create(173,  1,  0), Tuple.Create(174,  0,  3), Tuple.Create(175,  3,  4), Tuple.Create(176,  4,  3)
    };

    //Hunk & Tofu Doors List -- final door acts as final split since IGT ends there.
    vars.hunkDoors = new List<Tuple<int, int, int>>
    {
        Tuple.Create(  0,  4,  3), Tuple.Create(  1,  3, 11), Tuple.Create(  2, 11,  2), Tuple.Create(  3,  2, 26), Tuple.Create(  4, 26, 25), Tuple.Create(  5, 25, 22), Tuple.Create(  6, 22, 17), Tuple.Create(  7, 17, 15), Tuple.Create(  8, 15, 11), Tuple.Create(  9, 11, 10),
        Tuple.Create( 10, 10,  0), Tuple.Create( 11,  0,  1), Tuple.Create( 12,  1,  4), Tuple.Create( 13,  4,  5), Tuple.Create( 14,  5,  7), Tuple.Create( 15,  7, 12), Tuple.Create( 16, 12, 20), Tuple.Create( 17, 20, 19), Tuple.Create( 18, 19, 18), Tuple.Create( 19, 18, 16),
        Tuple.Create( 20, 16, 15), Tuple.Create( 21, 15, 10), Tuple.Create( 22, 10,  9), Tuple.Create( 23,  9, 28)
    };

    //Ex Battle Doors List -- 6 is end of Stage 1, 18 is end of Stage 2, the remainder of the doors are used in the "degen mode" option
    vars.exDoors = new List<Tuple<int, int, int>>
    {
        Tuple.Create(  0, 18, 22), Tuple.Create(  1, 22, 14), Tuple.Create(  2, 14, 14), Tuple.Create(  3, 14, 11), Tuple.Create(  4, 11,  8), Tuple.Create(  5,  8,  6),                            Tuple.Create(  7,  4,  3), Tuple.Create(  8,  3,  8), Tuple.Create(  9,  8,  2),
        Tuple.Create( 10,  2,  1), Tuple.Create( 11,  1, 14), Tuple.Create( 12, 14, 13), Tuple.Create( 13, 13,  7), Tuple.Create( 14,  7,  5), Tuple.Create( 15,  5,  4), Tuple.Create( 16,  4,  3), Tuple.Create( 17,  3,  2),                            Tuple.Create( 19,  4,  3),
        Tuple.Create( 20,  3, 11), Tuple.Create( 21, 11,  2), Tuple.Create( 22,  2, 26), Tuple.Create( 23, 26, 25), Tuple.Create( 24, 25,  1), Tuple.Create( 25,  1, 25), Tuple.Create( 26, 25, 22), Tuple.Create( 27, 22, 17), Tuple.Create( 28, 17, 15), Tuple.Create( 29, 15, 16),
        Tuple.Create( 30, 16, 15), Tuple.Create( 31, 15, 11), Tuple.Create( 32, 11, 10), Tuple.Create( 33, 10,  0), Tuple.Create( 34,  0,  1), Tuple.Create( 35,  1,  4), Tuple.Create( 36,  4,  5), Tuple.Create( 37,  5,  6), Tuple.Create( 38,  6,  5), Tuple.Create( 39,  5,  7),
        Tuple.Create( 40,  7, 12), Tuple.Create( 41, 12, 20), Tuple.Create( 42, 20, 21)
    };

    //Adding in stageIDs for set splits to prevent splits in incorrect places because door splits are not consecutive doors.
    //Leon A Set Split Door List -- skip 3 for Ada Shot
    vars.leonASet = new List<Tuple<int, int, int, int>>
    {
        Tuple.Create( 0,  3,  0, 1), Tuple.Create( 1, 21, 20, 0), Tuple.Create( 2, 18, 16, 0), Tuple.Create( 4,  6,  5, 2), Tuple.Create( 5, 12, 10, 1), 
        Tuple.Create( 6,  3,  4, 2), Tuple.Create( 7,  7, 13, 3), Tuple.Create( 8,  9,  5, 4), Tuple.Create( 9, 21, 20, 5), Tuple.Create(10, 16, 12, 5)
    };

    //Claire A Set Split Door List -- skip 3 for Irons Cutscene
    vars.claireASet = new List<Tuple<int, int, int, int>>
    {
        Tuple.Create( 0,  3,  0, 1), Tuple.Create( 1, 21, 20, 0), Tuple.Create( 2, 18, 16, 0), Tuple.Create( 4, 13,  2, 2), Tuple.Create( 5,  6,  5, 2), 
        Tuple.Create( 6, 12, 10, 1), Tuple.Create( 7, 17, 23, 0), Tuple.Create( 8,  9,  0, 2), Tuple.Create( 9,  7, 13, 3), Tuple.Create(10,  9,  5, 4),
        Tuple.Create(11, 21, 20, 5), Tuple.Create(12, 16, 12, 5), Tuple.Create(13, 21, 20, 5)
    };

    //Leon B Set Split Door List -- skip 2 for Ada Shot
    vars.leonBSet = new List<Tuple<int, int, int, int>>
    {
        Tuple.Create( 0, 11, 22, 0), Tuple.Create( 1, 21, 20, 0), Tuple.Create( 3, 12, 10, 1), Tuple.Create( 4, 14, 13, 0), Tuple.Create( 5,  3,  4, 2), 
        Tuple.Create( 6,  7, 13, 3), Tuple.Create( 7,  9,  5, 4), Tuple.Create( 8, 21, 20, 5), Tuple.Create( 9,  2,  1, 6)
    };

    //Claire B Set Split Door List -- skip 3 for Irons Cutscene
    vars.claireBSet = new List<Tuple<int, int, int, int>>
    {
        Tuple.Create( 0, 11, 22, 0), Tuple.Create( 1,  3,  4, 1), Tuple.Create( 2, 21, 20, 0), Tuple.Create( 4, 13,  7, 2), Tuple.Create( 5,  6,  5, 2), 
        Tuple.Create( 6, 12, 10, 1), Tuple.Create( 7,  9,  0, 2), Tuple.Create( 8,  7, 13, 3), Tuple.Create( 9,  9,  5, 4), Tuple.Create(10, 21, 20, 5),
        Tuple.Create(12,  2,  1, 6)
    };
}

update
{    
    //Reset variables when the timer is reset.
	if(timer.CurrentPhase == TimerPhase.NotRunning)
	{
		vars.completedSplits.Clear();
		vars.doorIterator = 0;
        vars.redJewels = 0;
        vars.redJewelsPrevious = 0;
        vars.fireplaceJewel = 0;
        vars.statueJewel = 0;
        vars.stage1 = 0;
        vars.stage2 = 0;
        vars.bombCounter = 0;
        vars.bombCounterPrevious = 0;
        vars.endSplitFlag = 0;
	}

    //Iterate through the inventory slots to return their values
    for(int i = 0; i < 10; i++)
	{
        vars.Inventory[i] = new DeepPointer(0x58ED34 + i*(0x4) - vars.versionDetector*(0x60)).Deref<byte>(game);
    }

	//Uncomment debug information in the event of an update.
	//print(modules.First().ModuleMemorySize.ToString());
}

start
{
    //A 4 in the first digit indicates game has started -- starts at start of gameplay
    //return ((current.gameState & 0x40000000) == 0x40000000);

    //Check that the last main menu option was selected to begin the run, or that a save was loaded for B scenario -- starts timer after selecting difficulty or loading save so that the cutscene can be skipped at the right time
    //For Hunk/Tofu, start the timer when you exit the map.
    return ((((current.gameState & 0x20000) == 0x20000) && ((old.gameState & 0x20000) != 0x20000) && !settings["hunk"]) || (settings["hunk"] && (old.gameState & 0x4000) == 0x4000 && (current.gameState & 0x4000) != 0x4000));
}

split
{
    //Final Split For Main Campaign -- Always Active
    if((current.gameState & 0x200000) == 0x200000 && !settings["hunk"] && !settings["ex"] && vars.endSplitFlag == 0)
    {
        vars.endSplitFlag++;
        return true;
    }
    
    //Create variables to check for the variables in each item slot
    byte[] currentInventory = (vars.Inventory as byte[]);

	//Item Splits
	if(settings["itemL"] || settings["itemC"] || settings["item"] || settings["extra"])
    {
        for(int i = 0; i < 10; i++)
        {
	    	//Check if any inventory slots include the variables in our items lists, check if the split was already completed and if the setting for the given item is activated
	    	if((vars.CommonItems.Contains(currentInventory[i]) || vars.LeonItems.Contains(currentInventory[i]) || vars.ClaireItems.Contains(currentInventory[i]) || vars.ExtraItems.Contains(currentInventory[i])) 
                && !vars.completedSplits.Contains(currentInventory[i]) && settings[currentInventory[i].ToString()])
            {
            	vars.completedSplits.Add(currentInventory[i]);
            	return true;
        	}
        }

        //Red Jewel Splits
        if((settings["red1"] && current.roomID == 12 && current.stageID == 0) || (settings["red2"] && current.roomID == 6 && current.stageID == 1))
        {
            //Reset the red jewel total to zero
            vars.redJewels = 0;

            //Cycle through the inventory and count the number of red jewels
            for(int i = 0; i < 10; i++)
            {
	        	if(currentInventory[i] == 51)
                {
                	vars.redJewels++;
            	}
            }

            //If the statue jewel split is active, the red rewel count increases in the Statue Room and the statue jewel flag is 0, change the flag to 1 and split
            if(settings["red1"] && vars.statueJewel == 0 && vars.redJewels > vars.redJewelsPrevious && current.roomID == 12 && current.stageID == 0)
            {
                vars.statueJewel++;
                return true;
            }

            //If the fireplace jewel split is active, the red rewel count increases in the Briefing Room and the fireplace jewel flag is 0, change the flag to 1 and split
            if(settings["red2"] && vars.fireplaceJewel == 0 && vars.redJewels > vars.redJewelsPrevious && current.roomID == 6 && current.stageID == 1)
            {
                vars.fireplaceJewel++;
                return true;
            }

            //Set the previous red jewel total to the current total
            vars.redJewelsPrevious = vars.redJewels;
        }
	}

    //Door Splits
	if((settings["doors"] || settings["set"]) && !settings["check"] && !settings["basic"] && !settings["ex"])
	{
        //Check for category and split set being used, then check if we are going through the correct door in the split order. Only run when transitioning rooms.
        if(old.roomID != current.roomID || old.oldRoom != current.oldRoom)
        {
            if(settings["leona"] && vars.leonADoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID))) ||
            settings["clairea"] && vars.claireADoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID))) ||
            settings["leonb"] && vars.leonBDoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID))) ||
            settings["claireb"] && vars.claireBDoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID))) ||
            settings["hunk"] && vars.hunkDoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID))) ||
            settings["setla"] && vars.leonASet.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID),Convert.ToInt32(current.stageID))) ||
            settings["setca"] && vars.claireASet.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID),Convert.ToInt32(current.stageID))) ||
            settings["setlb"] && vars.leonBSet.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID),Convert.ToInt32(current.stageID))) ||
            settings["setcb"] && vars.claireBSet.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID),Convert.ToInt32(current.stageID))))
            {
                vars.doorIterator++;
                return true;
            }
        }

        //Ada Shot Split
        if(((settings["leona"] && vars.doorIterator == 64) || (settings["leonb"] && vars.doorIterator == 37) || (settings["setla"] && vars.doorIterator == 3) || (settings["setlb"] && vars.doorIterator == 2)) 
        && current.roomID == 22 && current.stageID == 1 && current.oldRoom == 17 && current.camID == 2)
        {
            vars.doorIterator++;
            return true;
        }

        //Chief Irons Split
        if(((settings["clairea"] && vars.doorIterator == 65) || (settings["claireb"] && vars.doorIterator == 40) || ((settings["setca"] || settings["setcb"]) && vars.doorIterator == 3)) 
        && current.roomID == 27 && current.stageID == 1 && current.oldRoom == 10 && current.camID == 2)
        {
            vars.doorIterator++;
            return true;
        }

        //Claire Re-entering Elevator Split -- necessary because roomID and oldRoom values are identical upon re-entering, no need to use camID as well. stageID added for extra safety
        if(((settings["clairea"] && vars.doorIterator == 114) || (settings["claireb"] && vars.doorIterator == 95)) && current.roomID == 27 && current.stageID == 1 && current.oldRoom == 27 && current.camID == 15)
        {
            vars.doorIterator++;
            return true;
        }

        //Low%/NG+ Magnum Split Skips
        if(settings["low"] && ((settings["leona"] && vars.doorIterator == 83) || (settings["leonb"] && vars.doorIterator == 20)))
        {
            vars.doorIterator+=2;
        }

        //Set Split Master Key Pickup Split
        if(settings["setcb"] && vars.doorIterator == 11)
        {
            for(int i = 0; i < 10; i++)
            {
	        	//Check if any inventory slots include the variables in our items lists, check if the split was already completed and if the setting for the given item is activated
	        	if(currentInventory[i] == 98)
                {
                	vars.doorIterator++;
                	return true;
            	}
            }
        }
    }

    //EX Battle Splits
    if(settings["ex"])
	{
        //During room transition check if we are going through the correct next door and split
        if(old.roomID != current.roomID || old.oldRoom != current.oldRoom)
        {
            //Routed door splits stop at 18 if not using "degen mode" splits
            if(vars.doorIterator < 18 && vars.exDoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID))))
            {
                vars.doorIterator++;
                return true;
            }

            //Continue splitting along optimal route in "degen mode"
            if(settings["degen"] && vars.doorIterator > 18 && vars.exDoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID))))
            {
                vars.doorIterator++;
                return true;
            }
        }

        //End of Stage 1 Split
        if(vars.doorIterator == 6 && (current.gameState & 0x3004000) == 0x3004000 && current.roomID == 6 && current.camID == 8)
        {
            vars.stage1 = current.time;
            vars.doorIterator++;
            return true;
        }

        //End of Stage 2 Split
        if(vars.doorIterator == 18 && (current.gameState & 0x3004000) == 0x3004000 && current.roomID == 2 && current.camID == 0)
        {
            vars.stage2 = current.time;
            vars.doorIterator++;
            return true;
        }

        //Split on every door in Stage 3 when the option is active
        if(settings["exdoors"] && vars.doorIterator > 18 && (old.roomID != current.roomID || old.oldRoom != current.oldRoom))
	    {
	    	return true;
	    }

        //When in Stage 3, start looking for number of bombs in inventory
        if(vars.doorIterator > 17)
        {
            //Reset the bomb total to zero
            vars.bombCounter = 0;

            //Cycle through the inventory and count the number of bombs
            for(int i = 0; i < 8; i++)
            {
	        	if(currentInventory[i] == 36)
                {
                	vars.bombCounter++;
            	}
            }

            //If the number of bombs in the inventory has increased, increase the old total and split -- active in both bomb splits and degen splits for Stage 3
            if((settings["bombs"] || settings["degen"]) && vars.bombCounter > vars.bombCounterPrevious)
            {
                vars.bombCounterPrevious++;
                return true;
            }
        }

        //End of Stage 3 Split -- look for 4 bombs in inventory and end of stage door trigger (might work without bomb counter?)
        if(vars.bombCounter == 4 && (current.gameState & 0x3004000) == 0x3004000)
        {
            return true;
        }
    }

    //Checkpoint Door Splits -- written by CarcinogenSDA (with 2 minor fixes by me)
    if((old.roomID != current.roomID || old.oldRoom != current.oldRoom || current.camID == 15) && settings["check"] && !settings["basic"])
	{
		if(timer.CurrentSplit.Name == "RPD") {
	    	return current.roomID == 0 && current.stageID == 1 && current.oldRoom == 3;
	    } else if(timer.CurrentSplit.Name == "Stars Office") {
	    	return current.roomID == 20 && current.stageID == 0 && current.oldRoom == 21;
	    } else if(timer.CurrentSplit.Name == "Library") {
	        return current.roomID == 16 && current.stageID == 0 && current.oldRoom == 18;
	    } else if(timer.CurrentSplit.Name == "Ada Shot") {
	        return current.roomID == 22 && current.stageID == 1 && current.oldRoom == 17 && current.camID == 2;
	    } else if(timer.CurrentSplit.Name == "Club Key") {
	        return current.roomID == 5 && current.stageID == 2 && current.oldRoom == 6;
	    } else if(timer.CurrentSplit.Name == "Red Hallway") {
	    	return current.roomID == 10 && current.stageID == 1 && current.oldRoom == 12;
	    } else if(timer.CurrentSplit.Name == "G-Mutant") {
	    	return current.roomID == 4 && current.stageID == 2 && current.oldRoom == 3;
        } else if(timer.CurrentSplit.Name == "G-Mutant") {
	    	return current.roomID == 0 && current.stageID == 2 && current.oldRoom == 9;
	    } else if(timer.CurrentSplit.Name == "Sewers") {
	    	return current.roomID == 13 && current.stageID == 3 && current.oldRoom == 7;
	    } else if(timer.CurrentSplit.Name == "Birkin (G2)") {
	    	return current.roomID == 5 && current.stageID == 4 && current.oldRoom == 9;
	    } else if(timer.CurrentSplit.Name == "Lab Card Key") {
	    	return current.roomID == 20 && current.stageID == 5 && current.oldRoom == 21;
	    } else if(timer.CurrentSplit.Name == "MO Disk") {
	    	return current.roomID == 12 && current.stageID == 5 && current.oldRoom == 16;
	    } else if(timer.CurrentSplit.Name == "Chief Irons") {
	    	return current.roomID == 27 && current.stageID == 1 && current.camID == 10;
	    } else if(timer.CurrentSplit.Name == "Mr X") {
	    	return current.roomID == 10 && current.stageID == 0 && current.oldRoom == 9;
	    } else if(timer.CurrentSplit.Name == "Birkin (G1)") {
	    	return current.roomID == 0 && current.stageID == 2 && current.oldRoom == 9;
	    } else if(timer.CurrentSplit.Name == "Birkin (G1)") {
	    	return current.roomID == 4 && current.stageID == 2 && current.oldRoom == 3;
	    } else if(timer.CurrentSplit.Name == "Birkin (G3)") {
	    	return current.roomID == 5 && current.stageID == 4 && current.oldRoom == 9;
	    } else if(timer.CurrentSplit.Name == "Power Room Key") {
	    	return current.roomID == 20 && current.stageID == 5 && current.oldRoom == 21;
	    } else if(timer.CurrentSplit.Name == "Master Key") {
	    	return current.roomID == 6 && current.stageID == 5 && current.oldRoom == 8;
	    } else if(timer.CurrentSplit.Name == "Tyrant") {
	    	return current.roomID == 1 && current.stageID == 6 && current.oldRoom == 2;
	    } else if(timer.CurrentSplit.Name == "Crank") {
	    	return current.roomID == 13 && current.stageID == 0 && current.oldRoom == 14;
        } else if(current.roomID == old.roomID && current.oldRoom == old.oldRoom && current.camID == 15) { //accounting for re-entering elevator after coming back from boss fight
	    	return true;
	    } else if(current.stageID != old.stageID) {
	    	return true;
        } else if(current.roomID != old.roomID) {
	    	return true;
	    } else {
	    	return current.oldRoom != old.oldRoom;
	    }
	}

    //Basic Door Splits
    if(settings["basic"] && (old.roomID != current.roomID || old.oldRoom != current.oldRoom || (current.roomID == old.roomID && current.oldRoom == old.oldRoom && current.camID == 15)))
	{
		return true;
	}
}

gameTime
{
    //This is using the frame counter for Hunk, so it is accurate to within a frame (with rounding). The game may be doing some tricks to make the time appear more random and/or the refresh rate of LiveSplit makes it imperfect.
    if(settings["hunk"])
    {
        //subtracting 1 from the frame count on v1.0 because timer was 1-2 frames off on vanilla game, whereas 0-1 frames off on REbirth
        return (TimeSpan.FromSeconds((double)(current.hunkSec) + (((((current.hunkFrames - vars.versionDetector) % 30) + 30) % 30) / 30.0)));
    } else {
        //Main game and EX Battle. Since time resets to 0 at the beginning of each stage of EX battle, we make timestamps when completing the episodes and add it to the total time. Time does carry on after hitting door trigger, so LS time will look funny until next stage starts.
        return (TimeSpan.FromSeconds((double)(current.time) + vars.stage1 + vars.stage2 + (current.frame / 60.0)));
    }
}

isLoading
{
    return true;
}

reset
{
    return current.oldRoom == 255 && current.hp == 0 && (current.gameState & 0x20000) != 0x20000;
}