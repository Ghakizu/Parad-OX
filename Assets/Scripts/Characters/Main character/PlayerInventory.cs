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
	public List<_Weapons> WeaponsInventory;  //The inventory of the weapons of the player
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
	public List<GameObject> PotionsInventory; //Pickables that affect Player stats. Not instanciated ! WARNING!
	public List<_Clues> CluesInventoryLvl1 = new List<_Clues>(); //Clues to launch level 1
	public List<_Clues> CluesInventoryLvl2 = new List<_Clues>(); //Clues to launch level 2
	public List<_Clues> CluesInventoryLvl3 = new List<_Clues>(); //Clues to launch level 3

	private Vector3 velocity = Vector3.zero;  //to stock the velocity of the player when he pauses the game. 
	//When replaying, he will go back exactly in the same state that he was previously.
	private bool frame = true;  //the frame is here because the OnGui function is called 4 times in a frame. 
	//We just want the velocity of the first frame, the others being null.
	private bool Displaying = true;



	void Awake()
	{
		//Add all the gameObjects to their references and disables the weapons that we don't use
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

		//We must change the spell's name by their real name, once we chosed them. WARNING!
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
		//The layer must be changed in order to avoid problems with jumps. WARNING!
	}
		

	public void Update()
	{
		ChangeWeapon ();
		ChangeSpell ();
		if (Input.GetButton ("Wheel") && Input.GetKeyDown (KeyCode.A))
			Displaying = !Displaying;
	}


	public void OnGUI()
	{
		if (Input.GetButtonDown("Wheel") && frame) 
		{
			velocity = Player.CharacterRigidbody.velocity;  //stock the velocity
			frame = false;  //we don't want to go back again in this part of the function
			Player.CharacterRigidbody.constraints = RigidbodyConstraints.FreezeAll;  //freeze the player
			Player.IsGamePaused = true;
			Cursor.lockState = CursorLockMode.None;  //unlock the mouse
			Cursor.visible = true;
		}

		if (Input.GetButton("Wheel"))  //We must do it cleaner, not just display the weapons in lines, but do a real Canvas menu. WARNING!
		{
			/*if (Displaying || SpellsInventory.Count == 0)
				DisplayWeapons ();
			else 
			{
				DisplaySpells ();
			}*/
			//Inventory.gameObject.SetActive (true);
		}
		else if (Input.GetButtonUp("Wheel"))
		{
			Player.CharacterRigidbody.constraints = RigidbodyConstraints.FreezeRotation;  //unfreeze the player
			Player.IsGamePaused = false;
			Player.CharacterRigidbody.velocity = velocity;  //give him back his velocity
			Cursor.lockState = CursorLockMode.Locked;  //lock the mouse again
			Cursor.visible = false;
			frame = true;
		}
	}




	//WEAPONS
	private void ChangeWeapon()  //change weapons using shortcuts
	{
		if(Input.GetButtonDown("Fire1"))
		{
			Weapon2.Object.SetActive (false);
			Weapon1.Object.SetActive (true);
			ActiveWeapon = 1;
			Player.WeaponObject = Weapon1.Object;
		}
			
		if(Input.GetButtonDown("Fire2"))
		{
			Weapon1.Object.SetActive (false);
			Weapon2.Object.SetActive (true);
			ActiveWeapon = 2;
			Player.WeaponObject = Weapon2.Object;
		}
	}


	/*public void DisplayWeapons()
	{
		float width = 15; 
		foreach (_Weapons weapon in WeaponsInventory) 
		{
			if (GUI.Button (new Rect (width, 20, 100, 20), weapon.WeaponName)) 
			{
				if (Weapon1.isActiveAndEnabled) 
				{
					Weapon1.Object.SetActive (false);
					Weapon2.Object.SetActive (false);
					Weapon1 = weapon;
					Weapon1.Object = weapon.Object;
					Weapon1.Object.SetActive (true);
				} 
				else if (Weapon2.isActiveAndEnabled) 
				{
					Weapon1.Object.SetActive (false);
					Weapon2.Object.SetActive (false);
					Weapon2 = weapon;
					Weapon2.Object = weapon.Object;
					Weapon2.Object.SetActive (true);
				}
			}
			width += 120;
		}
	}*/


	//SPELLS
	private void ChangeSpell()  //change spells using shortcuts
	{
		if(Input.GetButtonDown("Fire3") && Spell1 != null)
		{
			Spell2.Object.SetActive (false);
			Spell1.Object.SetActive (true);
			ActiveSpell = 3;
		}

		if(Input.GetButtonDown("Fire4") && Spell1 != null)
		{
			Spell1.Object.SetActive (false);
			Spell2.Object.SetActive (true);
			ActiveSpell = 4;
		}
	}



	/*public void DisplaySpells()
	{
		float width = 15; 
		foreach (_Spells spell in SpellsInventory) 
		{
			if (GUI.Button (new Rect (width, 20, 100, 20), spell.WeaponName)) 
			{
				if (Spell1.isActiveAndEnabled) 
				{
					Spell1.Object.SetActive (false);
					Spell2.Object.SetActive (false);
					Spell1 = spell;
					Spell1.Object = spell.Object;
					Spell1.Object.SetActive (true);
				} 
				else if (Spell2.isActiveAndEnabled) 
				{
					Spell1.Object.SetActive (false);
					Spell2.Object.SetActive (false);
					Spell2 = spell;
					Spell2.Object = spell.Object;
					Spell2.Object.SetActive (true);
				}
			}
			width += 120;
		}
	}*/
}

//1 WARNING
//TODO : create a real menu to display when assigning weapons
//The spells are empty game objects with particules attached to the left hand of our player. 
//At the beginning of the game, we have no spells.
//So by default, our spell1 and spell2 is equal to null.