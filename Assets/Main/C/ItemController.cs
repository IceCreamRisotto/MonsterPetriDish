using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour {

    //對應編號物件經驗值量
    [Header("物件給予經驗值量")]
    public int[] itemsExp;

    //此次獲得物品數量
    //[Header("此次獲得物品數量")]
    int count;

    //物品欄物件
    [Header("物品欄物件")]
    public GameObject[] itemPropGameObjects;

    //收集冊物件
    [Header("收集冊物件")]
    public GameObject[] itemGameObjects;

    //收集冊數量
    [Header("收集冊物件數量文字")]
    public Text[] itemGameObjectsText;

    //可獲得道具編號(最後)
    [Header("可獲得道具編號(最後)")]
    public int canGetItemNo;

    //獲得物品物件
    [Header("獲得物品物件")]
    public GameObject[] getItemGameObject;
    //獲得物件下的子物件
    Image[] getItemGameObjectImage;

    //引用
    GameManager gameManager;

    GameManager_Main gameManager_Main;

    public GameObject getItemUI;

    public GameObject getItemUIButton;

    //物品圖片
    [Header("物品圖片")]
    public Sprite[] itemImage;

    // Use this for initialization
    void Start() {

        gameManager = FindObjectOfType<GameManager>();
        gameManager_Main = FindObjectOfType<GameManager_Main>();

        //檢查獲得物品
        if(PlayerPrefs.HasKey("getItemBoolean"))
        {
            if(PlayerPrefs.GetInt("getItemBoolean")==1)
            {
                int getItemNo = PlayerPrefs.GetInt("getItemNo");
                int getItem = PlayerPrefs.GetInt("getItem");
                gameManager.SetItems(getItemNo, getItem);
                PlayerPrefs.DeleteKey("getItemNo");
                PlayerPrefs.DeleteKey("getItem");
                PlayerPrefs.SetInt("getItemBoolean", 0);
            }
        }
        else
        {
            Debug.Log("物品是否獲取為空");
            PlayerPrefs.SetInt("getItemBoolean", 0);
        }

        getItemGameObjectImage = new Image[getItemGameObject.Length];
        for (int i = 0; i < getItemGameObject.Length; i++)
            getItemGameObjectImage[i] = getItemGameObject[i].transform.GetChild(0).gameObject.transform.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {

    }

    //打開物品
    public void ClickProp()
    {
        //int count = 0;
        for(int i=0;i<itemPropGameObjects.Length;i++)
        {
            if(gameManager.GetItems(i)>0)
            {
                /*itemPropGameObjects[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                itemPropGameObjects[i].transform.SetSiblingIndex(count);
                count++;*/
                itemPropGameObjects[i].SetActive(true);
            }
            else
            {
                //itemPropGameObjects[i].GetComponent<Image>().color = new Color32(255,255,255,1);
                itemPropGameObjects[i].SetActive(false);
            }
        }
    }

    //打開收集冊
    public void ClickInventory()
    {
        for(int i=0;i<itemGameObjects.Length;i++)
        {
            if(gameManager.GetItems(i)> 0)
            {
                itemGameObjects[i].SetActive(true);
                itemGameObjectsText[i].text = gameManager.GetItems(i).ToString();
            }
            else
            {
                itemGameObjects[i].SetActive(false);
                itemGameObjectsText[i].text = "";
            }
        }
    }


    //檢查對應物件剩餘量
    public int ItemMuch(int itemNo)
    {
        return gameManager.GetItems(itemNo);
    }

    //扣除對應物件剩餘量
    public void ItemDeduct(int itemNo)
    {
        if (gameManager.GetItems(itemNo) > 0)
            gameManager.SetItems(itemNo, -1);
        if (gameManager.GetItems(itemNo) <= 0)
            PropItemOpacity(itemNo);
    }

    //物件從物品消失
    void PropItemOpacity(int itemNo)
    {
        //itemPropGameObjects[itemNo].GetComponent<Image>().color = new Color32(255, 255, 255, 1);
        itemPropGameObjects[itemNo].SetActive(false);
    }

    //檢查對應物件經驗值
    public int ItemExp(int itemNo)
    {
        return itemsExp[itemNo];
    }


    //獲得道具部分腳本

    //觸發後開啟獲得道具視窗
    /*public void GetItemUIOpen()
    {
        getItemUI.SetActive(true);
        StartCoroutine(GetItemGameObjectOpen(0));
    }*/
    public void GetItemUIOpen(int count)
    {
        this.count = count;
        getItemUI.SetActive(true);
        StartCoroutine(GetItemGameObjectOpen(count));
    }

    //隨機抽取獲得物品&遞進顯示
    IEnumerator GetItemGameObjectOpen(int count)
    {
        int j=0;
        if (count != 0)
            for (int i = 0; i < count; i++)
            {
                yield return new WaitForSeconds(0.5f);
                j = Random.Range(0, canGetItemNo);
                getItemGameObjectImage[i].sprite = itemImage[j];
                gameManager.SetItems(j, 1);
                getItemGameObject[i].SetActive(true);
            }
        else
            for (int i=0;i<gameManager.explores[gameManager_Main.explorationNo-1];i++)
            {
                yield return new WaitForSeconds(0.5f);
                j = Random.Range(0, canGetItemNo);
                getItemGameObjectImage[i].sprite = itemImage[j];
                gameManager.SetItems(j, 1);
                getItemGameObject[i].SetActive(true);
            }
        yield return new WaitForSeconds(1f);
        getItemUIButton.SetActive(true);
    }

    //當獲得物品頁面的確定被點擊
    //關閉獲得物品頁面
    public void CloseGetItemUI()
    {
        for(int i=0;i<count;i++)
        {
            getItemGameObject[i].SetActive(false);
        }
        getItemUIButton.SetActive(false);
        getItemUI.SetActive(false);
    }

}
