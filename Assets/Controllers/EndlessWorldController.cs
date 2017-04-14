using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessWorldController : MonoBehaviour {

	public int size;
	public MazeBuilderScript builder;
	public GameObject player;
	private RandomMazeGenerator gen ;

	// Use this for initialization
	void Start () {
		gen = new RandomMazeGenerator (size);
		gen.build ();
		builder.BuildMaze (size, gen.print());
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void newLevel ()
	{
		Destroy (GameObject.FindWithTag ("maze"));
		player.transform.position = (new Vector3 (-4, 0, 0));
		gen = new RandomMazeGenerator (size);
		gen.build ();
		builder.BuildMaze (size, gen.print ());
		Debug.Log ("spawning new level");
	}
}
