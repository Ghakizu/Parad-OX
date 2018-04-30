using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : _Spells 
{
	//A Spell that confuse the target



	new public void Awake()
	//set all the stats of the spell
	{
		TimeOfEffect = 3;
		ObjectName = "Flash";
		sprite = MaterialsAssignations.FlashSprite;
		damages = 0; 
		RangeOfAttk = 100;
		TimeBetweenAttacks = 10;
		ManaConsumed = 60;
		SubDescription = "Create a flash to confuse the target";
		base.Awake ();
	}



	public void LaunchSpell(_Character other)
	//Launch the spell Flash againt the target
	{
		if (other.tag == "Enemy") 
		{

		}
	}
}
