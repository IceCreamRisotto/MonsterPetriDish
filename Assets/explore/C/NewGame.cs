using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class NewGame : MonoBehaviour {
    Flowchart flowchart;
    public int scene {
        get { return flowchart.GetIntegerVariable("SceneCount"); }
        set { flowchart.SetIntegerVariable("SceneCount",value); }
    }
    private void Start()
    {
        flowchart = FindObjectOfType<Flowchart>();
    }
    public void count() {
        
    }
}
