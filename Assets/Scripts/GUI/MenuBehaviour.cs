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

	public void LockMouse()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}


	public void Restart(MainCharacter player)
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		player.transform.position = player.GetComponent<MainCharacter> ().SpawnPoint;
		player.Health = player.MaxHealth;
		player.Stamina = player.MaxStamina;
		player.Mana = player.MaxMana;
	}

    public void ReturnToTitle(GameObject player)
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        GameObject.Destroy(player);
        SceneManager.LoadScene("Menu");
    }
}