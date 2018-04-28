using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fists : _Weapons 
{
	//The basic weapons of your player : your fists

	private PlayerInventory MyPlayer;

	new public void Awake()
	{
		MyPlayer = GetComponent<PlayerInventory> ();
		ObjectName = "Fists";
		sprite = MaterialsAssignations.FistsSprite;
		damages = 10; 
		RangeOfAttk = 30;
		TimeBetweenAttacks = 0.5f;
		SubDescription = "The basic weapon : your fists.";
		base.Awake ();
	}
}
