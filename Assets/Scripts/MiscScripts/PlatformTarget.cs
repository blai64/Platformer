﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTarget : SwitchTarget{

	public float MoveDistance = 1.0f; 
	public int DefaultDirection = 1;



	// Use this for initialization
	override public void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	override public void Update () {
		base.Update ();
	}

	override public void Activate(bool activating){
		Debug.Log ("Activating platform");
		int direction = DefaultDirection;
		if (!activating)
			direction *= -1;

		Vector3 curPos = transform.position;
		Vector3 newPos = transform.position + new Vector3(0.0f, direction * MoveDistance, 0.0f);

		StartCoroutine (MovePlatform (curPos, newPos, base.Duration));
	}

	IEnumerator MovePlatform(Vector3 oldPos, Vector3 newPos, float duration){
		float t = 0.0f;

		while (t < 1.0f) {
			t += Time.deltaTime / duration;
			transform.position = Vector3.Lerp (oldPos, newPos, t);

			yield return 0;
		}
	}
}