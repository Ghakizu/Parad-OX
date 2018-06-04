using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
	public bool IsGamePaused = false;  //is the game running or are we in a menu ?
	public Vector3 StockedVelocity = Vector3.zero;  //when we pause the game, we want the velocity of the player to be stocked

	//Changements of status, due to spells
	public float IsFreezed = 0;  //we're freezed if the spell "freeze" has been launched
	public float IsAirWallEnabled = 0;  //if airwall is enabled, this value is > 0 and no projectiles can attack us




	protected void Awake () 
	//Set the basic stats of the character
	{
		Mana = MaxMana;
		Health = MaxHealth;
		SpawnPoint = transform.position;  //A default value. We must update it when we change the scene
		JumpLayer = 10;  //The default value. Must be changed to another layer only used for the jumps.
		CharacterRigidbody = GetComponent<Rigidbody> ();
		CharacterObject = this.gameObject;
        Gravity = 800;
	}


	protected void FixedUpdate () 
	//apply the gravity every time if we're not freezed
	{
		if (IsFreezed <= 0 && !IsGamePaused)
		{
			ApplyGravity ();	
		}
	}


	public void Update()
	//Set the stats every frame
	{
		if (!IsGamePaused)
		{
			SetStats ();
			Die ();
			PauseGame ();
		}
	}


	public void OnTriggerStay(Collider other)
	//Set the value of IsAbleToJump if we touch the ground
	{
		if (other.gameObject.layer == JumpLayer)
		{
			IsAbleToJump = true;
		}
	}


	public void OnTriggerExit(Collider other)
	//If we leave the ground, we don't want to be able to jump anymore
	{
		if (other.gameObject.layer == JumpLayer)
		{
			IsAbleToJump = false;
		}
	}
		







	//PLAYING

	public void SetStats()
	//reset all the stats that are affected and must change during time
	{
		IsAbleToAttack = Mathf.Max (0, IsAbleToAttack - Time.deltaTime);
		IsAbleToLaunchSpell = Mathf.Max (0, IsAbleToLaunchSpell - Time.deltaTime);
        if (IsAbleToLaunchSpell == 0)
        {
            ActualSpell.particles.SetActive(false);
        }
		IsAirWallEnabled = Mathf.Max (0, IsAirWallEnabled - Time.deltaTime);
		Mana = Mathf.Min (Mana + Time.deltaTime, MaxMana);
		if (IsFreezed >= 0)
		{
			IsFreezed -= Time.deltaTime;
			if (IsFreezed < 0)
			{
				Freeze.UnfreezeAll (this);
			}
		}
	}

	public void PauseGame()
	//Pause the game
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			StockedVelocity = CharacterRigidbody.velocity;
			IsGamePaused = true;
			CharacterRigidbody.constraints = RigidbodyConstraints.FreezeAll;
			if (GetComponent<MainCharacter>() != null)
			{
				((MainCharacter)this).IsDisplaying = false;
			}
			NavMeshAgent agent = GetComponent<NavMeshAgent> ();
			if (agent != null)
			{
				agent.SetDestination(this.transform.position);
				agent.velocity = Vector3.zero;
			}
		}
	}


	public void ResumeGame()
	//Resume the game
	{
		CharacterRigidbody.velocity = StockedVelocity;
		IsGamePaused = false;
		CharacterRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
	}












	//MOVING

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










	//FIGHTING

	public void Attack(_Character other)
	//Allows a character to attack another
	{
		if (IsAbleToAttack <= 0 && other.IsAirWallEnabled <= 0)
		{
			ActualWeapon.Attack (other);
		}
	}


	public void LaunchSpell(_Character other)
	//Allows a character to launch the Spell of his actual spell
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
			((FireBall)ActualSpell).LaunchSpell ();
            break;
		case "EarthSpike":
			((EarthSpike)ActualSpell).LaunchSpell (other);
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
	}


	public void Die()
	//Allows a character to Die. If the character is a MainCharacter, we don't want to use this function
	{
		MainCharacter PossiblePlayer = GetComponent<MainCharacter> ();
		if (Health <= 0 && PossiblePlayer == null)
		{
			Destroy(CharacterObject);
		}

	}
}