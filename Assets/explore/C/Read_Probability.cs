using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Read_Probability : MonoBehaviour {

    Flowchart flowchart;
    GameManager gameManager;

    void Start()
    {
        flowchart = FindObjectOfType<Flowchart>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void read_probability() {
        for (int i = 0; i < gameManager.Explore_Prop_Probablilty.Length; i++) {
            flowchart.SetIntegerVariable("PropProbability" +i, (100-gameManager.Explore_Prop_Probablilty[i]));
        }
    }
}
