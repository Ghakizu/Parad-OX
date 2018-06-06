using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour 
{
	public GameObject LoadScene;

	public void OnTriggerStay(Collider other)
	{
		if (other.GetComponent<MainCharacter>() != null && Input.GetKeyDown(KeyCode.Mouse0))
		{
			LoadScene.SetActive (true);
			this.gameObject.SetActive (false);
		}
	}

}
