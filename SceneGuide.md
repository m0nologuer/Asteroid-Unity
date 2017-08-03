### Objects

Asteroid Objects are regular 3D objects in the scene. Physics is applied to these objects automatically. Every object must have a script attached to it, that derives from the class in the "component.py" script from above. The 'python_class' variable in the object refers simply to the name of the class that controls the behavior of the object. There is an "update" function that is called once per object.

### Materials

Materials describe the physical properties of the object, for use in physics simulation. Every material referenced by an object must be included in the master list of materials attached to the scene description.

### Object lists

Object lists control groups of objects. The one thing they do differently is allow for some randomness in the simulation. For example, if you use a color randomizer, the objects in that list will be initialized with a new color every time. The material randomizer changes the physical properties of the materials, and the texture randomizer adds random noise to the textures within the object list.

### Buffers

It's possible to render to multiple buffers using different shaders. This might be desirable if, for example, you want to have one buffer that contains an image render, and another that contains a segmented image. At the moment, only Cg shaders are supported. 

### Scene description

The scene description is what holds the whole simulation together. It references the object lists, materials, camera and buffers. 

The action controller is an object whose script contains a function "act" for processing the user's input. The action dim is the number of degrees of freedom of user input.

The score keeper is an object whose script keeps tabs on whether the sim is over, and what the score is.

