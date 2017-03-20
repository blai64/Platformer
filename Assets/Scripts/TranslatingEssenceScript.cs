using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslatingEssenceScript : MonoBehaviour {
	
	public GameObject Player;

	private float XDirection;
	private float YDirection;
	private float limit;

	void Update () {
		limit = PlayerBehavior.instance.GetEndX ();
		XDirection = PlayerBehavior.instance.GetXDistance ();
		YDirection = PlayerBehavior.instance.GetYDistance ();

		if (PlayerBehavior.instance.IsTeleporting ()) {
			Translate ();
		} else
			Disable ();
	}

	void Translate(){
		transform.Translate (XDirection / 50, YDirection / 50, 0);
		if (PlayerBehavior.instance.GetDirection() == 1 && transform.position.x > limit)
			PlayerBehavior.instance.StopTeleporting ();
		else if (PlayerBehavior.instance.GetDirection() == -1 && transform.position.x < limit)
			PlayerBehavior.instance.StopTeleporting ();
	}
	// disables aura and re-enables player
	void Disable() {
		this.gameObject.SetActive (false);
		Player.SetActive (true);
		PlayerBehavior.instance.StopTeleporting ();
	}
}
