using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITSATRAP : MonoBehaviour {

	public GameObject go;

    void OnTriggerEnter()
    {
        go.SetActive(true);
    }
}
