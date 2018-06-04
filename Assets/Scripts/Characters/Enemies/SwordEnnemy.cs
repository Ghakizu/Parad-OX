using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnnemy : _Enemies 
{
	//Enemies attacking with their Swords

	public void Awake()
	//Set different stats
	{
		RotateSpeed = 150;
		RangeOfDetection = 300;
		WalkSpeed = 70;
		RunSpeed = 120;
		MaxHealth = 150;
		MaxMana = 100;
		ActualWeapon = GetComponent<Sword> ();
		base.Awake ();
	}
}
