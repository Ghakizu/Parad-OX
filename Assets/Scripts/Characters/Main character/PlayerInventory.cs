using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
	public _Weapons Weapon1; //weapon of the first shortcut
	public _Weapons Weapon2; //weapon of the second shortcut


	//Spells Inventory
	public int ActiveSpell = 3;  //The actual spell. Can just be 3 or 4 (the shortcuts)
	public List<_Spells> SpellsInventory = new List<_Spells>();  //The inventory of the spells of the player
	public GameObject FreezeObject;  //the gameObject of the freeze spell
	public GameObject AirWallObject;  //the gameObject of the airwall spell
	public GameObject EarthSpikeObject;  //the gameObject of the earth spike spell
	public GameObject FireBallObject;  //the gameObject of the fire ball spell
	public GameObject FlashObject;  //the gameObject of the Flash spell
	public _Spells Spell1; //Spell of the first shortcut
	public _Spells Spell2; //Spell of the second shortcut

	public _Consumables cons1;
	public _Consumables cons2;


	//Consumables inventory
	public int ActiveConsumable = 5; //The actual consumable. Can be just 5 or 6 (the shortcuts)
	public List<_Consumables> ConsumablesInventory; //Pickables that affect Player stats. Not instanciated ! WARNING!


	//CluesInventory
	public List<_Clues> CluesInventoryLvl1 = new List<_Clues>(); //Clues to launch level 1
	public List<_Clues> CluesInventoryLvl2 = new List<_Clues>(); //Clues to launch level 2
	public List<_Clues> CluesInventoryLvl3 = new List<_Clues>(); //Clues to launch level 3


	//To affect the objects
	public Canvas Inventory;
	public int SelectedObject = 1;  //1 = actualweapon1, 2 = actualweapon2
	public int TypeOfObjects = 1; //1 = weapons, 2 = spells, 3 = potions, 4 = clues

    [SerializeField]
    private GameObject _weaponsCam;
    private GameObject WeaponsCam
    {
        get { return _weaponsCam; }
    }

    [SerializeField]
    private GameObject _weapon;
    private GameObject Weapon
    {
        get { return _weapon; }
    }



	void Awake()
	//Set all the variables
	{
		Player = GetComponent<MainCharacter> ();

		//WEAPONS
		FistsObject = GameObject.Find ("Fists");
		KatanaObject = GameObject.Find ("Katana");
		KatanaObject.SetActive (false);
		KnifeObject = GameObject.Find ("Knife");
		KnifeObject.SetActive (false);
		WeaponsInventory.Add(GetComponent<Fists>());
		WeaponsInventory.Add(GetComponent<Katana>());
		WeaponsInventory.Add(GetComponent<Knife>());
		Weapon1 = WeaponsInventory[0];
		Weapon2 = WeaponsInventory[1];
		Player.ActualWeapon = Weapon1;

		//SPELLS
		FreezeObject = GameObject.Find ("Freeze");
		AirWallObject = GameObject.Find ("AirWall");
		//AirWallObject.SetActive (false);
		EarthSpikeObject = GameObject.Find ("EarthSpike");
		//EarthSpikeObject.SetActive (false);
		FireBallObject = GameObject.Find ("FireBall");
		//FireBallObject.SetActive (false);
		FlashObject = GameObject.Find ("Flash");
		//FlashObject.SetActive (false);
		SpellsInventory.Add (GetComponent<Heal> ());
		SpellsInventory.Add (GetComponent<Freeze> ());
		SpellsInventory.Add (GetComponent<AirWall> ());
		SpellsInventory.Add (GetComponent<EarthSpike> ());
		SpellsInventory.Add (GetComponent<FireBall> ());
		SpellsInventory.Add (GetComponent<Flash> ());
		Spell1 = SpellsInventory[0];
		Spell2 = SpellsInventory[1];
		Player.ActualSpell = Spell1;


		ConsumablesInventory.Add (GetComponent<HealthPotion> ());
		ConsumablesInventory.Add (GetComponent<SpeedPotion> ());
		cons1 = ConsumablesInventory [0];
		cons2 = ConsumablesInventory[1];
	}

	public void Start()
	{
		// Desactivate the weapons camera and set the weapon layer to default if it's not the local player
		if(!Player.View.isMine)
		{
			WeaponsCam.SetActive(false);
			SetLayerRecursively(Weapon, 0);
		}
	}

    private static void SetLayerRecursively(GameObject go, int layerNumber)
    {
        Transform[] trans = go.GetComponentsInChildren<Transform>(true);
        int length = trans.Length;
        for(int i = 0; i < length; i++)
        {
            trans[i].gameObject.layer = layerNumber;
        }
    }



    public void Update()
	{
		ChangeWeapon ();
		ChangeSpell ();
		ShowInventory ();
	}






	public void ShowInventory()
	// show the inventory and hide the interface, or the contrary, depending on the button wheel
	{
		if (Input.GetButtonDown ("Wheel"))
		{
			Inventory.gameObject.SetActive (true);
			Inventory.GetComponent<DisplayInventory>().DisplayWeapons();
			Cursor.lockState = CursorLockMode.None;  //unlock the mouse
			Cursor.visible = true;
			Player.IsGamePaused = true;
			Player.Interface.SetActive (false);
		}
		else if (Input.GetButtonUp ("Wheel"))
		{
			Inventory.gameObject.SetActive (false);
			Cursor.lockState = CursorLockMode.Locked;  //lock the mouse again
			Cursor.visible = false;
			SelectedObject = 1;
			TypeOfObjects = 1;
			Player.IsGamePaused = false;
			Player.Interface.SetActive (true);
		}
	}


	//WEAPONS
	private void ChangeWeapon()  
	//change weapons using shortcuts
	{
		if(Input.GetButtonDown("Weapon1"))
		{
			Weapon2.Object.SetActive (false);
			Weapon1.Object.SetActive (true);
			ActiveWeapon = 1;
		}
			
		if(Input.GetButtonDown("Weapon2"))
		{
			Weapon1.Object.SetActive (false);
			Weapon2.Object.SetActive (true);
			ActiveWeapon = 2;
		}
	}
		


	//SPELLS
	private void ChangeSpell()  
	//change spells using shortcuts
	{
		if(Input.GetButtonDown("Spell1") && Spell1 != null)
		{
			ActiveSpell = 3;
			Player.ActualSpell = Spell2;
			Debug.Log ("coucou");
		}

		if(Input.GetButtonDown("Spell2") && Spell1 != null)
		{
			ActiveSpell = 4;
			Player.ActualSpell = Spell1;
		}
	}
}
//1 WARNING
//we must do the consumables