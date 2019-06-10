using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorCtrl : MonoBehaviour {
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void narmalMove() {
        animator.speed = 1f;
    }

    public void runMove() {
        animator.speed = 1.6f;
    }
}
