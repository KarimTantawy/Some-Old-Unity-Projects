using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MovableCharacter {
	public static int enemiesActive = 0;

	protected virtual void OnEnable ()
	{
		EventManager.StartListening ("EnemiesTurn", EnemyMove);
	}

	protected override void Start()
	{
		base.Start ();
	}

	public static int ResetCount()
	{
		enemiesActive = GameObject.FindGameObjectsWithTag("Enemy").Length;
		enemiesActive += GameObject.FindGameObjectsWithTag("Stationary Enemy").Length;
		return enemiesActive;
	}

	void OnDisable()
	{
		EventManager.StopListening ("EnemyiesTurn", EnemyMove);
	}

	protected virtual void EnemyMove ()
	{
		enemiesActive -= 1;
	}

	protected abstract void Died ();
}
