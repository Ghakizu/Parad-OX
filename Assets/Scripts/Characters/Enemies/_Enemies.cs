using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public abstract class _Enemies : _Character 
{
	//All the ennemies, they are able to Attack, to defend, to die ....


	public GameObject target;
	public float RangeOfDetection;
	private SphereCollider DetectionCollider;
	public List<GameObject> PlayersDetected = new List<GameObject>();

	new void Awake () 
	{
		DetectionCollider = GetComponentInChildren<SphereCollider> ();
		DetectionCollider.isTrigger = true;
		DetectionCollider.radius = RangeOfDetection;
		base.Awake ();
		target = null;
	}


	new public void OnTriggerEnter(Collider other)
	{
		base.OnTriggerEnter (other);
		if (other.tag == "Player" && !PlayersDetected.Contains(other.gameObject))
		{
			PlayersDetected.Add (other.gameObject);
		}
	}


	new public void Update()
	{
		base.Update();
		FindTarget ();
		if (target != null)
		{
			transform.LookAt (new Vector3 (target.transform.position.x, this.transform.position.y, target.transform.position.z));
			if ((target.transform.position - this.transform.position).magnitude > weapon.RangeOfAttk) 
			{
				Move (0, 0, 1, WalkSpeed);
			}
			else
			{
				Attack ((_Character)target.GetComponent<MainCharacter> ());
				Debug.Log ("attack");
			}
		}
	}


	new void FixedUpdate () 
	{
		base.FixedUpdate ();
	}





	public void FindTarget()
	{
		GameObject NewTarget = null;
		float Distancetarget = RangeOfDetection + 50;
		for (int i = 0; i < PlayersDetected.Count; ++i) 
		{
			float distance = (PlayersDetected [i].transform.position - this.transform.position).magnitude;
			if (distance > RangeOfDetection + 50)
			{
				PlayersDetected.Remove (PlayersDetected[i]);
			}
			else if (distance < Distancetarget)
			{
				Distancetarget = distance;
				NewTarget = PlayersDetected [i];
			}
		}
		target = NewTarget;
	}
}
