using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSoundScript : MonoBehaviour {

	public GameObject RenderObj;
	private SoundManager sm;

	void Awake () {
		sm = GameObject.Find ("SoundManager").GetComponent<SoundManager>();
	}

	public void PlayArmor() {
		if (IsVisible())
			sm.PlaySound ("armor-1");
	}

	public void PlayArmor2() {
		if (IsVisible())
			sm.PlaySound ("armor-2");
	}

	public void PlayArmor3() {
		if (IsVisible())
			sm.PlaySound ("armor-3");
	}

	public void PlayAttack() {
		if (IsVisible())
			sm.PlaySound ("knight-attack");
	}

	private bool IsVisible() {
		return RenderObj.GetComponent<MeshRenderer> ().isVisible;
	}
}
