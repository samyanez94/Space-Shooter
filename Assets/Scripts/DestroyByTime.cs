using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{

	public float lifeTime;

	// Used to destroy explotions.
	void Start ()
	{
		Destroy (gameObject, lifeTime);
	}
}
