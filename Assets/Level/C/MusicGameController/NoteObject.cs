using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class NoteObject : MonoBehaviour {

    public SpriteRenderer visuals;

    public Sprite[] noteSprites;

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
        UpdatePosition();
        GetHitOffset();

        if (transform.position.z <= laneController.targetBottomTrans.position.z)
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
            spriteNum += 6;
        }
        else if (isLongNoteEnd)
        {
            spriteNum += 12;
        }

        visuals.sprite = noteSprites[spriteNum - 1];
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
        Vector3 pos = laneController.TargetPosition;

        pos.z -= (gameController.DelayedSampleTime - trackEvent.StartSample) / (float)gameController.SampleRate * gameController.noteSpeed;

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
            if (hitOffset >= 4500 && hitOffset <= 7500)
            {
                hitLevel = 2;
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
