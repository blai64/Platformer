using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour {

	public GameObject Target;
	private SpriteRenderer sr;
	private float groundY;

	void Start() {
		sr = GetComponent<SpriteRenderer>();
		groundY = transform.position.y;
	}

	void Update () {
		if (Target != null) {
			if (GameObject.Find(Target.name)) {
				sr.enabled = true;
				transform.position = new Vector3(Target.transform.position.x,
												 groundY,
												 transform.position.z);
			} else {
				sr.enabled = false;
			}
		} else {
			sr.enabled = false;
			Destroy(this);
		}
	}
}
