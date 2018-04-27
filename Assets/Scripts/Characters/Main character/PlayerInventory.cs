using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(MainCharacter))]

public class PlayerInventory : MonoBehaviour 
{
	//The different methods that the player can use to manage his inventory


	public MainCharacter Player;

	//Weapons Inventory
	public int ActiveWeapon = 1; //The actual weapon. Can just be 1 or 2 (the shortcuts)
	public List<_Weapons> WeaponsInventory = new List<_Weapons>();  //The inventory of the weapons of the player
	public GameObject FistsObject;  //the gameObject of the Fists
	public GameObject KatanaObject;  //the gameObject of the Katana
	public GameObject KnifeObject;  //the gameObject of the Knife
	public GameObject TaserObject;  //the gameObject of the Taser
	public _Weapons Weapon1; //weapon of the first shortcut
	public _Weapons Weapon2; //weapon of the second shortcut


	//Spells Inventory
	public int ActiveSpell = 3;  //The actual spell. Can just be 3 or 4 (the shortcuts)
	public List<_Spells> SpellsInventory = new List<_Spells>();  //The inventory of the spells of the player
	public GameObject Spell1Object;  //the gameObject of the Spell1
	public GameObject Spell2Object;  //the gameObject of the Spell2
	public GameObject Spell3Object;  //the gameObject of the Spell3
	public GameObject Spell4Object;  //the gameObject of the Spell4
	public _Spells Spell1; //Spell of the first shortcut
	public _Spells Spell2; //Spell of the second shortcut


	//CluesInventory
	public List<GameObject> ConsumablesInventory; //Pickables that affect Player stats. Not instanciated ! WARNING!
	public List<_Clues> CluesInventoryLvl1 = new List<_Clues>(); //Clues to launch level 1
	public List<_Clues> CluesInventoryLvl2 = new List<_Clues>(); //Clues to launch level 2
	public List<_Clues> CluesInventoryLvl3 = new List<_Clues>(); //Clues to launch level 3


	public Canvas Inventory;

	public int SelectedObject = 1;
	public int TypeOfObjects = 1; //1 = weapons, 2 = spells, 3 = potions




	void Awake()
	{
		//WEAPONS
		FistsObject = GameObject.Find ("Fists");
		KatanaObject = GameObject.Find ("Katana");
		KatanaObject.SetActive (false);
		KnifeObject = GameObject.Find ("Knife");
		KnifeObject.SetActive (false);
		TaserObject = GameObject.Find ("Taser");
		TaserObject.SetActive (false);
		WeaponsInventory.Add(GetComponent<Fists>());
		WeaponsInventory.Add(GetComponent<Katana>());
		Weapon1 = WeaponsInventory[0];
		Weapon2 = WeaponsInventory[1];
		Player = GetComponent<MainCharacter> ();
		Player.WeaponObject = Weapon1.Object;

		//SPELLS
		/*Spell1Object = GameObject.Find ("Spell1");
		Spell1Object.SetActive (false);
		Spell2Object = GameObject.Find ("Spell2");
		Spell2Object.SetActive (false);
		Spell3Object = GameObject.Find ("Spell3");
		Spell3Object.SetActive (false);
		Spell4Object = GameObject.Find ("Spell4");
		Spell4Object.SetActive (false);
		Spell1 = null;
		Spell2 = null;*/
	}
		


	public void Update()
	{
		ChangeWeapon ();
		ChangeSpell ();
		if (Input.GetButtonDown ("Wheel"))
		{
			Inventory.gameObject.SetActive (true);
			Inventory.GetComponent<DisplayInventory>().DisplayWeapons();
			Cursor.lockState = CursorLockMode.None;  //unlock the mouse
			Cursor.visible = true;
			Player.IsGamePaused = true;
			Player.NormalDisplays.SetActive (false);
		}
		else if (Input.GetButtonUp ("Wheel"))
		{
			Inventory.gameObject.SetActive (false);
			Cursor.lockState = CursorLockMode.Locked;  //lock the mouse again
			Cursor.visible = false;
			SelectedObject = 1;
			TypeOfObjects = 1;
			Player.IsGamePaused = false;
			Player.NormalDisplays.SetActive (true);
		}
	}





	//WEAPONS
	private void ChangeWeapon()  //change weapons using shortcuts
	{
		if(Input.GetButtonDown("Weapon1"))
		{
			Weapon2.Object.SetActive (false);
			Weapon1.Object.SetActive (true);
			ActiveWeapon = 1;
			Player.WeaponObject = Weapon1.Object;
		}
			
		if(Input.GetButtonDown("Weapon2"))
		{
			Weapon1.Object.SetActive (false);
			Weapon2.Object.SetActive (true);
			ActiveWeapon = 2;
			Player.WeaponObject = Weapon2.Object;
		}
	}
		


	//SPELLS
	private void ChangeSpell()  //change spells using shortcuts
	{
		if(Input.GetButtonDown("Spell1") && Spell1 != null)
		{
			Spell2.Object.SetActive (false);
			Spell1.Object.SetActive (true);
			ActiveSpell = 3;
		}

		if(Input.GetButtonDown("Spell2") && Spell1 != null)
		{
			Spell1.Object.SetActive (false);
			Spell2.Object.SetActive (true);
			ActiveSpell = 4;
		}
	}
}

//1 WARNING
//The spells are empty game objects with particules attached to the left hand of our player. 
//At the beginning of the game, we have no spells.
//So by default, our spell1 and spell2 is equal to null.