using UnityEngine;
using UnityEngine.Events;

public class IInteractable : MonoBehaviour 
{
    [Header("Trigger")]
    public Trigger trigger = Trigger.ButtonPress;

    [Header("Action Graphs")]
    public RuntimeDialogueGraph dialogueGraph;

    [Header("Events")]
    public UnityEvent onInteract;
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
        if (dialogueGraph != null)
        {
            ActionManager.Instance.OpenGraph(dialogueGraph);
        }
        else if (onInteract != null)
        {
            onInteract?.Invoke();
            return;
        }
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
