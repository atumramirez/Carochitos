using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("Components")]
    public GameObject DialoguePanel;
    public Button NextButton;
    
    public TextMeshProUGUI DialogueText;

    [Header("Speaker Name")]
    public GameObject LeftSpeaker;
    public TextMeshProUGUI LeftSpeakerNameText;
    public GameObject RightSpeaker;
    public TextMeshProUGUI RightSpeakerNameText;

    [Header("Sprites")]
    public Image LeftSprite;
    public Image LeftExpressionSprite;
    public Image RightSprite;
    public Image RightExpressionSprite;

    [Header("Choice Button")]
    public Button ChoiceButton;
    public Transform ChoiceButtonContainer;

    [Header("Background")]
    public Image Forground;
    public Image Background;

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
        LeftSprite.gameObject.SetActive(false);
        RightSprite.gameObject.SetActive(false);
        LeftSpeaker.SetActive(false);
        RightSpeaker.SetActive(false);

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

        if (isReading != true)
        {
            EndDialogue();
        }
    }

    public void StartDialogue()
    {
        if (ActionManager.Instance.CurrentNode is not BaseDialogueAction node) return;

        bool hasChoices = node is QuestionAction;

        if (NextButton != null)
        {
            NextButton.gameObject.SetActive(!hasChoices);
        }

        if (!isReading)
        {
            isReading = true;
            DialoguePanel.SetActive(true);
        }

        if (node.CharacterSprite != null)
        {
            switch (node.Side)
            {
                case Side.Left:

                    RightSpeaker.SetActive(false);
                    LeftSpeaker.SetActive(true);

                    LeftSpeakerNameText.SetText(node.CharacterSprite._name);

                    if (node.CharacterSprite != null)
                    {
                        LeftSprite.gameObject.SetActive(true);

                        LeftExpressionSprite.enabled = true;


                        LeftSprite.sprite = node.CharacterSprite._baseSprite;
                        LeftSprite.SetNativeSize();

                        LeftExpressionSprite.sprite = node.CharacterSprite.SelectEmotion(node.Emotion);
                        LeftExpressionSprite.SetNativeSize();
                    }
                    else
                    {
                        LeftSprite.enabled = false;
                        LeftExpressionSprite.enabled = false;
                    }

                    break;

                case Side.Right:

                    LeftSpeaker.SetActive(false);
                    RightSpeaker.SetActive(true);

                    RightSpeakerNameText.SetText(node.CharacterSprite._name);

                    if (node.CharacterSprite != null)
                    {
                        RightSprite.gameObject.SetActive(true);

                        RightExpressionSprite.enabled = true;

                        RightSprite.sprite = node.CharacterSprite._baseSprite;
                        RightSprite.SetNativeSize();

                        RightExpressionSprite.sprite = node.CharacterSprite.SelectEmotion(node.Emotion);
                        RightExpressionSprite.SetNativeSize();
                    }
                    else
                    {
                        RightSprite.enabled = false;
                        RightExpressionSprite.enabled = false;
                    }

                    break;
            }
        }
        

        DialogueText.SetText(node.DialogueText);

        foreach (Transform child in ChoiceButtonContainer)
        {
            Destroy(child.gameObject);
        }

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


    public void ChangeBackground()
    {
        if (ActionManager.Instance.CurrentNode is not ChangeBackgroundAction node) return;

        Background.sprite = node.Background;
    }

    public void AddSprite(Side side)
    {
        switch (side)
        {
            case Side.Left:
                LeftSprite.gameObject.SetActive(true);
                break;

            case Side.Right:
                RightSprite.gameObject.SetActive(true);
                break;
        }
    }

    public void RemoveSprite(Side side)
    {
        switch (side)
        {
            case Side.Left:
                LeftSprite.gameObject.SetActive(false);
                break;

            case Side.Right:
                RightSprite.gameObject.SetActive(false);
                break;
        }
    }



    // Image Faiding
    [SerializeField] private float fadeDuration = 1f;
    private Coroutine currentFade;

    public void FadeIn()
    {
        StartFade(1f);
    }

    public void FadeOut()
    {
        StartFade(0f);
    }

    private void StartFade(float targetAlpha)
    {
        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(FadeRoutine(targetAlpha));
    }

    private IEnumerator FadeRoutine(float targetAlpha)
    {
        Color color = Forground.color;
        float startAlpha = color.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            color.a = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            Forground.color = color;

            yield return null;
        }

        color.a = targetAlpha;
        Forground.color = color;

        ActionManager.Instance.EndAction();
    }


    // Wait
    public void Wait(int duration)
    {
        StartCoroutine(PauseAction(duration));
    }

    private IEnumerator PauseAction(int duration)
    {
        yield return new WaitForSeconds(duration);
        ActionManager.Instance.EndAction();
    }
}
