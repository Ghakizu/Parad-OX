using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : _Weapons
{
	//A knife, that only cut tomatoes

	private PlayerInventory MyPlayer;

	new public void Awake()
	{
		MyPlayer = GetComponent<PlayerInventory> ();
		ObjectName = "Knife";
		sprite = MaterialsAssignations.KnifeSprite;
		damages = 30; 
		RangeOfAttk = 40;
		TimeBetweenAttacks = 0.5f;
		SubDescription = "A small knife usefull to cut tomatoes.";
		base.Awake ();
	}
}
