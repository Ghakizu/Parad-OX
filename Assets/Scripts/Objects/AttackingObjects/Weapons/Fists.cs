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
		RangeOfAttk = 200; //to adapt
		Object = MyPlayer.FistsObject;
		ObjectName = "Fists";
		description = "Fists";
	}
}
