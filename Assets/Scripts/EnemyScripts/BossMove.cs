using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossMove : MonoBehaviour {

	//public Transform p1;
	//public Transform p2;
	//public float speed = 0.05f;

	public float semimajor = 9.0f;
	public float semiminor = 2.0f;
	public float centerx = 30.0f;
	public float centery = 18.5f;


	public string nextSceneName;

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
	private bool isDead;
	private bool invincible;
	private int stopCounter; 

	private IEnumerator curRoutine;

	private float speed;

	private Animator anim;


	void Start () {
		health = 3;
		x = centerx;
		y = centery;
		alpha = 0f;
		isMoving = true;
		invincible = false;
		isDead = false;
		speed = 5.0f;

		stopCounter = 1;

		anim = transform.GetChild (0).GetComponent<Animator> ();
		anim.SetBool ("isLeft", false);
		anim.SetBool ("isAttacking", true);
	}
	
	void Update() {
		if (isMoving && health > 0 && !isDead) {
			moveInOval ();
		}
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
		anim.SetBool ("isHurt", false);
		anim.SetBool ("isAttacking", true);
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.CompareTag ("Killer") && health > 0
		    && !invincible && !isDead) {
			Debug.Log ("kk");
			isMoving = false;
			invincible = true; 

			health--;

			if (curRoutine != null)
				StopCoroutine (curRoutine);

			anim.SetBool ("isHurt", true);

			if (health <= 0) {
				anim.SetBool ("isAttacking", false);
				gameObject.tag = "Untagged";
				isDead = true;
				StartCoroutine (KillBoss ()); 
			} else {
				curRoutine = Stop (4);
				StartCoroutine (curRoutine);

			}

		} else if (col.gameObject.CompareTag ("Ground") && isDead) {
			GetComponent<BoxCollider> ().size = new Vector3 (1.0f, 1.0f, 1.0f);
			anim.SetBool ("isDead", true);
			//StartCoroutine (ChangeScene ());
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.CompareTag ("BossStop")) {
			isMoving = false;
			stopCounter = (stopCounter + 1) % 2;
			if (stopCounter == 0)
				anim.SetBool ("isLeft", !anim.GetBool ("isLeft"));

			if (curRoutine != null)
				StopCoroutine (curRoutine);

			anim.SetBool ("isAttacking", false);
			curRoutine = Stop (2);
			StartCoroutine (curRoutine);

		}
	}

	IEnumerator KillBoss(){
		yield return new WaitForSeconds (1.0f);
		anim.SetBool ("isHurt", false);
		GetComponent<Rigidbody> ().useGravity = true;
	} 

	IEnumerator ChangeScene(){
		UIFade.instance.Fade (false);
		yield return new WaitForSeconds (1.5f);
		SceneManager.LoadScene (nextSceneName);
	}
}
