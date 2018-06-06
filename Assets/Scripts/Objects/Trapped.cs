using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapped : MonoBehaviour {

	public float x;
	public float y;
	public float z;
    public bool mainRoom;
	public GameObject player;
    public int coordinatex;
    public int coordinatey;
    public int coordinatez;
    private GameObject ennemy;
    public GameObject question;
    public GameObject answer01;
    public GameObject answer02;
    public GameObject answer03;
    public GameObject answer04;

    void Awake()
    {
        ennemy = (GameObject)Resources.Load("Fists enemy");
    }

	void OnMouseDown ()
	{
        player = GameObject.FindGameObjectWithTag("Player");
        if (mainRoom)
        {
            player.transform.position = new Vector3(x, y, z);
            Deactivate_text();
        }
        else
            SpawnEnnemies();
	}

    private void SpawnEnnemies()
    {
        GameObject.Instantiate(ennemy, new Vector3(coordinatex + 150, coordinatey, coordinatez), new Quaternion());
        GameObject.Instantiate(ennemy, new Vector3(coordinatex + 50, coordinatey, coordinatez), new Quaternion());
        GameObject.Instantiate(ennemy, new Vector3(coordinatex - 50, coordinatey, coordinatez), new Quaternion());
        GameObject.Instantiate(ennemy, new Vector3(coordinatex - 150, coordinatey, coordinatez), new Quaternion());
    }

    public void Deactivate_text()
    {
        question.SetActive(false);
        answer01.SetActive(false);
        answer02.SetActive(false);
        answer03.SetActive(false);
        answer04.SetActive(false);
    }
}
