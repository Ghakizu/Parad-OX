using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : _Spells 
{
	//A freeze spell : freeze the enemy during TimeOfFreeze seconds


	public float TimeOfFreeze = 2;


	private PlayerInventory MyPlayer;

	new public void Awake()
	{
		MyPlayer = GetComponent<PlayerInventory> ();
		Object = MyPlayer.FreezeObject;
		ObjectName = "Freeze";
		sprite = MaterialsAssignations.FreezeSprite;
		damages = 0; 
		RangeOfAttk = 300;
		TimeBetweenAttacks = 3;
		ManaConsumed = 50;
		SubDescription = "Freeze your enemy during " + TimeOfFreeze + " seconds.";
		base.Awake ();
	}




	public void LaunchSpell(_Enemies other)
	{
		if (other.tag == "Enemy") 
		{
			other.IsFreezed = TimeOfFreeze;
			other.GetComponent<Renderer> ().material = MaterialsAssignations.FreezedMaterial;
			FreezeAll (other);
			owner.Mana -= ManaConsumed;
		}
	}

	public static void FreezeAll(_Character character)
	{
		character.CharacterRigidbody.velocity = Vector3.zero;
		character.CharacterRigidbody.constraints = RigidbodyConstraints.FreezeAll;
		character.CharacterRigidbody.isKinematic = true;
	}

	public static void UnfreezeAll(_Character character)
	{
		character.CharacterRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
		character.CharacterRigidbody.isKinematic = false;
	}
}
