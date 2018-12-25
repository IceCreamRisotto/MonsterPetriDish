using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackMove : MonoBehaviour {
    public RectTransform oun;
    private RectTransform parent;
    public RectTransform handle;
    private Vector2 oldParent;
    private bool dragging = false;
    private bool dragg = false;
    // Use this for initialization
    void Start () {
        parent = GetComponent<RectTransform>();
        //oldParent = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
        Drag();
	}

    /*private void FixedCoordinatesY()
    {
        parent.anchoredPosition
    }*/

    private void Drag()
    {
        if (!dragging) { return; }
        Vector2 newPos = new Vector2(0f, 0f);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(oun, Input.mousePosition, null, out newPos);
        Vector2 result = new Vector2(newPos.x, 0);
        //handle.anchoredPosition = result;
        if (!dragg) { dragg = true; oldParent = result- new Vector2(handle.position.x, handle.position.y); }
        handle.anchoredPosition = result-oldParent;
        
        //handle.anchoredPosition = new Vector2(parent.position.x-oldParent.x,0);
    }

    public void StartDrag()
    {
        dragging = true;
    }

    public void EndDrag()
    {
        dragging = false;
        dragg = false;
        //oldParent = parent;
        parent.anchoredPosition = Vector2.zero;
    }
}
