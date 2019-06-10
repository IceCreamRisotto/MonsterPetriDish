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
    public int backNum
    {
        get { return flowchart.GetIntegerVariable("backNum"); }
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Start()
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
    //開始音效
    public void PlaySound()
    {
        audioSource.clip = bgm[backNum];
        audioSource.Play();
    }
    //停止音效
    public void stop()
    {
        audioSource.Stop();
    }

}
