using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_player_evolution : MonoBehaviour {
    GameManager gameManager;
    public string[] Switch_animator;
    Animator animator;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        animator.Play(Switch_animator[gameManager.playerStatusNo]);
    }
}
