using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCP : MonoBehaviour {

    public GameObject question;
    public GameObject answer01;
    public GameObject answer02;
    public GameObject answer03;
    public GameObject answer04;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Deactivate_text();
            GameObject.Destroy(this);
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
