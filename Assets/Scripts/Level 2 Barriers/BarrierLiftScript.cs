using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierLiftScript : MonoBehaviour {
	public GameObject Lever;

	public GameObject Player;

	private PlayerBehavior pb;


	override public void Start () {
		pb = Player.GetComponent<PlayerBehavior> ();
	}

	override public void Update () {

	}

	override public void Activate (bool activating) {
		

	}
}