using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Correct_Answer : MonoBehaviour 
{
    
    public doorStayOpen door;
    public bool mainRoom;
    public float x;
    public float y;
    public float z;
    public GameObject question;
    public GameObject answer01;
    public GameObject answer02;
    public GameObject answer03;
    public GameObject answer04;

    void OnMouseDown()
	{
        if (mainRoom)
            door.Open();
        else
        {
            (GameObject.FindGameObjectWithTag("Player")).transform.position = new Vector3(x, y, z);
            Deactivate_text();
        }
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
