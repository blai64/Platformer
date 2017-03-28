using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConditions : MonoBehaviour {

	public GameObject Witch;
	private Animator anim;

	void Start () {
		anim = Witch.GetComponent<Animator>();
		anim.SetTrigger("hasEntered");
	}
}
