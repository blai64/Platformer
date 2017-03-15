﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
	// Script
	public static PlayerBehavior instance; 


	// Public gameobjects
	public GameObject Teleporter; 
	public GameObject teleportAura;
	public GameObject trajectoryBallPrefab;


	// Components
	private Rigidbody body;
	private Rigidbody teleporterBody;
	private Animator anim;
	private TeleporterBehavior tb;

	// Textbox
	public GameObject text;

	// Animation state variables
	private bool isLeft = false;
	private bool isGrounded = true;
	private float prevHeight;
	private bool canMove = true;
	private bool enableMove = false;
	public float AnimTimer = 2f;
	private float animTimer;

	// Physics variables
	public float JumpForce = 1.0f;
	public float WalkingSpeed = 0.05f;
	private float jumpForce;
	private float speed;

	// Throwing variables
	private bool attached = true;
	private float xThrowMagnitude;
	private float yThrowMagnitude;
	private float xTeleportVector;
	private float yTeleportVector;
	private float startX = 728f;
	private float startY = 143f;
	private float endX;
	private float endY;
	private int direction = 1;
	public int teleportCharges = 3;
	private bool teleporting = false;
	private List<GameObject> trajectoryBallList = new List<GameObject> ();
	private bool listFilled = false;
	private bool projected = false;


	void Awake()
	{
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy (gameObject); 
	}

	void Start () {
		body = GetComponent<Rigidbody> ();
		teleporterBody = Teleporter.GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		tb = Teleporter.GetComponent<TeleporterBehavior> ();

		prevHeight = body.position.y;

		jumpForce = JumpForce;
		speed = WalkingSpeed;
		animTimer = AnimTimer;
	}
		
	void Update () {
		if (Input.anyKey)
			UIFade.instance.Fade (false);

		if (canMove) {
			InputManager ();
		} else {
			if (enableMove) {
				EnableMove ();
			}
		}
	}

	/**************************** ACCESS METHODS ****************************/
	public int GetDirection() {
		return direction;
	}

	public bool IsTeleporting(){
		return teleporting;
	}

	public void StopTeleporting(){
		teleporting = false;
		tb.Disappear ();
		attached = true;
	}

	public float GetXDistance(){
		return xTeleportVector;
	}

	public float GetYDistance(){
		return yTeleportVector;
	}

	public float GetEndX(){
		return teleporterBody.transform.position.x;
	}

	/**************************** INPUT MANAGER ****************************/

	void InputManager() {
		// Jumping
		if (Input.GetKey (KeyCode.UpArrow)) {
			Jump ();
		}

		// Change Direction / Move / Walk
		if (Input.GetKey (KeyCode.LeftArrow)) {
			ChangeDirection (true);
			Move ();
			Walk (true);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			ChangeDirection (false);
			Move ();
			Walk (true);
		}

		// Stop Walking
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			Walk (false);
		} else if (Input.GetKeyUp (KeyCode.RightArrow)) {
			Walk (false);
		}

		// Falling
		Fall ();

		if (Input.GetKeyDown (KeyCode.Space) && !attached && TeleporterBehavior.instance.isGrounded) {
			Teleport ();
		}

		// While holding the mouse down, displays trajectory of throw
		if (Input.GetMouseButtonDown (0) && attached) {
			projected = true;
		}
		if(projected)
			DisplayThrowTrajectory ();

		if (Input.GetMouseButtonUp (0) && attached) {
			ThrowTeleporter ();
			DestroyProjectedPath ();
		}

		/** TEST FUNCTIONS REMOVE LATER **/
		if (Input.GetKeyDown ("d")) {
			Die ();
		} else if (Input.GetKeyDown ("h")) {
			Happy ();
		} else if (Input.GetKeyDown ("t") && attached) {
			ThrowSingle (500f);
		} else if (Input.GetKeyDown (KeyCode.LeftShift)) {
			Pull (true);
		} else if (Input.GetKeyDown (KeyCode.RightShift)) {
			Pull (false);
		}
	}

	/**************************** TELEPORTER CODE ****************************/

	//resets the position of the teleport aura 
	void AuraReset(){
		teleportAura.SetActive (true);
		teleportAura.transform.position = this.gameObject.transform.position;
		this.gameObject.SetActive (false);
	}

	// teleporting to the teleporter sphere
	void Teleport() {
		if (teleportCharges > 0) {
			AuraReset ();
			teleporting = true;
			xTeleportVector = teleporterBody.transform.position.x - transform.position.x;
			yTeleportVector = teleporterBody.transform.position.y - transform.position.y;
			transform.position = new Vector3 (teleporterBody.transform.position.x,
				teleporterBody.transform.position.y + .6f,
				transform.position.z);
			teleportCharges -= 1;
			TeleporterBehavior.instance.isGrounded = false;
			// TODO: Particle System + Animation for Teleportation?
		}
	}

	// throw the teleporter
	void ThrowTeleporter() {
		endX = Input.mousePosition.x;
		endY = Input.mousePosition.y;
		//fails to throw if you try to throw in the direction you aren't facing
		if ((!isLeft && endX > startX) || (isLeft && endX < startX)) {
			xThrowMagnitude = (endX - startX) * direction;
			yThrowMagnitude = endY - startY;
			Throw (new Vector3 (xThrowMagnitude / 2f * direction, yThrowMagnitude / 2f, 0));
		}
	}

	// Displays throw trajectory
	void DisplayThrowTrajectory(){
		projected = true;
		float xValue;
		float yValue;
		Vector3 initialVelocity = new Vector3(((Input.mousePosition.x - startX) / 2) / teleporterBody.mass * Time.fixedDeltaTime,
											   (Input.mousePosition.y - startY) / 2 / teleporterBody.mass * Time.fixedDeltaTime,
										   	   0);
		// fills list with 10 trajectory balls
		if (!listFilled) {
			for (int x = 0; x < 20; x++)
				trajectoryBallList.Add (Instantiate (trajectoryBallPrefab));
			listFilled = true;
		}
		// places balls on path of projected trajectory
		for (int i = 0; i < 20; i++) {
			xValue = Teleporter.transform.position.x + (initialVelocity.x * (i *.1f));
			yValue = Teleporter.transform.position.y + (initialVelocity.y * (i * .1f) + (-4.9f)*((i *.1f)*(i *.1f)));
			if(!isLeft)
				trajectoryBallList [i].transform.position = new Vector3 (xValue + 1, yValue + 1, 0);
			else
				trajectoryBallList [i].transform.position = new Vector3 (xValue - .3f , yValue + .5f, 0);
		}
	}
		
	// clears the trajectory projection
	void DestroyProjectedPath(){
		projected = false;
		listFilled = false;
		foreach (GameObject ball in trajectoryBallList) {
			Destroy (ball);
		}
		trajectoryBallList = new List<GameObject> ();

	}


	//pick up teleporter when you collide with it
	public void pickUp(){
		attached = true;
		tb.Disappear ();
		TeleporterBehavior.instance.isGrounded = false;
	}

	/**************************** COLLISION EVENTS ****************************/

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Ground") {
			Land ();
		}
		else if (col.gameObject.CompareTag("Killer")){
			Die();
		}
	}

	void OnCollisionExit(Collision col) {
		if (col.gameObject.tag == "Ground") {
			isGrounded = false;
		}
	}

	//If player is colliding and presses button, then change switch
	void OnTriggerStay (Collider other){
		if (other.gameObject.CompareTag ("Switch") && 
			Input.GetKeyDown (KeyCode.L)) {
			Debug.Log ("should animate");
			if (other.gameObject.GetComponent<SwitchScript>().IsActive)
				Pull(false);
			else 
				Pull(true);
		}

	}

	/**************************** ANIMATION EVENTS ****************************/

	private void Jump() {
		anim.SetTrigger ("isJumping");
		if (isGrounded) {
			body.AddForce (Vector3.up * jumpForce);
			isGrounded = false;
		}
	}

	private void Fall() {
		if (!isGrounded) {
			float currHeight = body.position.y;
			if (currHeight - prevHeight < 0f) {
				anim.SetBool ("isFalling", true);
				anim.SetBool ("isJumping", false);
			}
			prevHeight = currHeight;
		}
	}

	private void Land() {
		isGrounded = true;
		anim.SetBool ("isFalling", false);
	}

	private void ChangeDirection(bool left) {
		isLeft = left;
		anim.SetBool ("isLeft", left);
		direction *= -1;
	}

	private void Walk(bool on) {
		anim.SetBool ("isWalking", on);
	}

	private void Move() {
		if (isLeft) {
			direction = -1;
		} else {
			direction = 1;
		}
		transform.Translate (speed * direction, 0, 0);
	}

	private void EnableMove() {
		animTimer -= Time.deltaTime;
		if (animTimer < 0f) {
			animTimer = AnimTimer;
			canMove = true;
			enableMove = false;
		}

	}

	private void Throw(Vector3 force) {
		attached = false;
		anim.SetTrigger ("isThrowing");
		tb.SetForce (force);
	}

	private void ThrowSingle(float force) {
		attached = false;
		anim.SetTrigger ("isThrowing");
		tb.SetSingleForce (force);
	}

	private void Pull(bool left) {
		if (left) {
			anim.SetTrigger ("isPullingLeft");
		} else {
			anim.SetTrigger ("isPullingRight");
		}
		canMove = false;
		enableMove = true;
	}

	private void Die() {
		anim.SetTrigger ("isDead");
		canMove = false;
		MainCamera.instance.transform.Find ("FadeOut").gameObject.GetComponent<Fade> ().FadeInOut (false);
	}

	private void Happy() {
		anim.SetTrigger ("isHappy");
		canMove = false;
		enableMove = true;
	}

	private void Leave() {
		anim.SetTrigger ("isLeaving");
		canMove = false;
	}
}