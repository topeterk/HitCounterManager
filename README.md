# HitCounterManager - A Dark Souls inspired Hit Counter

Free Hit Counter that is running in the background, so you can focus on your stream.  
No need to keep any windows open for a window capture any more.  
Initially designed for Dark Souls and similar games but supports any game.

To see some **screenshots** of this application go to the **[Wiki pages](../../wiki)**.  
To see **how to** use the application watch the **[Tutorial on YouTube](https://www.youtube.com/watch?v=iXGExlS4xeM)**.

<p align="center"><a href="Images/Preview.png"><img src="Images/Preview.png" alt="Preview" height="250px"/></a></p>

<img src="Images/installer-banner.png" alt="banner" align="right">

[![Releases](https://img.shields.io/github/release/topeterk/HitCounterManager.svg?label=Latest%20release:&longCache=true&style=for-the-badge&colorB=0088FF)](../../releases/latest)  
[![GitHub Releases](https://img.shields.io/github/downloads/topeterk/HitCounterManager/total.svg?label=Downloads:&longCache=true&style=for-the-badge&colorB=0088FF)](../../releases)  
[![GitHub](https://img.shields.io/github/license/topeterk/HitCounterManager.svg?label=License:&longCache=true&style=for-the-badge&colorB=0088FF)](LICENSE)

## Key-Features

* Offline application
* No window capture needed for stream integration
* Hot key support for seamless use **ingame** (Windows only)
* Easy to use profile management

## Features
It was designed for Dark Souls III "0 hit" or "no death" runs to count every hit/death during the playthrough
* Creates a HTML file of the hit counter that can be used by Open BroadCaster Software in order to make it visible on stream
  * The design and layout can be costumized completely
* Instead of using GUI buttons, keyboard shortcuts can be configured:
  * Increase/Decrease hit count of the current split (boss hits and hits on the way separately)
  * Go to next/previous split
  * Reset the run (sets all hit counts to 0 and selects first split)
  * Save the run as your PB (personal best)
* Keyboard shortcuts are working in fullscreen applications (like Dark Souls III)
* Via GUI every value can be changed at any time
* Multiple profiles can be created (e.g. "Any%", "AllBosses", "DLC only"..)
* Switching to other profiles will remember the hit counts of the currently selected profile
* Button to save current run as PB (personal best)
* Counts the attempts of each run (implicitly on every run's reset or manually)
* Shows furthest progress since last application start (can also be set manually)
* Settings file "HitCounterManagerSave.xml" holds all your configuration data and is designed to work in newer versions, too. Save and restore this file at any time.
* Comes with some pre-defined profiles for:
  * Bloodborne + The old hunters
  * Dark Souls 1 Prepare To Die Edition
  * Dark Souls 2
  * Dark Souls 3 + Ashes of Ariandel + The Ringed City
  * Demon's Souls
  * Salt and Sanctuary
  * Sekiro
  * The Surge + A walk in the park
* Comes with different designs..
  * **.. for counting hits:**
  
    * **Numeric** _(Traditional no hit runs)_
      * Shows the amount of hits for the current run, PB and their differences
    * **NumericWay** _(Traditional no hit runs)_
      * As **Numeric** except that hits are shown splitted into boss hits and way hits
    * **YesNo** _(Traditional no hit runs)_
      * Shows check marks/crosses if one got hit at the current run and PB
      * Every split has a check mark which has an amount of zero hits
    * **PBSplits**
      * Shows the amount of hits for the current run, PB and their differences
      * Every split is marked "better" which has a less or equal amount of hits compared to the PB split
  * **.. for counting deaths:**
  
    * **Deaths** _(Very basic death counter)_
      * Very simple counter that only shows sum of all hits (deaths)
    * **NoDeath** _(Traditional no death runs)_
      * Shows check marks/crosses until the players death for the current run and PB
      * All splits have check marks until the current split appears (__Now__ only) or a split appears that has already counted hits.
  * **.. for measuring progress:**
  
    * **Bosses** _(Simple boss or split checklist)_
      * Shows check marks/crosses for every boss (split) that is still alive or already defeated at the current run
      * Just add a hit to the boss/split to "mark the boss done"
* The appearance of all designs can be modified via GUI
  * Set the amount of shown splits that are already finished
  * Set the amount of shown splits that are upcoming
  * Show or hide the attempts counter
  * Show or hide the headline
  * Enable or disable the high contrast mode to support better readability on low bitrate streams
  * Switch to a custom CSS and font

## Get the software
All available releases can be found at the [Releases page](../../releases) on GitHub.

## Installation

### Systemrequirements
* OS: Windows Vista, Windows Server 2003 or newer (32/64 bit)
  * Portable version (ZIP)
    * [.Net Framework 2.0 or newer](https://www.microsoft.com/net)
    > Note: Version 1.13 and older requires [.Net Framework 4.5 or newer](https://www.microsoft.com/net)
  * Installer version (Setup)
    * [.Net Framework 4.5 or newer](https://www.microsoft.com/net)
* OS: Any (32/64 bit)
  * Portable version (ZIP)
    * [Mono](https://www.mono-project.com/) (_tested with 5.14.0_)  
      Run the application in the application's directory with **mono HitCounterManager.exe**  
      **Note**: The Non-Windows / Mono version does not support global hot keys

### Configuration file
The file [HitCounterManagerSave.xml](HitCounterManagerSave.xml) is used for all settings you can set by this application.  
You can save and restore this file at any time or copy it to a newer/portable version of this application.
> Note: It holds all your settings and profiles in one place. No other files are involved.

### Designs / Fonts
The pre-delivered HTML designs will update with an interval of 1.5 seconds.
> Previews of the pre-delivered HTML desings can be found at the **[Wiki pages](../../wiki)**.

A specific font can be selected at the appearance dialog in two ways:
* The font is already installed locally on the system
  * _(Not required but recommended)_ Clear the field for the font URL as no external font must be loaded
  * Enter your font family name of choice at the font name field and hit Apply
  > Example: _sans-serif_ or _courier_
* The font is **not** installed locally and must be loaded from an external _@font-face_ ressource
  * Enter the URL to the ressource into the font URL field
  * Enter the matching font family name at the font name field and hit Apply
  > Example: URL _https://fonts.googleapis.com/css?family=Fontdiner+Swanky_ and name _Fontdiner Swanky_

To find suitable fonts I can recommend Google's font collection: [https://fonts.google.com](https://fonts.google.com).  
Search for one of your liking and see the embedded font and/or CSS instruction as you can simply copy URL and name from there into the application. For the previous example the instructions looked like this _(the part to copy in bold)_:
> URL from Embed Font instructions:  
> _&lt;link href="**https://fonts.googleapis.com/css?family=Fontdiner+Swanky**" rel="stylesheet"&gt;_  
> Name from CSS instructions:  
> _font-family: '**Fontdiner Swanky**', cursive;_

## Setting up Broadcasting software

### Streamlabs OBS (SLOBS)
* Add a **Browser Source** to your scene
* Check the **Local file** checkbox
* Insert the HTML's filepath of a design file into the **Local File** field.
> Example: _C:\MyHitCounter\Designs\HitCounterNumeric.html_  
> Note: It was tested with some beta versions up to 0.11.1, so the instruction _may_ change in newer versions.

### OBS Studio
* Add a **Browser Source** to your scene
* Insert the HTML's filepath of a design file into the **URL** field including this prefix that allows access to local files: **http://absolute/**
> Example: _http://absolute/C:/MyHitCounter/Designs/HitCounterNumeric.html_

### Open Broadcaster Software (OBS, _not_ OBS Studio!)
* Add a **CLR Browser** to your scene
* The opacity can be set to 100% because the background will be rendered transparent, so no color-keying is required.
* Insert the HTML's filepath of a design file into the **URL** field.
> Example: _C:\MyHitCounter\Designs\HitCounterNumeric.html_
* If you experience troubles with the rendering of the HTML file (flickering or misplacement), try removing the CSS overrides of the CLR Brower instance.
* When no data is displayed, there could be a problem with the cross-domain security settings. This is because the HTMLs are rendered via file:// protocol instead of http://, so maybe you need to change some deeper settings of the CLR browser under _OBS -> Settings -> Browser -> Instance -> ..._
    * FileAccessFromFilesUrls (Set to **Enabled**)
    * UniversialAccessFromFilesUrls (Set to **Enabled**, _but should also work when disabled_)
    * WebSecurity (Set to **Disabled**, _but should also work when enabled_)

## Moders and Developers

### Using standalone Chrome browser instead of broadcasting software
* When no data is displayed, there could be a problem with cross-domain security settings that can be avoided by allowing acces to local files in general:
  * Start Chrome with **--allow-file-access-from-files**
  > Example: _"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" --allow-file-access-from-files_

### Changing layout and design
You can modify or create new custom designs. Simply modify or create a new HTML, CSS or Javascript files based on any of the pre-delivered designs. For example, the style like **background color, font color or sizes can be easily modified in the CSS files**.  
You can modify the [HitCounter.template](HitCounter.template) which comes with the installation or you can create an own template file to set custom settings for your designs at a single location.  
> The application is using JavaScript syntax when writing data into the output file. Therefore the template has to have a line with the text **HITCOUNTER_LIST_START** which is the starting mark. The mark with all further lines will be replaced with the JavaScript equivalent of the application's current data. This replacement is done until the **HITCOUNTER_LIST_END** text mark is reached.  
Eventually it means **you are 100% free in the design of your hit counter**.  

## Anything is missing, something is annoying/can be improved or you just found a bug?
Message me via GitHub / e-mail or simply open an issue and I will try to help you out. Alternatively you can also send me a whipser on Twitch: [GeneralGunrider](https://www.twitch.tv/generalgunrider)

## Special thanks
I would never have created this tool without the inspiration by watching the awesome 0 hit and no death runners...  
Thanks to (in alphabetical order):
* [CouchJockey](https://www.twitch.tv/couchjockey)
* [Dinossindgeil](https://www.twitch.tv/dinossindgeil)
* [DonnyRekt](https://www.twitch.tv/donnyrekt)
* [FaraazKhan](https://www.twitch.tv/faraazkhan)
* [GUD_LAK](https://www.twitch.tv/gud_lak)
* [Kazoodle](https://www.twitch.tv/kazoodle)
* [Sayvi](https://www.twitch.tv/sayvi)
* [SlipperySuzie](https://www.twitch.tv/slipperysuzie)
* [Soldi](https://www.twitch.tv/soldi)
* [SquillaKilla](https://www.twitch.tv/squillakilla)
* [The_Happy_Hob](https://www.twitch.tv/the_happy_hob)
* [TigerG92](https://www.twitch.tv/tigerg92)
* Every member of the [Hitless team on Twitch](https://www.twitch.tv/team/hitless)
* And also all the other great challenge runners out there that I cannot name here all.
  
> Praise the sun!  :sunny: . . . :fire: . . .  :running: :dash: 
