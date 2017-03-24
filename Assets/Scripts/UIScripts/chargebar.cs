using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chargebar : MonoBehaviour {

	public Image crystal1;
	public Image crystal2;
	public Image crystal3;

	public Sprite active;
	public Sprite inactive;

	private int fillAmount;
	//private SpriteRenderer barSprite;
	// Use this for initialization
	void Start () {
		fillAmount = 3;
		crystal1.sprite = active;
		crystal2.sprite = active;
		crystal3.sprite = active;
	}
	
	// Update is called once per frame
	void Update () {
		//fillAmount = PlayerBehavior.instance.teleportCharges / 3.0f;
		//bar.fillAmount = fillAmount;
		fillAmount = PlayerBehavior.instance.teleportCharges;
		if (fillAmount == 0) {
			crystal1.sprite = inactive;
			crystal2.sprite = inactive;
			crystal3.sprite = inactive;
		} else if (fillAmount == 1) {
			crystal1.sprite = active;
			crystal2.sprite = inactive;
			crystal3.sprite = inactive;
		} else if (fillAmount == 2) {
			crystal1.sprite = active;
			crystal2.sprite = active;
			crystal3.sprite = inactive;
		} else if (fillAmount == 3) {
			crystal1.sprite = active;
			crystal2.sprite = active;
			crystal3.sprite = active;
		}

	}
}
