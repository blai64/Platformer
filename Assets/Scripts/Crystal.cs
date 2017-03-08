using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour {

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.name == "witch_char"){
			PlayerBehavior.instance.teleportCharges += 1;
			Destroy(this.gameObject);
		}
	}

}
