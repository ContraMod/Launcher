# Contra Launcher

<a href="https://discordapp.com/invite/015E6KXXHmdWFXCtt"><img src="https://discordapp.com/assets/e05ead6e6ebc08df9291738d0aa6986d.png" alt="Discord" width="22"></a>
[![Actions Status](https://github.com/Teteros/contra-launcher/workflows/Build/badge.svg)](https://github.com/Teteros/contra-launcher/actions)
[![GitHub Releases](https://img.shields.io/github/release/Teteros/contra-launcher.svg)](https://github.com/Teteros/contra-launcher/releases)
<a href="https://www.moddb.com/mods/contra"><img src="https://button.moddb.com/download/medium/172830.png" alt="Moddb Icon" height="20"></a>

>Contra is a freeware modification for Command and Conquer Generals: Zero Hour real-time strategy game. It's a big project started in 2004. It adds many new units as well as numerous new upgrades, new general's powers and buildings. It also adds new sounds, maps, bug fixes, enhanced graphics, and other effects, as well as three new generals.

## Compiling

Requires Windows 7 or higher and one of the following SDKs:

**.NET Core 3.1** (Preferred)

`dotnet publish /p:Configuration=Release netcore`

Build Location: `netcore\bin\Release\netcoreapp3.1\win-x86\publish\Contra_Launcher.exe`

**.Net Framework 4.5**

`msbuild /p:Configuration=Release /p:Platform=AnyCPU net45`

Build Location: `net45\bin\Release\Contra_Launcher.exe`

Instead of command-line you can also open the solution in Visual Studio 2019 and built from there.

## Running

This repository only contains the source code for the launcher, download the [mod archive](https://www.moddb.com/mods/contra/downloads) to play the mod itself.

Grab latest dev builds (artifacts) from [GitHub Actions](https://github.com/Teteros/ctrlauncher/actions) for testing or download the [stable](https://github.com/Teteros/ctrlauncher/releases/latest) release.

Binary tested on Windows 7-10 and Wine (Linux) x64.
