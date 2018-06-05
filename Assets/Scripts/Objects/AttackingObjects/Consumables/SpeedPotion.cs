using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : _Consumables 
{
	//A spell that boost the speed of the player

	public static float SpeedMultiplier;

	new public void Awake()
	//set all the stats of the item
	{
		SpeedMultiplier = 1.5f;
		ObjectName = MaterialsAssignations.SpeedPotionName;
		damages = 20; 
		RangeOfAttk = 0;
		TimeBetweenAttacks = 10;
		description = "Increase the speed of your character for " + damages + " seconds.";
		base.Awake ();
		sprite = Materials.SpeedPotionSprite;
	}



	public void UseItem()
	//Use the item Speed Potion on us
	{
		((MainCharacter)owner).SpeedMultiplier += damages;
	}

}
