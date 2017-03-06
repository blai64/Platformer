using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterBehavior : MonoBehaviour {

	public GameObject parentBone;

	// Components
	private Rigidbody rb;
	private Animator anim;

	private Vector3 throwForce;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();

		Reset ();
	}

	public void Disappear() {
		anim.SetBool ("isVisible", false);
	}

	public void Reset() {
		transform.parent = parentBone.transform;
		transform.localPosition = new Vector3 (-0.1f, 0f, -0.2f);
		transform.localRotation = new Quaternion (-0.6f, 0.1f, 0.1f, 0.8f);
		rb.isKinematic = true;
	}

	public void Summon() {
		anim.SetBool ("isVisible", true);
	}

	public void Release() {
		transform.parent = null;
		rb.isKinematic = false;
		rb.AddForce (throwForce);
	}

	public void SetForce(Vector3 force) {
		throwForce = force;
	}

	public void SetSingleForce(float force) {
		throwForce = -1f * transform.forward * force;
	}
}
