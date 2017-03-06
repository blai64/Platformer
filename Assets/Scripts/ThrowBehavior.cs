using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBehavior : MonoBehaviour {

	public GameObject Teleporter;
	private TeleporterBehavior tb;

	void Start() {
		tb = Teleporter.GetComponent<TeleporterBehavior> ();
	}

	public void SummonObject() {
		tb.Summon ();
	}

	public void ThrowObject() {
		tb.Release ();
	}
}
