﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	public static MainCamera instance = null;

	public List<Transform> targetsOnScreen; 

	public float dampTime = 0.3f;
	public float xOffset = 0f;
	public float yOffset = 0f;
	public float destinationZ;

	public float DampTime {
		get { return dampTime; } 
		set { dampTime = value; } 
	}

	private Vector3 velocity = Vector3.zero;
	public Transform target;
	public Transform teleportTarget;
	private PlayerBehavior pb;

	private Camera cam;

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

		transform.Find ("FadeOut").gameObject.SetActive (true);
		pb = target.gameObject.GetComponent<PlayerBehavior> ();

		transform.Find ("FadeOut").gameObject.GetComponent<Fade> ().FadeInOut (true,false);
	}

	void Start(){
		cam = gameObject.GetComponent<Camera> ();
		destinationZ = transform.position.z; 
	}

	void Update ()  {
		Vector3 destination = transform.position; 
		/*
		Vector3 teleporterLocation;
		int zoomIn = 0;
		foreach (Transform t in targetsOnScreen) {
			teleporterLocation = cam.WorldToViewportPoint (t.position);
			if (!(teleporterLocation.x > 0.05f && teleporterLocation.x < 0.95f &&
				teleporterLocation.y > 0.05f && teleporterLocation.y < 0.95f)) {
				transform.position = destination - new Vector3 (0, 0, 0.1f);

			} else if (teleporterLocation.x > 0.2f && teleporterLocation.x < 0.8f &&
				teleporterLocation.y > 0.2f && teleporterLocation.y < 0.8f &&
				destination.z < -8.5f) {
				zoomIn++;

			}
		}

		if (zoomIn >= targetsOnScreen.Count && zoomIn > 0)
			transform.position = destination + new Vector3 (0, 0, 0.1f);
		*/

		if (PlayerBehavior.instance.IsTeleporting () && teleportTarget) {
			Vector3 point = cam.WorldToViewportPoint(target.position);
			float direction = (float) pb.GetDirection ();
			Vector3 delta = teleportTarget.position - cam.ViewportToWorldPoint(new Vector3(0.5f - direction * xOffset,
				0.5f + yOffset,
				point.z));
			destination = transform.position + delta;
			destination.z = destinationZ; 
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}


		else if (target && !PlayerBehavior.instance.IsTeleporting ()) {
			Vector3 point = cam.WorldToViewportPoint(target.position);
			float direction = (float) pb.GetDirection ();
			Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f - direction * xOffset,
				0.5f + yOffset,
				point.z));
			destination = transform.position + delta;
			destination.z = destinationZ; 
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}

	}
		

	public void MoveCamera(Vector3 delta){
		//transform.position = Vector3.SmoothDamp (transform.position, transform.position + delta, ref velocity, dampTime);
		destinationZ = transform.position.z + delta.z;
		xOffset = xOffset + delta.x;
		yOffset = yOffset + delta.y;
	}
}
