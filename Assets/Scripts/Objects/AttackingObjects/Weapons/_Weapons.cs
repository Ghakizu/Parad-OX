using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class _Weapons : _AttackingObjects 
{
	//All the weapons (physical objects that can hurt another character)


	new public void Awake()
	//Set the description and the owner for all the attacking objects
	{
		base.Awake ();
		SetDescription ();
	}


	public void SetDescription()
	//A function that set the description that need to appear in the inventory
	{
		description = "\t\t\t\t" + ObjectName + 
			"\nAttack : " + damages + 
			"\nRange : " + RangeOfAttk + 
			"\nRythm : " + Math.Round(1 / TimeBetweenAttacks, 2) + 
			" per seconds\n" + SubDescription;
	}


	public void Attack(_Character other)
	//function that make a character attack another one
	{
		MainCharacter PossiblePlayer = other.GetComponent<MainCharacter> ();  	
		other.Health -= owner.ActualWeapon.damages;
		owner.IsAbleToAttack = TimeBetweenAttacks;
		if (other.Health <= 0)
		{
			if (PossiblePlayer != null)
			{
				PossiblePlayer.Die ();  //Die like a main character
			}
			else
			{
				other.Die ();  //Die like an IA character
			}
		}
	}
}
