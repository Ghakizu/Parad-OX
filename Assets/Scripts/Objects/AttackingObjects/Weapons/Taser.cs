using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taser : _Weapons 
{
	private PlayerInventory MyPlayer;

	void Start()
	{
		MyPlayer = GetComponent<PlayerInventory> ();
		damages = 10;  //to adapt
		RangeOfAttk = 100; //to adapt
		Object = MyPlayer.TaserObject;
		ObjectName = "taser";
	}
}
