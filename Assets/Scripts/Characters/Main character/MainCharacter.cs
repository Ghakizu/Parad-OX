using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainCharacter : _Character 
{
	//The character that the player is using;

	private bool cheatCode = false;  //cheatcode
	private bool IsAbleToRun = true;  //if you're tired youn can't run anymore
	private bool crouch = false;  //is the player crouched ?
	private float speed;  //actual speed of the player
	//private PlayerInventory Inventory;  //the inventory of our player. Maybe it's useless

	public static float CheatSpeed = 500;  //the speed when we're cheating
	public static float CrouchSpeed = 50; //speed when crouching
	public float OutOfStaminaSpeed = 50;  //speed when stamina is 0
	public float MaxStamina = 50;  //How long can you dash
	public float Stamina = 100;  //Actual stamina
	public GameObject cam;  //the main camera of the player
	public bool IsGamePaused = false;  //Is the game running or are we in a menu ?

	public Slider StaminaButton;  //for the moment it's just the diplay of our stamina status
	public GameObject NormalDisplays;
	public GameObject Target = null;

	public List<_Enemies> EnemiesList = new List<_Enemies>();



	new public void Awake () 
	{
		base.Awake ();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		speed = WalkSpeed;
		SpawnPoint = transform.position;
	}


	new public void Update () 
	{
		base.Update ();
		if (!IsGamePaused && IsFreezed <= 0)
		{
			cheatcodes ();
			Crouch ();
			CameraRotations ();
			move ();
			Attack ();
			LaunchSpell ();
		}
		else
		{
			Stamina += 10 * Time.deltaTime;
			Stamina = Mathf.Min (Stamina, MaxStamina);
		}
		StaminaButton.value = Stamina / MaxStamina;
			
	}
		

	new public void FixedUpdate ()
	{ 
		if (!IsGamePaused && !cheatCode && IsFreezed <=0)  //we only want to move up and down if our character is not cheating
		{
			Jump ();
		}
		if (!cheatCode && IsFreezed <= 0)
		{
			base.FixedUpdate();
		}
	}


	new public void OnTriggerEnter(Collider other)
	{
		base.OnTriggerEnter (other);
		switch (other.gameObject.tag) 
		{
		case "deathplane":
			transform.position = SpawnPoint;
			break;
		case "respawn": //rename this "checkpoint" maybe ? WARNING!
			SpawnPoint = other.transform.position;
			break;
		case "partend": //end of the first part of the first level
			transform.position = new Vector3 (-1624, 4620, -3920);
			break;
		}
	}







	 public void Attack()
	{
		if (Input.GetButtonDown("Shoot"))
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit))
			{
				if (hit.rigidbody.gameObject.tag == "Enemy" 
					&& (hit.transform.position - this.transform.position).magnitude < ActualWeapon.RangeOfAttk 
					&& IsAbleToAttack < 0)
				{
					_Enemies enemy = hit.collider.gameObject.GetComponent<_Enemies> ();
					ActualWeapon.Attack (enemy);
				}
			}
		}
	}


	public void LaunchSpell()
	{
		if (Input.GetButtonDown("Spell"))
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit))
			{
				if (hit.rigidbody.gameObject.tag == "Enemy" 
					&& (hit.transform.position - this.transform.position).magnitude < ActualWeapon.RangeOfAttk 
					&& IsAbleToAttack < 0)
				{
					Debug.Log ("spell");
					_Enemies enemy = hit.collider.gameObject.GetComponent<_Enemies> ();
					Debug.Log (enemy.IsFreezed);
					switch (ActualSpell.ObjectName)
					{
					case "Freeze":
						((Freeze)ActualSpell).LaunchSpell (enemy);
						break;
					}
				}
			}
		}
	}


	private void cheatcodes ()
	{
		//enables or disables the cheatcodes
		if (Input.GetKeyDown (KeyCode.F))  //allows or not your player to fly
		{
			CharacterRigidbody.velocity = Vector3.zero;
			cheatCode = !cheatCode;
			speed = cheatCode ? CheatSpeed : WalkSpeed;  //set the speed depending on if you're cheating
			CharacterRigidbody.useGravity = !cheatCode;
		}

		if (Input.GetKeyDown (KeyCode.R))  //Respawn to the last checkpoint automatically
		{
			CharacterRigidbody.velocity = Vector3.zero;
			transform.position = SpawnPoint + new Vector3(0, 2, 0);
		}
	}



	public void Crouch()
	{
		if(Input.GetButtonDown ("Crouch") && !cheatCode)
		{
			if (crouch)
			{
				cam.transform.position = transform.position + new Vector3(0, 10, 0);
				speed = WalkSpeed;
			}
			else
			{
				cam.transform.position = transform.position + new Vector3(0, 0, 0);
				speed = CrouchSpeed;
			}
			crouch = !crouch;
		}
	}



	private void move()
	{
		//movements along the Y axe if cheating
		if (cheatCode)
		{
			if (Input.GetKey (KeyCode.H))
				transform.Translate (new Vector3 (0, 1, 0) * speed * Time.deltaTime);
			if (Input.GetKey (KeyCode.B))
				transform.Translate (new Vector3 (0, -1, 0) * speed * Time.deltaTime);
		}
		//Movements along X and Z axes
		if (Input.GetButton ("Dash") && Stamina > 0 && IsAbleToRun && !cheatCode && !crouch //We can't dash if we're cheating
			&& (Input.GetButton("Horizontal") || Input.GetButton ("Vertical"))) 
		{
			base.Move (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"), RunSpeed); 
			Stamina -= 10 * Time.deltaTime;
			if (Stamina < 0)
			{
				IsAbleToRun = false;
				speed = OutOfStaminaSpeed;
			}
		}
		else
		{
			base.Move(Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"), speed); 
			Stamina += 10 * Time.deltaTime;
			Stamina = Mathf.Min (Stamina, MaxStamina);
			if (!IsAbleToRun && Stamina > 40)
			{
				IsAbleToRun = true;
				speed = WalkSpeed;
			}
		}
		//Rotation of the character
		transform.Rotate (Vector3.up * Input.GetAxis ("Mouse X") * RotateSpeed * Time.deltaTime); 
	}


	new private void Jump()  
	{
		//Jump if the player is on the ground and if the button "Jump" is pressed
		if (Input.GetButton ("Jump") && !cheatCode && IsAbleToJump)
		{
			base.Jump ();
			IsAbleToJump = false;
		}
	}


	private void CameraRotations()
	{

		//Rotate the camera up and down
		float Y = -Input.GetAxis ("Mouse Y");
		float ActualRotation = cam.transform.localRotation.x;
		if ((ActualRotation <= 0.6 && Y > 0) || (ActualRotation >= -0.6 && Y < 0)) 
		{
			cam.transform.Rotate (new Vector3 (Y, 0, 0) * RotateSpeed * Time.deltaTime);
		}
	}


	new public void Die()
	{
		//TODO : display a GameOver screen
	}
}


//1 WARNING
//crouching reduce the area of detecting
