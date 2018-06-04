using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Correct_Answer : MonoBehaviour {
    
    public doorStayOpen door;
    public bool mainRoom;
    public float x;
    public float y;
    public float z;

    void OnMouseDown()
	{
        if (mainRoom)
            door.Open();
        else
        {
            (GameObject.FindGameObjectWithTag("Player")).transform.position = new Vector3(x, y, z);
        }
	}
}
