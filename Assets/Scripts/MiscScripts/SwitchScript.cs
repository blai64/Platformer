using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour {

	private bool isActive;

	private GameObject handle; 

	public bool IsActive{
		get{ return isActive;}
		set{ isActive = value;}
	}

	private float duration = 1.0f;
	private float snapToSwitchDuration = 0.25f;
	public float Duration{
		get{ return duration;}
		set{ duration = value;}
	}

	private float disableTime; 

	void Start () {
		isActive = false;
		handle = transform.Find ("lever_handle").gameObject;
	}

	void Update(){
		disableTime -= Time.deltaTime;
	}
		
	//If player is colliding and presses button, then change switch
	void OnTriggerStay (Collider other){
		if (other.gameObject.CompareTag ("Player") && 
			Input.GetKeyDown (KeyCode.L) &&
			disableTime < 0.0f) {
			isActive = !isActive;
			StartCoroutine (MoveSwitch ());
			StartCoroutine (MovePlayer ());
			disableTime = duration;
		}
	}

	IEnumerator MoveSwitch(){
		yield return new WaitForSeconds (0.5f);
		float t = 0.0f;
		float rotateDistance;

		Vector3 temp = handle.transform.rotation.eulerAngles;

		rotateDistance = -90;

		if (isActive)
			rotateDistance *= -1; 
		
		Quaternion toRot = Quaternion.Euler (temp.x, temp.y, temp.z + rotateDistance);
		Quaternion fromRot = handle.transform.rotation;

		while (t < 1.0f) {
			t += Time.deltaTime / duration;

			handle.transform.rotation = Quaternion.Lerp (fromRot, toRot, t);
			yield return 0;
		}
	}

	IEnumerator MovePlayer(){
		float t = 0.0f; 

		Vector3 oldPos = PlayerBehavior.instance.transform.position;
		Vector3 newPos = new Vector3 (transform.position.x, oldPos.y, oldPos.z);

		while (t < 1.0f) {
			t += Time.deltaTime / snapToSwitchDuration;
			PlayerBehavior.instance.transform.position = Vector3.Lerp (oldPos, newPos, t);

			yield return 0;
		}
	}

}
