using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using System;
using UnityEngine.UI;

public class GameManager_Main : MonoBehaviour {

    //已經變成探索系統管理器的遊戲管理器

    public int explorationNo;
    public Flowchart gamemanagerFlowchart;
    public Text explorationText;
    public GameObject[] explorationOnButton;
    public GameObject[] explorationOffButton;

    [Header("探索圖片")]
    public Image explorePicture; //探索確認頁面的圖片

    [Header("探索中圖片")]
    public Image explorringPicture; //探索中頁面的圖片

    [Header("探索標題")]
    public Text explorTitle; //探索確認頁面標題

    [Header("探索中標題")]
    public Text explorringTitle; //探索中頁面標題

    [Header("探索副標")]
    public Text explorTitleTwo; //探索確認頁面副標題(探索時間)

    [Header("探索按鈕陣列")]
    public GameObject[] explorButtons; //探索確認頁面的確認按鈕

    [Header("探索確認頁文本")]
    String[,] explorText=new string[2,2]; //探索確認頁面對應文本(第一維=第幾關&第二維=主副標題)

    Boolean explorationOnOff; //因為探索放在update，設置開關減少效能消耗
    DateTime explorationStartTime; //探索開始時間 備考:幾乎不需要
    DateTime explorationEndTime; //探索結束時間
    DateTime nullTime; //在判斷式上判斷其他DateTime變數是否為空值的標準
    //DateTime explorationEnd; //探索結束所需時長的標準
    TimeSpan reciprocal; //計時器


    public void debug()
    {
        /*b = new DateTime(a.Year,a.Month,a.Day,a.Hour+1,a.Minute,a.Second);
        Debug.Log("a="+a);
        Debug.Log(b);
        Debug.Log(b-a);
        Debug.Log(DateTime.Now);
        Debug.Log("TimeSpan" + b.Subtract(a));*/
        Debug.Log(reciprocal);
    }

    public void Start()
    {

        //探索確認選單，文本更新處
        explorText[0,0] = "Level.1  滾筒木屋";
        explorText[0,1] = "探索時間 半 小時";
        explorText[1,0] = "Level.2  洞窟";
        explorText[1,1] = "探索時間 1 小時";

        //a= GetTime(GetTimeStamp());
        //b= (01:00:00);
        if (PlayerPrefs.HasKey("explorationNo"))
        {
            explorationNo = PlayerPrefs.GetInt("explorationNo");
            /*if (PlayerPrefs.HasKey("explorationStartTime"))
                explorationStartTime = Convert.ToDateTime(PlayerPrefs.GetString("explorationStartTime"));
            else
                Debug.Log("探索開始時間儲存出現錯誤");*/
            if (PlayerPrefs.HasKey("explorationEndTime"))
                explorationEndTime = Convert.ToDateTime(PlayerPrefs.GetString("explorationEndTime"));
            else
                Debug.Log("探索結束時間儲存出現錯誤");
        }
        else
        {
            explorationNo = 0;
            explorationOnOff = false;
        }


    }

    private void Update()
    {
        if (explorationOnOff)
            explorationIng();
    }

