using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour {
	//TODO: Need to add animations for switch object

	private bool isActive;

	public bool IsActive{
		get{ return isActive;}
		set{ isActive = value;}
	}


	// Use this for initialization
	void Start () {
		isActive = false;
	}
		
	//If player is colliding and presses button, then change switch
	void OnTriggerStay (Collider other){
		if (other.gameObject.CompareTag ("Player") && Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log("Switching");
			isActive = !isActive;
		}
			
	}
}
