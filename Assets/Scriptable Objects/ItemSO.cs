using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    [Header("")]
    public string ItemName;
    public int requiredItems;
    
    [System.NonSerialized]
    private int _itemCount;
    public bool canCollect;
    

    /*public void SaveItemCollected()
    {
        string saveKey = $"Item_{ItemName}_Collected";
        PlayerPrefs.SetInt(saveKey, _itemCount);
        PlayerPrefs.SetInt(saveKey, Convert.ToInt32(canCollect));
        PlayerPrefs.Save();
    }*/

    public void LoadItemCollected()
    {
        string saveKey = $"Item_{ItemName}_Collected";
        _itemCount = PlayerPrefs.GetInt(saveKey, 0);
        canCollect = Convert.ToBoolean(PlayerPrefs.GetInt(saveKey));
    }

    public int GetItemCount()
    {
        return _itemCount;
    }

    public void AddItemCount()
    {
        _itemCount++;
        //SaveItemCollected();
        Debug.Log("ItemCount " + ItemName + " " + _itemCount);
        //CollectItem(string ItemName);
    }
}
