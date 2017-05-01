using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ElectricityGenerator : MonoBehaviour {

	private int _floorPositionX = 0;
	private int _floorPositionZ = 0;
	private int _floorAngleY = 0;

	private int _leverPositionX= 0;
	private int _leverPositionZ = 0;
	private int _leverAnglyY = 0;

	public int dimensions;
	List<Node> nodes;
	public Node[] solution;
	public int obstacleDistance;

	// Solve called in MazeBuilder Script
	public void Solve(int dim, String mazeStr) {
		dimensions = dim;
		nodes = new List<Node> ();
		int[] maze = Array.ConvertAll (mazeStr.Split (','), int.Parse);
		for (int i = 0; i < maze.Length; i++) {
			nodes.Add(new Node (i, maze [i]));
		}
		BreadthFirstSearch ();
		solution = BestRoute (nodes.Count-1);
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
					adj.parent.children.Push (adj);
					queue.Enqueue (adj);
				}
			}
		}
	}

	private void AdjacencyList(int i) {
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
		
	public Node[] BestRoute(int end) {
		Stack<Node> path= new Stack<Node> ();
		path.Push (nodes[end]);
		Node[] route = new Node[path.Peek().distance+1];
		while (path.Peek ().parent != null)
			path.Push (path.Peek ().parent);
		while (path.Count != 0)
			route [path.Peek().distance] = path.Pop();
		return route;
	}
		
	public bool GenerateFloor() {
		Node floor = randomFloor ();
		if (floor == null)
			return false;
		if (!GenerateLever (floor))
			return false;
		_floorPositionX = floor.index % dimensions;
		_floorPositionZ = floor.index / dimensions;
		Debug.Log ("Floor at (" + _floorPositionX + ", " + _floorPositionZ + ")");
		if (UpWall (floor.type) && DownWall (floor.type))
			_floorAngleY = 90;
		else
			_floorAngleY = 0;
		return true;
	}

	public int[] getFloor() {
		int [] coordinates = new int[3];
		coordinates[0] = _floorPositionX * 5;
		coordinates[1] = -_floorPositionZ * 5;
		coordinates [2] = _floorAngleY;
		return coordinates;
	}

	private Node randomFloor() {
		List<Node> legalObs = new List<Node> ();
		for (int i = legalObsStart(); i < solution.Length; i++) {
			if (UpWall (solution[i].type) && DownWall (solution[i].type))
				legalObs.Add(solution[i]);
			else if (RightWall (solution[i].type) && LeftWall (solution[i].type))
				legalObs.Add(solution[i]);
		}
		if (legalObs.Count == 0)
			return null;
		return legalObs[(UnityEngine.Random.Range (0, legalObs.Count-1))];
	}

	private int legalObsStart() {
		for (int i = 0; i < solution.Length; i++)
			if (solution [i].children.Count >= 2)
				return i + 1;
		return solution.Length;
	}

	public bool GenerateLever(Node floor) {
		Node lever = randomLever (floor);
		if (lever == null)
			return false;
		_leverPositionX = lever.index % dimensions;
		_leverPositionZ = lever.index / dimensions;
		Debug.Log ("Floor at (" + _leverPositionX + ", " + _leverPositionZ + ")");
		if (!RightWall (floor.type))
			_floorAngleY = 180;
		else if (!DownWall (floor.type))
			_floorAngleY = 90;
		else if (!LeftWall (floor.type))
			_floorAngleY = 0;
		else if (!UpWall (floor.type))
			_floorAngleY = -90;
		obstacleDistance = lever.obsDistance;
		return true;
	}

	public int[] getLever() {
		int [] coordinates = new int[3];
		coordinates[0] = _leverPositionX * 5;
		coordinates[1] = -_leverPositionZ * 5;
		coordinates [2] = _leverAnglyY;
		return coordinates;
	}

	private Node randomLever(Node floor) {
		List<Node> deadEnds = new List<Node> ();
		Queue<Node> queue = new Queue<Node> ();
		floor.obsSeen = true;
		floor.obsDistance = 0;
		floor.parent.obsDistance = 1;
		queue.Enqueue (floor.parent);
		while(queue.Count != 0) {
			Node temp = queue.Dequeue ();
			temp.obsSeen = true;
			if (temp.parent != null && !temp.parent.obsSeen) {
				queue.Enqueue (temp.parent);
				temp.parent.obsDistance = temp.obsDistance + 1;
			}
			if (temp.children.Count == 0)
				deadEnds.Add(temp);
			while (temp.children.Count != 0) {
				if (temp.children.Peek ().obsSeen)
					temp.children.Pop ();
				else {
					temp.children.Peek ().obsDistance = temp.obsDistance + 1;
					queue.Enqueue (temp.children.Pop ());	
				}
			}
		}
		return deadEnds[(UnityEngine.Random.Range (0, deadEnds.Count-1))];
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
		this.children = new Stack<Node> ();

		this.obsDistance = -1;
		this.obsSeen = false;
		this.obsParent = null;
	}

	public int index;
	public int type;
	public Node parent;
	public int distance;
	public bool seen;

	public Stack<Node> adjacent;
	public Stack<Node> children;

	public Node obsParent;
	public int obsDistance;
	public bool obsSeen;
}