using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.CompareTag ("Pit")) {
			MainCamera.instance.targetsOnScreen.Remove (transform);
			Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.CompareTag ("Killer") && !gameObject.CompareTag("Dead")) {
			tag = "Dead";
			StartCoroutine (Die ());
		}
	}

	IEnumerator Die(){
		transform.Rotate (90, 0, 0);
		DeathManager.instance.SpawnParticles (transform.position);
		yield return new WaitForSeconds (2.0f);
		Destroy (gameObject);
		yield return 0;
	}
}
