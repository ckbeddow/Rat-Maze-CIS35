using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPress : MonoBehaviour {

	[SerializeField] public AudioSource floorSound;
	[SerializeField] public AudioClip powerUp;
	[SerializeField] public AudioClip powerDown;

	public GameObject floor;
	public float time = 1.5f;
	public int distance = 0;
	Animator animator;
	void Start(){
		animator = GetComponentInChildren<Animator> ();
	}
	void OnTriggerEnter(Collider other) {
		//If the player hits the button turn off the floor
		if (other.gameObject.CompareTag ("Player") && floor.activeInHierarchy) {
			StartCoroutine (ShutOff ());
		}		
	}

	IEnumerator ShutOff() {
		floorSound.PlayOneShot (powerDown);
		floor.SetActive (false);
		animator.SetBool ("up", false);
		Debug.Log ("Waiting " + time * distance + " seconds...");
		yield return new WaitForSeconds (time*distance);
		animator.SetBool ("up", true);
		floorSound.PlayOneShot (powerUp);
		floor.SetActive (true);
	}

}
