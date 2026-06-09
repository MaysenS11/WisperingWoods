using System;
using System.Collections.Generic;
using UnityEngine;

public enum QuestState
{
    NotStarted,
    InProgress,
    Completed
}

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    // Dictionary to store quest states: Key is quest ID, Value is state
    private Dictionary<string, QuestState> _questStates = new Dictionary<string, QuestState>();
    private Dictionary<string, string> _questDescriptions = new Dictionary<string, string>();

    // Event for when a quest state changes
    public event Action<string, QuestState> OnQuestStateChanged;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        // Initialize descriptions for known quests
        _questDescriptions["wood_quest"] = "Find sticks";
        _questDescriptions["reliquary_quest"] = "Find the reliquary";
        _questDescriptions["candle_quest"] = "Find candles";
    }

    public string GetQuestDescription(string questId)
    {
        return _questDescriptions.ContainsKey(questId) ? _questDescriptions[questId] : questId;
    }

    public void UpdateQuestState(string questId, QuestState newState)
    {
        if (!_questStates.ContainsKey(questId) || _questStates[questId] != newState)
        {
            _questStates[questId] = newState;
            Debug.Log($"Quest '{questId}' changed to {newState}");
            OnQuestStateChanged?.Invoke(questId, newState);
            
            // Sync with PlayerPrefs for persistence if needed
            PlayerPrefs.SetInt($"Quest_{questId}", (int)newState);
            PlayerPrefs.Save();
        }
    }

    public QuestState GetQuestState(string questId)
    {
        if (_questStates.TryGetValue(questId, out QuestState state))
        {
            return state;
        }
        
        // Try to load from PlayerPrefs
        int savedState = PlayerPrefs.GetInt($"Quest_{questId}", (int)QuestState.NotStarted);
        _questStates[questId] = (QuestState)savedState;
        return (QuestState)savedState;
    }

    public bool IsQuestCompleted(string questId)
    {
        return GetQuestState(questId) == QuestState.Completed;
    }

    public bool IsQuestInProgress(string questId)
    {
        return GetQuestState(questId) == QuestState.InProgress;
    }
}
