using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    public static SoundController controller;
    public AudioSource source;
    private static float timeRate;
    private static float timeSound;


    private void Awake()
    {
        if (controller != null)
        {
            Destroy(gameObject);
            return;
        }
        controller = this;
        timeSound = 0.450f;
        timeRate = 0;
    }

    public static void PlaySound(AudioClip sound)
    {
        if (timeRate <= Time.time)
        {
            controller.source.PlayOneShot(sound);
            timeRate = Time.time+timeSound;
        }
    }
}
