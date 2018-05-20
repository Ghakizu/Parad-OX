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

	//Here, the object Name is the tag of the our clue -> if the inventory already contains a clue named like this, 
	//the clue is already collected and we want it to disappear



	new public void Awake()
	//just set the Object value : we don't want to set the owner because at the beginning there is no owner
	{
		level = 1;
		Object = this.gameObject;
	}

	public void Start()
	//Allows to find the player (he is only because the clues appear only on solo mode) ans to set player and Myplayer values
	{
		Player = GameObject.FindGameObjectWithTag ("Player");
		MyPlayer = Player.GetComponent<PlayerInventory> ();
	}
		

	public void OnMouseDown()
	//when we click on the object, we want him to disappear and to be added into our inventory
	{
		Vector3 offset = Player.transform.position - this.transform.position; //Le décalage entre l'objet et notre perso
		if (offset.magnitude < MaximumDistance) //On check si on est assez prêts de l'objet pour interagir avec lui
		{
			AddIntoInventory ();
			this.gameObject.SetActive (false);
			//GameObject.Destroy (this.gameObject);
		}
	}



	public void Update()
	{
		/*if (MyPlayer != null && MyPlayer.CluesInventoryLvl1.Contains (this))
		{
			GameObject.Destroy (this.gameObject);
		}*/
	}



    public void AddIntoInventory ()
	//function to call when we click on the object : add the clue to the inventory that we want
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
		owner = Player.GetComponent<MainCharacter> ();
	}
}
