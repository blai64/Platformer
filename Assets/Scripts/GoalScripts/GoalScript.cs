using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Player")) {
			MainCamera.instance.transform.Find ("FadeOut").gameObject.GetComponent<Fade> ().FadeInOut (false, false);
		}
	}
}
