using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {
	public static GameControl control;
	public float health;
	public float experience;

	private bool isLoaded = false;

	void Awake () 
	{
		if (control == null) {
			control = this;
		} else
			Destroy (gameObject);
	}

	void Start()
	{
		if (isLoaded == true) {
			Debug.Log (health);
			Debug.Log (experience);
			isLoaded = false;
		}	
	}

	void OnEnable()
	{
		Load ();
		isLoaded = true;
	}

	void OnDisable()
	{
		Save ();
	}

	public void Save () 
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data = new PlayerData (health, experience);

		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load()
	{
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();

			health = data.health;
			experience = data.experience;
		}
	}
}

[System.Serializable]
class PlayerData
{
	public float health;
	public float experience;

	public PlayerData(float health, float experience)
	{
		this.health = health;
		this.experience = experience;
	}
}
