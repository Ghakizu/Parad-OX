using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fists : _Weapons 
{
	private PlayerInventory MyPlayer;

	void Start()
	{
		MyPlayer = GetComponent<PlayerInventory> ();
		damages = 10;  //to adapt
		RangeOfAttk = 1; //to adapt
		WeaponObject = MyPlayer.FistsObject;
		WeaponName = "Fists";
	}
}
