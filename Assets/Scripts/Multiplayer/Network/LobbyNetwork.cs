using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNetwork : MonoBehaviour
{
    private string Version = "Alpha v0.0.1";

    // Use this for initialization
    /*  private void Start()
      {
          print("Connecting to server..");
          PhotonNetwork.ConnectUsingSettings(Version);
      } */

    [SerializeField]
    private GameObject _currentRoom;
    private GameObject CurrentRoom
    {
        get { return _currentRoom; }
    }

    [SerializeField]
    private GameObject _lobby;
    private GameObject Lobby
    {
        get { return _lobby; }
    }


    public void OnCLickSoloButton()
    {
        PhotonNetwork.offlineMode = true;
    }

    public void OnClickMultiplayerButton()
    {
        PhotonNetwork.offlineMode = false;
        print("Connecting to server..");
        PhotonNetwork.ConnectUsingSettings(Version);
    }

    private void OnConnectedToMaster()
    {
        print("Connected to master.");
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.playerName = PlayerNetwork.Instance.PlayerName;

        if(PhotonNetwork.offlineMode)
        {
            PhotonNetwork.CreateRoom("Solo");
            PhotonNetwork.LoadLevel(1);
        }
        else
            PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    private void OnJoinedLobby()
    {
        print("Joined lobby.");
        if(!PhotonNetwork.inRoom)
        {
            CurrentRoom.SetActive(false);
            Lobby.SetActive(true);
        }
        
    }
}