using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTemp : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.offlineMode = true;
        PhotonNetwork.CreateRoom("Solo");
     }
}