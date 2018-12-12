using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {
    public string wallNo;
    //public int Stop;
    private Vector2 playerVector;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerVector = collision.transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(wallNo=="1")
        {
            collision.transform.position = new Vector2(playerVector.x,collision.transform.position.y);
        }
        else if(wallNo == "2")
        {
            collision.transform.position = new Vector2(collision.transform.position.x, playerVector.y);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
