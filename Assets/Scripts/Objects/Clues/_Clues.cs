using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Clues : _Objects	
{
	//All the clues that allows the access to the different levels


	public int level;  //The corresponding level of the clue
	public GameObject Player;  //the gameObject of the player who will get the clue
	public PlayerInventory MyPlayer;  //the inventory of the player who will get the clue
	public int MaximumDistance = 200;  //from where do you want to pick up the Object ?
	public string tag;
	public bool collectible = true;



	new public void Awake()
	//Set object value
	{
		base.Awake ();
		Object = this.gameObject;
	}


	public void Start()
	//Allows to find the player and to destroy the clue if already collected
	{
		Player = GameObject.FindGameObjectWithTag ("Player");
		MyPlayer = Player.GetComponent<PlayerInventory> ();
		DestroyClue ();
	}
		

	public void OnTriggerStay()
	//when we click on the object, we want him to disappear and to be added into our inventory
	{
		if (Input.GetKeyDown(KeyCode.Mouse0) && collectible)
		{
			AddIntoInventory ();
			GameObject.Destroy (this.gameObject);
		}
	}





	public void DestroyClue()
	//destroy the clues if already collected
	{
		switch (level)
		{
		case 1:
			for (int i = 0; i < MyPlayer.CluesInventoryLvl1.Count; ++i) 
			{
				if (MyPlayer.CluesInventoryLvl1 [i].ObjectName == this.ObjectName) 
				{
					GameObject.Destroy (this.gameObject);
				}
			}
			break;
		case 2:
			for (int i = 0; i < MyPlayer.CluesInventoryLvl2.Count; ++i) 
			{
				if (MyPlayer.CluesInventoryLvl2 [i].ObjectName == this.ObjectName) 
				{
					GameObject.Destroy (this.gameObject);
				}
			}
			break;
		case 3:
			for (int i = 0; i < MyPlayer.CluesInventoryLvl3.Count; ++i) 
			{
				if (MyPlayer.CluesInventoryLvl3 [i].ObjectName == this.ObjectName) 
				{
					GameObject.Destroy (this.gameObject);
				}
			}
			break;
		}
	}



    public void AddIntoInventory ()
	//Add the clue into the inventory of the player
	{
		switch (level)
		{
		case 1:
			MyPlayer.CluesInventoryLvl1.Add (GetComponent<_Clues> ());
			int length = MyPlayer.CluesInventoryLvl1.Count - 1;
			MyPlayer.CluesInventoryLvl1 [length].description = description;
			MyPlayer.CluesInventoryLvl1 [length].level = 1;
			MyPlayer.CluesInventoryLvl1 [length].ObjectName = ObjectName;
			MyPlayer.CluesInventoryLvl1 [length].sprite = tag == "News" ? Materials.NewsSprite : Materials.PNJSprite;
			break;
		case 2:
			MyPlayer.CluesInventoryLvl2.Add (GetComponent<_Clues> ());
			int length2 = MyPlayer.CluesInventoryLvl2.Count - 1;
			MyPlayer.CluesInventoryLvl2 [length2].description = description;
			MyPlayer.CluesInventoryLvl2 [length2].level = 2;
			MyPlayer.CluesInventoryLvl2 [length2].ObjectName = ObjectName;
			MyPlayer.CluesInventoryLvl2 [length2].sprite = tag == "News" ? Materials.NewsSprite : Materials.PNJSprite;
			break;
		case 3:
			MyPlayer.CluesInventoryLvl3.Add (GetComponent<_Clues> ());
			int length3 = MyPlayer.CluesInventoryLvl3.Count - 1;
			MyPlayer.CluesInventoryLvl3 [length3].description = description;
			MyPlayer.CluesInventoryLvl3 [length3].level = 1;
			MyPlayer.CluesInventoryLvl3 [length3].ObjectName = ObjectName;
			MyPlayer.CluesInventoryLvl3 [length3].sprite = tag == "News" ? Materials.NewsSprite : Materials.PNJSprite;
			break;
		}
	}
}
