using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    [Header("Components")]
    public GameObject DialoguePanel;
    public TextMeshProUGUI SpeakerNameText;
    public TextMeshProUGUI DialogueText;

    [Header("Choice Button")]
    public Button ChoiceButton;
    public Transform ChoiceButtonContainer;

    private bool isReading;

    private readonly Dictionary<string, RuntimeDialogueNode> _nodeLookup = new();
    private RuntimeDialogueNode _currentNode;

    private void Update()
    {
        // Mudar isto para o New Input System
        if (isReading == true)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame && _currentNode != null && _currentNode.Choices.Count == 0)
            {
                NextLine();
            }
        }
    }

    private void Start()
    {
        isReading = false;
        DialoguePanel.SetActive(isReading);
    }

    public void StartDialogue(RuntimeDialogueGraph RuntimeGraph)
    {
        if (isReading != true)
        {
            foreach (var node in RuntimeGraph.AllNodes)
            {
                _nodeLookup[node.NodeID] = node;
            }

            if (!string.IsNullOrEmpty(RuntimeGraph.EntryNodeID))
            {
                isReading = true;
                ShowNode(RuntimeGraph.EntryNodeID);
            }
            else
            {
                EndDialogue();
            }
        }
    }

    public void NextLine()
    {
        if (!string.IsNullOrEmpty(_currentNode.NextNodeID))
        {
            ShowNode(_currentNode.NextNodeID);
        }
        else
        {
            EndDialogue();
        }
    }

    private void ShowNode(string nodeID)
    {
        if (!_nodeLookup.ContainsKey(nodeID))
        {
            EndDialogue();
            return;
        }

        _currentNode = _nodeLookup[nodeID];

        // Adicionar Animação da Caixa de Texto Aparecer
        DialoguePanel.SetActive(true);

        // Adicionar Animação de Escrever Texto
        SpeakerNameText.SetText(_currentNode.SpeakerName);
        DialogueText.SetText(_currentNode.DialogueText);

        // Destroy any children of the button container
        foreach (Transform child in ChoiceButtonContainer)
        {
            Destroy(child.gameObject);
        }

        if (_currentNode.Choices.Count > 0)
        {
            foreach (var choice in _currentNode.Choices)
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
                            ShowNode(choice.DestinationNodeID);
                        }
                        else
                        {
                            EndDialogue();
                        }
                    });
                }
            }
        }
    }

    private void EndDialogue()
    {
        _currentNode = null;

        // Adicionar Animação de a Caixa de Texto a desaparecer
        DialoguePanel.SetActive(false);

        // Destroy any children of the button container
        foreach (Transform child in ChoiceButtonContainer)
        {
            Destroy(child.gameObject);
        }

        isReading = false;
    }
}
