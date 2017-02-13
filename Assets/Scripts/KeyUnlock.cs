using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUnlock : MonoBehaviour {

	public GameObject barrier;

	void OnTriggerEnter(Collider other) {
		//If the player enters the key's collider then disable the barrier
		if (other.gameObject.CompareTag ("Player")) {
			this.gameObject.SetActive (false);
			Collider bar = barrier.GetComponent (typeof(Collider)) as Collider;
			bar.enabled = false;
		}
	}

}
