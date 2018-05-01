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
	private bool IsAbleToRun = true;  //if you're out of stamina you can't run anymore
	private bool crouch = false;  //is the player crouched
	public float CheatSpeed = 500;  //the speed when we're cheating
	public float CrouchSpeed = 50; //speed when crouching
	public float OutOfStaminaSpeed = 50;  //speed when stamina is 0
	public float MaxStamina = 150;  //How long can you dash
	public float Stamina = 150;  //Actual stamina
	public float speed;  //actual speed of the player

	//Playing
	public GameObject cam;  //the main camera of the player
	public bool IsGamePaused = false;  //Is the game running or are we in a menu ?

	//Displaying
	public Image StaminaButton;  //for the moment it's just the diplay of our stamina status
	public Image HealthButton;
	public Image ManaButton;
	public GameObject Interface; 
	public GameObject PauseMenu;
	public GameObject GameOver;


    //Network
    private PhotonView PhotonView;
    public PhotonView View
    {
        get { return PhotonView; }
    }

    //Serialized Fiels
    [SerializeField]
    private GameObject _display;
    private GameObject DisplayObj
    {
        get { return _display; }
    }

    [SerializeField]
    private GameObject _weapons;
    private GameObject Weapons
    {
        get { return _weapons; }
    }


	new public void Awake ()
	//SEt all the stats of the mainCharacter
	{
		MaxHealth = 150;
		MaxMana = 200;
		RotateSpeed = 200;
		WalkSpeed = 100;
		RunSpeed = 250;
		Heightjump = 300;
		base.Awake ();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
        cam = GetComponentInChildren<Camera>().gameObject;
		//cam = GameObject.Find ("MainCam");
		speed = WalkSpeed;
		CharacterObject.tag = "Player";
        PhotonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (!PhotonView.isMine)
        {
            cam.SetActive(false);
            DisplayObj.SetActive(false);
            Weapons.layer = 0;
            Camera WeaponsCam = Weapons.GetComponentInChildren<Camera>();
            WeaponsCam.enabled = false;
        }
    }

	new public void Update () 
	//Update all the stats of our player
	{
		base.Update ();
        if(PhotonView.isMine)
        {
			if(!GameOver.activeSelf && Input.GetKeyDown(KeyCode.Escape))
			{
				PauseMenu.SetActive (true);
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
            if (!IsGamePaused && IsFreezed <= 0)
            {
                cheatcodes();
                Crouch();
                CameraRotations();
                Move();
                Attack();
                LaunchSpell();
            }
            SetMainStats();
            SetButtonsValue();
        }
	}


	new public void FixedUpdate ()
	//Apply forces if we're not in pause
	{ 
		if (!IsGamePaused && !cheatCode && IsFreezed <=0 && PhotonView.isMine) 
		{
			Jump ();
		}
		if (!cheatCode && IsFreezed <= 0 && PhotonView.isMine)
		{
			base.FixedUpdate();
		}
	}


	new public void OnTriggerEnter(Collider other)
	//Set the respawns, and all the trigger events
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








	//MOVING THE CHARACTER

	private void Move()
	//Move our player
	{
		//movements along the Y axe if cheating
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

		//Movements along X and Z axes
		if (Input.GetButton ("Dash") && Stamina > 0 && IsAbleToRun && !cheatCode && !crouch
			&& (Input.GetButton("Horizontal") || Input.GetButton ("Vertical"))) 
		{
			base.Move (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"), RunSpeed); 
			Stamina -= 20 * Time.deltaTime;
			if (Stamina < 0)
			{
				IsAbleToRun = false;
				speed = OutOfStaminaSpeed;
			}
		}
		else
		{
			base.Move(Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"), speed); 
		}

		//Rotation of the character
		transform.Rotate (Vector3.up * Input.GetAxis ("Mouse X") * RotateSpeed * Time.deltaTime); 
	}



	private void cheatcodes ()
	//enables or disables the cheatcodes
	{
		//allows or not your player to fly
		if (Input.GetKeyDown (KeyCode.F))  
		{
			CharacterRigidbody.velocity = Vector3.zero;
			cheatCode = !cheatCode;
			speed = cheatCode ? CheatSpeed : WalkSpeed;  //set the speed depending on if you're cheating
			CharacterRigidbody.useGravity = !cheatCode;
		}

		//Respawn to the last checkpoint automatically
		if (Input.GetKeyDown (KeyCode.R))  
		{
			CharacterRigidbody.velocity = Vector3.zero;
			transform.position = SpawnPoint + new Vector3(0, 2, 0);
		}
	}



	public void Crouch()
	//Allows our player to crouch : must be changed, we don't want the camera just to go down. WARNING!
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



	new private void Jump()  
	//Jump if the player is on the ground and if the button "Jump" is pressed
	{
		if (Input.GetButton ("Jump") && !cheatCode && IsAbleToJump)
		{
			base.Jump ();
		}
	}



	private void CameraRotations()
	//Rotate the camera up and down
	{
		float Y = -Input.GetAxis ("Mouse Y");
		float ActualRotation = cam.transform.localRotation.x;
		if ((ActualRotation <= 0.6 && Y > 0) || (ActualRotation >= -0.6 && Y < 0)) 
		{
			cam.transform.Rotate (new Vector3 (Y, 0, 0) * RotateSpeed * Time.deltaTime);
		}
	}







	//UPDATE THE STATS OF OUR PLAYER

	public void SetButtonsValue()
	//Set the values of the slider
	{
		StaminaButton.transform.localScale = new Vector3(Stamina / MaxStamina, 1, 1);
		HealthButton.transform.localScale = new Vector3(Health / MaxHealth, 1, 1);
		ManaButton.transform.localScale = new Vector3(Mana / MaxMana, 1, 1);
	}


	public void SetMainStats()
	//Reset the values of our player
	{
		Stamina += 10 * Time.deltaTime;
		Stamina = Mathf.Min (Stamina, MaxStamina);
		if (!IsAbleToRun && Stamina > 40)
		{
			IsAbleToRun = true;
			speed = WalkSpeed;
		}
	}







	//FIGHTING

	public void Attack()
	//Attack with our weapon
	{
		if (Input.GetButtonDown("Attack"))
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit))
			{
				if (hit.collider.gameObject.tag == "Enemy" 
					&& (hit.transform.position - this.transform.position).magnitude < ActualWeapon.RangeOfAttk 
					&& IsAbleToAttack < 0)
				{
					_Character enemy = hit.collider.gameObject.GetComponent<_Character> ();
					base.Attack (enemy);
				}
			}
		}
	}


	public void LaunchSpell()
	//Launch the spell with our actual spell
	{
		if (Input.GetButtonDown("LaunchSpell"))
		{
			if (ActualSpell.ObjectName == "Heal" || ActualSpell.ObjectName == "AirWall")
			{
				Debug.Log ("coucou");
				base.LaunchSpell (null);
			}
			else
			{
				RaycastHit hit;
				if (Physics.Raycast(transform.position, transform.forward, out hit))
				{
					if (hit.collider.gameObject.tag == "Enemy" 
						&& (hit.transform.position - this.transform.position).magnitude < ActualWeapon.RangeOfAttk 
						&& Mana - ActualSpell.ManaConsumed > 0)
					{
						_Character enemy = hit.collider.gameObject.GetComponent<_Character> ();
						base.LaunchSpell (enemy);
					}
				}
			}
		}
	}
		

	new public void Die()
	//Die and display the GameOver menu
	{
		Interface.SetActive (false);
		GameOver.SetActive (true);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
}


//1 WARNING
//crouching reduce the area of detecting + we don't want to just put the camera down
