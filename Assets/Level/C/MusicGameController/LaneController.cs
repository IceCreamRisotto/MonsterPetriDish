﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class LaneController : MonoBehaviour {

    int lanes;

    RhythmGameController gameController;

    [Tooltip("此音軌使用的鍵盤按鍵")]
    public KeyCode keyboardButton;

    [Tooltip("音軌對應事件的編號")]
    public int laneID;

    //對目標位置的鍵盤按下的視覺效果
    public Transform targetVisuals;

    //上下邊界
    public Transform targetTopTrans;
    public Transform targetBottomTrans;

    //包含在此音軌中的所有事件列表
    List<KoreographyEvent> laneEvents = new List<KoreographyEvent>();

    //包含此音軌當前活動的所有音符對列
    Queue<NoteObject> trackedNotes = new Queue<NoteObject>();

    //檢測此音軌中的生成的下一個事件索引
    int pendingEventIdx = 0;

    //按壓鍵盤特效
    public GameObject downVisual;

    //音符移動的目標位置
    public Vector2 TargetPosition
    {
        get
        {
            return transform.position;
        }
    }

    //音效
    public AudioSource SESource;

    //音效庫
    public AudioClip[] SEClip;

    //長音符變數
    public bool hasLongNote;
    public float timeVal = 0;
    public GameObject longNoteHitEffectGo;
    GameObject hitLongEffectGo;

    // Use this for initialization
    void Start () {
		
	}

    //UiButton點下方法
    public void buttonClickDown()
    {
        CheckNoteHit();
        downVisual.SetActive(true);
    }

    //UiButton案住方法
    public void buttonClicking()
    {
        //檢測長音符
        if (hasLongNote)
        {
            if (timeVal >= 0.15f)
            {
                //顯示命中等級 (Great Perfect)
                if (longNoteHitEffectGo.activeSelf)
                {
                    //CreateHitLongEffect();
                }
                timeVal = 0;
            }
            else
            {
                timeVal += Time.deltaTime;
            }
        }
    }

    //UiButton抬起方法
    public void buttonClickUp()
    {
        downVisual.SetActive(false);
        //檢測長音符
        if (hasLongNote)
        {
            longNoteHitEffectGo.SetActive(false);
            hitLongEffectGo.SetActive(false);
            CheckNoteHit();
        }
    }


    // Update is called once per frame
    void Update () {
        //暫停
        if (gameController.isPauseState)
        {
            return;
        }

        //清除無效音符
        while (trackedNotes.Count > 0 && trackedNotes.Peek().isNoteMissed())
        {
            gameController.missTatal += 1;
            if (trackedNotes.Peek().isLongNoteEnd)
            {
                hasLongNote = false;
                timeVal = 0;
                longNoteHitEffectGo.SetActive(false);
                hitLongEffectGo.SetActive(false);
            }
            gameController.comboNum = 0;
            gameController.HideComboNumText();
            gameController.ChangHitLevelSprite(0);
            gameController.UpdateHp();
            trackedNotes.Dequeue();
        }

        //檢測新音符產生
        CheckSpawnNext();

        //檢測玩家輸入
        if (Input.GetKeyDown(keyboardButton))
        {
            CheckNoteHit();
            downVisual.SetActive(true);
        }
        else if (Input.GetKey(keyboardButton))
        {
            //檢測長音符
            if (hasLongNote)
            {
                if (timeVal >= 0.15f)
                {
                    //顯示命中等級 (Great Perfect)
                    if (longNoteHitEffectGo.activeSelf)
                    {
                        gameController.ChangHitLevelSprite(2);
                        //CreateHitLongEffect();
                    }
                    timeVal = 0;
                }
                else
                {
                    timeVal += Time.deltaTime;
                }
            }
        }
        else if (Input.GetKeyUp(keyboardButton))
        {
            downVisual.SetActive(false);
            //檢測長音符
            if (hasLongNote)
            {
                longNoteHitEffectGo.SetActive(false);
                hitLongEffectGo.SetActive(false);
                CheckNoteHit();
            }
        }
    }

    public void Initialize(RhythmGameController controller)
    {
        gameController = controller;
        //hitLongEffectGo = gameController.GetFreshEffectObject(gameController.hitLongEffectObjectPool, gameController.hitLongNoteEffectGo);
        lanes = gameController.GetLanes();
    }

    //檢查事件與當前音軌是否匹配
    public bool DoesMatch(int noteID)
    {
        return noteID == laneID;
    }

    //如果匹配，則把當前事件添加進音軌所持有的事件列表
    public void AddEventToLane(KoreographyEvent evt)
    {
        laneEvents.Add(evt);
    }

    //音符在音譜上產生的位置
    int GetSpawnSampleOffset()
    {
        //出生位置與目標點位置
        float spawnDistToTarget = targetTopTrans.position.x - transform.position.x;

        //到達目標點的時間
        float spawnPosToTargetTime = spawnDistToTarget / gameController.noteSpeed;

        return (int)spawnPosToTargetTime * gameController.SampleRate;
    }

    //檢測是否生成下一個音符
    void CheckSpawnNext()
    {
        //音符到達目標的時間(偏移值)
        int samplesToTarget = GetSpawnSampleOffset();

        //當前音樂播放時間
        float currentTime = gameController.DelayedSampleTime;

        //當前音符數組小於數列長度 && 當前音符開始時間小於音樂播放時間
        while (pendingEventIdx < laneEvents.Count && laneEvents[pendingEventIdx].StartSample < currentTime + samplesToTarget)
        {
            KoreographyEvent evt = laneEvents[pendingEventIdx];
            int noteNum = evt.GetIntValue();
            NoteObject newObj = gameController.GetFreshNoteObject();
            bool isLongNoteStart = false;
            bool isLongNoteEnd = false;
            if (noteNum > lanes)
            {
                isLongNoteStart = true;
                noteNum = noteNum - lanes;
                if (noteNum > lanes)
                {
                    isLongNoteStart = false;
                    isLongNoteEnd = true;
                    noteNum = noteNum - lanes;
                }
            }
            //初始化下一個音符
            newObj.Initialize(evt, noteNum, this, gameController, isLongNoteStart, isLongNoteEnd);
            trackedNotes.Enqueue(newObj);
            pendingEventIdx++;
        }



    }

    //
    //生成特效的有關方法
    //
    /*
    void CreateDownEffect()
    {
        GameObject downEffectGo = gameController.GetFreshEffectObject(gameController.downEffectObjectPool, gameController.downEffectGo);
        downEffectGo.transform.position = targetVisuals.position;
    }

    void CreateHitEffect()
    {
        GameObject hitEffectGo = gameController.GetFreshEffectObject(gameController.hitEffectObjectPool, gameController.hitEffectGo);
        hitEffectGo.transform.position = targetVisuals.position;
    }

    void CreateHitLongEffect()
    {
        longNoteHitEffectGo.SetActive(true);
        hitLongEffectGo.SetActive(true);
        hitLongEffectGo.transform.position = targetVisuals.position;
    }   */

    //檢測是否有擊中音符對象
    //如果是，他將執行命中並刪除
    public void CheckNoteHit()
    {
        if (trackedNotes.Count > 0)
        {
            NoteObject noteObject = trackedNotes.Peek();
            if (noteObject.hitOffset > -6000)
            {
                trackedNotes.Dequeue();
                int hitLevel = noteObject.IsNoteHittable();
                gameController.ChangHitLevelSprite(hitLevel);
                if (hitLevel > 0)
                {
                    //播放打擊音效
                    SESource.clip = SEClip[1];
                    SESource.Play();
                    //擊中音符目標
                    //更新分數
                    gameController.UpdateScoreText (Mathf.FloorToInt(Mathf.Ceil(50000/(float)gameController.musicTatal) * hitLevel));
                    //產生擊中特效
                    if (noteObject.isLongNoteStart)
                    {
                        hasLongNote = true;
                        //CreateHitLongEffect();
                    }//關閉長音符
                    else if (noteObject.isLongNoteEnd)
                    {
                        hasLongNote = false;
                    }
                    else //生成一個打擊特效
                    {
                        //CreateHitEffect();
                    }

                    //增加combo
                    gameController.comboNum++;
                }
                else
                {
                    //播放打擊音效
                    SESource.clip = SEClip[0];
                    SESource.Play();
                    //未擊中
                    //減少玩家HP
                    gameController.UpdateHp();
                    //斷掉combo
                    gameController.HideComboNumText();
                    gameController.comboNum = 0;
                    gameController.missTatal += 1;
                }
                noteObject.OnHit();
            }
            else
            {
                //播放打擊音效
                SESource.clip = SEClip[0];
                SESource.Play();
                //CreateDownEffect();
            }
        }
        else//當線上沒有音符時
        {
            //播放打擊音效
            SESource.clip = SEClip[0];
            SESource.Play();
            //CreateDownEffect();
        }
    }

}
