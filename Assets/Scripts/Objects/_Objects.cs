using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Objects : MonoBehaviour 
{
	//All the objects that can be picked up and used by a character


	public GameObject Object;  //To easily enable or disable the objects that we want
	public string ObjectName;  //The name of the Object (to allow switch case to know what attack to launch)
	public _Character owner;  //The owner of the Object. Can be a maincharacter or an ennemy. None if it's a clue on the floor for example
	public Sprite sprite;  //the sprite of the object (for the inventory
	public string description;  //the description of the Object (also for the inventory)


	public void Awake()
	//Set the owner of the Object : it must be a character
	{
		owner = this.GetComponent<_Character> ();
		if (owner == null) 
		{
			owner = this.GetComponentInParent<_Character> ();
		}
		Object = this.gameObject;
	}
}
