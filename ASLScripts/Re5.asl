// Resident Evil 5 Autosplitter Version 2.0.6 11/11/2021
// NG supports LRT
// NG+, DLC and IL Runs support IGT
// Supports all difficulties
// Splits can be obtained from https://www.speedrun.com/re5/resources
// Script and Remodification by Mysterion06, TheDementedSalad
// Pointers by Dread, Austin "Auddy" Davenport, Wipefinger, Mysterion06, TheDementedSalad & Mattmatt

// Special thanks to 
// Everybody who has worked on this Autosplitter over the years and made it possible


state("re5dx9")
{
	byte   Level: 		0xDA23D8, 0x273D0; 						// Current Level
	byte   HP: 			0xE243B4, 0xEC, 0x1364;					// Char HP						
	byte   newCut: 		0xE2487C, 0x984, 0x2EDC, 0x4; 			// Gamestate - 2 in game, 4 in cutscenes, 3 in shops, 0 loading screens
	byte   isPaused: 	0xE2487C, 0x984, 0x2EDC, 0x3D; 			// 8 paused, 0 unpaused	
	byte   menu: 		0xDA2970, 0x84, 0x4BC, 0x3A4;			// 1 main menu, 6 in game, 3 in shop (Makes sure timer doesn't count in main menu)
	byte   QTE:			0xDA1934, 0x2F8;						// Checks if a QTE prompt is active, 0 when no prompt, 1 when prompt.
	ushort areaCode: 	0xDA23D8, 0x273D8;						// Current area code, 100 beginning of 1-1, 115 when getting guns, 116 first majini attack
	int    P1ItemID: 	0xDA1934, 0xEBC;						// Player 1's latest item pickup ID
	int    P2ItemID: 	0xDA1934, 0xF38;						// Player 2's latest item pickup ID (DLC)
	int    P2Pickup: 	0xDA2A5C, 0x28, 0x10DC, 0x13C, 0x264; 	// 1 if Player 2 is in pickup animation (DLC)
	int    keyItem: 	0xDA2970, 0x84, 0x18;					// Latest picked up key item (doesn't change on checkpoint reset)
	float  IGT: 		0xDA23D8, 0x273F8;						// Gametime
	float  PauseBuff: 	0xDA1F8C, 0x53C;					    // 0.5 paused in game, 0 in shops, 1 in cutscenes (Catches any time bleed between pauses)
	
}

startup
{	
	vars.LvlStorage = new List<int>()
	{0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16};
	
	vars.completedSplits = new List<string>();
	vars.Rocket = 0;
	vars.totalGameTime = 0;

	settings.Add("Main", false, "Main Game");
	settings.CurrentDefaultParent = "Main";
	settings.Add("NG", false, "New Game (LRT)");
	settings.Add("NG+", false, "New Game + (IGT)");
	settings.Add("No1-1", false, "No 1-1 (LRT)");
	settings.Add("IL", false, "Individual Levels (IGT)");
	settings.CurrentDefaultParent = null;
	settings.Add("DLC", false, "DLC's & Item Splits (IGT)");
	settings.CurrentDefaultParent = "DLC";
	settings.Add("LiN", false, "Lost in Nightmares");
	settings.CurrentDefaultParent = "LiN";
	settings.Add("1316", false, "Password 3");
	settings.Add("1314", false, "Password 1");
	settings.Add("1313", false, "Centaur Emblem");
	settings.Add("1320", false, "Heat-Sensitive Paper");
	settings.Add("1315", false, "Password 2");
	settings.Add("1312", false, "Square Crank");
	settings.Add("1317", false, "Jail Key");
	settings.Add("1318", false, "Gold Crest");
	settings.Add("1319", false, "Silver Crest");
	settings.Add("1327", false, "Crank (Red)");
	settings.Add("1328", false, "Crank (Blue)");
	settings.Add("1329", false, "Crank (Violet)");
	settings.Add("1330", false, "Crank (Green)");
	settings.Add("1323", false, "Sun Eater Serpent Shard");
	settings.Add("1324", false, "Dual-Head Serpent Shard");
	settings.Add("1325", false, "Serpent Tail Shard");
	settings.Add("1326", false, "Tri-Head Serpent Shard");
	settings.Add("LiNEnd", true, "Final Split LiN (Always Active)");
	settings.CurrentDefaultParent = "DLC";
	settings.Add("DE", false, "Desperate Escape");
	settings.CurrentDefaultParent = "DE";
	settings.Add("1321", true, "Transport Area Key");
	settings.Add("1322", true, "Emergency Exit Key");
	settings.Add("RNG", true, "RNG Rocket (Only Tick If Using)");
	settings.Add("NoRNG", true, "Rocket Launcher (Not RNG)");
	settings.Add("DEEnd", true, "Final Split DE (Always Active)");
	settings.CurrentDefaultParent = null;
}

