using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour {

    //角色等級變數
    public int playerLv;

    //角色經驗值變數
    public int playerExp;
    int oldPlayerExp;

    //角色經驗值上限變數(暫時統一100)
    public int playerExpUp = 100;


    //經驗值相關UI
    public Text expText;
    public Slider expSlider;


    //引用
    public ItemController itemController;

	// Use this for initialization
	void Start () {

        //角色等級置入
		if(PlayerPrefs.HasKey("playerLv"))
        {
            
        }
        else
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
        }

        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    /*public void Initialize(ItemController itemControllerGo)
    {
        //itemController = itemControllerGo;
    }*/

    //吃到食物
    //扣除食物，增加經驗值，經驗值UI變化
    public void PlayerEat(int itemNo)
    {
        int exp;
        itemController.ItemDeduct(itemNo);
        exp = itemController.ItemExp(itemNo);
        playerExp += exp;
        StartCoroutine(ExpUIAdd(exp));
    }

    IEnumerator ExpUIAdd(int exp)
    {
        while(oldPlayerExp < playerExp)
        {
            oldPlayerExp += 1;

            if(oldPlayerExp >= playerExpUp)
            {
                playerLv += 1;
                oldPlayerExp = 0;
                playerExp -= playerExpUp;
            }

            expText.text = oldPlayerExp + "/" + playerExpUp;
            expSlider.value = (float)oldPlayerExp / (float)playerExpUp;
            yield return new WaitForSeconds((10f/exp)* 0.1f);
        }
        expText.text = playerExp + "/" + playerExpUp;
        expSlider.value = (float)playerExp / (float)playerExpUp;
        oldPlayerExp = playerExp;
    }
}
