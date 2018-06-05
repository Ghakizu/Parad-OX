using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Excalibur : _Weapons 
{
	//Excalibur, the best weapon (or not...)


	new public void Awake()
	//Set all the stats of the weapon
	{
		ObjectName = MaterialsAssignations.ExcaliburName;
		damages = 2; 
		RangeOfAttk = 80;
		TimeBetweenAttacks = 2f;
		SubDescription = "Excalibur, the best weapon... or not";
		base.Awake ();
		sprite = Materials.ExcaliburSprite;
	}
}
