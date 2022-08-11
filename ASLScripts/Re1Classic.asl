/*
 * 	BIOHAZARD / RESIDENT EVIL 1 (c) 1996 by Capcom
 *	Autosplitter for speedrunning with Livesplit
 *	Made by SHiiDO (twitch.tv/xshiidox - Discord: SHiiDO#4555)
 *	Last updated: 03-23-2021
 *
 *	Special thanks to:
 *	clix_gaming, Daranzanjoll, deserteagle417, DocNiederrheiner, MrZombieUK, OcNr1, Raclesis, SephJul, StevenMayte & WitchRain
 *
 *	for use with splits for every category: speedrun.com/re1/resources
 *
 *	README: https://github.com/xSHiiDOx/BiohazardAutoSplitter/blob/master/README.md
*/

state("Biohazard")
{
	/*
	*	Pointer paths
	*/
	
	uint time : "Biohazard.exe", 0x6A8E10;
	byte stage_ID : "Biohazard.exe", 0x8386F0;
	byte room_ID : "Biohazard.exe", 0x8386F1;
	byte cam_ID : "Biohazard.exe", 0x8386F2;
	uint flags1 : "Biohazard.exe", 0x833090;
}

startup
{
	/*
	*	Message at beginning - if hit Button "Cancel" it opens the readme online
	*	Message deactivated
	*/
	
	/*
	* 	var message = MessageBox.Show(timer.Form, "Autosplitter for every category - including Category Extensions.\n" + "For more information (like using old autosplitter or update own splits) go to Readme by pressing CANCEL.\n" + "Report bugs / feedback to the moderators of RE1 on speedrun.com/re1.\n", "NEW AUTOSPLITTER", MessageBoxButtons.OKCancel);
	* 	if(message == DialogResult.Cancel)
	* 	{
	* 		Process.Start("https://github.com/xSHiiDOx/BiohazardAutoSplitter/blob/master/README.md");
	* 	}
	*/

}
isLoading
{
	return true;
}

start
{
	/*
	*	Triggering start by using specific camera-angle from main-hall cutscene at beginning
	*/
	
	return current.cam_ID == 0 && current.stage_ID == 0 && current.room_ID == 6;
}

reset
{
	/*
	*	Reset is triggered when IGT goes lower the old one - IGT always starts at 0
	*/
	
	return current.time < old.time;
}

split
{
	/*
	*	Every door-animation (ladders, etc.) changes room_ID, stage_ID or both
	*	So any change triggers a split
	*	At specific moments in the game (Leaving Mansion 1, Entering Guardhouse, Leavin Guardhouse, etc.) the autosplitter will check for the right ID's and maybe waits
	*	Additional splits (missroutings, extra-splits, etc.) will be catched up in this moments
	*/

	if(timer.CurrentSplit.Name == "Laboratory - End")
	{
		return current.flags1 == 0;
	} else if(timer.CurrentSplit.Name == "End")	// Prevents old splits from ending to early - Bugfix 03-23-2021
	{
		return current.flags1 == 0;
	} else if(timer.CurrentSplit.Name == "Underground")
	{
		return current.room_ID == 0 && current.stage_ID == 4;
	} else if(timer.CurrentSplit.Name == "Courtyard 3")
	{
		return current.room_ID == 7 && current.stage_ID == 2;
	} else if(timer.CurrentSplit.Name == "Mansion 2")
	{
		return current.room_ID == 0 && current.stage_ID == 2;
	} else if(timer.CurrentSplit.Name == "Courtyard 2")
	{
		return current.room_ID == 27 && current.stage_ID == 5;
	} else if(timer.CurrentSplit.Name == "Guardhouse")
	{
		return current.room_ID == 4 && current.stage_ID == 2;
	} else if(timer.CurrentSplit.Name == "Courtyard 1")
	{
		return current.room_ID == 0 && current.stage_ID == 3;
	} else if(timer.CurrentSplit.Name == "Mansion 1")
	{
		return current.room_ID == 0 && current.stage_ID == 2;
	} else if(current.stage_ID != old.stage_ID)
	{
		return current.stage_ID != old.stage_ID;
	} else
	{
		return current.room_ID != old.room_ID;
	}
}

gameTime
{
	/*
	*	Game runs at 30fps
	*/
	
	return TimeSpan.FromSeconds(current.time / 30.0);
}
