using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyObjects : MonoBehaviour {

	public GameObject Restriction;
	private PhotonView photonView;
	private float timer;
	private bool timerstarted = false;
	public bool destroy;
	public PhotonView destination;

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
				photonView.RPC ("Activate_Wall", PhotonTargets.All);
				timerstarted = false;
			}
		}
	}

	void OnMouseDown ()
	{
		if (destroy) 
		{
			photonView.RPC ("Destroy_Wall", PhotonTargets.All);
		} 
		else 
		{
			photonView.RPC ("Deactivate_Wall", PhotonTargets.All);
			timer = 5;
			timerstarted = true;
		}
	}

	[PunRPC]
	void Deactivate_Wall()
	{
		GameObject Restriction = (PhotonView.Find (destination.viewID)).transform.gameObject;
		Restriction.SetActive (false);
	}

	[PunRPC]
	void Activate_Wall()
	{
		GameObject Restriction = (PhotonView.Find (destination.viewID)).transform.gameObject;
		Restriction.SetActive (true);
	}

	[PunRPC]
	void Destroy_Wall()
	{
		GameObject Restriction = (PhotonView.Find (destination.viewID)).transform.gameObject;
		GameObject.Destroy (Restriction);
	}
}
