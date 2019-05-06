using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rain : MonoBehaviour {

    public float speedX, speedY;
    public Transform button;
    public Transform[] startPosition;

    void Update () {
        down();
        reset();
	}

    void down() {
        gameObject.transform.position += new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime, 0);
    }

    void reset() {
        if (gameObject.transform.position.y <= button.position.y) {
            int i=UnityEngine.Random.Range(0, startPosition.Length);
            gameObject.transform.position = new Vector3(startPosition[i].position.x, startPosition[i].position.y, gameObject.transform.position.z);
        }
    }
}
