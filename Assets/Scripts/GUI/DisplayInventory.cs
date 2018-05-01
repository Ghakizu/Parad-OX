using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayInventory : MonoBehaviour 
{
	//All the functions that we need to display the inventory of our player

	public PlayerInventory player;  //which inventory do we have to display ?

	public ObjectsButton Object1;  //Button of the first object
	public ObjectsButton Object2;  //Button of the second object
	public ObjectsButton Object3;  //Button of the third object
	public ObjectsButton Object4;  //Button of the fourth object
	public ObjectsButton Object5;  //Button of the fifth object
	public ObjectsButton Object6;  //Button of the sixth object
	public ObjectsButton Object7;  //Button of the seventh object
	public ObjectsButton Object8;  //Button of the eighth object
	public ObjectsButton Object9;  //Button of the ninth object
	public ObjectsButton ActiveWeapon1;  //Button which display the first weapon that is equipped
	public ObjectsButton ActiveWeapon2;  //Button which display the second weapon that is equipped



	public void DisplayWeapons()
	//Display the weapons
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
	//Display the spells
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
	//Display the spells
	{
		int capacity = player.ConsumablesInventory.Count;
		Object1.Object = capacity > 0 ? player.ConsumablesInventory [0] : null;
		Object2.Object = capacity > 1 ? player.ConsumablesInventory [1] : null;
		Object3.Object = capacity > 2 ? player.ConsumablesInventory [2] : null;
		Object4.Object = capacity > 3 ? player.ConsumablesInventory [3] : null;
		Object5.Object = capacity > 4 ? player.ConsumablesInventory [4] : null;
		Object6.Object = capacity > 5 ? player.ConsumablesInventory [5] : null;
		Object7.Object = capacity > 6 ? player.ConsumablesInventory [6] : null;
		Object8.Object = capacity > 7 ? player.ConsumablesInventory [7] : null;
		Object9.Object = capacity > 8 ? player.ConsumablesInventory [8] : null;
		ActiveWeapon1.Object = player.cons1;
		ActiveWeapon2.Object = player.cons2;
		player.TypeOfObjects = 3;
	}
}
