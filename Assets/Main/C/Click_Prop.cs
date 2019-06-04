using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click_Prop : MonoBehaviour {

    private object copy;                        //要複製的東西 
    private GameObject copy2;
    private RectTransform copytransfrom;        //要複製的座標大小
    public GameObject super;    //要複製的物件位置
    private GameObject child;                   //複製出來的物件
    GameManager gameManager;

    private void Awake()
    {
        copy = Resources.Load("prop/"+ gameObject.name);
        copy2 = copy as GameObject;
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void eat(int itemID) {
        if (gameManager.items[itemID] > 0)
        {
            copytransfrom = GameObject.Find(gameObject.name).GetComponent<RectTransform>();
            child = Instantiate(copy2);
            child.transform.parent = super.transform;//放到superGameObject物件內
            RectTransform child2 = child.GetComponent<RectTransform>();
            child2.position = copytransfrom.position;
            child2.localScale = copytransfrom.localScale;
            child2.sizeDelta = copytransfrom.sizeDelta;
        }
    }

}
