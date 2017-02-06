using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MazeController : MonoBehaviour {

	private GameObject[] walls;
	public GameObject start;
	public int dimensions;
	public string mazeStr;

	// Use this for initialization
	void Start () {

		walls = Resources.LoadAll<GameObject> ("Halls");
		//dimensions = 5;
		SpawnMaze (mazeStr, dimensions);
		start.transform.position = new Vector3(-4, 0 , dimensions * 4);
		//TestSpawn(testwall);
		/*
		for (int z = 0; z < 3; z++) {
			for (int x = 0; x < 3; x++) {
				Instantiate (wall, new Vector3 (x * 5, 0, z*5), Quaternion.identity);
			}
		}
		*/
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

	void SpawnMaze(string theMaze, int dim){
		//Spawns a maze from a comma seperated string
		int[] maze = Array.ConvertAll (theMaze.Split (','), int.Parse);
		int i = 0;
		for (int z = dim; z > 0; z--) {
			for (int x = 0; x < dim; x++) {
				int whichWall = maze[i];
				i++;
				GameObject myWall = Instantiate (walls [whichWall]) as GameObject;
				Debug.Log ("Placeing Tile at x = " + x + " z = " + z);
				myWall.transform.position = new Vector3 (x * 4, 0, z * 4);
				if (z == 1 && x == dim - 1) {
					GameObject lastwall = Instantiate (walls [16]) as GameObject;
					lastwall.transform.position = new Vector3 ( (x + 1) * 4, 0, z * 4);
					Debug.Log ("Placing end Tile at x = " + x + " z = " + z);
				}
			}
		}
	}

	void TestSpawn(int wall){
		GameObject myWall = Instantiate (walls [wall]) as GameObject;
	}
}
