using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loading : MonoBehaviour {
    public GameObject loadingImage;
    public Slider loadingBar;
    private AsyncOperation async;
    public Text load_text;
    public string level;

    public int explorationNumber;
    GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void clickButton() {
        loadingImage.SetActive(true);
        StartCoroutine(loadLevelWithBar(level));
    }

    IEnumerator loadLevelWithBar(string level) {
        int dis=0;
        int toProgress = 0;
        async = Application.LoadLevelAsync(level);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f) {
            toProgress = (int)async.progress * 100;
            while (dis < toProgress) {
                dis++;
                setLoading(dis);
                yield return new WaitForEndOfFrame();
            }
        }

        toProgress = 100;
        while (dis < toProgress) {
            dis++;
            setLoading(dis);
            yield return new WaitForEndOfFrame();
        }

        TurnToScreen();//旋轉螢幕
        exploreChange();//如果選擇探索(explore),設定explorationNumber為選擇的場景

        async.allowSceneActivation = true;
    }

    private void setLoading(float percent) {    //改變%數
        loadingBar.value = percent / 100f;
        load_text.text = percent.ToString() + " %";
    }

    private void TurnToScreen() {   //畫面轉向
        if (level == "Level")
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;

            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;

            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToLandscapeRight = false;
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
        }
    }

    public void exploreChange() {
        if (level == "Explore") {
            explorationNumber = GameObject.Find("GameManager_Main").GetComponent<GameManager_Main>().explorationNo;
            gameManager.explorationNumber = explorationNumber;
            //Debug.Log(explorationNumber);
            //Debug.Log(gameManager.explorationNumber);
        }
    }



}
