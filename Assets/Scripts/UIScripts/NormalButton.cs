using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NormalButton : MonoBehaviour {
	public Button yourButton;
	public string nextScene;

	// Use this for initialization
	void Start () {
		Button btn = yourButton.GetComponent<Button> ();
		btn.onClick.AddListener (Update);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Main is pressed????!!!!");
		SceneManager.LoadScene (nextScene);
	}
}
