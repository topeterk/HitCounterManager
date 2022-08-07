// Resident Evil 6 Autosplitter Version 1.2.0 11/05/2022
// Supports IGT
// Supports both single and all campaigns + all difficulties
// Splits for campaigns can be obtained from https://www.speedrun.com/re6/resources
// Script & Pointers by TheDementedSalad

state ("BH6", "1.06") {
	float LeonGT: 0x13B8CF0, 0x8652D4;
	float HelenaGT: 0x13B8CF0, 0x86538C;
	float ChrisGT: 0x13B8CF0, 0x865444;
	float PiersGT: 0x13B8CF0, 0x8654FC;
	float JakeGT: 0x13B8CF0, 0x8655B4;
	float SherryGT: 0x13B8CF0, 0x86566C;
	float AdaGT: 0x13B8CF0, 0x865724;
	float AgentGT: 0x13B8CF0, 0x8657DC;
	byte SelCamp: 0x13C549C, 0x41290;
	byte Menu: 0x13C623C, 0x2C, 0x258, 0x48, 0x147;
	byte Cutscene: 0x13C5454, 0x86C;
	int CurLvl: 0x13C549C, 0x412A4;
	byte CharSel: 0x13B8CF0, 0x81F5A4; 
	int DA: 0x13C6468, 0x4120; 
}

state ("BH6", "1.1.0") {
	float LeonGT: 0x13C2CF0, 0x8652D4;
	float HelenaGT: 0x13C2CF0, 0x86538C;
	float ChrisGT: 0x13C2CF0, 0x865444;
	float PiersGT: 0x13C2CF0, 0x8654FC;
	float JakeGT: 0x13C2CF0, 0x8655B4;
	float SherryGT: 0x13C2CF0, 0x86566C;
	float AdaGT: 0x13C2CF0, 0x865724;
	float AgentGT: 0x13C2CF0, 0x8657DC;
	byte SelCamp: 0x13CF49C, 0x41290;
	byte Menu: 0x13D023C, 0x2C, 0x258, 0x48, 0x147;
	byte Cutscene: 0x13CF454, 0x86C;
	int CurLvl: 0x13CF49C, 0x412A4;
	byte CharSel: 0x13C2CF0, 0x81F5A4; 
	int DA: 0x13D0468, 0x4120; 
}

init {
	int gmSize = modules.First().ModuleMemorySize;
	switch (gmSize) {
		case 21630976:
			version = "1.06";
			break;
		case 21671936:
			version = "1.1.0";
			break;
	}
	
	vars.completedSplits = new List<int>();
}

startup {
	vars.totalGameTime = 0;
	vars.currentGT = 0;
	vars.oldGT = 0;

	vars.Storages = new List<HashSet<int>>
	{
		new HashSet<int>
		{105,101,102,103,
		200,201,202,279,209,203,
		204,206,250,
		510,511,512,514,
		701,706,702,773},
		new HashSet<int>
		{501,502,503,
		301,302,303,
		506,507,508,512,550,
		801,872,851,
		902,972,903,973},
		new HashSet<int>
		{305,307,302,306,
		401,402,
		601,602,
		506,515,579,510,578,
		902,905,950},
		new HashSet<int>
		{1001,1003,
		203,272,
		509,516,578,
		871,804,
		706,703,702},
	};
	
	settings.Add("CampType", true, "Select Campaign Type");
	settings.Add("SingC", false, "Single Campaign", "CampType");
	settings.Add("AllC", false, "All Campaigns", "CampType");
	settings.Add("Split", true, "Select Split Type");
	settings.Add("Sub", false, "Sub Chapter Splits", "Split");
	settings.Add("Full", false, "Full Chapter Splits", "Split");
}

