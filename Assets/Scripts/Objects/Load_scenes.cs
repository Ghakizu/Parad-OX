using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(PhotonView))]
public class Load_scenes : MonoBehaviour 
{
	//Objects that load another scene

	public string Scene; //the name of the scene to load
	public Vector3 Spawnpoint; //the place where we want our player to spawn in the nest scene. By default, it's (0, 0, 0).
    public Quaternion rotation;
	private bool IsTrigger = false; //Are we able to teleport ?
    private PhotonView PhotonView;
	private GameObject Player;
	private bool isloaded = false;
	private float mustload = 0;
	private int PlayerInGame = 0;

    private void Awake()
    {
        PhotonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        Player = GameObject.Find("_Player(Clone)");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
		{
			IsTrigger = true;
			Player = other.gameObject;
		}

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            IsTrigger = false;
    }

    private void Update()
    {
        if(IsTrigger && Input.GetKeyDown(KeyCode.E) && PhotonView.isMine)
        {
			Player.GetComponent<MainCharacter>().SpawnPoint = Spawnpoint;
			Player.GetComponent<SaveData> ().Save();
            PhotonNetwork.LoadLevel(Scene);
			SceneManager.sceneLoaded += OnSceneFinishedLoading;
			if (Scene == "Lvl1" ||Scene == "Boss_Centaurus")
			{
				Player.GetComponent<MainCharacter> ().Gravity = 250;
			}
			else
			{
				Player.GetComponent<MainCharacter> ().Gravity = 500;
			}
			if(Scene == "Hypogriffe")
			{
				if (PhotonNetwork.isMasterClient)
					MasterLoadedGame();
				else
					NonMasterLoadedGame();
			}
        }
    }

	private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		if(!isloaded)
		{
			Player = PhotonNetwork.Instantiate ("Main character", Spawnpoint, rotation, 0);
			isloaded = true;
			PlayerPrefs.Save();
			PlayerPrefs.SetString ("Scene", scene.name);
			PlayerPrefs.SetInt ("LOAD", 1);
			IsTrigger = false;
		}
	}

    private void OnGUI()
    {
        if(IsTrigger && PhotonView.isMine)
        {
            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 20, 200, 40), "Press E to interact");
        }
    }

	private void MasterLoadedGame()
	{
		PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);
		PhotonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
	}

	private void NonMasterLoadedGame()
	{
		PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);
	}
		
	[PunRPC]
	private void RPC_LoadGameOthers()
	{
		PhotonNetwork.LoadLevel("Hypogriffe");
	}

	[PunRPC]
	private void RPC_LoadedGameScene()
	{
		++PlayerInGame;
		if(PlayerInGame == PhotonNetwork.playerList.Length)
		{
			print("All players are loaded.");
			PhotonView.RPC("RPC_CreatePlayer", PhotonTargets.All);
		}
	}

	[PunRPC]
	private void RPC_CreatePlayer()
	{
		Vector3 position = new Vector3 (220, 87, Random.Range (-449, 306));
		Player = PhotonNetwork.Instantiate("Main character", position, Quaternion.identity, 0);
	}
}