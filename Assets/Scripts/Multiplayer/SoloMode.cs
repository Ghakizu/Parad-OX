using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloMode : MonoBehaviour
{
    public void OnCLickSoloButton()
    {
		PlayerPrefs.SetInt ("LOAD", 0);
        if (PhotonNetwork.connected)
            PhotonNetwork.Disconnect();
        else
            PhotonNetwork.offlineMode = true;
    }

    private void OnConnectedToMaster()
    {
        print("Connected to master.");
        PhotonNetwork.automaticallySyncScene = false;
        PhotonNetwork.playerName = PlayerNetwork.Instance.PlayerName;

        if (PhotonNetwork.offlineMode)
        {
            PhotonNetwork.CreateRoom("Solo");
            PhotonNetwork.LoadLevel("RealWorld");
        }
    }

    private void OnDisconnectedFromPhoton()
    {
        PhotonNetwork.offlineMode = true;
    }
}