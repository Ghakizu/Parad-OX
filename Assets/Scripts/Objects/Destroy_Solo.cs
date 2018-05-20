using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Solo : MonoBehaviour {

	public GameObject Door;

	void OnMouseDown()
	{
		GameObject.Destroy (Door);
	}
}
