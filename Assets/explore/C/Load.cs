using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load : MonoBehaviour {

    public SpriteRenderer[] background;
    public int i;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            i = 0;
            GetComponent<Image>().enabled = true;
            GetComponent<Animation>().Play("Fade");
        } else if (Input.GetKeyDown(KeyCode.W)){
            i = 1;
            GetComponent<Image>().enabled = true;
            GetComponent<Animation>().Play("Fade");
        }
    }

    void LoadMap( ){
        GameObject.Find("背景卷軸").GetComponent<Image>().sprite=background[i].sprite;
    }
}
