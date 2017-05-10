using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessWorldController : MonoBehaviour {

	public int size;
	public MazeBuilderScript builder;
	public GameObject player;
	public ThirdPersonCamera cameraControls;
	private RandomMazeGenerator gen ;
	public RatController playerControls;
	public GameObject nextLevelPanel;

	// Use this for initialization
	void Start () {
		size = 3;
		gen = new RandomMazeGenerator (size);
		gen.build ();
		builder.BuildMaze (size, gen.print());

		cameraControls.cameraControllEnabled = false;
		playerControls.enabledControls = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void newLevel ()
	{
		Destroy (GameObject.FindWithTag ("maze"));
		Destroy (GameObject.FindWithTag ("ElectricFloor"));
		Debug.Log ("Destroyed Floor");
		if(playerControls.count < 3 || playerControls.count%3==0) 
			size++;
		//player.transform.position = (new Vector3 (-4, 0, 0));
		gen = new RandomMazeGenerator (size);
		gen.build ();
		builder.BuildMaze (size, gen.print ());
		Debug.Log ("spawning new level");
	}

	public void onCloseSplashScreen(){
		cameraControls.cameraControllEnabled = true;
		playerControls.enabledControls = true;
	}

	public void onOpenSplashScreen() {
		cameraControls.cameraControllEnabled = false;
		playerControls.enabledControls = false;
	}

	public void onPickup(){
		onOpenSplashScreen();
		nextLevelPanel.SetActive (true);
		player.transform.position = (new Vector3 (-4, 0, 0));

	}

	public void nextLevel(){
		Debug.Log ("next button pushed");
		nextLevelPanel.SetActive (false);
		newLevel ();
		onCloseSplashScreen ();
	}

	public void reset(){
		player.transform.position = (new Vector3 (-4, 0, 0));
		player.SendMessage ("Reset");
	}
	public void backToMainMenu(){

	}
}
