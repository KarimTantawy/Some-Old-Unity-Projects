using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExplode : MonoBehaviour {
	public int amount;
	public GameObject viscera, explosion;
	public float explosionStrengthMax, explosionStrengthMin;
	public Sprite[] sprites = new Sprite[1];

	public void Explode () 
	{
		GameObject tmp;

		Destroy(Instantiate (explosion, transform.position, Quaternion.identity), 0.3f);

		if (sprites.Length <= 0)
			return;

		for(int i = 0; i <= amount; i++)
		{
			tmp = Instantiate (viscera, transform.position, Quaternion.identity);

			tmp.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, (sprites.Length-1))];
			tmp.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0f, 360f)));
			tmp.GetComponent<Rigidbody2D> ().AddRelativeForce (Vector2.up * Random.Range (explosionStrengthMin, explosionStrengthMax));

			Destroy (tmp, Random.Range (4f, 10f));
		}

	}
}
