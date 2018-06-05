using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaPotion : _Consumables 
{
	//An item that give stamina to the player

	new public void Awake()
	//set all the stats of the item
	{
		ObjectName = "Stamina Potion";
		damages = 20; 
		RangeOfAttk = 0;
		TimeBetweenAttacks = 0;
		description = "Restore " + damages + " stamina.";
		base.Awake ();
		sprite = Materials.StaminaPotionSprite;
	}



	public void UseItem()
	//Use the item Stamina Potion on us
	{
		((MainCharacter)owner).Stamina = Mathf.Min (((MainCharacter)owner).Stamina + damages, ((MainCharacter)owner).MaxStamina);
	}
}
