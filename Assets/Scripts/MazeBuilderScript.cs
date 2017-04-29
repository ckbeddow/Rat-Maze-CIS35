using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBuilderScript : MonoBehaviour {

	private GameObject[] walls;
	private Maze myMaze;
	private ElectricityGenerator obsGen;
	public int dimensions;
	public string mazeStr;
	public TextAsset mazeTxt;
	public bool BuildfromTxt;


//	void Start() {
//
//		RandomMazeGenerator gen = new RandomMazeGenerator (dimensions);
//		gen.build();
//		mazeStr = gen.print ();
//		BuildMaze ();
//		SpawnMaze (myMaze );
//	}


	public void BuildMaze () {
		myMaze = new Maze ();
		walls = Resources.LoadAll<GameObject> ("Halls");

		if (BuildfromTxt) {
			myMaze.GenerateFromTxt(mazeTxt);
		}
		else {
			myMaze.GenerateSimple(dimensions, mazeStr);
		}

		SpawnMaze (myMaze);

	}

	public void BuildMaze (int dim, string maze) {
		obsGen = new ElectricityGenerator ();
		myMaze = new Maze ();
		walls = Resources.LoadAll<GameObject> ("Halls");

		myMaze.GenerateSimple(dim, maze);
		SpawnMaze (myMaze);

		obsGen.Solve (dim, maze);
		if(obsGen.GenerateObstacle ());
			SpawnObstacles (obsGen);
	}
		
	void SpawnMaze(Maze myMaze){
		GameObject maze = new GameObject ();
		maze.name = "Maze";
		maze.tag = "maze";
		foreach (Tile tile in myMaze.tiles) {
			GameObject myWall = Instantiate (walls [tile.type]) as GameObject;
			myWall.transform.position = new Vector3 (tile.x * 5, tile.y, tile.z * 5);
			myWall.transform.parent = maze.transform;
		}
	}

	void SpawnObstacles(ElectricityGenerator myObs) {
		GameObject floor = Instantiate (Resources.Load<GameObject> ("Electric Floor")) as GameObject;
		floor.name = "Electric Floor";
		floor.tag = "ElectricFloor";
		floor.transform.position = new Vector3(myObs.getFloor ()[0], 0, myObs.getFloor ()[1]);
		floor.transform.Rotate (0, myObs.getFloor () [2], 0);
		//obs.GenerateLever();
	}
}
