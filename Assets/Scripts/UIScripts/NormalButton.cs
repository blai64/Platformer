using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NormalButton : MonoBehaviour {
	
	public Button yourButton;
	public string nextScene;

	void Start () {
		Button btn = yourButton.GetComponent<Button> ();
		btn.onClick.AddListener (ButtonEvent);
	}

	void ButtonEvent () {
		if (nextScene == "Quit") {
			Application.Quit ();
		} else {
			SceneManager.LoadScene (nextScene);
		}
		
	}
}
