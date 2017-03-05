using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	private Rigidbody body;
	private Rigidbody teleporterBody;

	public GameObject teleporterSphere; 
	public GameObject throwVectorArrow;

	private bool attatched;


	private float xThrowMagnitude;
	private float yThrowMagnitude;
	private float xTeleportVector;
	private float yTeleportVector;
	private float startX;
	private float startY;
	private float endX;
	private float endY;
	private float direction = 1;


	// Use this for initialization
	void Start () {
		attatched = true;
		body = this.GetComponent<Rigidbody> ();
		teleporterBody = teleporterSphere.GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		//player navigation
		if (Input.GetKey (KeyCode.LeftArrow)) {
			direction = -1;
			transform.Translate (.1f * direction, 0, 0);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			direction = 1;
			transform.Translate (.1f * direction, 0, 0);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			direction = 1;
			transform.Translate (0, .1f * direction, 0);
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
	}
}