using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

	// Public gameobjects
	public GameObject Teleporter; 
	public GameObject throwVectorArrow;

	// Components
	private Rigidbody body;
	private Rigidbody teleporterBody;
	private Animator anim;

	// Animation state variables
	private bool isLeft = false;
	private bool isGrounded = true;
	private float prevHeight;
	private bool canMove = true;

	// Physics variables
	public float JumpForce = 1.0f;
	public float WalkingSpeed = 0.05f;
	private float jumpForce;
	private float speed;

	// Throwing variables
	private bool attached = true;
	private float xThrowMagnitude;
	private float yThrowMagnitude;
//	private float xTeleportVector;
//	private float yTeleportVector;
	private float startX = 571f;
	private float startY = 143f;
	private float endX;
	private float endY;
	private int direction = 1;

	void Start () {
		body = GetComponent<Rigidbody> ();
		teleporterBody = Teleporter.GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		prevHeight = body.position.y;

		jumpForce = JumpForce;
		speed = WalkingSpeed;
	}

	void Update () {

		if (canMove) {
			InputManager ();
		} else {
			AnimatorStateInfo curr = anim.GetCurrentAnimatorStateInfo (0);
			if (curr.fullPathHash == Animator.StringToHash("Base Layer.idle_right")) {
				Debug.Log("pls");
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

		// Throwing
		if (attached) 
			Teleporter.transform.position = new Vector3 (this.transform.position.x,
														 this.transform.position.y + 1, 0);

		if (Input.GetKeyDown (KeyCode.Space) && !attached) {
			Teleport ();
		}

		if (Input.GetMouseButtonDown (0)) {
			Instantiate (throwVectorArrow);
			throwVectorArrow.transform.position = Teleporter.transform.position;
		}

		if (Input.GetMouseButtonUp (0) && attached) {
			ThrowTeleporter ();
		}

		/** TEST FUNCTIONS REMOVE LATER **/
		if (Input.GetKeyDown ("d")) {
			Die ();
		} else if (Input.GetKeyDown ("h")) {
			Happy ();
		} else if (Input.GetKeyDown ("t")) {
			Throw ();
		} else if (Input.GetKeyDown (KeyCode.LeftShift)) {
			Pull (true);
		} else if (Input.GetKeyDown (KeyCode.RightShift)) {
			Pull (false);
		}
	}

	/**************************** TELEPORTER CODE ****************************/

	//teleporting to the teleporter sphere
	void Teleport() {	
//		xTeleportVector = teleporterBody.transform.position.x - transform.position.x;
//		yTeleportVector = teleporterBody.transform.position.y - transform.position.y;
		transform.position = new Vector3 (teleporterBody.transform.position.x,
										  teleporterBody.transform.position.y, 0);
		body.velocity = teleporterBody.velocity;
		attached = true;
	}

	//throw the teleporter
	void ThrowTeleporter() {
		attached = false;
		endX = Input.mousePosition.x;
		endY = Input.mousePosition.y;
		//fails to throw if you try to throw in the direction you aren't facing
		if((endX - startX < 0 && direction == 1) || (startX - endX < 0 && direction == -1))
			endX = startX;
		xThrowMagnitude = endX - startX;
		yThrowMagnitude = endY - startY;
		teleporterBody.AddForce (xThrowMagnitude * 5 * direction, yThrowMagnitude * 5, 0);

		Throw ();
	}

	/**************************** COLLISION EVENTS ****************************/

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Teleporter") {
			attached = true;
		} else if (col.gameObject.tag == "Ground") {
			Land ();
		}
	}

	void OnCollisionExit(Collision col) {
		if (col.gameObject.tag == "Ground") {
			isGrounded = false;
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

	private void Throw() {
		anim.SetTrigger ("isThrowing");
	}

	private void Pull(bool left) {
		if (left) {
			anim.SetTrigger ("isPullingLeft");
		} else {
			anim.SetTrigger ("isPullingRight");
		}
		canMove = false;
	}

	private void Die() {
		anim.SetTrigger ("isDead");
		canMove = false;
	}

	private void Happy() {
		anim.SetTrigger ("isHappy");
		canMove = false;
	}

	private void Leave() {
		anim.SetTrigger ("isLeaving");
		canMove = false;
	}
}