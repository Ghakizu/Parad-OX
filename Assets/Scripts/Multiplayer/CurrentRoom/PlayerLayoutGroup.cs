using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayoutGroup : MonoBehaviour {

    [SerializeField]
    private GameObject _playerListingPrefab;
    private GameObject PlayerListingPrefab
    {
        get { return _playerListingPrefab; }
    }

    private List<PlayerListing> _playerListings = new List<PlayerListing>();
    private List<PlayerListing> PlayerListings
    {
        get { return _playerListings; }
    }
    
    private void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        PhotonNetwork.LeaveRoom();
    }


    // Called by photon when you join a room.
    private void OnJoinedRoom()
    {

        int length = transform.childCount;
        for(int i = 0; i < length; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        PhotonPlayer[] photonPlayers = PhotonNetwork.playerList;
        length = photonPlayers.Length;
        for(int i = 0; i < length; i++)
        {
            PlayerJoinedRoom(photonPlayers[i]);
        }
    }

    private void OnPhotonPlayerConnected(PhotonPlayer photonPlayer)
    {
        PlayerJoinedRoom(photonPlayer);
    }

    // Called by photon when someone leave the room.
    private void OnPhotonPlayerDisconnected(PhotonPlayer photonPlayer)
    {
        PlayerLeftRoom(photonPlayer);
    }

    private void PlayerJoinedRoom(PhotonPlayer photonPlayer)
    {
        if (photonPlayer == null)
            return;

        PlayerLeftRoom(photonPlayer);

        GameObject playerListingObj = Instantiate(PlayerListingPrefab);
        playerListingObj.transform.SetParent(transform, false);

        PlayerListing playerListing = playerListingObj.GetComponent<PlayerListing>();
        playerListing.ApplyPhotonPlayer(photonPlayer);

        PlayerListings.Add(playerListing);
    }

    private void PlayerLeftRoom(PhotonPlayer photonPlayer)
    {
        int i = PlayerListings.FindIndex(x => x.PhotonPlayer == photonPlayer);
        if(i != -1)
        {
            Destroy(PlayerListings[i].gameObject);
            PlayerListings.RemoveAt(i);
        }
    }

    public void OnClickRoomState()
    {
        if(PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.room.IsOpen = !PhotonNetwork.room.IsOpen;
            PhotonNetwork.room.IsVisible = PhotonNetwork.room.IsOpen;
        }

    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

}
