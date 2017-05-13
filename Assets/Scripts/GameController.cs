using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public Text scoreText;
	public Text gameOverText;
	public Text restartText;
	public RestartTouchArea restartButton;

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public float startWait;
	public float waveWait;
	public float spawnWait;
	public float restartWait;
	public int hazardCount;

	private bool gameOver;

	private int score;

	// Initializes variables and set ups the game.
	void Start ()
	{
		gameOver = false;
		gameOverText.text = "";
		restartText.text = "";
		restartButton.gameObject.SetActive (false);

		score = 0;
		scoreText.text = "Score: " + score;

		//Starts a Co-routine that will yield before spawnning enemied.
		StartCoroutine (SpawnWaves ());
	}

	// This function is called every frame. It checks if a game restart is needed.
	//	void Update ()
	//	{
	//		if (gameOver && Input.GetKeyDown (KeyCode.R))
	//			SceneManager.LoadScene (0, LoadSceneMode.Single);
	//	}

	// Spawns the waves of hazards.
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
	
		while (!gameOver) {
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Instantiate (hazard, spawnPosition, Quaternion.identity);

				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
		}
	}

	// Public funtion that updates the score.
	public void updateScore (int newValue)
	{
		score += newValue;
		scoreText.text = "Score: " + score;
	}

	// Co-routine that ends the game.
	public IEnumerator GameOver ()
	{
		gameOver = true;
		gameOverText.text = "Game Over";

		yield return new WaitForSeconds (restartWait);
		restartText.text = "Tap anywhere to restart";
		restartButton.gameObject.SetActive (true);
	}

	public void RestartGame ()
	{
		SceneManager.LoadScene (0, LoadSceneMode.Single);
	}
}