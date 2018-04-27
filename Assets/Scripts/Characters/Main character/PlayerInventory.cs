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
		Player.WeaponObject = Weapon1.WeaponObject;

		//We must change the spell's name by their real name, once we choose them. WARNING!
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
			Inventory.gameObject.SetActive (true);
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


	public Canvas Inventory;
	public Button Object1;
	public Button Object2;
	public Button Object3;
	public Button Object4;
	public Button Object5;
	public Button Object6;
	public Button Object7;
	public Button Object8;
	public Button Object9;
	public Button ActiveWeapon1;
	public Button ActiveWeapon2;
	public Button Weapons;
	public Button Spells;
	public Button Consumables;
	public Button clues;

/*	public void DisplayInventory()
	{
		Inventory.gameObject.SetActive (true);
		if (Spells)
		{
			
		}
		if (Weapons)
		{
			
		}
	}*/

	public void DisplayMyWeapons()
	{
		Debug.Log ("weapons");
		int capacity = WeaponsInventory.Count;
		Object1.GetComponent<Image>().overrideSprite = capacity > 0 ? WeaponsInventory [0].sprite : null;
		Object2.GetComponent<Image>().overrideSprite = capacity > 1 ? WeaponsInventory [1].sprite : null;
		Object3.GetComponent<Image>().overrideSprite = capacity > 2 ? WeaponsInventory [2].sprite : null;
		Object4.GetComponent<Image>().overrideSprite = capacity > 3 ? WeaponsInventory [3].sprite : null;
		Object5.GetComponent<Image>().overrideSprite = capacity > 4 ? WeaponsInventory [4].sprite : null;
		Object6.GetComponent<Image>().overrideSprite = capacity > 5 ? WeaponsInventory [5].sprite : null;
		/*Object7.GetComponent<Image>().overrideSprite = capacity > 6 ? WeaponsInventory [6].sprite : null;
		Object8.GetComponent<Image>().overrideSprite = capacity > 7 ? WeaponsInventory [7].sprite : null;
		Object9.GetComponent<Image>().overrideSprite= capacity > 8 ? WeaponsInventory [8].sprite : null;*/
	}

	public void DisplayMySpells()
	{
		Debug.Log ("spells");
		int capacity = SpellsInventory.Count;
		Object1.GetComponent<Image>().overrideSprite = capacity > 0 ? SpellsInventory [0].sprite : null;
		Object2.GetComponent<Image>().overrideSprite = capacity > 1 ? SpellsInventory [1].sprite : null;
		Object3.GetComponent<Image>().overrideSprite = capacity > 2 ? SpellsInventory [2].sprite : null;
		Object4.GetComponent<Image>().overrideSprite = capacity > 3 ? SpellsInventory [3].sprite : null;
		Object5.GetComponent<Image>().overrideSprite = capacity > 4 ? SpellsInventory [4].sprite : null;
		Object6.GetComponent<Image>().overrideSprite = capacity > 5 ? SpellsInventory [5].sprite : null;
		/*Object7.GetComponent<Image>().overrideSprite = capacity > 6 ? SpellsInventory [6].sprite : null;
		Object8.GetComponent<Image>().overrideSprite = capacity > 7 ? SpellsInventory [7].sprite : null;
		Object9.GetComponent<Image>().overrideSprite = capacity > 8 ? SpellsInventory [8].sprite : null;*/
	}


	//WEAPONS
	private void ChangeWeapon()  //change weapons using shortcuts
	{
		if(Input.GetButtonDown("Fire1"))
		{
			Weapon2.WeaponObject.SetActive (false);
			Weapon1.WeaponObject.SetActive (true);
			ActiveWeapon = 1;
			Player.WeaponObject = Weapon1.WeaponObject;
		}
			
		if(Input.GetButtonDown("Fire2"))
		{
			Weapon1.WeaponObject.SetActive (false);
			Weapon2.WeaponObject.SetActive (true);
			ActiveWeapon = 2;
			Player.WeaponObject = Weapon2.WeaponObject;
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
					Weapon1.WeaponObject.SetActive (false);
					Weapon2.WeaponObject.SetActive (false);
					Weapon1 = weapon;
					Weapon1.WeaponObject = weapon.WeaponObject;
					Weapon1.WeaponObject.SetActive (true);
				} 
				else if (Weapon2.isActiveAndEnabled) 
				{
					Weapon1.WeaponObject.SetActive (false);
					Weapon2.WeaponObject.SetActive (false);
					Weapon2 = weapon;
					Weapon2.WeaponObject = weapon.WeaponObject;
					Weapon2.WeaponObject.SetActive (true);
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
			Spell2.WeaponObject.SetActive (false);
			Spell1.WeaponObject.SetActive (true);
			ActiveSpell = 3;
		}

		if(Input.GetButtonDown("Fire4") && Spell1 != null)
		{
			Spell1.WeaponObject.SetActive (false);
			Spell2.WeaponObject.SetActive (true);
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
					Spell1.WeaponObject.SetActive (false);
					Spell2.WeaponObject.SetActive (false);
					Spell1 = spell;
					Spell1.WeaponObject = spell.WeaponObject;
					Spell1.WeaponObject.SetActive (true);
				} 
				else if (Spell2.isActiveAndEnabled) 
				{
					Spell1.WeaponObject.SetActive (false);
					Spell2.WeaponObject.SetActive (false);
					Spell2 = spell;
					Spell2.WeaponObject = spell.WeaponObject;
					Spell2.WeaponObject.SetActive (true);
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