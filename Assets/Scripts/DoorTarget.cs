using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTarget : SwitchTarget {

	public GameObject Player;
	public float RotateSpeed = 2f; 

	private PlayerBehavior pb;
	private bool open = false;

	private SoundManager sm;

	void Awake () {
		sm = GameObject.Find ("SoundManager").GetComponent<SoundManager>();
	}

	override public void Start () {
		base.Start ();
		pb = Player.GetComponent<PlayerBehavior> ();
	}

	override public void Update () {
		base.Update ();
	}

	override public void Activate (bool activating) {
		if (activating && !open) {
			sm.PlaySound ("door-open");
			StartCoroutine (RotateDoor (base.Duration));
			open = true;
		}
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

	public void PlaySound() {
		sm.PlaySound("door-open");
	}
}
