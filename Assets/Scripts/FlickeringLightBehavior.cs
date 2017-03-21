using UnityEngine;
using System.Collections;

public class FlickeringLightBehavior : MonoBehaviour {

	public float speed = 5f;

	private Light newLight;
	private Color originalColor;
	private float timePassed;
	private float changeValue;
	private float offset = 0.95f;

	void Start(){

		newLight = GetComponent<Light> ();

		if (newLight != null) {
			originalColor = newLight.color;
		} else {
			enabled = false;
			return;
		}

		changeValue = 0f;
		timePassed = 0f;
	}

	void Update () {
		timePassed = Time.time;
		timePassed = timePassed - Mathf.Floor (timePassed);
		newLight.color = originalColor * CalculateChange ();
	}

	private float CalculateChange() {
		changeValue = -Mathf.Sin (timePassed * speed * Mathf.PI) * 0.05f + offset;
		return changeValue;
	}
}