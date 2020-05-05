using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
	private DynamicAmmoManager dam;

	public void Start()
	{
		dam = gameObject.GetComponent<DynamicAmmoManager>();
	}

	public void Shoot()
	{
		dam.ShotFired();
	}

	public void Reload()
	{
		dam.SetAmmo (0);
	}

	public void One()
	{
		dam.SetAmmo (1);
	}

	public void Two()
	{
		dam.SetAmmo (2);
	}

	public void Three()
	{
		dam.SetAmmo (3);
	}

	public void Four()
	{
		dam.SetAmmo (4);
	}

	public void Five()
	{
		dam.SetAmmo (5);
	}

	public void Six()
	{
		dam.SetAmmo (6);
	}

	public void Seven()
	{
		dam.SetAmmo (7);
	}

	public void Eight()
	{
		dam.SetAmmo (8);
	}

	public void Nine()
	{
		dam.SetAmmo (9);
	}

	public void Ten()
	{
		dam.SetAmmo (10);
	}

	public void Eleven()
	{
		dam.SetAmmo (11);
	}

	public void Twelve()
	{
		dam.SetAmmo (12);
	}
}
