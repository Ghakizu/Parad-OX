using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : _Spells
{
	//A Spell that make a Fire Ball appear from us towards the target


	new public void Awake()
	//set all the stats of the spell
	{
		TimeOfEffect = 0;
		ObjectName = "FireBall";
		damages = 60; 
		RangeOfAttk = 300;
		TimeBetweenAttacks = 4;
		ManaConsumed = 80;
		SubDescription = "Launch a Fire ball towards the target";
		base.Awake ();
		sprite = Materials.FireBallSprite;
	}



	public void LaunchSpell(_Character other)
	//Launch the spell FireBall against the target
	{

	}
}
