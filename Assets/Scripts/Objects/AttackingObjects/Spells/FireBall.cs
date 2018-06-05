using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : _Spells
{
	//A Spell that make a Fire Ball appear from us towards the target

	public GameObject FireBallObject;

	new public void Awake()
	//set all the stats of the spell
	{
		TimeOfEffect = 0;
		ObjectName = MaterialsAssignations.FireBallName;
		damages = 60; 
		RangeOfAttk = 300;
		TimeBetweenAttacks = 4;
		ManaConsumed = 50;
		SubDescription = "Launch a Fire ball towards the target";
		base.Awake ();
		sprite = Materials.FireBallSprite;
		FireBallObject = (GameObject)Resources.Load ("FireBall");
	}



	public void LaunchSpell()
	//Launch the spell FireBall against the target
	{
		GameObject Bullet = Instantiate (FireBallObject, Object.transform) as GameObject;
		Bullet.GetComponent<Bullet> ().owner = owner;
		Bullet.GetComponent<Bullet> ().damages = damages;
		Bullet.transform.parent = null;
		GameObject cam = ((MainCharacter)owner).cam;
		Vector3 direction = cam == null ? transform.up : cam.transform.forward;
		Bullet.GetComponent<Rigidbody> ().AddForce (direction * 10000);
		GameObject.Destroy (Bullet, 5f);
	}
}
