using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using UnityEngine.UI;
using SonicBloom.Koreo.Players;
using UnityEngine.SceneManagement;

public class RhythmGameController : MonoBehaviour {

    [Tooltip("用於目標生成的軌道的事件對應ID")]
    [EventID]
    public string eventID;

    //音符速度
    public float noteSpeed = 1;

    [Tooltip("音符命中區間窗口(音符被命中的難度,單位ms)")]
    [Range(8f, 300f)]
    public float hitWindowsRangeInMS = 300f;

    //音樂樣本中的命中窗口
    int hitWindowRangeInSamples;

    //音符對象池
    //音符
    Stack<NoteObject> noteObjectPool = new Stack<NoteObject>();

    //按下特效
    public Stack<GameObject> downEffectObjectPool = new Stack<GameObject>();

    //擊中音符特效
    public Stack<GameObject> hitEffectObjectPool = new Stack<GameObject>();

    //擊中長音符特效
    public Stack<GameObject> hitLongEffectObjectPool = new Stack<GameObject>();

    //預製體資源
    //音符
    public NoteObject noteObject;
    //按下特效
    public GameObject downEffectGo;
    //擊中音符特效
    public GameObject hitEffectGo;
    //擊中長音符特效
    public GameObject hitLongNoteEffectGo;


    //引用
    Koreography playingKoreo;

    public AudioSource audioCom;

    public List<LaneController> noteLanes = new List<LaneController>();

    SimpleMusicPlayer simpleMusicPlayer;

    public Transform simpleMusicPlayerTrans;

    //其他
    [Tooltip("開始播放音頻之前提供的時間量,單位s")]
    public float leadInTime;
    //音頻播放之前的剩餘時間量
    float leadInTimeLeft;
    //音樂開始播放的倒計時器
    float timeLeftToPlay;

    //給星星前導時間
    public float StarLeadInTime
    {
        get
        {
            return leadInTime;
        }
    }

    //當前採樣時間，包含延遲呼叫
    public float DelayedSampleTime
    {
        get
        {
            return playingKoreo.GetLatestSampleTime() - SampleRate * leadInTimeLeft;
        }
    }

    //以unity為單位來訪問當前命中窗口大小
    public float WindowSizeInUnits
    {
        get
        {
            return noteSpeed * (hitWindowsRangeInMS * 0.001f);
        }
    }

    //獲取音樂樣本中的命中窗口
    public int HitWindowSampleWidth
    {
        get
        {
            return hitWindowRangeInSamples;
        }
    }

    //獲取音頻採樣率
    public int SampleRate
    {
        get
        {
            return playingKoreo.SampleRate;
        }
    }

    float hideHitLevelImageTimeVal;

    public int comboNum;

    public int score;

    public int hp = 10;

    public bool isPauseState;

    bool gameStart;

    //UI
    public Slider slider;

    public Text scoreText;

    public Image hitLevelImage;
    public Animator hitLevelImageAnim;

    public Text comboText;
    public Animator comboTextAnim;

    public GameObject gameOverUI;

    //資源
    public Sprite[] hitLevelSprites;

    public Koreography kgy;

    // Use this for initialization
    void Start () {
        InitializeLeadIn();

        simpleMusicPlayer = simpleMusicPlayerTrans.GetComponent<SimpleMusicPlayer>();
        simpleMusicPlayer.LoadSong(kgy, 0, false);

        for (int i = 0; i < noteLanes.Count; i++)
        {
            noteLanes[i].Initialize(this);
        }

        //採取到koreograpy對象
        playingKoreo = Koreographer.Instance.GetKoreographyAtIndex(0);
        //獲取事件軌跡
        KoreographyTrackBase rhythmTrack = playingKoreo.GetTrackByID(eventID);
        //獲取事件
        List<KoreographyEvent> rawEvents = rhythmTrack.GetAllEvents();

        for (int i = 0; i < rawEvents.Count; i++)
        {
            KoreographyEvent evt = rawEvents[i];
            int noteID = evt.GetIntValue();

            //編列所有音軌
            for (int j = 0; j < noteLanes.Count; j++)
            {
                LaneController lane = noteLanes[j];
                if (noteID > 6)
                {
                    noteID = noteID - 6;
                    if (noteID > 6)
                    {
                        noteID = noteID - 6;
                    }

                }
                if (lane.DoesMatch(noteID))
                {
                    lane.AddEventToLane(evt);
                    break;
                }
            }
        }
        //打擊區間=速度(音頻採樣率)*時間
        hitWindowRangeInSamples = (int)(SampleRate * hitWindowsRangeInMS * 0.001f);
    }
	
