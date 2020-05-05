using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Test1 : MonoBehaviour {
	//EventManager.TriggerEvent("test");
	private int mn;

	void Awake()
	{
		mn = 4234;
	}

	void OnEnable ()
	{
		EventManager.StartListening ("test", SomeFunction);
		EventManager.StartListening ("test", SomeFunction1);

	}

	void OnDisable()
	{
		EventManager.StopListening ("test", SomeFunction);
		EventManager.StopListening ("test", SomeFunction1);
	}

	void SomeFunction()
	{
		Debug.Log ("I've been called!!!!(SomeFunction, Test1)");
		Debug.Log (mn);
	}

	void SomeFunction1()
	{
		Debug.Log ("1");
	}
}
