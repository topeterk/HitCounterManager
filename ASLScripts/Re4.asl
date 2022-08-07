state("bio4")
{
	byte currentArea    : 0x7FC1C9;	// Current Area
	byte currentScreen  : 0x8597BB;	// Current Screen
	byte loadingScreen  : 0x858F77;	// Loading Screen
	byte loadingCount   : 0x858F7B;	// Loading Count
	byte map            : 0xCED6DC;	// Map Screen
	byte subtitle       : 0x817840;	// Subtitles on the Screen
	byte item           : 0x858EE4;	// Pick up Items
	int  isEndOfChapter : 0x85F6F8;	// End of Chapter
	int  mgEnd          : 0x8594B8;	// Ending Cutscene in Main Game
	int  swEnd          : 0x867BDC;	// Ending Cutscene in Separate Ways
	int  fslPlaga       : 0x85F9EC;	// First, Second, and Last Plaga
	int  tfPlaga        : 0x85F9F0;	// Third and Fourth Plaga
	int  operate        : 0x850300;	// Operate Command on Button Presses
	int  igt            : 0x85F704;	// In Game Time
}

state("bio4", "1.0.6")
{
	byte currentArea    : 0x7FB1C9;
	byte currentScreen  : 0x855F3B;
	byte loadingScreen  : 0x8556F7;
	byte loadingCount   : 0x8556FB;
	byte map            : 0xCE9E5C;
	byte subtitle       : 0x814030;
	byte item           : 0x855664;
	int  isEndOfChapter : 0x85BE78;
	int  mgEnd          : 0x855C38;
	int  swEnd          : 0x86434C;
	int  fslPlaga       : 0x85C16C;
	int  tfPlaga        : 0x85C170;
	int  operate        : 0x84CA80;
	int  igt            : 0x85BE84;
}

