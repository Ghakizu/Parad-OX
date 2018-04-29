using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



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
	public bool IsWaiting = true;





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
	}



	new public void OnTriggerEnter(Collider other)
	//collect all the players that are in his range of detection
	{
		base.OnTriggerEnter (other);
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
		Wait ();
	}







	public void UpdateTimes()
	//Update the timers
	{
		if (TimeToTarget >= 0)
		{
			TimeToTarget -= Time.deltaTime;
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
		}
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
			agent.SetDestination (this.transform.position);
		}
		else
		{
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
			{
				TimeToTarget = MaxTimeToTarget;
				target = NewTarget;
				agent.SetDestination (target.transform.position);
				agent.speed = RunSpeed;
				ResetWaitStatus ();
				IsWaiting = false;
			}
			else if(TimeToTarget > 0)
			{
				agent.SetDestination (target.transform.position);
				TotalTimeToWait = MaxTotalTimeToWait;
			}
			else if (TotalTimeToWait > 0)
			{
				target = null;
				IsWaiting = true;
			}
			else if ((this.transform.position - SpawnPoint).magnitude > 50)
			{
				IsWaiting = false;
				agent.SetDestination (SpawnPoint);
				agent.speed = WalkSpeed;
				ResetWaitStatus ();
			}
			else
			{
				Patrol ();
			}
		}
	}



	public void Wait()
	{
		//Wait and watch to the right and to the left
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
	{
		if(!IsWaiting)
		{
			left = false;
			right = true;
			TimeTurning = 0;
			WaitTime = 0;
			TotalTimeToWait = 0;
		}
	}

	public void Patrol()
	{
		Wait ();
		if(TotalTimeToWait <= 0)
		{
			
		}
	}
}
