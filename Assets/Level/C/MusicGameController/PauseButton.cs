using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class PauseButton : MonoBehaviour {

    public bool newPlay;

    public Sprite[] sprites;

    public RhythmGameController rhythmGameController;

    Button button;

    Image image;

    public Slider setSlider;

    public Dropdown setDropdown;

    List<string> temoNames;

    public Flowchart flowchart;

    GameManager gameManager;

    public SongIntroduction[] newSongButton;

    public AudioSource testAudio;

    public AudioClip[] testMusics;

    public CDAnim cdAnim;

    //難度Button
    public Image songLVButton;

    public Sprite[] songLVButtonImage;

    //難度Button文字
    Text songLVButtonText;

	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType<GameManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(PauseOrPlayMusic);
        image = GetComponent<Image>();

        songLVButtonText = songLVButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();

        if(gameManager!=null)
        {
            SetUIInitialize();
        }
        if(newPlay)
        {
            newPlay = false;
            NewFungusPauseOrPlayMusic();
            NewSongButton();
        }
        gameManager.InitializationFormLevel(this);
    }

    void NewFungusPauseOrPlayMusic()
    {
        rhythmGameController.isPauseState = !rhythmGameController.isPauseState;
        if (rhythmGameController.isPauseState)
        {
            image.sprite = sprites[1];
            rhythmGameController.PauseMusic();
        }
        else
        {
            image.sprite = sprites[0];
            rhythmGameController.PlayMusic();
        }
        Block block = flowchart.FindBlock("首次載入副本");
        flowchart.ExecuteBlock(block);
    }

    void NewSongButton()
    {
        for (int i = 0; i < gameManager.songslist.Length; i++)
            gameManager.NewButtonOn(newSongButton[i], i);
    }

    public void PauseOrPlayMusic()
    {
        rhythmGameController.isPauseState = !rhythmGameController.isPauseState;
        if(rhythmGameController.isPauseState)
        {
            image.sprite = sprites[1];
            rhythmGameController.PauseMusic();
            flowchart.SetBooleanVariable("set", true);
            Block block = flowchart.FindBlock("點擊設定");
            flowchart.ExecuteBlock(block);
        }
        else
        {
            image.sprite = sprites[0];
            rhythmGameController.PlayMusic();
            flowchart.SetBooleanVariable("set", false);
            Block block = flowchart.FindBlock("點擊設定");
            flowchart.ExecuteBlock(block);
        }
    }

    void SetUIInitialize()
    {
        setDropdown.options.Clear();
        Dropdown.OptionData temoData;
        for (int i = 0; i < gameManager.songslist.Length; i++)
        {
            temoData = new Dropdown.OptionData();
            temoData.text = gameManager.songslist[i];
            setDropdown.options.Add(temoData);
        }
    }

    public void PlayTestSong()
    {
        Block block = flowchart.FindBlock("試聽Button點擊");
        flowchart.ExecuteBlock(block);
        testAudio.clip = testMusics[gameManager.nowSong];
        testAudio.Play();
        cdAnim.CDPlay();
        songLVButton.sprite = songLVButtonImage[gameManager.nowSongLv];
        songLVButtonText.text = gameManager.nowSongLvString[gameManager.nowSongLv];
    }
}
