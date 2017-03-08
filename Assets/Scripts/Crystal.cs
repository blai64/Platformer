using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour {

	public float bobSpeed = 0.5f;
	public float bobAmplitude = 0.5f;

	private float posX;
	private float posY;
	private float posZ;

	private float timePassed;
	private float changeValue;

	void Start(){
		posX = transform.position.x;
		posY = transform.position.y;
		posZ = transform.position.z;
		changeValue = 0f;
		timePassed = 0f;
	}

	void Update () {
		timePassed = Time.time * bobSpeed;
		transform.position = new Vector3(posX, posY + CalculateChange (), posZ);
		transform.Rotate (new Vector3(0f, 1f, 0f));
	}

	private float CalculateChange() {
		changeValue = bobAmplitude * (Mathf.Sin (timePassed));
		return changeValue;
	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.name == "witch_char"){
			if(PlayerBehavior.instance.teleportCharges < 3)
				PlayerBehavior.instance.teleportCharges += 1;
			Destroy(this.gameObject);
		}
	}
}
