using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITrigger : MonoBehaviour {

	public string content;

	void OnTriggerEnter (Collider other){
		if (other.gameObject.CompareTag ("Player")) {
			UIFade.instance.Fade (true);
			UIFade.instance.SetText (content);
		}
	}

	void OnTriggerExit (Collider other){
		if (other.gameObject.CompareTag ("Player")) {
			UIFade.instance.Fade (false);
		}
	}
}
