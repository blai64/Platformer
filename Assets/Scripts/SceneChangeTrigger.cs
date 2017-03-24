using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeTrigger : MonoBehaviour {

	public GameObject Fader;
	private Fade fb;

	void Start() {
		fb = Fader.GetComponent<Fade> ();
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.CompareTag ("Player")) {
			fb.FadeInOut (false, false);
		}
	}
}
