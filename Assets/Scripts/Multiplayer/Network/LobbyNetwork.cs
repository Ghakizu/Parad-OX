using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNetwork : MonoBehaviour
{
    private string Version = "Alpha v0.0.1";
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