using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : _Consumables 
{
	//A spell that boost the speed of the player


	new public void Awake()
	//set all the stats of the item
	{
		ObjectName = "SpeedPotion";
		sprite = Materials.HealthPotionSprite;
		damages = 20; 
		RangeOfAttk = 0;
		TimeBetweenAttacks = 10;
		description = "Increase the speed of your player for " + damages + " seconds.";
		base.Awake ();
	}



	public void UseItem()
	//Use the item Speed Potion on us
	{

	}

}
