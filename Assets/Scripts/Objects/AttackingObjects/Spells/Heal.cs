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
		ObjectName = "Heal";
		sprite = MaterialsAssignations.HealSprite;
		damages = 20; 
		RangeOfAttk = 0;
		TimeBetweenAttacks = 50;
		ManaConsumed = 30;
		SubDescription = "Heal your player by " + damages + " HP.";
		base.Awake ();
	}



	public void LaunchSpell()
	//Launch the spell AirWall on us
	{
		owner.Health += damages;
		owner.Health = Mathf.Min (owner.Health, owner.MaxHealth);
	}
}
