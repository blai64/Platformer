﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFade : MonoBehaviour {
	public static UIFade instance;

	private float speed = 0.5f;

	void Awake()
	{
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy (gameObject); 
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (FadeIn (2.0f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Fade (bool fadeIn){
		if (fadeIn)
			StartCoroutine (FadeIn (0.0f));
		else
			StartCoroutine (FadeOut (0.0f));
	}



	IEnumerator FadeIn(float delay) {
		yield return new WaitForSeconds (delay);

		CanvasGroup cg = GetComponent<CanvasGroup> ();


		while (cg.alpha < 1) {
			cg.alpha += Time.deltaTime * speed; 
			yield return null;
		}
	}

	IEnumerator FadeOut(float delay) {
		yield return new WaitForSeconds (delay);

		CanvasGroup cg = GetComponent<CanvasGroup> ();


		while (cg.alpha > 0) {
			cg.alpha -= Time.deltaTime * speed; 
			yield return null;
		}
	}

}