using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPress : MonoBehaviour {

	private GameObject floor;
	private float time;

	void OnTriggerEnter(Collider other) {
		//If the player hits the button turn off the floor
		floor = GameObject.FindWithTag("Electricity");
		if (other.gameObject.CompareTag ("Player")) {
			StartCoroutine(ShutOff());
		}		
	}

	IEnumerator ShutOff() {
		time = 5;
		floor.SetActive (false);
		yield return new WaitForSeconds (time);
		floor.SetActive (true);
	}
		
}
