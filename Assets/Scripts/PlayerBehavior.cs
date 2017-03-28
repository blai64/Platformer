using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
	
	// Script
	public static PlayerBehavior instance;

	// Public gameobjects
	public GameObject Teleporter; 
	public GameObject teleportAura;
	public GameObject trajectoryBallPrefab;
	public Camera cam;

	// Components
	private Rigidbody body;
	private Rigidbody teleporterBody;
	private Animator anim;
	private TeleporterBehavior tb;
	private WitchSoundScript wsm;

	// Animation state variables
	private bool isLeft = false;
	private bool isGrounded = true;
	private bool isExiting = false;
	private bool canExit = false;
	private float prevHeight;
	private bool canUseArrows = true;
	private bool canMove = true;
	private bool enableMove = false;
	public float AnimTimer = 2f;
	private float animTimer;

	// Physics variables
	public float JumpForce = 1.0f;
	public float WalkingSpeed = 0.05f;
	private float jumpForce;
	private float speed;
	private float colliderToBottom; 
	private float colliderToSide; 
	private float colliderOffset = 0f; 

	// Throwing variables
	private bool attached = true;
	private float xThrowMagnitude;
	private float yThrowMagnitude;
	private float xPreviusPos;
	private float yPreviusPos;
	private float startX = 0f;
	private float startY = -100f;
	private float endX;
	private float endY;
	private int direction = 1;
	public int teleportCharges = 3;
	private bool teleporting = false;
	private List<GameObject> trajectoryBallList = new List<GameObject> ();
	private bool listFilled = false;
	private bool projected = false;
	private bool isLeftOfTeleporter;

	// Pause
	public GameObject controller;
	private bool isPaused;

	//Collecting energy
	public GameObject chargeParticles;

	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}

	void Start () {
		body = GetComponent<Rigidbody> ();
		teleporterBody = Teleporter.GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		tb = Teleporter.GetComponent<TeleporterBehavior> ();
		wsm = GetComponent<WitchSoundScript> ();

		prevHeight = body.position.y;

		jumpForce = JumpForce;
		speed = WalkingSpeed;
		animTimer = AnimTimer;
		isPaused = false;
		colliderToBottom = GetComponent<BoxCollider> ().bounds.extents.y;
		colliderToSide = GetComponent<BoxCollider> ().bounds.extents.x;

		EmitParticles (false);
	}
		
	void Update () {
		isPaused = controller.GetComponent<PauseGame>().isPaused ();

		if (canMove) {
			if(!isPaused)
				InputManager ();
		} else {
			if (enableMove) {
				EnableMove ();
			}
		}
	
		if (isExiting) {
			transform.Translate(new Vector3(0f, 0f, 2f) * Time.deltaTime);
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
		EmitParticles (false);
		tb.Disappear ();
		attached = true;
	}

	public float GetXDistance(){
		return xPreviusPos;
	}

	public float GetYDistance(){
		return yPreviusPos;
	}

	public float GetEndX(){
		return teleporterBody.transform.position.x;
	}

	public bool leftOfTeleporter(){
		return isLeftOfTeleporter;
	}

	/**************************** INPUT MANAGER ****************************/

	void InputManager() {

		if (canUseArrows) {
			// Jumping
			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
				Jump ();
			}

			// Change Direction / Move / Walk
			if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
				ChangeDirection (true);
				Move ();
				Walk (true);
			}
			if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
				ChangeDirection (false);
				Move ();
				Walk (true);
			}
		}

		// Stop Walking
		if (Input.GetKeyUp (KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) {
			Walk (false);
		} else if (Input.GetKeyUp (KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) {
			Walk (false);
		}
		// Falling
		Fall ();

		if (Input.GetKeyDown (KeyCode.Space) && !attached && TeleporterBehavior.instance.isGrounded) {
			Teleport ();
		}

		if (!isPaused) {
			// While holding the mouse down, displays trajectory of throw
			if (Input.GetMouseButtonDown (0) && attached) {
				canUseArrows = false;
				startX = Input.mousePosition.x;
				projected = true;
			}
			if (projected) {
				Vector3 screenPos = cam.WorldToScreenPoint (transform.position);

				if ((Input.mousePosition.x > screenPos.x) && isLeft) {
					ChangeDirection (false);
				} else if ((Input.mousePosition.x < screenPos.x) && !isLeft) {
					ChangeDirection (true);
				}
		

				DisplayThrowTrajectory ();
			}

			if (Input.GetMouseButtonUp (0) && attached && projected) {

				ThrowTeleporter ();
				DestroyProjectedPath ();


			}

			// Cancels the throwing animation without throwing
			if (Input.GetMouseButtonDown (1) && projected) {
				projected = false;
				canUseArrows = true;
				DestroyProjectedPath ();
			}
		}
	}

	/**************************** TELEPORTER CODE ****************************/

	// resets the position of the teleport aura 
	void AuraReset() {
		//teleportAura.SetActive (true);
		teleportAura.GetComponent<TranslatingEssenceScript> ().Activate ();
		teleportAura.transform.position = this.gameObject.transform.position;
		this.gameObject.SetActive (false);
	}

	// teleporting to the teleporter sphere
	void Teleport() {
		if (teleportCharges > 0) {
			if (transform.position.x < Teleporter.transform.position.x)
				isLeftOfTeleporter = true;
			else
				isLeftOfTeleporter = false;
			wsm.PlayTeleport ();
			AuraReset ();
			teleporting = true;
			EmitParticles (true);
			xPreviusPos = transform.position.x;
			yPreviusPos = transform.position.y;
			transform.position = new Vector3 (teleporterBody.transform.position.x,
				teleporterBody.transform.position.y + .6f,
				transform.position.z);
			teleportCharges -= 1;
			TeleporterBehavior.instance.isGrounded = false;
		}
	}

	// throw the teleporter
	void ThrowTeleporter() {
		endX = Input.mousePosition.x;
		endY = Input.mousePosition.y;
		xThrowMagnitude = (endX - startX) * direction;
		yThrowMagnitude = endY - startY;
		Throw (new Vector3 (xThrowMagnitude / 2f * direction, yThrowMagnitude / 2f, 0));
	}

	// Displays throw trajectory
	void DisplayThrowTrajectory() {
		projected = true;
		float xValue, yValue, xOffset, yOffset;

		Vector3 initialVelocity = new Vector3(((Input.mousePosition.x - startX) / 2f) / teleporterBody.mass * Time.fixedDeltaTime,
											  ((Input.mousePosition.y - startY) / 2f) / teleporterBody.mass * Time.fixedDeltaTime,
										   	   0f);
		// fills list with 10 trajectory balls
		int offset = 0;
		RaycastHit objectHit; 
		Vector3 prevPosition = transform.position;
		Vector3 targetPosition;
		GameObject targetBall; 
		int numBallsValid = 0;
		int i = 0;
		while (trajectoryBallList.Count < 20) {
			if (i >= trajectoryBallList.Count)
				trajectoryBallList.Add (Instantiate (trajectoryBallPrefab));
			
				
			xValue = teleporterBody.transform.position.x + (initialVelocity.x * (i * .1f));
			yValue = teleporterBody.transform.position.y + (initialVelocity.y * (i * .1f) + (-4.9f)*((i *.1f)*(i *.1f)));

			if (isLeft) {
				xOffset = -0.2f;
				yOffset = 0.6f;
			} else {
				xOffset = 0.7f;
				yOffset = 0.8f;
			}

			targetBall = trajectoryBallList [i - offset];
			targetBall.transform.position = new Vector3 (xValue + xOffset,
														 yValue + yOffset, 0f);
			
			targetPosition = targetBall.transform.position;

			if (Physics.Raycast (prevPosition, targetPosition - prevPosition, out objectHit, Vector3.Distance(targetPosition,prevPosition))) {
				break;
			}
			prevPosition = targetPosition;
			i++;
			numBallsValid++;
		}
		List<GameObject> toDelete = trajectoryBallList.GetRange (
			Mathf.Min (numBallsValid, trajectoryBallList.Count - 1),
			Mathf.Max (0, trajectoryBallList.Count - numBallsValid));
		foreach (GameObject g in toDelete) {
			trajectoryBallList.Remove (g); 
			Destroy (g);
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
			RaycastHit objectHit; 
			if (Physics.Raycast (transform.position, Vector3.down, out objectHit, colliderToBottom + colliderOffset)) {
				if (objectHit.transform.CompareTag("Ground")){
					Land ();
				}
			}
			else if (Physics.Raycast (transform.position + new Vector3(colliderToSide,0,0), Vector3.down, out objectHit, colliderToBottom + colliderOffset)) {
				if (objectHit.transform.CompareTag("Ground")){
					Land ();
				}
			}
			else if (Physics.Raycast (transform.position - new Vector3(colliderToSide,0,0), Vector3.down, out objectHit, colliderToBottom + colliderOffset)) {
				if (objectHit.transform.CompareTag("Ground")){
					Land ();
				}
			}

		} else if (col.gameObject.CompareTag ("Killer")) {
			Die ();
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.CompareTag ("Crystal")) {
			StartCoroutine (Charge ());
			Happy ();
		} else if (col.gameObject.CompareTag ("Exit") && canExit) {
			Leave ();
		} else if (col.gameObject.CompareTag ("Pit")) {
			Die ();
		}
	}


	void OnCollisionExit(Collision col) {
		if (col.gameObject.tag == "Ground") {
			RaycastHit objectHit; 
			if (Physics.Raycast (transform.position, Vector3.down, out objectHit, colliderToBottom + colliderOffset)) {
				if (!objectHit.transform.CompareTag ("Ground")) {
					isGrounded = false;
				}
			} else
				isGrounded = false;

		}
	}
		
	//If player is colliding and presses button, then change switch
	void OnTriggerStay (Collider other){
		if (other.gameObject.CompareTag ("Switch") && 
			(Input.GetKeyDown(KeyCode.LeftShift))) {
			if (other.gameObject.GetComponent<SwitchScript>().IsActive)
				Pull(false);
			else 
				Pull(true);
		}
	}
		
	/**************************** ANIMATION EVENTS ****************************/

	private void Jump() {
		anim.SetTrigger ("isJumping");
		wsm.PlayJumpThrow ();
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

	public void EnableArrows() {
		canUseArrows = true;
	}

	public void DisableArrows() {
		canUseArrows = false;
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
		wsm.PlayDeath ();
		canMove = false;
		MainCamera.instance.transform.Find ("FadeOut").gameObject.GetComponent<Fade> ().FadeInOut (false, true);
	}

	private void Happy() {
		anim.SetTrigger ("isHappy");
		anim.SetBool ("isWalking", false);
		wsm.PlayCrystalCollect ();
		canMove = false;
		enableMove = true;
	}

	private void Leave() {
		anim.SetTrigger ("isLeaving");
		isExiting = true;
		canMove = false;
	}

	private void Shrink() {
		anim.SetTrigger ("isShrinking");
	}

	private void Grow() {
		anim.SetTrigger ("isGrowing");
	}

	public void CanExit() {
		canExit = true;
	}

	private void EmitParticles(bool val){
		GameObject particle;
		ParticleSystem.EmissionModule em;
		for (int i = 0; i < chargeParticles.transform.childCount; i++) {
			particle = chargeParticles.transform.GetChild (i).gameObject; 
			em = particle.GetComponent<ParticleSystem> ().emission;
			em.enabled = val;
		}
	}

	IEnumerator Charge() {
		EmitParticles (true);

		yield return new WaitForSeconds (1.5f);

		EmitParticles (false);
	}
}