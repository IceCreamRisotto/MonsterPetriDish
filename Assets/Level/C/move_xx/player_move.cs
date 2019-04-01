using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour {
    public float runspeed;
    private Transform player_tran;
    private bool continuous_r = false;
    private bool continuous_l = false;

    private void Awake()
    {
        player_tran = GameObject.Find("player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (continuous_r) {
            move_r();
        }
        if (continuous_l)
        {
            move_l();
        }
    }

    void move_r() {
        player_tran.position = new Vector3(player_tran.position.x + runspeed * Time.deltaTime, player_tran.position.y, player_tran.position.z);
        player_tran.localScale = new Vector3(1f, 1f, 1f);
    }

    void move_l() {
        player_tran.position = new Vector3(player_tran.position.x - runspeed * Time.deltaTime, player_tran.position.y, player_tran.position.z);
        player_tran.localScale = new Vector3(-1f, 1f, 1f);
    }

    public void click_press_r(bool start)
    {
        continuous_r = start;
    }

    public void click_press_l(bool start2)
    {
        continuous_l = start2;
    }

}
