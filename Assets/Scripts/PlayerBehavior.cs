using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
	// Script
	public static PlayerBehavior instance; 


	// Public gameobjects
	public GameObject Teleporter; 
	public GameObject throwVectorArrow;

	// Components
	private Rigidbody body;
	private Rigidbody teleporterBody;
	private Animator anim;
	private TeleporterBehavior tb;

	// Crystals
	public GameObject crystal;

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
	private float startX;
	private float startY;
	private float endX;
	private float endY;
	private int direction = 1;
	public int teleportCharges = 3;


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

		if (canMove) {
			InputManager ();
		} else {
			if (enableMove) {
				EnableMove ();
			}
		}
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

		if (Input.GetMouseButtonDown (0) && attached) {
			
		}

		if (Input.GetMouseButtonUp (0) && attached) {
			ThrowTeleporter ();
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

	// teleporting to the teleporter sphere
	void Teleport() {
		if (teleportCharges > 0) {
			xTeleportVector = teleporterBody.transform.position.x - transform.position.x;
			yTeleportVector = teleporterBody.transform.position.y - transform.position.y;
			transform.position = new Vector3 (teleporterBody.transform.position.x,
				teleporterBody.transform.position.y + .6f,
				transform.position.z);
			body.velocity = teleporterBody.velocity;

			tb.Disappear ();
			attached = true;
			teleportCharges -= 1;
			TeleporterBehavior.instance.isGrounded = false;

			// TODO: Particle System + Animation for Teleportation?
		}
	}

	// throw the teleporter
	void ThrowTeleporter() {
		startX = 571f;
		startY = 143f;
		endX = Input.mousePosition.x;
		endY = Input.mousePosition.y;
		//fails to throw if you try to throw in the direction you aren't facing
		if ((!isLeft && endX > startX) || (isLeft && endX < startX)) {
			xThrowMagnitude = (endX - startX) * direction;
			yThrowMagnitude = endY - startY;
			if (xThrowMagnitude > 100)
				xThrowMagnitude = 100;
			if (yThrowMagnitude > 100)
				yThrowMagnitude = 100;
			Throw (new Vector3 (xThrowMagnitude * 5f * direction, yThrowMagnitude * 5f, 0));
		}
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