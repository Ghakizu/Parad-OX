using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour 
{
	//All the functions usefull for the main menu display


	public void LaunchScene (string scene) 
	//Launch the scene given in parameters
	{
		SceneManager.LoadScene ("Hub_Scene");
	}

	public void setCanvasEnabled(GameObject canvas)
	//Set a gameobject active
	{
		canvas.SetActive (true);
	}

	public void setCanvasDisabled(GameObject canvas)
	//set a game object inactive
	{
		canvas.SetActive (false);
	}

	public void QuitGame()
	//Quit the game
	{
		Application.Quit ();
	}
}