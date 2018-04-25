using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PNJs : _Character 
{
	//All the NPCs og the game


	public string[] dialogues; //all the sentences the NPCs can say.
	//If he has different things to say, we can put them in different cases of the table.



	new void Awake ()
	{
		base.Awake ();
	}





	public void OnMouseDown()
	{
		//We have to display the text when we click on him
	}
}
