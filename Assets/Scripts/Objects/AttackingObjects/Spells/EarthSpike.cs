﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpike : _Spells
{
	//A Spell that make a spike appear from under the target


	new public void Awake()
	//set all the stats of the spell
	{
		TimeOfEffect = 0;
		ObjectName = MaterialsAssignations.EarthSpikeName;
		damages = 80; 
		RangeOfAttk = 200;
		TimeBetweenAttacks = 6;
		ManaConsumed = 100;
		SubDescription = "A spike appear from under the target.";
		base.Awake ();
		sprite = Materials.EarthSpikeSprite;
	}



	public void LaunchSpell(_Character other)
	//Launch the spell EarthSpike against the target
	{
		
	}
}
