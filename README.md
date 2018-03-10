# HitCounterManager

Manages a hit counter running in the background, so you can focus on your stream.  
No need to keep any windows open for a window capture any more.
  
To see some **screenshots** of this application go to the **[Wiki pages](../../wiki)**.  
To see **how to** use the application watch the **[Tutorial on YouTube](https://www.youtube.com/watch?v=iXGExlS4xeM)**.

## Key-Features

* Offline application
* No window capture needed for stream integration
* Easy to use profile management
* Hot key support for seamless use ingame

## Features
It was designed for Dark Souls III "0 hit" or "no death" runs to count every hit/death during the playthrough
* Creates a HTML file of the hit counter that can be used by Open BroadCaster Software in order to make it visible on stream
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
* Counts the attempts of each run (implicitly on every run's reset or manually)
* Settings file "HitCounterManagerSave.xml" holds all your configuration data and is designed to work in newer versions, too. Save and restore this file at any time.
* Comes with some pre-defined profiles for:
  * Bloodborne + The old hunters
  * Dark Souls 1 Prepare To Die Edition
  * Dark Souls 2
  * Dark Souls 3 + Ashes of Ariandel + The Ringed City
  * The Surge + A walk in the park
* Comes with different designs:
  * **Numeric**
    * Shows the amount of hits for the current run, PB and their differences
    * Every split is marked "better" which has an amount of zero hits
  * **PBSplits**
    * Shows the amount of hits for the current run, PB and their differences
    * Every split is marked "better" which has a less or equal amount of hits compared to the PB split
  * **YesNo**
    * Shows check marks/crosses if one got hit at the current run and PB
    * Every split has a check mark which has an amount of zero hits
  * **NoDeath**
    * Shows check marks/crosses until the players death for the current run and PB
    * All splits have check marks until the current split appears (__Now__ only) or a split appears that has already counted hits.
* The appearance of all designs can by modified via GUI
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
* OS: Windows Vista, Windows Server 2003 or newer
* [.Net Framework 4.5](https://www.microsoft.com/net)

### Using the Installer
The installer places all files at your installation folder (default: My Documents) or any other location you like.  
> Note: There is only one requirement that the application must able to read/write data in the installation folder.

### Configuration file
The file [HitCounterManagerSave.xml](HitCounterManagerSave.xml) is used for all settings you can set by this application.  
You can save and restore this file at any time or copy it to a newer/portable version of this application.
> Note: It holds all your settings and profiles in one place. No other files are involved.

### Designs
The pre-delivered HTML designs will update themself every 1.5 seconds.
> Previews of the pre-delivered HTML desings can be found at the **[Wiki pages](../../wiki)**.

#### Configuring Browser Source for OBS Studio
Just insert the HTML's filepath in the URL field.
> To avoid cross domain issues, OBS Studio added the pseudo domain _absolute_ for local files. That said the path sould look like this:
> http://absolute/C:/MyHitCounter/HitCounterNumeric.html

#### Configuring CLR browser plugin for Open Broadcaster Software (OBS, not OBS Studio!)
The opacity can be set to 100% because the background will be rendered transparent, so no color-keying is involved.
Since CRL browser plugin come with some pre-defined CSS, this may cause trouble with the rendering of the HTML file (flickering or misplacement).
If you encounter issues here, try to simply remove the CLR browser plugin's CSS overrides.  
> Recommended: Opacity 100% and no CSS overrides

When no data is displayed, there could be a problem with cross-domain security settings. This is because the HTMLs are rendered via file:// protocol instead of http://. Make sure to allow file access from files.
> OBS -> Settings -> Browser -> Instance -> ...  
> * FileAccessFromFilesUrls (Set to **Enabled**)
> * UniversialAccessFromFilesUrls (Set to **Enabled**, _but should also work when disabled_)
> * WebSecurity (Set to **Disabled**, _but should also work when enabled_)

#### Using standalone Chrome browser
When no data is displayed, there could be a problem with cross-domain security settings. This is because the HTMLs are rendered via file:// protocol instead of http://. Make sure to allow file access from files.
> Start Chrome with **--allow-file-access-from-files**  
> Example: _"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" --allow-file-access-from-files_

### Changing layout and design
You can modify or create new custom designs. Simply modify or create a new HTML file based on any of the pre-delivered designs.  

Alternatively you can modify the [HitCounter.template](HitCounter.template) which comes with the installation or you can create an own template file to get the look you want. That means **you are 100% free in the design of your hit counter**.  
> The application is using JavaScript syntax when writing data into the output file. Therefore the template has to have a line with the text **HITCOUNTER_LIST_START** which is the starting mark. The mark with all further lines will be replaced with the JavaScript equivalent of the application's current data. This replacement is done until the **HITCOUNTER_LIST_END** text mark is reached.  

## Anything is missing, something is annoying/can be improved or you just found a bug?
Message me via GitHub / e-mail or simply open an issue and I will try to help you out. Alternatively you can also send me a whipser on Twitch: [GeneralGunrider](https://www.twitch.tv/generalgunrider)

## Special thanks
I would never have created this tool without the inspiration by watching the awesome 0 hit and no death runners...  
Thanks to:
* [The_Happy_Hob](https://www.twitch.tv/the_happy_hob)
* [FaraazKhan](https://www.twitch.tv/faraazkhan)
* [SquillaKilla](https://www.twitch.tv/squillakilla)
* [CouchJockey](https://www.twitch.tv/couchjockey)
* [SlipperySuzie](https://www.twitch.tv/slipperysuzie)
* [DonnyRekt](https://www.twitch.tv/donnyrekt)
* [TigerG92](https://www.twitch.tv/tigerg92)
* [SayviTV](https://www.twitch.tv/sayvitv)
* [Kazoodle](https://www.twitch.tv/kazoodle)
* [GUD_LAK](https://www.twitch.tv/gud_lak)  
  
  
  
> Praise the sun!  :sunny: . . . :fire: . . .  :running: :dash: 
