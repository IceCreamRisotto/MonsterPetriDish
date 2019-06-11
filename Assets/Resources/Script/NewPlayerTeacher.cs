using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerTeacher : MonoBehaviour {

    //存檔物件名稱(依據場景修改)
    [Header("存檔物件名稱")]
    public string playerPrefsName;

    //場景序計數器
    [Header("場景序")]
    public int sceneCount;

    //場景序計數器終點(依據場景修改)
    [Header("場景序終點")]
    public int sceneCountEnd;

    [Header("當前場景文字")]
    public string[] sceneTalk;

    

	// Use this for initialization
	void Start () {
        //先判斷該場景是否已經做完新手教學
        if(PlayerPrefs.HasKey(playerPrefsName))
        {
            //場景序=上次中斷時場景 || 新場景0
            sceneCount = PlayerPrefs.GetInt(playerPrefsName);
        }
        else
        {
            sceneCount = 0;
            PlayerPrefs.SetInt(playerPrefsName, 0);
        }

        if(sceneCount != sceneCountEnd)
        {

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
