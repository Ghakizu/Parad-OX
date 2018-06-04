using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorStayOpen : MonoBehaviour {

    public Animator anim;
    public bool isopen;

    void Update()
    {
        if (anim.GetBool("Open") && anim.GetFloat("opening") >= 0)
            anim.SetFloat("opening", anim.GetFloat("opening") - Time.deltaTime);
    }

    public void Open()
    {
        anim.SetBool("Open", true);
        anim.SetFloat("opening", .5f);
        isopen = true;
    }
}
