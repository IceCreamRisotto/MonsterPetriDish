using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly_Prop : MonoBehaviour
{

    private RectTransform playerPos;
    [Header("移動時間(約)")]
    public float smoothTime = 0.01f;
    private Vector3 m;
    //public AnimationCurve curve;
    //float x;


    // Use this for initialization
    void Start()
    {
        playerPos = GameObject.Find("Player").GetComponent<RectTransform>();
        //StartCoroutine(Endding());
    }

    // Update is called once per frame
    void Update()
    {
        //x = Time.deltaTime + x;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, playerPos.position, ref m, smoothTime);
        //Vector3 newPos = Vector3.Lerp(transform.position, playerPos.position, curve.Evaluate(x));
        transform.position = newPos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "eatTag")
        {
            Destroy(this.gameObject);
        }
    }

    /*IEnumerator Endding()
    {
        yield return new WaitForSeconds(endTime);
        Destroy(this.gameObject);
    }*/


}
