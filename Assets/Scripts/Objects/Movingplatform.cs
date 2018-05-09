using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movingplatform : MonoBehaviour 
{
	//Moving platforms for the Level1


	public float movementamplitude = 140;  //How far will the platforms go
	private Vector3 origin;  //Where do the platforms come from
	public bool up;  //Are the platform moving up or down ?

	public void Start()
	//Set the origin
	{
		origin = transform.position;
	}

	public void FixedUpdate ()
	//Apply the movement
	{
		Move ();
	}


	public void Move()
	//Move the platforms
	{
		if ((up && transform.position.y >= origin.y + movementamplitude) 
			|| (!up && transform.position.y <= origin.y - movementamplitude))
		{
			up = !up;
		}
		if (up)
		{
			transform.position += new Vector3 (0, 0.7f, 0);
		}
		else
		{
			transform.position += new Vector3 (0, -0.7f, 0);
		}
	}
}
