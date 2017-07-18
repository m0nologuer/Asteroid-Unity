# Asteroid-Unity
A Unity plugin for creating Asteroid scenes

This is a plugin to generate Asteroid simulations from Unity scenes. 
Asteroid simulations are an entirely different thing from Unity scenes - it's just convinient to use Unity to design them.

## Getting started

To get started,

1. Download the entire folder Asteroid-Unity into the Assets folder of your Unity project.
2. Create *three folders*, one called 'assets', one called 'shaders' and one called 'userscripts', both in the main Assets folder
⋅⋅* The assets folder is for storing 3D models. *OBJ files only.*
..* The userscripts folder is for Python scripts. Every single Asteroid object needs to be associated with a python script.
..* Add an init.py file for the userscripts folder, which should be blank except for one line ''' __all__ = ['name1', 'name2'] ''' where the names are Python files in the folder
..* Every asteroid object python script is derived from [components.py](https://gist.github.com/m0nologuer/5415e5ea9cf83335d3882bec8b6badc8). Add that file to the userscripts folder.
..* The shaders folder is for custom shaders - each 'render mode' of the scene may be different.
3. Create an empty GameObject in Unity and add to that the component 'GenerateAsteroidScene.cs'. 
..* This is the object you need to fill in.
..* When you press play, it will generate a json representation of the scene at Assets/assets/scene.json
4. Zip the assets, shaders and userscripts folder into a single zip file & upload to Asteroid!

## Creating a scene

... coming soon.
email saku.p@cantab.net if you want to use this ASAP.
