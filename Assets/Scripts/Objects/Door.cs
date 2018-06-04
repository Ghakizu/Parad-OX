using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public Animator anim;
    private float close;

	void Update ()
    {
        if (close >= 0)
            close -= Time.deltaTime;
        else
            anim.SetBool("Open", false);
        if (anim.GetBool("Open") && anim.GetFloat("opening") >= 0)
            anim.SetFloat("opening", anim.GetFloat("opening") - Time.deltaTime);
        if (!anim.GetBool("Open") && anim.GetFloat("closing") >= 0)
            anim.SetFloat("closing", anim.GetFloat("closing") - Time.deltaTime);
	}

    void OnMouseDown()
    {
        anim.SetBool("Open", true);
        anim.SetFloat("opening", .5f);
        close = 2f;
        anim.SetFloat("closing", .5f);
    }
}
