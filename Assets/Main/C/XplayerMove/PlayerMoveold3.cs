using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float speed;
    public float waitTime;

    public RectTransform player;
    public RectTransform Moving_target;

    public float distance;//位移距離
    private float x_move;
    private float y_move;

    private int states;

    public bool clicked;

    private Animator animator;

    private Coroutine coRou=null;
    private Coroutine coRou2 = null;

    public GameObject playerPicture;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        speed = 0.5f;
        waitTime = 2f;
        states = 1;
        clicked = false;
    }

    private void Update(){
        if (!clicked)
        {
            if (coRou == null)
                coRou = StartCoroutine(wait_time());

            switch (states)
            {
                case 0:
                    Debug.Log(states +"click");
                    break;
                case 1:
                    Debug.Log(states + "rest");
                    Wait();
                    break;
                case 2:
                    Debug.Log(states + "move");
                    Move();
                    break;
                case 3:
                    Debug.Log(states + "sleep");
                    sleep();
                    break;
            }
        }
    }

    IEnumerator wait_time()
    {
        yield return new WaitForSeconds(waitTime);
        random();
        coRou = null;
    }
    void random() {
        states = UnityEngine.Random.Range(1,4);

        animator.SetInteger("states",states);

        if (states == 2) {
            x_move = Random.Range(-distance, distance);
            y_move = Random.Range(-distance, distance);
            Moving_target.position = player.position + new Vector3(x_move, y_move, 0);
        }
    }

    public void Wait() {

    }

    public void Move(){
        //Debug.Log(x_move);
        if (x_move > 0f){
            playerPicture.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else {
            playerPicture.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        player.position = Vector2.MoveTowards(player.position,Moving_target.position,speed);
    }

    public void clickButton() {
        clicked = true;
        StopCoroutine(coRou);
        Debug.Log("click0");
        animator.SetInteger("states",0);
        if (coRou2 == null)
            coRou2=StartCoroutine(OnCoroutine());
        else
        {
            StopCoroutine(coRou2);
            coRou2 = StartCoroutine(OnCoroutine());
        }
    }
    IEnumerator OnCoroutine()
    {
        yield return new WaitForSeconds(waitTime);
        coRou = null;
        clicked = false;
    }


    public void sleep() {

    }
}
