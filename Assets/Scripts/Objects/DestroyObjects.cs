using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyObjects : MonoBehaviour {

	public GameObject Restriction;
	private PhotonView photonView;

	void Awake()
	{
		photonView = GetComponent<PhotonView> ();
	}

	void OnMouseDown ()
	{
		photonView.RPC ("Destroy_Wall", PhotonTargets.All, Restriction);
	}

	[PunRPC]
	void Destroy_Wall(GameObject Restriction)
	{
		GameObject.Destroy (Restriction);
	}
}
