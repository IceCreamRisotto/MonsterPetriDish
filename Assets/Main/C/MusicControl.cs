using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicControl : MonoBehaviour {

    private AudioSource audioSource;
    private bool muteState;
    private float preVolume;
    //public Slider musicSlider;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1;
        muteState = false;
        preVolume = audioSource.volume;
    }

    /*public void VolumeChanged(float newVolume)
    {
        audioSource.volume = newVolume;
        muteState = false;
    }*/

    public void MuteClick()
    {
        muteState = !muteState;
        if (muteState)
        {
            preVolume = audioSource.volume;
            audioSource.volume = 0;
            //musicSlider.value = 0;
            //BUG bool
            muteState = true;
        }
        else
        {
            audioSource.volume = preVolume;
            //musicSlider.value = preVolume;
        }
    }

}
