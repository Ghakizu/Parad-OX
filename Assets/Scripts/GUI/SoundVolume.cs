using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundVolume : MonoBehaviour {

    public AudioMixer mixer;

    public void setvolume(float volume)
    {
        mixer.SetFloat("volume", volume);
    }
	
}
