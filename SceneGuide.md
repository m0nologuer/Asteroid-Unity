### Objects

Asteroid Objects are regular 3D objects in the scene. Physics is applied to these objects automatically. They may also have a script attached to them that controls behavior.

- object_name : name of the objct, can be blank
- python_script : a python script for controlling the object's behavior
- python_class : the name of the class that controls the behavior of the object
- mass : for the physics simulator
- velocity : for the physics simulator
- material: asteroid material that describes physical properties.
  
When objects have a script attached to it, that derives from the class in the ["component.py" script](https://gist.github.com/m0nologuer/5415e5ea9cf83335d3882bec8b6badc8) 

### Materials

Materials describe the physical properties of the object, for use in physics simulation. Every material referenced by an object must be included in the master list of materials attached to the scene description.

### Object lists

Object lists control groups of objects. The one thing they do differently is allow for some randomness in the simulation.

- children: the Asteroid objects included in this list.

- use_randomizer: do we enable the randomizer at all?
- use_color_randomizer: if enabled, the objects in that list will be spawned with a new color every time
- use_texture_randomizer: if enabled, the objects in that list will have noise randomly added to their texture
- use_material_randomizer: if enabled, the objects in that list will have their physical properies randomized
- use_spawn_randomizer: if enabled, the objects in that list will be spawned at random positions

- random_material_variance: the variance with which to randomize physical material properties
- random_color_brightness: the brightness of the random colors, ranging from 0 (black) to 1 (e.g. bright red)

- spawn_number: number of objects n to be spawned each time (we select at random n from the list of all objects)
- spawn_x: if we take the position & orientation of the list object as the origin, how far away in the x direction can the objects be randomly spawned
- spawn_y: same as above, except for y direction.

### Buffers

It's possible to render to multiple buffers using different shaders. This might be desirable if, for example, you want to have one buffer that contains an image render, and another that contains a segmented image. At the moment, only Cg shaders are supported. 
  
 - name: buffer name
 - shader : main shader path
 - filters : a bunch of screen space post-processing filters path

### Constraints

These are used to fixing two objects to each other, or to the scene. See [Bullet constraints](https://www.panda3d.org/manual/index.php/Bullet_Constraints) for more details.

- object1: the first object to attach
- object2: the second object to attach (can be blank, in which case we are just fixing an object in place)
- type - Hinge, Ball, Slider or Cone

The three "var" varibles set properties of the constraint depending on which type it is.


### Scene description

The scene description is what holds the whole simulation together. It references the object lists, materials, camera and buffers, which are self-explanatory. The other variables:

- lights: only point lights are processed correctely
- action_dim: the number of degrees of freedom of user input
- action_controller: action controller is an object whose script contains a function "act" for processing the user's input
- score_keeper: score keeper is an object whose script keeps tabs on whether the sim is over, and what the score is

### Scripting

The ["component.py" script](https://gist.github.com/m0nologuer/5415e5ea9cf83335d3882bec8b6badc8) contains a component class that every object controller script should be derived from.

Check out the commented out code for an idea of what each object script should contain.
