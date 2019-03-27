using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly_Prop : MonoBehaviour {

    private RectTransform playerPos;
    [Header("移動時間(約)")]
    public float smoothTime = 0.01f;
    private Vector3 m;
    [Header("幾秒後物件消失")]
    public float endTime = 1.5f;
    //public AnimationCurve curve;
    //float x;
    

	// Use this for initialization
	void Start () {
        playerPos = GameObject.Find("Player").GetComponent<RectTransform>();
        StartCoroutine(Endding());
	}
	
	// Update is called once per frame
	void Update () {
        //x = Time.deltaTime + x;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, playerPos.position, ref m, smoothTime);
        //Vector3 newPos = Vector3.Lerp(transform.position, playerPos.position, curve.Evaluate(x));
        transform.position = newPos;
        
	}

    IEnumerator Endding()
    {
        yield return new WaitForSeconds(endTime);
        Destroy(this.gameObject);
    }


}
