using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class _Enemies : _Character 
{
	//All the ennemies, they are able to Attack, to defend, to die ....
	public bool Is_Freezed;

	new void Awake () 
	{
		base.Awake ();
		Is_Freezed = false;
	}


	new void FixedUpdate () 
	{
		base.FixedUpdate ();
	}


	new public void Attack(_Character perso)
	{

	}


	public void Lookat()
	{
		//The ennemy must look at us from a certain distance
	}


	public void Follow()
	{
		//when you're close enough, the ennemy will chase you
	}
}
