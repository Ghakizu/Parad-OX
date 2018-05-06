using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour {

    bool isTrigger = false;
    private PhotonView PhotonView;
    private GameObject Player;
    [SerializeField]
    private GameObject _spawn;
    private GameObject Spawn
    {
        get { return _spawn; }
    }
    

    private void Awake()
    {
        PhotonView = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isTrigger = true;
            Player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isTrigger = false;
        }
    }

    private void OnGUI()
    {
        if(isTrigger && PhotonView.isMine)
        {
            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 20, 200, 40), "Press E to teleport");
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && isTrigger)
        {
            Player.transform.SetPositionAndRotation(Spawn.transform.position, Spawn.transform.rotation);
        }
    }
}

