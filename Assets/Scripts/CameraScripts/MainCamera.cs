using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	public static MainCamera instance = null;

	public float dampTime = 0.3f;
	public float xOffset = 0f;
	public float yOffset = 0f;

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
	}

	void Start(){
		cam = gameObject.GetComponent<Camera> ();
		transform.Find ("FadeOut").gameObject.GetComponent<Fade> ().FadeInOut (true);
	}

	void Update ()  {
		Vector3 destination = transform.position; 

		Vector3 teleporterLocation = cam.WorldToViewportPoint (teleportTarget.position);
		if (!(teleporterLocation.x > 0.1f && teleporterLocation.x < 0.9f &&
		    teleporterLocation.y > 0.1f && teleporterLocation.y < 0.9f))
			transform.position = destination - new Vector3 (0, 0, 0.05f);
		else if (teleporterLocation.x > 0.25f && teleporterLocation.x < 0.75f &&
			teleporterLocation.y > 0.25f && teleporterLocation.y < 0.75f && 
			destination.z < -8.5f)
			transform.position = destination + new Vector3 (0, 0, 0.05f);

		if (PlayerBehavior.instance.IsTeleporting () && teleportTarget) {
			Vector3 point = cam.WorldToViewportPoint(target.position);
			float direction = (float) pb.GetDirection ();
			Vector3 delta = teleportTarget.position - cam.ViewportToWorldPoint(new Vector3(0.5f - direction * xOffset,
				0.5f + yOffset,
				point.z));
			destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}

		else if (target && !PlayerBehavior.instance.IsTeleporting ()) {
			Vector3 point = cam.WorldToViewportPoint(target.position);
			float direction = (float) pb.GetDirection ();
			Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f - direction * xOffset,
																				   0.5f + yOffset,
																				   point.z));
			destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}

	}
}
