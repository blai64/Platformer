using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConditions : MonoBehaviour {

	public GameObject Witch;
	private PlayerBehavior pb;
	private Animator anim;

	void Start () {
		pb = Witch.GetComponent<PlayerBehavior>();
		anim = Witch.GetComponent<Animator>();
		anim.SetTrigger("hasEntered");
	}
}
