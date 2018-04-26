using System.Collections.Generic;
using UnityEngine;

public class RoomLayoutGroup : MonoBehaviour {

    [SerializeField]
    private GameObject _roomListingPrefab;
    private GameObject RoomListingPrefab
    {
        get { return _roomListingPrefab; }
    }

    private List<RoomListing> _roomListingButtons = new List<RoomListing>();
    private List<RoomListing> RoomListingButtons
    {
        get { return _roomListingButtons; }
    }

    private void OnReceivedRoomListUpdate()
    {
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        int length = rooms.Length;

        for(int i = 0; i < length; i++)
        {
            RoomReceived(rooms[i]);
        }

        RemoveOldRooms();
    }

    private void RoomReceived(RoomInfo room)
    {
        int i = RoomListingButtons.FindIndex(x => x.RoomName == room.Name);

        if (i == -1)
        {
            if(room.IsVisible && room.PlayerCount < room.MaxPlayers)
            {
                GameObject roomListingObj = Instantiate(RoomListingPrefab);
                roomListingObj.transform.SetParent(transform, false);

                RoomListing roomListing = roomListingObj.GetComponent<RoomListing>();
                RoomListingButtons.Add(roomListing);

                i = RoomListingButtons.Count - 1;
            }
        }
        else
        {
            RoomListing roomListing = RoomListingButtons[i];
            roomListing.SetRoomNameText(room.Name);
            roomListing.Updated = true;
        }
    }

    private void RemoveOldRooms()
    {
        List<RoomListing> removeRooms = new List<RoomListing>();
        int length = RoomListingButtons.Count;

        for(int i = 0; i < length; i++)
        {
            RoomListing roomListing = RoomListingButtons[i];
            if (!roomListing.Updated)
                removeRooms.Add(roomListing);
            else
                roomListing.Updated = false;
        }

        length = removeRooms.Count;

        for(int i = 0; i < length; i++)
        {
            RoomListing roomListing = removeRooms[i];
            GameObject roomListingObj = roomListing.gameObject;
            RoomListingButtons.Remove(roomListing);
            Destroy(roomListingObj);
        }
    }
}
