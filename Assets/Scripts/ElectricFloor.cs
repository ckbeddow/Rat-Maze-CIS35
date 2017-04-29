using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricFloor : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void OnTriggerEnter(Collider other) {
		player = GameObject.FindWithTag ("Player");
		player.SendMessage ("Die");
	}
}
