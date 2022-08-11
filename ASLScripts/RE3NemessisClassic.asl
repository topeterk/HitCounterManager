/*
By deserteagle417
Thanks to Mysterion for helping me tons with the RE0 autosplitter, which I used as a base for the changes to the original RE3 script.
Thanks to CursedToast for inspiration on a lot of ideas and helpful conversations!
Thanks to everyone who contributed the original scripts for REbirth version, Mediakite version, Taiwanese version, and the Chinese version;
they were incorporated into this script.
Thanks to Syrelash for asking me a very simple question when working on this that fixed a very big issue!!
Thanks to Hamo for testing many revisions of this!
*/

state("bio3_PC", "CHN/TWN")
{
    uint hp        : 0x6FFC00;
	uint save      : 0x6FF884;
	uint total     : 0x704E98;
	uint current   : 0x13706C, 0x5ac;
	uint gameState : 0x6FF7C0;
    byte roomID    : 0x704EE8;
    byte camID     : 0x704EEA;
    byte oldRoom   : 0x704EEC;
    byte stageID   : 0x704EED;
    byte oldCam    : 0x704EEE;
    byte character : 0x704EF6;
	byte oldStage  : 0x6E7F95;
}

state("bio3_PC", "Mediakite")
{
    uint hp        : 0x65BE60;
	uint save      : 0x65BAE4;
	uint total     : 0x6610F8;
	uint current   : 0x13399C, 0x5ac;
	uint gameState : 0x65BA20;
    byte roomID    : 0x661148;
    byte camID     : 0x66114A;
    byte oldRoom   : 0x66114C;
    byte stageID   : 0x66114D;
    byte oldCam    : 0x66114E;
	byte oldStage  : 0x6441F5;
    byte character : 0x66D656;
}

state("BIOHAZARD(R) 3 PC", "REbirth")
{
	uint hp        : 0x6620E0;
	uint save      : 0x667378;
	uint total     : 0x661D64;
	uint current   : 0x6449D4;
	uint gameState : 0x661CA0;
    byte stageID   : 0x6673C6;
	byte roomID    : 0x6673C8;
    byte camID     : 0x6673CA;
    byte oldRoom   : 0x6673CC;
	byte oldStage  : 0x6673CD;
    byte oldCam    : 0x6673CE;
    byte character : 0x6673D6;
}

