using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTarget : SwitchTarget {

	public GameObject Player;
	public float RotateSpeed = 3f; 
	public int DefaultDirection = 1;

	private PlayerBehavior pb;
	private bool open = false;

	override public void Start () {
		base.Start ();
		pb = Player.GetComponent<PlayerBehavior> ();
	}

	override public void Update () {
		base.Update ();
	}

	override public void Activate (bool activating) {
		if (activating && !open){
			StartCoroutine (RotateDoor (base.Duration));
			open = true;}

	}

	IEnumerator RotateDoor(float duration){
		yield return new WaitForSeconds (0.5f);

		float t = 0.0f;

		while (t < 1.0f) {
			transform.Rotate (new Vector3(0f, RotateSpeed, 0f));
			t += Time.deltaTime / duration;

			yield return 0;
		}

		pb.CanExit ();
	}
}
