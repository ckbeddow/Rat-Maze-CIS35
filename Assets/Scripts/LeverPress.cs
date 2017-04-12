using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPress : MonoBehaviour {

	public GameObject player;
	public GameObject floor;
	public float time = 5.0f;

	void OnTriggerEnter(Collider other) {
		//If the player hits the button turn off the floor
		if (other.gameObject.CompareTag ("Player")) {
			StartCoroutine(ShutOff());
		}		
	}

	IEnumerator ShutOff() {
		floor.SetActive (false);
		yield return new WaitForSeconds (time);
		floor.SetActive (true);
	}
		
}