startup
{
	// Creating the Options
	settings.Add("MainGameSplits", true, "Main Game");
	settings.Add("MainGameChapterSplits", true, "Chapters", "MainGameSplits");
	settings.Add("Chapter1GameMode1", true, "Chapter 1-1", "MainGameChapterSplits");
	settings.Add("Chapter2GameMode1", true, "Chapter 1-2", "MainGameChapterSplits");
	settings.Add("Chapter3GameMode1", true, "Chapter 1-3", "MainGameChapterSplits");
	settings.Add("Chapter4GameMode1", true, "Chapter 2-1", "MainGameChapterSplits");
	settings.Add("Chapter5GameMode1", true, "Chapter 2-2", "MainGameChapterSplits");
	settings.Add("Chapter6GameMode1", true, "Chapter 2-3", "MainGameChapterSplits");
	settings.Add("Chapter7GameMode1", true, "Chapter 3-1", "MainGameChapterSplits");
	settings.Add("Chapter8GameMode1", true, "Chapter 3-2", "MainGameChapterSplits");
	settings.Add("Chapter9GameMode1", true, "Chapter 3-3", "MainGameChapterSplits");
	settings.Add("Chapter10GameMode1", true, "Chapter 3-4", "MainGameChapterSplits");
	settings.Add("Chapter11GameMode1", true, "Chapter 4-1", "MainGameChapterSplits");
	settings.Add("Chapter12GameMode1", true, "Chapter 4-2", "MainGameChapterSplits");
	settings.Add("Chapter13GameMode1", true, "Chapter 4-3", "MainGameChapterSplits");
	settings.Add("Chapter14GameMode1", true, "Chapter 4-4", "MainGameChapterSplits");
	settings.Add("Chapter15GameMode1", true, "Chapter 5-1", "MainGameChapterSplits");
	settings.Add("Chapter16GameMode1", true, "Chapter 5-2", "MainGameChapterSplits");
	settings.Add("Chapter17GameMode1", true, "Chapter 5-3", "MainGameChapterSplits");
	settings.Add("Chapter18GameMode1", true, "Chapter 5-4", "MainGameChapterSplits");

	settings.Add("MainGameItemSplits", false, "Key Items", "MainGameSplits");
	settings.Add("Item164GameMode1", true, "Emblem (Right half)", "MainGameItemSplits");
	settings.Add("Item165GameMode1", true, "Emblem (Left half)", "MainGameItemSplits");
	settings.Add("Item59GameMode1", true, "Insignia Key", "MainGameItemSplits");
	settings.Add("Item60GameMode1", true, "Round Insignia", "MainGameItemSplits");
	settings.Add("Item140GameMode1", true, "Camp Key", "MainGameItemSplits");
	settings.Add("Item139GameMode1", true, "Old Key", "MainGameItemSplits");
	settings.Add("Item61GameMode1", true, "False Eye", "MainGameItemSplits");
	settings.Add("Item128GameMode1", true, "Golden Sword", "MainGameItemSplits");
	settings.Add("Item196GameMode1", true, "Platinum Sword", "MainGameItemSplits");
	settings.Add("Item167GameMode1", true, "Castle Gate Key", "MainGameItemSplits");
	settings.Add("Item195GameMode1", true, "Prison Key", "MainGameItemSplits");
	settings.Add("Item163GameMode1", true, "Gallery Key", "MainGameItemSplits");
	settings.Add("Item31GameMode1", true, "Goat Ornament", "MainGameItemSplits");
	settings.Add("Item58GameMode1", true, "Moonstone (Right half)", "MainGameItemSplits");
	settings.Add("Item105GameMode1", true, "Moonstone (Left half)", "MainGameItemSplits");
	settings.Add("Item29GameMode1", true, "Stone Tablet", "MainGameItemSplits");
	settings.Add("Item15GameMode1", true, "Salazar Family Insignia", "MainGameItemSplits");
	settings.Add("Item57GameMode1", true, "Serpent Ornament", "MainGameItemSplits");
	settings.Add("Item30GameMode1", true, "Lion Ornament", "MainGameItemSplits");
	settings.Add("Item111GameMode1", true, "Queen's Grail", "MainGameItemSplits");
	settings.Add("Item110GameMode1", true, "King's Grail", "MainGameItemSplits");
	settings.Add("Item141GameMode1", true, "Dynamite", "MainGameItemSplits");
	settings.Add("Item123GameMode1", true, "Key to the Mine", "MainGameItemSplits");
	settings.Add("Item130GameMode1", true, "Stone of Sacrifice", "MainGameItemSplits");
	settings.Add("Item132GameMode1", true, "Freezer Card Key", "MainGameItemSplits");
	settings.Add("Item146GameMode1", true, "Waste Disposal Card Key", "MainGameItemSplits");
	settings.Add("Item131GameMode1", true, "Storage Room Card Key", "MainGameItemSplits");
	settings.Add("Item135GameMode1", true, "Piece of the Holy Beast, Eagle", "MainGameItemSplits");
	settings.Add("Item134GameMode1", true, "Piece of the Holy Beast, Serpent", "MainGameItemSplits");
	settings.Add("Item116GameMode1", true, "Emergency Lock Card Key", "MainGameItemSplits");
	settings.Add("Item136GameMode1", true, "Jet-ski Key", "MainGameItemSplits");

	settings.Add("SeparateWaysSplits", true, "Separate Ways");
	settings.Add("SeparateWaysChapterSplits", true, "Chapters", "SeparateWaysSplits");
	settings.Add("Chapter1GameMode2", true, "Chapter 1", "SeparateWaysChapterSplits");
	settings.Add("Chapter2GameMode2", true, "Chapter 2", "SeparateWaysChapterSplits");
	settings.Add("Chapter3GameMode2", true, "Chapter 3", "SeparateWaysChapterSplits");
	settings.Add("Chapter4GameMode2", true, "Chapter 4", "SeparateWaysChapterSplits");
	settings.Add("Chapter5GameMode2", true, "Chapter 5", "SeparateWaysChapterSplits");

	settings.Add("SeparateWaysItemSplits", false, "Key Items", "SeparateWaysSplits");
	settings.Add("Item59GameMode2", true, "Insignia Key", "SeparateWaysItemSplits");
	settings.Add("Item118GameMode2", true, "Green Catseye", "SeparateWaysItemSplits");
	settings.Add("Item60GameMode2", true, "Round Insignia", "SeparateWaysItemSplits");
	settings.Add("Item129GameMode2", true, "Iron Key", "SeparateWaysItemSplits");
	settings.Add("Item142GameMode2", true, "Lift Activation Key", "SeparateWaysItemSplits");
	settings.Add("Item27GameMode2", true, "Hourglass w/ gold decor", "SeparateWaysItemSplits");
	settings.Add("Item49GameMode2", true, "Activation Key (blue)", "SeparateWaysItemSplits");
	settings.Add("Item51GameMode2", true, "Activation Key (red)", "SeparateWaysItemSplits");

	settings.Add("AssignmentAdaSplits", true, "Assignment Ada");
	settings.Add("AssignmentAdaSamplesSplits", true, "Plaga Samples", "AssignmentAdaSplits");
	settings.Add("Sample8", true, "Plaga Sample 1", "AssignmentAdaSamplesSplits");
	settings.Add("Sample12", true, "Plaga Sample 2", "AssignmentAdaSamplesSplits");
	settings.Add("Sample1073741824", true, "Plaga Sample 3", "AssignmentAdaSamplesSplits");
	settings.Add("Sample1342177280", true, "Plaga Sample 4", "AssignmentAdaSamplesSplits");
	settings.Add("Sample28", true, "Plaga Sample 5", "AssignmentAdaSamplesSplits");

	settings.Add("DoorSplits", true, "Door Splits");

	settings.SetToolTip("MainGameChapterSplits", "Check this Option if you want to split on Chapters.");
	settings.SetToolTip("MainGameItemSplits", "Check this Option if you want to split on Key Items.");
	settings.SetToolTip("SeparateWaysChapterSplits", "Check this Option if you want to split on Chapters.");
	settings.SetToolTip("SeparateWaysItemSplits", "Check this Option if you want to split on Key Items.");
	settings.SetToolTip("AssignmentAdaSamplesSplits", "Check this Option if you want to split on Plaga Samples.");
	settings.SetToolTip("DoorSplits", "Check this Option if you want to use Door Splits.");

	// Creating the Lists
	vars.BlackListedDoors = new Dictionary<int, Tuple<int, int>>{ };
	vars.WhiteListedDoors = new Dictionary<int, Tuple<int, int>>{ };
	vars.ObtainedKeyItems = new List<byte>{ };
	vars.ObtainedPlagaSamples = new List<int>{ };
	vars.BlackListedDoors.Add(1, Tuple.Create(0, 0));
	vars.BlackListedDoors.Add(2, Tuple.Create(0, 0));

	// Main Game Blacklist
	vars.BlackListedDoors.Add(3, Tuple.Create(159, 192));
	vars.BlackListedDoors.Add(4, Tuple.Create(178, 138));
	vars.BlackListedDoors.Add(5, Tuple.Create(207, 174));
	vars.BlackListedDoors.Add(6, Tuple.Create(221, 174));
	vars.BlackListedDoors.Add(7, Tuple.Create(248, 88));
	vars.BlackListedDoors.Add(8, Tuple.Create(236, 2));
	vars.BlackListedDoors.Add(9, Tuple.Create(2, 119));
	vars.BlackListedDoors.Add(10, Tuple.Create(154, 167));
	vars.BlackListedDoors.Add(11, Tuple.Create(167, 250));
	vars.BlackListedDoors.Add(12, Tuple.Create(229, 74));
	vars.BlackListedDoors.Add(13, Tuple.Create(80, 140));
	vars.BlackListedDoors.Add(14, Tuple.Create(140, 113));
	vars.BlackListedDoors.Add(15, Tuple.Create(113, 140));
	vars.BlackListedDoors.Add(16, Tuple.Create(147, 37));

	// Separate Ways Blacklist
	vars.BlackListedDoors.Add(17, Tuple.Create(173, 112));
	vars.BlackListedDoors.Add(18, Tuple.Create(202, 79));
	vars.BlackListedDoors.Add(19, Tuple.Create(216, 70));
	vars.BlackListedDoors.Add(20, Tuple.Create(119, 68));

	// Main Game Whitelist
	vars.WhiteListedDoors.Add(1, Tuple.Create(7, 206));
	vars.WhiteListedDoors.Add(2, Tuple.Create(149, 7));
	vars.WhiteListedDoors.Add(3, Tuple.Create(133, 207));
	vars.WhiteListedDoors.Add(4, Tuple.Create(133, 237));
	vars.WhiteListedDoors.Add(5, Tuple.Create(221, 180));
	vars.WhiteListedDoors.Add(6, Tuple.Create(230, 178));
	vars.WhiteListedDoors.Add(7, Tuple.Create(150, 13));
	vars.WhiteListedDoors.Add(8, Tuple.Create(150, 88));
	vars.WhiteListedDoors.Add(9, Tuple.Create(183, 252));
	vars.WhiteListedDoors.Add(10, Tuple.Create(192, 68));
	vars.WhiteListedDoors.Add(11, Tuple.Create(192, 191));
	vars.WhiteListedDoors.Add(12, Tuple.Create(123, 216));
	vars.WhiteListedDoors.Add(13, Tuple.Create(216, 226));
	vars.WhiteListedDoors.Add(14, Tuple.Create(160, 141));
	vars.WhiteListedDoors.Add(15, Tuple.Create(101, 81));
	vars.WhiteListedDoors.Add(16, Tuple.Create(123, 191));

	// Separate Ways Whitelist
	vars.WhiteListedDoors.Add(17, Tuple.Create(112, 231));
	vars.WhiteListedDoors.Add(18, Tuple.Create(231, 160));
	vars.WhiteListedDoors.Add(19, Tuple.Create(160, 145));
	vars.WhiteListedDoors.Add(20, Tuple.Create(153, 34));

	// Assignment Ada Whitelist
	vars.WhiteListedDoors.Add(21, Tuple.Create(15, 147));
	vars.WhiteListedDoors.Add(22, Tuple.Create(15, 13));
}

