using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class parameter : MonoBehaviour {
    public float floor_speed;
    public Transform left_lmt, right_lmt;

    public Animator player;

    private void Update()
    {
        player.speed = floor_speed /1.1f;
    }

}
