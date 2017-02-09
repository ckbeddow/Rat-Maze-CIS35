2/9:
	I made major changes to how the editor works. Now mazes are be generated in the editor using the maze builder script on the MazeBuilder gameobject
	The maze builder can take either a string and dimentions as before or can take a .txt file.
	The txt file has information for each of the maze tiles seperated by newlines. The format is (tiletype,x,z) see testmaze1.txt for an example
	This way we can make a level as a scene then manually add objects in and test it out.
	Future addition will be an easier way of building the txt file for the maze builder.
	