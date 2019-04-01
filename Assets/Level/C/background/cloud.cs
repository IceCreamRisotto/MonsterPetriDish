using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud : MonoBehaviour {

    public Transform[] clouds;
    public float clouds_speed;
    public Transform limit_Left;
    public Transform limit_Right;
	
	void Update () {
        foreach (Transform i in clouds)
        {
            i.position = new Vector3(i.position.x - clouds_speed * Time.deltaTime, i.position.y, i.position.z);
            if (i.position.x <= limit_Left.position.x)
            {
                i.position = new Vector3(limit_Right.position.x, i.position.y, i.position.z);
            }
        }
    }

}
