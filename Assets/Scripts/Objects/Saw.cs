using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour {

    private Vector3 origin;
    public float amplitude;
    public bool right;
    public bool lateral;
    private float currenttime = 0;
    public int speed;

    void Awake()
    {
        origin = transform.position;
    }

	void Update ()
    {
        if (lateral)
        {
            if (currenttime > amplitude || currenttime < -amplitude)
                right = !right;
            if (right)
            {
                transform.position += new Vector3(0, 0, Time.deltaTime * speed);
                currenttime += Time.deltaTime;
            }
            else
            {
                transform.position -= new Vector3(0, 0, Time.deltaTime * speed);
                currenttime -= Time.deltaTime;
            }
        }
        else
        {
            if (currenttime > amplitude || currenttime < -amplitude)
                right = !right;
            if (right)
            {
                transform.position += new Vector3(Time.deltaTime * speed, 0, 0);
                currenttime += Time.deltaTime;
            }
            else
            {
                transform.position -= new Vector3(Time.deltaTime * speed, 0, 0);
                currenttime -= Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
			other.gameObject.GetComponent<MainCharacter>().Health -= 50;
        }
    }
}
