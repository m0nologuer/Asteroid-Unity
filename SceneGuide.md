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

Every rendered object in the scene has to use the Asteroid PBR shader. The variables set in the shader are used for rendering in the simulation.

### Object lists

Object lists control groups of objects. Every object that is part of the simulation should be connected to one of these. The main point of sorting objects into lists is so you can apply randomizers.

### Randomizers

![alt text](http://i.imgur.com/lKvrzhW.png "Logo Title Text 1")

Randomizers are used to vary the simulaiton every time it is run. Every simulation can have multiple randomizer trees. Randomizer trees are like root nodes to which you can attach different randomizers.

#### Asteroid Randomizer Tree

- target: object list that the tree acts on
- options: array of named options (can be any strings)
- probabilities: array of probabilities (should add up to one)
- randomizers: array of randomizers to call next.

The randomizer tree works by selecting a random option every time the simulation is spawned. Depending on the random choice that time, **it applies the randomizer in the corresponding slot**. The randomizer in any slot can *itself* be a randomizer tree- which allows for unlimitied branching.

The named options selected by the randomizer are stored, and can be used for scripting behavior.

#### Asteroid Randomizer

Randomizers can come in useful for creating variations on the scene that you have.

- color randomizer: if enabled, the objects in that list will be spawned with a new color every time
- texture randomier: if enabled, the objects in that list will have noise randomly added to their texture
- spawn randomizer: if enabled, the objects in that list will be spawned at random positions

- random_color_brightness: the brightness of the random colors, ranging from 0 (black) to 1 (e.g. bright red)

- spawn_number: number of objects n to be spawned each time (we select at random n from the list of all objects)
- spawn_x: if we take the position & orientation of the list object as the origin, how far away in the x direction can the objects be randomly spawned
- spawn_y: same as above, except for y direction.

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
- frame1: the first rotation & positional frame of the constraints. 
- frame2: the second frame (can be blank) 

![alt text](http://i.imgur.com/eDu2Ejb.png "Logo Title Text 1")

The prefab "Asteroid Constraint Frame" which has a model arrow attached can be used to specify the frames. 

The three "var" varibles set properties of the constraint depending on which type it is. They are only relevant to the hinge and cone constraints. Var1 and Var2 represent the angular degrees of freedom.


### Scene description

The scene description is what holds the whole simulation together. It references the object lists and sensors, as well as:

- lights: only point lights are processed correctely
- action_dim: the number of degrees of freedom of user input
- action_controller: action controller is an object whose script contains a function "act" for processing the user's input
- score_keeper: score keeper is an object whose script keeps tabs on whether the sim is over, and what the score is
- camera: the camera object

### Scripting

The ["component.py" script](https://gist.github.com/m0nologuer/5415e5ea9cf83335d3882bec8b6badc8) contains a component class that every object controller script should be derived from.

Check out the [scripting guide](https://github.com/m0nologuer/Asteroid-Unity/blob/master/ScriptingGuide.md) for an idea of what each object script should contain.
