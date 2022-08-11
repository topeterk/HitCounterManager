state("Cuphead") {}

startup
{
	vars.Log = (Action<object>)(output => print("[Cuphead] " + output));

	var bytes = File.ReadAllBytes(@"Components\LiveSplit.ASLHelper.bin");
	var type = Assembly.Load(bytes).GetType("ASLHelper.Unity");
	vars.Helper = Activator.CreateInstance(type, timer, this);

	vars.Splits = new Dictionary<string, string>();
	vars.SceneLevels = new Dictionary<string, int>();

	vars.Grades = new[] { "D-", "D", "D+", "C-", "C", "C+", "B-", "B", "B+", "A-", "A", "A+", "S", "P" };
	vars.Difficulties = new[] { "Simple", "Regular", "Expert" };

	vars.BossHG = new[] { 8, 11, 12 }; // B+, A+, S for Simple, Regular, Expert
	vars.PRank = 13;

	settings.Add("ilMode", false, "Use IL timer?");

	settings.Add("ilTimeLoadless", false, "Use Loadless time instead of IGT?", "ilMode");
	settings.SetToolTip("ilTimeLoadless", "Use loadless time (reflecting real runs) instead of the timer in-game.\nIncludes pauses / parries and removes loads on King Dice.");

	settings.Add("highestGrade", false, "Only split on highest grade.");
	settings.SetToolTip("highestGrade", "Only splits on levels with grades when they have been completed\nwith the highest grade for difficulty (B+, A+, S, or P).\nDoes not affect IL mode.");

	settings.Add("splits", true, "Splits:");

	var xml = System.Xml.Linq.XDocument.Load(@"Components\Cuphead.Splits.xml");
	foreach (var split in xml.Element("Splits").Elements("Split"))
	{
		string id = split.Attribute("ID").Value, name = split.Attribute("Name").Value;
		string tt = split.Attribute("ToolTip").Value, splitType = split.Attribute("Type").Value;

		settings.Add(id, false, name, "splits");
		settings.SetToolTip(id, tt);

		vars.Splits[id] = splitType;
		if (splitType == "LEVEL_COMPLETE")
			vars.SceneLevels[id] = int.Parse(split.Attribute("Level").Value);
	}

	vars.Helper.AlertLoadless("Cuphead");

	vars.CompletedSplits = new List<string>();
}

onStart
{
	timer.IsGameTimePaused = true;
	vars.CompletedSplits.Clear();

	if (!current.InILMode)
		timer.SetGameTime(TimeSpan.Zero);
}

onSplit
{}

onReset
{
	current.IsKDLevelEnding = false;
}

