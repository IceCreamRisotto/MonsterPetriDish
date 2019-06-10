using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeCtrl : MonoBehaviour {
    public GameObject[] audioImage;
    public AudioSource[] audioSource;
    float saveBgmVolume;

    private void Start()
    {
        if (PlayerPrefs.HasKey("BgmVolume"))
        {
            saveBgmVolume = PlayerPrefs.GetFloat("BgmVolume");
            if (saveBgmVolume == 0)
            {
                audioImage[0].SetActive(true);
                audioImage[1].SetActive(false);
            }
            else {
                audioImage[1].SetActive(true);
                audioImage[0].SetActive(false);
            }
        }
        else
        {
            audioImage[1].SetActive(true);
            audioImage[0].SetActive(false);
        }
    }

    public void MuteClick()
    {
        if (PlayerPrefs.GetFloat("BgmVolume") != 0)
        {
            PlayerPrefs.SetFloat("BgmVolume", 0);
            switchSound();
            audioImage[0].SetActive(true);
            audioImage[1].SetActive(false);
        }
        else
        {
            PlayerPrefs.SetFloat("BgmVolume", 0.8f);
            switchSound();
            audioImage[1].SetActive(true);
            audioImage[0].SetActive(false);
        }
    }
    void switchSound(){
        for (int i=0; i < audioSource.Length; i++) {
            audioSource[i].volume = PlayerPrefs.GetFloat("BgmVolume");
        }
    }
}
