using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour {
	int direction = -1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (.02f * direction, 0, 0);
		if (transform.position.x > 82)
			direction *= -1;
		if (transform.position.x < 65)
			direction *= -1;
	}
}
