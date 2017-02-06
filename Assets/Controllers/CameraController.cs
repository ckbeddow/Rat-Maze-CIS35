using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public bool thirdPerson;
	private Vector3 offset;

	public Transform lookAt;
	public Transform camTransform;

	private Camera cam;

	private float distance = 2.0f;
	private float currentX = 0.0f;
	private float currentY = 0.0f;
	private float sensivityX = 4.0f;
	// Use this for initialization
	void Start () {


		//Third Person Follow Camera
		if (thirdPerson) {
			camTransform = transform;
			cam = Camera.main;
		}
		//Over view tracking camera
		else {
			offset = transform.position - player.transform.position;
		}
	}

	void Update(){

		//Third Person Follow Camera
		if (thirdPerson) {
			currentX += Input.GetAxis ("Mouse X") * sensivityX;
		}
	}
	void LateUpdate () {
		

		//Third Person Follow Camera
		if (thirdPerson) {
			Vector3 dir = new Vector3 (-distance, 1, 0);
			Quaternion rotation = Quaternion.Euler (currentY, currentX, 0);
			camTransform.position = lookAt.position + rotation * dir;
			camTransform.LookAt (lookAt.position);
		}
		//Over View tracking camera
		else {
			transform.position = player.transform.position + offset;
		}
	}
}
