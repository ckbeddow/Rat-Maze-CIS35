using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour {

	private float horizontal = 0.0f;
	private float vertical = 0.0f;

	// Use this for initialization
	void Start () {
		
	}

	public float getHorizontal() {
		return horizontal;
	}

	public float getVertical() {
		return vertical;
	}

	public void MoveUp(){
		vertical = 1.0f;
	}

	public void MoveDown(){
		vertical = -1.0f;
	}

	public void MoveLeft(){
		horizontal = -1.0f;
	}

	public void MoveRight(){
		horizontal = 1.0f;
	}

	public void StopHorizontal(){
		horizontal = 0.0f;
	}

	public void StopVertical(){
		vertical = 0.0f;
	}
}
