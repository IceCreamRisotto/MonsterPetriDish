using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evolution : MonoBehaviour {

    GameManager gameManager;
    public M animator_state;
    public string[] Switch_animator;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Evolution_state();
    }

    public void Evolution_Button() {
        gameManager.playerStatusNo = (gameManager.playerStatusNo+1) % Switch_animator.Length;       //test
        gameManager.Evolution_prefs();//進化儲存
        Evolution_state();
    }

    //進化狀態chick
    void Evolution_state() {
        animator_state.StopAllCoroutines();
        animator_state.states = 1;
        animator_state.animator.Play(Switch_animator[gameManager.playerStatusNo]);
        animator_state.coRou = null;
        animator_state.coRou2 = null;
        animator_state.Moving_target.position = animator_state.GetComponent<RectTransform>().position;
    }
}
