using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class PauseButton : MonoBehaviour {

    public Sprite[] sprites;

    public RhythmGameController rhythmGameController;

    Button button;

    Image image;

    [Header("設定畫面歌名")]
    public Text setSongName;

    [Header("設定畫面難度按鈕圖片")]
    public Image setSongButton;

    //設定畫面難度文字
    Text setSongButtonText;

    [Header("設定畫面難度按鈕圖片")]
    public Image setGameScoreSongButton;

    //結算畫面難度文字
    Text setGameScoreSongButtonText;

    //public Slider setSlider;

    //public Dropdown setDropdown;

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
        setSongButtonText = setSongButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        setGameScoreSongButtonText = setGameScoreSongButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();


        /*if(gameManager!=null)
        {
            SetUIInitialize();
        }*/
        if (gameManager.newSongPlay)
        {
            gameManager.newSongPlay = false;
            NewFungusPauseOrPlayMusic();
            NewSongButton();
        }
        gameManager.InitializationFormLevel(this);

        if (gameManager.nowSong != -1)
            setSongName.text = gameManager.songslist[gameManager.nowSong];
        else
            Debug.Log("歌曲未選擇，歌曲名未載入");
        setSongButton.sprite = songLVButtonImage[gameManager.nowSongLv];
        setSongButtonText.text = gameManager.nowSongLvString[gameManager.nowSongLv];
        setGameScoreSongButton.sprite = songLVButtonImage[gameManager.nowSongLv];
        setGameScoreSongButtonText.text = gameManager.nowSongLvString[gameManager.nowSongLv];
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
        if (rhythmGameController.gameStart)
        {
            rhythmGameController.isPauseState = !rhythmGameController.isPauseState;
            if (rhythmGameController.isPauseState)
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
    }

    //設定的歌曲下拉選單置入
    /*
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
    */

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

    //難度Button點擊
    public void TestLVButtonImage()
    {
        if (gameManager.nowSongLv == 2)
            gameManager.nowSongLv = 0;
        else
            gameManager.nowSongLv += 1;
        songLVButton.sprite = songLVButtonImage[gameManager.nowSongLv];
        songLVButtonText.text = gameManager.nowSongLvString[gameManager.nowSongLv];
    }

    public void ReNewPlay()
    {
        gameManager.NewSongPlayTrue();
    }
}
