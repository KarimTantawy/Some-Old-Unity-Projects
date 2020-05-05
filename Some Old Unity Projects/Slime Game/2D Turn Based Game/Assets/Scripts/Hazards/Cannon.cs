using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Enemy {
	public int waitNTurns;
	public Sprite[] sprites = new Sprite[2];
	public GameObject projectile;
	public GameObject barrelExplosion;

	public Transform barrel;

	public enum directions {down, up};
	public directions cannonDirection = directions.down;

	private int waitAmount;

	protected override void Start () {

		waitAmount = waitNTurns;
			
		switch (cannonDirection) 
		{
			case directions.down:
				Down ();
				break;
			case directions.up:
				Up();
				break;
			/*
			case directions.right:
				Right ();
				break;
			case directions.left:
				Left ();
				break;
			*/
			default:
				Debug.Log ("Cannon has no direction. Karim you idiot.");
				break;
		}
	}

	void Down()
	{
		gameObject.GetComponent<SpriteRenderer> ().sprite = sprites [0];
	}

	void Up()
	{
		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer> ();

		sr.sprite = sprites [1];
		sr.flipY = true;
		sr.flipX = true;

		transform.eulerAngles = new Vector3(0f, 0f, 180f);
	}

	protected override void EnemyMove()
	{
		if (waitAmount > 0) 
		{
			SkipTurn ();
			return;
		}

		waitAmount = waitNTurns;
		Attack ();

	}

	protected override void Died()
	{
		enemiesActive -= 1;
		Debug.Log ("I die");
	}

	protected override void OnCantMove ()
	{
		Debug.Log ("I'm Blocked!");
	}

	void Attack()
	{
		GameObject cannonBall = Instantiate (projectile, barrel.position, barrel.rotation, gameObject.transform);
		GameObject explosion = Instantiate (barrelExplosion, barrel.position, barrel.rotation, gameObject.transform);

		int sortOrder = 0;

		if (cannonDirection == directions.down)
			sortOrder = gameObject.GetComponent<SpriteRenderer> ().sortingOrder;
		else if(cannonDirection == directions.up)
			sortOrder = gameObject.GetComponent<SpriteRenderer> ().sortingOrder-1;

		cannonBall.GetComponent<SpriteRenderer> ().sortingOrder = sortOrder;
		explosion.GetComponent<SpriteRenderer> ().sortingOrder = sortOrder+2;

		Destroy(explosion, 0.3f);
	}

	void SkipTurn()
	{
		waitAmount -= 1;
		enemiesActive -= 1;
	}
}
