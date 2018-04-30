using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Load_scenes : MonoBehaviour 
{
	//Objects that load another scene

	public string Scene; //the name of the scene to load
	public Vector3 Spawnpoint; //the place where we want our player to spawn in the nest scene. By default, it's (0, 0, 0).
	public Vector3 Rotation; //the Rotation we want for our player when respawning. By default, it's (0, 0, 0).
	private GameObject Player; //The character we want to move in the other scene
	private bool IsTrigger = false; //Are we able to teleport ?



	public static IEnumerator LoadScenes(string SceneToLoad, GameObject Player, Vector3 Spawnpoint, Vector3 Rotation)
	{
		Scene CurrentScene = SceneManager.GetActiveScene();

		AsyncOperation AsyncLoad = SceneManager.LoadSceneAsync(SceneToLoad, LoadSceneMode.Additive);

		while (!AsyncLoad.isDone)
		{
			yield return null;
		} 
		Debug.Log ("coucou");
		SceneManager.MoveGameObjectToScene(Player, SceneManager.GetSceneByName(SceneToLoad));
		Player.transform.position = Spawnpoint;
		Player.transform.Rotate (Rotation);

		SceneManager.UnloadSceneAsync(CurrentScene);
		NetworkServer.SpawnObjects ();
	}


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player = other.gameObject;
            IsTrigger = true;  //we now can teleport to another scene
        }
    }


    private void OnTriggerExit(Collider other)
    {
        IsTrigger = false; //we can't teleport anymore
    }

    
    void OnGUI()
    {
        if (IsTrigger)
        {
            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 20, 200, 40), "Press E to move in " + Scene);
        }
    }


    void Update()
    {
        if(IsTrigger && Input.GetKeyDown(KeyCode.E))
        {
			StartCoroutine(LoadScenes(Scene, Player, Spawnpoint, Rotation));
            IsTrigger = false;
        }
    }
}