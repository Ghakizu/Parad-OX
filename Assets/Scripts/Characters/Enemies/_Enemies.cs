using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.UI;



[RequireComponent(typeof(NavMeshAgent))]

public abstract class _Enemies : _Character 
{
	//All the ennemies, they are able to Attack, to defend, to die ....

	//STATS
	public GameObject target;  //the target that the character has to follow
	public float RangeOfDetection;  //how far do the character will detect us
	public SphereCollider DetectionCollider;  //The range of detection of the character. If you enter in it, he will chase you
	public List<GameObject> PlayersDetected = new List<GameObject>();  //the list of the players that are detected
	public NavMeshAgent agent;  //the NavMeshAgent of the character
	public Vector3 PatrolLocation;  //the next location where the character must move
	public float PatrolDistance = 200;  //How far can the enemy move around his SpawnPoint ?
	public float Timer = 0;  //the timer that lead the character

	//DIFFERENT TIMERS
	public float TargetTime = 3;  //how long will the ennemy continue to follow you
	public float WaitTime = 0.3f;  //How long does the ennemy wait between two turns
	public float TurnTime = 0.5f;  //During how much time does the enemy turn around
	public float ResetTime = 5;  //when do we want our character to be reset ?
	public float AttackTime;  //How long does an attack take

	//DIFFERENT BOOLEANS
	public bool IsWaiting = true;  //are we waiting
	public bool IsTargetting = false;  //Are we followinf someone
	public bool IsTurning = true;  //are we turning around
	public bool IsWalking = false;  //are we walking (not chasing)
	public bool IsAttacking = false;  //are we attacking another character
	public bool left = false;  //are we turning to the left
	public bool right = false;  //are we turning to the right

	//Spells effects
	public float IsFLashed = 0;  //Is he affect by the flash spell ?

	//Buttons
	public Image HealthButton;  //image displaying the health of the enemy

	//Animations
    public Animator anim;  //animator of the enemies








	new public void Awake ()
	//Setting all the basic stats;
	{
		Debug.Log ("awake");
		TurnTime = 0.5f;
		WaitTime = 0.3f;
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
		AttackTime = ActualWeapon.TimeBetweenAttacks;
		HealthButton = GetComponentInChildren<Image> ().transform.GetChild(0).GetComponent<Image>();
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
			Attack ();
			SetButton ();
            SetAnimation();
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










	public void SetButton()
	//Set the healt button
	{
		HealthButton.transform.localScale = new Vector3(Health / MaxHealth, 1, 1);
		if (target != null)
			HealthButton.transform.parent.LookAt (target.transform);
	}


	public void UpdateTimes ()
	//Update the timer of the character
	{
		Timer = Mathf.Max (0, Timer - Time.deltaTime);
		IsFLashed = Math.Max (0, IsFLashed - Time.deltaTime);
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
		if (IsFreezed <= 0 && target != null && IsAbleToAttack <= 0 && IsFLashed <= 0 &&
			(target.transform.position - this.transform.position).magnitude < ActualWeapon.RangeOfAttk + 10
			&& Physics.Linecast (transform.position, target.transform.position, out hit)
			&& hit.collider.gameObject.tag == "Player")
		{
			base.Attack ((_Character)target.GetComponent<MainCharacter> ());
			IsAttacking = true;
			Timer = AttackTime;
            anim.SetBool("Attacking", true);
            anim.SetFloat("attack", .5f);
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
					&& distance < MaxDistance * hit.collider.gameObject.GetComponentInParent<MainCharacter>().DetectionRange)
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
		if (IsFreezed > 0 || IsAttacking || IsFLashed > 0)
		{
			agent.SetDestination (this.transform.position);
			agent.velocity = Vector3.zero;
		}
		else if (target != null && (target.transform.position - this.transform.position).magnitude >= ActualWeapon.RangeOfAttk)
		{
			agent.speed = RunSpeed;
			agent.SetDestination(target.transform.position);
		}
		else if (IsWaiting || IsTurning)
		{
			agent.SetDestination (this.transform.position);
			agent.velocity = Vector3.zero;
		}
		else if (IsWalking && (PatrolLocation - this.transform.position).magnitude <= agent.stoppingDistance + 30 && Timer >= 2)
		{
			FindNewLocation ();
		}
	}



	public void SetRotation()
	//Rotate the character when needed
	{
		if (IsWaiting)
		{
			if(right)
			{
				transform.Rotate (new Vector3 (0, -140, 0) * Time.deltaTime);
			}
			else
			{
				transform.Rotate(new Vector3(0, 140, 0) * Time.deltaTime);
			}
		}
		else if (target != null && IsFLashed <= 0)
		{
			Vector3 WhereToLook = target.transform.position - this.transform.position;
			WhereToLook.y = 0;
			Quaternion rotation = Quaternion.LookRotation (WhereToLook);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * RotateSpeed);
		}
		else if (IsFLashed > 0)
		{
			transform.Rotate (new Vector3 (0, -400, 0) * Time.deltaTime);
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



    private void SetAnimation()
    //Animations for the player
    {
        if (anim.GetBool("LauchingSpell") && anim.GetFloat("spell") > 0)
            anim.SetFloat("spell", anim.GetFloat("spell") - Time.deltaTime);
        if (anim.GetBool("LauchingSpell") && anim.GetFloat("spell") <= 0)
            anim.SetBool("LauchingSpell", false);
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            if (agent.speed == RunSpeed)
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Running", true);
            }
            else if (agent.speed == WalkSpeed)
            {
                anim.SetBool("Walking", true);
                anim.SetBool("Running", false);
            }
        }
        else
        {
            anim.SetBool("Running", false);
            anim.SetBool("Walking", false);
        }
        if (IsAbleToAttack >= 0)
            anim.SetBool("Attacking", false);
        if (anim.GetFloat("attack") >= 0)
            anim.SetFloat("attack", anim.GetFloat("attack") - Time.deltaTime);

        if (ActualWeapon.ObjectName == "Fists" || ActualWeapon.ObjectName == "Knife")
        {
            anim.SetBool("Knife", true);
            anim.SetBool("Sword", false);
            anim.SetBool("Bat", false);
        }
        else if (ActualWeapon.ObjectName == "Wooden Sword" || ActualWeapon.ObjectName == "Excalibur")
        {
            anim.SetBool("Knife", false);
            anim.SetBool("Sword", true);
            anim.SetBool("Bat", false);
        }
        if (ActualWeapon.ObjectName == "Bat")
        {
            anim.SetBool("Knife", false);
            anim.SetBool("Sword", false);
            anim.SetBool("Bat", true);
        }
        if (IsAbleToAttack >= 0)
        {
            anim.SetBool("Attacking", false);
        }
    }






}
//WARNING! When we go behind him, the character is not looking at us anymore