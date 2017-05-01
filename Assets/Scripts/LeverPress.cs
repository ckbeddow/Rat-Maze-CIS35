using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPress : MonoBehaviour {

	public GameObject floor;
	private float time = 1.5f;
	public int distance = 0;

	void OnTriggerEnter(Collider other) {
		//If the player hits the button turn off the floor
		if (other.gameObject.CompareTag ("Player") && floor.activeInHierarchy) {
			StartCoroutine (ShutOff ());
		}		
	}

	IEnumerator ShutOff() {
		floor.SetActive (false);
		Debug.Log ("Waiting " + time * distance + " seconds...");
		yield return new WaitForSeconds (time*distance);
		floor.SetActive (true);
	}

}
