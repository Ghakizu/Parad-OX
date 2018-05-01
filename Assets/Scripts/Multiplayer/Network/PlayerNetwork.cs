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
	private string Name = "Labyrinthe bis";
    private int PlayerInGame = 0;
    private Queue<GameObject> spawn;

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
        PhotonNetwork.LoadLevel(1);
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
        Vector3 position = new Vector3(Random.Range(0f, 200f), Random.Range(20f, 50f));
        PhotonNetwork.Instantiate("_Player", position, Quaternion.identity, 0);
    }
}