using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class _Weapons : _AttackingObjects 
{

	void Awake()
	{
		owner = this.GetComponent<_Character> ();
		if (owner == null)
			owner = this.GetComponentInParent<_Character> ();
	}

	public void Attack(_Character other)
	{
		MainCharacter PossiblePlayer = other.GetComponent<MainCharacter> ();
		if(owner.IsAbleToAttack <= 0)
		{	
			other.Health -= owner.weapon.damages;
			owner.IsAbleToAttack = owner.weapon.TimeBetweenAttacks;
			if (other.Health <= 0)
			{
				if (PossiblePlayer != null)
				{
					PossiblePlayer.Die ();
				}
				else
				{
					other.Die ();
				}
			}
		}
	}
}
