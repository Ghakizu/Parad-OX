using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : _Consumables 
{
	//A spell that heals the player

	new public void Awake()
	//set all the stats of the item
	{
		ObjectName = "HealthPotion";
		damages = 20; 
		RangeOfAttk = 0;
		TimeBetweenAttacks = 10;
		description = "Heal your player by " + damages + " HP.";
		base.Awake ();
		sprite = Materials.HealthPotionSprite;
	}



	public void UseItem()
	//Use the item Healt Potion on us
	{
		owner.Health += damages;
		owner.Health = Mathf.Min (owner.Health, owner.MaxHealth);
	}

}
