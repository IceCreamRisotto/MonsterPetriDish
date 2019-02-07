using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using System;

public class GameManager_Main : MonoBehaviour {

    public int explorationNo;
    public Flowchart gamemanagerFlowchart;
    DateTime explorationStartTime;
    DateTime explorationEndTime;
    DateTime nullTime;
    



    public void debug()
    {
        /*b = new DateTime(a.Year,a.Month,a.Day,a.Hour+1,a.Minute,a.Second);
        Debug.Log("a="+a);
        Debug.Log(b);
        Debug.Log(b-a);
        Debug.Log(DateTime.Now);
        Debug.Log("TimeSpan" + b.Subtract(a));*/
    }

    public void Start()
    {
        //a= GetTime(GetTimeStamp());
        //b= (01:00:00);
        if (PlayerPrefs.HasKey("explorationNo"))
        {
            explorationNo = PlayerPrefs.GetInt("explorationNo");
        }
        else
        {
            explorationNo = 0;
        }
    }

    private void Update()
    {
        
    }

    private static string GetTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    }

    private DateTime GetTime(string timeStamp)
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

    public void explorationChack()
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
        }
    }

    public void explorationEnterValue(int explorationNumber)
    {
        explorationNo = explorationNumber;
        PlayerPrefs.SetInt("explorationNo", explorationNo);
        explorationStartTime = GetTime(GetTimeStamp());
        PlayerPrefs.SetString("explorationStartTime", explorationStartTime.ToString());
        //判斷哪一個探索關卡，給予endTime相應的加長時間
        if(explorationNumber==2)//洞窟
        {
            explorationEndTime = new DateTime(explorationStartTime.Year, explorationStartTime.Month, explorationStartTime.Day, explorationStartTime.Hour + 1, explorationStartTime.Minute, explorationStartTime.Second);
            PlayerPrefs.SetString("explorationEndTime", explorationEndTime.ToString());
        }
    }

    void timeToInt(String Time)
    {
        int n;
        String str;
        //n=Time.IndexOf(" ")
    }
}
