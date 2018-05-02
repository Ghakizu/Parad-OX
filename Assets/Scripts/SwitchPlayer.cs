using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayer : MonoBehaviour
{

    private PhotonView PhotonView;
    private GameObject sourcePlayer;
    private bool isTrigger = false;
    private Vector3 sourcePosition;
    private Quaternion sourceRotation;
    private GameObject destinationPlayer;

    private void Awake()
    {
        PhotonView = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isTrigger = false;
        }
    }

    private void OnGUI()
    {
        if (isTrigger && PhotonView.isMine)
        {
            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 20, 200, 40), "Press E to teleport");
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isTrigger && PhotonView.isMine)
        {
            PhotonView.RPC("Switch_Player", PhotonTargets.All);
        }
    }

    [PunRPC]
    void Switch_Player()
    {
        var list = FindObjectsOfType<MainCharacter>();
        sourcePlayer = list[0].gameObject;
        destinationPlayer = list[1].gameObject;
        sourcePosition = sourcePlayer.transform.position;
        sourceRotation = sourcePlayer.transform.rotation;
        sourcePlayer.transform.SetPositionAndRotation(destinationPlayer.transform.position, destinationPlayer.transform.rotation);
        destinationPlayer.transform.SetPositionAndRotation(sourcePosition, sourceRotation);
    }


}
