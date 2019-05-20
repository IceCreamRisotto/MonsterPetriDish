using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDAnim : MonoBehaviour {

    RectTransform rectTransform;

    float i=0;

    [Header("旋轉速度")]
    public float speed;

    [Header("幾秒轉一次")]
    public float second;

	// Use this for initialization
	void Awake () {
        rectTransform = GetComponent<RectTransform>();
    }

    public void CDPlay()
    {
        StartCoroutine(TransferCD());
    } 

    IEnumerator TransferCD()
    {
        while (true)
        {
            if (i > 360)
                i = 0;
            i = i + speed;
            rectTransform.transform.rotation = Quaternion.Euler(0f, 0f, i);
            yield return new WaitForSeconds(second);
        }
    }
}
