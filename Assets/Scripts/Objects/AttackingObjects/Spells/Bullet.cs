using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	public _Character owner;  //who created this bullet ?
	public float damages;  //How much damages does the bullet do ?

	public void OnTriggerEnter(Collider other)
	//Destroy the object if it collides with another object
	{
		_Character target = other.GetComponent<_Character> ();
		if (target != owner && !other.isTrigger)
		{
			GameObject.Destroy (this.gameObject);
			if(target != null)
			{
				target.Health -= damages;
			}
		}
	}

}
