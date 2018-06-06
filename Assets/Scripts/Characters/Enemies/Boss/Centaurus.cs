using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centaurus : _Enemies 
{
	//First boss centaurus

	public GameObject Player;
	public GameObject LoadScene;

	new public void Awake()
	//Set different stats
	{
		RotateSpeed = 250;
		RangeOfDetection = 2000;
		WalkSpeed = 50;
		RunSpeed = 100;
		MaxHealth = 200;
		MaxMana = 100;
		ActualWeapon = GetComponent<CentaurusWeapon> ();
		base.Awake ();
	}


	new public void Update()
	{
		if (Health <= 0)
		{
			LoadScene.SetActive (true);
		}
		if (PlayersDetected.Count != 0 && PlayersDetected[0].transform.position.y < this.transform.position.y + 10)
		{
			IsAirWallEnabled = 1;
		}
		else
		{
			IsAirWallEnabled = 0;
		}
		base.Update ();
	}
}
