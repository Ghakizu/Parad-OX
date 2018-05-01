using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : _Consumables 
{
	new public void Awake()
	//set all the stats of the item
	{
		ObjectName = "HealthPotion";
		sprite = MaterialsAssignations.HealthPotionSprite;
		damages = 20; 
		RangeOfAttk = 0;
		TimeBetweenAttacks = 10;
		description = "Heal your player by " + damages + " HP.";
		base.Awake ();
	}



	public void UseItem()
	//Launch the spell AirWall on us
	{
		owner.Health += damages;
		owner.Health = Mathf.Min (owner.Health, owner.MaxHealth);
	}

}
