using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManouver : MonoBehaviour {

	private float targetManouver;
	private Rigidbody rb;
	private float currentSpeed;

	public float tilt;
	public float dodge;
	public float smoothing;
	public Vector2 startWait;
	public Vector2 manouverTime;
	public Vector2 manouverWait;
	public Boundary boundary;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		currentSpeed = rb.velocity.z;
		StartCoroutine (Evade ());
	}

	IEnumerator Evade () {
		yield return new WaitForSeconds(Random.Range (startWait.x, startWait.y));

		while (true) {
			targetManouver = Random.Range (1, dodge) * -Mathf.Sign(transform.position.x);
			yield return new WaitForSeconds (Random.Range (manouverTime.x, manouverTime.y));
			targetManouver = 0;
			yield return new WaitForSeconds (Random.Range (manouverWait.x, manouverWait.y));
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float newManouver = Mathf.MoveTowards (rb.velocity.x, targetManouver, Time.deltaTime * smoothing);
		rb.velocity = new Vector3 (newManouver, 0.0f, currentSpeed);
		rb.position = new Vector3 ( 
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0.0f, 
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
