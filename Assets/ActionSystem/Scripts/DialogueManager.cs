using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("Components")]
    public GameObject DialoguePanel;
    public Button NextButton;
    public TextMeshProUGUI SpeakerNameText;
    public TextMeshProUGUI DialogueText;

    [Header("Choice Button")]
    public Button ChoiceButton;
    public Transform ChoiceButtonContainer;

    private bool isReading = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (NextButton != null)
        {
            NextButton.onClick.AddListener(() =>
            {
                if (isReading)
                {
                    NextLine();
                }
            });
        }
        
    }

    // Called from Perform()
    public void StartDialogue()
    {
        if (ActionManager.Instance.CurrentNode is not BaseDialogueAction node) return;

        bool hasChoices = node is QuestionAction;

        // Show / hide continue button
        if (NextButton != null)
        {
            NextButton.gameObject.SetActive(!hasChoices);
        }

        // Open UI if needed
        if (!isReading)
        {
            isReading = true;
            DialoguePanel.SetActive(true);
        }

        // Set text
        SpeakerNameText.SetText(node.SpeakerName);
        DialogueText.SetText(node.DialogueText);

        // Clear old choices
        foreach (Transform child in ChoiceButtonContainer)
        {
            Destroy(child.gameObject);
        }

        // Handle choices
        if (node is QuestionAction questionNode)
        {
            foreach (var choice in questionNode.Choices)
            {
                Button button = Instantiate(ChoiceButton, ChoiceButtonContainer);
                var text = button.GetComponentInChildren<TextMeshProUGUI>();

                if (text != null)
                    text.text = choice.ChoiceText;

                button.onClick.AddListener(() =>
                {
                    if (string.IsNullOrEmpty(choice.DestinationNodeID))
                    {
                        Debug.LogWarning("Choice has no destination.");
                        return;
                    }

                    NextLine(choice.DestinationNodeID);
                });
            }
        }
    }

    // Called from End() or Input
    public void NextLine(string overrideNodeID = null)
    {
        var current = ActionManager.Instance.CurrentNode;

        if (current == null)
        {
            EndDialogue();
            return;
        }

        // Block continue during choices
        if (current is QuestionAction && string.IsNullOrEmpty(overrideNodeID))
        {
            return;
        }

        // Preview next node to decide UI behavior
        string nextID = overrideNodeID ?? current.NextNodeID;

        if (!ActionManager.Instance.NodeList.TryGetValue(nextID, out var nextNode))
        {
            EndDialogue();
            ActionManager.Instance.CloseGraph();
            return;
        }

        // Close UI if next is not dialogue
        if (nextNode is not BaseDialogueAction)
        {
            EndDialogue();
        }

        // One unified call
        ActionManager.Instance.EndAction(overrideNodeID);
    }

    public void EndDialogue()
    {
        isReading = false;
        DialoguePanel.SetActive(false);

        foreach (Transform child in ChoiceButtonContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
