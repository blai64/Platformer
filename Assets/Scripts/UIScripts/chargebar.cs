using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chargebar : MonoBehaviour {

	public Image bar;
	private float fillAmount;
	//private SpriteRenderer barSprite;
	// Use this for initialization
	void Start () {
		fillAmount = 1.0f;
		//barSprite = bar.GetComponent<SpriteRenderer> ();
		bar.fillAmount = fillAmount;
	}
	
	// Update is called once per frame
	void Update () {
		fillAmount = PlayerBehavior.instance.teleportCharges / 3.0f;
		bar.fillAmount = fillAmount;
	}
}
