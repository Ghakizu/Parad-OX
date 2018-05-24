using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapped : MonoBehaviour {

	public float x;
	public float y;
	public float z;
    public bool mainRoom;
	public GameObject player;

	void OnMouseDown ()
	{
        player = GameObject.FindGameObjectWithTag("Player");
        if (mainRoom)
		    player.transform.position = new Vector3 (x, y, z);
	}
}
