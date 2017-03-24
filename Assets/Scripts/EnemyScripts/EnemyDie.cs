using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.CompareTag ("Killer")) {
			MainCamera.instance.targetsOnScreen.Remove (transform);
			Destroy (this.gameObject);
		}
	}
}
