using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ElectricityGenerator : MonoBehaviour {

	private int _floorPositionX = 0;
	private int _floorPositionZ = 0;
	private int _floorAngleY = 0;

	private int _leverX= 0;
	private int _leverY = 0;

	public int dimensions;
	List<Node> nodes;
	public Node[] solution;

	// Solve called in MazeBuilder Script
	public void Solve(int dim, String mazeStr) {
		dimensions = dim;
		nodes = new List<Node> ();
		int[] maze = Array.ConvertAll (mazeStr.Split (','), int.Parse);
		for (int i = 0; i < maze.Length; i++) {
			nodes.Add(new Node (i, maze [i]));
		}
		BreadthFirstSearch (nodes);
		solution = BestRoute ();
	}

	public void BreadthFirstSearch(List<Node> nodes) {
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
					adj.parent.children.Add (adj);
					queue.Enqueue (adj);
				}
			}
			Debug.Log ("Index: "+temp.index+" Parent: "+temp.parent);
			for (int i = 0; i < temp.children.Count; i++)
				Debug.Log ("Child at " + temp.children [i].index);
		}
	}
		
	public Node[] BestRoute() {
		Stack<Node> path= new Stack<Node> ();
		path.Push (nodes[nodes.Count-1]);
		Node[] route = new Node[path.Peek().distance+1];
		while (path.Peek ().parent != null)
			path.Push (path.Peek ().parent);
		while (path.Count != 0)
			route [path.Peek().distance] = path.Pop();
		return route;
	}

	//Selects a random node in solution some distance away from start and before the end
	public bool GenerateObstacle() {
		List<Node> legalObs = new List<Node> ();
		//Currently set to be at least a distance of "dimensions" away from start
		for (int i = dimensions-1; i < solution.Length; i++) {
			if (UpWall (solution[i].type) && DownWall (solution[i].type))
				legalObs.Add(solution[i]);
			else if (RightWall (solution[i].type) && LeftWall (solution[i].type))
				legalObs.Add(solution[i]);
		}
		Debug.Log (legalObs.Count);
		for (int i = 0; i < legalObs.Count; i++)
			Debug.Log ("Legal floor at: " + legalObs [i].index);
		if (legalObs.Count == 0)
			return false; //there were no legal electric floors
		Node random = legalObs[(UnityEngine.Random.Range (0, legalObs.Count-1))];
		_floorPositionX = random.index % dimensions;
		_floorPositionZ = random.index / dimensions;
		if (UpWall (random.type) && DownWall (random.type))
			_floorAngleY = 90;
		Debug.Log ("Floor at (" + _floorPositionX + ", " + _floorPositionZ + ")");
		return true;
	}
		
	public void GenerateLever() {
		// Willl have to then select a dead-end in same set as "start" for the lever
		_leverX = 0;
		_leverY = 0;
	}

	// Not done
	public int[] getFloor() {
		int [] coordinates = new int[3];
		coordinates[0] = _floorPositionX * 5;
		coordinates[1] = -_floorPositionZ * 5;
		coordinates [2] = _floorAngleY;
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
		if (!RightWall(nodes [i].type) && (i+1)%dimensions != 0) {
			nodes [i].adjacent.Push (nodes [i + 1]);
		}
		// Down
		if(!DownWall(nodes[i].type) && i+dimensions < nodes.Count)
			nodes[i].adjacent.Push(nodes[i+dimensions]);
		// Left
		if(!LeftWall(nodes[i].type) && i%dimensions != 0)
			nodes[i].adjacent.Push(nodes[i-1]);
		// UP
		if(!UpWall(nodes[i].type) && i-dimensions >= 0)
			nodes[i].adjacent.Push(nodes[i-dimensions]);
	}
					
	private bool RightWall(int value) {
		return ((1 & value) == 1);
	}

	private bool DownWall(int value) {
		return ((2 & value) == 2);
	}

	private bool LeftWall(int value) {
		return ((4 & value) == 4);
	}

	private bool UpWall(int value) {
		return ((8 & value) == 8);
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
		this.children = new List<Node> ();
	}

	public int index;
	public int type;
	public Node parent;
	public int distance;
	public bool seen;
	public Stack<Node> adjacent;
	public List<Node> children;

}