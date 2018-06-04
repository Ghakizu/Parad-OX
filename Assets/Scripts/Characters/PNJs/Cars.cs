using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cars : MonoBehaviour 
{
	public bool inside;  //Is the car inside the city ?
	public bool HasStartedTurn;  //Has the car started to turn
	public float canTurn;  //Is the car able to turn ?
	public bool HasEntered;  //Has the car entered the city ?
	private GameObject car;  //Game object of the car

	public void Awake()
	//Set the values of the car
	{
		car = this.gameObject.transform.parent.gameObject;
		HasEntered = false;
	}


	public void Start()
	//Set the inside value
	{
		HasStartedTurn = !inside;
	}


	public void OnTriggerEnter(Collider other)
	//Destroy and turn the car if needed
	{
		if (other.tag == "CarsTurns")
		{
			if (HasStartedTurn && canTurn <= 0)
			{
				if (other.gameObject.GetComponent<BoxCollider>() != null)
				{
					car.transform.Rotate (0, 90, 0);
					canTurn = .5f;
				}
				else if (other.gameObject.GetComponent<SphereCollider>() != null)
				{
					car.transform.Rotate (0, -90, 0);
					canTurn = .5f;
				}
			}
			else
			{
				if (!HasStartedTurn)
				{
					if (other.gameObject.GetComponent<CapsuleCollider>() != null)
					{
						HasStartedTurn = true;
						car.transform.Rotate (0, -90, 0);
						canTurn = .5f;
					}
				}
			}
		}
		if (other.tag == "Border")
		{
			if (!HasEntered)
			{
				HasEntered = true;
			}
			else
			{
				GameObject.Destroy (car, 3);
			}
		}
	}


	public void Update()
	//Move the car and set the value of canTurn
	{
		car.transform.Translate (new Vector3(-5, 0, 0));
		canTurn = Mathf.Max (0, canTurn - Time.deltaTime);
	}

}
