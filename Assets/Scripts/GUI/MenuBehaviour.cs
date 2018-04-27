using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour 
{
	public void LaunchScene (string scene) 
	{
		SceneManager.LoadScene ("Hub_Scene");
	}

	public void setCanvasEnabled(GameObject canvas)
	{
		canvas.SetActive (true);
	}

	public void setCanvasDisabled(GameObject canvas)
	{
		canvas.SetActive (false);
	}

	public void QuitGame()
	{
		Application.Quit ();
	}
}