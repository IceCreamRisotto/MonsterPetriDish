﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour {

    private Transform me;
    private Transform oldMe;
    ItemMoveController itemMoveController;

	// Use this for initialization
	void Start () {
        itemMoveController = ItemMoveController.FindObjectOfType<ItemMoveController>();
        me = GetComponent<Transform>();
        //StartCoroutine(Move());
        itemMoveController.ItemMove(me);
    }

    /*IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            oldMe = me;
            me.position = new Vector2(oldMe.position.x - 0.01f, oldMe.position.y);
        }
    }*/

    // Update is called once per frame
    void Update () {
		
	}
}
