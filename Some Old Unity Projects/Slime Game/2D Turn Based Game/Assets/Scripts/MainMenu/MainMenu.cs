﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
	void Update () {
		if (Input.GetKeyDown (KeyCode.E))
			GameManager.instance.LoadNextScene ();
	}
}
