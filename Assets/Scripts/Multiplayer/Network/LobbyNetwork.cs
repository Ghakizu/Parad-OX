using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNetwork : MonoBehaviour
{
    private string Version = "Alpha v0.0.1";
/*
    // Use this for initialization
    private void Start()
    {
        if (!PhotonNetwork.connected)
        {
            print("Connecting to server..");
            PhotonNetwork.ConnectUsingSettings("0.0.0");
        }
    }

    private void OnConnectedToMaster()
    {
        print("Connected to master.");
        PhotonNetwork.automaticallySyncScene = true;
       // PhotonNetwork.playerName = PlayerNetwork.Instance.PlayerName;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    private void OnJoinedLobby()
    {
        print("Joined lobby.");

        if (!PhotonNetwork.inRoom) ;
         //   MainCanvasManager.Instance.LobbyCanvas.transform.SetAsLastSibling();
    } */

    void Start ()
    {

    }

    public void OnClick_LobbyNetwork()
    {
        Debug.LogError("Connecting to server...");
        PhotonNetwork.ConnectUsingSettings(Version);
    }

    private void Update()
    {
        Debug.LogError(PhotonNetwork.connectionState);
    }

    private void OnConnectedToMaster()
    {
        Debug.LogError("Connected to master.");
        PhotonNetwork.playerName = PlayerNetwork.Instance.PlayerName;

        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    private void OnJoinedLobby()
    {
        Debug.LogError("Joined lobby");

    }
}