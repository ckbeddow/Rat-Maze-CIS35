using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMazeGenerator {

	private int sidelength;
	private int[] tiles;
	private int size;
	private Djset maze;


	public RandomMazeGenerator(int n){
		sidelength = n;
		size = n * n;
		tiles = new int[size];
		tiles [0] = 11;
		for (int i = 1; i < size -1; i++) {
			tiles [i] = 15;
		}
		tiles [size - 1] = 14;
		maze = new Djset (size);
	}

	public void build() {
		while (maze.getsize () > 1) {
			divide (0, size - 1);
		}
	}

	//Prints a commma deliminated string containing the value of each tile
	public string print(){
		string output = "";
		for (int i = 0; i < size; i++) {
			output = output + tiles[i];
			if(i != size-1) output = output + " ,";

		}
		Debug.Log (output);
		return output;
	}

	private int validNeighbor(int x){
		int[] temp = new int[4];
		temp [0] = up (x);
		temp [1] = down (x);
		temp [2] = left (x);
		temp [3] = right (x);
		int pos = Random.Range (0, 3);
		for (int i = 0; i < 4; i++) {
			pos = pos + 1;
			if (pos > 3) {
				pos = pos - 4;
			}
			if (maze.find (temp [pos]) >= 0 && maze.find (temp [pos]) != maze.find (x)) {
				return temp [pos];
			}
		}
		return -1;
	}

	private void breakWall(int x, int y){
		if (x < 0 || y < 0)
			return;
		if (y == up (x)) {
			tiles [x] = tiles [x] - 8;
			tiles [y] = tiles [y] - 2;
		}
		if (y == down (x)) {
			tiles [x] = tiles [x] - 2;
			tiles [y] = tiles [y] - 8;
		}
		if (y == left (x)) {
			tiles [x] = tiles [x] - 4;
			tiles [y] = tiles [y] - 1;
		}
		if (y == right (x)) {
			tiles [x] = tiles [x] - 1;
			tiles [y] = tiles [y] - 4;
		}
		maze.djunion (x, y);
	}

	private int up(int x) {
		if (x < sidelength)
			return -1;
		else
			return x - sidelength;
	}

	private int down(int x) {
		if (x > (size - sidelength))
			return -1;
		else
			return x + sidelength;
	}

	private int left(int x) {
		if (x % sidelength == 0)
			return -1;
		else
			return x - 1;
	}

	private int right( int x) {
		if ((x + 1) % sidelength == 0)
			return -1;
		else
			return x + 1;
	}

	private void divide(int start, int end){
		int x = Random.Range (0, (end - start) + 1) + start;
		breakWall (x, validNeighbor (x));
		if (end > start) {
			if (x > start)
				divide (start, x - 1);
			if (end > x)
				divide (x + 1, end);
		}
	}

}

public class Djset {

	private int[] parent;
	private int[] rank;
	private int maxsize;
	private int size;

	public Djset(int n){ //constructor with make-set inlcuded
		maxsize = n;
		size = maxsize;
		parent = new int[size];
		rank = new int[size];
		for (int i = 0; i < size; i++){
			parent[i] = i;
			rank[i] = 0;
		}
	}


	public void djunion(int x, int y){
		if ((x > maxsize - 1) || (y > maxsize - 1))
			return;
		else {
			link (locate (x), locate (y));
			size--;
		
		}
	}
	public void link(int x, int y){
		if (x == y)
			return;
		if (rank [x] > rank [y]) {
			parent [y] = x;
		} else {
			parent [x] = y;
			if (rank [x] == rank [y]) {
				rank [y]++;
			}
		}
	}
	public int find(int x){
		if (x > maxsize - 1 || x < 0)
			return -1;
		return locate (x);
	
	}
	public int getsize(){
		return size;
	}


	private int locate(int x){
		if (x != parent [x]) {
			parent [x] = locate (parent [x]);
		}
		return parent [x];
	}



}