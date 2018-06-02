using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInventory : MonoBehaviour 
{
	//The different methods that the player can use to manage his inventory


	public MainCharacter Player;  //The player who this inventory belongs to


	//Weapons Inventory
	public List<_Weapons> WeaponsInventory = new List<_Weapons>();  //The inventory of the weapons of the player
	public GameObject FistsObject;  //the gameObject of the Fists
	public GameObject KatanaObject;  //the gameObject of the Katana
	public GameObject KnifeObject;  //the gameObject of the Knife
	public _Weapons Weapon1; //weapon of the first shortcut
	public _Weapons Weapon2; //weapon of the second shortcut


	//Spells Inventory
	public List<_Spells> SpellsInventory = new List<_Spells>();  //The inventory of the spells of the player
	public GameObject FreezeObject;  //the gameObject of the freeze spell
	public GameObject AirWallObject;  //the gameObject of the airwall spell
	public GameObject EarthSpikeObject;  //the gameObject of the earth spike spell
	public GameObject FireBallObject;  //the gameObject of the fire ball spell
	public GameObject FlashObject;  //the gameObject of the Flash spell
	public GameObject HealObject; //The gameObject of the HealObject
	public _Spells Spell1; //Spell of the first shortcut
	public _Spells Spell2; //Spell of the second shortcut


	//Consumables inventory
	public List<_Consumables> ConsumablesInventory; //Pickables that affect Player stats.
	public GameObject HealPotionObject;  //The gameObject of the Heal Potion
	public GameObject SpeedPotionObject;  //The gameObject of the Speed Potion
	public GameObject ManaPotionObject;  //The gameObject of the Mana Potion
	public GameObject StaminaPotionObject;  //The gameObject of the Stamina Potion
	public _Consumables cons1;  //Consumable of the first shortcut
	public _Consumables cons2;  //Consumable of the second shortcut


	//CluesInventory
	public List<_Clues> CluesInventoryLvl1 = new List<_Clues>(); //Clues to launch level 1
	public List<_Clues> CluesInventoryLvl2 = new List<_Clues>(); //Clues to launch level 2
	public List<_Clues> CluesInventoryLvl3 = new List<_Clues>(); //Clues to launch level 3


	//To affect the objects
	public Canvas Inventory;  //The canvas where we display the inventory
	public int SelectedObject = 1;  //1 = actualweapon1, 2 = actualweapon2
	public int TypeOfObjects = 1; //1 = weapons, 2 = spells, 3 = potions, 4 = clues
	public int SelectedWeapon = 1;  //which weapon is active
	public int SelectedSpell = 1;  //which spell is active
	public int SelectedConsumable = 1;  //which consumable is active
	public int SelectedClue = 1;  //which clues should be displayed


	void Start()
	//Set all the variables
	{
		Player = GetComponent<MainCharacter> ();

		//WEAPONS
		FistsObject = GetComponentInChildren<Fists>().gameObject;
		KatanaObject = GetComponentInChildren<Katana>().gameObject;
		KatanaObject.SetActive (false);
		KnifeObject = GetComponentInChildren<Knife>().gameObject;
		KnifeObject.SetActive (false);
		WeaponsInventory.Add(FistsObject.GetComponent<Fists>());
		WeaponsInventory.Add(KatanaObject.GetComponent<Katana>());
		WeaponsInventory.Add(KnifeObject.GetComponent<Knife>());
		Weapon1 = WeaponsInventory[0];
		Weapon2 = WeaponsInventory[1];
		Player.ActualWeapon = Weapon1;

		//SPELLS
		FreezeObject = GetComponentInChildren<Freeze>().gameObject;
		FreezeObject.SetActive (false);
		AirWallObject = GetComponentInChildren<AirWall>().gameObject;
		AirWallObject.SetActive (false);
		EarthSpikeObject = GetComponentInChildren<EarthSpike>().gameObject;
		EarthSpikeObject.SetActive (false);
		FireBallObject = GetComponentInChildren<FireBall>().gameObject;
		FireBallObject.SetActive (false);
		FlashObject = GetComponentInChildren<Flash>().gameObject;
		FlashObject.SetActive (false);
		HealObject = GetComponentInChildren<Heal> ().gameObject;
		SpellsInventory.Add (HealObject.GetComponent<Heal> ());
		SpellsInventory.Add (FreezeObject.GetComponent<Freeze> ());
		SpellsInventory.Add (AirWallObject.GetComponent<AirWall> ());
		SpellsInventory.Add (EarthSpikeObject.GetComponent<EarthSpike> ());
		SpellsInventory.Add (FireBallObject.GetComponent<FireBall> ());
		SpellsInventory.Add (FlashObject.GetComponent<Flash> ());
		Spell1 = SpellsInventory[1];
		Spell2 = SpellsInventory[0];
		Player.ActualSpell = Spell1;

		//CONSUMABLES
		ConsumablesInventory.Add (GetComponent<HealthPotion> ());
		ConsumablesInventory.Add (GetComponent<SpeedPotion> ());
		ConsumablesInventory.Add (GetComponent<StaminaPotion> ());
		ConsumablesInventory.Add (GetComponent<ManaPotion> ());
		HealPotionObject = GetComponentInChildren<HealthPotion> ().gameObject;
		SpeedPotionObject = GetComponentInChildren<SpeedPotion> ().gameObject;
		ManaPotionObject = GetComponentInChildren<ManaPotion> ().gameObject;
		StaminaPotionObject = GetComponentInChildren<StaminaPotion> ().gameObject;
		cons1 = ConsumablesInventory [0];
		cons2 = ConsumablesInventory[1];
		Player.ActualConsumable = cons1;
	}
		

    public void Update()
	//Use the inventory
	{
		ChangeWeapon ();
		ChangeSpell ();
		ChangeConsumables ();
		ShowInventory ();
	}





	//Inventory
	public void ShowInventory()
	//Show the inventory and hide the interface, or the contrary, depending on the button wheel
	{
		if (Input.GetButtonDown ("Wheel") && !Player.IsGamePaused)
		{
			Inventory.gameObject.SetActive (true);
			Inventory.GetComponent<DisplayInventory>().DisplayWeapons();
			Player.IsDisplaying = true;
			Player.Interface.SetActive (false);
		}
		else if (Input.GetButtonUp ("Wheel") || Input.GetKeyDown(KeyCode.Escape))
		{
			Inventory.gameObject.SetActive (false);
			TypeOfObjects = 1;
			Player.IsDisplaying = false;
			Player.Interface.SetActive (true);
			if (Player.IsGamePaused)
			{
				Player.PauseMenu.SetActive (false);  //put the pause menu to the foreground
				Player.PauseMenu.SetActive (true);
			}
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
			Player.ActualWeapon = Weapon1;
			SelectedWeapon = 1;
		}
			
		if(Input.GetButtonDown("Weapon2"))
		{
			Weapon1.Object.SetActive (false);
			Weapon2.Object.SetActive (true);
			Player.ActualWeapon = Weapon2;
			SelectedWeapon = 2;
		}
	}
		


	//SPELLS
	private void ChangeSpell()  
	//change spells using shortcuts
	{
		if(Input.GetButtonDown("Spell1") && Spell1 != null)
		{
			Spell2.Object.SetActive (false);
			Spell1.Object.SetActive (true);
			Player.ActualSpell = Spell1;
			SelectedSpell = 1;
		}

		if(Input.GetButtonDown("Spell2") && Spell2 != null)
		{
			Spell1.Object.SetActive (false);
			Spell2.Object.SetActive (true);
			Player.ActualSpell = Spell2;
			SelectedSpell = 2;
		}
	}



	//CONSUMABLES
	private void ChangeConsumables()
	//Change the consumables using shortcuts
	{
		if(Input.GetButtonDown("Consumable1") && cons1 != null)
		{
			Player.ActualConsumable = cons1;
			SelectedConsumable = 1;
		}

		if(Input.GetButtonDown("Consumable2") && cons2 != null)
		{
			Player.ActualConsumable = cons2;
			SelectedConsumable = 2;
		}
	}
}
