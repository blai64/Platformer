using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainButton : MonoBehaviour {
	public Button yourButton;
	public GameObject menu;
	public string nextScene;
	private Renderer render;
	private bool show;

	// Use this for initialization
	void Start () {
		//render = menu.GetComponent<Renderer> ();
		//render.enabled = false;
		menu.SetActive(false);
		//witch.SetActive (true);
		//menu.SetActive (false);
		show = menu.activeSelf;
		Debug.Log (show);

			//Button btn = yourButton.GetComponent<Button> ();
			//btn.onClick.AddListener (Update);
	}
	
	// Update is called once per frame
	void Update () {
		show = menu.activeSelf;
		if (Input.GetKeyDown (KeyCode.Return)) {
			yourButton.Select ();
			if (show == true)
				menu.SetActive (false);
			else
				menu.SetActive (true);
			//render.enabled = true;
			//SceneManager.LoadScene (nextScene);
		}
	}
}
