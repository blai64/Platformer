using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionScript : MonoBehaviour {

	public Camera cam;

	private Animator anim;

	private float MoveDistance = 5.0f;
	private float MoveTime = 1.5f;
	private bool moving;

	void Start() {
		anim = GetComponent<Animator> ();
	}

	void OnTriggerStay (Collider other) {
		if (other.gameObject.CompareTag ("Player") && !moving) {
//			gameObject.GetComponent<Rigidbody> ().AddForce (0,3,0,ForceMode.Impulse);

			if (transform.position.x > other.transform.position.x) {
				anim.SetBool ("isLeft", true);
			} else {
				anim.SetBool ("isLeft", false);
			}

			anim.SetBool ("isAttacking", true);

			int direction = 1; 

			if (other.transform.position.x < transform.position.x)
				direction *= -1; 

			Vector3 curPos = transform.position;
			Vector3 newPos = transform.position + new Vector3(direction * MoveDistance, 0, 0);

			moving = true;
			StartCoroutine (MoveEnemy(curPos, newPos, MoveTime));
		}
	}

	IEnumerator MoveEnemy(Vector3 oldPos, Vector3 newPos, float duration) {
		yield return new WaitForSeconds (1);

		float t = 0.0f;
		while (t < 1.0f) {
			t += Time.deltaTime / duration;
			transform.position = Vector3.Lerp (oldPos, newPos, t);

			yield return 0;
		}
		yield return new WaitForSeconds (1);
		moving = false;
		anim.SetBool ("isAttacking", false);
	}
}


