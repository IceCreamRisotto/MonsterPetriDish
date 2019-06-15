using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class NewGame : MonoBehaviour {
    Flowchart flowchart;
    public NewPlayerTeacher change;
    public int scene {
        get { return flowchart.GetIntegerVariable("SceneCount"); }
        set { flowchart.SetIntegerVariable("SceneCount",value); }
    }
    public int newGame {
        get { return flowchart.GetIntegerVariable("newGame"); }
        set { flowchart.SetIntegerVariable("newGame", value); }
    }
    private void Start()
    {
        flowchart = FindObjectOfType<Flowchart>();
        scene = change.sceneCount;
    }

    public void count() {
        scene = change.sceneCount;
    }

    public void readNewGame() {
        newGame = PlayerPrefs.GetInt(change.playerPrefsName);
    }
}
