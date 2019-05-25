using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evolution : MonoBehaviour {

    GameManager gameManager;
    public M animator_state;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Evolution_Button() {
        if (gameManager.playerStatusNo == 0){
            gameManager.playerStatusNo = 1;
            gameManager.Evolution_prefs();
            animator_state.StopAllCoroutines();
            animator_state.states = 1;
            animator_state.animator.Play("rest_magic");
            animator_state.coRou = null;
            animator_state.coRou2 = null;
            animator_state.Moving_target.position = animator_state.GetComponent<RectTransform>().position;
        }
        else if (gameManager.playerStatusNo == 1){
            gameManager.playerStatusNo = 2;
            gameManager.Evolution_prefs();
            animator_state.StopAllCoroutines();
            animator_state.states = 1;
            animator_state.animator.Play("rest_mushroom");
            animator_state.coRou = null;
            animator_state.coRou2 = null;
            animator_state.Moving_target.position = animator_state.GetComponent<RectTransform>().position;
        }
        else if (gameManager.playerStatusNo == 2) {
            gameManager.playerStatusNo = 0;
            gameManager.Evolution_prefs();
            animator_state.StopAllCoroutines();
            animator_state.states = 1;
            animator_state.animator.Play("rest");
            animator_state.coRou = null;
            animator_state.coRou2 = null;
            animator_state.Moving_target.position = animator_state.GetComponent<RectTransform>().position;
        }
    }
}
