using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : _Weapons
{
	//A knife, that only cut tomatoes


	new public void Awake()
	//Set all the stats of the weapon
	{
		ObjectName = "Knife";
		damages = 30; 
		RangeOfAttk = 40;
		TimeBetweenAttacks = 0.5f;
		SubDescription = "A small knife usefull to cut tomatoes.";
		base.Awake ();
		sprite = Materials.KnifeSprite;
	}
}
