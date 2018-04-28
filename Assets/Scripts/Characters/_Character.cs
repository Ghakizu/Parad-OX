using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]  //All characters must have a Rigidbody

public abstract class _Character : MonoBehaviour 
{
	// All living characters, they're able to move, jump....

	//STATS

	//Moving the character
	public float RotateSpeed = 200;  //for camera moves
	public float WalkSpeed = 100;  //normal speed
	public float RunSpeed = 250;  //speed when dashing
	public float Heightjump = 300;  //when jumping
	public float Gravity = 500;  //for smooths jumps
	public Vector3 SpawnPoint;  //location where the player respawn when dying
	public static LayerMask JumpLayer;  //which Layer allows the player to jump ? -> To put into the inspector

	//Attacking
	public float MaxMana = 100;  //how many spell you can launch
	public float Mana = 100;  //actual mana
	public float MaxHealth = 150;  //how long can you survive
	public float Health = 150;  //actual health

	//playing
	public GameObject CharacterObject;  //the gameObject of the character
	public Rigidbody CharacterRigidbody;  //the rigidbody of the character
	public  BoxCollider Collider;  //Can be another collider than a Box !!!! We have to adapt
	protected bool IsAbleToJump = true;  //We're able to jump only if we touch the ground
	public float IsFreezed = 0;  //we're freezed if the spell "freeze" has been launched
	public float IsAbleToAttack = 0;  //we cant't attack to time in less than a second for example, depending on the delay of the weapon
	public _Weapons ActualWeapon;  //The actual weapon of the character : set in the inventory
	public _Spells ActualSpell;






	protected void Awake () 
	{
		SpawnPoint = transform.position;  //A default value
		JumpLayer = 0;  //The default value. Must be changed to another layer only used for the jumps. WARNING!
		CharacterRigidbody = GetComponent<Rigidbody> ();
		Collider = GetComponent<BoxCollider> ();
		CharacterObject = this.gameObject;
	}

	protected void FixedUpdate () 
	{
		if (IsFreezed <= 0)
		{
			ApplyGravity ();	
		}
	}

	public void Update()
	{
		if (IsAbleToAttack >= 0)
		{
			IsAbleToAttack -= Time.deltaTime;
		}
		if (IsFreezed >= 0)
		{
			IsFreezed -= Time.deltaTime;
			if (IsFreezed < 0)
			{
				this.GetComponent<Renderer> ().material = MaterialsAssignations.NormalMaterial;
				Freeze.UnfreezeAll (this);
			}
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		IsAbleToJump = other.gameObject.layer == JumpLayer;
	}
		






	//Allows a character to move along the x and z axes
	protected void Move (float x, float y, float z, float speed)
	{	
		transform.Translate (Vector3.forward * z * speed * Time.deltaTime);
		transform.Translate (new Vector3 (1, 0, 0) * x * speed * Time.deltaTime);
	}
		
	//Allows a character to jump
	protected void Jump ()
	{
		CharacterRigidbody.velocity = new Vector3(0, Heightjump, 0);
	}
		
	//Apply a gravity force in order to have smooths jumps
	protected void ApplyGravity()
	{
		CharacterRigidbody.AddForce (Vector3.down * Gravity);
	}

	public void Attack(_Character other)
	{
		ActualWeapon.Attack (other);
	}

	public void Die()
	{
		Destroy(CharacterObject);
	}
}

//1WARNING