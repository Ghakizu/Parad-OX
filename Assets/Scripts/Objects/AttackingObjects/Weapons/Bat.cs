using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : _Weapons 
{
	//a bat to play baseball


	new public void Awake()
	//Set all the stats of the weapon
	{
		ObjectName = MaterialsAssignations.BatName;
		damages = 50; 
		RangeOfAttk = 60;
		TimeBetweenAttacks = 2f;
		SubDescription = "A bat to play baseball, not to hit someone else.";
		base.Awake ();
		sprite = Materials.BatSprite;
	}
}
