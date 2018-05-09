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
	public PlayerInventory Player;  //The player that has the inventory that we want to display
	public Button OtherWeapon;  //for the two ActiveWeapon buttons : the other activeWeapon button
	private ColorBlock Red;  //Color red (if the button is highlighted)
	private ColorBlock Grey;  //color grey (if the button isn't highlighted)
	public Canvas Inventory;  //The canvas of the inventory


	public void Awake()
	//Set all the stats of the button
	{
		Object = null;
		ObjectName = GetComponentInChildren<Text> ();
		Red = GetComponent<Button> ().colors;
		Red.normalColor = new Color32 (255, 146, 146, 200);
		Grey = GetComponent<Button> ().colors;
		Grey.normalColor = new Color32 (245, 245, 245, 100);
		Player = Inventory.GetComponent<DisplayInventory> ().player;
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
	//Display the weaponsStats if there is an object
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
		GetComponent<Button> ().colors = Red;
		Player.SelectedObject = SelectedObject;
		OtherWeapon.GetComponent<Button> ().colors = Grey;
		switch (Player.TypeOfObjects)
		{
		case 1:
			switch (SelectedObject) 
			{
			case 1:
				Player.Weapon2.Object.SetActive (false);
				Player.Weapon1.Object.SetActive (true);
				break;
			case 2:
				Player.Weapon1.Object.SetActive (false);
				Player.Weapon2.Object.SetActive (true);
				break;
			}
			break;
		case 2:
			switch (SelectedObject) 
			{
			case 1:
				Player.Spell2.Object.SetActive (false);
				Player.Spell1.Object.SetActive (true);
				break;
			case 2:
				Player.Spell1.Object.SetActive (false);
				Player.Spell2.Object.SetActive (true);
				break;
			}
			break;
		}
	}



	public void AssignWeapon()
	//Assign the weapon
	{
		if (Player.TypeOfObjects == 1 && Object != null)
		{
			if (Player.SelectedObject == 1)
			{
				Player.Weapon1.Object.SetActive (false);
				Player.Weapon1 = (_Weapons)Object;
				Player.Weapon1.Object.SetActive (true);
			}
			else
			{
				Player.Weapon2.Object.SetActive (false);
				Player.Weapon2 = (_Weapons)Object;
				Player.Weapon2.Object.SetActive (true);
			}
			Inventory.GetComponent<DisplayInventory>().DisplayWeapons();
		}
		else if(Player.TypeOfObjects == 2 && Object != null)
		{
			if (Player.SelectedObject == 1)
			{
				Player.Spell1.Object.SetActive (false);
				Player.Spell1 = (_Spells)Object;
				Player.Spell1.Object.SetActive (true);
			}
			else
			{
				Player.Spell2.Object.SetActive (false);
				Player.Spell2 = (_Spells)Object;
				Player.Spell2.Object.SetActive (true);
			}
			Inventory.GetComponent<DisplayInventory>().DisplaySpells();
		}
	}
}