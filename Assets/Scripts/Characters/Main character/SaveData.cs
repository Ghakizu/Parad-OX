using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviour 
{
	
    private MainCharacter player;
    private PlayerInventory inventory;


    private void Awake()
    {
        player = GetComponent<MainCharacter>();
        inventory = GetComponent<PlayerInventory>();
    }

    public void Save()
    {
		int Bool = 0;

		PlayerPrefs.SetFloat ("Health", player.Health);
		PlayerPrefs.SetFloat ("spawnX", player.SpawnPoint.x);
		PlayerPrefs.SetFloat ("spawnY", player.SpawnPoint.y);
		PlayerPrefs.SetFloat ("spawnZ", player.SpawnPoint.z);
		PlayerPrefs.SetString ("Scene", SceneManager.GetActiveScene().name);


		PlayerPrefs.SetInt ("SelectedObjects", inventory.SelectedObject);
		PlayerPrefs.SetInt ("TypeOfObjects", inventory.TypeOfObjects);
		PlayerPrefs.SetInt ("SelectedWeapon", inventory.SelectedWeapon);
		PlayerPrefs.SetInt ("SelectedSpell", inventory.SelectedSpell);
		PlayerPrefs.SetInt ("SelectedCosumable", inventory.SelectedConsumable);
		PlayerPrefs.SetInt ("SelectedClue", inventory.SelectedClue);


		//Weapons
		List<string> WeaponsInventory = new List<string> ();
		for(int i = 0; i < inventory.WeaponsInventory.Count; ++i)
		{
			WeaponsInventory.Add (inventory.WeaponsInventory [i].ObjectName);
		}

		Bool = WeaponsInventory.Contains (inventory.FistsObject.GetComponent<Fists> ().ObjectName) ? 1 : 0;
		PlayerPrefs.SetInt ("Fists", Bool);
		Bool = WeaponsInventory.Contains (inventory.SwordObject.GetComponent<Sword> ().ObjectName) ? 1 : 0;
		PlayerPrefs.SetInt ("Sword", Bool);
		Bool = WeaponsInventory.Contains (inventory.ExcaliburObject.GetComponent<Excalibur> ().ObjectName) ? 1 : 0;
		PlayerPrefs.SetInt ("Excalibur", Bool);
		Bool = WeaponsInventory.Contains (inventory.BatObject.GetComponent<Bat> ().ObjectName) ? 1 : 0;
		PlayerPrefs.SetInt ("Bat", Bool);
		Bool = WeaponsInventory.Contains (inventory.KnifeObject.GetComponent<Knife> ().ObjectName) ? 1 : 0;
		PlayerPrefs.SetInt ("Knife", Bool);

		PlayerPrefs.SetString ("Weapon1", inventory.Weapon1.ObjectName);
		PlayerPrefs.SetString ("Weapon2", inventory.Weapon2.ObjectName);
		Debug.Log(PlayerPrefs.GetString("Weapon2"));
		PlayerPrefs.SetString ("ActualWeapon", player.ActualWeapon.ObjectName);



		//Spells
		List<string> SpellsInventory = new List<string> ();
		for(int i = 0; i < inventory.SpellsInventory.Count; ++i)
		{
			SpellsInventory.Add (inventory.SpellsInventory [i].ObjectName);
		}

		Bool = SpellsInventory.Contains (inventory.HealObject.GetComponent<Heal> ().ObjectName) ? 1 : 0;
		PlayerPrefs.SetInt ("Heal", Bool);
		Bool = SpellsInventory.Contains (inventory.AirWallObject.GetComponent<AirWall> ().ObjectName) ? 1 : 0;
		PlayerPrefs.SetInt ("AirWall", Bool);
		Bool = SpellsInventory.Contains (inventory.EarthSpikeObject.GetComponent<EarthSpike> ().ObjectName) ? 1 : 0;
		PlayerPrefs.SetInt ("EarthSpike", Bool);
		Bool = SpellsInventory.Contains (inventory.FireBallObject.GetComponent<FireBall> ().ObjectName) ? 1 : 0;
		PlayerPrefs.SetInt ("FireBall", Bool);
		Bool = SpellsInventory.Contains (inventory.FreezeObject.GetComponent<Freeze> ().ObjectName) ? 1 : 0;
		PlayerPrefs.SetInt ("Freeze", Bool);
		Bool = SpellsInventory.Contains (inventory.FlashObject.GetComponent<Flash> ().ObjectName) ? 1 : 0;
		PlayerPrefs.SetInt ("Flash", Bool);

		PlayerPrefs.SetString ("Spell1", inventory.Spell1.ObjectName);
		PlayerPrefs.SetString ("Spell2", inventory.Spell2.ObjectName);
		PlayerPrefs.SetString ("ActualSpell", player.ActualSpell.ObjectName);



		//Consumables
		List<string> ConsInventory = new List<string> ();
		for(int i = 0; i < inventory.ConsumablesInventory.Count; ++i)
		{
			ConsInventory.Add (inventory.ConsumablesInventory [i].ObjectName);
		}

		Bool = ConsInventory.Contains (inventory.HealPotionObject.GetComponent<HealthPotion> ().ObjectName) ? 1 : 0;
		PlayerPrefs.SetInt ("HealPotion", Bool);
		Bool = ConsInventory.Contains (inventory.SpeedPotionObject.GetComponent<SpeedPotion> ().ObjectName) ? 1 : 0;
		PlayerPrefs.SetInt ("SpeedPotion", Bool);
		Bool = ConsInventory.Contains (inventory.ManaPotionObject.GetComponent<ManaPotion> ().ObjectName) ? 1 : 0;
		PlayerPrefs.SetInt ("ManaPotion", Bool);
		Bool = ConsInventory.Contains (inventory.StaminaPotionObject.GetComponent<StaminaPotion> ().ObjectName) ? 1 : 0;
		PlayerPrefs.SetInt ("StaminaPotion", Bool);

		PlayerPrefs.SetString ("cons1", inventory.cons1.ObjectName);
		PlayerPrefs.SetString ("cons2", inventory.cons2.ObjectName);
		PlayerPrefs.SetString ("ActualCons", player.ActualConsumable.ObjectName);


        PlayerPrefs.Save();
    }

    public void Load()
    {
		if(SceneManager.GetActiveScene().name != PlayerPrefs.GetString("Scene"))
			SceneManager.LoadScene(PlayerPrefs.GetString("Scene"));
		
		player = (PhotonNetwork.Instantiate ("Main character", player.SpawnPoint, Quaternion.identity, 0)).GetComponent<MainCharacter> ();
		inventory = player.GetComponent<PlayerInventory> ();

		player.Health = PlayerPrefs.GetFloat ("Health");
		player.SpawnPoint.x = PlayerPrefs.GetFloat ("spawnX");
		player.SpawnPoint.y = PlayerPrefs.GetFloat ("spawnY");
		player.SpawnPoint.z = PlayerPrefs.GetFloat ("spawnZ");

		player.cheatCode = false;
		player.crouch = false;


		inventory.SelectedObject = PlayerPrefs.GetInt ("SelectedObjects");
		inventory.TypeOfObjects = PlayerPrefs.GetInt ("TypeOfObjects");
		inventory.SelectedWeapon = PlayerPrefs.GetInt ("SelectedWeapon");
		inventory.SelectedSpell = PlayerPrefs.GetInt ("SelectedSpell");
		inventory.SelectedConsumable = PlayerPrefs.GetInt ("SelectedCosumable");
		inventory.SelectedClue = PlayerPrefs.GetInt ("SelectedClue");


		//Weapons
		inventory.WeaponsInventory = new List<_Weapons>();

		if (PlayerPrefs.GetInt("Fists") == 1)
		{
			inventory.WeaponsInventory.Add(inventory.FistsObject.GetComponent<Fists>());
		}
		if (PlayerPrefs.GetInt("Sword") == 1)
		{
			inventory.WeaponsInventory.Add(inventory.SwordObject.GetComponent<Sword>());
		}
		if (PlayerPrefs.GetInt("Excalibur") == 1)
		{
			inventory.WeaponsInventory.Add(inventory.ExcaliburObject.GetComponent<Excalibur>());
		}
		if (PlayerPrefs.GetInt("Bat") == 1)
		{
			inventory.WeaponsInventory.Add(inventory.BatObject.GetComponent<Bat>());
		}
		if (PlayerPrefs.GetInt("Knife") == 1)
		{
			inventory.WeaponsInventory.Add(inventory.KnifeObject.GetComponent<Knife>());
		}

		inventory.Weapon1 = SetWeapon(PlayerPrefs.GetString ("Weapon1"));
		inventory.Weapon2 = SetWeapon (PlayerPrefs.GetString ("Weapon2"));
		Debug.Log(PlayerPrefs.GetString("Weapon2"));
		player.ActualWeapon = SetWeapon(PlayerPrefs.GetString ("ActualWeapon"));



		//Spells
		inventory.SpellsInventory = new List<_Spells> ();

		if (PlayerPrefs.GetInt("Heal") == 1)
		{
			inventory.SpellsInventory.Add(inventory.HealObject.GetComponent<Heal>());
		}
		if (PlayerPrefs.GetInt("AirWall") == 1)
		{
			inventory.SpellsInventory.Add(inventory.AirWallObject.GetComponent<AirWall>());
		}
		if (PlayerPrefs.GetInt("EarthSpike") == 1)
		{
			inventory.SpellsInventory.Add(inventory.EarthSpikeObject.GetComponent<EarthSpike>());
		}
		if (PlayerPrefs.GetInt("FireBall") == 1)
		{
			inventory.SpellsInventory.Add(inventory.FireBallObject.GetComponent<FireBall>());
		}
		if (PlayerPrefs.GetInt("Freeze") == 1)
		{
			inventory.SpellsInventory.Add(inventory.FreezeObject.GetComponent<Freeze>());
		}
		if (PlayerPrefs.GetInt("Flash") == 1)
		{
			inventory.SpellsInventory.Add(inventory.FlashObject.GetComponent<Flash>());
		}

		inventory.Spell1 = SetSpell(PlayerPrefs.GetString ("Spell1"));
		inventory.Spell2 = SetSpell (PlayerPrefs.GetString ("Spell2"));
		player.ActualSpell = SetSpell(PlayerPrefs.GetString ("ActualSpell"));


		//Consumables
		inventory.ConsumablesInventory = new List<_Consumables>();

		if (PlayerPrefs.GetInt("HealPotion") == 1)
		{
			inventory.ConsumablesInventory.Add(inventory.HealPotionObject.GetComponent<HealthPotion>());
		}
		if (PlayerPrefs.GetInt("SpeedPotion") == 1)
		{
			inventory.ConsumablesInventory.Add(inventory.SpeedPotionObject.GetComponent<SpeedPotion>());
		}
		if (PlayerPrefs.GetInt("ManaPotion") == 1)
		{
			inventory.ConsumablesInventory.Add(inventory.HealPotionObject.GetComponent<HealthPotion>());
		}
		if (PlayerPrefs.GetInt("StaminaPotion") == 1)
		{
			inventory.ConsumablesInventory.Add(inventory.HealPotionObject.GetComponent<HealthPotion>());
		}

		inventory.cons1 = SetConsumables(PlayerPrefs.GetString ("cons1"));
		inventory.cons2 = SetConsumables (PlayerPrefs.GetString ("cons2"));
		player.ActualConsumable = SetConsumables(PlayerPrefs.GetString ("ActualCons"));
    }



	private _Weapons SetWeapon(string Weapon)
    {
		if (Weapon == MaterialsAssignations.FistsName)
			return inventory.FistsObject.GetComponent<Fists> ();
		if (Weapon == MaterialsAssignations.KnifeName)
			return inventory.KnifeObject.GetComponent<Knife> ();
		if (Weapon == MaterialsAssignations.SwordName)
			return inventory.SwordObject.GetComponent<Sword> ();
		if (Weapon == MaterialsAssignations.ExcaliburName)
			return inventory.ExcaliburObject.GetComponent<Excalibur> ();
		if (Weapon == MaterialsAssignations.BatName)
			return inventory.KnifeObject.GetComponent<Bat> ();
		return null;
    }

    private _Spells SetSpell(string Spell)
    {
		if (Spell == MaterialsAssignations.FreezeName)
			return inventory.FreezeObject.GetComponent<Freeze> ();
		if (Spell == MaterialsAssignations.AirWallName)
			return inventory.AirWallObject.GetComponent<AirWall> ();
		if (Spell == MaterialsAssignations.EarthSpikeName)
			return inventory.EarthSpikeObject.GetComponent<EarthSpike> ();
		if (Spell == MaterialsAssignations.FireBallName)
			return inventory.FireBallObject.GetComponent<FireBall> ();
		if (Spell == MaterialsAssignations.FlashName)
			return inventory.FlashObject.GetComponent<Flash> ();
		if (Spell == MaterialsAssignations.HealName)
			return inventory.HealObject.GetComponent<Heal> ();
		return null;
    }

    private _Consumables SetConsumables(string cons)
    {
		if (cons == MaterialsAssignations.HealthPotionName)
			return inventory.HealPotionObject.GetComponent<HealthPotion> ();
		if (cons == MaterialsAssignations.SpeedPotionName)
			return inventory.SpeedPotionObject.GetComponent<SpeedPotion> ();
		if (cons == MaterialsAssignations.ManaPotionName)
			return inventory.ManaPotionObject.GetComponent<ManaPotion> ();
		if (cons == MaterialsAssignations.StaminaPotionName)
			return inventory.StaminaPotionObject.GetComponent<StaminaPotion> ();
		return null;
    }
}
