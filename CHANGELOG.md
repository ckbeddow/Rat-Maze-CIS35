4/12:
	Created trigger for electric floor. Now recognizes when the player walks into it and displays a message that says "Zap! You died." 
	Also there is a block prefab on level Basic 2.0 that will deactivate the floor for some seconds when the rat collide with it.
	Added a timer to track how long it takes to solve the maze. May needed to be edited when we introduce transition from one level to another.

4/4:
 	Fixed electricity lighting.

3/25:
	Added two basic levels

3/10:
	Added Rat Model. Added new ratcontroller and camera script for the new model. 
	Created a new scene and maze text file called "ClassDemo". Also testing electric floor effects.

3/8:
	Added a lightning prefab. 

2/13:
	Played with README.md. Continue to access, change, add, commit, and push to get used to git hub commands. 

2/9:
	I made major changes to how the editor works. Now mazes are be generated in the editor using the maze builder script on the MazeBuilder gameobject
	The maze builder can take either a string and dimentions as before or can take a .txt file.
	The txt file has information for each of the maze tiles seperated by newlines. The format is (tiletype,x,z) see testmaze1.txt for an example
	This way we can make a level as a scene then manually add objects in and test it out.
	Future addition will be an easier way of building the txt file for the maze builder.

