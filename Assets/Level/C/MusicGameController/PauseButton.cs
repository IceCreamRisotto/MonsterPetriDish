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

    public Slider setSlider;

    public Dropdown setDropdown;

    List<string> temoNames;

    public Flowchart flowchart;

    GameManager gameManager;

	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType<GameManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(PauseOrPlayMusic);
        image = GetComponent<Image>();
        if(gameManager!=null)
        {
            SetUIInitialize();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PauseOrPlayMusic()
    {
        rhythmGameController.isPauseState = !rhythmGameController.isPauseState;
        if(rhythmGameController.isPauseState)
        {
            flowchart.SetBooleanVariable("set", true);
            image.sprite = sprites[1];
            rhythmGameController.PauseMusic();
            Block block = flowchart.FindBlock("點擊設定");
            flowchart.ExecuteBlock(block);
        }
        else
        {
            flowchart.SetBooleanVariable("set", false);
            image.sprite = sprites[0];
            rhythmGameController.PlayMusic();
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
}
