using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTemp : MonoBehaviour
{
	public int x = 0;
	public int y = 0;
	public int z = 0;

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.offlineMode = true;
        PhotonNetwork.CreateRoom("Solo");
		Vector3 position = new Vector3 (x, y, z);
		PhotonNetwork.Instantiate ("_Player", position, Quaternion.identity, 0);
	}
}