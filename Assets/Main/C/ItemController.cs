using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour {

    //對應編號物件剩餘量
    [Header("物件剩餘量")]
    public int[] items;

    //對應編號物件經驗值量
    [Header("物件給予經驗值量")]
    public int[] itemsExp;

    //物品欄物件
    [Header("物品欄物件")]
    public GameObject[] itemPropGameObjects;

    //收集冊物件
    [Header("收集冊物件")]
    public GameObject[] itemGameObjects;

    //收集冊數量
    [Header("收集冊物件數量")]
    public Text[] itemGameObjectsText;

    //這個物件一次扣多少個
    //int[] deduct;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void ClickProp()
    {
        for(int i=0;i<itemPropGameObjects.Length;i++)
        {
            if(items[i]>0)
            {
                itemPropGameObjects[i].SetActive(true);
            }
            else
            {
                itemPropGameObjects[i].SetActive(false);
            }
        }
    }

    public void ClickInventory()
    {
        for(int i=0;i<itemGameObjects.Length;i++)
        {
            if(items[i]>0)
            {
                itemGameObjects[i].SetActive(true);
                itemGameObjectsText[i].text = items[i].ToString();
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
        return items[itemNo];
    }

    //扣除對應物件剩餘量
    public void ItemDeduct(int itemNo)
    {
        if (items[itemNo] > 0)
            items[itemNo] -= 1;
        if (items[itemNo] <= 0)
            PropItemOpacity(itemNo);
    }

    void PropItemOpacity(int itemNo)
    {
        itemPropGameObjects[itemNo].SetActive(false);
    }

    //檢查對應物件經驗值
    public int ItemExp(int itemNo)
    {
        return itemsExp[itemNo];
    }

    
}
