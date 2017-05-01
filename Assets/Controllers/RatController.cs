using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatController : MonoBehaviour {

	public bool alwaysRun = false;
	public bool playedScamper = false;
	public bool playedSqueak = false;
	public float walkSpeed = 2;
	public float runSpeed = 6;
	public float gravity = -9.8f;

	public bool enabledControls = true; //used to stop control of rat during menues and splash screen [reduntant on mobile]

	//add audio source and cllips
	[SerializeField] AudioSource ratNoise;
	[SerializeField] AudioClip scamper;
	[SerializeField] AudioClip squeak;
	[SerializeField] AudioClip zap;
	[SerializeField] AudioClip squeal;
	[SerializeField] AudioClip success;



	//public ButtonControl controls;
	[SerializeField] VirtualJoystick controls;

	//from Steve's MoveMe.cs
	private float horizontal;
	private float vertical;
	public bool mobileControls = true; //true for now until we determine how to detect

	public float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;
	float velocityY;

	public Text countText;
	public Text winText;
	public EndlessWorldController world;
	int count;


	CharacterController controller;
	Animator animator;
	Transform cameraT;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		cameraT = Camera.main.transform;
		controller = GetComponent<CharacterController> ();

		//from Steve's 
		horizontal = 0.0f;
		vertical = 0.0f;

		count = 0;
		SetCountText ();
		//winText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 input;
		if (enabledControls) {
			if (mobileControls) {
				//horizontal = controls.getHorizontal ();
				//vertical = controls.getVertical ();
				horizontal = controls.InputDirection.x;
				vertical = controls.InputDirection.z;
				input = new Vector2 (horizontal, vertical);
				//get horr and get vert
			} else {
				input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
			}
		} else {
			input = new Vector2 (0, 0);
		}

		Vector2 inputDir = input.normalized;

		if (inputDir != Vector2.zero) {
			float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
			playedSqueak = false;
			if (playedScamper != true) {
				ratNoise.PlayOneShot(scamper);
				playedScamper = true;
			}
		} else {
			playedScamper = false;
			if (playedSqueak != true){
				ratNoise.PlayOneShot(squeak);
				playedSqueak = true;
			}
		}

		bool running = false;
		if (!alwaysRun) {
			running = Input.GetKey (KeyCode.LeftShift);
		} else {
			running = !Input.GetKey (KeyCode.LeftShift);
		}
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
			Debug.Log ("pickup");
			world.onPickup ();
			SetCountText ();
		}
	}

	void SetCountText(){
		countText.text = count.ToString ();
		if (count >= 1) {
			winText.text = "You Win!";
			ratNoise.PlayOneShot (success);
		}
	}

	void Die() {
		ratNoise.PlayOneShot (zap);
		enabledControls = false;
		animator.SetBool ("isDead", true);
		enabledControls = false;

		//winText.text = "Zap! You died!";
	}

	public void MoveUp(){
		vertical = 1.0f;
	}

	public void MoveDown(){
		vertical = -1.0f;
	}

	public void MoveLeft(){
		horizontal = -1.0f;
	}

	public void MoveRight(){
		horizontal = 1.0f;
	}

	public void StopHorizontal(){
		horizontal = 0.0f;
	}

	public void StopVertical(){
		vertical = 0.0f;
	}
}
