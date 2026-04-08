using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("Components")]
    public GameObject DialoguePanel;
    public TextMeshProUGUI SpeakerNameText;
    public TextMeshProUGUI DialogueText;

    [Header("Choice Button")]
    public Button ChoiceButton;
    public Transform ChoiceButtonContainer;

    private bool isReading;
    private BaseDialogueAction currentDialogueAction;

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
        isReading = false;
        DialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (currentDialogueAction != null)
        {
            if (isReading == true && currentDialogueAction is SpeakAction speakAction)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    NextLine(currentDialogueAction.NextNodeID);
                }
            }
        } 
    }

    public void StartDialogue(BaseDialogueAction baseDialogueAction)
    {
        currentDialogueAction = baseDialogueAction;

        if (isReading == false)
        {
            isReading = true;
            DialoguePanel.SetActive(true);
        }

        SpeakerNameText.SetText(currentDialogueAction.SpeakerName);
        DialogueText.SetText(currentDialogueAction.DialogueText);

        foreach (Transform child in ChoiceButtonContainer)
        {
            Destroy(child.gameObject);
        }

        if (currentDialogueAction is QuestionAction questionAction)
        {
            Debug.Log("Choices count: " + questionAction.Choices.Count);

            foreach (var choice in questionAction.Choices)
            {
                Button button = Instantiate(ChoiceButton, ChoiceButtonContainer);
                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

                if (buttonText != null)
                {
                    buttonText.text = choice.ChoiceText;
                }

                if (button != null)
                {
                    button.onClick.AddListener(() =>
                    {
                        if (!string.IsNullOrEmpty(choice.DestinationNodeID))
                        {
                            NextLine(choice.DestinationNodeID);
                        }
                        else
                        {
                            EndDialogue();
                            ActionManager.Instance.EndGraph();
                            return;
                        }
                    });
                }
            }
        }
    }

    public void NextLine(string nodeID)
    {
        if (!ActionManager.Instance.NodeList.ContainsKey(nodeID))
        {
            EndDialogue();
            ActionManager.Instance.EndGraph();
            return;
        }
        else
        {
            if (ActionManager.Instance.NodeList[nodeID] is not BaseDialogueAction)
            {
                EndDialogue();
            }

            ActionManager.Instance.NextAction(nodeID);
        }
    }

    public void EndDialogue()
    {
        currentDialogueAction = null;

        isReading = false;
        DialoguePanel.SetActive(false);

        foreach (Transform child in ChoiceButtonContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
