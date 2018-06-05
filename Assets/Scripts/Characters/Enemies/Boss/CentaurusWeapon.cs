using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentaurusWeapon : _Weapons 
{
	//The head of Centaurus


	new public void Awake()
	//Set all the stats of the weapon
	{
		ObjectName = "Centaurus Head";
		damages = 100; 
		RangeOfAttk = 60;
		TimeBetweenAttacks = 5;
		SubDescription = "Centaurus head, as strong as Zidane head";
		base.Awake ();
	}
}
