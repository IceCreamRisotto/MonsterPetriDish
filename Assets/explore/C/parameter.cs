using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class parameter : MonoBehaviour {
    public float floor_speed;
    public Transform left_lmt, right_lmt;

    public Animator animator;

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("move"))
        {
            animator.speed = floor_speed / 1.1f;
        }
        else {
            animator.speed = 1f;
        }
    }

    public void start()
    {
        floor_speed = 1f;
    }

    public void stop()
    {
        floor_speed = 0;
    }

    public void run()
    {
        floor_speed = 1.7f;
    }
}
