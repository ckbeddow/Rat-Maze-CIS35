using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

	public bool cameraControllEnabled = true;
	public bool lockCursor;
	public float mouseSensitivity = 2.5f;
	public Transform target;
	public float dstFromTarget = 2;
	public Vector2 pitchMinMax = new Vector2 (10, 85);

	private float mouseX;
	private float mouseY;
	public bool mobileControls = true; //true for now until we determine how to detect
	[SerializeField] VirtualJoystick control;

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
				//yaw += mouseX * mouseSensitivity;
				//pitch -= mouseY * mouseSensitivity;
				yaw += control.InputDirection.x * mouseSensitivity;
				pitch = 25;
				//pitch -= control.InputDirection.z * mouseSensitivity;
			
			} else {
				yaw += Input.GetAxis ("Mouse X") * mouseSensitivity;
				pitch -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
			}
			pitch = Mathf.Clamp (pitch, pitchMinMax.x, pitchMinMax.y);



			currentRotation = Vector3.SmoothDamp (currentRotation, new Vector3 (pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
			transform.eulerAngles = currentRotation;
			



			Vector3 cameraPos = target.position - transform.forward * dstFromTarget;

			// the camera wants to be behind the character

			CompensateForWalls (target.position, ref cameraPos);
			transform.position = cameraPos;

		}
	}

	private void CompensateForWalls( Vector3 fromObject, ref Vector3 toTarget) {
		RaycastHit wallHit = new RaycastHit ();
		if (Physics.Linecast(fromObject, toTarget, out wallHit)){
			toTarget = new Vector3 (wallHit.point.x, toTarget.y, wallHit.point.z);
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
