using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirWall : _Spells
{
	//A Spell that disables all flying projectiles that can hurt the character during TimeOfEffect seconds



	new public void Awake()
	//set all the stats of the spell
	{
		TimeOfEffect = 5;
		ObjectName = "AirWall";
		damages = 0; 
		RangeOfAttk = 0;
		TimeBetweenAttacks = 10;
		ManaConsumed = 50;
		SubDescription = "Disables all the flying projectiles during " + TimeOfEffect + " seconds.";
		base.Awake ();
		sprite = Materials.AirWallSprite;
	}



	public void LaunchSpell()
	//Launch the spell AirWall on us
	{
		owner.IsAirWallEnabled = TimeOfEffect;
	}
}
