using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour {
	public static DeathManager instance = null;
	public GameObject chargeParticles;

	private float particleDuration = 3.0f;

	void Awake()
	{
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern,
			//meaning there can only ever be one instance of a GameManager.
			Destroy (gameObject); 
	}


	public void SpawnParticles(Vector3 location){
		GameObject particles = Instantiate (chargeParticles, location, Quaternion.identity);
		EmitParticles (particles, true);

		StartCoroutine (EndEmission (particles));


	}

	private void EmitParticles(GameObject particles, bool val) {
		GameObject particle;
		ParticleSystem.EmissionModule em;
		for (int i = 0; i < particles.transform.childCount; i++) {
			particle = particles.transform.GetChild (i).gameObject; 
			em = particle.GetComponent<ParticleSystem> ().emission;
			em.enabled = val;
		}

	}
	IEnumerator EndEmission(GameObject particles){
		yield return new WaitForSeconds (particleDuration);
		EmitParticles (particles, false);
		yield return new WaitForSeconds (1.0f);
		Destroy (particles);
	}
}
