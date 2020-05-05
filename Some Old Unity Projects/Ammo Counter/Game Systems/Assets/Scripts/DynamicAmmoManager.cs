using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAmmoManager : MonoBehaviour
{
	public float metreHeight;
	public int maxAmount;
	public GameObject AmmoInstance;
	public RectTransform startPos;

	private GameObject[] instances;
	private int currentCount;
	private int currentBullet;
	private int prevAmount;

	public int CurrentBullet
	{
		get
		{
			return currentBullet;
		}
		set
		{
			currentBullet = value;

			if (currentBullet < 0)
				currentBullet = currentCount-1;
		}	
	}

	void Start()
	{
		if (AmmoInstance == null || metreHeight == 0 || maxAmount <= 0)
			Destroy (this);
		
		instances = new GameObject[maxAmount];

		currentCount = maxAmount - 1;
		CurrentBullet = currentBullet;

		Populate ();

		SetAmmo (12);

	}

	public void SetAmmo(int amount)
	{
		if (amount <= 0)
			amount = prevAmount;
		else
			prevAmount = amount;
		
		float instanceHeight = metreHeight / (float)amount;
		Vector2 height = new Vector2 (instances [0].GetComponent<RectTransform> ().rect.width, instanceHeight);

		currentCount = amount-1;
		CurrentBullet = amount-1;

		instances [0].SetActive (true);
		instances [0].GetComponent<Animator> ().SetBool ("BulletFired", false);
		instances [0].GetComponent<RectTransform> ().sizeDelta = height;

		for (int i = 1; i < amount; i++) 
		{
			instances [i].SetActive (true);
			instances [i].GetComponent<Animator> ().SetBool ("BulletFired", false);
			instances [i].GetComponent<RectTransform> ().anchoredPosition = new Vector3(0f, instanceHeight / 2, 0f);
			instances [i].GetComponent<RectTransform> ().sizeDelta = height;
		}

		for (int i = amount; i < maxAmount; i++) 
		{
			if(instances [i].GetComponent<Animator> ().GetBool ("BulletFired"))
				instances [i].GetComponent<Animator> ().SetBool ("BulletFired", false);

			instances [i].SetActive (false);
		}
			
	}

	void Populate()
	{

		instances [0] = (GameObject)Instantiate (AmmoInstance, transform.position, Quaternion.identity);
		instances [0].transform.SetParent (gameObject.transform, false);
		instances [0].GetComponent<RectTransform> ().anchoredPosition = startPos.anchoredPosition;
		instances [0].GetComponent<RectTransform> ().localScale = new Vector3 (3f, 3f, 0f);

		Transform previous = instances[0].transform;

		for (int i = 1; i < instances.Length; i++) 
		{
			instances [i] = (GameObject)Instantiate (AmmoInstance, transform.position, Quaternion.identity);
			instances [i].transform.SetParent (previous, false);
			instances [i].GetComponent<RectTransform> ().anchoredPosition = new Vector3(0f, 0f, 0f);
			instances [i].GetComponent<RectTransform> ().localScale = new Vector3 (1f, 1f, 0f);
			instances [i].name = "Bullet " + (i+1).ToString ();

			previous = instances [i].transform;
		}


		foreach (GameObject g in instances) 
		{
			g.SetActive (false);
		}

	}

	public void ShotFired()
	{
		if (instances == null)
			return;

		instances [CurrentBullet].GetComponent<Animator> ().SetBool ("BulletFired", true);

		CurrentBullet -= 1;
	}
}
