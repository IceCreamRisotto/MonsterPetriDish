using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click_event : MonoBehaviour {

     public GameObject image;
     private bool click_type = false;

    private void Update()
    {
        if (click_type) {
            image.GetComponent<Image>().color = new Color32(200, 200, 200, 255);
        }
    }

    public void press(bool start) {
         click_type = start;
         if (click_type)
         {
            image.GetComponent<Image>().color = new Color32(200,200,200,255);
        }
         else if(!click_type){
            image.GetComponent<Image>().color = new Color32(255,255,255,200);
        }
     }
}
