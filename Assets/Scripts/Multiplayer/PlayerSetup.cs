using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

	public Behaviour[] ComponentToDisable;

	public GameObject[] ComponentToReset;

	    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if(!isLocalPlayer)
        {
            int length = ComponentToDisable.Length;
            for(int i = 0; i < length; i++)
            {
                ComponentToDisable[i].enabled = false;
            }

			for(int i = 0; i < ComponentToReset.Length; i++) 
			//the objects that have an other layer in order to avoid camera clipping
			{
				ComponentToReset [i].layer = 0;
			}
        }
    }


	//useless
    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    } 


}
//Changing the color of our player is useless for the game, because we're doing an fps.