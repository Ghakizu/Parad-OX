using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : _Spells 
{
	//A freeze spell : freeze the enemy during TimeOfFreeze seconds



	new public void Awake()
	//set all the stats of the spell
	{
		TimeOfEffect = 3;
		ObjectName = "Freeze";
		damages = 0; 
		RangeOfAttk = 300;
		TimeBetweenAttacks = 3;
		ManaConsumed = 50;
		SubDescription = "Freeze your enemy during " + TimeOfEffect + " seconds.";
		base.Awake ();
		sprite = Materials.FreezeSprite;
	}




	public void LaunchSpell(_Character other)
	//Launch the spell freeze against another character
	{
		if (other.tag == "Enemy") 
		{
			other.IsFreezed = TimeOfEffect;
			FreezeAll (other);
		}
	}



	public static void FreezeAll(_Character character)
	//A static function that freeze a character : he becomes unable to move
	{
		character.CharacterRigidbody.velocity = Vector3.zero;
		character.CharacterRigidbody.constraints = RigidbodyConstraints.FreezeAll;
		character.CharacterRigidbody.isKinematic = true;
	}


	public static void UnfreezeAll(_Character character)
	//A static function that unfreeze a character : he becomes able to move again
	{
		character.CharacterRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
		character.CharacterRigidbody.isKinematic = false;
	}
}