update{
    if (timer.CurrentPhase == TimerPhase.NotRunning)
    {
    vars.currentGT = 0;
	vars.oldGT = 0;
	vars.totalGameTime = 0;
	vars.completedSplits.Clear();
    }
	
	if (current.CurLvl == 104 && old.CurLvl == 1100 || current.CurLvl == 500 && old.CurLvl == 1110 || current.CurLvl == 304 && old.CurLvl == 1101 || current.CurLvl == 1000 && old.CurLvl == 1130)
	{
		vars.completedSplits.Add(old.CurLvl);
		return true;
	}
}

start {
	
	vars.totalGameTime = 0;
	vars.currentGT = 0;
	vars.oldGT = 0;
	
	switch ((byte)current.CharSel)
	{
	case 0:
		return current.SelCamp == 0 && current.LeonGT > 0 && old.LeonGT == 0 && current.DA > 0;
	case 1:
		return current.SelCamp == 0 && current.HelenaGT > 0 && old.HelenaGT == 0 && current.DA > 0;
	case 2:
		return current.SelCamp == 1 && current.ChrisGT > 0 && old.ChrisGT == 0 && current.DA > 0;
	case 3:
		return current.SelCamp == 1 && current.PiersGT > 0 && old.PiersGT == 0 && current.DA > 0;
	case 4:
		return current.SelCamp == 2 && current.JakeGT > 0 && old.JakeGT == 0 && current.DA > 0;
	case 5:
		return current.SelCamp == 2 && current.SherryGT > 0 && old.SherryGT == 0 && current.DA > 0; 
	case 6:
		return current.SelCamp == 3 && current.AdaGT > 0 && old.AdaGT == 0 && current.DA > 0;
	case 7:
		return current.SelCamp == 3 && current.AgentGT > 0 && old.AgentGT == 0 && current.DA > 0;
	}
}

split {
	if (settings["Sub"] || settings["Full"]){
		if (vars.currentGT != vars.oldGT && vars.currentGT == 0 && vars.totalGameTime != 0 && current.DA != 0 && current.Menu != 4){
		return true;
		}

	}
	
	if (settings["Sub"]){
	return current.CurLvl != old.CurLvl && vars.Storages[current.SelCamp].Contains(current.CurLvl) && old.CurLvl != 804 && current.Menu != 4;
	}
}

isLoading {
	return true;
}

gameTime {
	switch ((byte)current.CharSel)
	{
	case 0:
	  vars.currentGT = current.LeonGT;
      vars.oldGT = old.LeonGT;
      break;
	case 1:
      vars.currentGT = current.HelenaGT;
      vars.oldGT = old.HelenaGT;
      break;
	case 2:
      vars.currentGT = current.ChrisGT;
      vars.oldGT = old.ChrisGT;
      break;
	case 3:
      vars.currentGT = current.PiersGT;
      vars.oldGT = old.PiersGT;
      break;
	case 4:
      vars.currentGT = current.JakeGT;
      vars.oldGT = old.JakeGT;
      break;
	case 5:
      vars.currentGT = current.SherryGT;
      vars.oldGT = old.SherryGT;
      break;
	case 6:
      vars.currentGT = current.AdaGT;
      vars.oldGT = old.AdaGT;
      break;
	case 7:
      vars.currentGT = current.AgentGT;
      vars.oldGT = old.AgentGT;
      break;
	}
	
	if(vars.currentGT > vars.oldGT){
    return TimeSpan.FromSeconds(System.Math.Floor(vars.totalGameTime + vars.currentGT));
    }
    if (vars.currentGT == 0 && vars.oldGT > 0 && current.DA != 0 && current.Menu != 4){
        vars.totalGameTime = System.Math.Floor(vars.totalGameTime + vars.oldGT);
        return TimeSpan.FromSeconds(System.Math.Floor(vars.totalGameTime + vars.currentGT));
        }
    } 
	
reset 
{
	if(settings["SingC"]){
	if (vars.oldGT == vars.currentGT && current.Menu == 4 && current.Cutscene == 0){
		return true;
	}
	}
}
