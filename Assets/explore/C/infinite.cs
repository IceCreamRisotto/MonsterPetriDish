using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infinite : MonoBehaviour {
    public float speed;
    private Vector2 offset;
    private float nowpos;
    public bool start_infinite;

    private void Awake()
    {
        start_infinite = true;
        offset = new Vector2(0, 0);
        GetComponent<Image>().material.SetTextureOffset("_MainTex",offset);
    }

    private void Update()
    {
        if (start_infinite == true) {
            nowpos += speed * Time.deltaTime;
            offset = new Vector2(nowpos, 0);
            GetComponent<Image>().material.SetTextureOffset("_MainTex", offset);
        }
    }
    void stopInfinite() {
        start_infinite = false;
    }
    void startInfinite() {
        start_infinite = true;
    }
}
