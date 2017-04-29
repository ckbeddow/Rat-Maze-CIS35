using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ElectricityGenerator : MonoBehaviour {

	private int _floorX = 0;
	private int _floorY = 0;
	private int _leverX= 0;
	private int _leverY = 0;

	public int dimensions;
	public Node[] nodes;
	public Node[] solution;

	// Solve called in MazeBuilder Script
	public void Solve(int dim, String mazeStr) {
		dimensions = dim;
		nodes = new Node[dimensions * dimensions];
		int[] maze = Array.ConvertAll (mazeStr.Split (','), int.Parse);
		for (int i = 0; i < nodes.Length; i++) {
			nodes [i] = new Node (i, maze [i]);
		}
		BreadthFirstSearch ();
		BestRoute ();
	}

	public void BreadthFirstSearch() {
		Queue<Node> queue = new Queue<Node> ();
		nodes [0].seen = true;
		nodes [0].distance = 0;
		queue.Enqueue (nodes [0]);
		while (queue.Count != 0) {
			Node temp = queue.Dequeue ();
			AdjacencyList (temp.index);
			while (temp.adjacent.Count != 0) {
				Node adj = temp.adjacent.Pop(); 
				if (!adj.seen) {
					adj.seen = true;
					adj.distance = temp.distance + 1;
					adj.parent = temp;
					queue.Enqueue (adj);
				}
			}
		}
	}
		
	public void BestRoute() {
		Stack<Node> path= new Stack<Node> ();
		string output = "";
		path.Push (nodes[nodes.Length-1]);
		solution = new Node[path.Peek().distance+1];
		while (path.Peek ().parent != null)
			path.Push (path.Peek ().parent);
		while (path.Count != 0) {
			Node i = nodes[path.Pop ().index];
			solution [i.distance] = i;
			output = output + "(" + i.index % dimensions + ", " + i.index / dimensions + ") ";
		}
		Debug.Log(output);
	}

	//Selects a random node in solution some distance away from start and before the end
	//Currently set to be a least 1/3 of the distance away from start.
	public void GenerateObstacle() {
		Node random = solution[UnityEngine.Random.Range (solution.Length/3, solution.Length-1)];
		_floorX = random.index % dimensions;
		_floorY = random.index / dimensions;
		Debug.Log ("Floor at (" + _floorX + ", " + _floorY + ")");
		//GenerateLever ();
	}
		
	public void GenerateLever() {
		// Willl have to then select a dead-end in same set as "start" for the lever
		_leverX = 0;
		_leverY = 0;
	}

	// Not done
	public int[] getFloor() {
		int [] coordinates = new int[2];
		coordinates[0] = _floorX * 5;
		coordinates[1] = -_floorY * 5;
		return coordinates;
	}
		
	public int[] getLever() {
		int [] coordinates = new int[2];
		coordinates[0] = _leverX;
		coordinates[1] = _leverY;
		return coordinates;
	}

	public void AdjacencyList(int i) {
		// Right
		if (!GetBit (1, nodes [i].type) && (i + 1) % dimensions != 0) {
			nodes [i].adjacent.Push (nodes [i + 1]);
		}
		// Down
		if(!GetBit(2, nodes[i].type) && i+dimensions < nodes.Length)
			nodes[i].adjacent.Push(nodes[i+dimensions]);
		// Left
		if(!GetBit(4, nodes[i].type) && i%dimensions != 0)
			nodes[i].adjacent.Push(nodes[i-1]);
		// UP
		if(!GetBit(8, nodes[i].type) && i-dimensions >= 0)
			nodes[i].adjacent.Push(nodes[i-dimensions]);
	}

	private bool GetBit(int number, int value) {
		return ((number & value) == number);
	}
		
}

// Helper class for maze solving
public class Node {

	public Node (int i, int key) {
		this.index = i;
		this.type = key;
		this.parent = null;
		this.distance = -1;
		this.seen = false;
		this.adjacent = new Stack<Node>();
	}

	public int index;
	public int type;
	public Node parent;
	public int distance;
	public bool seen;
	public Stack<Node> adjacent;

}



//IN THE CONTROLLER(S)
/**
	BFS obsgen;
	obsgen.Solve(dim,  mazeStr);
	obsgen.generateObstacle();

	instantiate electricfloor Attribute obsgen.GetFloor()[0], obsgen.GetFloor()[1];
	instantiate electriclever Attribute obsgen.GetFloor()[0], obsgen.GetFloor()[1];
*/