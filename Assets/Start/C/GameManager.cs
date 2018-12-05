using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float itemMoveSpeed = 0.01f;
    public float itemWaitSecond = 0.1f;
    private Transform oldItem;

    public void ItemMove(Transform item)
    {
        StartCoroutine(Move(item));
        /*while (true)
        {
            yield return new WaitForSeconds(0.1f);
            oldMe = me;
            me.position = new Vector2(oldMe.position.x - 0.01f, oldMe.position.y);
        }*/
    }

    IEnumerator Move(Transform item)
    {
        while (true)
        {
            yield return new WaitForSeconds(itemWaitSecond);
            oldItem = item;
            item.position = new Vector2(oldItem.position.x - itemMoveSpeed, oldItem.position.y);
        }
    }
}
