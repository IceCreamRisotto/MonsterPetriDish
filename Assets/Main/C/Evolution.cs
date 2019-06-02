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

    [Header("開啟選擇道具頁面")]
    public GameObject evolutionOpen;
    [Header("開啟確認頁面")]
    public GameObject ok;

    [Header("確認有沒有該道具")]
    public int[] chickProp;
    public GameObject[] openProp;

    [Header("確認道具")]
    public GameObject[] clickProp;

    [Header("文字改變")]
    public Text clickText;

    [Header("進化方向")]
    public int playerStatusNo;
    [Header("點擊道具")]
    public int itemClick;

    public ItemController item;//取得控制道具元件

    private void Start()
    {
        item = GameObject.Find("ItemController").GetComponent<ItemController>();
        gameManager = FindObjectOfType<GameManager>();
        
        //Evolution_state();
        //statusChange();
    }

    //選擇進化道具畫面
    public void open() {
        evolutionOpen.SetActive(true);
        for (int i = 0; i < chickProp.Length; i++)
        {
            if (gameManager.items[chickProp[i]] >= 1)
                openProp[i].SetActive(true);
            else
                openProp[i].SetActive(false);
        }
    }
    public void close() {
        evolutionOpen.SetActive(false);
        ok.SetActive(false);
    }

    //改變"道具圖示"
    public void confirmPicture(GameObject click) { 
        ok.SetActive(true);
        for (int i=0; i < clickProp.Length; i++) {
            clickProp[i].SetActive(false);
        }
        click.SetActive(true);
    }
    //改變"文字訊息"
    public void confirmText(string text) {
        clickText.text = text;
    }
    //設定"進化方向"
    public void playerStatusNoChange(int No) {
        playerStatusNo = No;
    }
    //設定"使用道具"
    public void statusProp(int prop) {
        itemClick = prop;
    }

    //點擊確認或取消
    public void confirm() {
        gameManager.playerStatusNo = playerStatusNo;
        gameManager.Evolution_prefs();//進化儲存
        itemLess();//道具扣除
        Evolution_state();//main的角色改變
        statusChange();//狀態欄腳色改變
        //關閉視窗
        evolutionOpen.SetActive(false);
        ok.SetActive(false);
    }
    public void itemLess() {
        item.ItemDeduct(itemClick);
    }
    public void cancel() {
        ok.SetActive(false);
    }

    //gameManager.playerStatusNo = (gameManager.playerStatusNo+1) % Switch_animator.Length;       //test
    //gameManager.Evolution_prefs();//進化儲存
    //Evolution_state();
    //statusChange();

    //改變Main角色
    void Evolution_state() {
        animator_state.StopAllCoroutines();
        animator_state.states = 1;
        animator_state.animator.Play(Switch_animator[gameManager.playerStatusNo]);
        animator_state.coRou = null;
        animator_state.coRou2 = null;
        animator_state.Moving_target.position = animator_state.GetComponent<RectTransform>().position;
    }

    //改變狀態欄角色
    void statusChange()
    {
        image.sprite = Resources.Load("Character/" + Status[gameManager.playerStatusNo], typeof(Sprite)) as Sprite;
    }
}
