using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusChange : MonoBehaviour {
    GameManager gameManager;
    public Image image;
    [Header("狀態欄改變方向")]
    public string[] Status;
    void statusChange()
    {
        image = GetComponent<Image>();
        gameManager = FindObjectOfType<GameManager>();
        image.sprite = Resources.Load("Character/" + Status[gameManager.playerStatusNo],typeof(Sprite)) as Sprite;
    }
}
