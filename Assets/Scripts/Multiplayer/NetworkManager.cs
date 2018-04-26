using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {
    private string VERSION = "Alpha v0.0.1";

    [SerializeField]
    private InputField roomName_Input;
    private RoomOptions options;
    private string playerName = "Player";

    [SerializeField]
    private GameObject spawn;

    private string roomname = "";
	// Use this for initialization
	void Start () {
       
	}

    void OnJoinedLobby()
    {
           options = new RoomOptions();
    }

    private void Awake()
    { 
        PhotonNetwork.ConnectUsingSettings(VERSION);
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = true;
    }

    void Refresh()
    {
        if (roomName_Input.text != "")
            roomname = roomName_Input.text;
    }

    void Host()
    {
        options.IsVisible = true;
        options.MaxPlayers = 2;
        Refresh();
        PhotonNetwork.CreateRoom(roomname, options, TypedLobby.Default);
    }

    void Join()
    {
        PhotonNetwork.JoinRoom(roomname);
    }

    void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(playerName, spawn.transform.position, spawn.transform.rotation, 0);
    }
}
