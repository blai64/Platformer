using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour {
	public static UIFade instance;

	private float inSpeed = 0.5f;
	private float outSpeed = 1.0f;
	private IEnumerator current;

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
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetText(string content){
		transform.Find ("Panel/Text").gameObject.GetComponent<Text> ().text = content;
	}

	public void Fade (bool fadeIn){
		if (current != null)
			StopCoroutine (current);
		if (fadeIn) {
			current = FadeIn (0.0f);
		}
		else {
			current = FadeOut (0.0f);
		}

		StartCoroutine (current);
	}



	IEnumerator FadeIn(float delay) {
		yield return new WaitForSeconds (delay);

		CanvasGroup cg = GetComponent<CanvasGroup> ();


		while (cg.alpha < 1) {
			cg.alpha += Time.deltaTime * inSpeed; 
			yield return null;
		}
	}

	IEnumerator FadeOut(float delay) {
		yield return new WaitForSeconds (delay);

		CanvasGroup cg = GetComponent<CanvasGroup> ();


		while (cg.alpha > 0) {
			cg.alpha -= Time.deltaTime * outSpeed; 
			yield return null;
		}
	}

}
