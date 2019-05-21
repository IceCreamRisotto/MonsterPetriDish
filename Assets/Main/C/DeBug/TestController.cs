using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour {

    GameManager gameManager;

	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TestItemGet()
    {
        for(int i=0;i>gameManager.items.Length;i++)
        {
            gameManager.items[i] += 10;
        }
    }

}
