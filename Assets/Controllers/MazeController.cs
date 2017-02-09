using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//Unused ATM
//Will server the funtion of controlling the different parts of the maze
//i.e. hazards and such
public class MazeController : MonoBehaviour {

	private GameObject[] walls;
	private Maze myMaze;
	public GameObject start;
	public int dimensions;
	public string mazeStr;
	public TextAsset mazeTxt;

	// Use this for initialization
	void Start () {
		myMaze = new Maze ();
		walls = Resources.LoadAll<GameObject> ("Halls");

		myMaze.GenerateSimple(dimensions, mazeStr);
		//myMaze.GenerateFromTxt(mazeTxt);
		SpawnMaze (myMaze);

	}

	void SpawnRandomWall(){
		for (int x = 0; x < 3; x++) {
			for (int z = 0; z < 3; z++) {
				int whichWall = UnityEngine.Random.Range (0, 16);

				GameObject myWall = Instantiate (walls [whichWall]) as GameObject;

				myWall.transform.position = new Vector3 (x * 5, 0, z * 5);
			}
		}
	}

	void SpawnMaze(Maze myMaze){
		foreach (Tile tile in myMaze.tiles) {
			GameObject myWall = Instantiate (walls [tile.type]) as GameObject;
			myWall.transform.position = new Vector3 (tile.x * 5, tile.y, tile.z * 5);
		}
	}
	void TestSpawn(int wall){
		GameObject myWall = Instantiate (walls [wall]) as GameObject;
	}
}
