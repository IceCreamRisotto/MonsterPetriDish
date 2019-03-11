using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infinite : MonoBehaviour {
    public float speed;
    private Vector2 offset;
    private float nowpos;

    // Use this for initialization
    void Start()
    {
        offset = new Vector2(0, 0);
        GetComponent<Image>().material.SetTextureOffset("_MainTex", offset);
    }

    // Update is called once per frame
    void Update()
    {
        nowpos += speed * Time.deltaTime;
        offset = new Vector2(nowpos, 0);
        GetComponent<Image>().material.SetTextureOffset("_MainTex", offset);
    }
}
