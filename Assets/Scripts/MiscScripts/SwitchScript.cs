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

	private float duration = 1.0f;

	public float Duration{
		get{ return duration;}
		set{ duration = value;}
	}

	private float disableTime; 


	// Use this for initialization
	void Start () {
		isActive = false;
	}

	void Update(){
		disableTime -= Time.deltaTime;
	}
		
	//If player is colliding and presses button, then change switch
	void OnTriggerStay (Collider other){
		if (other.gameObject.CompareTag ("Player") && 
			Input.GetKeyDown (KeyCode.Space) &&
			disableTime < 0.0f) {
			isActive = !isActive;
			disableTime = duration;
		}
			
	}
}
