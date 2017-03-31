using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour {

	public float LeftBound = 41;
	public float RightBound = 59;
	private float leftBound;
	private float rightBound;
	private int direction = 1;

	Rigidbody body;

	void Start () {
		body = this.GetComponent<Rigidbody> ();
		leftBound = LeftBound;
		rightBound = RightBound;
	}

	void Update () {
		body.velocity = new Vector3 (direction,0,0);
		if (transform.position.x > rightBound)
			direction = -1;
		if (transform.position.x < leftBound)
			direction = 1;
	}
}
