using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class ConfirmGetProp : MonoBehaviour
{

    GameManager gameManager;
    public Flowchart flowchart;


    public int PropNumber
    {
        get { return flowchart.GetIntegerVariable("PropNumber"); }
    }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        flowchart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
    }

    public void GetProp()
    {
        gameManager.Explore_Prop_Probablilty[14] = 15;//進化道具重製
        gameManager.ProbabilityProp_DataSave();//
        confirm_evolution_props();
        gameManager.SetItems(PropNumber, 1);
    }

    void confirm_evolution_props()
    {
        for (int i = 0; i < gameManager.Evolution_props.Length; i++) {
            if (PropNumber == gameManager.Evolution_props[i]) {
                gameManager.SetItems_Evolution_props(PropNumber);
            }
        }
    }
}
