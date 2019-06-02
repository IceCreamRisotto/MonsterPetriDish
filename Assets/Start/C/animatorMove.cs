using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorMove : MonoBehaviour {

    Animator animator;
    int evolution;
    public string[] Switch_animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start () {
        evolution = PlayerPrefs.GetInt("playerStatusNo");
        change();
	}

    void change() {
        animator.Play(Switch_animator[evolution]) ;
    }

}
