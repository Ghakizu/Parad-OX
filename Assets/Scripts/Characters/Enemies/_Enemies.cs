﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;



[RequireComponent(typeof(NavMeshAgent))]

public abstract class _Enemies : _Character 
{
	//All the ennemies, they are able to Attack, to defend, to die ....

	//STATS
	public GameObject target;  //the target that the character has to follow
	public float RangeOfDetection;  //how far do the character will detect us
	private SphereCollider DetectionCollider;  //The range of detection of the character. If you enter in it, he will chase you
	public List<GameObject> PlayersDetected = new List<GameObject>();  //the list of the players that are detected
	public NavMeshAgent agent;  //the NavMeshAgent of the character
	public Vector3 PatrolLocation;  //the next location where the character must move
	public float PatrolDistance = 200;  //How far can the enemy move around his SpawnPoint ?
	public float Timer = 0;  //the timer that lead the character

	//DIFFERENT TIMERS
	public float TargetTime = 3;  //how long will the ennemy continue to follow you
	public float WaitTime = 0.5f;  //How long does the ennemy wait between two turns
	public float TurnTime = 1;  //During how much time does the enemy turn around
	public float ResetTime = 5;  //when do we want our character to be reset ?
	public float AttackTime = 10.5f;  //How long does an attack take

	//DIFFERENT BOOLEANS
	public bool IsWaiting = true;  //are we waiting
	public bool IsTargetting = false;  //Are we followinf someone
	public bool IsTurning = true;  //are we turning around
	public bool IsWalking = false;  //are we walking (not chasing)
	public bool IsAttacking = false;  //are we attacking another character
	public bool left = false;  //are we turning to the left
	public bool right = false;  //are we turning to the right










	new void Awake ()
	//Setting all the basic stats;
	{
		TurnTime = 1;
		WaitTime = 0.5f;
		TargetTime = 3;
		base.Awake ();
		agent = GetComponent<NavMeshAgent> ();
		DetectionCollider = GetComponentInChildren<SphereCollider> ();
		DetectionCollider.isTrigger = true;
		DetectionCollider.radius = RangeOfDetection;
		target = null;
		agent.speed = WalkSpeed;
		agent.acceleration = 10000;
		agent.angularSpeed = RotateSpeed;
		agent.stoppingDistance = ActualWeapon.RangeOfAttk - 10;
		CharacterObject.tag = "Enemy";
		agent.updateRotation = true;
        Health = 40;
		AttackTime = 10.5f;
	}



	new public void Update()
	//move, attack, and set the stats every frames
	{	
		base.Update();
		if (!IsGamePaused && IsFreezed <= 0)
		{
			UpdateTimes ();
			SetNewTarget ();
			SetAgentDestination ();
			SetRotation ();
			Live ();
		}
	}



	public void OnTriggerEnter(Collider other)
	//collect all the players that are in his range of detection
	{
		if (other.tag == "Player" && !PlayersDetected.Contains(other.gameObject))
		{
			PlayersDetected.Add (other.gameObject);
		}
	}








