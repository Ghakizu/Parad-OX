using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class _AttackingObjects : _Objects 
{
	//The weapons and the spells of the different characters


	public float damages;  //Damages of the weapon. Must be added in each wepon script
	public float RangeOfAttk;  //How far can yo attack ?
	public float TimeBetweenAttacks;  //How long do you have to wait between two attacks ?
	public string SubDescription;  //the description that must follow the "basis" description
}
