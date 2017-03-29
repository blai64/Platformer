using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour {

	//public Transform p1;
	//public Transform p2;
	//public float speed = 0.05f;

	public float semimajor = 9.0f;
	public float semiminor = 2.0f;
	public float centerx = 30.0f;
	public float centery = 18.5f;

	private float x;
	private float y;
	private int alpha;


	void Start () {
		x = 24.7f;
		y = 19.5f;
		alpha = 0;
	}
	
	void Update() {
		/*
		float step = speed * Time.deltaTime;
		if (transform.position != p1.position) {
			transform.position = Vector3.MoveTowards (transform.position, p1.position, step);
		} else if (transform.position != p2.position) {
			transform.position = Vector3.MoveTowards (transform.position, p2.position, step);
		}*/

		alpha += 10;
		x = centerx + (float)(semimajor * Mathf.Cos(alpha*.005f));
		y = centery + (float)(semiminor * Mathf.Sin(alpha*.005f));
		this.gameObject.transform.position = new Vector3 (x, y, 0);


	}
}
