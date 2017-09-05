# Asteroid-Unity
This is a Unity plugin for creating simulations that can be run on the Asteroid platform.

Asteroid simulations are an entirely different thing from Unity scenes - it's just convinient to use Unity to design them.

You may want to start by checking out the sample scenes [here](https://github.com/m0nologuer/Asteroid-Sim-Example).

## Getting started

To get started,

1. Download the entire folder Asteroid-Unity into the Assets folder of your Unity project.
2. Create *three folders*, one called 'assets', one called 'shaders' and one called 'userscripts', both in the main Assets folder

![alt text](http://i.imgur.com/6hg06xg.png "Logo Title Text 1")


  **Assets**
  * The assets folder is for storing visual content 3D models and textures. 
  
  **Scripts**
  * The userscripts folder is for Python scripts. Scripts are used to control the actions of every object in the scene.
  * [Optional] You can initialize a github repo in this folder. If you add a remote, Asteroid will automatically keep pulling the most recent version of the scripting code from that repo (especially useful when debugging).
  
  **Shaders**
  * The shaders folder is for custom shaders - each 'render mode' of the scene may be different.
  * They should be written in either cG or GLSL.

3. Create an empty GameObject in Unity and add to that the script 'GenerateAsteroidScene.cs'. 
  * This object, when filled in, should contain everything relevant to the simulation scene.
  * When you press play, it will **generate a json representation** of the scene at Assets/assets/scene.json

4. Zip the assets, shaders and userscripts folder into a single zip file & upload to Asteroid!

## Creating a scene

Check out the Unity sample scenes [here](https://github.com/m0nologuer/Asteroid-Sim-Example).

For a more detailed guide of how to create a scene [here](https://github.com/m0nologuer/Asteroid-Unity/blob/master/SceneGuide.md).

For a more detailed scripting guide go [here](https://github.com/m0nologuer/Asteroid-Unity/blob/master/SceneGuide.md).

## Support
Email saku.p@cantab.net for absolutely anything related to support, feature requests etc etc.
