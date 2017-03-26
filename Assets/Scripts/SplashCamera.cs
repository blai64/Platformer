using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashCamera : MonoBehaviour {

	void Awake () {

		transform.Find ("FadeOut").gameObject.SetActive (true);
		transform.Find ("FadeOut").gameObject.GetComponent<Fade> ().FadeInOut (true,false);
	}
}
