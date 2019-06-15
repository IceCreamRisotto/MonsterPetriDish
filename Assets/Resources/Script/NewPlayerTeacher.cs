using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPlayerTeacher : MonoBehaviour
{

    //存檔物件名稱(依據場景修改)
    [Header("存檔物件名稱")]
    public string playerPrefsName;

    //場景序計數器
    [Header("場景序")]
    public int sceneCount;

    //場景序計數器終點(依據場景修改)
    //[Header("場景序終點")]
    //public int sceneCountEnd;

    [Header("當前場景遮罩")]
    public int[] sceneMask;

    [Header("當前場景文字")]
    public string[] sceneTalk;

    //引用
    //遮罩
    Transform maskObject;

    //黑幕
    GameObject canvasObject;

    //人物
    GameObject childObject;

    //文字
    Text textObjectMask;

    //下一步按鈕
    GameObject buttonObject;

    void Awake()
    {
        maskObject = gameObject.transform.GetChild(0);
        canvasObject = gameObject.transform.GetChild(1).gameObject;
        childObject = canvasObject.transform.Find("TeacherUI").gameObject;
        //textObject = childObject.transform.Find("Talk").Find("SceneText").gameObject.GetComponent<Text>();
        textObjectMask = childObject.transform.Find("TalkMask").Find("SceneText").gameObject.GetComponent<Text>();
        buttonObject = canvasObject.transform.Find("Click").gameObject;
    }

    // Use this for initialization
    void Start()
    {
        //測試用
        //PlayerPrefs.SetInt(playerPrefsName, 0);

        //初始化
        canvasObject.SetActive(false);
        childObject.SetActive(false);
        //ClickButtonOff();

        //先判斷該場景是否已經做完新手教學
        if (PlayerPrefs.HasKey(playerPrefsName))
        {
            //場景序=上次中斷時場景 || 新場景0
            sceneCount = PlayerPrefs.GetInt(playerPrefsName);
        }
        else
        {
            sceneCount = 0;
            PlayerPrefs.SetInt(playerPrefsName, 0);
        }

        //如果新手教學沒播完
        if(sceneCount != -1)
        {
            canvasObject.SetActive(true);
            Invoke("SceneFunction",0.8f);
            Invoke("ClickButtonOn", 1.7f);
        }
	}

    //重看新手教學
    public void ReTeacher()
    {
        sceneCount = 0;
        PlayerPrefs.SetInt(playerPrefsName, 0);
        canvasObject.SetActive(true);
        Invoke("SceneFunction", 0.8f);
        Invoke("ClickButtonOn", 1.7f);
    }

    //點擊UI
    public void ClickButton()
    {
        SceneFunction();
    }

    //場景對話
    void SceneFunction()
    {
        //如果還有對話事件
        if(sceneCount<sceneTalk.Length)
        {
            //關閉上一個遮罩顯示
            if(sceneCount!=0)
                maskObject.GetChild(sceneMask[sceneCount - 1]).gameObject.SetActive(false);
            //開啟當前遮罩
            maskObject.GetChild(sceneMask[sceneCount]).gameObject.SetActive(true);
            //判斷使用哪個遮罩
            canvasObject.transform.parent = maskObject.GetChild(sceneMask[sceneCount]);

            childObject.SetActive(false);
            childObject.SetActive(true);

            //textObject.text = sceneTalk[sceneCount];
            textObjectMask.text = sceneTalk[sceneCount];

            PlayerPrefs.SetInt(playerPrefsName, sceneCount);
            sceneCount++;
        }
        else
        {
            PlayerPrefs.SetInt(playerPrefsName, -1);
            canvasObject.SetActive(false);
        }
    }

    //Button開啟
    void ClickButtonOn()
    {
        buttonObject.SetActive(true);
    }

    //Button關閉
    void ClickButtonOff()
    {
        buttonObject.SetActive(false);
    }
}
