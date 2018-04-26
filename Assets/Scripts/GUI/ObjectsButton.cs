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

	void Awake()
	{
		Object = null;
		ObjectName = GetComponentInChildren<Text> ();
	}

	void Update()
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
		}
	}


	public void OnMouseExit()
	{
		WeaponsStats.SetActive (false);
		ChangeInventory.SetActive (true);
	}
}
