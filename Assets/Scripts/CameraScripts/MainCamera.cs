using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	public static MainCamera instance = null;

	private float dampTime = 0.3f;

	public float DampTime {
		get { return dampTime; } 
		set { dampTime = value; } 
	}
		
	private Vector3 velocity = Vector3.zero;
	public Transform target;

	private Camera cam;

	void Awake()
	{
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy (gameObject); 

		transform.Find ("FadeOut").gameObject.SetActive (true);
	}

	void Start(){
		Debug.Log ("start");
		cam = gameObject.GetComponent<Camera> ();
		transform.Find ("FadeOut").gameObject.GetComponent<Fade> ().FadeInOut (true);
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
