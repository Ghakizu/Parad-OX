using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpike : _Spells
{
	//A Spell that make a spike appear from under the target


	new public void Awake()
	//set all the stats of the spell
	{
		TimeOfEffect = 0;
		ObjectName = "EarthSpike";
		sprite = MaterialsAssignations.EarthSpikeSprite;
		damages = 80; 
		RangeOfAttk = 200;
		TimeBetweenAttacks = 10;
		ManaConsumed = 100;
		SubDescription = "Make a spike appear from under the target.";
		base.Awake ();
	}



	public void LaunchSpell(_Character other)
	//Launch the spell EarthSpike against the target
	{
		if (other.tag == "Enemy") 
		{

		}
	}
}
