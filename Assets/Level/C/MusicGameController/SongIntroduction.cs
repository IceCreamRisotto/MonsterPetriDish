using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongIntroduction : MonoBehaviour {

    Button button;

    int songNo;

    string songName;

    Text songText;

    GameManager gameManager;

    public void Initialization(GameManager gameCon,int no,string text)
    {
        gameManager = gameCon;
        songNo = no;
        songName = text;
        songText = transform.GetChild(0).gameObject.GetComponent<Text>();
        songText.text = songName;
    }

    // Use this for initialization
    void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetSong);
    }

    void SetSong()
    {
        gameManager.nowSong = songNo;
        gameManager.PlayTestSong();
    }
}
