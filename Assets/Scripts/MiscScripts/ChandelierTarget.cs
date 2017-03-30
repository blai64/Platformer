using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChandelierTarget : SwitchTarget {

	public float MoveDistance = 1.0f; 
	public int DefaultDirection = 1;

	private SoundManager sm;

	private Vector3 originalPosition;

	override public void Start () {
		base.Start ();
		sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
		originalPosition = transform.position;
	}

	override public void Update () {
		base.Update ();
	}

	override public void Activate(bool activating){
		StartCoroutine (DropChandelier());

		StartCoroutine (ReturnChandelier (originalPosition, base.Duration));
	}

	IEnumerator DropChandelier(){
		yield return new WaitForSeconds (0.5f);
		gameObject.tag = "Killer";
		GetComponent<Rigidbody> ().useGravity = true;
	}

	IEnumerator ReturnChandelier(Vector3 newPos, float duration){
		yield return new WaitForSeconds (2.0f);

		gameObject.tag = "Untagged";
		GetComponent<Rigidbody> ().useGravity = false;

		yield return new WaitForSeconds (0.5f);

		Vector3 oldPos = transform.position;



		duration = duration * 3;
		float t = 0.0f;

		sm.PlaySound("platform");
	
		while (t < 1.0f) {
			t += Time.deltaTime / duration;
			transform.position = Vector3.Lerp (oldPos, newPos, t);

			yield return 0;
		}
		transform.position = newPos;
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		sm.StopSound("platform");
		base.MakeSwitchAvailable ();

	}
}
