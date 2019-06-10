using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class faceSound : MonoBehaviour {

    public Flowchart flowchart;
    public AudioClip[] face;
    private AudioSource audioSource;
    float saveBgmVolume;
    public int faceNum {
        get { return flowchart.GetIntegerVariable("faceNum"); }
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
    //撥放音效
    public void PlaySound()
    {
        audioSource.clip = face[faceNum];
        audioSource.Play();
    }
    //停止音效
    public void stop() {
        audioSource.Stop();
    }
}
