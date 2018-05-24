using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Correct_Answer : MonoBehaviour {

	public GameObject Door;
    public bool mainRoom;
    public float x;
    public float y;
    public float z;

    void OnMouseDown()
	{
        if (mainRoom)
            GameObject.Destroy(Door);
        else
        {
            (GameObject.FindGameObjectWithTag("Player")).transform.position = new Vector3(x,y,z);
        }
	}
}
