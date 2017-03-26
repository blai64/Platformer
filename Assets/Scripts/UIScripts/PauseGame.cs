using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
	
	public Transform canvas;
	private bool paused;

	void Start(){
		paused = false;
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			Pause();
		}
	}

	public void Pause() {
		
		paused = true;
		if (canvas.gameObject.activeInHierarchy == false) {
			canvas.gameObject.SetActive (true);
			Time.timeScale = 0;
		} else {
			canvas.gameObject.SetActive (false);
			paused = false;
			Time.timeScale = 1;

		}
	}

	public bool isPaused(){
		return paused;
	}

	
}
