using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class ConfirmGetProp : MonoBehaviour {

    GameManager gameManager;
    public Flowchart flowchart;
    public int PropLength;
    //public int Prop;

    public int PropNumber {
        get { return flowchart.GetIntegerVariable("PropNumber"); }
    }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        flowchart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
    }

    public void GetProp() {
        //Prop = PropNumber;
        gameManager.SetItems(PropNumber, 1);
    }

}
