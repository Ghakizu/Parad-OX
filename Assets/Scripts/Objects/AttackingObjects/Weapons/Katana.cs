using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : _Weapons 
{
	private PlayerInventory MyPlayer;

	void Start()
	{
		MyPlayer = GetComponent<PlayerInventory> ();
		damages = 10;  //to adapt
		RangeOfAttk = 50; //to adapt
		Object = MyPlayer.KatanaObject;
		ObjectName = "Katana";
		description = "Katana";
	}
}
