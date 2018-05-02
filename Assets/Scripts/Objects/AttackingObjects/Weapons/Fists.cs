using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fists : _Weapons 
{
	//The basic weapons of your player : your fists

	new public void Awake()
	//Set all the stats of the weapon
	{
		ObjectName = "Fists";
		sprite = MaterialsAssignations.FistsSprite;
		damages = 10; 
		RangeOfAttk = 100;
		TimeBetweenAttacks = 0.5f;
		SubDescription = "The basic weapon : your fists.";
		base.Awake ();
	}
}
