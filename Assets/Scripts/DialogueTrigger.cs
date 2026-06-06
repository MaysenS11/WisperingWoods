using UnityEngine;
using UnityEngine.UIElements;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] UIDocument dialougeDocument;

    void Awake()
    {
        VisualElement root = dialougeDocument.rootVisualElement;
        root.SetEnabled(true);
    }
}
