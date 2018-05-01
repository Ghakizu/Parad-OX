using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class PNJs : _Character 
{
	//All the NPCs of the game


	public NavMeshAgent agent;
	public float Walkspeed = 100;
	public float range = 1000;
	public bool IsArrived = true;
	public float time = 0;
	public string[] dialogues; //all the sentences the NPCs can say.
	//If he has different things to say, we can put them in different cases of the table.



	new void Awake ()
	{
		base.Awake ();
		WalkSpeed = 50;
		range = 1000;
		IsArrived = true;
		agent = this.gameObject.GetComponent<NavMeshAgent> ();
		agent.speed = WalkSpeed;
		agent.stoppingDistance = 0;
		agent.acceleration = WalkSpeed;
		agent.autoBraking = false;
		Health = 10;
	}



	public new void Update()
	{
		base.Update();
		FindNewLocation();
		time -= Time.deltaTime;
		if((agent.destination - this.transform.position).magnitude < 100 || time <= 0)
		{
			IsArrived = true;
		}
	}


	public void FindNewLocation()
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


	public void OnMouseDown()
	{
		//We have to display the text when we click on him
	}
}
