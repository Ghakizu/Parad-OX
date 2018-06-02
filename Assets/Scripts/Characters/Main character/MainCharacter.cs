using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainCharacter : _Character 
{
	//The character that the player is using;

	//STATS
	//Moving the character
	private bool cheatCode = false;  //Are we cheating
	private bool crouch = false;  //is the player crouched
	private bool IsTired = false;  //has the player used all his stamina ?
	private float CheatSpeed = 500;  //the speed when we're cheating
	private float CrouchSpeed = 50; //speed when crouching
	private float OutOfStaminaSpeed = 40;  //speed when stamina is 0
	public float MaxStamina = 150;  //How long can you dash
	public float Stamina = 150;  //Actual stamina
	private float speed;  //actual speed of the player

	//Playing
	public GameObject cam;  //the main camera of the player
	public bool IsDisplaying = false;

	//Displaying
	public Image StaminaButton;  //Display our stamina status
	public Image HealthButton;  //Display our health statis
	public Image ManaButton;  //Display our Mana status
	public GameObject Interface;  //the interface that is running when playing
	public GameObject PauseMenu;  //the interface of the pause menu
	public GameObject GameOver;  //the interface of the game over : Do a new scenne ? WARNING!
	public GameObject PlayerDisplays;  //All the displays of the player

	//Animator
	public Animator anim;  //The animator of our player

    //Network
    public PhotonView View;  //the photonView of the player : to be active on the network

	//Sound
    public AudioClip sound;  //The walk sound for the player





	new private void Awake ()
	//Set all the stats of the mainCharacter
	{
		MaxHealth = 150;
		MaxMana = 200;
		RotateSpeed = 200;
		WalkSpeed = 100;
		RunSpeed = 250;
		Heightjump = 300;
		base.Awake ();
        cam = GetComponentInChildren<Camera>().gameObject;
		speed = WalkSpeed;
		CharacterObject.tag = "Player";
		View = GetComponent<PhotonView>();
    }


    private void Start()
	//Disables the stuffs that we don't want to see, because it's not our player
    {
		DisableComponentsForMulti ();
    }


	new private void Update () 
	//Update all the stats of our player
	{
        if(View.isMine)
        {
			if (!GameOver.activeSelf)
			{
				base.Update ();
			}
			RunGame ();
			Move ();
			Fight ();
        }
	}


	new private void FixedUpdate ()
	//Apply forces if we're not in pause
	{
		if (!IsGamePaused && IsFreezed <= 0 && View.isMine && !cheatCode)
		{
			base.FixedUpdate ();
			Jump ();
		}
	}


	private void OnTriggerEnter(Collider other)
	//Set the respawns, and all the trigger events
	{
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
		case "SecondRoomLabyrinth":
			transform.position = new Vector3 (-6500, 5064, 8500);
			break;
		}
	}










	//Disables the components that we don't want to see in the multiplayer mode

	private void DisableComponentsForMulti()
	//Disables all the components
	{
		if (!View.isMine)
		{
			cam.SetActive(false);
			PlayerDisplays.SetActive(false);
			Camera WeaponsCam = this.GetComponentInChildren<Camera>();
			WeaponsCam.gameObject.SetActive(false);
			ChangeLayers (this.transform, 0);
		}
	}


	private static void ChangeLayers(Transform transform, int layer)
	//Change the layer of all the weapons (we don't want to see them on the second cam)
	{
		transform.gameObject.layer = layer;
		foreach (Transform child in transform)
		{
			if (child.gameObject.name != "Displays")
			{
				ChangeLayers (child, layer);
			}
		}
	}










	//Update status of the game

	private void RunGame()
	//Update the stats of our player, and see if the Pause menu is called
	{
		if (!IsGamePaused && !GameOver.activeSelf)
		{
			SetMainStats();
			SetButtonsValue();
		}
		SetMouseStatus ();
	}
		

	private void SetButtonsValue()
	//Set the values of the slider
	{
		StaminaButton.transform.localScale = new Vector3(Stamina / MaxStamina, 1, 1);
		HealthButton.transform.localScale = new Vector3(Health / MaxHealth, 1, 1);
		ManaButton.transform.localScale = new Vector3(Mana / MaxMana, 1, 1);
	}


	private void SetMainStats()
	//Reset the values of our player
	{
		Stamina = Mathf.Min (Stamina + 10 * Time.deltaTime, MaxStamina);
		if (IsTired && Stamina > 40)
		{
			IsTired = false;
		}
		if (speed == RunSpeed && (Input.GetButton("Horizontal") || Input.GetButton ("Vertical")))
		{
			Stamina -= 20 * Time.deltaTime;
			if (Stamina < 0)
			{
				IsTired = true;
			}
		}
	}


	private void SetMouseStatus()
	//Set the status of the mouse every frame
	{
		if (!IsGamePaused && !GameOver.activeSelf && !IsDisplaying)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}









	//MOVING THE CHARACTER

	private void Move()
	//Move the character
	{
		if (!IsDisplaying && IsFreezed <= 0 && !IsGamePaused && !GameOver.activeSelf)
		{
			Cheatcodes ();
			Crouch ();
			SetSpeed ();
			CheatMoves ();
			CameraRotations ();
			Jump ();
			base.Move(Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"), speed);
            SetAnimation();
        }
	}


	private void Cheatcodes ()
	//enables or disables the cheatcodes
	{
		//allows or not your player to fly
		if (Input.GetKeyDown (KeyCode.F))  
		{
			CharacterRigidbody.velocity = Vector3.zero;
			cheatCode = !cheatCode;
			CharacterRigidbody.useGravity = !cheatCode;
		}

		//Respawn to the last checkpoint automatically
		if (Input.GetKeyDown (KeyCode.R))  
		{
			CharacterRigidbody.velocity = Vector3.zero;
			transform.position = SpawnPoint + new Vector3(0, 2, 0);
		}
	}



	private void Crouch()
	//Allows our player to crouch : must be changed, we don't want the camera just to go down. WARNING!
	{
		if(Input.GetButtonDown ("Crouch") && !cheatCode)
		{
			if (crouch)
			{
				cam.transform.position = transform.position + new Vector3(0, 10, 0);
			}
			else
			{
				cam.transform.position = transform.position + new Vector3(0, 0, 0);
                anim.SetFloat("crouch", .3f);
			}
			crouch = !crouch;
            anim.SetBool("Crouching", crouch);
        }
	}



	private void SetSpeed()
	//Set the speed of the player
	{
		if(cheatCode)
		{
			speed = CheatSpeed;
		}
		else if (IsTired)
		{
			speed = OutOfStaminaSpeed;
		}
		else if (crouch)
		{
			speed = CrouchSpeed;
		}
		else if (Input.GetButton ("Dash") && Stamina > 0)
		{
			speed = RunSpeed;
		}
		else
		{
			speed = WalkSpeed;
		}
	}



	private void CheatMoves()
	//Movements along the Y axe if cheating
	{
		if (cheatCode)
		{
			if (Input.GetKey (KeyCode.H))
			{
				transform.Translate (new Vector3 (0, 1, 0) * speed * Time.deltaTime);
			}
			if (Input.GetKey (KeyCode.B))
			{
				transform.Translate (new Vector3 (0, -1, 0) * speed * Time.deltaTime);
			}
		}
	}



	private void CameraRotations()
	//Rotate the camera
	{
		//Rotation around Y axe
		transform.Rotate (Vector3.up * Input.GetAxis ("Mouse X") * RotateSpeed * Time.deltaTime);

		//Rotation up and down
		float Y = -Input.GetAxis ("Mouse Y");
		float ActualRotation = cam.transform.localRotation.x;
		if ((ActualRotation <= 0.6 && Y > 0) || (ActualRotation >= -0.6 && Y < 0)) 
		{
			cam.transform.Rotate (new Vector3 (Y, 0, 0) * RotateSpeed * Time.deltaTime);
		}
	}


	new private void Jump()  
	//Jump if the player is on the ground and if the button "Jump" is pressed
	{
		if (Input.GetButton ("Jump") && !cheatCode && IsAbleToJump && !IsTired)
		{
			base.Jump ();
            anim.SetBool("Jumping", true);
            anim.SetFloat("jump", 0.3f);
		}
	}



	private void SetAnimation()
	//Animations for the player
	{
        /*if(Input.GetButton("Horizontal") || Input.GetButton ("Vertical"))
		{
			SoundController.PlaySound (sound);
			if (speed == RunSpeed) 
			{
				anim.SetBool ("Walk", false);
				anim.SetBool ("Run", true);
			}
			else
			{
				anim.SetBool ("Walk", true);
				anim.SetBool ("Run", false);
			}
		}
		else
		{
			anim.SetBool ("Walk", false);
			anim.SetBool ("Run", false);
		}
		if (IsAbleToAttack <= 0)
		{
			anim.SetBool ("Attacking", false);
		}*/
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            if (speed == RunSpeed)
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Running", true);
            }
            else if (speed == WalkSpeed || speed == CrouchSpeed)
            {
                anim.SetBool("Walking", true);
                anim.SetBool("Running", false);
            }
        }
        else
        {
            anim.SetBool("Running", false);
            anim.SetBool("Walking", false);
        }
        anim.SetBool("InTheAir", !IsAbleToJump);
        if (ActualWeapon.ObjectName == "Fists" || ActualWeapon.ObjectName == "Knife")
        {
            anim.SetBool("Knife", true);
            anim.SetBool("Sword", false);
            anim.SetBool("Bat", false);
        }
        if (ActualWeapon.ObjectName == "Sword" || ActualWeapon.ObjectName == "Excalibur")
        {
            anim.SetBool("Knife", false);
            anim.SetBool("Sword", true);
            anim.SetBool("Bat", false);
        }
        if (ActualWeapon.ObjectName == "Bat")
        {
            anim.SetBool("Knife", false);
            anim.SetBool("Sword", false);
            anim.SetBool("Bat", true);
        }
        if (IsAbleToAttack <= 0)
        {
            anim.SetBool("Attacking", false);
        }
        if (anim.GetFloat("jump") > 0)
        {
            anim.SetFloat("jump", anim.GetFloat("jump") - Time.deltaTime);
        }
        else
        {
            anim.SetBool("Jumping", false);
        }
        if (anim.GetFloat("crouch") > 0)
        {
            anim.SetFloat("crouch", anim.GetFloat("crouch") - Time.deltaTime);
        }
    }














	//FIGHTING

	private void Fight()
	//Attack, launch spells and die
	{
		if(IsFreezed <= 0 && !GameOver.activeSelf && !IsGamePaused && !IsDisplaying)
		{
			Attack ();
			LaunchSpell ();
		}
		Die ();
	}


	private void Attack()
	//Attack with our weapon
	{
		if (Input.GetButtonDown ("Attack") && IsAbleToAttack <= 0 && IsFreezed <= 0) 
		{
			IsAbleToAttack = ActualWeapon.TimeBetweenAttacks;
			anim.SetBool ("Attacking", true);
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit)
				&& hit.collider.gameObject.tag == "Enemy" 
				&& (hit.transform.position - this.transform.position).magnitude < ActualWeapon.RangeOfAttk )
			{
				_Character enemy = hit.collider.gameObject.GetComponent<_Character> ();
				enemy.Health -= ActualWeapon.damages;
			}
		}
	}


	private void LaunchSpell()
	//Launch the spell with our actual spell
	{
		if (Input.GetButtonDown("LaunchSpell") && IsAbleToLaunchSpell <= 0 && IsFreezed <= 0 && Mana - ActualSpell.ManaConsumed > 0)
		{
			IsAbleToLaunchSpell = ActualSpell.TimeBetweenAttacks;
			Mana -= ActualSpell.ManaConsumed;
			if (ActualSpell.ObjectName == "Heal" || ActualSpell.ObjectName == "AirWall")
			{
				base.LaunchSpell (null);
			}
			else
			{
				RaycastHit hit;
				if (Physics.Raycast(transform.position, transform.forward, out hit)
					&& hit.collider.gameObject.tag == "Enemy" 
					&& (hit.transform.position - this.transform.position).magnitude < ActualSpell.RangeOfAttk)
				{
					_Character enemy = hit.collider.gameObject.GetComponent<_Character> ();
					base.LaunchSpell (enemy);
				}
			}
		}
	}
		


	new public void Die()
	//Die and display the GameOver menu
	{
		if(Health <= 0)
		{
			Interface.SetActive (false);
			GameOver.SetActive (true);
		}
	}
}


//1 WARNING
//crouching reduce the area of detecting + we don't want to just put the camera down