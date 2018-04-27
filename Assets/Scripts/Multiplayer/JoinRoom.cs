using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinRoom : MonoBehaviour {

    [SerializeField]
    private Text _roomName;
    private Text RoomName
    {
        get { return _roomName; }
    }

    public void OnClick_JoinRoom()
    {

        if (PhotonNetwork.JoinRoom(RoomName.text))
        {
            print("Joining the room...");
        }
        else
        {
            print("Can't send a request to join the room.");
        }
    }

    private void OnPhotonJoinRoomFailed(object[] codeAndMessage)
    {
        print("Can't join a room : " + codeAndMessage[1]);
    }

    private void OnJoinedRoom()
    {
        print("Room joined succesfully.");
    }
}
