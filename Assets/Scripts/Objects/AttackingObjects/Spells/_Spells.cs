using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class _Spells : _AttackingObjects 
{
    //All the spells of the characters

    public GameObject particles;  //The particles that must appear when launching the spell

	public float ManaConsumed;  //The mana that must Behaviour used to launch the spell
	public float TimeOfEffect;  //the time that the spell is going to last (for those which affect stats for examples)


	new public void Awake()
	//Set the description and the owner for all the attacking objects
	{
		base.Awake ();
		SetDescription ();
        particles = this.gameObject.transform.GetChild(0).gameObject;
	}


	public void SetDescription()
	//A function that set the description that need to appear in the inventory
	{
		description = "\t\t\t\t" + ObjectName + "\nCost : " + ManaConsumed + " Mana";
		if (damages != 0)
		{
			description += "\nEffect : " + damages;
		}
		 description += "\nRange : " + RangeOfAttk + 
			"\nReload Time : " + TimeBetweenAttacks + 
			" seconds\n" + SubDescription;
	}
}
