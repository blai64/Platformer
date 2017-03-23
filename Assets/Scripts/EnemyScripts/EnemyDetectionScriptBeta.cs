using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionScriptBeta : MonoBehaviour {
	//private float MoveDistance = 5.0f;
	//private float MoveTime = 1.5f;
	private bool moving;
	private bool detected; 
	private float speed = 2.0f;
	private float maxChaseDistance = 10.0f; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay (Collider other){
		
		if (other.gameObject.CompareTag ("Player") && !moving) {
			/*
			int direction = 1; 

			if (other.transform.position.x < transform.position.x)
				direction *= -1; 

			Vector3 curPos = transform.position;
			Vector3 newPos = transform.position + new Vector3 (direction * MoveDistance, 0, 0);
			*/
			moving = true;
			//StartCoroutine (MoveEnemy(curPos, newPos, MoveTime));
			StartCoroutine (ChasePlayer (other.transform));

		} 
	}

	IEnumerator MoveEnemy(Vector3 oldPos, Vector3 newPos, float duration){
		yield return new WaitForSeconds (1);
		float t = 0.0f;
		while (t < 1.0f) {
			t += Time.deltaTime / duration;
			transform.position = Vector3.Lerp (oldPos, newPos, t);

			yield return 0;
		}
		yield return new WaitForSeconds (1);
		moving = false;
	}

	IEnumerator ChasePlayer(Transform playerTransform){
		gameObject.GetComponent<Rigidbody> ().AddForce (0, 3, 0, ForceMode.Impulse);

		yield return new WaitForSeconds (1);

		RaycastHit objectHit; 

		while (true) {
			transform.LookAt (playerTransform);
			Quaternion temp = transform.rotation;
			temp.x = 0;
			temp.z = 0;
			transform.rotation = temp;
			transform.position += transform.forward * speed * Time.deltaTime;


			if (Physics.Raycast (transform.position, transform.forward, out objectHit, 100.0f)) {
				if (!objectHit.transform.CompareTag("Player")){
					Debug.Log("Cant see player!!");
					break;
				}
			}

			//if you are out of range or there is an object between you and the player, then stop
			if (Vector3.Distance (playerTransform.position, transform.position) > maxChaseDistance) {
				Debug.Log ("max distance exceeded - stopping chase");
				break; 
			}

			yield return 0;
		}

		Debug.Log ("setting move to false");
		moving = false; 
	}
}


