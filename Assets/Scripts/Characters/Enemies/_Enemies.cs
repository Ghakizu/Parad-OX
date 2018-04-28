using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



[RequireComponent(typeof(NavMeshAgent))]

public abstract class _Enemies : _Character 
{
	//All the ennemies, they are able to Attack, to defend, to die ....


	public GameObject target;
	public float RangeOfDetection;
	private SphereCollider DetectionCollider;
	public List<GameObject> PlayersDetected = new List<GameObject>();
	public NavMeshAgent agent;
	public float MaxTimeToTarget = 1.5f;
	public float ActualTime = 0;
	public float MinimumDistance = 20;
	public float NormalDistance = 20;

	new void Awake () 
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
	}


	new public void OnTriggerEnter(Collider other)
	{
		base.OnTriggerEnter (other);
		if (other.tag == "Player" && !PlayersDetected.Contains(other.gameObject))
		{
			PlayersDetected.Add (other.gameObject);
			other.GetComponent<MainCharacter> ();
		}
	}


	new public void Update()
	{	
		base.Update();
		if (IsFreezed <= 0)
		{
			FindTarget ();
			if (target != null) 
			{
				RaycastHit hit;
				if ((target.transform.position - this.transform.position).magnitude < ActualWeapon.RangeOfAttk
					&& Physics.Linecast (transform.position, target.transform.position, out hit)
					&& hit.collider.gameObject.tag == "Player") //always true we just want to get the "hit" value
				{ 
					Attack ((_Character)target.GetComponent<MainCharacter> ());
				}
			}
		}
		else
		{
			agent.SetDestination (this.transform.position);
		}
	}





	public void FindTarget()
	{
		GameObject NewTarget = null;
		float Distancetarget = RangeOfDetection + 50;
		for (int i = 0; i < PlayersDetected.Count; ++i) 
		{
			float distance = (PlayersDetected [i].transform.position - this.transform.position).magnitude;
			RaycastHit hit;
			if (Physics.Linecast(transform.position, PlayersDetected[i].transform.position, out hit)) //always true we just want to get the "hit" value
			{
				if (hit.collider.gameObject.tag == "Player" 
					&& distance < Distancetarget)
				{
					Distancetarget = distance;
					NewTarget = PlayersDetected [i];
				}
				else if (distance > RangeOfDetection + 50)
				{
					PlayersDetected.Remove (PlayersDetected[i]);
				}
			}
		}
		if (NewTarget == null && ActualTime > 0) 
		{
			ActualTime -= Time.deltaTime;
		} 
		else if (NewTarget != null) 
		{
			ActualTime = MaxTimeToTarget;
			target = NewTarget;
			agent.SetDestination (target.transform.position);
			agent.speed = RunSpeed;
		}
		else
		{
			if(target != null)
			{
				target = null;
				agent.SetDestination (SpawnPoint);
				agent.speed = WalkSpeed;
			}
		}
	}

}
