using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : _Spells 
{
	//A Spell that disables all flying projectiles that can hurt the character during TimeOfEffect seconds



	new public void Awake()
	//set all the stats of the spell
	{
		TimeOfEffect = 0;
		ObjectName = MaterialsAssignations.HealName;
		damages = 20; 
		RangeOfAttk = 0;
		TimeBetweenAttacks = 6;
		ManaConsumed = 30;
		SubDescription = "Heal your player by " + damages + " HP.";
		base.Awake ();
		sprite = Materials.HealSprite;
	}



	public void LaunchSpell()
	//Launch the spell AirWall on us
	{
		owner.Health = Mathf.Min (owner.Health + damages, owner.MaxHealth);
	}
}
