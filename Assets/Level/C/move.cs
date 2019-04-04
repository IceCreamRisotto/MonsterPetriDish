using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

    private bool RightMoveState;
    private bool LeftMoveState;
    public float move_speed;

    private Transform player_tran;

    void Awake() {
        RightMoveState = false;
        LeftMoveState = false;
        player_tran = GameObject.Find("player").GetComponent<Transform>();
    }

    void Update()
    {
        if (RightMoveState)
        {
            player_tran.position = new Vector3(player_tran.position.x + move_speed * Time.deltaTime, player_tran.position.y, player_tran.position.z);
            player_tran.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (LeftMoveState) {
            player_tran.position = new Vector3(player_tran.position.x - move_speed * Time.deltaTime, player_tran.position.y, player_tran.position.z);
            player_tran.localScale = new Vector3(-1f, 1f, 1f);
        }

    }

    public void goRight(bool start) {
        RightMoveState = start;
    }

    public void goLeft(bool start) {
        LeftMoveState = start;
    }
}
