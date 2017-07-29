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
  * Every single Asteroid object *must* be associated with a python script.
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

Check out the Unity sample scene [here] (https://github.com/m0nologuer/Asteroid-Sim-Example).

# Objects

Asteroid Objects are regular 3D objects in the scene. Physics is applied to these objects automatically. Every object must have a script attached to it, that derives from the class in the "component.py" script from above. The 'python_class' variable in the object refers simply to the name of the class that controls the behavior of the object. There is an "update" function that is called once per object.

# Materials

Materials describe the physical properties of the object, for use in physics simulation. Every material referenced by an object must be included in the master list of materials attached to the scene description.

# Object lists

Object lists control groups of objects. The one thing they do differently is allow for some randomness in the simulation. For example, if you use a color randomizer, the objects in that list will be initialized with a new color every time. The material randomizer changes the physical properties of the materials, and the texture randomizer adds random noise to the textures within the object list.

# Buffers

It's possible to render to multiple buffers using different shaders. This might be desirable if, for example, you want to have one buffer that contains an image render, and another that contains a segmented image. At the moment, only Cg shaders are supported. 

# Scene description

The scene description is what holds the whole simulation together. It references the object lists, materials, camera and buffers. 

The action controller is an object whose script contains a function "act" for processing the user's input. The action dim is the number of degrees of freedom of user input.

The score keeper is an object whose script keeps tabs on whether the sim is over, and what the score is.

## Support
Email saku.p@cantab.net for absolutely anything related to support, feature requests etc etc.
