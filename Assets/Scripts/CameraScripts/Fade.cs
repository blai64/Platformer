using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

	private SpriteRenderer fadeoutSprite;

	public int FadeDuration = 2;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

	public void FadeInOut(bool fadingIn){
		StartCoroutine(StartFade(FadeDuration, fadingIn));
	}

	IEnumerator StartFade(float duration, bool fadingIn){
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
	}
}
