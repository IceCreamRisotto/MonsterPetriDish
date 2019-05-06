using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class propMove : MonoBehaviour {

    public RectTransform player;
    public RectTransform po;
    public float speed;
    bool state = false;
    player play;
    infinite inf;

    private void Awake()
    {
        play=GameObject.Find("player").GetComponent<player>();
        inf= GameObject.Find("背景卷軸").GetComponent<infinite>();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            play.run();
            state = true;
        }
        move();
    }
    private void move()
    {
        if (state)
        {
            if (player.transform.position.x >= po.transform.position.x)
            {
                player.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                play.stop();
                play.di();
                inf.start_infinite = false;
                state = false;
            }
        }

    }
}
