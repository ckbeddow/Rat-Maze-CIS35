# Rat-Maze-CIS35
Rat Maze project for Cabrillo CIS35 Class

### Team Members:
* Chuck Beddow - charles.beddow@gmail.com
* Clara Olson - clara.olson22@gmail.com
* Dave VandenHoek - david.vandenhoek@gmail.com

### Description:
The player is an albino lab rat part of a cognitive psychology experiment. He finds himself inside a maze with obstacles and challenges that he must overcome in order to find the coveted cheese at the end!

### Team Roles:
#### Chuck:
* Rat Design/Animation
* Models
* Scenery
* Skybox
* Lighting 

#### Clara: 
* Level Design
* Particle System
* Interactive Obstacles
* Obstacle Generator Algorithm (given time)

#### Dave:
* Sound Effects
* Navigation/Movement
* iOS Deployment
* Opening GUI

### Features:
* Mouse Trap/Stuffed Mouse
* Electric Floor/Lever
* Rolling Ball/Spider (Timing a trip on a hallway)

### Contextual Ideas:
* Light
* Water Bottle
* Sugar Pellet Dispenser

### Maze Designer Automation
* Randomly generate maze
* Randomly select coordinates outside wall to be StartTile and EndTile (with Rat and Cheese Prefabs spawning with these tiles)
* Use a Maze Solver algorithm so find shortest route from StartTile to EndTile
* Create an obstabcle along the route determined MazeSolver
* Use disjointed sets to make sure that the "key" is placed in the same set as the StartTile and Obstacle
* Consider the Key-Obstacle a "Mini Maze" with a sub-StartTile(obstacle) and sub-EndTile(key)
* Can repeate steps within the minimaze to combine obstacles as mazes get more complicated
* We then won't have to design by hand

### Data Structures Relevant Algorithm Assignments
* Assignment 6: Disjointed Sets (Maze Generator) - lecture passed - due April 30
* Assignment 8: Breadth-First Search (Maze Solver) - lecture 4/12 - due TBA