update{
    if (timer.CurrentPhase == TimerPhase.NotRunning)
    {
		vars.completedSplits.Clear();
		vars.Rocket = 0;
        vars.totalGameTime = 0;
    }
}

start
{
	return vars.LvlStorage.Contains(current.Level) && current.IGT > old.IGT && current.newCut == 2;
}

split
{
	if(current.Level > old.Level){
		return true;
	}
	
	if(current.QTE == 0 && old.QTE == 1 && current.newCut == 4 && current.areaCode == 508){
		return true;
	}

	if(settings ["DLC"]){
		vars.hashString = current.keyItem.ToString();
	
		if (current.keyItem == current.P1ItemID || current.keyItem == current.P2ItemID || current.keyItem == 1315){
			if (settings[vars.hashString] && !vars.completedSplits.Contains(vars.hashString))
			{
				vars.completedSplits.Add(vars.hashString);
				return true;
			}
		}
	

		if(current.P2ItemID == 269 && current.P2Pickup == 0 && old.P2Pickup == 1){
			vars.Rocket++;
			return true;
		}
	
		if (settings ["RNG"] && settings ["NoRNG"]){
			if(vars.Rocket == 1 && !vars.completedSplits.Contains("RNG")){
				vars.completedSplits.Add("RNG");
			}
			else if(vars.Rocket == 2 && !vars.completedSplits.Contains("NoRNG")){
				vars.completedSplits.Add("NoRNG");
			}
		}

		if (!settings ["RNG"] && settings ["NoRNG"]){
			if(vars.Rocket == 1){
				vars.completedSplits.Add("NoRNG");
			}
		}
	
		if(settings["LiNEnd"]){
			if(current.areaCode == 617 && current.newCut == 7 && old.newCut != 7){
				return settings["LiNEnd"];
			}
		}
				
		if(settings["DEEnd"]){
			if(current.areaCode == 619 && current.newCut == 7 && old.newCut != 7){
				return settings["DEEnd"];
			}
		}
	}
}
	
isLoading
{
	if(settings["NG"] || settings["No1-1"]) {
		if(current.newCut == 3 && current.HP != 0 || current.newCut != 2 && current.newCut != 3 && current.HP != 0 || current.menu == 1 || current.isPaused == 8 && current.newCut != 3 || current.PauseBuff != 1 || current.newCut == 7){
			return true;
		}
        else{
            return false;
        }
	}
	if(settings["NG+"] || settings["IL"] || settings["DLC"]) {
		return true;
	}
}

gameTime
{
	if(settings["NG+"]){
		if(current.IGT > old.IGT){
			return TimeSpan.FromSeconds(System.Math.Floor(vars.totalGameTime + current.IGT));
		}
		if(current.IGT == 0 && old.IGT > 0){
			vars.totalGameTime = System.Math.Floor(vars.totalGameTime + old.IGT);
			return TimeSpan.FromSeconds(System.Math.Floor(vars.totalGameTime + current.IGT));
		}
	}
	if(settings["IL"] || settings ["DLC"]){
		return TimeSpan.FromSeconds(current.IGT);
	}
}

reset
{
	if(current.Level == 0 && current.IGT == 0 && current.newCut == 2){
		return true;
	}
	
	if(settings["No1-1"]){
		return current.Level == 1 && current.areaCode == 114 && current.IGT > 0 && old.IGT == 0;
	}
	
	if(settings["IL"]){
		return vars.LvlStorage.Contains(current.Level) && current.IGT > old.IGT && old.IGT == 0 && current.newCut == 2;
	}
}
