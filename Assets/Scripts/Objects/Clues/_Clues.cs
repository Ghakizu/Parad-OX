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



	new public void Awake()
	//Set object value
	{
		Object = this.gameObject;
	}


	public void Start()
	//Allows to find the player and to destroy the clue if already collected
	{
		Player = GameObject.FindGameObjectWithTag ("Player");
		MyPlayer = Player.GetComponent<PlayerInventory> ();
		DestroyClue ();
	}
		

	public void OnMouseDown()
	//when we click on the object, we want him to disappear and to be added into our inventory
	{
		Debug.Log ("onmousedown");
		Vector3 offset = Player.transform.position - this.transform.position;
		Debug.Log (offset.magnitude);
		if (offset.magnitude < MaximumDistance)
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
			MyPlayer.CluesInventoryLvl1.Add (this);
			break;
		case 2:
			MyPlayer.CluesInventoryLvl2.Add (this);
			break;
		case 3:
			MyPlayer.CluesInventoryLvl3.Add (this);
			break;
		}
		owner = Player.GetComponent<MainCharacter> ();
	}
}
