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

	public static BossMove instance;

	private float x;
	private float y;
	private float alpha;
	private float radian;

	private Transform p1;
	private Transform p2;
	private Transform p3;
	private Transform p4;

	public int health;
	private int hit;

	public float smoothTime = 0.3F;

	private bool isMoving;
	private bool isMoving2;
	private bool invincible;

	private IEnumerator curRoutine;

	private float speed;


	void Start () {
		health = 3;
		x = centerx;
		y = centery;
		alpha = 0f;
		isMoving = true;
		invincible = false;
		speed = 5.0f;

	}
	
	void Update() {
		/*
		float step = speed * Time.deltaTime;
		if (transform.position != p1.position) {
			transform.position = Vector3.MoveTowards (transform.position, p1.position, step);
		} else if (transform.position != p2.position) {
			transform.position = Vector3.MoveTowards (transform.position, p2.position, step);
		}
		*/
		if (isMoving && health > 0) {
			moveInOval ();
		}
		/*
		if (radian % (Mathf.PI/4) < 0.05) {
			isMoving = false;

			Debug.Log ("here");

			if (curRoutine != null)
				StopCoroutine (curRoutine);
			
			curRoutine = Stop (2);
			StartCoroutine (curRoutine);
		}*/

			
	}

	void moveInOval(){
		alpha += speed;
		radian = alpha * .005f;
		x = centerx + (float)(semimajor * Mathf.Cos (radian));
		y = centery + (float)(semiminor * Mathf.Sin (radian));
		Vector3 targetPosition = new Vector3 (x, y, 0);
		this.gameObject.transform.position = targetPosition;
	}

	IEnumerator Stop(int time) {
		yield return new WaitForSeconds (time);
		isMoving = true;
		invincible = false;
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.CompareTag ("Killer") && health > 0
			&& !invincible) {
			Debug.Log ("kk");
			isMoving = false;
			invincible = true; 
			if (health == 3)
				health = 2;
			else if (health == 2)
				health = 1;
			else if (health == 1)
				health = 0;

			if (curRoutine != null)
				StopCoroutine (curRoutine);

			curRoutine = Stop (4);
			StartCoroutine (curRoutine);

		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.CompareTag ("BossStop")) {
			isMoving = false;
		
			if (curRoutine != null)
				StopCoroutine (curRoutine);

			curRoutine = Stop (2);
			StartCoroutine (curRoutine);

		}
	}
}
