# HitCounterManager - A Dark Souls inspired Hit Counter

**Free Hit Counter** that is running in the background, so you can focus on your stream.  
No need to keep any windows open for a window capture any more.  
Initially designed for Dark Souls but supports any game.  
Just add the local HTML file to you broadcasting software and the setup is done.  
**Works completely offline, no account or login required.**

[![Download latest version here](https://img.shields.io/badge/-Download%20latest%20version%20here-brightgreen?longCache=true&style=for-the-badge)](../../releases/latest)
[![Addons](https://img.shields.io/badge/-Addons-yellow?longCache=true&style=for-the-badge)](#addons)
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
* Celeste
* Crash Bandicoot 1
* Crash Bandicoot 2
* Crash Bandicoot 3
* Crash Bandicoot 4
* Cuphead
* Dark Souls 1 Prepare To Die Edition
* Dark Souls 2
* Dark Souls 3 + Ashes of Ariandel + The Ringed City
* Demon's Souls
* Elden Ring
* Hades
* Hollow Knight
* Mortal Shell
* Salt and Sanctuary
* Sekiro
* The Surge + A walk in the park
* The Surge 2

## Get the software
All available releases can be found at the [Releases page](../../releases) on GitHub.  
[![Download latest version here](https://img.shields.io/badge/-Download%20latest%20version%20here-brightgreen?longCache=true&style=for-the-badge)](../../releases/latest)

## Addons
There are addons available now, e.g.:
* The LiveSplit plugin [**LiveSplit.HitCounterManagerConnector**](https://github.com/topeterk/LiveSplit.HitCounterManagerConnector)
  * Automatically selecting the HCM splits and restarts a new run when LiveSplit is doing so either due to AutoSplitter or manual input.
* The HitCounterManager plugin [**AutoSplitterCore for HCM**](https://github.com/neimex23/HitCounterManager)
  * Adds AutoSplitting and InGameTime directly into HitCounterManager without requiring LiveSplit

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
    * Running with [Wine](https://www.winehq.org) (_tested with 10.0 on Ubuntu 25.04_)  
      Start the application in the application's directory with **wine HitCounterManager.exe**  
      > **Note**: Wine seems to required **32 bit mode** (therefore it might not run on OS without 32 bit support!)  
    * Running with [Mono](https://www.mono-project.com/) (_tested with 5.14.0_)  
      Start the application in the application's directory with **mono HitCounterManager.exe**  
      > **Note**: Mono supports **32 bit mode only** (therefore it cannot be run on OS without 32 bit support!)  
      > However, it __may__ work now due to latest Mono releases (but not tested yet)

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
Thanks to everyone for helping other community members!

To learn more about Team Hitless you can also have a look at the [website](https://www.teamhitless.com).

## Special thanks
I would never have created this tool without the inspiration by watching the awesome 0 hit and no death runners...  
Thanks to (in alphabetical order):
* [CouchJockey](https://www.twitch.tv/couchjockey)
* [Dinossindgeil](https://www.twitch.tv/dinossindgeil)
* [DonnyRekt](https://www.twitch.tv/donnyrekt)
* [FaraazKhan](https://www.twitch.tv/faraazkhan)
* [Sayvi](https://www.twitch.tv/sayvi)
* [SlipperySuzie](https://www.twitch.tv/slipperysuzie)
* [Soldi](https://www.twitch.tv/soldi)
* [SquillaKilla](https://www.twitch.tv/squillakilla)
* [The_Happy_Hob](https://www.twitch.tv/the_happy_hob)
* [TigerG92](https://www.twitch.tv/tigerg92)
* [Zoodle](https://www.twitch.tv/zoodle)
* Every member of the [Hitless team on Twitch](https://www.twitch.tv/team/hitless)
* And also all the other great challenge runners out there that I cannot name here all.
  
> Praise the sun!  :sunny: . . . :fire: . . .  :running: :dash: 
