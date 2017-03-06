using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	private float dampTime = 0.3f;

	public float DampTime {
		get { return dampTime; } 
		set { dampTime = value; } 
	}
		
	private Vector3 velocity = Vector3.zero;
	public Transform target;

	private Camera cam;

	void Start(){
		cam = gameObject.GetComponent<Camera> ();
	}

	void Update ()  {
		if (target) {
			Vector3 point = cam.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}

	}
}
