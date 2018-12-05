using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudTrans : MonoBehaviour {

    public Transform[] CloudPosition;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("cloud"))
        {
            Transform oldVector = collision.transform;
            collision.transform.position = new Vector2(5,oldVector.position.y);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
