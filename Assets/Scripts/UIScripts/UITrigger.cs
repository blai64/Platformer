using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITrigger : MonoBehaviour {

	public string content; 



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