init
{
	int PTR_SIZE = game.Is64Bit() ? 0x8 : 0x4;

	current.SaveSlot = IntPtr.Zero;
	current.IsKDLevelEnding = false;

	vars.Helper.TryOnLoad = (Func<dynamic, bool>)(mono =>
	{
		#region PlayerData
		var pd = mono.GetClass("PlayerData");

		vars.Helper["inGame"] = pd.Make<bool>("inGame");
		vars.Helper["saveSlotIndex"] = pd.Make<int>("_CurrentSaveFileIndex");
		vars.Helper["saveFiles"] = pd.MakeArray<IntPtr>("_saveFiles");

		vars.GetCurrentSave = (Func<IntPtr>)(() =>
		{
			var slot = vars.Helper["saveSlotIndex"].Current;

			if (vars.Helper["saveFiles"].Current.Length < 2)
				return IntPtr.Zero;

			return vars.Helper["saveFiles"].Current[slot];
		});

		// Level Completion
		var pldm = mono.GetClass("PlayerLevelDataManager");
		var pldo = mono.GetClass("PlayerLevelDataObject");
		if (pldm.Fields.Count == 0 || pldo.Fields.Count == 0)
			return false;

		vars.GetAllLevelsData = (Func<List<dynamic>>)(() =>
		{
			var levels = vars.Helper.ReadList<IntPtr>(current.SaveSlot + pd["levelDataManager"], pldm["levelObjects"]);
			var ret = new List<dynamic>();

			foreach (var level in levels)
			{
				var id = vars.Helper.Read<int>(level + pldo["levelID"]);
				var completed = vars.Helper.Read<bool>(level + pldo["completed"]);
				var grade = vars.Helper.Read<int>(level + pldo["grade"]);
				var difficulty = vars.Helper.Read<int>(level + pldo["difficultyBeaten"]);

				ret.Add(new
				{
					ID = id,
					Completed = completed,
					Grade = grade,
					Difficulty = difficulty
				});
			}

			return ret;
		});

		vars.IsLevelCompleted = (Func<int, int, int, bool>)((levelId, targetDifficulty, targetGrade) =>
		{
			foreach (var level in vars.GetAllLevelsData())
			{
				if (level.ID == levelId)
				{
					return level.Completed && level.Grade >= targetGrade && level.Difficulty >= targetDifficulty;
				}
			}

			return false;
		});

		vars.IsInOverworld = (Func<bool>)(() =>
		{
			return current.Scene == "scene_map_world_1"
			       || current.Scene == "scene_map_world_2"
			       || current.Scene == "scene_map_world_3"
			       || current.Scene == "scene_map_world_4"
			       || current.Scene == "scene_map_world_DLC";
		});
		#endregion // PlayerData

		#region Level
		var lvl = mono.GetClass("Level");
		var lsd = mono.GetClass("LevelScoringData");

		vars.Helper["lvlType"] = lvl.Make<int>("Current", "type");
		vars.Helper["lvlTime"] = lvl.Make<float>("Current", "LevelTime");
		vars.Helper["lvlDifficulty"] = lvl.Make<int>("Current", "mode");
		vars.Helper["lvlEnding"] = lvl.Make<bool>("Current", "Ending");
		vars.Helper["lvlWon"] = lvl.Make<bool>("Won");

		vars.Helper["lvlIsDicePalace"] = lvl.Make<bool>("IsDicePalace");
		vars.Helper["lvlIsDicePalaceMain"] = lvl.Make<bool>("IsDicePalaceMain");

		vars.Helper["lsdTime"] = lvl.Make<float>("ScoringData", lsd["time"]);
		#endregion // Level

		#region SceneLoader
		var sl = mono.GetClass("SceneLoader");

		vars.Helper["sceneName"] = sl.MakeString("SceneName");
		vars.Helper["lvl"] = sl.Make<int>("CurrentLevel");
		vars.Helper["doneLoading"] = sl.Make<bool>("_instance", "doneLoadingSceneAsync");
		#endregion // SceneLoader

		return true;
	});

	vars.Helper.Load();
}

update
{
	if (!vars.Helper.Update())
		return false;

	current.SaveSlot = vars.GetCurrentSave();
	if (current.SaveSlot == IntPtr.Zero)
		return false;

	current.InILMode = settings["ilMode"] && timer.Run.Count == 1;

	current.Loading = !vars.Helper["doneLoading"].Current;
	current.Scene = vars.Helper["sceneName"].Current;

	current.InGame = vars.Helper["inGame"].Current;
	current.InOverworld = vars.IsInOverworld();
	current.InKingDice = vars.Helper["lvlIsDicePalace"].Current;
	current.InKingDiceMain = vars.Helper["lvlIsDicePalaceMain"].Current;

	current.Level = vars.Helper["lvl"].Current;
	current.Time = vars.Helper["lvlTime"].Current;
	// LevelScoringData#time
	current.LSDTime = vars.Helper["lsdTime"].Current;
	current.Difficulty = vars.Helper["lvlDifficulty"].Current;
	current.IsEnding = vars.Helper["lvlEnding"].Current;
	current.HasWon = vars.Helper["lvlWon"].Current;

	// Battle=0 (B+/A+/S), Tutorial=1 (N/A), Platforming=2 (P)
	current.Type = vars.Helper["lvlType"].Current;
	current.HighestGrade =
	    current.Type == 0 ? vars.BossHG[current.Difficulty] :
	    current.Type == 2 ? vars.PRank : -1;

	if (current.Scene == "scene_win")
		current.Scene = old.Scene;

	// auto-reset after results screen
	if (current.InILMode && settings.ResetEnabled && timer.CurrentPhase == TimerPhase.Ended
	    && (current.Time == 0f || current.InOverworld))
	{
		vars.Log("Resetting due to IL End | Time: " + current.Time + " | IsOverworld: " + current.InOverworld);
		vars.Helper.Timer.Reset();
	}

	/* King Dice consists of several levels. The game gets the final time for the boss by summing
	 * the level times of each of these. Every time a level finishes, it appends the current.Time to the current.LSDTime.
	 *
	 * If we are in a miniboss (i.e. InKingDiceMain is false), the time is updated when the boss is defeated. This syncs with
	 * HasWon from false -> true and IsEnding from false -> true (when ilEnd would fire). current.Time also freezes.
	 *
	 * If we are InKingDiceMain, then time is updated briefly after the space on the board is selected (and we are transitioning
	 * into a boss). HasWon and IsEnding are not updated, and current.Time doesn't freeze.
	 *
	 * So to check if a level is ending we check if the LSDTime is updated (and isn't being reset).
	 * We set this value back to false when we transition from the main palace to a miniboss or vica-versa (the next level has started).
	*/
	if (old.InKingDiceMain != current.InKingDiceMain)
	{
		current.IsKDLevelEnding = false;
	}

	if (current.InKingDice && old.LSDTime != current.LSDTime && current.LSDTime != 0)
	{
		current.IsKDLevelEnding = true;
	}
}

