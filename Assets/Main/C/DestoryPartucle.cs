using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestoryPartucle : MonoBehaviour {

    ParticleSystem p;

    RectTransform rectTrans;

    public Vector2 offset;

    // Use this for initialization
    void Start () {
        p = GetComponent<ParticleSystem>();
        rectTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 screenPos = Camera.main.ScreenToWorldPoint(rectTrans.position);
        transform.position = screenPos + offset;

        if (!p.isPlaying)
            Destroy(this.gameObject);
	}
}
