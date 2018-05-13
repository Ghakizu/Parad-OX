using System.Collections;
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

	//DIFFERENT TIMERS
	public float MaxTimeToTarget = 3;  //How long do the character will continue to follow you after he doesn't see you anymore
	public float TimeToTarget = 0;  //if this value = 0, he will not chase anyone. Else, he has a target 
	public float MaxWaitTime = 0.5f;  //How much time to the character will wait between two turns
	public float WaitTime = 0;  //actual Time to wait
	public float MaxTimeTurning = 1;  //how long do we have to turn
	public float TimeTurning = 0;  //time left to turn
	public float MaxTotalTimeToWait;  //the time for a loop (turning, waiting, turning, etc)
	public float TotalTimeToWait = 0;  //How much time does the character have to finish his loop
	public bool left = false;  //are we turning to the left
	public bool right = false;  //are we turning to the right
	public bool IsWaiting = true;  //are we waiting
	public Vector3 PatrolLocation = Vector3.zero;  //the next location where the character must move
	public float DistanceAroundSpawnPoint = 200;  //How far can the enemy move around his SpawnPoint ?
	public float timerToReset = 5;  //when do we want our character to be reset ?





	new void Awake ()
	//Setting all the basic stats;
	{
		base.Awake ();
		agent = GetComponent<NavMeshAgent> ();
		DetectionCollider = GetComponentInChildren<SphereCollider> ();
		DetectionCollider.isTrigger = true;
		DetectionCollider.radius = RangeOfDetection;
		target = null;
		agent.speed = WalkSpeed;
		agent.acceleration = RunSpeed;
		agent.angularSpeed = RotateSpeed;
		agent.stoppingDistance = ActualWeapon.RangeOfAttk - 10;
		CharacterObject.tag = "Enemy";
        MaxTotalTimeToWait = 4 * MaxTimeTurning + 4 * MaxWaitTime;
		agent.updateRotation = true;
        Health = 40;
	}



	public void OnTriggerEnter(Collider other)
	//collect all the players that are in his range of detection
	{
		if (other.tag == "Player" && !PlayersDetected.Contains(other.gameObject))
		{
			PlayersDetected.Add (other.gameObject);
		}
	}


	new public void Update()
	//move, attack, and set the stats every frames
	{	
		base.Update();
		UpdateTimes ();
		FindTarget ();
		Attack ();
		LookFor ();
	}







	public void UpdateTimes()
	//Update the timers
	{
		if (TimeToTarget >= 0)
		{
			TimeToTarget -= Time.deltaTime;
			if(TimeToTarget <= 0)
			{
				TotalTimeToWait = MaxTotalTimeToWait;
			}
		}
		if (target == null && IsWaiting && IsFreezed <= 0)
		{
			if (WaitTime >= 0)
			{
				WaitTime -= Time.deltaTime;
				if (WaitTime <= 0)
				{
					TimeTurning = MaxTimeTurning;
					if (left == right)
					{
						right = !right;
					}
					else
					{
						left = !left;
					}
				}
			}
			if(TimeTurning >= 0)
			{
				TimeTurning -= Time.deltaTime;
				if (TimeTurning <= 0)
				{
					WaitTime = MaxWaitTime;
				}
			}
		}
		if (TotalTimeToWait >= 0)
		{
			TotalTimeToWait -= Time.deltaTime;

			if(TotalTimeToWait <= 0)
			{
				IsWaiting = false;
			}
		}
		timerToReset = Math.Min (0, timerToReset - Time.deltaTime);
	}



	public void Attack()
	//Attack the player if it is possible
	{
		RaycastHit hit;
		if (IsFreezed <= 0 && target != null &&
			(target.transform.position - this.transform.position).magnitude < ActualWeapon.RangeOfAttk + 10
			&& Physics.Linecast (transform.position, target.transform.position, out hit)
			&& hit.collider.gameObject.tag == "Player")
		{
			base.Attack ((_Character)target.GetComponent<MainCharacter> ());
		}
	}



	public void FindTarget()
	//Find the target that the IA must follow and set his destination
	{
		if (IsFreezed >= 0)
		{
			//if the character is freezed, we don't want him to move
			agent.SetDestination (this.transform.position);
		}
		else
		{
			//else we find his new target
			GameObject NewTarget = null;
			float MaxDistance = RangeOfDetection + 50;
			for (int i = 0; i < PlayersDetected.Count; ++i) 
			{
				RaycastHit hit;
				if (Physics.Linecast(transform.position, PlayersDetected[i].transform.position, out hit))
				{
					float distance = hit.distance;
					if (hit.collider.gameObject.tag == "Player" && distance < MaxDistance)
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

			if (NewTarget != null)
			//if there is a target, we reset the target and make the character move to it
			{
				PatrolLocation = Vector3.zero;
				TimeToTarget = MaxTimeToTarget;
				target = NewTarget;
				Move (target.transform.position, RunSpeed);
			}

			else if(TimeToTarget > 0)
			//if the character is continuing to follow us, we just reset his destination
			{
				agent.SetDestination (target.transform.position);
			}

			else if (TotalTimeToWait > 0)
			//if we're hidden from the character, we want him to set his target to null and to Look for us
			{
				target = null;
				ResetWaitStatus ();
			}

			else
			//else, we want him to patrol
			{
				Patrol ();
			}
		}
	}



	public void Patrol ()
	//Move to a random position around the position of the spawnpoint, and then look for us
	{
		if ((this.transform.position - PatrolLocation).magnitude < agent.stoppingDistance + 5 || 
			(this.GetComponent<Rigidbody>().velocity == Vector3.zero && timerToReset <=0))
		{
			ResetWaitStatus ();
			TotalTimeToWait = MaxTotalTimeToWait;
			PatrolLocation = Vector3.zero;
		}
		else if (PatrolLocation == Vector3.zero && TotalTimeToWait <= 0)
		{
			System.Random rnd = new System.Random ();
			do 
			{
				PatrolLocation = new Vector3 (rnd.Next ((int)(SpawnPoint.x - DistanceAroundSpawnPoint), (int)(SpawnPoint.x + DistanceAroundSpawnPoint)), 
					SpawnPoint.y, rnd.Next ((int)(SpawnPoint.z - DistanceAroundSpawnPoint), (int)(SpawnPoint.z + DistanceAroundSpawnPoint)));
			} while ((this.transform.position - PatrolLocation).magnitude < agent.stoppingDistance + 30);
			Move (PatrolLocation, WalkSpeed);
		}
	}



	public void Move(Vector3 location, float speed)
	//Move to another location
	{
		IsWaiting = false;
		agent.SetDestination (location);
		agent.speed = speed;
	}


	public void LookFor()
	//Wait and watch to the right and to the left
	{
		if(IsWaiting)
		{
			agent.SetDestination (this.transform.position);
			if(TimeTurning >= 0 && right)
			{
				transform.Rotate (new Vector3 (0, -70, 0) * Time.deltaTime);
			}
			else if (TimeTurning >= 0 && !right)
			{
				transform.Rotate(new Vector3(0, 70, 0) * Time.deltaTime);
			}
		}
	}



	public void ResetWaitStatus()
	//After calling this function, the Player will look for us
	{
		if(!IsWaiting)
		{
			timerToReset = 10;
			IsWaiting = true;
			left = false;
			right = true;
			TimeTurning = 0;
			WaitTime = 0;
		}
	}
}
//WARNING! When we go behind him, the character is not looking at us anymore