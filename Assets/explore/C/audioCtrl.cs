using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class audioCtrl : MonoBehaviour {

    public Flowchart flowchart;
    private AudioSource audioSource;
    public AudioClip[] bgm;

    float saveBgmVolume;
    public int scence {
        get { return flowchart.GetIntegerVariable("scence"); }
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        audioSource.clip = bgm[scence - 1];
        //Debug.Log(scence - 1);
        if (PlayerPrefs.HasKey("BgmVolume"))
        {
            saveBgmVolume = PlayerPrefs.GetFloat("BgmVolume");
            audioSource.volume = saveBgmVolume;
        }
        else
        {
            audioSource.volume = 0.8f;
        }
        audioSource.Play();
    }

}
