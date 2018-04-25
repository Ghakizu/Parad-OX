using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movingplatform : MonoBehaviour {
	public float movementamplitude = 140;
	private Vector3 origin;
	public bool up;

	void Start()
	{
		origin = transform.position;
	}

	void FixedUpdate () 
	{
		if (up && transform.position.y >= origin.y + movementamplitude)
			up = !up;
		if (!up && transform.position.y <= origin.y - movementamplitude)
			up = !up;
		if (up)
			transform.position += new Vector3 (0, 0.7f, 0);
		else
			transform.position += new Vector3 (0, -0.7f, 0);
	}
}
