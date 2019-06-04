using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusChange : MonoBehaviour {
    GameManager gameManager;
    public Image image;
    [Header("狀態欄改變方向")]
    public string[] Status;
    private void Start()
    {
        image = GetComponent<Image>();
        gameManager = FindObjectOfType<GameManager>();
        image.sprite = Resources.Load("Character/" + Status[gameManager.playerStatusNo], typeof(Sprite)) as Sprite;
    }
    void statusChange()
    {
        image.sprite = Resources.Load("Character/" + Status[gameManager.playerStatusNo],typeof(Sprite)) as Sprite;
    }
}
