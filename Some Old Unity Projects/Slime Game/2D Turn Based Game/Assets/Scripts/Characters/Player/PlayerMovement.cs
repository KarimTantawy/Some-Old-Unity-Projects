using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovableCharacter {
	
	private void Update ()
	{
		//if (reachedPosition)
			//GameManager.instance.playersTurn = true;

		if(!GameManager.instance.PlayersTurn || isMoving == true || !GameManager.instance.playerControl) return;

		int horizontal = 0;
		int vertical = 0;

		if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow))
			vertical = 1;
		else if (Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow))
			horizontal = 1;
		else if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow))
			vertical = -1;
		else if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow))
			horizontal = -1;
		
		if (horizontal != 0) 
			vertical = 0;

		if (horizontal != 0 || vertical != 0) 
		{
			GameManager.instance.playerControl = false;
			PlayerAttemptMove (horizontal, vertical);
		}
	}

	void PlayerAttemptMove (int xDir, int yDir)
	{
		GameObject hitObject = AttemptMove (xDir, yDir);

		if (hitObject != null)
			OnCantMove ();
		
		GameManager.instance.playerControl = true;
	}

	protected override void OnCantMove ()
	{
		Debug.Log ("I'm Blocked!");
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Enemy") 
		{
			CharacterExplode ce = gameObject.GetComponent<CharacterExplode>();
			ce.Explode();
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			GameManager.instance.playerControl = false;
			GameManager.instance.PlayerDead = true;

		}
		else if (col.gameObject.tag == "Exit") 
			GameManager.instance.LoadNextScene ();
	}
}
