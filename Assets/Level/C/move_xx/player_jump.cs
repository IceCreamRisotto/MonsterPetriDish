using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_jump : MonoBehaviour {

    private Rigidbody2D player_j;
    public float player_jump_s;
    private bool jumping = false;
    private bool continuous = false;

    private void Awake()
    {
        player_j = GameObject.Find("player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(continuous==true)
            jump();
    }

    public void jump() {
        if (player_j.velocity.y != 0) { return; }
            player_j.velocity = Vector3.up * player_jump_s * Time.deltaTime;


    }

    public void click_press(bool start) {
        continuous = start;
    }
}
