using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverText : MonoBehaviour {

	public GameObject leverbox;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftShift)){
			leverbox.SetActive(false);
		}
	}
}
