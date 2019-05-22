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
    [Header("等級/經驗值/經驗值上限")]
    public int[] playerExpManager=new int[3];


    //物品管理相關
    //對應編號物件剩餘量
    [Header("物件剩餘量")]
    public int[] items;

    //初次副本場景觸發器
    [Header("初次副本場景觸發器")]
    public bool newSongPlay;

    //歌曲列表
    [Header("歌曲列表")]
    public string[] songslist;

    //當前副本播放歌曲
    [Header("當前播放歌曲")]
    public int nowSong;

    //當前預設難度
    [Header("當前預設難度")]
    public int nowSongLv;

    //當前預設難度
    [Header("難度文字")]
    public string[] nowSongLvString;

    //副本初始化物件
    PauseButton pauseButton;

    //當前探索編號
    public int explorationNumber;

    public string[] GetProp;//=new string[15];

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

        GetProp = new string[15];
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
        for (int i = 0; i < items.Length; i++)
        {
            if (PlayerPrefs.HasKey("item" + i))
            {
                items[i] = PlayerPrefs.GetInt("item" + i);
            }
            else
            {
                Debug.Log("第" + i + "項物件剩餘量為空");
            }
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //副本專用載入物件初始化
    public void InitializationFormLevel(PauseButton pause)
    {
        pauseButton = pause;
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
        for(int i=0;i<items.Length;i++)
        {
            PlayerPrefs.SetInt("item" + i, items[i]);
        }
    }

    //副本初次選單歌曲Button輸入
    public void NewButtonOn(SongIntroduction buttonObject,int no)
    {
        buttonObject.Initialization(this, no, songslist[no]);
        buttonObject.gameObject.SetActive(true);
    }

    
    public void PlayTestSong()
    {
        pauseButton.PlayTestSong();
    }

    //副本載入(初次場景bool打勾)
    public void NewSongPlayTrue()
    {
        newSongPlay = true;
    }
}
