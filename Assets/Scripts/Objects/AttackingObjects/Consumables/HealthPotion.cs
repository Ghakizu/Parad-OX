using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : _Consumables 
{
	//An item that heals the player

	new public void Awake()
	//set all the stats of the item
	{
		ObjectName = "Health Potion";
		damages = 20; 
		RangeOfAttk = 0;
		TimeBetweenAttacks = 0;
		description = "Restore " + damages + " HP.";
		base.Awake ();
		sprite = Materials.HealthPotionSprite;
	}



	public void UseItem()
	//Use the item Health Potion on us
	{
		owner.Health = Mathf.Min (owner.Health + damages, owner.MaxHealth);
	}
}
