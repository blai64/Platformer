using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	private Rigidbody body;
	private Rigidbody teleporterBody;

	public GameObject teleporterSphere; 
	public GameObject throwVectorArrow;

	private bool attatched;

	Animator anim;
	private bool facingRight = true;

	/* Check if she's in ground*/
	private bool isGrounded = true;
	public float jumpForce = 1.0f;

	private float xThrowMagnitude;
	private float yThrowMagnitude;
	private float xTeleportVector;
	private float yTeleportVector;
	private float startX;
	private float startY;
	private float endX;
	private float endY;
	private float direction = 1;

	private bool walkingLeft = false;
	private bool walkingRight = false;
	private bool jumping = false;


	// Use this for initialization
	void Start () {
		attatched = true;
		body = this.GetComponent<Rigidbody> ();
		teleporterBody = teleporterSphere.GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
	}


	// Update is called once per frame
	void Update () {
		//OnCollisionEnter ();
		if(isGrounded != true)
			Debug.Log (isGrounded);

		//player navigation
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			anim.SetBool ("isWalkingLeft", true);
			walkingLeft = true;
			facingRight = false;

		}
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			anim.SetBool ("isWalkingLeft", false);
			walkingLeft = false;
		}

		if (walkingLeft) {
			direction = -1;
			transform.Translate (.1f * direction, 0, 0);
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			anim.SetBool ("isWalkingRight", true);
			walkingRight = true;
			facingRight = true;

		}
		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			anim.SetBool ("isWalkingRight", false);
			walkingRight = false;
		}

		if (walkingRight) {
			direction = 1;
			transform.Translate (.1f * direction, 0, 0);
		}


		if (Input.GetKey (KeyCode.UpArrow)) {
			if (facingRight) {
				anim.SetBool ("isJumpingRight", true);
			} else
				anim.SetBool ("isJumpingLeft", true);
<<<<<<< HEAD
			jumping = true;
=======
>>>>>>> 7cdff69584c1767044897428f60983eda1e839e1
			//direction = 1;
			//transform.Translate (0, .1f * direction, 0);
			body.AddForce (Vector3.up * jumpForce);
		}

		if (isGrounded && jumping) {
			body.AddForce (Vector3.up * jumpForce);
			jumping = false;
		}

		if (isGrounded && !jumping) {
			anim.SetBool ("isJumpingRight", false);
			anim.SetBool ("isJumpingLeft", false);
			Debug.Log ("landed!");
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			direction = -1;
			transform.Translate (0, .1f * direction, 0);
		}

		//teleporter sphere following player
		if (attatched) 
			teleporterSphere.transform.position = new Vector3 (this.transform.position.x,this.transform.position.y + 1, 0);

		if (Input.GetKeyDown (KeyCode.Space) && !attatched) {
			Teleport ();
		}

		if (Input.GetMouseButtonDown (0)) {
			startX = 571f;
			startY = 143f;
			Instantiate (throwVectorArrow);
			throwVectorArrow.transform.position = teleporterSphere.transform.position;
		}
		if (Input.GetMouseButtonUp (0) && attatched) {
			throwTeleporter ();
		}
	}




	//teleporting to the teleporter sphere
	void Teleport()
	{	
		xTeleportVector = teleporterBody.transform.position.x - transform.position.x;
		yTeleportVector = teleporterBody.transform.position.y - transform.position.y;
		transform.position = new Vector3 (teleporterBody.transform.position.x,teleporterBody.transform.position.y, 0);
		body.velocity = teleporterBody.velocity;
		attatched = true;
	}

	//throw the teleporter
	void throwTeleporter()
	{
		attatched = false;
		endX = Input.mousePosition.x;
		endY = Input.mousePosition.y;
		//fails to throw if you try to throw in the direction you aren't facing
		if((endX - startX < 0 && direction == 1) || (startX - endX < 0 && direction == -1))
			endX = startX;
		xThrowMagnitude = endX - startX;
		yThrowMagnitude = endY - startY;
		teleporterBody.AddForce (xThrowMagnitude * 5 * direction, yThrowMagnitude * 5, 0);
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.name == "Teleporter Sphere") {
			attatched = true;
		}
		if (col.gameObject.name == "Plane")
			isGrounded = true;
	}
	void OnCollisionExit(Collision col){
		if (col.gameObject.name == "Plane")
			isGrounded = false;
	}
}