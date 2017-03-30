using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour {
	int direction = 1;

	Rigidbody body;
	// Use this for initialization
	void Start () {
		body = this.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		body.velocity = new Vector3 (direction,0,0);
		if (transform.position.x > 59)
			direction = -1;
		if (transform.position.x < 41)
			direction = 1;
	}
}
