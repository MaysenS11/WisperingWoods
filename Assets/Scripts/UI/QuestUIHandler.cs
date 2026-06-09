using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class QuestUIHandler : MonoBehaviour
{
    [SerializeField] private UIDocument questUIDocument;
    private VisualElement _questPopUp;
    private Label _questText;
    
    [SerializeField] private ItemSO woodItem; // Reference to the wood scriptable object
    [SerializeField] private ItemSO reliquaryItem;
    [SerializeField] private ItemSO candleItem;

    private void Awake()
    {
        if (questUIDocument == null)
        {
            questUIDocument = GetComponent<UIDocument>();
        }
    }

    private void OnEnable()
    {
        if (questUIDocument == null || questUIDocument.rootVisualElement == null) return;

        var root = questUIDocument.rootVisualElement;
        _questPopUp = root.Q<VisualElement>("QuestPopUp");
        _questText = root.Q<Label>("QuestText");
        
        // Hide on start
        if (_questPopUp != null) _questPopUp.style.display = DisplayStyle.None;

        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.OnQuestStateChanged += HandleQuestStateChanged;
            
            // Check current status in case quest is already active
            UpdateQuestDisplay("wood_quest", QuestManager.Instance.GetQuestState("wood_quest"));
        }
    }

    private void OnDisable()
    {
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.OnQuestStateChanged -= HandleQuestStateChanged;
        }
    }

    private void HandleQuestStateChanged(string questId, QuestState state)
    {
        UpdateQuestDisplay(questId, state);
    }
    
    private void Update()
    {
        // Update the display for the current active quest
        if (QuestManager.Instance.IsQuestInProgress("wood_quest"))
        {
            RefreshQuestText("wood_quest", woodItem);
        }
        else if (QuestManager.Instance.IsQuestInProgress("reliquary_quest"))
        {
            RefreshQuestText("reliquary_quest", reliquaryItem);
        }
        else if (QuestManager.Instance.IsQuestInProgress("candle_quest"))
        {
            RefreshQuestText("candle_quest", candleItem);
        }
    }

    private void UpdateQuestDisplay(string questId, QuestState state)
    {
        // Show popup if ANY quest is in progress
        bool anyInProgress = QuestManager.Instance.IsQuestInProgress("wood_quest") || 
                             QuestManager.Instance.IsQuestInProgress("reliquary_quest") || 
                             QuestManager.Instance.IsQuestInProgress("candle_quest");

        if (anyInProgress)
        {
            if (_questPopUp != null) _questPopUp.style.display = DisplayStyle.Flex;
        }
        else
        {
            if (_questPopUp != null) _questPopUp.style.display = DisplayStyle.None;
        }
    }

    private void RefreshQuestText(string questId, ItemSO item)
    {
        if (_questText == null || item == null) return;
        
        string description = QuestManager.Instance.GetQuestDescription(questId);
        int current = item.GetItemCount();
        int required = item.requiredItems;
        
        _questText.text = $"{description} {current}/{required}";
    }
}
