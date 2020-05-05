using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public GameObject gameOverScreen;
	public GameObject transitionScreen;
	public bool playerControl;

	private static bool playersTurn, pressed, playerDead;
	private GameObject[] enemies;

	public bool PlayersTurn
	{
		get 
		{
			return playersTurn;
		}

		set 
		{
			playersTurn = value;

			if (playersTurn == false) 
			{
				Enemy.ResetCount ();
				EventManager.TriggerEvent ("EnemiesTurn");
				Debug.Log ("Enemies Turn");
			}
		}
	}

	public bool PlayerDead
	{
		get 
		{
			return playerDead;
		}

		set 
		{
			playerDead = value;

			if (playerDead == true) 
			{
				GameOver ();
			}
		}
	}

	void Awake () 
	{
		if (instance == null)
			instance = this;
		else
			Destroy (gameObject);

		gameOverScreen.SetActive(false);

		pressed = false;

		transitionScreen.GetComponent<Animation>() ["transitionOpen"].speed = 1;
		transitionScreen.GetComponent<Animation>().Play ("transitionOpen");

		playerControl = true;
		PlayersTurn = true;
		PlayerDead = false;
			
	}
	
	void Update () 
	{
		if (Enemy.enemiesActive <= 0)
			PlayersTurn = true;

		if (Input.GetKeyDown (KeyCode.E) && !pressed && SceneManager.GetActiveScene().buildIndex != 0) 
		{
			RestartLevel ();	
			pressed = true;
		}

	}

	void RestartLevel ()
	{
		TransitionClose ();
		StartCoroutine(TransitionToRestart ());
	}

	void GameOver()
	{
		StartCoroutine (GameOverScreen ());
	}

	void TransitionClose()
	{
		transitionScreen.GetComponent<Animation>() ["transitionOpen"].speed = -1;
		transitionScreen.GetComponent<Animation>() ["transitionOpen"].time = transitionScreen.GetComponent<Animation>() ["transitionOpen"].length;
		transitionScreen.GetComponent<Animation>().Play ("transitionOpen");
	}

	IEnumerator TransitionToRestart()
	{
		yield return new WaitForSeconds (2f);
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	IEnumerator GameOverScreen()
	{
		yield return new WaitForSeconds (2f);
		if(!pressed)
			gameOverScreen.SetActive(true);
	}

	IEnumerator LoadNextSceneTransition()
	{
		yield return new WaitForSeconds (2f);
		//if (SceneManager.sceneCount >= (SceneManager.GetActiveScene ().buildIndex + 1))
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		//else
			//Application.Quit ();
	}

	public void LoadNextScene()
	{
		TransitionClose ();
		StartCoroutine(LoadNextSceneTransition ());
	}

}
