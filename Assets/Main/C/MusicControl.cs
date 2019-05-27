using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicControl : MonoBehaviour {

    private AudioSource audioSource;
    private bool muteState;
    float saveBgmVolume;
    //public Slider musicSlider;

    public AudioClip[] Bgms;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Bgms[(int)Random.Range(0,Bgms.Length)];
    }

    private void Start()
    {
        
        if (PlayerPrefs.HasKey("BgmVolume"))
        {
            saveBgmVolume = PlayerPrefs.GetFloat("BgmVolume");
            audioSource.volume = saveBgmVolume;
            if (saveBgmVolume == 0)
                muteState = true;
            else
                muteState = false;
        }
        else
        {
            audioSource.volume = 0.8f;
            muteState = false;
        }
        audioSource.Play();
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
            audioSource.volume = 0;
            //musicSlider.value = 0;
            //BUG bool
            PlayerPrefs.SetFloat("BgmVolume", 0);
            muteState = true;
        }
        else
        {
            audioSource.volume = 0.8f;
            PlayerPrefs.SetFloat("BgmVolume", 0.8f);
            //musicSlider.value = preVolume;
        }
    }

}
