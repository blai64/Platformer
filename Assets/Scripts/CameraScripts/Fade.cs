using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour {

	private SpriteRenderer fadeoutSprite;

	public string NextSceneName = "Splash";
	public int FadeDuration = 2;

	public void FadeInOut(bool fadingIn, bool isDead) {
		StartCoroutine(StartFade(FadeDuration, fadingIn, isDead));
	}

	public void FadeMenu() {
		NextSceneName = "Splash";
		StartCoroutine(StartFade(FadeDuration, false, false));
	}

	IEnumerator StartFade(float duration, bool fadingIn, bool isDead) {
		float t = 0.0f;
		fadeoutSprite = gameObject.GetComponent<SpriteRenderer> ();
		Color temp = fadeoutSprite.color;
		Color lerpedColor;
		Color targetColor;

		if (fadingIn)
			targetColor = Color.clear;
		else
			targetColor = Color.black;

		while (t < 1.0f) {
			t += Time.deltaTime / duration;

			lerpedColor = Color.Lerp (temp, targetColor, t);
			fadeoutSprite.color = lerpedColor;

			yield return 0;
		}

		if (!fadingIn) {
			if (isDead) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			} else {
				NextScene ();
			}
		}
	}

	private void NextScene() {
		SceneManager.LoadScene(NextSceneName);
	}
}
