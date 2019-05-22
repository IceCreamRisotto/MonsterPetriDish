using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Store : MonoBehaviour {

    GameManager gameManager;
    Flowchart flowchart;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        flowchart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
    }

    public int eventCount
    {
        get { return flowchart.GetIntegerVariable("eventCount"); }
        set { flowchart.SetIntegerVariable("eventCount", value); }
    }
    public void Count()
    {//計算遇到事件次數,儲存至GameManager
        //Debug.Log(eventCount);
        gameManager.eventCount = eventCount;
    }
    public void readEventCount()
    {//讀取GameManager事件遭遇次數
        eventCount= gameManager.eventCount;
    }
}
