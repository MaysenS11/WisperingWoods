using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSO itemSO;
    [SerializeField] private string _questName;

    void Start()
    {
        GameEventManager.Instance.gameObject.GetComponent<QuestManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //AddListenerSubmit += Collect;
        if (QuestManager.Instance.IsQuestInProgress(_questName))
        {
            Collect();
        }
    }
    void Collect()
    {
        itemSO.AddItemCount();
        Destroy(gameObject);
    }
}
