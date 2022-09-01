
# HitCounterManager - A Dark Souls inspired Hit Counter

**Free Hit Counter** that is running in the background, so you can focus on your stream.  
No need to keep any windows open for a window capture any more.  
Initially designed for Dark Souls but supports any game.  
Just add the local HTML file to you broadcasting software and the setup is done.  
**Works completely offline, no account or login required.**

[![Download latest version here](https://img.shields.io/badge/-Download%20latest%20version%20here-brightgreen?longCache=true&style=for-the-badge)](../../releases/latest)
[![Setup Guide / Wiki](https://img.shields.io/badge/-Setup%20Guide%20%2F%20Wiki-blue?longCache=true&style=for-the-badge)](../../wiki)
[![Tutorial on YouTube](https://img.shields.io/badge/-Tutorial%20on%20YouTube-red?longCache=true&style=for-the-badge)](https://www.youtube.com/watch?v=iXGExlS4xeM&list=PLvBCl9o55PB7BYB7vXVxQuP5J27X_XXzm)

<p align="center"><a href="Images/Preview.png"><img src="Images/Preview.png" alt="Preview" width="700px"/></a></p>

[![Releases](https://img.shields.io/github/release/topeterk/HitCounterManager.svg?label=Latest%20release:&longCache=true&style=for-the-badge&colorB=0088FF)](../../releases/latest)
[![GitHub Releases](https://img.shields.io/github/downloads/topeterk/HitCounterManager/total.svg?label=Downloads:&longCache=true&style=for-the-badge&colorB=0088FF)](../../releases)
[![GitHub](https://img.shields.io/github/license/topeterk/HitCounterManager.svg?label=License:&longCache=true&style=for-the-badge&colorB=0088FF)](LICENSE)

## Key-Features

* Offline application
* No window capture needed for stream integration
* Hot key support for seamless use **ingame** (Windows only)
* Easy to use profile management
* On the fly configurable design
* Configurable keyboard hot keys that can easily be used while playing
* One single save file for all settings and profiles: **HitCounterManagerSave.xml**
  > Note: The settings will be kept when upgrading on newer versions.
  > I try my best keeping it backwards compatible that you can still load an upgraded save file on older versions.
* Create profiles for each challenge or game or build a sequence of runs (called succession) like for the Dark Souls trilogy
* Dark Mode (not the best looking but hey: its dark)
* Timer (including current, PB and gold split times)
* Customizable designs (Find [some examples](../../wiki/Designs) at the [Wiki pages](../../wiki))

### Pre-defined profiles

* Bloodborne + The old hunters
* Dark Souls 1 Prepare To Die Edition
* Dark Souls 2
* Dark Souls 3 + Ashes of Ariandel + The Ringed City
* Demon's Souls
* Elden Ring
* Salt and Sanctuary
* Sekiro
* The Surge + A walk in the park
* The Surge 2
* Mortal Shell
* Celeste
* Crash Bandicut 1 
* Crash Bandicut 2
* Crash Bandicut 3
* Crash Bandicut 4
* Cuphead
* Hades
* Hollow Knight

## Get the software
All available releases can be found at the [Releases page](../../releases) on GitHub.  
[![Download latest version here](https://img.shields.io/badge/-Download%20latest%20version%20here-brightgreen?longCache=true&style=for-the-badge)](../../releases/latest)

## Systemrequirements
* OS: Windows Vista, Windows Server 2003 or newer (32/64 bit)
  * Portable version (ZIP)
    * [.Net Framework 2.0 or newer](https://www.microsoft.com/net)
    > **Note**: Version 1.13 and older requires [.Net Framework 4.5 or newer](https://www.microsoft.com/net)
  * Installer version (Setup)
    * [.Net Framework 4.8 or newer](https://www.microsoft.com/net)
    > **Note**: Version 1.20 and older requires [.Net Framework 4.5 or newer](https://www.microsoft.com/net)
* OS: Any (32/64 bit)
  > **Note**: This version is **not supporting global hot keys**  
  * Portable version (Tar ball)
    * Running with [Mono](https://www.mono-project.com/) (_tested with 5.14.0_)  
      Start the application in the application's directory with **mono HitCounterManager.exe**  
      > **Note**: Mono supports **32 bit mode only** (therefore it cannot be run on OS without 32 bit support!)

In case your non-Windows OS is no longer supporting 32 bit Mono then this application will not run!  
However, there is a new HitCounterManager in development (version 2.x) that can be used instead.  
Please have a look at the _v2.x_ branch or find *pre-compiled development builds* [here](../../issues/21).

## Installation, Guides, Help
Please have a look at the **[Wiki pages](../../wiki)**, the **[Setup Guide](../../wiki/SetupGuide)** or the [FAQ](../../wiki/SetupGuide#FAQ).

## Anything is missing, something is annoying/can be improved or you just found a bug?
Message me via GitHub / e-mail or simply open an issue and I will try to help you out. Alternatively you can also send me a whipser on Twitch: [GeneralGunrider](https://www.twitch.tv/generalgunrider)

## Community
There is a great growing community on Discord called [Team Hitless](https://discord.gg/4E7cSK7).
If you are seeking for help, guidance or just talk about no hit runs, this is the right place for it.
And If you English is low level you can visit spanish community on [No Hit Hispano](https://discord.gg/ntygnch)  
Thanks to everyone for helping other community members!

## Special thanks
I would never have created this tool without the inspiration by watching the awesome 0 hit and no death runners...  
Thanks to (in alphabetical order):
* [CouchJockey](https://www.twitch.tv/couchjockey)
* [Dinossindgeil](https://www.twitch.tv/dinossindgeil)
* [DonnyRekt](https://www.twitch.tv/donnyrekt)
* [FaraazKhan](https://www.twitch.tv/faraazkhan)
* [Kazoodle](https://www.twitch.tv/kazoodle)
* [Sayvi](https://www.twitch.tv/sayvi)
* [SlipperySuzie](https://www.twitch.tv/slipperysuzie)
* [Soldi](https://www.twitch.tv/soldi)
* [SquillaKilla](https://www.twitch.tv/squillakilla)
* [The_Happy_Hob](https://www.twitch.tv/the_happy_hob)
* [TigerG92](https://www.twitch.tv/tigerg92)

* Every member of the [Hitless team on Twitch](https://www.twitch.tv/team/hitless) and [No Hit Hispano on Twitch](https://www.twitch.tv/nohithispano)
* And also all the other great challenge runners out there that I cannot name here all.
  
 
# **AutoSplitter Extension**

* This Extension can give you the possibility to Automatically split when an event happen in a game
* Posibility to AutoSplit for:
 Dark Souls 1, Dark Souls 2, Dark Souls 3, Sekiro Shadow Die Twice, Elden Ring, Hollow Knight, Celeste, Cuphead and Others 
* Auto start Timer when game run is started
* Using InGameTime or RealTime Values on the Internal Timer, Delta, Total Time and PB in OBS Layout
* Save file for all settings of AutoSplit: **HitCounterManagerSaveAutoSplitter.xml** compatible with all futures versions.

## AutoSplitter Flags

 - Sekiro || **[Inmediatly/Loading After]**
    - Kill a Boss
    - Is Activated a Idol 
    - Trigger a position.

 - Elden Ring || [**Inmediatly/Loading After]**
   - Kill a Boss.
   - Is Activated a Grace
   - Trigger a position

- DarkSouls 1 || **[Inmediatly/Loading Game After]**
   - Kill a Boss
   - Active a Bonfire
   - Level the Charapter
   - Trigger a Position
   - Obtain a Item

 - DarkSouls 2 || **[Inmediatly/Loading After]**
   - Kill a Boss
   - Level the Charapter
   - Trigger a position

 - DarkSouls 3 || **[Inmediatly/Loading After]**
   - Kill a Boss
   - Is Activated a Bonfire
   - Level the Charapter
   - Custom Flag

 - Hollow Knight || **[Inmediatly]**
   - Kill a Boss/Minibosses
   - Dreamers/Kills Events
   - Colosseum
   - Pantheons
   - Charms/Skills
   - Trigger a position

 - Celeste || **[Inmediatly]**
   - Chapter
   - Checkpoints

 - Cuphead *v1.2.4* || **[Inmediatly]**
   - Kill a Boss
   - Complete a Level
> Unfortunately the creator of the dll has it out of date, and in LiveSplit there is only one TempFix that is not applicable here. If one day he updates it, I will be happy to do it.


 - ASL || **[Inmediatly]**
   - Any ASL with Split (AutoSplitter Language for LiveSplit).
	> Can give you the possibility to use AutoSplitting for any game that was development ASL file to LiveSplit


## AutoMods Features
- Sekiro: 
   - No Logos Intro
   - No Tutorials 

- Elden Ring:
   - No Logos Intro

## Systemrequirements
* Software: [.Net Framework 4.8 or newer](https://dotnet.microsoft.com/en-us/download/dotnet-framework).
* OS: Windows 7 or newer (**64-bits only**).
* Execute the program as Administrator.

## Issues or Bugs
* Issues Contact to <ezequielmedina23@gmail.com> or Twitch: [Neimex23](https://www.twitch.tv/neimex23).

## Special thanks

Thanks to Beta Testers of AutoSplit(in alphabetical order):
* [AvegaX](https://www.twitch.tv/avegax)
* [Bajamuten](https://www.twitch.tv/bajamuten)
* [Bender](https://www.twitch.tv/BenderzGreat)
* [Cursedwind](https://www.twitch.tv/cursedwind)
* [DanielloPiuBello](https://www.twitch.tv/daniellopiubello)
* [Duuruk](https://www.twitch.tv/disabled_dogs)
* [Empaventuras](https://www.twitch.tv/empaventuras)
* [Hyakujin](https://www.twitch.tv/hyakujin)
* [Keinon](https://www.twitch.tv/soykeinon)
* [LokMae](https://www.twitch.tv/lokmae)
* [Slash15](https://www.twitch.tv/slash15_)
* [OlallaZ2](https://www.twitch.tv/olallaz2)
* [Zirob21](https://www.twitch.tv/zirob21)

* Special Thanks to **Hauke Lasinger** and **[Mario Schulz](https://www.twitch.tv/D4rn4S)** for develop the IGT Track Timer :fire:

[-]..And All that participated in the Pre-Release version.

##
> Praise the sun!  :sunny: . . . :fire: . . .  :running: :dash: 
