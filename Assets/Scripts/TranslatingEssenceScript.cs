using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslatingEssenceScript : MonoBehaviour {
	
	public GameObject Player;
	public GameObject chargeParticles;

	private float XDirection;
	private float YDirection;
	private float limit;

	void Start(){
		EmitParticles (false);
	}

	void Update () {
		limit = PlayerBehavior.instance.GetEndX ();
		XDirection = PlayerBehavior.instance.GetXDistance ();
		YDirection = PlayerBehavior.instance.GetYDistance () + 0.5f; //make the ball move slightly above teleporter

		if (PlayerBehavior.instance.IsTeleporting ()) {
			Translate ();
		} else
			Disable ();
	}

	void Translate(){
		Debug.Log ("translating");
		transform.Translate (XDirection / 100, YDirection / 100, 0);
		if (PlayerBehavior.instance.leftOfTeleporter() && transform.position.x > limit)
			PlayerBehavior.instance.StopTeleporting ();
		else if (!PlayerBehavior.instance.leftOfTeleporter() && transform.position.x < limit)
			PlayerBehavior.instance.StopTeleporting ();
	}
	// disables aura and re-enables player
	void Disable() {
		this.transform.GetChild (0).gameObject.SetActive (false);
		EmitParticles (false);
		//this.gameObject.SetActive (false);
		Player.SetActive (true);
		//PlayerBehavior.instance.StopTeleporting ();
	}

	public void Activate(){
		this.transform.GetChild (0).gameObject.SetActive (true);
		EmitParticles (true);
	}

	private void EmitParticles(bool val){
		GameObject particle;
		ParticleSystem.EmissionModule em;
		for (int i = 0; i < chargeParticles.transform.childCount; i++) {
			particle = chargeParticles.transform.GetChild (i).gameObject; 
			em = particle.GetComponent<ParticleSystem> ().emission;
			em.enabled = val;
		}

	}

}
