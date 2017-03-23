using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainButton : MonoBehaviour {
	public GameObject menu;
	public string nextScene;
	private Renderer render;
	private bool show;
	//private bool pause;

	// Use this for initialization
	void Start () {
		//render = menu.GetComponent<Renderer> ();
		//render.enabled = false;
		menu.SetActive(false);
		show = menu.activeSelf;
		Debug.Log (show);
		//pause = false;

		//Button btn = yourButton.GetComponent<Button> ();
		//btn.onClick.AddListener (Update);
	}
	
	// Update is called once per frame
	void Update () {
		show = menu.activeSelf;
		if (Input.GetKeyDown (KeyCode.Return)) {
			//yourButton.Select ();
			menu.SetActive (true);
			//Time.timeScale = 0.0f;
			//pause = true;
			//render.enabled = true;
			//SceneManager.LoadScene (nextScene);
		}
	}

	//public bool isPaused(){
	//	return pause;
	//}
}
