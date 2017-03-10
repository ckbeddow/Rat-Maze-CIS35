using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatController : MonoBehaviour {

	public float walkSpeed = 2;
	public float runSpeed = 6;
	public float gravity = -9.8f;


	public float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;
	float velocityY;

	public Text countText;
	public Text winText;
	int count;

	CharacterController controller;
	Animator animator;
	Transform cameraT;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		cameraT = Camera.main.transform;
		controller = GetComponent<CharacterController> ();

		count = 0;
		SetCountText ();
		winText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		Vector2 inputDir = input.normalized;

		if (inputDir != Vector2.zero) {
			float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime) ;
		}

		bool running = Input.GetKey (KeyCode.LeftShift);
		float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

		velocityY += Time.deltaTime * gravity;
		Vector3 velocity = transform.forward * currentSpeed;

		controller.Move (velocity * Time.deltaTime + Vector3.up * velocityY);
		currentSpeed = new Vector2 (controller.velocity.x, controller.velocity.z).magnitude;

		if (controller.isGrounded) {
			velocityY = 0;
		}

		float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
		animator.SetFloat ("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
	}

	void SetCountText(){
		countText.text = "Find the Cheese";
		if (count >= 1) {
			winText.text = "You Win!";
		}
	}
}
