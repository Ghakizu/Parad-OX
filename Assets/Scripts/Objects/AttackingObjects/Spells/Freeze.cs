using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : _Spells 
{
	public void SpellAttack(_Enemies other)
	{
		if (other.CompareTag ("ennemy")) 
		{
			other.CharacterRigidbody.velocity = Vector3.zero;
			other.Is_Freezed = true;
			int elapsed = 0;
			while (elapsed < 2)
				elapsed += Time.deltaTime;
			other.Is_Freezed = false;
		}
	}

}
