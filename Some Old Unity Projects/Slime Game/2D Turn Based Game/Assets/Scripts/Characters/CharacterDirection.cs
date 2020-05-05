using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDirection : MonoBehaviour {
	
	public float attemptMoveAnimationDuration;
	public enum Direction {Up, Down, Left, Right};
	public Direction characterDirection;

	private Animator anim;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		characterDirection = Direction.Down;
	}

	public void Moving(Vector2 direction, float moveTime)
	{
		StartCoroutine (DirectionMove(direction, moveTime));
	}

	public void AttemptMove(Vector2 direction)
	{
		StartCoroutine (AttemptMoveAnim (direction));
	}

	IEnumerator DirectionMove(Vector2 direction, float moveTime)
	{
		anim.SetBool ("CharacterMoving", true);
		anim.SetFloat ("MoveX", direction.x);
		anim.SetFloat ("MoveY", direction.y);

		yield return new WaitForSeconds (moveTime);

		anim.SetBool ("CharacterMoving", false);
	}

	IEnumerator AttemptMoveAnim(Vector2 direction)
	{
		anim.SetBool ("AttemptMove", true);
		anim.SetFloat ("MoveX", direction.x);
		anim.SetFloat ("MoveY", direction.y);

		yield return new WaitForSeconds (attemptMoveAnimationDuration);

		anim.SetBool ("AttemptMove", false);
	}
}
