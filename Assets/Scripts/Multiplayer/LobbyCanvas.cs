using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvas : MonoBehaviour
{

    [SerializeField]
    private RoomLayoutGroup _roomLayoutGroup;
    private RoomLayoutGroup RoomLayoutGroup
    {
        get { return _roomLayoutGroup; }
    }

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

    public void OnClickJoinRoom(string roomName)
    {
        if (PhotonNetwork.JoinRoom(roomName))
        {
            Lobby.SetActive(false);
            CurrentRoom.SetActive(true);
        }
        else
            print("Join room failed.");
    }

}