init
{
	// Check the Version of the Game
	version = modules.First().FileVersionInfo.FileVersion;
	if (version == "1.0.18384.3" || version == "1.0.18384.2" || version == "1.0.18384.1")
	{
		version = "1.0.6";
	}
	else
	{
		version = "";
	}

	// Initializing Variables
	vars.TimerModel = new TimerModel { CurrentState = timer };

	Action ResetVariables = () => {
		vars.gameMode = 0;
		vars.chapter = 0;
		vars.towerDoor = 0;
		vars.BlackListedDoors[1] = Tuple.Create(0, 0);
		vars.BlackListedDoors[2] = Tuple.Create(0, 0);
	};
	vars.ResetVariables = ResetVariables;
	vars.ResetVariables();
}

start
{
	// Check to see if Main Game is running
	if (current.loadingCount <= 240 && old.loadingCount >= 240 && current.currentArea == 233 && settings["MainGameSplits"])
	{
		vars.ResetVariables();
		vars.gameMode = 1;
		vars.ObtainedKeyItems = new List<byte>{ };
		return true;
	}

	// Check to see if Separate Ways is running
	if (current.map == 0 && old.map == 1 && current.currentArea == 231 && settings["SeparateWaysSplits"])
	{
		vars.ResetVariables();
		vars.gameMode = 2;
		vars.ObtainedKeyItems = new List<byte>{ };
		return true;
	}

	// Check to see if Assignment Ada is running
	if (current.subtitle == 0 && old.subtitle == 1 && current.currentArea == 114 && settings["AssignmentAdaSplits"])
	{
		vars.ResetVariables();
		vars.gameMode = 3;
		vars.ObtainedPlagaSamples = new List<int>{ };
		return true;
	}
}

