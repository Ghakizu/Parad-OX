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


	public void Restart(MainCharacter player)
	//Allows the player to Restart his game after losing
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		player.transform.position = player.GetComponent<MainCharacter> ().SpawnPoint;
		player.Health = player.MaxHealth;
		player.Stamina = player.MaxStamina;
		player.Mana = player.MaxMana;
	}


	public void Resume(GameObject Player)
	//Allows to reset the IsGamePaused value of the player and to unfreeze his position
	{
		Player.GetComponent<MainCharacter> ().IsGamePaused = false;
		Player.GetComponent<MainCharacter> ().CharacterRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
		_Character[] characters = GameObject.FindObjectsOfType<_Character> ();
		foreach (_Character character in characters)
		{
			character.ResumeGame ();
		}
	}


    public void ReturnToTitle(GameObject player)
	//Return to title when playing
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        GameObject.Destroy(player.transform.parent.gameObject);
        SceneManager.LoadScene("Menu");
    }
}