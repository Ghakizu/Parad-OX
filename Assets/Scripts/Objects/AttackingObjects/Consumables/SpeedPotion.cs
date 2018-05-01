using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : _Consumables {

	new public void Awake()
	//set all the stats of the item
	{
		ObjectName = "SpeedPotion";
		sprite = MaterialsAssignations.HealthPotionSprite;
		damages = 20; 
		RangeOfAttk = 0;
		TimeBetweenAttacks = 10;
		description = "Increase the speed of your player for " + damages + " seconds.";
		base.Awake ();
	}



	public void UseItem()
	//Launch the spell AirWall on us
	{
		//owner.Health += damages;
		//owner.Health = Mathf.Min (owner.Health, owner.MaxHealth);
	}

}
