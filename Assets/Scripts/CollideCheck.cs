using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideCheck : MonoBehaviour {

	private bool grounded = false;

	void OnCollisionEnter(Collision col){
		if (col.gameObject.name == "Plane") {
			grounded = true;
		}
	}

	public bool ifGrounded(){
		return grounded;
	}
}
