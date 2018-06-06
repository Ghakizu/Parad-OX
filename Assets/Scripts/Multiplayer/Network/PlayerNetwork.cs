using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerNetwork : MonoBehaviour
{

    public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }
    public GameObject spawn1;
    public GameObject spawn2;

    private PhotonView PhotonView;
    private string _name = "Maze";
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    private int PlayerInGame = 0;
    private Queue<GameObject> spawn;
    private bool isLoaded = false;
    private MainCharacter mainCharacter;
    private GameObject Player;

    // Use this for initialization
    private void Awake()
    {
        Instance = this;
        PhotonView = GetComponent<PhotonView>();

        PlayerName = "Player#" + Random.Range(1000, 9999);

        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 30;

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }


    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == Name)
        {
            if (PhotonNetwork.isMasterClient)
                MasterLoadedGame();
            else
                NonMasterLoadedGame();
        }
        else if (scene.name == "RealWorld" && !isLoaded)
        {
            Player = PhotonNetwork.Instantiate("Main character", new Vector3(231, -988, 29), Quaternion.Euler(0, -90, 0), 0);
            //Player.transform.SetParent(transform.parent, false);
            isLoaded = true;
            mainCharacter = Player.GetComponent<MainCharacter>(); 

        
        }
        else if (scene.name == "Multiplayer")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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

    private void OnDisconnectedFromPhoton()
    {
        print("Disconnected from master");
    }

    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel(_name);
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
        Vector3 position;
        if (PhotonNetwork.isMasterClient)
            position = spawn1.transform.position;
        else
            position = spawn2.transform.position;
        GameObject Player = PhotonNetwork.Instantiate("Main character", position, Quaternion.identity, 0);
        //Player.transform.SetParent(transform.parent, false);
    }
}