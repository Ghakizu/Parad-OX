using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectsButton : MonoBehaviour 
{
	public _Objects Object;
	public Text ObjectName;
	public GameObject ChangeInventory;
	public GameObject WeaponsStats;
	public PlayerInventory player;
	public Button OtherWeapon;
	private ColorBlock red;
	private ColorBlock grey;
	public Canvas Inventory;

	public void Awake()
	{
		Object = null;
		ObjectName = GetComponentInChildren<Text> ();
		red = GetComponent<Button> ().colors;
		red.normalColor = new Color32 (255, 146, 146, 200);
		grey = GetComponent<Button> ().colors;
		grey.normalColor = new Color32 (245, 245, 245, 100);
	}

	public void Update()
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
	{
		WeaponsStats.SetActive (false);
		ChangeInventory.SetActive (true);
	}


	public void SelectWeaponToAssign(int SelectedObject)
	{
		Debug.Log ("kek");
		GetComponent<Button> ().colors = red;
		player.SelectedObject = SelectedObject;
		OtherWeapon.GetComponent<Button> ().colors = grey;
	}

	public void AssignWeapon()
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
		else if(player.TypeOfObjects == 1 && Object != null)
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
