using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cars : MonoBehaviour 
{
	public Vector3 Spanwpoint;

	public bool inside;
	public bool HasStarted;
	public float canTurn;

	public void Awake()
	{
		HasStarted = !inside;
	}

	public void OnTriggerEnter(Collider other)
	{
		Debug.Log ("coucou");
		if (other.tag == "CarsTurns")
		{
			if (HasStarted && canTurn <= 0)
			{
				if (other.gameObject.GetComponent<BoxCollider>() != null)
				{
					transform.Rotate (0, 90, 0);
					canTurn = 2;
				}
				else if (other.gameObject.GetComponent<SphereCollider>() != null)
				{
					transform.Rotate (0, -90, 0);
					canTurn = 2;
				}
			}
			else
			{
				if (!HasStarted)
				{
					if (other.gameObject.GetComponent<CapsuleCollider>() != null)
					{
						HasStarted = true;
						transform.Rotate (0, -90, 0);
						canTurn = 2;
					}
				}
			}
		}
	}


	public void Update()
	{
		this.transform.Translate (new Vector3(-1, 0, 0));
		canTurn = Mathf.Max (0, canTurn - Time.deltaTime);
		Debug.Log (Random.Range (1, 3));
	}

}
