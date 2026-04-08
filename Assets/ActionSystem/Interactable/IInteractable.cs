using UnityEngine;

public class IInteractable : MonoBehaviour 
{
    [Header("Trigger")]
    public Trigger trigger = Trigger.ButtonPress;

    public RuntimeDialogueGraph dialogueGraph;
    public enum Trigger
    {
        ButtonPress,
        EnterArea,
        ExitArea,
        Autorun
    }

    public void Start()
    {
        if (trigger == Trigger.Autorun)
        {
            OnInteract();
        }
    }

    public void OnInteract()
    {
        ActionManager.Instance.OpenGraph(dialogueGraph);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (trigger == Trigger.EnterArea)
        {
            OnInteract();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (trigger == Trigger.ExitArea)
        {
            OnInteract();
        }
    }
}
