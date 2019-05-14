using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerExp : MonoBehaviour {


    int oldPlayerExp;


    //經驗值相關UI
    public Text expText;
    public Slider expSlider;


    //引用
    public GameManager_Main gameManager_Main;
    public ItemController itemController;

    public GameManager gameManager;

	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType<GameManager>();

        //playerLv = gameManager.GetPlayerExp(0);
        //playerExp = gameManager.GetPlayerExp(1);

        expText.text = gameManager.GetPlayerExp(1) + "/" + gameManager.GetPlayerExp(2);
        expSlider.value = (float)gameManager.GetPlayerExp(1) / (float)gameManager.GetPlayerExp(2);

        oldPlayerExp = gameManager.GetPlayerExp(1);

        /*
        //角色等級置入
		if(!PlayerPrefs.HasKey("playerLv"))
        {
            playerLv = 0;
            Debug.Log("角色等級為空");
        }

        //角色經驗值置入
        if(PlayerPrefs.HasKey("playerExp"))
        {
            playerExp = PlayerPrefs.GetInt("playerExp");
            expText.text = playerExp + "/" + playerExpUp;
            expSlider.value = (float)playerExp / (float)playerExpUp;

            oldPlayerExp = playerExp;
        }
        else
        {
            playerExp = 0;
            oldPlayerExp = playerExp;
            Debug.Log("角色經驗值為空");
        }*/


    }
	
	// Update is called once per frame
	void Update () {
        
	}

    /*public void Initialize(ItemController itemControllerGo)
    {
        //itemController = itemControllerGo;
    }*/
    
    //獲取角色等級
    public int GetPlayerLv()
    {
        return gameManager.GetPlayerExp(0);
    }

    //吃到食物
    //扣除食物，增加經驗值，經驗值UI變化，存檔
    public void PlayerEat(int itemNo)
    {
        int exp;
        itemController.ItemDeduct(itemNo);
        exp = itemController.ItemExp(itemNo);
        gameManager.SetPlayerExp(1,exp);
        StartCoroutine(ExpUIAdd(exp));
    }

    IEnumerator ExpUIAdd(int exp)
    {
        while(oldPlayerExp < gameManager.GetPlayerExp(1))
        {
            oldPlayerExp += 1;

            if(oldPlayerExp >= gameManager.GetPlayerExp(2))
            {
                gameManager.SetPlayerExp(0,1);
                gameManager_Main.PlayerLvUp();
                oldPlayerExp = 0;
                gameManager.SetPlayerExp(1, (gameManager.GetPlayerExp(2) * -1));
            }

            expText.text = oldPlayerExp + "/" + gameManager.GetPlayerExp(2);
            expSlider.value = (float)oldPlayerExp / (float)gameManager.GetPlayerExp(2);
            yield return new WaitForSeconds((10f/exp)* 0.1f);
        }
        expText.text = gameManager.GetPlayerExp(1) + "/" + gameManager.GetPlayerExp(2);
        expSlider.value = (float)gameManager.GetPlayerExp(1) / (float)gameManager.GetPlayerExp(2);
        oldPlayerExp = gameManager.GetPlayerExp(1);
    }
}
