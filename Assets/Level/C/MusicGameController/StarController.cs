using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class StarController : MonoBehaviour
{

    BackGroundController gameController;

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
    Queue<StarObject> trackedNotes = new Queue<StarObject>();

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

    //長音符變數
    public bool hasLongNote;
    public float timeVal = 0;
    public GameObject longNoteHitEffectGo;
    GameObject hitLongEffectGo;

    // Use this for initialization
    void Start()
    {

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
    void Update()
    {

        //清除無效音符
        while (trackedNotes.Count > 0 && trackedNotes.Peek().isNoteMissed())
        {
            if (trackedNotes.Peek().isLongNoteEnd)
            {
                hasLongNote = false;
                timeVal = 0;
            }
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

    public void Initialize(BackGroundController controller)
    {
        gameController = controller;
        //hitLongEffectGo = gameController.GetFreshEffectObject(gameController.hitLongEffectObjectPool, gameController.hitLongNoteEffectGo);
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
            StarObject newObj = gameController.GetFreshStarObject();
            bool isLongNoteStart = false;
            bool isLongNoteEnd = false;
            if (noteNum > 6)
            {
                isLongNoteStart = true;
                noteNum = noteNum - 6;
                if (noteNum > 6)
                {
                    isLongNoteStart = false;
                    isLongNoteEnd = true;
                    noteNum = noteNum - 6;
                }
            }
            //初始化下一個音符
            newObj.Initialize(evt, noteNum, this, gameController, isLongNoteStart, isLongNoteEnd);
            trackedNotes.Enqueue(newObj);
            pendingEventIdx++;
        }



    }

    //檢測是否有擊中音符對象
    //如果是，他將執行命中並刪除
    public void CheckNoteHit()
    {
        if (trackedNotes.Count > 0)
        {
            StarObject noteObject = trackedNotes.Peek();
            if (noteObject.hitOffset > -6000)
            {
                trackedNotes.Dequeue();
                int hitLevel = noteObject.IsNoteHittable();
                if (hitLevel > 0)
                {
                    //擊中音符目標
                    //更新分數
                    //產生擊中特效
                    if (noteObject.isLongNoteStart)
                    {
                        hasLongNote = true;
                        //CreateHitLongEffect();
                    }
                    else if (noteObject.isLongNoteEnd)
                    {
                        hasLongNote = false;
                    }
                    else
                    {
                        //CreateHitEffect();
                    }

                    //增加combo
                }
                else
                {
                    //未擊中
                    //減少玩家HP
                    //斷掉combo
                }
                noteObject.OnHit();
            }
            else
            {
                //CreateDownEffect();
            }
        }
    }

}
