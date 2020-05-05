using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProjectile : MonoBehaviour {
	public float speed;
	public GameObject explosion;
	private Rigidbody2D rb;
	// Use this for initialization
	void Start () 
	{
		rb = gameObject.GetComponent<Rigidbody2D> ();
		Destroy (gameObject, 100f);
	}

	void FixedUpdate()
	{
		rb.MovePosition (transform.position - transform.up * Time.deltaTime * speed);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag != "Stationary Enemy") {
			GameObject explosionObject = Instantiate (explosion, transform.position, transform.rotation);
			explosionObject = SetExplosionSortOrder (explosionObject);
			Destroy(explosionObject, 0.3f);
			Enemy.enemiesActive -= 1;
			Destroy (gameObject);
		}
	}

	GameObject SetExplosionSortOrder(GameObject explosionObject)
	{
		GameObject temp = explosionObject;

		SpriteRenderer[] miniExplosions = temp.GetComponentsInChildren<SpriteRenderer>();

		foreach(SpriteRenderer sp in miniExplosions)
		{
			sp.sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
		}

		return temp;
	}
}