    private static string GetTimeStamp() //獲取時間DateTime方法(1)
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    } 

    private DateTime GetTime(string timeStamp) //獲取時間DateTime方法(2)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));  //时间戳起始点转为目前时区
        long lTime = long.Parse(timeStamp + "0000000");//转为long类型  
        TimeSpan toNow = new TimeSpan(lTime); //时间间隔
        return dtStart.Add(toNow); //加上时间间隔得到目标时间
    } 

    public void OnMenu(GameObject ui)
    {
        ui.SetActive(false);
    }

    public void explorationChack() //開啟探索ui時判斷
    {
        if (explorationNo == 0)
        {
            Block talkBlock = gamemanagerFlowchart.FindBlock("探索出現");
            gamemanagerFlowchart.ExecuteBlock(talkBlock);
        }
        else
        {
            exploreDataUpdate(explorationNo);
            Block talkBlock = gamemanagerFlowchart.FindBlock("探索中出現");
            gamemanagerFlowchart.ExecuteBlock(talkBlock);
            explorationOnOff = true;
        }
    }

    public void explorationEnterValue(int explorationNumber) //確認探索button
    {
        explorationNo = explorationNumber;
        PlayerPrefs.SetInt("explorationNo", explorationNo);
        explorationStartTime = GetTime(GetTimeStamp());
        //PlayerPrefs.SetString("explorationStartTime", explorationStartTime.ToString());
        //判斷哪一個探索關卡，給予endTime相應的加長時間
        if (explorationNumber == 1)//滾筒木屋
        {
            //explorationEnd = new DateTime(nullTime.Year, nullTime.Month, nullTime.Day, nullTime.Hour+1, nullTime.Minute, nullTime.Second);
            //explorationEndTime = new DateTime(explorationStartTime.Year, explorationStartTime.Month, explorationStartTime.Day, explorationStartTime.Hour, explorationStartTime.Minute+30, explorationStartTime.Second);
            explorationEndTime = explorationStartTime.AddMinutes(30);
            PlayerPrefs.SetString("explorationEndTime", explorationEndTime.ToString());
        }
        if (explorationNumber == 2)//洞窟
        {
            //explorationEnd = new DateTime(nullTime.Year, nullTime.Month, nullTime.Day, nullTime.Hour+1, nullTime.Minute, nullTime.Second);
            //explorationEndTime = new DateTime(explorationStartTime.Year, explorationStartTime.Month, explorationStartTime.Day, explorationStartTime.Hour + 1, explorationStartTime.Minute, explorationStartTime.Second);
            explorationEndTime = explorationStartTime.AddHours(1);
            PlayerPrefs.SetString("explorationEndTime", explorationEndTime.ToString());
        }
        exploreDataUpdate(explorationNo);
        Block talkBlock = gamemanagerFlowchart.FindBlock("探索選單確定"); //呼叫探索中
        gamemanagerFlowchart.ExecuteBlock(talkBlock);
        explorationOnOff = true;
    }

    public void explorationIng() //探索計時器(update)
    {
        
        reciprocal = explorationEndTime - GetTime(GetTimeStamp());
        if (reciprocal > nullTime-nullTime)
        {
            explorationText.text = "剩餘時間 " + reciprocal;
            buttonOnOff(explorationOnButton, true);
            buttonOnOff(explorationOffButton, false);
        }
        else
        {
            explorationText.text = "剩餘時間 00:00:00";
            buttonOnOff(explorationOnButton, false);
            buttonOnOff(explorationOffButton, true);
        }
    }

    void buttonOnOff(GameObject[] buttons,Boolean a)//a判斷t或f，把button打開或關掉
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(a);
        }
    }

    public void explorationClaer() //馬上完成&探索結束按鈕
    {
        explorationNo = 0;
        PlayerPrefs.DeleteKey("explorationNo"); //重置編號其他變數也會重設
    }

    public void explrationOff() //調用關閉探索計時器Update
    {
        explorationOnOff = false;
    }

    public void exploreButton(int exploreNumber) //點擊探索關卡，修改任務介面
    {
        exploreDataUpdate(exploreNumber);

        for (int i = 0; i < explorButtons.Length; i++)//開啟所屬編號button，其餘關閉
        {
            if (i + 1 == exploreNumber)
            {
                explorButtons[i].SetActive(true);
            }
            else
            {
                explorButtons[i].SetActive(false);
            }
        }

        Block talkBlock = gamemanagerFlowchart.FindBlock("探索選單出現"); //呼叫確認頁面出現
        gamemanagerFlowchart.ExecuteBlock(talkBlock);
    }

    void exploreDataUpdate(int exploreNumber) //探索資訊更新
    {
        explorTitle.text = explorText[exploreNumber - 1, 0]; //修改標題
        explorTitleTwo.text = explorText[exploreNumber - 1, 1]; //修改花費時間
        explorePicture.sprite = Resources.Load("explore/" + exploreNumber, typeof(Sprite)) as Sprite; //改圖

        explorringTitle.text = explorText[exploreNumber - 1, 0]; //修改探索中標題
        explorringPicture.sprite = Resources.Load("explore/" + exploreNumber, typeof(Sprite)) as Sprite; //改圖
    }
}
