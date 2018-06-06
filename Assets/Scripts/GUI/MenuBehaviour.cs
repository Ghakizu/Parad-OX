﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour 
{
    //All the functions usefull for the main menu display

    public AudioMixer mixer;  //To change the volume

    public void LaunchScene (string scene) 
	//Launch the scene given in parameters
	{
		SceneManager.LoadScene (scene);
	}

    public void OnCLickSoloButton()
    {
        PhotonNetwork.offlineMode = true;
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
		player.GetComponent<SaveData> ().Load ();
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		for (int i = 0; i < enemies.Length; ++i)
		{
			_Enemies enemy = enemies [i].GetComponent<_Enemies> ();
			if (enemy != null)
			{
				enemy.Health = enemy.MaxHealth;
				enemy.transform.position = enemy.SpawnPoint;
			}
		}
		player.transform.position = player.SpawnPoint;
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
        SceneManager.LoadScene("Menu");
    }

    public void setvolume(float volume)
	//Set the sound of the volume
    {
        mixer.SetFloat("volume", volume);
    }

	public void LoadGame()
	//Load the last game saved
	{
		PlayerPrefs.SetInt ("LOAD", 1);
	}


	public void SetSensitivity(Slider sensitivity)
	//Set the sensivity of the mouse;
	{
		PlayerPrefs.SetFloat ("Sensitivity", sensitivity.value);
	}
}