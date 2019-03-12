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

    public Image explorePicture;
    public Button explorButton;

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
        else if(explorationNo == 2)
        {
            Block talkBlock = gamemanagerFlowchart.FindBlock("探索3出現");
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
        if(explorationNumber==2)//洞窟
        {
            //explorationEnd = new DateTime(nullTime.Year, nullTime.Month, nullTime.Day, nullTime.Hour+1, nullTime.Minute, nullTime.Second);
            explorationEndTime = new DateTime(explorationStartTime.Year, explorationStartTime.Month, explorationStartTime.Day, explorationStartTime.Hour + 1, explorationStartTime.Minute, explorationStartTime.Second);
            PlayerPrefs.SetString("explorationEndTime", explorationEndTime.ToString());
        }
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
        explorePicture.sprite = Resources.Load("explore/"+exploreNumber, typeof(Sprite)) as Sprite;
    }
}
