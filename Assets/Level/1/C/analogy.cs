using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class analogy : MonoBehaviour {
    public RectTransform parent;
    public RectTransform handle;
    public Transform player;
    private bool dragging = false;
    [SerializeField]
    private float speed=0.2f;
    private Animator animator;

    private void Awake()
    {
        animator =GameObject.Find("player").GetComponent<Animator>();
    }

    private void Update()
    {
        Drag();
    }

    private void StartDrag() {
        dragging = true;
        animator.SetBool("run", true);
    }

    private void Drag() {
        if (!dragging) { return; }
        Vector2 newPos = new Vector2(0f,0f);
        //UI的mouse座標轉成local座標
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, Input.mousePosition, null, out newPos);
        Vector2 result = Vector2.ClampMagnitude(newPos,42.5f);
        handle.anchoredPosition = result;

        state(result);
        animator.speed = result.magnitude/42.5f;
        Vector3 move = new Vector3(result.x, result.y, 0f);
        player.Translate(move * Time.deltaTime * speed);
    }

    private void EndDrag() {
        dragging = false;
        handle.anchoredPosition = Vector2.zero;
        animator.SetBool("run", false);
        animator.speed = 1f;
    }

    private void state(Vector2 result) {
        if (result.x > 0f) 
            player.transform.localScale = new Vector3(1f,1f,1f);
        else
            player.transform.localScale = new Vector3(-1f, 1f, 1f);
    }
}
