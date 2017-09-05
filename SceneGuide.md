# Asteroid Scene Guide

![alt text](http://i.imgur.com/KnbNek9.png "Logo Title Text 1")

### Objects

Asteroid Objects are regular 3D objects in the scene. Physics is applied to these objects automatically. They may also have a script attached to them that controls behavior.

- object_name : name of the objct, can be blank
- python_script : a python script for controlling the object's behavior
- python_class : the name of the class that controls the behavior of the object
- mass : for the physics simulator
- velocity : for the physics simulator
  
When objects have a script attached to it, that derives from the class in the ["component.py" script](https://gist.github.com/m0nologuer/5415e5ea9cf83335d3882bec8b6badc8). See the [scripting guide](https://github.com/m0nologuer/Asteroid-Unity/blob/master/ScriptingGuide.md) for more info about this.

### Physically Based Rendering

![alt text](http://i.imgur.com/N8WTlO0.png "Logo Title Text 1")

Every rendered object in the scene should use the Asteroid PBR shader. Th

### Object lists

Object lists control groups of objects. Every object that is part of the simulation should be connected to one of these. The main point of sorting objects into lists is so you can apply randomizers.

### Randomizers



### Sensors

It's possible to render the scene from the perspective of different "sensors". This might be desirable if, for example, you want to have one buffer that contains an image render, and another that contains a segmented image. Each of these sensors is rendered from the perspective of a certain camera with a certain shader. At the moment, only Cg and HLSL  shaders are supported. 
  
 - name: buffer name
 - shader : main shader path
 - filters : a bunch of screen space post-processing filters path
 - camera_object: the object which the camera should track

### Constraints

These are used to fixing two objects to each other, or to the scene. See [Bullet constraints](https://www.panda3d.org/manual/index.php/Bullet_Constraints) for more details.

- object1: the first object to attach
- object2: the second object to attach (can be blank, in which case we are just fixing an object in place)
- type - Hinge, Ball, Slider or Cone

The three "var" varibles set properties of the constraint depending on which type it is.


### Scene description

The scene description is what holds the whole simulation together. It references the object lists and buffers, as well as:

- lights: only point lights are processed correctely
- action_dim: the number of degrees of freedom of user input
- action_controller: action controller is an object whose script contains a function "act" for processing the user's input
- score_keeper: score keeper is an object whose script keeps tabs on whether the sim is over, and what the score is
- camera

### Scripting

The ["component.py" script](https://gist.github.com/m0nologuer/5415e5ea9cf83335d3882bec8b6badc8) contains a component class that every object controller script should be derived from.

Check out the [scripting guide](https://github.com/m0nologuer/Asteroid-Unity/blob/master/ScriptingGuide.md) for an idea of what each object script should contain.
