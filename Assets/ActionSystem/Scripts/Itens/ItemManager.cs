using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemManager : MonoBehaviour
{
    [Header("Components")]
    public GameObject DialoguePanel;
    public TextMeshProUGUI SpeakerNameText;
    public TextMeshProUGUI DialogueText;

    private bool isReading = false;
    private GiveItemAction BaseActionNode;

    public static ItemManager Instance;

    private void Awake()
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


    /*
    private void Update()
    {
        if (BaseActionNode != null)
        {
            if (isReading == true && BaseActionNode is GiveItemAction)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    CloseBox();
                }
            }
        }
    }


    public void OpenBox(GiveItemAction baseActionNode)
    {
        isReading = true;
        DialoguePanel.SetActive(true);

        BaseActionNode = baseActionNode;

        SpeakerNameText.SetText(BaseActionNode.Item.Name);
        DialogueText.SetText(BaseActionNode.Item.Description); 
    }

    public void CloseBox()
    {
        ActionManager.Instance.NextAction(BaseActionNode.NextNodeID);
        isReading = false;
        DialoguePanel.SetActive(false);
        BaseActionNode = null;
    }
    */
}
