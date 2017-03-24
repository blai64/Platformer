using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectEnemy : MonoBehaviour {

	public Transform targetToAdd;

	void OnTriggerEnter(Collider col) {
		
		if (col.gameObject.CompareTag ("Player")) {
			if (!MainCamera.instance.targetsOnScreen.Contains(targetToAdd))
				MainCamera.instance.targetsOnScreen.Add (targetToAdd);
		}
	}
}
