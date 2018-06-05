﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour 
{
	//Object that spawns cars

	private GameObject cars;  //cars to spawn
	private float Timer;  //Actual timer
	private float MaxTime;  //Time between cars
	private int Consecutive = 0;  //How many consecutive ?
	private bool Inside = false;  //What is the last turn ?
	public float offset;  //When do you wantthe object to spawn cars ?


	private void Awake()
	//set some values
	{
		cars = (GameObject)Resources.Load("Moving car");
		MaxTime = 12;
		Timer = offset;
	}


	private void Update()
	//call spawncars funtcion
	{
		SpawnCars ();
	}



	public void SpawnCars()
	//spawn cars when needed
	{
		Timer = Mathf.Max (0, Timer - Time.deltaTime);
		if (Timer == 0)
		{
			Timer = MaxTime;
			GameObject car = Instantiate (cars, this.transform.position, this.transform.rotation) as GameObject;
			if (Consecutive == 3)
			{
				Consecutive = 0;
				car.GetComponentInChildren<Cars> ().inside = !Inside;
				Inside = !Inside;
			}
			else
			{
				int side = Random.Range (1, 3);
				car.GetComponentInChildren<Cars> ().inside = side == 1;
				if ((side == 1 && Inside) || (side == 2 && !Inside))
				{
					Consecutive += 1;
				}
				else
				{
					Consecutive = 0;
					Inside = side == 1;
				}
			}
		}
	}
}
