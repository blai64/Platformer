using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveTrigger : MonoBehaviour {

	public Vector3 translation;
	public bool availableFromLeft;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Player")) {
			if (other.transform.position.x < transform.position.x && availableFromLeft) {
				MainCamera.instance.MoveCamera (translation);
				availableFromLeft = !availableFromLeft;
			} else if (other.transform.position.x > transform.position.x && !availableFromLeft) {
				MainCamera.instance.MoveCamera (-1.0f * translation);
				availableFromLeft = !availableFromLeft;
			}
				
		}
	}
}
