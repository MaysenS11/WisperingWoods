using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSO itemSO;

    void OnTriggerEnter2D(Collider2D other)
    {
        //AddListenerSubmit += Collect;
        if (itemSO.canCollect)
        {
            Collect();
        }
        else
        {
            //invokeDisplayCollectLater
            Collect(); //DebugOnly
        }
    }
    void Collect()
    {
        itemSO.AddItemCount();
        Destroy(gameObject);
    }
}
