using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyObjects : MonoBehaviour {

	public GameObject Restriction;
	private PhotonView photonView;
	private float timer;
	private bool timerstarted = false;
	public bool destroy;

	void Awake()
	{
		photonView = GetComponent<PhotonView> ();
	}

	void Update ()
	{
		if (timerstarted) 
		{
			if (timer > 0)
				timer -= Time.deltaTime;
			else 
			{
				photonView.RPC ("Activate_Wall", PhotonTargets.All, Restriction);
				timerstarted = false;
			}
		}
	}

	void OnMouseDown ()
	{
		if (destroy) 
		{
			photonView.RPC ("Destroy_Wall", PhotonTargets.All, Restriction);
		} 
		else 
		{
			photonView.RPC ("Deactivate_Wall", PhotonTargets.All, Restriction);
			timer = 5;
			timerstarted = true;
		}
	}

	[PunRPC]
	void Deactivate_Wall(GameObject Restriction)
	{
		Restriction.SetActive (false);
	}

	[PunRPC]
	void Activate_Wall(GameObject Restriction)
	{
		Restriction.SetActive (true);
	}

	[PunRPC]
	void Destroy_Wall(GameObject Restriction)
	{
		GameObject.Destroy (Restriction);
	}
}
