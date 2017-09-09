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
  
  t
