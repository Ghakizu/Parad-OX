using UnityEngine;
using System.IO;

public class PlayerNetwork : MonoBehaviour
{

    public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }


    // Use this for initialization
    private void Awake()
    {
        Instance = this;

        PlayerName = "Player#" + Random.Range(1000, 9999);
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        float randomvalue = Random.Range(-42f, 0f);
        Vector3 position = new Vector3(randomvalue, -18, 38);
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "_Player"), position, Quaternion.identity, 0);
    }
}