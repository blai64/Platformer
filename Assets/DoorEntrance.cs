using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEntrance : MonoBehaviour {

	private SoundManager sm;

	void Awake () {
		sm = GameObject.Find ("SoundManager").GetComponent<SoundManager>();
	}

	public void PlaySound() {
		sm.PlaySound("door-open");
	}
}
