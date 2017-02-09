using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct Tile {
	//helper object for the maze class
	//contains tile type and location
	public Tile (int myType, int myX, int myZ) {
		type = myType;
		x = myX;
		z = myZ;
		y = 0;
	}

	public int type;
	// int from range 0-16
	// binary wall placement NWSE example. 0000 no walls, 0110 west wall and south wall
	public int y; //currently only one level mazes so y is default set to 0
	public int x;
	public int z;
}

public class Maze {

	public List<Tile> tiles;

	public void GenerateSimple(int dim, string tilelist){
		tiles = new List<Tile>();
		//Builds a square maze dimXdim from a comma seperated string
		int[] maze = Array.ConvertAll (tilelist.Split (','), int.Parse);
		int i = 0;
		for (int z = 0; z > -dim; z--) {
			for (int x = 0; x < dim; x++) {
				Tile myTile = new Tile (maze [i], x, z);
				i++;
				tiles.Add (myTile);
			}
		}
		Tile endTile = new Tile (16, dim, 1 - dim);
		tiles.Add (endTile);
		Tile startTile = new Tile (14, -1, 0);
		tiles.Add (startTile);
	}

	public void GenerateFromTxt(TextAsset mazeTxt){
		tiles = new List<Tile>();
		//builds a maze from a text file of tiles
		//each tiles is seperated by a newline
		//each element is seperated by a comma
		//example : 	type, x, z
		//				type, x, z

		string mazeStr = mazeTxt.text;
		List<string> eachTile = new List<string> ();
		eachTile.AddRange (mazeStr.Split ("\n" [0]));

		foreach (var item in eachTile) {
			int[] temp = Array.ConvertAll (item.Split (','), int.Parse);
			tiles.Add(new Tile(temp[0],temp[1],temp[2]));
		}
	}
}
