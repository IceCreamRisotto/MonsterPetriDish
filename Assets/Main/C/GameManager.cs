using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //全域GameManager
    static GameManager instance;

    //角色等級變數
    public int playerLv;

    //角色經驗值變數
    public int playerExp;

    //角色經驗值上限變數(暫時統一100)
    public int playerExpUp = 100;



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
        if (!PlayerPrefs.HasKey("playerLv"))
        {
            playerLv = 0;
            Debug.Log("角色等級為空");
        }

        //角色經驗值置入
        if (PlayerPrefs.HasKey("playerExp"))
        {
            playerExp = PlayerPrefs.GetInt("playerExp");
        }
        else
        {
            playerExp = 0;
            Debug.Log("角色經驗值為空");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //存檔
    public void DataSave()
    {

    }
}
