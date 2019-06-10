using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Sound : MonoBehaviour {
    public Flowchart flowchart;
    public AudioClip[] sound;
    private AudioSource audioSource;
    float saveBgmVolume;
    public int soundNum {
        get { return flowchart.GetIntegerVariable("soundNum"); }
    }
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("BgmVolume"))
        {
            saveBgmVolume = PlayerPrefs.GetFloat("BgmVolume");
            audioSource.volume = saveBgmVolume;
        }
        else
        {
            audioSource.volume = 0.8f;
        }
    }
    //播放音效
    public void PlaySound()
    {
        audioSource.clip = sound[soundNum];
        audioSource.Play();
    }
    //停止音效
    public void stop()
    {
        audioSource.Stop();
    }
}
