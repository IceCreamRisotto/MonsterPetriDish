using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mountain : MonoBehaviour {

    public Transform[] mountains;
    public float mountains_speed;
    public Transform limit_Left;
    public Transform limit_Right;

    void Update()
    {
        float map = (float)GameObject.Find("map").GetComponent<floor_move>().floor_speed/2.4f;
        mountains_speed = map;

        foreach (Transform i in mountains)
        {
            i.position = new Vector3(i.position.x - mountains_speed * Time.deltaTime, i.position.y, i.position.z);
            if (i.position.x <= limit_Left.position.x)
            {
                i.position = new Vector3(limit_Right.position.x, i.position.y, i.position.z);
            }
        }
    }
}
