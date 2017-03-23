using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
	public Transform canvas;
	private bool paused;

	void Start(){
		paused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			paused = true;
			Pause();
		}
	}

	void Pause(){
		if (canvas.gameObject.activeInHierarchy == false) {
			canvas.gameObject.SetActive (true);
			Time.timeScale = 0;
		} else {
			canvas.gameObject.SetActive (false);
			Time.timeScale = 1;
			paused = false;
		}
	}

	public bool isPaused(){
		return paused;
	}

	
}
