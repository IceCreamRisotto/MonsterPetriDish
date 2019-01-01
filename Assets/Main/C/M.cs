using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M : MonoBehaviour {

    public RectTransform player;
    public RectTransform Moving_target;

    private Animator animator;
    private Coroutine coRou = null;
    private Coroutine coRou2 = null;

    public float speed;
    public float waitTime;
    public float distance;//位移距離
    public float longTime;//等太久睡覺

    private float x_move;
    private float y_move;

    private int states;

    public Transform turn;

    public Transform lim_UpperRight;
    public Transform lim_BottomLeft;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        states = 1;
    }

    private void Update()
    {
        if (coRou==null) {//等太久會睡覺
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
                Debug.Log(states);
                break;
        }
    }

    void Move() {
        if (x_move > 0f)
        {
            turn.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            turn.transform.localScale = new Vector3(1f, 1f, 1f);
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
        animator.SetInteger("states", states);
    }

    public void click() {
        StopAllCoroutines();
        states = 0;
        animator.SetInteger("states", states);
    }

    public void click_off() {
        coRou = null;
        coRou2 = null;
        states = 1;
        animator.SetInteger("states", states);
    }



}
