using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour {

    //小雞狀態頁控制器

    //遊戲管理器
    GameManager gameManager;

    //文本

    [Header("小雞基本狀態文本")]
    public string[] stattusTextEditor;

    [Header("小雞詳細內容文本")]
    public string[] contentTextEditor;


    //狀態UI相關

    [Header("屬性框(基本狀態)")]
    public Text statusText;

    [Header("介紹框(詳細內容)")]
    public Text contentText;

    // Use this for initialization
    void Start () {
        gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //點擊狀態Button
    public void ClickStatusButton()
    {
        statusText.text = "名稱:" + gameManager.playerName + "\nLv:" + gameManager.playerExpManager[0] + "\n職業:" + gameManager.playerCareer[gameManager.playerStatusNo] + "\n擅長技能:" + gameManager.playerSpecialSkill[gameManager.playerStatusNo];
        contentText.text = contentTextEditor[gameManager.playerStatusNo];
    }
}