	// Update is called once per frame
	void Update () {
        if (timeLeftToPlay > 0)
        {
            timeLeftToPlay -= Time.unscaledDeltaTime;

            if (timeLeftToPlay <= 0)
            {
                audioCom.Play();
                timeLeftToPlay = 0;
            }
        }

        //倒數引導時間
        if (leadInTimeLeft > 0)
        {
            leadInTimeLeft = Mathf.Max(leadInTimeLeft - Time.unscaledDeltaTime, 0);
        }
    }

    //(初始化)前導時間結束，播放音樂
    void InitializeLeadIn()
    {
        if (leadInTime > 0)
        {
            leadInTimeLeft = leadInTime;
            timeLeftToPlay = leadInTime;
        }
        else
        {
            audioCom.Play();
        }
    }

    //
    //有關對象池
    //
    //從音符池中取對象的方法
    public NoteObject GetFreshNoteObject()
    {
        NoteObject retObj;

        if (noteObjectPool.Count > 0)
        {
            retObj = noteObjectPool.Pop();
        }
        else
        {
            //資源源
            retObj = Instantiate(noteObject);
        }

        retObj.gameObject.SetActive(true);
        retObj.enabled = true;

        return retObj;
    }

        //把音符對象放回對象池
        public void ReturnNoteObjectToPool(NoteObject obj)
        {
            if (obj != null)
            {
                obj.transform.position = noteObject.gameObject.transform.position;
                obj.enabled = false;
                obj.gameObject.SetActive(false);
                noteObjectPool.Push(obj);
            }
        }

    //從特效池中取對象的方法(GameObejct)
    public GameObject GetFreshEffectObject(Stack<GameObject> stack, GameObject effectObject)
    {
        GameObject effectGo;

        if (stack.Count > 0)
        {
            effectGo = stack.Pop();
        }
        else
        {
            effectGo = Instantiate(effectObject);
        }

        effectGo.SetActive(true);

        return effectGo;
    }

    //將特效動畫放回特效池
    public void ReturnEffectGoToPool(GameObject effectGo, Stack<GameObject> stack)
    {
        if (effectGo != null)
        {
            effectGo.gameObject.SetActive(false);
            stack.Push(effectGo);
        }
    }

    //顯示命中等級對應的圖片
    public void ChangHitLevelSprite(int hitLevel)
    {
        hideHitLevelImageTimeVal = 1;
        hitLevelImage.sprite = hitLevelSprites[hitLevel];
        hitLevelImage.SetNativeSize();
        //hitLevelImageAnim.SetTrigger("hit");
        hitLevelImageAnim.Play("UIAnimation", 0, 0);
        hitLevelImage.gameObject.SetActive(true);
        if (comboNum >= 5)
        {
            comboText.gameObject.SetActive(true);
            comboText.text = comboNum.ToString();
            comboTextAnim.Play("UIAnimation", 0, 0);
        }
    }

    //隱藏打擊判定UI
    private void HideHitLevelImage()
    {
        hitLevelImage.gameObject.SetActive(false);
    }

    //隱藏comboUI
    public void HideComboNumText()
    {
        comboText.gameObject.SetActive(false);
    }

    //分數更新
    public void UpdateScoreText(int addNum)
    {
        score += addNum;
        scoreText.text = score.ToString();
    }

    //血量更新
    public void UpdateHp()
    {
        hp = hp - 2;
        slider.value = (float)hp / 10;
        if (hp == 0)
        {
            isPauseState = true;
            simpleMusicPlayer.Pause();
            gameOverUI.SetActive(true);
        }
    }

    //音樂暫停
    public void PauseMusic()
    {
        if (!gameStart)
        {
            return;
        }
        simpleMusicPlayer.Pause();
    }

    //音樂播放
    public void PlayMusic()
    {
        simpleMusicPlayer.Play();
    }

    //重玩
    public void RePlay()
    {
        SceneManager.LoadScene(1);
    }

    //返回主畫面
    public void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }
}
