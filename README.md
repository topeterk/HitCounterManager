# HitCounterManager

Manages a hit counter running in the background, so you can focus on your stream.  
No need any more to keep any windows open for a window capture.  
  
To see some **screenshots** of this application go to the **[Wiki pages] (../../wiki)**.  
To see **how to** use the application watch the **[Tutorial on YouTube] (https://www.youtube.com/watch?v=aa4nRtGxnvE)**.

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
* Comes with **3 high quality** graphical representations:
  * Numeric (shows all counters of all splits for current run, PB and their differences)
  * Yes/No (shows check marks or crosses if one got hit at current run or PB)
  * NoDeath (shows check marks for all splits being alive, ignoring the hit counts)
* Comes with **3 low quality** graphical representations:
  * Numeric **Black** (shows all counters of all splits for current run, PB and their differences)
  * Yes/No **Black** (shows check marks or crosses if one got hit at current run or PB)
  * NoDeath **Black** (shows check marks for all splits being alive, ignoring the hit counts)  

## Get the software
All available releases can be found at the [Releases page] (https://github.com/topeterk/HitCounterManager/releases) on GitHub.

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
The pre-delivered HTML representations will update themself every 1.5 seconds, so there is no need for high frame rates.  
> Recommended: FPS between 5 and 10

Those pre-delivered HTML representations are available:
* 
* [HitCounterNumericBlack.html] (HitCounterNumericBlack.html)
* [HitCounterYesNo.html] (HitCounterYesNo.html)
* [HitCounterYesNoBlack.html] (HitCounterYesNoBlack.html)
* [HitCounterNoDeath.html] (HitCounterNoDeath.html)
* [HitCounterNoDeathBlack.html] (HitCounterNoDeathBlack.html)

> Previews can be found at the **[Wiki pages] (../../wiki)**.

The opacity can be set to 100% because the background will be rendered transparent, so no color-keying is involved.
Since CRL browser plugin come with some pre-defined CSS, this may cause trouble with the rendering of the HTML file (flickering or misplacement).
If you encounter issues here, try to simply remove the CLR browser plugin's CSS overrides.  
> Recommended: Opacity 100% and no CSS overrides

When no data is displayed, there could be a problem with cross-domain security settings. This is because the HTMLs are rendered via file:// protocol instead of http://. Make sure to allow file access from files.
> OBS -> Settings -> Browser -> Instance -> ...  
> * FileAccessFromFilesUrls (Set to **Enabled**)
> * UniversialAccessFromFilesUrls (Set to **Enabled**, _but should also work when disabled_)
> * WebSecurity (Set to **Disabled**, _but should also work when enabled_)

### Using standalone Chrome browser
When no data is displayed, there could be a problem with cross-domain security settings. This is because the HTMLs are rendered via file:// protocol instead of http://. Make sure to allow file access from files.
> Start Chrome with **--allow-file-access-from-files**  
> Example: _"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" --allow-file-access-from-files_

### Changing layout and design
You can modify or create new custom graphical representation. Simply create a new HTML file based on [HitCounter.html] (HitCounter.html) and any of the pre-delivered HTML representations.  

Alternatively you can modify the [HitCounter.template] (HitCounter.template) which comes with the installation or you can create an own template file to get the look you want.  
> The application is using JavaScript syntax when writing data into the output file. For that the template has to have a line with the text **HITCOUNTER_LIST_START** which is the starting mark.
The mark with all further lines will be replaced with the JavaScript equivalent of the application's current data. This happens until the **HITCOUNTER_LIST_END** text mark is reached.
This way you are 100% free in the design of your hit counter.

## Anything is missing, something is annoying/can be improved or you just found a bug?
Message me via GitHub / e-mail or simply open an issue and I will try to help you out.

## Special thanks
I would never have created this tool without the inspiration by watching the awesome 0 hit and no death runners...  
Thanks to:
* [The_Happy_Hob] (https://www.twitch.tv/the_happy_hob)
* [FaraazKhan] (https://www.twitch.tv/faraazkhan)
* [SquillaKilla] (https://www.twitch.tv/squillakilla)
* [SayviTV] (https://www.twitch.tv/sayvitv)
* [Kazoodle] (https://www.twitch.tv/kazoodle)  
  
  
  
> Praise the sun!  :sunny: . . . :fire: . . .  :running: :dash: 