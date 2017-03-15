/// <summary>
/// Main menu.
/// attaches to Main Camera
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public Texture background;
	public float optButHeight = 0.5f;

	void OnGUI(){
//display background
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
//add buttons
		if (GUI.Button (new Rect (Screen.width * 0.4f, Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.075f), "Play RatMaze")) {
			ChangeToScene (1);
		}

		if (GUI.Button (new Rect (Screen.width * 0.45f, Screen.height * optButHeight, Screen.width * 0.1f, Screen.height * 0.075f), "Options")) {
			print ("add code for Options");
		}
	}
	public void ChangeToScene(int sceneToChangeTo) {
		SceneManager.LoadScene (sceneToChangeTo);
	}
}
