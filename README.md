# SA2 Hunting Teacher

This project aims to help new SA2 runners learn hunting pieces efficiently.

## Description

Normally new runners to SA2 face a daunting task for learning all of the emerald pieces in the game reuquired to effectively run either of the 2 main story modes that make up the 2 most popular categories.
This processes generally involves spending countless hours grinding each individual hunting stage over and over until the player feels as though they've learned all of the possible pieces in that stage and there truly are no shortcuts.
Even then it's possible (and indeed very common) that one or two very rare pieces could have slipped through the cracks of this process.
So I developed this project to aid new players in learning their pieces by playing through a curated list of sets that guarantees the player will not only see every single possible piece in the stage, but also that they will do so as efficiently as possible
without any unnecessarily repeated sets.

## Getting Started

### Dependencies

* Windows
  * Linux running Wine should also work but this tool is not designed for or tested under linux.
* [Sonic Adventure 2 - Steam Version](https://store.steampowered.com/app/213610/Sonic_Adventure_2/)
  * There are minor differences between the different platform releases but in general learning the pieces on 1 is enough to know them on all
  * That said, this tool is intended for the Steam version, and requires the steam version of the game to be running alongside on the same machine.
* [.NET 8 Desktop Runtime](https://aka.ms/dotnet-core-applaunch?missing_runtime=true&arch=x64&apphost_version=8.0.0&gui=true)
  * Installed in your Wine environment if running Linux
* Optional but HIGHLY recommended:
  * [SA Mod Manager](https://github.com/X-Hax/SA-Mod-Manager/releases/latest)
	* This tool doesn't require the mod loader to be installed, but it's highly recommended you have this anyway for the general fixes the mod loader provides
	* As well as for the ability to install QoL speedrunning mods and practice mods.
  * [SA2 Story Style Upgrades](https://github.com/QueenSuzie/sa2-story-style-upgrades/releases/latest)
	* With `Include Current Hunting Level Upgrade` set to ON
	* Can also be 1-Click installed from [GameBanana](https://gamebanana.com/mods/478254)
  * A [fully completed save file](https://www.speedrun.com/sa2b/resources/acci5) to play on
	* Or at least one which has all the hunting levels unlocked for access through Stage Select

### Installing

The latest version can be found for download as an executable file on the [releases tab.](https://github.com/QueenSuzie/sa2-hunting-teacher/releases)

### Usage

To use this tool:

* Run it as an Administrator (you should get a prompt to run as admin anyway)
* Ensure SA2 is running before pressing start (it doesn't matter which order you open this or SA2 in as long as both are running)
* Configure the tool
  * Set the level you want to learn and number of repetitions
* Press start in Hunting Teacher app
* In game, load into the level you selected in the app
* Continue playing this level until your sequence is complete!

This tool will ensure that for each repitition of a sequence you see every single piece in the level at least once.
While running this tool you will have infinite lives so you don't need to worry about game overing.
The tool will also ensure that your set does NOT change if you die or restart the level.
Exiting the stage will also not progress the sequence so you get the same set you were already on until you collect all 3 pieces and "win" the level.
It's necessary to actually touch the third piece and trigger a win condition to progress the sequence.

Once a sequence is complete the tool will automatically reset itself to allow you to train in other levels.
You can also break out of a sequence early at any time by pressing the reset button.

## License

This project is licensed under the [GPL v3.0] License - see the LICENSE file for details