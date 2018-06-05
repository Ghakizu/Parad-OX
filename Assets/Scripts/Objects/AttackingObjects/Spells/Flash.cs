using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : _Spells 
{
	//A Spell that confuse the target

	public SphereCollider ObjectCollider;  //the area where the collider is effective
	public List<_Enemies> targets;  //All the targets that are in the range of the spell

	new public void Awake()
	//set all the stats of the spell
	{
		TimeOfEffect = 3;
		ObjectName = MaterialsAssignations.FlashName;
		damages = 0; 
		RangeOfAttk = 250;
		TimeBetweenAttacks = 4;
		ManaConsumed = 60;
		SubDescription = "Create a flash that confuse the target";
		base.Awake ();
		sprite = Materials.FlashSprite;
		ObjectCollider = GetComponent<SphereCollider> ();
		ObjectCollider.isTrigger = true;
		ObjectCollider.radius = RangeOfAttk * 100;
		targets = new List<_Enemies> ();
	}



	public void OnTriggerStay(Collider other)
	//Adds the targets if they are in the range of the spell
	{
		_Enemies target = other.gameObject.GetComponent<_Enemies> ();
		if (target != null && !targets.Contains(target))
		{
			targets.Add (target);
		}
	}


	public void OnTriggerExit(Collider other)
	//Removes the targets if they're not in the range of the spell anymore
	{
		_Enemies target = other.gameObject.GetComponent<_Enemies> ();
		if (target != null && targets.Contains(target))
		{
			targets.Remove (target);
		}
	}


	public void LaunchSpell(_Character other)
	//Launch the spell Flash againt the target
	{
		for (int i = 0; i < targets.Count; ++i)
		{
			targets [i].IsFLashed = TimeOfEffect;
		}
	}
}
