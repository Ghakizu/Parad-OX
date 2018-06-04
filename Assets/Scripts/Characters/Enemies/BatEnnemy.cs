using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnnemy : _Enemies 
{
	//Enemies attacking with their Bats

	public void Awake()
	//Set different stats
	{
		RotateSpeed = 150;
		RangeOfDetection = 300;
		WalkSpeed = 70;
		RunSpeed = 120;
		MaxHealth = 250;
		MaxMana = 100;
		ActualWeapon = GetComponent<Bat> ();
		ActualWeapon.TimeBetweenAttacks = 5;
		base.Awake ();
	}
}
