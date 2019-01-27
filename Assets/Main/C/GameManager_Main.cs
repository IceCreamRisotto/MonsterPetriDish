using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class GameManager_Main : MonoBehaviour {

    public int explorationNo;
    public Flowchart gamemanagerFlowchart;
     

    public void Start()
    {
        explorationNo = 0;
    }

    public void OnMenu(GameObject ui)
    {
        ui.SetActive(false);
    }

    public void explorationChack()
    {
        if (gamemanagerFlowchart.GetIntegerVariable("explorationNumber")==0)
        {
            Block talkBlock = gamemanagerFlowchart.FindBlock("探索出現");
            gamemanagerFlowchart.ExecuteBlock(talkBlock);
        }
        else
        {
            Block talkBlock = gamemanagerFlowchart.FindBlock("探索3出現");
            gamemanagerFlowchart.ExecuteBlock(talkBlock);
        }
    }
}
