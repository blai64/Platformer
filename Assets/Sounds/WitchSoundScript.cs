using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSoundScript : MonoBehaviour {

	private SoundManager sm;

	void Awake () {
		sm = GameObject.Find ("SoundManager").GetComponent<SoundManager>();
	}
	
	public void PlayFootStep1() {
		sm.PlaySound ("footstep-1");
	}

	public void PlayFootStep2() {
		sm.PlaySound ("footstep-2");
	}

	public void PlayJumpThrow() {
		sm.PlaySound ("jump-throw");
	}

	public void PlayCrystalCollect() {
		sm.PlaySound ("collect-crystal");
	}

	public void PlayDeath() {
		sm.PlaySound ("death");
	}

	public void PlayLever() {
		sm.PlaySound ("lever");
	}

	public void PlayTeleport() {
		sm.PlaySound ("teleport");
	}
}
