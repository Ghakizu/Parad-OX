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

    public void OnClickMultiplayerButton()
    {
        print("Connecting to server..");
        PhotonNetwork.ConnectUsingSettings(Version);
    }

    private void OnConnectedToMaster()
    {
        print("Connected to master.");
        PhotonNetwork.playerName = PlayerNetwork.Instance.PlayerName;

        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    private void OnJoinedLobby()
    {
        print("Joined lobby.");
    }
}