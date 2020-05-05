using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Enemy {
	public Vector2[] positions = new Vector2[1];
	public Vector2[] translatedPositions;

	private int i;
	private int tempI;
	private SlimePath spReference;

	protected override void Start () 
	{
		base.Start ();

		i = positions.Length-1;
		tempI = 0;

		translatedPositions = new Vector2[positions.Length];

		if(positions.Length > 0)
			Translate ();

		SlimePath spReference;
		spReference = gameObject.GetComponent<SlimePath> ();
		spReference.enabled = true;
	}

	void Translate()
	{
		Vector2 prevPosition = transform.position;

		for(int j = 0; j <= i; j++) 
		{
			translatedPositions [j] = positions [j] - prevPosition;
			prevPosition = positions [j];
		}
	}

	protected override void EnemyMove ()
	{
		if (positions.Length == 0 || translatedPositions.Length == 0) 
		{
			enemiesActive -= 1;
			return;
		}
		
		RaycastHit2D hit;

		if (!isMoving) {
			if (tempI > i)
				tempI = 0;
			
			finalPosition = transform.position + (Vector3)translatedPositions [tempI];
			Move ((int)translatedPositions [tempI].x, (int)translatedPositions [tempI].y, out hit);
			tempI += 1;
		}

	}

	protected override void Died ()
	{
		Debug.Log ("I dead");
	}

	protected override void OnCantMove ()
	{
		Debug.Log ("Can't Move");
	}

}
