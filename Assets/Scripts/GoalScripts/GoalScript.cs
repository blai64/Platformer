using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour {

	public GameObject fadeout; 
	private SpriteRenderer fadeoutSprite;

	// Use this for initialization
	void Start () {
		fadeoutSprite = fadeout.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Player")) {
			StartCoroutine (FadeOut (4));
			
		}
	}

	IEnumerator FadeOut(float duration){
		float t = 0.0f;

		Color temp = fadeoutSprite.color;
		Color lerpedColor;

		while (t < 1.0f) {
			t += Time.deltaTime / duration;

			lerpedColor = Color.Lerp (temp, Color.black, t);
			fadeoutSprite.color = lerpedColor;

			yield return 0;
		}
	}
}
