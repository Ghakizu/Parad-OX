using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centaurus : _Enemies 
{
	//First boss centaurus

	public GameObject Player;

	new public void Awake()
	//Set different stats
	{
		RotateSpeed = 150;
		RangeOfDetection = 200;
		WalkSpeed = 50;
		RunSpeed = 100;
		MaxHealth = 100;
		MaxMana = 100;
		ActualWeapon = GetComponent<CentaurusWeapon> ();
		base.Awake ();
	}

	public void Start()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
	}


	new public void Update()
	{
		if (Player.transform.position.y > this.transform.position.y)
		{
			IsAirWallEnabled = 1;
		}
		else
		{
			IsAirWallEnabled = 0;
		}
	}
}
