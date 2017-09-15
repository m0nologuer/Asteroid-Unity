  # Asteroid Scripting Guide
  
  Scripting is totally optional, but can be useful if you are using Asteroid for e.g. machine learning simulation. Scripting is used for
  * making objects behave dynamically
  * processing the actions of an agent
  * scoring the performance of an agent
  * randomizing the simulation

  ### Getting started

  * Add an init.py file the userscripts folder, which should be blank except for one line ''' __all__ = ['name1', 'name2'] ''' where the names are Python files in the folder
  * Every asteroid object python script is derived from [components.py](https://gist.github.com/m0nologuer/5415e5ea9cf83335d3882bec8b6badc8). Add that file to the userscripts folder.
  
  ### Functions
  
  #### Optional: Initialize
  
  The *init* function belongs to every game object. It does nothing much other than automatically set the key variables for each object. There's not much need to override this function at all.
  
  `def __init__(self, name, model, physics):`
  
   The name is a string representing the object name that makes it easier to identify. This can be blank.
   The model is a [Panda3D node](https://www.panda3d.org/reference/1.9.1/python/panda3d.core.NodePath) the represents the 3D model loaded into the game engine.
   The physics variable is a hash that stores the physical properties of the object. The variables are
   * mass
   * velocity
   * position
   * rotation
   * scale
  
  The *setup* function also belongs to every game object. Any setup should be done here, though this is optional and not often necessary. It takes as argument a list of all the other game objects in the scene, plus the outputs of all the randomizer trees in the scene.
  
	`def setup(self, objects, randomizer_trees):`
	
   Setup is called on the objects in the scene in order of their object ID. 
   *randomizer_trees* is an array of an array of strings - representing the named outcomes for each randomizer tree. 
   This part scripting is also totally optional.

  #### Optional: Update
  
  The main function used for gameobject behavior is the update function. It must return task.cont.
  
	`def update(self, task):
	 #self.runtime = self.runtime + self.dt;
		return task.cont`
   
   It is called once per frame and is totally optional.
   
   #### Essential: Action function
   
   For the simulation to process actions correctly, there must be one object designated as a controller. This controller must have a script attached that has a function called "act"
   
  `def act(self, action, object_array):
	 self.physics.velocity.x += action[0] * self.dt
	 self.physics.velocity.y += action[1] * self.dt`
  
    The action is an array of floats, that are sent via the 'act' API call. The size of the array corresponds to the number of degrees of freedom of the simulation.

  #### Essential: Action Monitor
	
  To use the game engine for training simulations, an action monitor is needed for two reasons: to score the simulation using a rewar, and to tell when the simulation is over. 
	
	`def reward(self, object_array):
	 flag = [obj for obj in object_array if obj is Flag][0]
   return (self.physics.position - flag.physics.position).length()`

	`def episode_over(self, object_array):
  	 return (self.runtime > 5)`
  
  For both of these functions, as before, the object array is an array of all the gameobjects in the scene.
