# Asteroid-Unity
This is a Unity plugin for creating simulations that can be run on the Asteroid platform.

Asteroid simulations are an entirely different thing from Unity scenes - it's just convinient to use Unity to design them.

## Getting started

To get started,

1. Download the entire folder Asteroid-Unity into the Assets folder of your Unity project.
2. Create *three folders*, one called 'assets', one called 'shaders' and one called 'userscripts', both in the main Assets folder

  **Assets**
  * The assets folder is for storing 3D models. *OBJ files only.*
  
  **Scripts**
  * The userscripts folder is for Python scripts. Scripts are used to control the actions of every object in the scene.
  * Add an init.py file the userscripts folder, which should be blank except for one line ''' __all__ = ['name1', 'name2'] ''' where the names are Python files in the folder
  * Every asteroid object python script is derived from [components.py](https://gist.github.com/m0nologuer/5415e5ea9cf83335d3882bec8b6badc8). Add that file to the userscripts folder.
  * [Optional] You can initialize a github repo in this folder. If you add a remote, Asteroid will automatically pull the most recent version of the scripting code from that repo (especially useful when debugging).
  
  **Shaders**
  * The shaders folder is for custom shaders - each 'render mode' of the scene may be different.

3. Create an empty GameObject in Unity and add to that the script 'GenerateAsteroidScene.cs'. 
  * This object, when filled in, should contain everything relevant to the simulation scene.
  * [More on how to fill this scene out below]
  * When you press play, it will generate a json representation of the scene at Assets/assets/scene.json

4. Zip the assets, shaders and userscripts folder into a single zip file & upload to Asteroid!

## Creating a scene

Check out the Unity sample scene [here](https://github.com/m0nologuer/Asteroid-Sim-Example).

For a more detailed guide of how to create a scene [here](https://github.com/m0nologuer/Asteroid-Unity/blob/master/SceneGuide.md).

## Support
Email saku.p@cantab.net for absolutely anything related to support, feature requests etc etc.
