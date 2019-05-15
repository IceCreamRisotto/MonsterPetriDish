using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class NoteObject : MonoBehaviour {

    int lanes;

    //public Transform TopTarget;

    public Animator visuals;

    //public Animator[] noteSprites;

    KoreographyEvent trackEvent;

    public bool isLongNoteStart;

    public bool isLongNoteEnd;

    LaneController laneController;

    RhythmGameController gameController;

    public int hitOffset;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //暫停
        if(gameController!=null)
        if (gameController.isPauseState)
        {
            return;
        }

        if (laneController!=null)
        UpdatePosition();
        if(gameController!=null)
        GetHitOffset();
        if (laneController != null)
            if (transform.position.x <= laneController.targetBottomTrans.position.x)
            {
                gameController.ReturnNoteObjectToPool(this);
                ResetNote();
            }
    }

    //初始化方法
    public void Initialize(KoreographyEvent evt, int noteNum, LaneController laneCont,
    RhythmGameController gameCont, bool isLongStart, bool isLongEnd)
    {
        trackEvent = evt;
        laneController = laneCont;
        gameController = gameCont;
        isLongNoteStart = isLongStart;
        isLongNoteEnd = isLongEnd;
        int spriteNum = noteNum;

        if (isLongNoteStart)
        {
            spriteNum += lanes;
        }
        else if (isLongNoteEnd)
        {
            spriteNum += lanes+lanes;
        }

        visuals.SetInteger("Stly", spriteNum);
        //noteSprites[spriteNum - 1].SetInteger("Stly", spriteNum);

        lanes=gameController.GetLanes();
    }

    //將note對象重置
    void ResetNote()
    {
        trackEvent = null;
        laneController = null;
        gameController = null;
    }

    //返回對象池
    void RetuenToPool()
    {
        visuals.SetInteger("attack", 1);
        gameController.ReturnNoteObjectToPool(this);
        ResetNote();
    }

    //擊中音符對象
    public void OnHit()
    {
        RetuenToPool();
    }

    //音符移動
    void UpdatePosition()
    {
        Vector2 pos = laneController.TargetPosition;

        pos.x -= (gameController.DelayedSampleTime - trackEvent.StartSample) / (float)gameController.SampleRate * gameController.noteSpeed;

        transform.position = pos;
    }

    //計算音符偏移值
    void GetHitOffset()
    {
        float curTime = gameController.DelayedSampleTime;
        int noteTime = trackEvent.StartSample;
        int hitWindow = gameController.HitWindowSampleWidth;
        hitOffset = hitWindow - Mathf.Abs(noteTime - (int)curTime);
    }

    //當前音服是否已經Miss
    public bool isNoteMissed()
    {
        bool bMissed = true;
        if (enabled)
        {
            float curTime = gameController.DelayedSampleTime;
            int noteTime = trackEvent.StartSample;
            int hitWindow = gameController.HitWindowSampleWidth;

            bMissed = curTime - noteTime > hitWindow;
        }
        return bMissed;
    }

    //打擊音符判定計算
    public int IsNoteHittable()
    {
        int hitLevel = 0;
        if (hitOffset >= 0)
        {
            if (hitOffset >= 5000 && hitOffset <= 9000)
            {
                hitLevel = lanes;
            }
            else
            {
                hitLevel = 1;
            }
        }
        else
        {
            this.enabled = false;
        }

        return hitLevel;
    }
}
