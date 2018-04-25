using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class _Clues : NetworkBehaviour	
{
	//Tous les indices

	public int level;
	private GameObject Player;
	private PlayerInventory MyPlayer;
	public bool IsCollectible = false;
	public GameObject ClueObject;



	void Start()
	{
		ClueObject = this.gameObject;
		//NetworkServer.SpawnObjects ();
	}




    void AddIntoInventory ()
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
		//GameObject.Destroy (ClueObject);
	}


	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			Player = other.gameObject;
			MyPlayer = Player.GetComponent<PlayerInventory> ();
			IsCollectible = true;
		}
	}


	private void OnTriggerExit()
	{
		IsCollectible = false;
	}


	void OnGUI()
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
			//Network.Destroy(ClueObject);
			//CmdUpdateObjectState (this.gameObject, IsCollectible);
		}
	}

	/*[Command]
	public void CmdUpdateObjectState(GameObject MyObject, bool IsCollectible)
	{
		GameObject.Destroy(MyObject);
	}*/

}