start
{
	// start timer on ilEnter when in ILMode
	if (current.InILMode && old.Time == 0f && current.Time > 0f)
	{
		vars.Log("Starting due to IL Enter | Time: " + old.Time + " -> " + current.Time);
		return true;
	}

	if (current.Scene == "scene_cutscene_intro" && current.InGame && current.Loading)
	{
		vars.Log("Starting due to save select | InGame: " + current.InGame + " | Loading: " + current.Loading);
		return true;
	}
}

split
{
	if(current.InILMode)
	{
		if ((current.InKingDice && (!current.InKingDiceMain || !current.IsKDLevelEnding)) || current.Time == 0f || !current.HasWon)
			return false;

		vars.Log("Splitting due to IL End | Time: " + current.Time + " | HasWon: " + current.HasWon);
		return true;
	}

	foreach (var split in vars.Splits)
	{
		string id = split.Key, type = split.Value;

		if (!settings[id] || vars.CompletedSplits.Contains(id))
			continue;

		switch (type)
		{
			case "SCENE_ENTER":
			{
				if (old.Scene != id && current.Scene == id)
				{
					vars.Log("SCENE_ENTER | " + id);

					vars.CompletedSplits.Add(id);
					return true;
				}

				continue;
			}

			case "SCENE_LEAVE":
			{
				if (old.Scene == id && current.Scene != id)
				{
					vars.Log("SCENE_LEAVE | " + id);

					vars.CompletedSplits.Add(id);
					return true;
				}

				continue;
			}

			case "LEVEL_COMPLETE":
			{
				var targetGrade = settings["highestGrade"] ? current.HighestGrade : -1;
				if (current.Scene == id
				    && vars.SceneLevels.ContainsKey(id) && current.Level == vars.SceneLevels[id]
				    && vars.IsLevelCompleted(current.Level, -1, targetGrade))
				{
					vars.Log("LEVEL_COMPLETE | " + id + " in " + current.Time);

					vars.CompletedSplits.Add(id);
					return true;
				}

				continue;
			}

			case "ENDING":
			{
				var scene = id.Substring(0, id.Length - 2);
				var diff = int.Parse(id[id.Length - 1].ToString());
				if (!old.IsEnding && current.IsEnding && current.Scene == scene && current.Difficulty == diff)
				{
					vars.Log("ENDING | " + id);

					vars.CompletedSplits.Add(id);
					return true;
				}

				continue;
			}
		}
	}
}

reset
{
	if (current.InILMode && (current.Loading && current.LSDTime == 0f || current.InOverworld))
	{
		vars.Log("Resetting due to reset {} | Time: " + current.Time + " | InOverworld: " + current.InOverworld + " | InKingDice: " + current.InKingDice + " | Loading: " + current.Loading);
		current.IsKDLevelEnding = false;
		return true;
	}
}

gameTime
{
	if (!current.InILMode || settings["ilTimeLoadless"])
		return;

	if (!current.InKingDice)
		return TimeSpan.FromSeconds(current.Time);

	// King dice is a series of levels whose time at the end is a sum of all levels, updated after each level is completed.
	// If a level is ending we just use that time, otherwise we update the current time with the current level time
	var time = current.IsKDLevelEnding ? current.LSDTime : current.LSDTime + current.Time;
	return TimeSpan.FromSeconds(time);
}

isLoading
{
	return (current.InILMode && !settings["ilTimeLoadless"]) || current.Loading;
}

exit
{
	vars.Helper.Dispose();
}

shutdown
{
	vars.Helper.Dispose();
}
