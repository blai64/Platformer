using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterBehavior : MonoBehaviour {

	public GameObject parentBone;
	public static TeleporterBehavior instance;
	//public GameObject Projector;

	// Components
	private Rigidbody rb;
	private Animator anim;

	private Vector3 throwForce;

	public bool isGrounded = false;

	private SoundManager sm;

	void Awake () {
		sm = GameObject.Find ("SoundManager").GetComponent<SoundManager>();
	}

	void Start() {
		instance = this;
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		//Projector.SetActive(false);

		Reset ();
	}

	public void Disappear() {
		anim.SetBool ("isVisible", false);
		//Projector.SetActive(false);
	}

	public void Reset() {
		transform.parent = parentBone.transform;
		transform.localPosition = new Vector3 (-0.1f, 0f, -0.2f);
		transform.localRotation = new Quaternion (-0.6f, 0.1f, 0.1f, 0.8f);
		rb.isKinematic = true;
	}

	public void Summon() {
		anim.SetBool ("isVisible", true);
		//Projector.SetActive(true);
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

	//Collisions
	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Ground") {
			//transform.parent = col.transform;
			transform.position = new Vector3(transform.position.x,
											 transform.position.y + 0.01f,
											 transform.position.z);
			isGrounded = true;
			sm.PlaySound ("stone-drop");
		}
		if (col.gameObject.name == "witch_char" && isGrounded) {
			PlayerBehavior.instance.pickUp ();
		}
	}

	void OnCollisionExit(Collision col) {
		if (col.gameObject.tag == "Ground") {
			isGrounded = false;
		}
	}

	void Update() {
		//Projector.transform.position = new Vector3(transform.position.x,
		//											transform.position.y,
		//											Projector.transform.position.z);
	}
}
