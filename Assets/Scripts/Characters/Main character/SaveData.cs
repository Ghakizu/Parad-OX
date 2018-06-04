using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour {
    private MainCharacter player;
    private PlayerInventory inventory;
    private string Weapons1;
    private string Weapons2;
    private List<_Weapons> Weapons;
    private string Spell1;
    private string Spell2;
    private string cons1;
    private string cons2;

    private void Awake()
    {
        player = GetComponent<MainCharacter>();
        inventory = GetComponent<PlayerInventory>();
        Weapons1 = inventory.Weapon1.ObjectName;
        Weapons2 = inventory.Weapon2.ObjectName;
        Spell1 = inventory.Spell1.ObjectName;
        Spell2 = inventory.Spell2.ObjectName;
        cons1 = inventory.cons1.ObjectName;
        cons2 = inventory.cons2.ObjectName;
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("MaxStamina", player.MaxStamina);
        PlayerPrefs.SetFloat("MaxHealth", player.MaxHealth);
        PlayerPrefs.SetFloat("MaxMana", player.MaxMana);
        PlayerPrefs.SetString("Weapons1", Weapons1);
        PlayerPrefs.SetString("Weapons2", Weapons2);
        PlayerPrefs.SetString("Spell1", Spell1);
        PlayerPrefs.SetString("Spell2", Spell2);
        PlayerPrefs.SetString("cons1", cons1);
        PlayerPrefs.SetString("cons2", cons2);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        player.MaxStamina = PlayerPrefs.GetFloat("MaxStamina");
        player.MaxHealth = PlayerPrefs.GetFloat("MaxHealth");
        player.MaxStamina = PlayerPrefs.GetFloat("MaxMana");
        inventory.Weapon1 = SetWeapon(PlayerPrefs.GetString("Weapon1"));
        inventory.Weapon2 = SetWeapon(PlayerPrefs.GetString("Weapon2"));
        inventory.Spell1 = SetSpell(PlayerPrefs.GetString("Spell1"));
        inventory.Spell2 = SetSpell(PlayerPrefs.GetString("Spell2"));
        //inventory.cons1 = SetConsumables();
        //inventory.cons2 = SetConsumables();
    }

    private _Weapons SetWeapon(string Weapon)
    {
        _Weapons output =null;
        switch (Weapon)
        {
            case "Fists":
                break;

            case "Knife":
                break;

            case "Sword":
                break;

            case "Excalibur":
                break;

            case "Bat":
                break;
        }
        return output;
    }

    private _Spells SetSpell(string Spell)
    {
        _Spells output = null;
        switch (Spell)
        {
            case "Freeze":
                break;

            case "AirWall":
                break;

            case "EarthSpike":
                break;

            case "FireBall":
                break;

            case "Flash":
                break;
        }
        return output;
    }

    private _Consumables SetConsumables(string cons)
    {
        Debug.LogError("Not implemented");
        return null;
    }
}
