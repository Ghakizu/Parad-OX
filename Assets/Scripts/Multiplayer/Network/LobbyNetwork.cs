using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNetwork : MonoBehaviour
{
    private string Version = "Alpha v0.0.3";
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

    private PlayerNetwork _playerNetwork;
    private PlayerNetwork PlayerNetwork
    {
        get { return _playerNetwork; }
    }

    [SerializeField]
    private GameObject _lobby;
    private GameObject Lobby
    {
        get { return _lobby; }
    }

    private void Awake()
    {
        PhotonNetwork.Disconnect();
        print("Connecting to server..");
        PhotonNetwork.ConnectUsingSettings(Version);
        _playerNetwork = GameObject.FindGameObjectWithTag("DDOL").GetComponentInChildren<PlayerNetwork>();
    }

    public void OnClickDisconnect()
    {
        PhotonNetwork.Disconnect();
    }

    private void OnConnectedToMaster()
    {
        print("Connected to master.");
        PhotonNetwork.automaticallySyncScene = false;
        PhotonNetwork.playerName = PlayerNetwork.Instance.PlayerName;

        if(PhotonNetwork.offlineMode)
        {
            PhotonNetwork.CreateRoom("Solo");
            PhotonNetwork.LoadLevel("RealWorld");
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

    private void OnDisconnectedFromPhoton()
    {
        if(!PlayerNetwork.LaunchMulti)
        {
            PhotonNetwork.offlineMode = true;
        }
    }
}