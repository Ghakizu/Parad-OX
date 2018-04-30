using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Infini : MonoBehaviour 
{

	bool isTeleporting;
    public GameObject player;

	void Start () 
	{
		//MyPlayer = GetComponent<PlayerStats> ();
		//player = MyPlayer.gameObject;
		isTeleporting = false;
	}
	
	void OnTriggerEnter(Collider other)
	{
		player = other.gameObject;
		if (other.CompareTag("Player"))
		{
			
			isTeleporting = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			isTeleporting = false;
		}
	}

	void OnGUI()
	{
		if (isTeleporting)
		{
			GUI.Box(new Rect (Screen.width / 2 - 100, Screen.height / 2 - 20, 200, 40), "Press T to teleport");
		}
	}

	void Update()
	{
		if (isTeleporting && Input.GetKeyDown (KeyCode.T)) 
		{
            isActive("Lvl1", player);
            isTeleporting = false;
		}
	}

    void isActive(string sceneToLoad, GameObject player)
    {
		StartCoroutine(Load_scenes.LoadScenes(sceneToLoad, player, new Vector3(-1626, -2366, -4027), new Vector3(0, 0, 0)));
    }
}
