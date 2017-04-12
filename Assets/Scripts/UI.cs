using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public Text timeText;
	//public Text winText;
	//public Text goalText;

	// Use this for initialization
	void Start () {
		timeText.text = "00:00";
		//winText.text = "";
	}

	void Update () {
		timeText.text = string.Format("{0:00}:{1:00}", (int) (Time.timeSinceLevelLoad/60), (int) (Time.timeSinceLevelLoad%60));
	}
		
}
