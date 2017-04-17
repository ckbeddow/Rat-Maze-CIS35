using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

	public bool cameraControllEnabled = true;
	public bool lockCursor;
	public float mouseSensitivity = 10;
	public Transform target;
	public float dstFromTarget = 2;
	public Vector2 pitchMinMax = new Vector2 (10, 85);

	private float mouseX;
	private float mouseY;
	public bool mobileControls = true; //true for now until we determine how to detect

	public float rotationSmoothTime = .12f;
	Vector3 rotationSmoothVelocity;
	Vector3 currentRotation;


	float yaw;
	float pitch;

	void Start() {
		mouseX = 0.0f;
		mouseY = 0.0f;

		if (lockCursor) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
	void LateUpdate () {
		if (cameraControllEnabled) {
			if (mobileControls) {
				yaw += mouseX * mouseSensitivity;
				pitch -= mouseY * mouseSensitivity;
			
			} else {
				yaw += Input.GetAxis ("Mouse X") * mouseSensitivity;
				pitch -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
			}
			pitch = Mathf.Clamp (pitch, pitchMinMax.x, pitchMinMax.y);
			
			currentRotation = Vector3.SmoothDamp (currentRotation, new Vector3 (pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
			transform.eulerAngles = currentRotation;
			
			transform.position = target.position - transform.forward * dstFromTarget;
		}
	}
	public void CamUp(){
		mouseY = 1.0f;
	}

	public void CamDown(){
		mouseY = -1.0f;
	}

	public void CamLeft(){
		mouseX = -1.0f;
	}

	public void CamRight(){
		mouseX = 1.0f;
	}

	public void StopYaw(){
		mouseX = 0.0f;
	}

	public void StopPitch(){
		mouseY = 0.0f;
	}
}
