using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    //全域GameManager
    static GameManager instance;

    //角色經驗值相關
    //角色經驗值上限變數(暫時統一100)
    public int playerExpUp = 100;

    //0=角色等級,1=角色經驗值,2=角色經驗值上限
    public int[] playerExpManager=new int[3];


    //物品管理相關
    //對應編號物件剩餘量
    [Header("物件剩餘量")]
    public int[] items;


    private void Awake()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (instance==null) //第一個GameManager
        {
            instance = this;
            DontDestroyOnLoad(this);
            name = "通用事件管理";
        }
        else if(this!=instance) //若已經有不可破壞物件，刪除自己
        {
            Debug.Log("刪除" + sceneName + "的" + name);
            Destroy(this);
        }
        

    }

    // Use this for initialization
    void Start()
    {
        //角色等級置入
        if (PlayerPrefs.HasKey("playerLv"))
        {
            playerExpManager[0] = PlayerPrefs.GetInt("playerLv");
        }
        else
        {
            playerExpManager[0] = 0;
            Debug.Log("角色等級為空");
        }

        //角色經驗值置入
        if (PlayerPrefs.HasKey("playerExp"))
        {
            playerExpManager[1] = PlayerPrefs.GetInt("playerExp");
        }
        else
        {
            playerExpManager[1] = 0;
            Debug.Log("角色經驗值為空");
        }

        playerExpManager[2] = playerExpUp;

        //物件剩餘量提取
        if (PlayerPrefs.HasKey("items1"))
            items[0] = PlayerPrefs.GetInt("items1");
        else
        {
            Debug.Log("第一項物件剩餘量為空");
            items[0] = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //角色經驗值相關資料提取
    //0=角色等級,1=角色經驗值,2=角色經驗值上限
    public int GetPlayerExp(int no)
    {
        return playerExpManager[no];
    }

    //修改角色經驗值相關資料變數
    public void SetPlayerExp(int no,int value)
    {
        playerExpManager[no] += value;
        ExpDataSave();
    }

    //獲得對映編號物件剩餘量
    public int GetItems(int no)
    {
        return items[no];
    }

    //增加對應編號物品剩餘量
    public void SetItems(int no,int value)
    {
        items[no] += value;
        ItemDataSave();
    }

    //經驗相關存檔
    void ExpDataSave()
    {
        PlayerPrefs.SetInt("playerLv", playerExpManager[0]);
        PlayerPrefs.SetInt("playerExp", playerExpManager[1]);
    }

    //物品相關存檔
    void ItemDataSave()
    {
        PlayerPrefs.SetInt("items1", items[0]);
    }
}
