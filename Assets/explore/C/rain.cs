using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rain : MonoBehaviour {

    public float speedX, speedY;
    RectTransform resetPosition;
    public RectTransform button;

    private void Awake()
    {
        resetPosition = gameObject.GetComponent<RectTransform>();
    }

    void Update () {
        down();
        reset();
	}

    void down() {
        gameObject.transform.position += new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime, 0);
    }

    void reset() {
        if (gameObject.transform.position.y <= button.transform.position.y) {
            gameObject.transform.position = new Vector3(resetPosition.position.x, resetPosition.position.y, resetPosition.position.z);
        }
    }
}
