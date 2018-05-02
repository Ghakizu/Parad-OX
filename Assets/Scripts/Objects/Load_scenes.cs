using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_scenes : MonoBehaviour 
{
	//Objects that load another scene

	public string Scene; //the name of the scene to load
	public Vector3 Spawnpoint; //the place where we want our player to spawn in the nest scene. By default, it's (0, 0, 0).
    public Quaternion rotation;
	private bool IsTrigger = false; //Are we able to teleport ?
    private PhotonView PhotonView;
    private GameObject Player;

    private void Awake()
    {
        PhotonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        Player = GameObject.Find("_Player(Clone)");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            IsTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            IsTrigger = false;
    }

    private void Update()
    {
        if(IsTrigger && Input.GetKeyDown(KeyCode.E) && PhotonView.isMine)
        {
            PhotonNetwork.LoadLevel(Scene);
            Player.transform.position = Spawnpoint;
            Player.transform.rotation = rotation;
            IsTrigger = false;
        }
    }

    private void OnGUI()
    {
        if(IsTrigger && PhotonView.isMine)
        {
            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 20, 200, 40), "Press E to interact");
        }
    }

    [PunRPC]
    private void OnPlayerLoadedScene()
    {

    }

}