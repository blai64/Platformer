using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionScript : MonoBehaviour {

	public Camera cam;

	private Animator anim;

	private bool moving;
	private int direction;
	private float speed = 0.05f;
	private float chaseEndDelay = 2.0f;
	private Transform eyes; // empty game object for raycasting 

	void Start() {
		anim = GetComponent<Animator> ();
		eyes = transform.Find ("Eyes");
	}

	void Update(){
		if (moving) {
			//GetComponent<Rigidbody> ().AddForce (direction * speed * 1000, 0, 0);
			transform.Translate (direction * speed, 0, 0);
		}
	}

	void ChangeDirection(Transform playerTransform){
		if (transform.position.x > playerTransform.position.x) {
			anim.SetBool ("isLeft", true);
			direction = 1;
		} else {
			Debug.Log ("right");
			anim.SetBool ("isLeft", false);
			direction = -1;
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Player") && !moving) {
//			gameObject.GetComponent<Rigidbody> ().AddForce (0,3,0,ForceMode.Impulse);
			ChangeDirection(other.transform);
			anim.SetBool ("isAttacking", true);
			moving = true;
			StartCoroutine (MoveEnemy(other.transform));
		}
	}

	IEnumerator StopAttacking(){
		anim.SetBool ("isAttacking", false);
		moving = false;
		yield return 0;
	}

	IEnumerator MoveEnemy(Transform playerTransform) {
		RaycastHit objectHit; 

		while (true) {
			eyes.LookAt (playerTransform);
			if (Physics.Raycast (eyes.position, eyes.forward, out objectHit, 100.0f)) {
				if (!objectHit.transform.CompareTag("Player")){
					Debug.Log("Cant see player!!");
					break;
				}
			}
			ChangeDirection (playerTransform);

			yield return new WaitForSeconds(0.5f);
		}
		yield return new WaitForSeconds (chaseEndDelay);
		anim.SetBool ("isAttacking", false);
		moving = false; 

	}
}


