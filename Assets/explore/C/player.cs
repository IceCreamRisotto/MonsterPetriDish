using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour {

    private Animator animator;
    public GameObject Dialog;
    public GameObject content;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void di()
    {
        Dialog.SetActive(true);
    }

    public void run() {
        animator.SetInteger("state", 0);
    }
    public void stop() {
        animator.SetInteger("state",1);
    }

}
