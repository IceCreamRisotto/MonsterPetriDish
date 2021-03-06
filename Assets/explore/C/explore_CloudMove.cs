﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explore_CloudMove : MonoBehaviour {
    public float speed;
    private Transform left_lmt, right_lmt;
    public GameObject floor;
    private void Awake()
    {
        left_lmt = GameObject.Find("背景移動").GetComponent<parameter>().left_lmt;
        right_lmt = GameObject.Find("背景移動").GetComponent<parameter>().right_lmt;
    }
    private void Update()
    {
        re_speed();
        move();
        lmt();
    }

    private void re_speed()
    {
        speed = floor.GetComponent<FloorMove>().speed / 2.5f;
    }

    void move()
    {
        transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
    }

    void lmt()
    {
        if (transform.position.x <= left_lmt.transform.position.x)
        {
            transform.position = new Vector3(right_lmt.position.x, transform.position.y, transform.position.z);
        }
    }
}
