# Green Hell Mapper

This project is a Unity Mod Manager plugin for the game **Green Hell**, designed to track and display the player's in-game location. The plugin sends real-time location data to a UI software over UDP, allowing you to view your current position and travel history on an interactive map.

## Features

- **Live Location Tracking:** Sends player's current location to an external UI software every 100ms.
- **View cone:** Along with the location data is sent player's forward vector, indicating which direction player is looking at.
- **Efficient Updates:** Location is only sent if the player has moved at least 1 meter from the previous recorded position.
- **Interactive Map:** The UI software supports panning and zooming to explore the map. No map markers at the moment as the only intent was to display the current location.
- **Movement History:** The map displays where the player has been.
- **Coordinates Log:** All locations are saved to a `coordinates.txt` file, located in the same directory as the game's executable (`GH.exe`).
- **UDP Communication:** Location data is transmitted using the UDP protocol for fast and efficient communication. The used ports are 11000 and 11001.

## Motivation

While orienteering in **Green Hell** is an enjoyable challenge, sometimes you just want to navigate more efficiently. This plugin helps players find their way quickly by showing their exact position in real-time, reducing the frustration of wandering around when you're in a hurry.

## How It Works

1. The plugin tracks the local player's position in the game.
2. Every 100ms, if the player has moved more than 1 meter from their last recorded position, the plugin sends the updated coordinates over UDP to the Mapper UI software.
3. The Mapper UI software displays the live location on a map and updates the player's travel history.
4. All recorded coordinates are stored in a `coordinates.txt` file for future reference or analysis.

## Installation

1. Download the latest release of the plugin.
2. Place the plugin file in the Unity Mod Manager plugins folder for **Green Hell**.
3. Ensure that the plugin is activated and the UI software is running.

## Usage

- Start **Green Hell** and ensure the plugin is active.
- Open the Mapper UI software to view your live position on the map.
- The plugin will automatically log your coordinates in `coordinates.txt`.

![Mapper](https://github.com/user-attachments/assets/a2aa21fd-7499-40cf-947a-a8ac254942ae)

## Compiling the project

Make sure that you add following files to the **MapperSource/Libs** folder or update references to them:
- Unity Mod Manager
  - 0Harmony.dll
  - UnityModManager.dll
- Green Hell
  - Assembly-CSharp.dll
  - Assembly-CSharp-firstpass.dll
  - UnityEngine.dll
  - UnityEngine.CoreModule.dll
  - UnityEngine.IMGUIModule.dll
  - UnityEngine.UI.dll
