using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Clues : _Objects	
{
	//All the clues that allows the access to the different levels


	public int level;  //The corresponding level of the clue
	public GameObject Player;
	public PlayerInventory MyPlayer;
	public bool IsCollectible = false;



	new public void Awake()
	//just set the Object value : we don't want to set the owner because at the beginning there is no owner
	{
		Object = this.gameObject;
	}


	public void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			Player = other.gameObject;
			MyPlayer = Player.GetComponent<PlayerInventory> ();
			IsCollectible = true;
		}
	}


	public void OnTriggerExit()
	{
		IsCollectible = false;
	}


	public void OnGUI()
	{
		if (IsCollectible)
		{
			GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 20, 200, 40), "Press E to collect");
			if (Input.GetKeyDown(KeyCode.E))
			{
				AddIntoInventory ();
			}
		}
	}


	public void OnMouseDown()
	{
		Vector3 offset = Player.transform.position - this.transform.position; //Le décalage entre l'objet et notre perso
		if (offset.magnitude < 100f) //On check si on est assez prêts de l'objet pour interagir avec lui
		{
			AddIntoInventory ();
			this.transform.Translate (0, 7, 0);
		}
	}





    public void AddIntoInventory ()
	{
		switch (level) //add the clue in the inventory desired when clicked
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
	}
}
