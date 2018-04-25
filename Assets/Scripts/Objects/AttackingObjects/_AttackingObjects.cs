using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class _AttackingObjects : MonoBehaviour 
{
	//The weapons and the spells of th edifferent characters

	public float damages;  //Damages of the weapon. Must be added in each wepon script
	public float RangeOfAttk;  //How far can yo attack ?
	public float TimeBetweenAttacks;
	public GameObject WeaponObject;  //To easily enable or disable the objects that we want
	public string WeaponName;
	public _Character owner;
	public Sprite sprite;


	public static void Attack(_Character attacker, _Character defender)
	{
		
	}


	public static void SpecialAttack(_Character attacker, _Character defender)
	{
		
	}

}
