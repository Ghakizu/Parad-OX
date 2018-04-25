using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManagerCustom : NetworkManager {
    [SerializeField]
    InputField ipAddressinput;
    [SerializeField]
    Button Starthost;
    [SerializeField]
    Button Join;

    public void StartupHost()
    {
        Setport();
        NetworkManager.singleton.StartHost(); 
    }

    public void JoinGame()
    {
        SetIPAddress();
        Setport();
        NetworkManager.singleton.StartClient();
    }

    void SetIPAddress()
    {
        string ipAddress = ipAddressinput.text; 
        NetworkManager.singleton.networkAddress = ipAddress;
    }

    void Setport()
    {
        NetworkManager.singleton.networkPort = 7777;
    }
}
