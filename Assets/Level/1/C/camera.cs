using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

    public GameObject obj;
    public float speed;
    public Vector2 clamp;

    private void Start()
    {
        speed = 0.1f;
        clamp =new Vector2(0f,90f);
    }

    private void Update()
    {
        Vector2 objPos = (Vector2)obj.transform.position;
        Vector2 cameraPos = (Vector2)gameObject.transform.position;
        Vector2 vv = objPos - cameraPos;
        if (vv.magnitude >= 0.1f) {
            cameraPos = Vector2.Lerp(cameraPos, objPos, speed);
            gameObject.transform.position = new Vector3(Mathf.Clamp(cameraPos.x,clamp.x,clamp.y), cameraPos.y,-10);
        }
    }
}
