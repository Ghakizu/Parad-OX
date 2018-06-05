using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class PNJs : _Character 
{
	//All the NPCs of the game


	public NavMeshAgent agent;  //the agent of the PNJ
	public float Walkspeed = 100;  //the speed of the character
	public float range = 1000;  //around what distance will the character look for a new location to go
	public bool IsArrived = true;  //Is the characater arrived to his destination
	public float time = 0;  //Time to reset the target


	new void Awake ()
	//Set the main stats
	{
		base.Awake ();
		WalkSpeed = 50;
		range = 1000;
		IsArrived = true;
		agent = this.gameObject.GetComponent<NavMeshAgent> ();
		agent.speed = WalkSpeed;
		agent.stoppingDistance = 0;
		agent.acceleration = 10000;
		agent.autoBraking = false;
		Health = 10;
	}



	public new void Update()
	//Call all the others functions
	{
		if (!IsGamePaused)
		{
			base.Update();
			FindNewLocation();
			SetNewStats ();
		}
	}






	public void SetNewStats()
	//Set the stats of the character
	{
		time -= Time.deltaTime;
		if((agent.destination - this.transform.position).magnitude < 100 || time <= 0)
		{
			IsArrived = true;
		}
	}

	public void FindNewLocation()
	//Find a new location to go
	{
		if(IsArrived)
		{
			System.Random rnd = new System.Random ();
			Vector3 NewDestination;
			do 
			{
				NewDestination = new Vector3 (rnd.Next ((int)(this.transform.position.x - range), (int)(this.transform.position.x + range)), 
					SpawnPoint.y, rnd.Next ((int)(this.transform.position.z - range), (int)(this.transform.position.z + range)));
				agent.SetDestination(NewDestination);
				IsArrived = false;
				time = 5;
			} while ((this.transform.position - NewDestination).magnitude < 30);
		}
	}
}
