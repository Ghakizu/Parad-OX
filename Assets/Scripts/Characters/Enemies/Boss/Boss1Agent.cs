using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class Boss1Agent : _Enemies 
{

	private GameObject Player;
	public NavMeshAgent agent;
	private float DelayBetweenAttack = 1; //The delay between two attacks must depend on the weapon
	private float AbleToAttack = 0;


	new void Awake () 
	{
		base.Awake ();
		Player = GameObject.FindGameObjectWithTag("Player");
		agent.SetDestination (Player.transform.position);
	}
	

	new void Update () 
	{
		base.Update ();
		if (Vector3.Distance(agent.transform.position, Player.transform.position ) <= 100)
		{
			AbleToAttack = DelayBetweenAttack;
		}
		else if (AbleToAttack > 0)
		{
			AbleToAttack -= Time.deltaTime;
		}
		else
		{
			agent.SetDestination (Player.transform.position);
		}
	}

}
