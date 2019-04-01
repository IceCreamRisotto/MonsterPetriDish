using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_grass : MonoBehaviour {

    public Transform[] background;
    public float background_speed;
    public Transform limit_Left;
    public Transform limit_Right;
    
    void Update()
    {
        foreach (Transform i in background)
        {
            i.position = new Vector3(i.position.x - background_speed * Time.deltaTime, i.position.y, i.position.z);
            if (i.position.x <= limit_Left.position.x)
            {
                i.position = new Vector3(limit_Right.position.x, i.position.y, i.position.z);
            }
        }
    }
}
