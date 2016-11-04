# HitCounterManager
Manages a hit counter  
This readme is compatible with version 1.4.0.0 or newer

## Features
It was designed for Dark Souls III 0 hit or no death runs to count every hit during the playthrough
* Creates a HTML file of the hit counters (can be used by CLR browser plugin in Open BroadCaster Software in order to make it visible on stream)
  * The design and layout can be costumized completely
* Instead of using GUI buttons, keyboard shortcuts can be configured:
  * Increase hit count of the current split
  * Reset the run (set all hit counts to 0 and select first split)
  * Select next split
* Keyboard shortcuts are working in fullscreen applications (like Dark Souls III)
* Via GUI every value can be changed at any time
* Multiple profiles can be created (e.g. "Any%", "AllBosses", "DLC only"..)
* Switching to other profiles will keep hit counts of currently selected profile
* Button to save current run as PB (personal best)
* Settings file "HitCounterManagerSave.xml" holds all your configuration data and is designed to work in newer versions, too. Save and restore this file at any time.
* Comes with some pre-defined profiles:
  * Dark Souls III All Bosses Ashes of Ariandel
  * Dark Souls III All Bosses Vanilla
  * Dark Souls II All Bosses Vanilla
  * Dark Souls II Any% GER
  * Dark Souls I PtdE All Bosses
* Comes with 3 graphical representations:
  * Numeric (shows all counters of all splits for current run, PB and their differences)
  * Yes/No (shows check marks or crosses if one got hit at current run or PB)
  * NoDeath (shows check marks for all splits already done, ignoring the hit counts)  

> Note: Those features will be available the first time with version 1.2.0.0

Want to have a look at the front-end? **Find some [images here] (../../wiki)**.

## Get the software
Checkout the [Releases] (https://github.com/topeterk/HitCounterManager/releases) section and choose the version of you choice.

## Installation

### Systemrequirements
.Net Framework 4.5

### Using the Installer
The installer places all files at your installation folder (default: My Documents) or any other location you like.  
> Note: There is only one requirement that the application must able to read/write data in the installation folder.

### Configuration file
The file [HitCounterManagerSave.xml] (HitCounterManagerSave.xml) is used for all settings you can set by this application.  
You can save and restore this file at any time or copy it to a newer/portable version of this application.
> Note: It holds all your settings and profiles in one place. No other files are involved.

### Configuring CLR browser plugin for OBS
The pre-delivered [HitCounterNumeric.html] (HitCounterNumeric.html) or [HitCounterYesNo.html] (HitCounterYesNo.html) or [HitCounterNoDeath.html] (HitCounterNoDeath.html) will update themself every second, so there should be no need to render more frames than one every second. However, to speed up loading time and to give the browser the opportunity doing some background tasks, it should be a slightly higher number.  
> Recommended: FPS between 3 and 10

The opacity can be set to 100% because the background will be rendered transparent, so no color-keying is involved. Since CRL browser plugin come with some pre-defined CSS, this may cause trouble with the rendering of the HTML file (flickering or misplacement). If you encounter issues here, try to simply remove the CLR browser plugin's CSS overrides.  
> Recommended: Opacity 100% and no CSS overrides

When no data is displayed, there could be a problem with cross-domain security settings. This is because the HTMLs are rendered via file:// protocol instead of http://. Make sure to allow file access from files.
> OBS -> Settings -> Browser -> Instance -> ...  
> * FileAccessFromFilesUrls (Set to **Enabled**)
> * UniversialAccessFromFilesUrls (Set to **Enabled**, _but should also work when disabled_)
> * WebSecurity (Set to **Disabled**, _but should also work when enabled_)

### Using usual standalone Chrome browser
When no data is displayed, there could be a problem with cross-domain security settings. This is because the HTMLs are rendered via file:// protocol instead of http://. Make sure to allow file access from files.
> Start Chrome with **--allow-file-access-from-files**  
> Example: _"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" --allow-file-access-from-files_

### Changing layout and design
You can modify or create new custom graphical representation. Simply create a new HTML file based on [HitCounterNumeric.html] (HitCounterNumeric.html) or [HitCounterYesNo.html] (HitCounterYesNo.html) or [HitCounterNoDeath.html] (HitCounterNoDeath.html).  

Alternatively you can modify the [HitCounter.template] (HitCounter.template) which comes with the installation or you can create an own template file to get the look you want.  
> The application is using JavaScript syntax when writing data into the output file. For that the template has to have a line with the text **HITCOUNTER_LIST_START** which is the starting mark. The mark with all further lines will be replaced with the JavaScript equivalent of the application's current data. This happens until the **HITCOUNTER_LIST_END** text mark is reached.
This way you are 100% free in the design of your hit counter.

## Something is missing, annoying, can be improvent or you just found a bug?
Message me via github, e-mail or simple open an issue and I will try to help you out.

## Special thanks
I would never have created this tool without the inspiration by watching the awesome 0 hit and no death runners...  
Thanks to:
* [The_Happy_Hob] (https://www.twitch.tv/the_happy_hob)
* [FaraazKhan] (https://www.twitch.tv/faraazkhan)
* [SquillaKilla] (https://www.twitch.tv/squillakilla)
* [SayviTV] (https://www.twitch.tv/sayvitv)
* [Kazoodle] (https://www.twitch.tv/kazoodle)
