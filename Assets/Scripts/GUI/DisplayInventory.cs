using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayInventory : MonoBehaviour 
{
	public PlayerInventory player;

	public ObjectsButton Object1;
	public ObjectsButton Object2;
	public ObjectsButton Object3;
	public ObjectsButton Object4;
	public ObjectsButton Object5;
	public ObjectsButton Object6;
	public ObjectsButton Object7;
	public ObjectsButton Object8;
	public ObjectsButton Object9;
	public ObjectsButton ActiveWeapon1;
	public ObjectsButton ActiveWeapon2;

	public void DisplayWeapons()
	{
		int capacity = player.WeaponsInventory.Count;
		Object1.Object = capacity > 0 ? player.WeaponsInventory [0] : null;
		Object2.Object = capacity > 1 ? player.WeaponsInventory [1] : null;
		Object3.Object = capacity > 2 ? player.WeaponsInventory [2] : null;
		Object4.Object = capacity > 3 ? player.WeaponsInventory [3] : null;
		Object5.Object = capacity > 4 ? player.WeaponsInventory [4] : null;
		Object6.Object = capacity > 5 ? player.WeaponsInventory [5] : null;
		Object7.Object = capacity > 6 ? player.WeaponsInventory [6] : null;
		Object8.Object = capacity > 7 ? player.WeaponsInventory [7] : null;
		Object9.Object = capacity > 8 ? player.WeaponsInventory [8] : null;
		ActiveWeapon1.Object = player.Weapon1;
		ActiveWeapon2.Object = player.Weapon2;
		player.TypeOfObjects = 1;
	}

	public void DisplaySpells()
	{
		int capacity = player.SpellsInventory.Count;
		Object1.Object = capacity > 0 ? player.SpellsInventory [0] : null;
		Object2.Object = capacity > 1 ? player.SpellsInventory [1] : null;
		Object3.Object = capacity > 2 ? player.SpellsInventory [2] : null;
		Object4.Object = capacity > 3 ? player.SpellsInventory [3] : null;
		Object5.Object = capacity > 4 ? player.SpellsInventory [4] : null;
		Object6.Object = capacity > 5 ? player.SpellsInventory [5] : null;
		Object7.Object = capacity > 6 ? player.SpellsInventory [6] : null;
		Object8.Object = capacity > 7 ? player.SpellsInventory [7] : null;
		Object9.Object = capacity > 8 ? player.SpellsInventory [8] : null;
		ActiveWeapon1.Object = player.Spell1;
		ActiveWeapon2.Object = player.Spell2;
		player.TypeOfObjects = 2;
	}

	public void DisplayConsumables()
	{
		int capacity = player.SpellsInventory.Count;
		Object1.Object = capacity > 0 ? player.SpellsInventory [0] : null;
		Object2.Object = capacity > 1 ? player.SpellsInventory [1] : null;
		Object3.Object = capacity > 2 ? player.SpellsInventory [2] : null;
		Object4.Object = capacity > 3 ? player.SpellsInventory [3] : null;
		Object5.Object = capacity > 4 ? player.SpellsInventory [4] : null;
		Object6.Object = capacity > 5 ? player.SpellsInventory [5] : null;
		Object7.Object = capacity > 6 ? player.SpellsInventory [6] : null;
		Object8.Object = capacity > 7 ? player.SpellsInventory [7] : null;
		Object9.Object = capacity > 8 ? player.SpellsInventory [8] : null;
		ActiveWeapon1.Object = player.Spell1;
		ActiveWeapon2.Object = player.Spell2;
		player.TypeOfObjects = 3;
	}


}