split
{
	// Door Splits
	if (current.currentArea != old.currentArea && settings["DoorSplits"])
	{
		if (!vars.BlackListedDoors.ContainsValue(Tuple.Create(Convert.ToInt32(old.currentArea), Convert.ToInt32(current.currentArea))))
		{
			vars.BlackListedDoors[1] = Tuple.Create(Convert.ToInt32(old.currentArea), Convert.ToInt32(current.currentArea));
			vars.BlackListedDoors[2] = Tuple.Create(Convert.ToInt32(current.currentArea), Convert.ToInt32(old.currentArea));
			return true;
		}
		if (vars.WhiteListedDoors.ContainsValue(Tuple.Create(Convert.ToInt32(old.currentArea), Convert.ToInt32(current.currentArea))) || vars.WhiteListedDoors.ContainsValue(Tuple.Create(Convert.ToInt32(current.currentArea), Convert.ToInt32(old.currentArea))))
		{
			return true;
		}
	}

	// Chapter Splits
	if (current.isEndOfChapter - old.isEndOfChapter == 65536)
	{
		vars.chapter++;
		return settings["Chapter" + vars.chapter.ToString() + "GameMode" + vars.gameMode.ToString()];
	}

	// Item Splits
	if (current.item != old.item && !vars.ObtainedKeyItems.Contains(current.item) && settings["Item" + current.item.ToString() + "GameMode" + vars.gameMode.ToString()])
	{
		vars.ObtainedKeyItems.Add(current.item);
		return true;
	}

	// Assignment Ada Splits
	if ((current.fslPlaga > old.fslPlaga || current.tfPlaga > old.tfPlaga) && vars.gameMode == 3)
	{
		if (!vars.ObtainedPlagaSamples.Contains(current.fslPlaga) && settings["Sample" + current.fslPlaga.ToString()])
		{
			vars.ObtainedPlagaSamples.Add(current.fslPlaga);
			return true;
		}
		if (!vars.ObtainedPlagaSamples.Contains(current.tfPlaga) && settings["Sample" + current.tfPlaga.ToString()])
		{
			vars.ObtainedPlagaSamples.Add(current.tfPlaga);
			return true;
		}
	}

	// Check to see if the Door was used after defeating Krauser
	if (current.subtitle == 0 && current.fslPlaga == 28 && current.operate == 0 && old.operate == 65537 && vars.towerDoor == 0)
	{
		vars.towerDoor = 1;
	}

	// Split at the Bridge Door after reunited with Ashley
	if (current.currentArea == 174 && old.currentArea == 207 && current.isEndOfChapter >= 655360 && settings["DoorSplits"])
	{
		return true;
	}

	// Final Splits
	return
	current.mgEnd != old.mgEnd && vars.gameMode == 1 ||
	current.swEnd != old.swEnd && vars.gameMode == 2 ||
	current.subtitle == 0 && old.subtitle == 1 && current.fslPlaga == 28 && current.operate == 256 && vars.towerDoor == 1 && vars.gameMode == 3;
}

isLoading
{
	// Pause Timer when the Load Screen or Pause Menu
	if (current.currentArea == 65 || current.currentArea == 175 || current.currentArea == 64)
	{
		return current.loadingScreen == 255 || current.loadingCount >= 240 || current.currentScreen == 208 || current.loadingCount >= 100 && current.currentScreen == 64;
	}
	else if (current.currentArea == 192 || current.currentArea == 2 || current.currentArea == 148)
	{
		return current.loadingScreen == 255 || current.loadingCount >= 240 || current.currentScreen == 208 || current.loadingCount >= 220 && current.currentScreen == 64;
	}
	else
	{
		return current.loadingScreen == 255 || current.loadingCount >= 240 || current.currentScreen == 208;
	}
}

reset
{
	// Reset Timer when the Main Menu
	if (current.igt == 0 && old.igt != 0)
	{
		vars.ResetVariables();
		return true;
	}
}

exit
{
	// Reset Timer when the Game Exit
	if (timer.CurrentPhase != TimerPhase.Ended)
	{
		vars.TimerModel.Reset();
	}
}
