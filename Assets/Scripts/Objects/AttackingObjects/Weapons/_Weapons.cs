using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class _Weapons : _AttackingObjects 
{

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
