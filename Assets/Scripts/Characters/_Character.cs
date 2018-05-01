using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]  //All characters must have a Rigidbody

public abstract class _Character : Photon.MonoBehaviour 
{
	// All living characters, they're able to move, jump....


	//STATS
	//Moving the character
	public float RotateSpeed;  //for camera moves
	public float WalkSpeed;  //normal speed
	public float RunSpeed;  //speed when dashing
	public float Heightjump;  //when jumping
	protected bool IsAbleToJump = true;  //We're able to jump only if we touch the ground, ie if this variable is <= 0
	public float Gravity = 500;  //for smooths jumps
	public Vector3 SpawnPoint;  //location where the player respawn when dying
	public static LayerMask JumpLayer;  //which Layer allows the player to jump ?

	//Attacking
	public float MaxMana;  //how many spell you can launch
	public float Mana;  //actual mana
	public float MaxHealth;  //how long can you survive
	public float Health;  //actual health
	public float IsAbleToAttack = 0;  //we cant't attack if this variable is > 0 -> depends on the weapon that we use
	public float IsAbleToLaunchSpell = 0;  //We can't launch a spell is this variable is > 0 -> depends on the spell that we use
	public _Weapons ActualWeapon;  //The actual weapon of the character : set in the inventory
	public _Spells ActualSpell;  //The actual spell of the character : set in the inventory

	//playing
	public GameObject CharacterObject;  //the gameObject of the character
	public Rigidbody CharacterRigidbody;  //the rigidbody of the character

	//Changements of status, due to spells
	public float IsFreezed = 0;  //we're freezed if the spell "freeze" has been launched
	public float IsAirWallEnabled = 0;  //if airwall is enabled, this value is > 0 and no projectiles can attack us




	protected void Awake () 
	//Set the basic stats of the character
	{
		Mana = MaxMana;
		Health = MaxHealth;
		SpawnPoint = transform.position;  //A default value. We must update it when we change the scene
		JumpLayer = 10;  //The default value. Must be changed to another layer only used for the jumps. WARNING!
		CharacterRigidbody = GetComponent<Rigidbody> ();
		CharacterObject = this.gameObject;
	}


	protected void FixedUpdate () 
	//apply the gravity every time if we're not freezed
	{
		if (IsFreezed <= 0)
		{
			ApplyGravity ();	
		}
	}


	public void Update()
	//Set the stats every frame
	{
		SetStats ();
	}


	public void OnTriggerEnter(Collider other)
	//Set the value of IsAbleToJump if we touch the ground
	{
		if (other.gameObject.layer == JumpLayer)
		{
			IsAbleToJump = true;
		}
	}
		



	public void SetStats()
	//reset all the stats that are affected and must change during time
	{
		if (IsAbleToAttack >= 0)
		{
			IsAbleToAttack -= Time.deltaTime;
		}
		if (IsAbleToLaunchSpell >= 0)
		{
			IsAbleToLaunchSpell -= Time.deltaTime;
		}
		if (IsFreezed >= 0)
		{
			IsFreezed -= Time.deltaTime;
			if (IsFreezed < 0)
			{
				Freeze.UnfreezeAll (this);
			}
		}
		if (IsAirWallEnabled >= 0)
		{
			IsAirWallEnabled -= Time.deltaTime;
		}
		if (Mana < MaxMana)
		{
			Mana += Time.deltaTime;
			if (Mana > MaxMana)
			{
				Mana = MaxMana;
			}
		}
	}


	public void Move (float x, float y, float z, float speed)
	//Allows a character to move along the x and z axes
	{	
		transform.Translate (Vector3.forward * z * speed * Time.deltaTime);
		transform.Translate (new Vector3 (1, 0, 0) * x * speed * Time.deltaTime);
	}
		

	public void Jump ()
	//Allows a character to jump
	{
		CharacterRigidbody.velocity = new Vector3(0, Heightjump, 0);
		IsAbleToJump = false;
	}
		

	public void ApplyGravity()
	//Apply a gravity force in order to have smooths jumps
	{
		CharacterRigidbody.AddForce (Vector3.down * Gravity);
	}


	public void Attack(_Character other)
	//Allows a character to attack another
	{
		if (IsAbleToAttack <= 0)
		{
			ActualWeapon.Attack (other);
		}
	}


	public void LaunchSpell(_Character other)
	//Allows a character to launch the Spell of his actual spell
	{
		if(IsAbleToLaunchSpell <= 0)
		{
			switch (ActualSpell.ObjectName)
			{
			case "Freeze":
				((Freeze)ActualSpell).LaunchSpell (other);
				break;
			case "Flash":
				((Flash)ActualSpell).LaunchSpell (other);
				break;
			case "FireBall":
				((FireBall)ActualSpell).LaunchSpell (other);
				break;
			case "EarthSpike":
				((FireBall)ActualSpell).LaunchSpell (other);
				break;
			case "AirWall":
				((AirWall)ActualSpell).LaunchSpell ();
				break;
			case "Heal":
				((Heal)ActualSpell).LaunchSpell ();
				break;
			default:
				return;
			}
			Mana -= ActualSpell.ManaConsumed;
			IsAbleToLaunchSpell = ActualSpell.TimeBetweenAttacks;
		}
	}


	public void Die()
	//Allows a character to Die. If the character is a MainCharacter, we don't want to use this function
	{
		Destroy(CharacterObject);
	}
}

//1WARNING