using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor_move : MonoBehaviour {

    public GameObject[] floor;
    public float floor_speed;
    public Transform limit_Left;
    public Transform limit_Right;

    private void Update()
    {
        foreach (GameObject i in floor) {
            if (i.active) {
                i.transform.position = new Vector3(i.transform.position.x - floor_speed * Time.deltaTime, i.transform.position.y, i.transform.position.z);
            }
            chick(i);
        }
    }
    void chick(GameObject i) {
        if (i.transform.position.x <= limit_Left.position.x) {
            i.transform.position = new Vector3(limit_Right.position.x, i.transform.position.y, i.transform.position.z);
            i.SetActive(false);
            int j = UnityEngine.Random.Range(0, floor.Length);
            while (true) {
                j = UnityEngine.Random.Range(0, floor.Length);
                if (!floor[j].active)
                {
                    floor[j].SetActive(true);
                    break;
                }
            }

        }
        
    }

}
