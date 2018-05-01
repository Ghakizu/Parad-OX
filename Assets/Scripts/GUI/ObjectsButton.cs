using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectsButton : MonoBehaviour 
{
	//All the buttons usefull to display the inventory


	public _Objects Object;  //The object that is assigned to the button
	public Text ObjectName;  //The Name of the Object
	public GameObject ChangeInventory;  //The gameobject that displays the buttons to change inventory
	public GameObject WeaponsStats;  //The Gameobject that displays the stats of the weapon
	public PlayerInventory player;  //The player that has the inventory that we want to display
	public Button OtherWeapon;  //for the two ActiveWeapon buttons : the other activeWeapon button
	private ColorBlock red;  //Color red (if the button is highlighted)
	private ColorBlock grey;  //color grey (if the button isn't highlighted)
	public Canvas Inventory;  //The canvas of the inventory


	public void Awake()
	//Set all the stats of the button
	{
		Object = null;
		ObjectName = GetComponentInChildren<Text> ();
		red = GetComponent<Button> ().colors;
		red.normalColor = new Color32 (255, 146, 146, 200);
		grey = GetComponent<Button> ().colors;
		grey.normalColor = new Color32 (245, 245, 245, 100);
		player = Inventory.GetComponent<DisplayInventory> ().player;
	}


	public void Update()
	//Update the sprite and the text of the object
	{
		if (Object != null) 
		{
			GetComponent<Image> ().overrideSprite = Object.sprite;
			ObjectName.text = Object.ObjectName;
		}
		else
		{
			GetComponent<Image> ().overrideSprite = null;
			ObjectName.text = "";
		}
	}


	public void OnMouseEnter()
	//Display the weaponsStats if There is an object
	{
		if (Object != null) 
		{
			ChangeInventory.SetActive (false);
			WeaponsStats.SetActive (true);
			WeaponsStats.GetComponentInChildren<Image> ().overrideSprite = Object.sprite;
			WeaponsStats.GetComponentInChildren<Text> ().text = Object.description;
		}
	}


	public void OnMouseExit()
	//Stop to display the weaponsStats and reshow the menu to change inventory
	{
		WeaponsStats.SetActive (false);
		ChangeInventory.SetActive (true);
	}


	public void SelectWeaponToAssign(int SelectedObject)
	//Change the active weapon
	{
		GetComponent<Button> ().colors = red;
		player.SelectedObject = SelectedObject;
		OtherWeapon.GetComponent<Button> ().colors = grey;
	}


	public void AssignWeapon()
	//Assign the weapon
	{
		if (player.TypeOfObjects == 1 && Object != null)
		{
			if (player.SelectedObject == 1)
			{
				player.Weapon1 = (_Weapons)Object;
			}
			else
			{
				player.Weapon2 = (_Weapons)Object;
			}
			Inventory.GetComponent<DisplayInventory>().DisplayWeapons();
		}
		else if(player.TypeOfObjects == 2 && Object != null)
		{
			if (player.SelectedObject == 1)
			{
				player.Spell1 = (_Spells)Object;
			}
			else
			{
				player.Spell2 = (_Spells)Object;
			}
			Inventory.GetComponent<DisplayInventory>().DisplaySpells();
		}
	}
}
