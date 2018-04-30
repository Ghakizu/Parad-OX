using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : _Weapons 
{
	//A katana, that cut many things

	new public void Awake()
	//Set all the stats of the weapon
	{
		ObjectName = "Katana";
		sprite = MaterialsAssignations.KatanaSprite;
		damages = 40; 
		RangeOfAttk = 80;
		TimeBetweenAttacks = 1.5f;
		SubDescription = "A wonderful katana for the ninjas";
		base.Awake ();
	}
}
