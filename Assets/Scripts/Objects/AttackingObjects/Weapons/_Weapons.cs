using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class _Weapons : _AttackingObjects 
{
	//All the weapons (physical objects that can hurt another character)


	public void Attack(_Character other)
	{
		MainCharacter PossiblePlayer = other.GetComponent<MainCharacter> ();
		if(owner.IsAbleToAttack <= 0)
		{	
			other.Health -= owner.ActualWeapon.damages;
			owner.IsAbleToAttack = TimeBetweenAttacks;
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
