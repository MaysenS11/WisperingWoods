using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Ink.Runtime;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("Ink Configuration")]
    [SerializeField] private TextAsset inkJSONAsset;
    private Story _currentStory;

    [Header("UI References")]
    [SerializeField] private UIDocument dialogueUIDocument;
    private Label _dialogueTextLabel;
    private VisualElement _playerNameTag;
    private VisualElement _npcNameTag;

    [Header("Global Settings")]
    [SerializeField] private List<ItemSO> trackableItems = new List<ItemSO>();

    private bool _isDialogueActive;
    private const string NAME_VAR = "name";

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple DialogueManagers found!");
            return;
        }
        Instance = this;

        if (dialogueUIDocument == null)
        {
            dialogueUIDocument = GetComponent<UIDocument>();
        }

        if (dialogueUIDocument == null || dialogueUIDocument.rootVisualElement == null)
        {
            Debug.LogWarning("Dialogue UIDocument or Root missing. Will retry on first use.");
        }
        else
        {
            InitializeUIReferences();
        }

        InitializeStory();
    }

    private void InitializeUIReferences()
    {
        if (dialogueUIDocument == null || dialogueUIDocument.rootVisualElement == null) return;

        var root = dialogueUIDocument.rootVisualElement;
        _dialogueTextLabel = root.Q<Label>("DialougeText");
        _playerNameTag = root.Q<VisualElement>("Player_Name");
        _npcNameTag = root.Q<VisualElement>("NPC_Name");

        ResetUI();
    }

    private void InitializeStory()
    {
        if (inkJSONAsset == null)
        {
            Debug.LogWarning("Ink JSON Asset not assigned to DialogueManager");
            return;
        }

        _currentStory = new Story(inkJSONAsset.text);
        
        // Bind any external functions or variables here if needed
        _currentStory.variablesState.variableChangedEvent += OnVariableChanged;
    }

    private void OnEnable()
    {
        if (GameEventManager.Instance != null && GameEventManager.Instance.inputEvents != null)
        {
            GameEventManager.Instance.inputEvents.OnSubmitPressed += HandleSubmit;
        }
    }

    private void OnDisable()
    {
        if (GameEventManager.Instance != null && GameEventManager.Instance.inputEvents != null)
        {
            GameEventManager.Instance.inputEvents.OnSubmitPressed -= HandleSubmit;
        }
    }

    private void OnDestroy()
    {
        if (_currentStory != null)
        {
            _currentStory.variablesState.variableChangedEvent -= OnVariableChanged;
        }
    }

    private void OnVariableChanged(string varName, Ink.Runtime.Object newValue)
    {
        if (varName == NAME_VAR)
        {
            UpdateNameTags(newValue.ToString());
        }
        
        // Handle Quest variable changes from Ink
        if (varName.StartsWith("q_"))
        {
            string questId = varName.Substring(2);
            // In Ink 2.1.0+, values are accessed safely via the specific Ink runtime types
            int stateInt = 0;
            if (newValue is Ink.Runtime.IntValue intVal) {
                stateInt = intVal.value;
            }
            
            QuestState newState = (QuestState)stateInt;
            QuestManager.Instance.UpdateQuestState(questId, newState);
        }
    }

    public void StartDialogue(string knotName = null)
    {
        if (_currentStory == null)
        {
            Debug.LogError("Cannot start dialogue: Story is null");
            return;
        }

        // Re-initialize UI references in case UIDocument was reloaded or just enabled
        InitializeUIReferences();

        // Ensure UI is clean before starting
        ResetUI();

        // Unsubscribe before reset to avoid duplicate events
        _currentStory.variablesState.variableChangedEvent -= OnVariableChanged;
        
        // Reset the story to its initial state if we're starting a fresh conversation
        _currentStory.ResetState();
        
        // Re-subscribe after reset
        _currentStory.variablesState.variableChangedEvent += OnVariableChanged;

        // CRITICAL: Synchronize Unity data BEFORE choosing the path
        SyncUnityStateToInk();

        if (!string.IsNullOrEmpty(knotName))
        {
            try 
            {
                _currentStory.ChoosePathString(knotName);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error choosing path {knotName}: {e.Message}");
                // Fallback to start of story if knot fails
            }
        }

        _isDialogueActive = true;
        MenuManager.Instance.SetMenu(MenuManager.MenuState.Dialouge);
        
        // Initial name tag update based on current variable state
        UpdateNameTags(_currentStory.variablesState[NAME_VAR]?.ToString());
        
        ContinueStory();
    }

    private void SyncUnityStateToInk()
    {
        // Push item counts to Ink variables (e.g., item_wood)
        foreach (var item in trackableItems)
        {
            string inkVarName = "item_" + item.ItemName.ToLower().Replace(" ", "_");
            try 
            {
                // Assigning directly to variablesState indexer works if the variable is defined in Ink
                _currentStory.variablesState[inkVarName] = item.GetItemCount();
            }
            catch (System.Exception) 
            {
                // Variable doesn't exist in Ink, ignore
            }
        }

        // Push quest states to Ink (e.g., q_wood_quest)
        // We create a copy of the keys to avoid "Collection was modified" exception
        List<string> inkVariables = new List<string>();
        foreach (var varName in _currentStory.variablesState)
        {
            inkVariables.Add(varName);
        }

        foreach (var varName in inkVariables)
        {
            if (varName.StartsWith("q_"))
            {
                string questId = varName.Substring(2);
                QuestState state = QuestManager.Instance.GetQuestState(questId);
                _currentStory.variablesState[varName] = (int)state;
            }
        }
    }

    private void HandleSubmit()
    {
        if (!_isDialogueActive) return;
        ContinueStory();
    }

    private void ContinueStory()
    {
        if (_currentStory.canContinue)
        {
            string text = _currentStory.Continue();
            
            // Check if what we continued was actually content or just a state change
            if (string.IsNullOrWhiteSpace(text) && _currentStory.canContinue)
            {
                ContinueStory();
                return;
            }

            // Handle tags
            HandleTags(_currentStory.currentTags);
            
            _dialogueTextLabel.text = text.Trim();
            
            // Ensure name tags are correct after a continue
            UpdateNameTags(_currentStory.variablesState[NAME_VAR]?.ToString());
        }
        else if (_currentStory.currentChoices.Count > 0)
        {
            // Simple choice handling
            _currentStory.ChooseChoiceIndex(0);
            ContinueStory();
        }
        else
        {
            ExitDialogue();
        }
    }

    private void HandleTags(List<string> tags)
    {
        foreach (string tag in tags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length < 2) continue;

            string key = splitTag[0].Trim().ToUpper();
            string value = splitTag[1].Trim();

            switch (key)
            {
                case "QUEST":
                    if (splitTag.Length >= 3)
                    {
                        string questId = value;
                        string status = splitTag[2].Trim().ToUpper();
                        QuestState state = status switch
                        {
                            "START" => QuestState.InProgress,
                            "COMPLETE" => QuestState.Completed,
                            _ => QuestState.NotStarted
                        };
                        QuestManager.Instance.UpdateQuestState(questId, state);
                    }
                    break;
                case "ITEM":
                    // Event for item related logic if needed
                    break;
            }
        }
    }

    private void UpdateNameTags(string currentName)
    {
        //Debug.Log($"Updating name tags for: '{currentName}'");
        
        // Handle Ink string representation which might include quotes
        if (!string.IsNullOrEmpty(currentName))
        {
            currentName = currentName.Replace("\"", "").Trim();
        }

        if (string.IsNullOrEmpty(currentName))
        {
            if (_playerNameTag != null) _playerNameTag.style.display = DisplayStyle.None;
            if (_npcNameTag != null) _npcNameTag.style.display = DisplayStyle.None;
            return;
        }

        if (currentName == "Char")
        {
            if (_playerNameTag != null) _playerNameTag.style.display = DisplayStyle.Flex;
            if (_npcNameTag != null) _npcNameTag.style.display = DisplayStyle.None;
        }
        else if (currentName == "NPC")
        {
            if (_playerNameTag != null) _playerNameTag.style.display = DisplayStyle.None;
            if (_npcNameTag != null) _npcNameTag.style.display = DisplayStyle.Flex;
        }
        else
        {
            // If it's a name we don't recognize, hide both tags 
            // instead of leaving the previous one visible
            if (_playerNameTag != null) _playerNameTag.style.display = DisplayStyle.None;
            if (_npcNameTag != null) _npcNameTag.style.display = DisplayStyle.None;
        }
    }

    private void ResetUI()
    {
        if (_playerNameTag != null) _playerNameTag.style.display = DisplayStyle.None;
        if (_npcNameTag != null) _npcNameTag.style.display = DisplayStyle.None;
    }
    

    private void ExitDialogue()
    {
        if (QuestManager.Instance.IsQuestCompleted("candle_quest"))
        {
            StartCoroutine(MenuManager.Instance.ExampleCoroutine());
        }
        if (MenuManager.Instance.saveGame == false)
        {
            //Debug.Log("Setting saveGame to true on MenuManager");
            MenuManager.Instance.saveGame = true;
            MenuManager.Instance.SetMenu(MenuManager.MenuState.Ingame);
        }
        _isDialogueActive = false;
        MenuManager.Instance.SetMenu(MenuManager.MenuState.Ingame);
        ResetUI();
    }
    
    
}
