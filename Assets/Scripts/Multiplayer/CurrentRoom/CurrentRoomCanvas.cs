using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour {

    public void OnClickStartGame()
    {
        if (!PhotonNetwork.isMasterClient)
            return;
        PhotonNetwork.LoadLevel(1);
    }

}
