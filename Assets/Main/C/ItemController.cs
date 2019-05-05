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

    //這個物件一次扣多少個
    //int[] deduct;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

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
    }

    //檢查對應物件經驗值
    public int ItemExp(int itemNo)
    {
        return itemsExp[itemNo];
    }
}
