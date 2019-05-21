using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class parameter : MonoBehaviour {
    public float floor_speed;
    public Transform left_lmt, right_lmt;

    public Animator animator;

    public Flowchart flowchart;
    public int exploreBackground {
        get { return flowchart.GetIntegerVariable("scence"); }
        set { flowchart.SetIntegerVariable("scence", value); }
    }

    private void Start()
    {
        exploreBackground = GameObject.Find("通用事件管理").GetComponent<GameManager>().explorationNumber;
    }

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
