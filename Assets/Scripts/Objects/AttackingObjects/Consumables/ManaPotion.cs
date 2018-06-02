using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : _Consumables 
{
	//An item that give Mana to the player

	new public void Awake()
	//set all the stats of the item
	{
		ObjectName = "ManaPotion";
		damages = 20; 
		RangeOfAttk = 0;
		TimeBetweenAttacks = 0;
		description = "Restore " + damages + " Mana.";
		base.Awake ();
		sprite = Materials.ManaPotionSprite;
	}



	public void UseItem()
	//Use the item Mana Potion on us
	{
		owner.Mana = Mathf.Min (owner.Mana + damages, owner.MaxMana);
	}
}
