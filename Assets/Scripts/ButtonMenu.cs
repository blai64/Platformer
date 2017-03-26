using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour {

	public Button yourButton;
	public string buttonName;
	private PauseGame pg;

	void Start () {
		Button btn = yourButton.GetComponent<Button> ();
		pg = GameObject.Find ("UI Controller").GetComponent<PauseGame> ();

		if (buttonName == "Resume") {
			btn.onClick.AddListener (Resume);
		} else if (buttonName == "Pause") {
			btn.onClick.AddListener (Pause);
		} else if (buttonName == "Restart") {
			btn.onClick.AddListener (Restart);
		} else if (buttonName == "Quit") {
			btn.onClick.AddListener (Quit);
		} else if (buttonName == "Main") {
			btn.onClick.AddListener (MainMenu);
		}
	}

	void Pause() {
		pg.Pause ();
	}

	void Restart() {
		pg.Pause ();
		MainCamera.instance.transform.Find ("FadeOut").gameObject.GetComponent<Fade> ().FadeInOut (false, true);
	}

	void Resume () {
		pg.Pause ();
	}

	void Quit() {
		pg.Pause ();
		Application.Quit ();
	}

	void MainMenu() {
		pg.Pause();
		MainCamera.instance.transform.Find ("FadeOut").gameObject.GetComponent<Fade> ().FadeMenu();
	}
}
