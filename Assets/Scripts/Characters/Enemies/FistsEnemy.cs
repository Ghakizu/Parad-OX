using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FistsEnemy : _Enemies 
{
	//Enemies attacking with their Fists

	public void Awake()
	//Set different stats
	{
		RotateSpeed = 150;
		RangeOfDetection = 200;
		WalkSpeed = 50;
		RunSpeed = 100;
		MaxHealth = 100;
		MaxMana = 100;
		ActualWeapon = GetComponent<Fists> ();
		base.Awake ();
	}
}
