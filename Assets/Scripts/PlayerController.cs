using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Public class to declare our boundaries
[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{

	private Rigidbody rb;
	private Quaternion calibrationQuaternion;
	private AudioSource audioSource;
	private float nextFire;

	public GameObject shot;
	//	public SimpleTouchPad touchPad;
	//	public SimpleTouchAreaButton fireButton;
	public Transform shotSpawn;
	public Boundary boundary;
	public float fireDelta;
	public float speed;
	public float tilt;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		CalibrateAccelerometer ();
		audioSource = GetComponent<AudioSource > ();
	}

	void Update ()
	{
		// Instantiates a bolt 'shot' at the shotSpawn position.
		if (Input.GetButton ("Fire1") && (Time.time > nextFire)) {
			nextFire = Time.time + fireDelta;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play ();
		}
	}

	// Use this when working with RigidBodies
	void FixedUpdate ()
	{
//		float moveHorizontal = Input.GetAxis ("Horizontal");
//		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 accelerationRaw = Input.acceleration;

		Vector3 acceleration = FixAcceleration (accelerationRaw);

//		Vector2 direction = touchPad.GetDirection ();

		rb.velocity = new Vector3 (acceleration.x, 0.0f, acceleration.y) * speed;

		// Bounds the x and y player position between our boundaries.
		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);

		// Tilts the player ship when there's a velocity.
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}

	// Used to calibrate Input.acceleration input
	void CalibrateAccelerometer ()
	{
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}

	// Get the 'calibrated' value from the Input
	Vector3 FixAcceleration (Vector3 acceleration)
	{
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}
}
