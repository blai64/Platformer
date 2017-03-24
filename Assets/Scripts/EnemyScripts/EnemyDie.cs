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
}
