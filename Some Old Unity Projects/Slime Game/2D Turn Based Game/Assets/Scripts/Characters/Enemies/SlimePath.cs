using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePath : MonoBehaviour 
{
	public GameObject SlimePathsParent;
	public GameObject SquareSlimePathOrigin, SquareSlimePath, LongSlimePath;

	private EnemyMovement enemy;

	void Awake()
	{
		this.enabled = false;
	}

	void OnEnable () 
	{
		
		enemy = gameObject.GetComponent<EnemyMovement> ();

		Vector2[] positionsTmp = enemy.positions;
		Vector2[] tranPositionsTmp = enemy.translatedPositions;

		if (positionsTmp.Length == 0 || tranPositionsTmp.Length == 0) 
		{
			Instantiate (SquareSlimePath, transform.position, Quaternion.identity);
			return;
		}

		for (int i = 0; i < positionsTmp.Length; i++) 
		{
			if (positionsTmp [i] == (Vector2)transform.position)
				Instantiate (SquareSlimePathOrigin, positionsTmp [i], Quaternion.identity,  SlimePathsParent.transform);
			else
				Instantiate (SquareSlimePath, positionsTmp [i], Quaternion.identity,  SlimePathsParent.transform);

			if(i == 0)
				CreateSlimePath (transform.position, positionsTmp [i], tranPositionsTmp [i]);
			else
				CreateSlimePath (positionsTmp[i-1], positionsTmp [i], tranPositionsTmp [i]);

		}

	}

	void CreateSlimePath(Vector2 currentPos, Vector2 posToReach, Vector2 dist)
	{
		Vector2 direction = TranslateTransPositionTemp (dist);

		Vector3 rot = new Vector3 (0f, 0f, 0f);

		while (true) 
		{

			currentPos = currentPos + direction;

			if (currentPos == posToReach)
				break;
			
			if (direction.y == 1)
				rot = new Vector3 (0f, 0f, 180f);
			else if(direction.y == -1)
				rot = new Vector3 (0f, 0f, 0f);
			else if(direction.x == 1)
				rot = new Vector3 (0f, 0f, 90f);
			else if(direction.x == -1)
				rot = new Vector3 (0f, 0f, -90f);

			Instantiate (LongSlimePath, currentPos, Quaternion.Euler (rot), SlimePathsParent.transform);
		}
	}

	Vector2  TranslateTransPositionTemp (Vector2 tp)
	{
		int x = 0;
		int y = 0;

		if (tp.y > 0)
			y = (int)tp.y / (int)tp.y;
		else if(tp.y < 0)
			y = (int)tp.y / -(int)tp.y;
		else if (tp.x > 0)
			x = (int)tp.x / (int)tp.x;
		else if(tp.x < 0)
			x = (int)tp.x / -(int)tp.x;

		return new Vector2 (x, y);
	}
}
