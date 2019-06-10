using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class MoveSound : MonoBehaviour {
    public Flowchart flowchart;
    public AudioClip[] sound;
    private AudioSource audioSource;
    float saveBgmVolume;
    public int moveNum
    {
        get { return flowchart.GetIntegerVariable("moveNum"); }
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
        audioSource.clip = sound[moveNum];
        audioSource.Play();
    }
    //停止音效
    public void stop()
    {
        audioSource.Stop();
    }
    //加快行走音效
    public void runSound() {
        audioSource.pitch = 1.5f;
    }
    //一般行走音效
    public void normalSound() {
        audioSource.pitch = 1f;
    }



}