	public void Live()
	//All the functions that the character must do
	{
		if (IsWalking && (PatrolLocation - this.transform.position).magnitude <= agent.stoppingDistance + 30 && Timer >= 2)
		{
			FindNewLocation ();
		}
		if (target != null)
		{
			if(!IsAttacking)
			{
				Vector3 WhereToLook = target.transform.position - this.transform.position;
				WhereToLook.y = 0;
				Quaternion rotation = Quaternion.LookRotation (WhereToLook);
				transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * RotateSpeed);
			}
			if ((target.transform.position - this.transform.position).magnitude <= ActualWeapon.RangeOfAttk)
			{
				agent.SetDestination (this.transform.position);
				Attack ();
			}
		}
		if (IsAttacking)
		{
			agent.SetDestination (this.transform.position);
			agent.velocity = Vector3.zero;
		}
	}

	public void UpdateTimes ()
	//Update the timer of the character
	{
		Timer = Mathf.Max (0, Timer - Time.deltaTime);
		if (target != null)
		{
			if (!IsAttacking)
			{
				Timer = TargetTime;
				IsWaiting = false;
				IsTurning = false;
				IsWalking = false;
				IsTargetting = true;
			}
			else if (IsAttacking && Timer == 0)
			{
				IsAttacking = false;
			}

		}
		else
		{
			if (Timer == 0)
			{
				if (IsTurning)
				{
					Timer = WaitTime;
					IsTurning = false;
					IsWaiting = true;
				}
				else if (IsWaiting)
				{
					if (left == right)
					{
						right = !right;
					}
					else
					{
						left = !left;
					}
					if (!left && !right)
					{
						IsWalking = true;
						Timer = ResetTime;
						FindNewLocation ();
					}
					else
					{
						IsTurning = true;
						Timer = TurnTime;
					}
					IsWaiting = false;
				}
				else if (IsWalking)
				{
					IsWalking = false;
					IsWaiting = true;
					Timer = WaitTime;
				}
				else if (IsTargetting)
				{
					IsTargetting = false;
					IsAttacking = false;
					IsWaiting = true;
					Timer = WaitTime;
					target = null;
				}
			}
		}
	}



	public void Attack()
	//Attack the player if it is possible
	{
		RaycastHit hit;
		if (IsFreezed <= 0 && target != null && IsAbleToAttack <= 0 &&
			(target.transform.position - this.transform.position).magnitude < ActualWeapon.RangeOfAttk + 10
			&& Physics.Linecast (transform.position, target.transform.position, out hit)
			&& hit.collider.gameObject.tag == "Player")
		{
			base.Attack ((_Character)target.GetComponent<MainCharacter> ());
			IsAttacking = true;
			Timer = AttackTime;
		}
	}



	public void SetNewTarget()
	//Set the target if ther is one available
	{
		float MaxDistance = RangeOfDetection + 50;
		GameObject NewTarget = null;
		for (int i = 0; i < PlayersDetected.Count; ++i)
		{
			RaycastHit hit;
			if (Physics.Linecast(transform.position, PlayersDetected[i].transform.position, out hit))
			{
				float distance = hit.distance;
				if (hit.collider.gameObject.tag == "Player" 
					&& distance < MaxDistance * hit.collider.gameObject.GetComponent<MainCharacter>().DetectionRange)
				{
					MaxDistance = distance;
					NewTarget = PlayersDetected [i];
				}
				else if (distance > RangeOfDetection + 50)
				{
					PlayersDetected.Remove (PlayersDetected[i]);
				}
			}
		}
		target = NewTarget;
	}



	public void SetAgentDestination()
	//Move the enemy to his destination
	{
		if (IsWaiting || IsFreezed >= 0 || IsTurning)
		{
			agent.SetDestination (this.transform.position);
			agent.velocity = Vector3.zero;
		}
		else if (target != null)
		{
			agent.speed = RunSpeed;
			agent.SetDestination(target.transform.position);
		}
	}



	public void SetRotation()
	//Rotate the character when needed
	{
		if (IsWaiting)
		{
			if(right)
			{
				transform.Rotate (new Vector3 (0, -70, 0) * Time.deltaTime);
			}
			else
			{
				transform.Rotate(new Vector3(0, 70, 0) * Time.deltaTime);
			}
		}
	}



	public void FindNewLocation()
	//Find the new location when patrolling (random)
	{
		System.Random rnd = new System.Random ();
		do 
		{
			PatrolLocation = new Vector3 (rnd.Next ((int)(SpawnPoint.x - PatrolDistance), (int)(SpawnPoint.x + PatrolDistance)), 
				SpawnPoint.y, rnd.Next ((int)(SpawnPoint.z - PatrolDistance), (int)(SpawnPoint.z + PatrolDistance)));
		} while ((this.transform.position - PatrolLocation).magnitude < agent.stoppingDistance + 30);
		agent.speed = WalkSpeed;
		agent.SetDestination (PatrolLocation);
	}
}
//WARNING! When we go behind him, the character is not looking at us anymore