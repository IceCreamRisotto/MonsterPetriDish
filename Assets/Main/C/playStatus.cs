using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playStatus : MonoBehaviour {

    public RectTransform player;
    public RectTransform Moving_target;

    public Animator animator;
    public Coroutine coRou = null;//sleep
    public Coroutine coRou2 = null;//idel和移動

    public float speed;
    public float waitTime;
    public float distance;//位移距離
    public float longTime;//等太久睡覺

    private float x_move;
    private float y_move;

    public int states;

    public Transform turn;//角色
    public Transform turn_face;//zzz
    public float v3=0.5f;

    public Transform lim_UpperRight;
    public Transform lim_BottomLeft;

    public string[] Switch_animator;

    GameManager gameManager;

    [Header("進化按鈕")]
    public GameObject EvolutionButton;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        states = 1;
    }

    void Start()
    {
        //Debug.Log(Switch_animator[PlayerPrefs.GetInt("playerStatusNo")]);
        animator.Play(Switch_animator[PlayerPrefs.GetInt("playerStatusNo")]);
    }

    private void Update()
    {
        Evolution_Button();//是否顯示進化按鈕
        click_off();
        if (coRou == null) {//等太久會睡覺
            coRou = StartCoroutine(sleep());
        }

        switch (states) {
            case 0:
                //Debug.Log(states);
                break;
            case 1:
                //Debug.Log(states);
                if (coRou2 == null)//idel
                    coRou2 = StartCoroutine(rest());
                break;
            case 2:
                //Debug.Log(states);
                if (coRou2 == null)//移動
                    coRou2 = StartCoroutine(wait_move());
                Move();
                break;
            case 3:
                //Debug.Log(states);
                break;
        }
    }

    void Move() {
        
        if (x_move > 0f)
        {
            turn.transform.localScale = new Vector3(-v3, v3, v3);
        }
        else
        {
            turn.transform.localScale = new Vector3(v3, v3, v3);
        }

        float x_abs, y_abs;
        x_abs = Mathf.Abs(player.position.x - Moving_target.position.x);
        y_abs = Mathf.Abs(player.position.y - Moving_target.position.y);

        if (x_abs < 0.1f && y_abs < 0.1f)
        {
            animator.SetInteger("states", 1);
        }
        player.position = Vector2.MoveTowards(player.position, Moving_target.position, speed);
    }


    IEnumerator rest() {
        animator.SetInteger("states", states);
        yield return new WaitForSeconds(waitTime);//後面換wait_move
        states = 2;
        coRou2 = null;
    }

    IEnumerator wait_move() {
        animator.SetInteger("states",states);
        x_move = Random.Range(-distance, distance);
        y_move = Random.Range(-distance, distance);
        Moving_target.position = player.position + new Vector3(x_move, y_move, 0f);
        //Debug.Log("1"+Moving_target.position);

        if (Moving_target.position.x <= lim_BottomLeft.position.x || Moving_target.position.x >= lim_UpperRight.position.x)//限制範圍
        {
            x_move = -x_move;
            Moving_target.position = Moving_target.position + new Vector3(2*x_move,0,0);
            Moving_target.position = new Vector3(Mathf.Clamp(Moving_target.position.x, lim_BottomLeft.position.x, lim_UpperRight.position.x), Moving_target.position.y, 0f);
            //Debug.Log("2" + Moving_target.position);
        }
        if (Moving_target.position.y <= lim_BottomLeft.position.y || Moving_target.position.y >= lim_UpperRight.position.y)
        {
            y_move = -y_move;
            Moving_target.position = Moving_target.position + new Vector3(0, 2 * y_move, 0);
            Moving_target.position = new Vector3(Moving_target.position.x,Mathf.Clamp(Moving_target.position.y, lim_BottomLeft.position.y, lim_UpperRight.position.y),0f);
            //Debug.Log("3" + Moving_target.position);
        }

        yield return new WaitForSeconds(waitTime);//前面是要先執行的(移動) //後面是要換rest
        states = 1;
        coRou2 = null;
    }

    IEnumerator sleep()
    {
        yield return new WaitForSeconds(longTime);
        StopCoroutine(coRou2);
        coRou = null;
        states = 3;
        zzz_dir();
        animator.SetInteger("states", states);
    }

    void zzz_dir() {
        if (turn.transform.localScale.x > 0f)
        {
            turn_face.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else {
            turn_face.transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        }
    }

    public void click() {
        StopAllCoroutines();
        states = 0;
        animator.SetInteger("states", states);
    }

    public void click_off() {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("click")||
            animator.GetCurrentAnimatorStateInfo(0).IsName("click_magic") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("click_mushroom")) {

            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

            if (info.normalizedTime >= 1.0f) {
                coRou = null;
                coRou2 = null;
                states = 1;
                animator.SetInteger("states", states);
            }
        }
    }

    public void eatWakeUp() {
        if (animator.GetInteger("states") == 3)
        {
            click();
        }
        else {
            StopCoroutine(coRou);
            coRou = null;
        }
    }


    //判斷是否跳出進化按鈕
    public void Evolution_Button()
    {
        if (gameManager.playerExpManager[0] >= gameManager.level && gameManager.playerStatusNo == 0 &&
            (gameManager.items[13] >= 1 || gameManager.items[14] >= 1))
        {
            EvolutionButton.SetActive(true);
        }
        else
            EvolutionButton.SetActive(false);
    }
}
