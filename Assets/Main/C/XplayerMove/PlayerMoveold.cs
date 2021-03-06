﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveold : MonoBehaviour {
    public float speed;
    [Header("等待後有機會移動")]
    public float waitTime;
    private bool Imove = false;
    private Vector3 mo;
    private float x_move;
    private float y_move;
    private Vector2 X_Moving_limit=new Vector2(-2.6f, 2.6f);//X移動限制
    private Vector2 Y_Moving_limit=new Vector2(-3.5f, 3.3f);//Y移動限制

    private void Start()
    {
        
    }

    void Update()
    {
        if (!Imove)
        {//移動距離亂數
            Imove = true;
            StartCoroutine(wait_time());
        }
        moving();
    }

    IEnumerator wait_time()
    {
        yield return new WaitForSeconds(waitTime);
        random();
    }

    void random()
    {
        x_move = Random.Range(-2.5f,2.5f);
        y_move = Random.Range(-2.5f,2.5f);
        mo =transform.position + new Vector3(x_move,y_move,0);
        Imove = false;
    }

    void moving(){
        transform.position = Vector2.MoveTowards(transform.position,mo,speed);
        transform.position =new Vector2(Mathf.Clamp(transform.position.x, X_Moving_limit.x, X_Moving_limit.y), Mathf.Clamp(transform.position.y, Y_Moving_limit.x, Y_Moving_limit.y));//限制範圍
    }

}
