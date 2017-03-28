using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveTrigger : MonoBehaviour {

	private Vector3 translation;
	public float zoomZ;

	public float camXRight;
	public float camYDown;

	public bool availableFromLeft;

	void OnTriggerEnter(Collider other){
		translation = new Vector3 (camXRight, camYDown, zoomZ);

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
