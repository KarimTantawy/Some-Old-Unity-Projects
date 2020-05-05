using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovableCharacter : MonoBehaviour {
	public float moveTime = 0.1f;           
	public LayerMask blockingLayer;  

	protected bool reachedPosition, isMoving;

	protected bool ReachedPosition
	{
		get
		{
			return reachedPosition;
		}
		set 
		{
			reachedPosition = value;

			if (value == true) 
			{
				if (gameObject.tag == "Player" && GameManager.instance.PlayersTurn)
					GameManager.instance.PlayersTurn = false;
				else if (gameObject.tag == "Enemy")
					Enemy.enemiesActive -= 1;
			}
		}
	}

	protected Vector2 finalPosition;

	private BoxCollider2D boxCollider;      
	private Rigidbody2D rb2D;               
	private float inverseMoveTime;         
	private CharacterDirection charDirection;

	protected virtual void Start ()
	{
		charDirection = GetComponent<CharacterDirection> ();

		isMoving = false;

		reachedPosition = true;

		boxCollider = GetComponent <BoxCollider2D> ();

		rb2D = GetComponent <Rigidbody2D> ();

		inverseMoveTime = 1f / moveTime;
	}

	protected virtual bool Move (int xDir, int yDir, out RaycastHit2D hit)
	{
		Vector2 direction = new Vector2 (xDir, yDir);
		Vector2 start = transform.position;
		Vector2 end = start + direction;

		finalPosition = end;

		boxCollider.enabled = false;

		hit = Physics2D.Linecast (start, end, blockingLayer);

		boxCollider.enabled = true;

		if (gameObject.tag == "Player") {
			if (hit.transform == null) 
			{
				charDirection.Moving (direction, moveTime);
				StartCoroutine (SmoothMovement (end));
				return true;
			} else 
			{
				charDirection.AttemptMove (direction);
				return false;
			}
		}

		charDirection.Moving (direction, moveTime);
		StartCoroutine (SmoothMovement (end));

		return true;
	}

	protected IEnumerator SmoothMovement (Vector3 end)
	{
		ReachedPosition = false;

		isMoving = true;

		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

		while(sqrRemainingDistance > float.Epsilon)
		{
			Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);

			rb2D.MovePosition (newPosition);

			sqrRemainingDistance = (transform.position - end).sqrMagnitude;

			if (finalPosition == (Vector2)transform.position) 
			{
				ReachedPosition = true;
				isMoving = false;
			}
			
			yield return null;
		}
	}

	protected virtual GameObject AttemptMove (int xDir, int yDir)
	{
		RaycastHit2D hit;

		Move (xDir, yDir, out hit);

		if(hit.transform == null)
			return null;
		
		GameObject hitObject = hit.transform.gameObject;

		return hitObject;

	}
		
	protected abstract void OnCantMove();

}
