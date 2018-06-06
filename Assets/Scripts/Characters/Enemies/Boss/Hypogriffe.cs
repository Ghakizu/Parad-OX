using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hypogriffe : _Enemies
{
	public GameObject door1;
	public GameObject door2;
	public GameObject door3;
	public float timeDoor = 0;
	public Vector3 Spawn1;
	public Vector3 Spawn2;
	public Vector3 Spawn3;
	public Vector3 LastSpawn;

	new public void Awake()
	//Set different stats
	{
		RotateSpeed = 250;
		RangeOfDetection = 2000;
		WalkSpeed = 50;
		RunSpeed = 100;
		MaxHealth = 200;
		MaxMana = 100;
		ActualWeapon = GetComponent<CentaurusWeapon> ();
		base.Awake ();
		Spawn1 = new Vector3 (-350, 120, 350);
		Spawn2 = new Vector3 (20, 120, -400);
		Spawn3 = new Vector3 (350, 120, 350);
		LastSpawn = Vector3.zero;
	}

	new public void OnTriggerStay(Collider other)
	{
		base.OnTriggerStay (other);
	}


	new public void Update()
	{
		base.Update ();
		if (timeDoor > 0)
		{
			timeDoor = Mathf.Max (timeDoor - Time.deltaTime, 0);
			if (timeDoor == 0)
			{
				if (LastSpawn == Spawn1)
					door1.transform.Translate (Vector3.forward * 300);
				if (LastSpawn == Spawn2)
					door2.transform.Translate (Vector3.forward * 300);
				if (LastSpawn == Spawn3)
						door3.transform.Translate (Vector3.forward * 300);
			}
		}
		if ((Spawn1 - this.transform.position).magnitude <= 150 && Spawn1 != LastSpawn)
		{
			IsFreezed = 8;
			timeDoor = 8;
			door1.transform.Translate (Vector3.back * 300);
			LastSpawn = Spawn1;
		}
		else if ((Spawn2 - this.transform.position).magnitude <= 150 &&  Spawn2 != LastSpawn)
		{
			IsFreezed = 8;
			timeDoor = 8;
			door2.transform.Translate (Vector3.back * 300);
			LastSpawn = Spawn2;
		}
		else if ((Spawn3 - this.transform.position).magnitude <= 150  && Spawn3 != LastSpawn)
		{
			IsFreezed = 8;
			timeDoor = 8;
			door3.transform.Translate (Vector3.back * 300);
			LastSpawn = Spawn3;
		}
		if (IsFreezed >= 0)
		{
			transform.Rotate (new Vector3 (0, 3, 0));
			IsAirWallEnabled = 0;
		}
		else
		{
			IsAirWallEnabled = 1;
		}
	}

}
