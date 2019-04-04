using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class jump_p : MonoBehaviour {
    private Rigidbody2D player_rigi;
    public float jump_speed;
    private bool jump_state;

    private void Awake()
    {
        player_rigi = GameObject.Find("player").GetComponent<Rigidbody2D>();
        jump_state = false;
    }

    private void FixedUpdate()
    {
        if (jump_state) {
            jump();
        }
    }
    public void jump() {
        if (player_rigi.velocity.y != 0) { return; }
            player_rigi.AddForce(Vector3.up * jump_speed);
    }
    public void click(bool start) {
        jump_state = start;
    }
}