startup
{
	//Enable Door Splits
	settings.Add("doors", true, "Enable door splits for every door in the route. Only use one!");
        settings.CurrentDefaultParent = "doors";
		settings.Add("any", true, "any% Door Splits (210 total splits)");
        settings.SetToolTip("any", "Compatible with the splits which end in ``Fin''.");
		settings.Add("nemmy", false, "Nemesis% Door Splits (211 total splits)");
        settings.Add("knife", false, "any% Knife Only Splits (216 total splits)");
        settings.CurrentDefaultParent = null;
    settings.Add("alts", false, "Door Split Alterations");
        settings.CurrentDefaultParent = "alts";
        settings.Add("reload", false, "Add splits for reloading rooms (adds 4 splits)");
        settings.SetToolTip("reload", "Adds splits for exiting and re-entering basement and System Disk rooms");
        settings.Add("rng", false, "Include Doors From Major RNG Segments (adds 16 splits)");
        settings.SetToolTip("rng", "Adds in door splits from the train crash until exiting the chapel and from exiting the elevator of the hospital to returning to the save room.");
        settings.Add("hosB3", false, "Hospital B3 before 4F (must be using the Major RNG Segments option above)");
        settings.SetToolTip("hosB3", "Automatically built into any% Knife Only splits");
        settings.Add("early", false, "Alters the route to get early crank");
        settings.SetToolTip("early", "Automatically built into Nemesis% splits");
        settings.CurrentDefaultParent = null;
    settings.Add("exist", false, "Other Door Split Sets (disables all of the above options)");
        settings.CurrentDefaultParent = "exist";
        settings.Add("hiear", false, "Use the ``Over Hiear'' any% splits");
        settings.CurrentDefaultParent = null;
    settings.Add("basic", false, "Basic Door Splits (disables all of the above options)");
        settings.SetToolTip("basic", "Splits on every door transition.");

    //Variables for our settings
	vars.KeyItems = new List<int>()
    {115, 65,  66, 117, 114, 
       6, 69,  50,  68,  60, 
      79, 80,  49,  52,  63,
      54, 51,  95,  57, 120,
     119, 94,  76,  77,  64, 
     116, 82,  81,  85, 123,
     124, 61, 125, 126, 112, 
     111, 96};

	vars.KeyItemSettings = new List<String>()
	{"Wherehouse Key", "Lighter Oil", "Lighter", "STARS Key", "Lockpick",
    "Grenade Launcher", "Blue Gem", "Fire Hook", "Green Gem", "Wrench",
    "Bronze Book", "Bronze Compass", "Battery", "Fuse", "Fire Hose",
    "Oil Additive", "Power Cable", "Rusted Crank", "Machine Oil", "Winder Key", 
    "Bezel Key", "Chronos Chain", "Gold Gear", "Silver Gear", "Tape Recorder", 
    "Sickroom Key", "Vaccine Base", "Vaccine Medium", "Vaccine", "Main Gate Key", 
    "Graveyard Key", "Iron Pipe", "Rear Gate Key", "Facility Key", "System Disc", 
    "Water Sample", "Card Key"};

    //any% Item Split Options
	settings.Add("item", false, "Key Item Splits");
		settings.CurrentDefaultParent = "item";
		for(int i = 0; i < 37; i++){
        	settings.Add("" + vars.KeyItems[i].ToString(), true, "" + vars.KeyItemSettings[i].ToString());
    	}
		settings.CurrentDefaultParent = null;

    vars.ExtraItems = new List<int>()
	{  4, 67,  5,  58, 12,
     121, 72, 71,  70, 78,
      88, 89, 90,  13, 91,
      92, 16, 42, 110, 43,
      47, 55, 56, 127, 10};

	vars.ExtraItemsSettings = new List<String>()
	{"Shotgun", "Lighter (w/ Oil)", "Magnum", "Mixed Oil", "Mine Thrower",
    "Chronos Key", "Crystal Ball", "Obsidian Ball", "Amber Ball", "Chronos Gear",
    "Medium Base", "EAGLE Parts A", "EAGLE Parts B", "EAGLE 6.0", "M37 Parts A",
    "M37 Parts B", "Western Custom", "First Aid Box", "Infinite Ammunition", "Crank",
    "STARS Card (Jill)", "Card Case", "STARS Card (Brad)", "Facility Key (Stripped)", "Rocket Launcher"};

	//Additional Item Split Options
	settings.Add("extra", false, "Additional Items to Split On");
		settings.CurrentDefaultParent = "extra";
		for(int i = 0; i < 25; i++)
        {
        	settings.Add("" + vars.ExtraItems[i].ToString(), false, "" + vars.ExtraItemsSettings[i].ToString());
    	}
		settings.CurrentDefaultParent = null;

    //Fully Customizabale Door Splits -- default true values match with standard any% route
    settings.Add("custom", false, "Fully Customizable Door Splits (splits upon leaving room)");
    settings.SetToolTip("custom", "Defaults are the standard any% route with no room resets or RNG sections");
        settings.CurrentDefaultParent = "custom";
        settings.Add("1000", true, "Starting Alley");
        settings.Add("1001", true, "Warehouse");
        settings.Add("1002", true, "Warehouse Save");
        settings.Add("1003", true, "Warehouse");
        settings.Add("1004", true, "Warehouse Alley");
        settings.Add("1005", true, "Warehouse Street");
        settings.Add("1006", false, "Basement Reset (Exit)");
        settings.Add("1007", false, "Basement Reset (Re-enter)");
        settings.Add("1008", true, "Basement Alley");
        settings.Add("1009", true, "Bar Jack Street");
        settings.Add("1010", true, "Bar Jack Alley");
        settings.Add("1011", true, "Bar Jack");
        settings.Add("1012", true, "Bar Jack Alley");
        settings.Add("1013", true, "Barricade Alley");
        settings.Add("1014", false, "Burning Alley to Save Room (Nemesis%)");
        settings.Add("1015", false, "Save Room to Burning Alley (Nemesis%)");
        settings.Add("1016", true, "Burning Alley");
        settings.Add("1017", true, "RPD Street");
        settings.Add("1018", true, "RPD Front");
        settings.Add("1019", true, "RPD Hall");
        settings.Add("1020", true, "RPD West Office");
        settings.Add("1021", true, "RPD Evidence Room");
        settings.Add("1022", true, "RPD 1F Stairs");
        settings.Add("1023", true, "RPD 2F Stairs");
        settings.Add("1024", true, "STARS Hallway");
        settings.Add("1025", true, "STARS Office");
        settings.Add("1026", true, "STARS Hallway");
        settings.Add("1027", true, "RPD 2F Stairs");
        settings.Add("1028", true, "RPD 1F Stairs");
        settings.Add("1029", true, "RPD Evidence Room");
        settings.Add("1030", true, "RPD West Office");
        settings.Add("1031", true, "RPD Hall");
        settings.Add("1032", true, "RPD Front");
        settings.Add("1033", true, "RPD Street");
        settings.Add("1034", true, "Parasite Alley");
        settings.Add("1035", true, "Firehose Alley");
        settings.Add("1036", true, "Bus Street");
        settings.Add("1037", true, "Parking Lot");
        settings.Add("1038", true, "Parking Lot Save");
        settings.Add("1039", true, "Parking Lot Back");
        settings.Add("1040", true, "Construction Site");
        settings.Add("1041", true, "Compass Alley");
        settings.Add("1042", true, "Shopping Alley");
        settings.Add("1043", false, "Restaurant to Shopping Alley (Nemesis%)");
        settings.Add("1044", true, "Restaurant Basement");
        settings.Add("1045", true, "Restaurant Basement to Shopping Alley");
        settings.Add("1046", false, "Shopping Alley to Save Room (Early Crank)");
        settings.Add("1047", false, "Save Room to Shopping Alley (Early Crank)");
        settings.Add("1048", true, "Shopping Alley");
        settings.Add("1049", true, "Press Street");
        settings.Add("1050", true, "Press 1F");
        settings.Add("1051", true, "Press 2F");
        settings.Add("1052", true, "Press 1F");
        settings.Add("1053", true, "Press Street");
        settings.Add("1054", true, "Y Alley");
        settings.Add("1055", true, "Party Party Alley");
        settings.Add("1056", true, "Lonsdale Yard");
        settings.Add("1057", true, "Train Station");
        settings.Add("1058", true, "Rear Train Car");
        settings.Add("1059", true, "Front Train Car");
        settings.Add("1060", true, "Train Station");
        settings.Add("1061", true, "Lonsdale Yard");
        settings.Add("1062", true, "Party Party Alley");
        settings.Add("1063", false, "Y Alley to Gas Station (Early Crank)");
        settings.Add("1064", false, "Gas Station Street (Early Crank)");
        settings.Add("1065", false, "Gas Station Inside (Early Crank)");
        settings.Add("1066", false, "Gas Station Street (Early Crank)");
        settings.Add("1067", true, "Y Alley to Press Street");
        settings.Add("1068", true, "Press Street");
        settings.Add("1069", true, "Shopping Alley");
        settings.Add("1070", true, "Compass Alley");
        settings.Add("1071", true, "Shopping Alley");
        settings.Add("1072", true, "Press Street");
        settings.Add("1073", true, "Y Alley");
        settings.Add("1074", true, "Press Street");
        settings.Add("1075", true, "Shopping Alley");
        settings.Add("1076", true, "Compass Alley");
        settings.Add("1077", true, "Construction Site");
        settings.Add("1078", true, "Power Station Outside");
        settings.Add("1079", true, "Power Station Access");
        settings.Add("1080", true, "Power Station ");
        settings.Add("1081", true, "Power Station Outside");
        settings.Add("1082", true, "Construction Site");
        settings.Add("1083", true, "Parking Lot Back");
        settings.Add("1084", true, "Parking Lot Save");
        settings.Add("1085", true, "Parking Lot");
        settings.Add("1086", true, "Bus Street");
        settings.Add("1087", true, "Firehose Alley");
        settings.Add("1088", true, "Parasite Alley");
        settings.Add("1089", true, "RPD Street");
        settings.Add("1090", true, "Burning Alley");
        settings.Add("1091", true, "Alley to Sales Office");
        settings.Add("1092", true, "Outside Sales Office");
        settings.Add("1093", true, "Sales Office");
        settings.Add("1094", true, "Sales Office Storage");
        settings.Add("1095", true, "Sales Office");
        settings.Add("1096", true, "Outside Sales Office");
        settings.Add("1097", true, "Alley to Sales Office");
        settings.Add("1098", true, "Burning Alley");
        settings.Add("1099", true, "RPD Street");
        settings.Add("1100", true, "Parasite Alley");
        settings.Add("1101", true, "Firehose Alley");
        settings.Add("1102", true, "Bus Street");
        settings.Add("1103", true, "Parking Lot");
        settings.Add("1104", true, "Parking Lot Save");
        settings.Add("1105", true, "Parking Lot Back");
        settings.Add("1106", true, "Construction Site");
        settings.Add("1107", true, "Compass Alley");
        settings.Add("1108", true, "Shopping Alley (Late Crank)");
        settings.Add("1109", true, "Rusty Crank Save (Late Crank)");
        settings.Add("1110", true, "Shopping Alley");
        settings.Add("1111", true, "Press Street");
        settings.Add("1112", true, "Y Alley to Gas Station (Late Crank)");
        settings.Add("1113", true, "Gas Station Street (Late Crank)");
        settings.Add("1114", true, "Gas Station Inside (Late Crank)");
        settings.Add("1115", true, "Gas Station Street (Late Crank)");
        settings.Add("1116", true, "Y Alley");
        settings.Add("1117", true, "Party Party Alley");
        settings.Add("1118", true, "Worm Encounter");
        settings.Add("1119", true, "Train Station");
        settings.Add("1120", true, "Rear Train Car");
        settings.Add("1121", true, "Front Train Car");
        settings.Add("1122", true, "Rear Train Car (Moving)");
        settings.Add("1123", true, "Train Crash");
        settings.Add("1124", false, "Clock Tower Bedroom");
        settings.Add("1125", false, "Chess Room");
        settings.Add("1126", false, "Library");
        settings.Add("1127", false, "CT Hall 1F");
        settings.Add("1128", false, "Dining Room ");
        settings.Add("1129", false, "Piano Room");
        settings.Add("1130", true, "Chapel");
        settings.Add("1131", true, "Piano Room");
        settings.Add("1132", true, "Dining Room ");
        settings.Add("1133", true, "CT Hall 1F");
        settings.Add("1134", true, "CT Hall 2F");
        settings.Add("1135", true, "CT Balcony");
        settings.Add("1136", true, "Clock Tower");
        settings.Add("1137", true, "CT Balcony");
        settings.Add("1138", true, "CT Hall 2F");
        settings.Add("1139", true, "CT Hall 1F");
        settings.Add("1140", true, "Library");
        settings.Add("1141", true, "Chronos Corridor");
        settings.Add("1142", true, "Clock Puzzle");
        settings.Add("1143", true, "Chronos Corridor");
        settings.Add("1144", true, "Library");
        settings.Add("1145", true, "CT Hall 1F");
        settings.Add("1146", true, "CT Hall 2F");
        settings.Add("1147", true, "CT Balcony");
        settings.Add("1148", true, "Clock Tower");
        settings.Add("1149", true, "CT Balcony");
        settings.Add("1150", true, "Clock Tower Nemesis Fight");
        settings.Add("1151", true, "Start Carlos Segment");
        settings.Add("1152", true, "Piano Room");
        settings.Add("1153", true, "Dining Room ");
        settings.Add("1154", true, "CT Hall 1F");
        settings.Add("1155", true, "Library");
        settings.Add("1156", true, "Chronos Corridor");
        settings.Add("1157", true, "Clock Puzzle Room");
        settings.Add("1158", true, "Hospital Street");
        settings.Add("1159", true, "Hospital Lobby");
        settings.Add("1160", true, "Hospital Save");
        settings.Add("1161", true, "Hospital Office (4F first)");
        settings.Add("1162", false, "Hospital Office (B3 first)");
        settings.Add("1163", false, "Hospital B3 Hall (B3 first)");
        settings.Add("1164", false, "Hospital Laboratory (B3 first)");
        settings.Add("1165", false, "Hospital Vaccine Room (B3 first)");
        settings.Add("1166", false, "Hospital Laboratory (B3 first)");
        settings.Add("1167", false, "Hospital B3 Hall (B3 first)");
        settings.Add("1168", false, "Hospital 4F Hall");
        settings.Add("1169", false, "Hospital Data Room");
        settings.Add("1170", false, "Hospital 4F Hall");
        settings.Add("1171", false, "Hospital Room 402");
        settings.Add("1172", false, "Hospital 4F Hall (B3 first)");
        settings.Add("1173", false, "Hospital 4F Hall (4F first)");
        settings.Add("1174", false, "Hospital B3 Hall (4F first)");
        settings.Add("1175", false, "Hospital Laboratory (4F first)");
        settings.Add("1176", false, "Hospital Vaccine Room (4F first)");
        settings.Add("1177", false, "Hospital Laboratory (4F first)");
        settings.Add("1178", false, "Hospital B3 Hall (4F first)");
        settings.Add("1179", true, "Hospital Office");
        settings.Add("1180", true, "Hospital Save");
        settings.Add("1181", true, "Hospital Lobby");
        settings.Add("1182", true, "Hospital Street");
        settings.Add("1183", true, "Clock Puzzle Room");
        settings.Add("1184", true, "Chronos Corridor");
        settings.Add("1185", true, "Library");
        settings.Add("1186", true, "CT 1F Hall");
        settings.Add("1187", true, "Dining Room ");
        settings.Add("1188", true, "Piano Room");
        settings.Add("1189", true, "Save Jill");
        settings.Add("1190", true, "Chapel");
        settings.Add("1191", true, "Piano/Dining Room");
        settings.Add("1192", true, "CT 1F Hall");
        settings.Add("1193", true, "Library");
        settings.Add("1194", true, "Chronos Corridor");
        settings.Add("1195", true, "Clock Puzzle Room");
        settings.Add("1196", true, "Hospital Street");
        settings.Add("1197", true, "Park Office");
        settings.Add("1198", true, "Hospital Street");
        settings.Add("1199", true, "Park Entrance");
        settings.Add("1200", true, "Lake Walkway");
        settings.Add("1201", true, "Park Rear Exit");
        settings.Add("1202", true, "Lake Walkway");
        settings.Add("1203", true, "Park Entrance");
        settings.Add("1204", true, "Fountain ");
        settings.Add("1205", true, "Fountain Tunnel");
        settings.Add("1206", true, "Graveyard");
        settings.Add("1207", true, "Graveyard Cabin");
        settings.Add("1208", true, "Cabin Radio Room");
        settings.Add("1209", true, "Graveyard Cabin");
        settings.Add("1210", true, "Gravedigger Fight");
        settings.Add("1211", true, "Fountain Tunnle");
        settings.Add("1212", true, "Fountain");
        settings.Add("1213", true, "Park Entrance");
        settings.Add("1214", true, "Lake Walkway");
        settings.Add("1215", true, "Park Rear Exit");
        settings.Add("1216", true, "Factory Access");
        settings.Add("1217", true, "Factory Main Hall");
        settings.Add("1218", true, "Factory Save");
        settings.Add("1219", true, "Steam Room");
        settings.Add("1220", true, "Factory Save");
        settings.Add("1221", true, "Factory Main Hall");
        settings.Add("1222", false, "Treatment Room Reset (Exit)");
        settings.Add("1223", false, "Treatment Room Reset (Re-enter)");
        settings.Add("1224", true, "Treatment Room");
        settings.Add("1225", true, "Elevator");
        settings.Add("1226", true, "Toxic Pool Room");
        settings.Add("1227", true, "Factory Sewer Hall");
        settings.Add("1228", true, "Factory B1 Save");
        settings.Add("1229", true, "Water Puzzle");
        settings.Add("1230", true, "Factory B1 Save");
        settings.Add("1231", true, "Factory Sewer Hall");
        settings.Add("1232", true, "Toxic Pool Room");
        settings.Add("1233", true, "Elevator");
        settings.Add("1234", true, "Treatment Room");
        settings.Add("1235", true, "Disposal Access");
        settings.Add("1236", true, "Waste Disposal Nemesis Fight");
        settings.Add("1237", true, "Disposal Access");
        settings.Add("1238", true, "Treatment Room");
        settings.Add("1239", true, "Factory Main Hall");
        settings.Add("1240", true, "Communication Room");
        settings.Add("1241", true, "Hatch Escape Path");
        settings.Add("1242", true, "Junkyard");
        settings.Add("1243", true, "Final Nemesis Fight");
        settings.Add("1244", true, "Escape Elevator");
        settings.CurrentDefaultParent = null;

    //Message to Switch to Game Time
	if (timer.CurrentTimingMethod == TimingMethod.RealTime)
    {        
        var timingMessage = MessageBox.Show (
            "This game uses In-Game Time (IGT) as the timing method.\n"+
            "LiveSplit is currently set to show Real Time (RTA).\n"+
            "Would you like to set the timing method to Game Time? This is required for verification.",
            "LiveSplit | Resident Evil 3: Nemesis",
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
	vars.InventoryJill = new byte[10];
	vars.InventoryCarlos = new byte[8];
	vars.doorIterator = 0;
    vars.thousandDoor = 0;
    vars.JPN = 0;
    vars.CHN = 0;
    vars.REB = 0;
    vars.CTJ = 0;
    vars.endSplitFlag = 0;

    //Determine Version
    switch (modules.First().ModuleMemorySize)
	{
		case (11538432):
			version = "REbirth";
            vars.REB = 1;
			break;
        case (6713344):
			version = "Mediakite";
            vars.JPN = 1;
			break;
        case (7385088):
			version = "CHN/TWN";
            vars.CHN = 1;
			break;
	};

    //Door Splits Master List
    vars.masterDoors = new List<Tuple<int, int, int>>
    {
        Tuple.Create(  0, 13,  1), Tuple.Create(  1,  1,  0), Tuple.Create(  2,  0,  1), Tuple.Create(  3,  1,  2), Tuple.Create(  4,  2,  3), Tuple.Create(  5,  3,  4), Tuple.Create(  6,  4,  3), Tuple.Create(  7,  3,  4), Tuple.Create(  8,  4,  6), Tuple.Create(  9,  6,  5),
        Tuple.Create( 10,  5,  7), Tuple.Create( 11,  7,  5), Tuple.Create( 12,  5,  8), Tuple.Create( 13,  8,  9), Tuple.Create( 14,  9, 12), Tuple.Create( 15, 12,  9), Tuple.Create( 16,  9, 10), Tuple.Create( 17, 10, 16), Tuple.Create( 18, 16, 17), Tuple.Create( 19, 17, 18),
        Tuple.Create( 20, 18, 19), Tuple.Create( 21, 19, 20), Tuple.Create( 22, 20, 24), Tuple.Create( 23, 24, 25), Tuple.Create( 24, 25, 26), Tuple.Create( 25, 26, 25), Tuple.Create( 26, 25, 24), Tuple.Create( 27, 24, 20), Tuple.Create( 28, 20, 19), Tuple.Create( 29, 19, 18),
        Tuple.Create( 30, 18, 17), Tuple.Create( 31, 17, 37), Tuple.Create( 32, 37, 36), Tuple.Create( 33, 36, 26), Tuple.Create( 34, 26, 24), Tuple.Create( 35, 24,  0), Tuple.Create( 36,  0,  1), Tuple.Create( 37,  1, 27), Tuple.Create( 38, 27,  2), Tuple.Create( 39,  2,  3),
        Tuple.Create( 40,  3,  5), Tuple.Create( 41,  5,  6), Tuple.Create( 42,  6, 17), Tuple.Create( 43, 17,  6), Tuple.Create( 44, 17, 18), Tuple.Create( 45, 18,  6), Tuple.Create( 46,  6, 22), Tuple.Create( 47, 22,  6), Tuple.Create( 48,  6,  7), Tuple.Create( 49,  7, 16),
        Tuple.Create( 50, 16, 15), Tuple.Create( 51, 15, 16), Tuple.Create( 52, 16,  7), Tuple.Create( 53,  7,  8), Tuple.Create( 54,  8,  9), Tuple.Create( 55,  9, 10), Tuple.Create( 56, 10, 11), Tuple.Create( 57, 11, 12), Tuple.Create( 58, 12, 12), Tuple.Create( 59, 12, 11),
        Tuple.Create( 60, 11, 10), Tuple.Create( 61, 10,  9), Tuple.Create( 62,  9,  8), Tuple.Create( 63,  8, 14), Tuple.Create( 64, 14, 13), Tuple.Create( 65, 13, 23), Tuple.Create( 66, 23,  8), Tuple.Create( 67,  8,  7), Tuple.Create( 68,  7,  6), Tuple.Create( 69,  6,  5),
        Tuple.Create( 70,  5,  6), Tuple.Create( 71,  6,  7), Tuple.Create( 72,  7,  8), Tuple.Create( 73,  8,  7), Tuple.Create( 74,  7,  6), Tuple.Create( 75,  6,  5), Tuple.Create( 76,  5,  3), Tuple.Create( 77,  3,  4), Tuple.Create( 78,  4, 19), Tuple.Create( 79, 19, 20),
        Tuple.Create( 80, 20,  4), Tuple.Create( 81,  4,  3), Tuple.Create( 82,  3,  2), Tuple.Create( 83,  2, 27), Tuple.Create( 84, 27,  1), Tuple.Create( 85,  1,  0), Tuple.Create( 86,  0, 24), Tuple.Create( 87, 24, 26), Tuple.Create( 88, 26, 36), Tuple.Create( 89, 36, 35),
        Tuple.Create( 90, 35, 14), Tuple.Create( 91, 14, 11), Tuple.Create( 92, 11, 27), Tuple.Create( 93, 27, 28), Tuple.Create( 94, 28, 27), Tuple.Create( 95, 27, 11), Tuple.Create( 96, 11, 14), Tuple.Create( 97, 14, 35), Tuple.Create( 98, 35, 36), Tuple.Create( 99, 36, 26),
        Tuple.Create(100, 26, 24), Tuple.Create(101, 24,  0), Tuple.Create(102,  0,  1), Tuple.Create(103,  1, 27), Tuple.Create(104, 27,  2), Tuple.Create(105,  2,  3), Tuple.Create(106,  3,  5), Tuple.Create(107,  5,  6), Tuple.Create(108,  6, 22), Tuple.Create(109, 22,  6),
        Tuple.Create(110,  6,  7), Tuple.Create(111,  7,  8), Tuple.Create(112,  8, 14), Tuple.Create(113, 14, 13), Tuple.Create(114, 13, 23), Tuple.Create(115, 23,  8), Tuple.Create(116,  8,  9), Tuple.Create(117,  9, 10), Tuple.Create(118, 10, 11), Tuple.Create(119, 11, 12),
        Tuple.Create(120, 12, 12), Tuple.Create(121, 12, 21), Tuple.Create(122, 21, 21), Tuple.Create(123, 21,  5), Tuple.Create(124,  5,  6), Tuple.Create(125,  6,  7), Tuple.Create(126,  7,  4), Tuple.Create(127,  4,  2), Tuple.Create(128,  2,  1), Tuple.Create(129,  1,  0),
        Tuple.Create(130,  0,  1), Tuple.Create(131,  1,  2), Tuple.Create(132,  2,  4), Tuple.Create(133,  4, 10), Tuple.Create(134, 10, 11), Tuple.Create(135, 11, 12), Tuple.Create(136, 12, 11), Tuple.Create(137, 11, 10), Tuple.Create(138, 10,  4), Tuple.Create(139,  4,  7),
        Tuple.Create(140,  7,  8), Tuple.Create(141,  8,  9), Tuple.Create(142,  9,  8), Tuple.Create(143,  8,  7), Tuple.Create(144,  7,  4), Tuple.Create(145,  4, 10), Tuple.Create(146, 10, 11), Tuple.Create(147, 11, 12), Tuple.Create(148, 12, 11), Tuple.Create(149, 11, 13),
        Tuple.Create(150, 13, 16), Tuple.Create(151, 16, 23), Tuple.Create(152, 23, 23), Tuple.Create(153, 23, 22), Tuple.Create(154, 22, 19), Tuple.Create(155, 19, 20), Tuple.Create(156, 20, 21), Tuple.Create(157, 21, 23), Tuple.Create(158, 23,  2), Tuple.Create(159,  2,  3),
        Tuple.Create(160,  3,  4), Tuple.Create(161,  4,  5), Tuple.Create(162,  4,  9), Tuple.Create(163,  9, 10), Tuple.Create(164, 10, 11), Tuple.Create(165, 11, 10), Tuple.Create(166, 10,  9), Tuple.Create(167,  9,  5), Tuple.Create(168,  5,  8), Tuple.Create(169,  8,  5),
        Tuple.Create(170,  5,  6), Tuple.Create(171,  6,  5), Tuple.Create(172,  5,  4), Tuple.Create(173,  5,  9), Tuple.Create(174,  9, 10), Tuple.Create(175, 10, 11), Tuple.Create(176, 11, 10), Tuple.Create(177, 10,  9), Tuple.Create(178,  9,  4), Tuple.Create(179,  4,  3),
        Tuple.Create(180,  3,  2), Tuple.Create(181,  2, 23), Tuple.Create(182, 23, 21), Tuple.Create(183, 21, 20), Tuple.Create(184, 20, 19), Tuple.Create(185, 19, 22), Tuple.Create(186, 22, 23), Tuple.Create(187, 23, 23), Tuple.Create(188, 23, 16), Tuple.Create(189, 16,  0),
        Tuple.Create(190,  0, 15), Tuple.Create(191, 15, 14), Tuple.Create(192, 14,  7), Tuple.Create(193,  7,  8), Tuple.Create(194,  8,  9), Tuple.Create(195,  9,  0), Tuple.Create(196,  0,  1), Tuple.Create(197,  1,  0), Tuple.Create(198,  0, 12), Tuple.Create(199, 12, 14),
        Tuple.Create(200, 14, 15), Tuple.Create(201, 15, 14), Tuple.Create(202, 14, 12), Tuple.Create(203, 12, 13), Tuple.Create(204, 13, 16), Tuple.Create(205, 16, 17), Tuple.Create(206, 17, 18), Tuple.Create(207, 18, 20), Tuple.Create(208, 20, 18), Tuple.Create(209, 18, 21),
        Tuple.Create(210, 21, 16), Tuple.Create(211, 16, 13), Tuple.Create(212, 13, 12), Tuple.Create(213, 12, 14), Tuple.Create(214, 14, 15), Tuple.Create(215, 15, 16), Tuple.Create(216, 16,  0), Tuple.Create(217,  0,  1), Tuple.Create(218,  1,  2), Tuple.Create(219,  2,  1),
        Tuple.Create(220,  1,  0), Tuple.Create(221,  0,  3), Tuple.Create(222,  3,  0), Tuple.Create(223,  0,  3), Tuple.Create(224,  3,  3), Tuple.Create(225,  3,  7), Tuple.Create(226,  7,  4), Tuple.Create(227,  4,  5), Tuple.Create(228,  5,  6), Tuple.Create(229,  6,  5),
        Tuple.Create(230,  5,  4), Tuple.Create(231,  4,  7), Tuple.Create(232,  7,  3), Tuple.Create(233,  3,  3), Tuple.Create(234,  3,  8), Tuple.Create(235,  8,  9), Tuple.Create(236,  9,  8), Tuple.Create(237,  8,  3), Tuple.Create(238,  3,  0), Tuple.Create(239,  0, 10),
        Tuple.Create(240, 10, 11), Tuple.Create(241, 11, 12), Tuple.Create(242, 12, 13), Tuple.Create(243, 13, 15), Tuple.Create(244, 15, 14)
    };

    //any% Splits
    vars.anyDoors = new List<int>()
	{  0,   1,   2,   3,   4,   5,             8,   9,
      10,  11,  12,  13,            16,  17,  18,  19,
      20,  21,  22,  23,  24,  25,  26,  27,  28,  29,
      30,  31,  32,  33,  34,  35,  36,  37,  38,  39,
      40,  41,  42,       44,  45,            48,  49,
      50,  51,  52,  53,  54,  55,  56,  57,  58,  59,
      60,  61,  62,                      67,  68,  69,
      70,  71,  72,  73,  74,  75,  76,  77,  78,  79,
      80,  81,  82,  83,  84,  85,  86,  87,  88,  89,
      90,  91,  92,  93,  94,  95,  96,  97,  98,  99,
     100, 101, 102, 103, 104, 105, 106, 107, 108, 109,
     110, 111, 112, 113, 114, 115, 116, 117, 118, 119,
     120, 121, 122, 123,  
     130, 131, 132, 133, 134, 135, 136, 137, 138, 139,
     140, 141, 142, 143, 144, 145, 146, 147, 148, 149,
     150, 151, 152, 153, 154, 155, 156, 157, 158, 159,
     160, 161, 
                                                  179,
     180, 181, 182, 183, 184, 185, 186, 187, 188, 189,
     190, 191, 192, 193, 194, 195, 196, 197, 198, 199,
     200, 201, 202, 203, 204, 205, 206, 207, 208, 209,
     210, 211, 212, 213, 214, 215, 216, 217, 218, 219,
     220, 221,           224, 225, 226, 227, 228, 229,
     230, 231, 232, 233, 234, 235, 236, 237, 238, 239,
     240, 241, 242, 243, 244
    };

    //Nemesis% Splits
    vars.nemmyDoors = new List<int>()
	{  0,   1,   2,   3,   4,   5,             8,   9,
      10,  11,  12,  13,  14,  15,  16,  17,  18,  19,
      20,  21,  22,  23,  24,  25,  26,  27,  28,  29,
      30,  31,  32,  33,  34,  35,  36,  37,  38,  39,
      40,  41,  42,  43,            46,  47,  48,  49,
      50,  51,  52,  53,  54,  55,  56,  57,  58,  59,
      60,  61,  62,  63,  64,  65,  66,  67,  68,  69,
      70,  71,  72,  73,  74,  75,  76,  77,  78,  79,
      80,  81,  82,  83,  84,  85,  86,  87,  88,  89,
      90,  91,  92,  93,  94,  95,  96,  97,  98,  99,
     100, 101, 102, 103, 104, 105, 106, 107,
     110, 111,                     116, 117, 118, 119,
     120, 121, 122, 123,
     130, 131, 132, 133, 134, 135, 136, 137, 138, 139,
     140, 141, 142, 143, 144, 145, 146, 147, 148, 149,
     150, 151, 152, 153, 154, 155, 156, 157, 158, 159,
     160, 161, 
                                                  179,
     180, 181, 182, 183, 184, 185, 186, 187, 188, 189,
     190, 191, 192, 193, 194, 195, 196, 197, 198, 199,
     200, 201, 202, 203, 204, 205, 206, 207, 208, 209,
     210, 211, 212, 213, 214, 215, 216, 217, 218, 219,
     220, 221,           224, 225, 226, 227, 228, 229,
     230, 231, 232, 233, 234, 235, 236, 237, 238, 239,
     240, 241, 242, 243, 244
    };

    //any% Knife Only Splits
    vars.anyKnifeDoors = new List<int>()
	{  0,   1,   2,   3,   4,   5,             8,   9,
      10,  11,  12,  13,            16,  17,  18,  19,
      20,  21,  22,  23,  24,  25,  26,  27,  28,  29,
      30,  31,  32,  33,  34,  35,  36,  37,  38,  39,
      40,  41,  42,       44,  45,            48,  49,
      50,  51,  52,  53,  54,  55,  56,  57,  58,  59,
      60,  61,  62,                      67,  68,  69,
      70,  71,  72,  73,  74,  75,  76,  77,  78,  79,
      80,  81,  82,  83,  84,  85,  86,  87,  88,  89,
      90,  91,  92,  93,  94,  95,  96,  97,  98,  99,
     100, 101, 102, 103, 104, 105, 106, 107, 108, 109,
     110, 111, 112, 113, 114, 115, 116, 117, 118, 119,
     120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 
     130, 131, 132, 133, 134, 135, 136, 137, 138, 139,
     140, 141, 142, 143, 144, 145, 146, 147, 148, 149,
     150, 151, 152, 153, 154, 155, 156, 157, 158, 159,
     160,      162, 
                                                  179,
     180, 181, 182, 183, 184, 185, 186, 187, 188, 189,
     190, 191, 192, 193, 194, 195, 196, 197, 198, 199,
     200, 201, 202, 203, 204, 205, 206, 207, 208, 209,
     210, 211, 212, 213, 214, 215, 216, 217, 218, 219,
     220, 221,           224, 225, 226, 227, 228, 229,
     230, 231, 232, 233, 234, 235, 236, 237, 238, 239,
     240, 241, 242, 243, 244
    };

    //Over Hiear any% Splits
    vars.anyOverHiearDoors = new List<int>()
	{  0,   1,   2,   3,   4,   5,             8,   9,
      10,  11,  12,  13,            16,  17,  18,  19,
      20,  21,  22,  23,  24,  25,  26,  27,  28,  29,
      30,  31,  32,  33,  34,  35,  36,  37,  38,  39,
      40,  41,  42,       44,  45,            48,  49,
      50,  51,  52,  53,  54,  55,  56,  57,  58,  59,
      60,  61,  62,                      67,  68,  69,
      70,  71,  72,  73,  74,  75,  76,  77,  78,  79,
      80,  81,  82,  83,  84,  85,  86,  87,  88,  89,
      90,  91,  92,  93,  94,  95,  96,  97,  98,  99,
     100, 101, 102, 103, 104, 105, 106, 107, 108, 109,
     110, 111, 112, 113, 114, 115, 116, 117, 118, 119,
     120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 
     130, 131, 132, 133, 134, 135, 136, 137, 138, 139,
     140, 141, 142, 143, 144, 145, 146, 147, 148, 149,
     150, 151, 152, 153, 154, 155, 156, 157, 158, 159,
     160, 161, 
                         174, 175, 176, 177, 178, 179,
     180, 181, 182, 183, 184, 185, 186, 187, 188, 189,
     190, 191, 192, 193, 194, 195, 196, 197, 198, 199,
     200, 201, 202, 203, 204, 205, 206, 207, 208, 209,
     210, 211, 212, 213, 214, 215, 216, 217, 218, 219,
     220, 221, 222, 223, 224, 225, 226, 227, 228, 229,
     230, 231, 232, 233, 234, 235, 236, 237, 238, 239,
     240, 241, 242, 243, 244
    };

    //Room Reloads
    vars.roomReloads = new List<int>()
    {6, 7, 222, 223};

    //Early/Late Powder RNG Segment
    vars.powderRNG = new List<int>()
    {124, 125, 126, 127, 128, 129};

    //Hospital RNG Segment - 4F First
    vars.hospital4F = new List<int>()
    {168, 169, 170, 171, 176, 174, 175, 176, 177, 178};

    //Hospital RNG Segment - B3 First
    vars.hospitalB3 = new List<int>()
    {162, 163, 164, 165, 166, 167, 168, 169, 170, 171, 172};

    //Early Crank Add Doors
    vars.earlyCrankAdd = new List<int>()
    {46, 47, 63, 64, 65, 66};

    //Early Crank Remove Doors
    vars.earlyCrankRemove = new List<int>()
    {108, 109, 112, 113, 114, 115};
}

update
{    
    //Reset variables when the timer is reset.
	if(timer.CurrentPhase == TimerPhase.NotRunning)
	{
		vars.completedSplits.Clear();
		vars.doorIterator = 0;
        vars.thousandDoor = 0;
        vars.CTJ = 0;
        vars.endSplitFlag = 0;
	}

    //Iterate through the inventory slots to return their values
    for(int i = 0; i < 8; i++)
	{
        vars.InventoryJill[i] = new DeepPointer(0x667584 - (vars.JPN * 0x6280) + (vars.CHN * 0x9DB20) + (i * 0x4)).Deref<byte>(game);
        vars.InventoryCarlos[i] = new DeepPointer(0x6676C4 - (vars.JPN * 0x6280) + (vars.CHN * 0x9DB20) + (i * 0x4)).Deref<byte>(game);
    }
    vars.InventoryJill[8] = new DeepPointer(0x667584 - (vars.JPN * 0x6280) + (vars.CHN * 0x9DB20) + (8 * 0x4)).Deref<byte>(game);
    vars.InventoryJill[9] = new DeepPointer(0x667584 - (vars.JPN * 0x6280) + (vars.CHN * 0x9DB20) + (9 * 0x4)).Deref<byte>(game);

	//Uncomment debug information in the event of an update.
	//print(modules.First().ModuleMemorySize.ToString());
}

start
{
   if(current.roomID == 13 && current.camID == 11) 
   {
	  return (current.gameState & 0x8000000) == 0x8000000;
   }
   return false;
}

split
{
    //Ending Split -- Always Active
    if(current.roomID == 14 && current.oldRoom == 15 && current.camID == 2 && vars.endSplitFlag == 0 && ((current.gameState & 0x4000) == 0x4000))
    {
        Thread.Sleep(500);
        vars.endSplitFlag++;
        return true;
    }
    
    //Create variables to check for the variables in each item slot
    byte[] currentInventoryJill = (vars.InventoryJill as byte[]);
    byte[] currentInventoryCarlos = (vars.InventoryCarlos as byte[]);

	//Item Splits
	if(settings["item"] || settings["extra"])
    {
		if(current.character != 8) //Jill occupies character 0 (8 inventory slots) and 1 (10 inventory slots). Carlos is character 8.
        {
            for(int i = 0; i < 10; i++) //Iterate through Jill's inventory
            {
		    	//Check if any of Jill's inventory slots include the variables in our items lists, check if the split was already completed and if the setting for the given item is activated
		    	if((vars.KeyItems.Contains(currentInventoryJill[i]) || vars.ExtraItems.Contains(currentInventoryJill[i])) && !vars.completedSplits.Contains(currentInventoryJill[i]) && settings[currentInventoryJill[i].ToString()])
                {
                	vars.completedSplits.Add(currentInventoryJill[i]);
                	return true;
            	}

    	    }
        } else {
            for(int i = 0; i < 8; i++) //Iterate through Carlos' inventory
            {
		    	//Check if any of Carlos' inventory slots include the variables in our items lists, check if the split was already completed and if the setting for the given item is activated
		    	if((vars.KeyItems.Contains(currentInventoryCarlos[i]) || vars.ExtraItems.Contains(currentInventoryCarlos[i])) && !vars.completedSplits.Contains(currentInventoryCarlos[i]) && settings[currentInventoryCarlos[i].ToString()])
                {
                	vars.completedSplits.Add(currentInventoryCarlos[i]);
                	return true;
            	}

    	    }
        }
	}

    //Door Splits
	if(settings["doors"] && !settings["basic"] && !settings["hiear"] && !settings["custom"])
	{
        //Skipping unwanted splits
        if(settings["any"] && vars.doorIterator < 255 && (
            ((settings["hosB3"] && vars.doorIterator == 161) || (settings["early"] && vars.earlyCrankRemove.Contains(vars.doorIterator))) ||
            (!vars.anyDoors.Contains(vars.doorIterator) 
            && !(settings["reload"] && vars.roomReloads.Contains(vars.doorIterator))
            && !(settings["rng"] && !settings["hosB3"] && (vars.powderRNG.Contains(vars.doorIterator) || vars.hospital4F.Contains(vars.doorIterator)))
            && !(settings["rng"] && settings["hosB3"] && (vars.powderRNG.Contains(vars.doorIterator) || vars.hospitalB3.Contains(vars.doorIterator)))
            && !(settings["early"] && vars.earlyCrankAdd.Contains(vars.doorIterator))))) //any%
        {
            vars.doorIterator++;
        } else if(settings["nemmy"] && vars.doorIterator < 255 && (
            (settings["hosB3"] && vars.doorIterator == 161) ||
            (!vars.nemmyDoors.Contains(vars.doorIterator) 
            && !(settings["reload"] && vars.roomReloads.Contains(vars.doorIterator))
            && !(settings["rng"] && !settings["hosB3"] && (vars.powderRNG.Contains(vars.doorIterator) || vars.hospital4F.Contains(vars.doorIterator)))
            && !(settings["rng"] && settings["hosB3"] && (vars.powderRNG.Contains(vars.doorIterator) || vars.hospitalB3.Contains(vars.doorIterator)))))) //nemmy%
        {
            vars.doorIterator++;
        } else if(settings["knife"] && vars.doorIterator < 255 && (
            (settings["early"] && vars.earlyCrankRemove.Contains(vars.doorIterator)) ||
            (!vars.anyKnifeDoors.Contains(vars.doorIterator) 
            && !(settings["reload"] && vars.roomReloads.Contains(vars.doorIterator))
            && !(settings["rng"] && (vars.powderRNG.Contains(vars.doorIterator) || vars.hospitalB3.Contains(vars.doorIterator)))
            && !(settings["early"] && vars.earlyCrankAdd.Contains(vars.doorIterator))))) //knife only any%
        {
            vars.doorIterator++;
        }
        
        //any%
        if((old.roomID != current.roomID || old.oldRoom != current.oldRoom) && settings["any"])
        {
            if(vars.anyDoors.Contains(vars.doorIterator) && vars.masterDoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID)))) {
                vars.doorIterator++;
                return true;
            } else if(settings["reload"] && vars.roomReloads.Contains(vars.doorIterator) && vars.masterDoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID)))) {
                vars.doorIterator++;
                return true;
            } else if(settings["rng"] && !settings["hosB3"] && (vars.powderRNG.Contains(vars.doorIterator) || vars.hospital4F.Contains(vars.doorIterator)) && vars.masterDoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID)))) {
                vars.doorIterator++;
                return true;
            } else if(settings["rng"] && settings["hosB3"] && (vars.powderRNG.Contains(vars.doorIterator) || vars.hospitalB3.Contains(vars.doorIterator)) && vars.masterDoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID)))) {
                vars.doorIterator++;
                return true;
            } else if(settings["early"] && vars.earlyCrankAdd.Contains(vars.doorIterator) && vars.masterDoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID)))) {
                vars.doorIterator++;
                return true;
            }
        } 

        //Nemesis%
        if((old.roomID != current.roomID || old.oldRoom != current.oldRoom) && settings["nemmy"])
        {
            if(vars.nemmyDoors.Contains(vars.doorIterator) && vars.masterDoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID)))) {
                vars.doorIterator++;
                return true;
            } else if(settings["reload"] && vars.roomReloads.Contains(vars.doorIterator) && vars.masterDoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID)))) {
                vars.doorIterator++;
                return true;
            } else if(settings["rng"] && !settings["hosB3"] && (vars.powderRNG.Contains(vars.doorIterator) || vars.hospital4F.Contains(vars.doorIterator)) && vars.masterDoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID)))) {
                vars.doorIterator++;
                return true;
            } else if(settings["rng"] && settings["hosB3"] && (vars.powderRNG.Contains(vars.doorIterator) || vars.hospitalB3.Contains(vars.doorIterator)) && vars.masterDoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID)))) {
                vars.doorIterator++;
                return true;
            }
        }

        //any% Knife Only
        if((old.roomID != current.roomID || old.oldRoom != current.oldRoom) && settings["knife"])
        {
            if(vars.anyKnifeDoors.Contains(vars.doorIterator) && vars.masterDoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID)))) {
                vars.doorIterator++;
                return true;
            } else if(settings["reload"] && vars.roomReloads.Contains(vars.doorIterator) && vars.masterDoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID)))) {
                vars.doorIterator++;
                return true;
            } else if(settings["rng"] && (vars.powderRNG.Contains(vars.doorIterator) || vars.hospitalB3.Contains(vars.doorIterator)) && vars.masterDoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID)))) {
                vars.doorIterator++;
                return true;
            } else if(settings["early"] && vars.earlyCrankAdd.Contains(vars.doorIterator) && vars.masterDoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID)))) {
                vars.doorIterator++;
                return true;
            }
        }
    }

    //any% Over Hiear Splits
    if(settings["hiear"])
    {
        if(vars.anyOverHiearDoors.Contains(vars.doorIterator) && vars.masterDoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID)))) {
            vars.doorIterator++;
            return true;
        } else if(!vars.anyOverHiearDoors.Contains(vars.doorIterator) && vars.doorIterator < 255)
        {
            vars.doorIterator++;
        }
    } 

    //Basic Door Splits
    if((old.roomID != current.roomID || old.oldRoom != current.oldRoom) && settings["basic"] && !settings["custom"])
	{
		if(old.roomID != current.roomID)
		{
			return true;
		} else if(old.oldRoom != current.oldRoom)
        {
            return true;
        }
	}

    //Custom Door Splits
    if(settings["custom"])
	{
		vars.thousandDoor = vars.doorIterator + 1000;
        if(settings[vars.thousandDoor.ToString()] && vars.masterDoors.Contains(Tuple.Create(vars.doorIterator,Convert.ToInt32(current.oldRoom),Convert.ToInt32(current.roomID))))
		{
			vars.doorIterator++;
            return true;
		} else if(!settings["" + vars.thousandDoor.ToString()] && vars.doorIterator < 255)
        {
            vars.doorIterator++;
        }
	}
}

gameTime
{
	if(vars.CHN == 1 || vars.JPN == 1)
    {
        //If current stays 0, that indicates CHN version, otherwise it's TWN or JPN version
        if(current.current == 0)
        {
            vars.CTJ = new DeepPointer(0x13705C, 0x5ac).Deref<uint>(game);
        } else {
            vars.CTJ = current.current;
        }
        
        if((current.gameState & 0x4000) == 0x4000) 
	    {
	    	return TimeSpan.FromSeconds((current.total) / 60.0);
	    } else {
	    	return TimeSpan.FromSeconds((vars.CTJ - current.save + current.total) / 60.0);
	    }
    } else if(vars.REB == 1)
    {
        if((current.gameState & 0x4000) == 0x4000) 
        {
	    	return TimeSpan.FromSeconds((current.save) / 60.0);
	    } else {
	    	return TimeSpan.FromSeconds((current.save - current.total + current.current) / 60.0);
	    }
    }
}

isLoading
{
    return true;
}

reset
{
    return current.hp == 65516 || current.oldRoom == 0xFF || current.oldCam == 0xFF;
}