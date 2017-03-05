using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abstract class for all things that could be a target of a switch
public class SwitchTarget : MonoBehaviour {

	public GameObject mySwitch;
	private bool isActive;
	private bool switchActive;

	// Use this for initialization
	void Start () {
		isActive = mySwitch.GetComponent<SwitchScript> ().IsActive;
	}
	
	//Check if switch active status is the same, otherwise 
	virtual public void Update () {
		switchActive = mySwitch.GetComponent<SwitchScript> ().IsActive;
		if (isActive != switchActive) {
			Debug.Log ("switch changed!");
			isActive = switchActive;
			Activate (isActive);
		}
	}

	//override to determine exactly what the switch target should be doing, can 
	//extend to different things (moving spikes up and down, 
	virtual public void Activate(bool activating){
		Debug.Log ("hello");
	}
}
