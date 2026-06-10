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
        else
        {
            MenuManager.Instance.SetMenu(MenuManager.MenuState.Dialouge);
            DialogueManager.Instance.StartDialogue("State_7");
        }
    }
    void Collect()
    {
        itemSO.AddItemCount();
        Destroy(gameObject);
    }
}
