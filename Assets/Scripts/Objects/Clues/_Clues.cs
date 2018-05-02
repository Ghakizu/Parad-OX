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
		level = 1;
		Object = this.gameObject;
	}


	public void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			Player = other.gameObject;
			MyPlayer = Player.GetComponent<PlayerInventory> ();
		}
	}


	public void OnMouseDown()
	{

		Vector3 offset = Player.transform.position - this.transform.position; //Le décalage entre l'objet et notre perso
		if (offset.magnitude < 20000) //On check si on est assez prêts de l'objet pour interagir avec lui
		{
			Debug.Log ("added");
			AddIntoInventory ();
			GameObject.Destroy (this.gameObject);
		}
	}



	public void Update()
	{
		if (MyPlayer != null && MyPlayer.CluesInventoryLvl1.Contains (this))
		{
			GameObject.Destroy (this.gameObject);

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
