using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load : MonoBehaviour {

    [Header("總關卡數")][SerializeField]
    private int Loading_Background_Amount;   //這關有幾個的關卡

    [Header("會跑出來的關卡數量")][SerializeField]
    private int Level_Amount;    //這關會執行關卡的個數

    Object[] obj;   //載入resources
    Material[] m_obj;   //將載入的resources轉成Material
    int[] obj_check;    //確認地圖的使用次數
    private int Background_Code;

    public bool changeing;

    void Awake()
    {
        //Loading_Background_Amount = 3;
        //Level_Amount = 3;

        obj = new Object[Loading_Background_Amount];
        m_obj = new Material[Loading_Background_Amount];
        obj_check = new int[m_obj.Length];

        changeing = false;

        for (int i=0;i<Loading_Background_Amount;i++) {
            obj[i]= Resources.Load("explore/infinite/level1/BackGround_" + i);
            m_obj[i] = obj[i] as Material;
        }

        Background_Code = UnityEngine.Random.Range(0, Loading_Background_Amount);   //讀取背景編號的方式
        Debug.Log(Background_Code);
        obj_check[Background_Code] += 1;
        LoadMap();

        Level_Amount--;
    }

    void Update()   //抓轉換的圖 呼叫轉場動畫 
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            if (Level_Amount > 0 && changeing==false) {
                changeing = true;
                Background_Code = UnityEngine.Random.Range(0,Loading_Background_Amount);//讀取背景編號的方式

                while (true)
                {
                    if (obj_check[Background_Code] >= 1)
                        Background_Code = UnityEngine.Random.Range(0, Loading_Background_Amount);
                    else
                        break;
                }

                obj_check[Background_Code] += 1;

                Debug.Log(Background_Code);
                GetComponent<Image>().enabled = true;
                GetComponent<Animation>().Play("Fade");

                Level_Amount--;
            }
        }
    }

    void LoadMap() {
        GameObject.Find("背景卷軸").GetComponent<Image>().material = m_obj[Background_Code];
    }

    void change_off() {
        changeing = false;
    }
}
