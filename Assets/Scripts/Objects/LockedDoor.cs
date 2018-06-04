using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour {

    public Animator anim;
    private float close;
    public doorStayOpen door1;
    public doorStayOpen door2;
    public doorStayOpen door3;

    void Update()
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
        if (door1.isopen && door2.isopen && door3.isopen)
        {
            anim.SetBool("Open", true);
            anim.SetFloat("opening", .5f);
            close = 2f;
            anim.SetFloat("closing", .5f);
        }
    }
}
