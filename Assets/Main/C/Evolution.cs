using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Evolution : MonoBehaviour {

    GameManager gameManager;
    public M animator_state;
    [Header("場上chicken變化")]
    public string[] Switch_animator;

    [Header("改變的狀態圖片")]
    public Image image;
    [Header("狀態欄改變方向")]
    public string[] Status;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
        Evolution_state();
        statusChange();
    }

    public void Evolution_Button() {
        gameManager.playerStatusNo = (gameManager.playerStatusNo+1) % Switch_animator.Length;       //test
        gameManager.Evolution_prefs();//進化儲存
        Evolution_state();
        statusChange();
    }

    //進化狀態chick
    void Evolution_state() {
        animator_state.StopAllCoroutines();
        animator_state.states = 1;
        animator_state.animator.Play(Switch_animator[gameManager.playerStatusNo]);
        animator_state.coRou = null;
        animator_state.coRou2 = null;
        animator_state.Moving_target.position = animator_state.GetComponent<RectTransform>().position;
    }


    void statusChange()
    {
        image.sprite = Resources.Load("Character/" + Status[gameManager.playerStatusNo], typeof(Sprite)) as Sprite;
    }
}
