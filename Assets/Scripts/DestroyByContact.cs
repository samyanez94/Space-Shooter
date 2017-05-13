using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
	private GameController gameController;

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;

	void Start ()
	{
		// Initializing the GameController object
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");

		if (gameControllerObject != null)
			gameController = gameControllerObject.GetComponent <GameController> ();
		else
			Debug.Log ("Cannot find 'GameController' script");
	}

	void OnTriggerEnter (Collider other)
	{
		// Explode hazards if the collide with something (shots)
		if (other.tag != "Boundary" && other.tag != "Enemy") {
			if (explosion != null)
				Instantiate (explosion, transform.position, transform.rotation);

			// Also explode player if it's the other object.
			if (other.tag == "Player") {
				Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
				// Call game over on GameController.
				gameController.StartCoroutine (gameController.GameOver ());

			} else
				// Update the score
				gameController.updateScore (scoreValue);
			// Destroy those guys
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}
