using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : _Weapons 
{
	//A Sword, that cut many things


	new public void Awake()
	//Set all the stats of the weapon
	{
		ObjectName = MaterialsAssignations.SwordName;
		damages = 40; 
		RangeOfAttk = 80;
		TimeBetweenAttacks = 1.5f;
		SubDescription = "A wonderful sword only used by the best ninjas";
		base.Awake ();
		sprite = Materials.SwordSprite;
	}
}
