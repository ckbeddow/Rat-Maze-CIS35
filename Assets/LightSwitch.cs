using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour {

	// Use this for initialization
//	public Material greenMat;
//	public Material redMat;
//
//	Color32 redColor;
//	Color32 greenColor;
//
//
//
//	void Start () {
//		redColor = new Color32 (178, 21, 21, 255);
//		greenColor = new Color32 (21, 178, 35, 255);
//	}
//
	[SerializeField] GameObject greenbulb;
	[SerializeField] GameObject redbulb;
	[SerializeField] GameObject greenlight;
	[SerializeField] GameObject redlight;

	public void Safe(bool safe){
		
		redbulb.SetActive (!safe);
		redlight.SetActive (!safe);
		greenbulb.SetActive (safe);
		greenlight.SetActive (safe);

	
	}


}
