using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapped : MonoBehaviour {

	public float x;
	public float y;
	public float z;
	public GameObject player;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update ()
	{
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}
	}

	void OnMouseDown ()
	{
		player.transform.position = new Vector3 (x, y, z);
	}
}
