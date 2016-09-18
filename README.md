# HitCounterManager
Manages a hit counter  

## Features
It was designed for Dark Souls III 0 hit runs, to count every hit during the playthrough
* Creates a HTML file of the hit counters (can be used by CLR browser plugin in Open BroadCaster Software in order to make it visible on stream)
* Instead of using GUI buttons, keyboard shortcuts can be configured:
  * Increase hit count of the current split
  * Reset the run (set all hit counts to 0 and select first split)
  * Select next split
* Keyboard shortcuts are working in fullscreen applications (like Dark Souls III)
* Via GUI every value can be changed at any time
* Multiple profiles can be created (e.g. "Any%", "AllBosses", "DLC only"..)
* Switching to other profiles will keep hit counts of currently selected profile
* Button to save current run as PB (personal best)
* Comes with sample profile: Dark Souls III 0 Hit All Bosses Vanilla
* Comes with 2 graphical representations:
  * Numeric (shows all counters of all splits for current run, PB and their differences)
  * Yes/No (shows check marks or crosses if one got hit at current run or PB)
> Note: Those features will be available the first time with version 1.1.0.0

## Get the software
Checkout the [Releases] (https://github.com/topeterk/HitCounterManager/releases) section and choose the version of you choice.

## Systemrequirements
.Net Framework 4.5

## Installation

### Using the Installer
The installer places the executable and an inital configuration file at your installation folder (default: Program Files).  
It additionally places templates and HTML files at %localappdata%\HitCounterManager.  
After first execution a further configuration file will be stored within %localappdata%\HitCounterManager.

### Using the portable version
After first execution a configuration file will be stored within %localappdata%\HitCounterManager.

### Configuring CLR browser plugin for OBS
The pre-delivered [HitCounterNumeric.html] (HitCounterNumeric.html) or [HitCounterYesNo.html] (HitCounterYesNo.html) will update themself once a second, so there should be no need to render more frames than one every second. However, to speed up loading time and to give the browser the opportunity doing some background tasks, it should be a slightly higher number.  
> By now I created a 400 to 800 pixel wide window and run it with 3 FPS without issues so far.  
> Recommended: FPS between 3 and 10

The opacity can be set to 100% because the background will be rendered transparent, so no color-keying is involved. Since CRL browser plugin come with some pre-defined CSS, this may cause trouble with the rendering of the HTML file (flickering or misplacement). If you encounter issues here, try to simply remove the CLR browser plugin's CSS overrides.  
> Recommended: Opacity 100% and no CSS overrides

### Changing layout and design
You can modify or create new custom graphical representation. Simply create a new HTML file based on [HitCounterNumeric.html] (HitCounterNumeric.html) or [HitCounterYesNo.html] (HitCounterYesNo.html).  

Alternatively you can modify the [HitCounter.template] (HitCounter.template) which comes with the installation or you can create an own template file to get the look you want.  
> The application is using JavaScript to write the data into the HTML table. For that the template have to have a line with the text **HITCOUNTER_LIST_START** which is the starting mark. The mark with all further lines will be replaced with the JavaScript equivalent of the application's current data. This happens until the **HITCOUNTER_LIST_END** text mark is reached.
This way you are 100% free in the design of your hit counter.

## Something is missing, annoying, can be improvent or you just found a bug?
Message me via github, e-mail or simple open an issue and I will try to help you out.

## Special thanks
I would never have created this tool without the inspiration by watching the awesome 0 hit runners...
Thanks to:
* [The_Happy_Hob] (https://www.twitch.tv/the_happy_hob)
* [FaraazKhan] (https://www.twitch.tv/faraazkhan)
* [SquillaKilla] (https://www.twitch.tv/squillakilla)
* [SayviTV] (https://www.twitch.tv/sayvitv)
