using System.Collections.Generic;
using UnityEngine;

public class ShrineUpdate : MonoBehaviour
{
    [SerializeField] private GameObject defShrine;
    [SerializeField] private GameObject woodShrine;
    [SerializeField] private GameObject reliquaryShrine;
    [SerializeField] private GameObject candleShrine;
    
    private Dictionary<string, GameObject> _shrineDict = new Dictionary<string, GameObject>();
    void Start()
    {
        QuestManager.Instance.OnQuestStateChanged += QuestChanged;
        _shrineDict["default_quest"] = defShrine;
        _shrineDict["wood_quest"] = woodShrine;
        _shrineDict["candle_quest"] = candleShrine;
        _shrineDict["reliquary_quest"] = reliquaryShrine;
    }
    
    void QuestChanged(string questName, QuestState state)
    {
        if (state == QuestState.Completed)
        {
            foreach (var keyValue in _shrineDict)
            {
                if (keyValue.Key == questName)
                {
                    keyValue.Value.SetActive(true);
                }
                else
                {
                    keyValue.Value.SetActive(false);
                }
            }
        }
    }
}
