using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StationaryObjectSort : MonoBehaviour {
	public int numberOfOrderLayers = 0;
	public bool setStationaryObjects = false;

	void Update () 
	{
		if (setStationaryObjects) 
		{
			SetStationaryObjectsOrder ();
			setStationaryObjects = false;
		}
	}

	void SetStationaryObjectsOrder()
	{
		if (numberOfOrderLayers == 0)
			numberOfOrderLayers = 1000;

		GameObject[] walls = GameObject.FindGameObjectsWithTag ("Wall");
		GameObject[] floors = GameObject.FindGameObjectsWithTag ("Floor");
		GameObject[] stationaryEnemies = GameObject.FindGameObjectsWithTag ("Stationary Enemy");

		foreach (GameObject o in walls) 
		{
			int num = numberOfOrderLayers;
			int yValue = (int)o.transform.position.y;

			num -= yValue;

			o.GetComponent<SpriteRenderer> ().sortingOrder = num;
			o.GetComponent<SpriteRenderer> ().sortingLayerName = "walls";
		}

		foreach (GameObject o in floors) 
		{
			o.GetComponent<SpriteRenderer> ().sortingOrder = 0;
		}

		foreach (GameObject o in stationaryEnemies) 
		{
			int num = numberOfOrderLayers;
			int yValue = (int)o.transform.position.y;

			num -= yValue;

			o.GetComponent<SpriteRenderer> ().sortingOrder = num;
		}
	}
}
