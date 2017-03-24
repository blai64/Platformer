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
		btn.onClick.AddListener (update);
	}

	void update () {
		SceneManager.LoadScene (nextScene);
	}
}
