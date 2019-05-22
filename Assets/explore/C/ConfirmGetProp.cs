using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class ConfirmGetProp : MonoBehaviour {

    GameManager gameManager;
    Flowchart flowchart_par;
    public string Prop;
    public int PropLength;

    public string PropNumber {
        get { return flowchart_par.GetStringVariable("PropNumber"); }
    }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        flowchart_par = GameObject.Find("Flowchart").GetComponent<Flowchart>();
    }

    public void GetProp() {

        Prop = PropNumber;
        //Debug.Log(Prop);
        PropLength = gameManager.GetProp.Length;
        //Debug.Log(PropLength);

        for (int i = 0; i < PropLength; i++)
        {
            //Debug.Log("成功");
            if (gameManager.GetProp[i] ==null)
            {
                gameManager.GetProp[i] = PropNumber;
                break;
            }
        }
    }
}
